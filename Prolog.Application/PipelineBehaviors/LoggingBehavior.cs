using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prolog.Abstractions.CommonModels;
using Prolog.Core.Loggers;
using Prolog.Core.Loggers.Helpers;
using Prolog.Domain;
using Prolog.Domain.Entities;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace Prolog.Application.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse> : IDisposable, IPipelineBehavior<TRequest, TResponse>
    where TRequest : ActionLogger
{
    private readonly ICurrentHttpContextAccessor _currentHttpContextAccessor;
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceScope _scope;

    public LoggingBehavior(IServiceProvider serviceProvider, ICurrentHttpContextAccessor currentHttpContextAccessor)
    {
        _currentHttpContextAccessor = currentHttpContextAccessor;
        _scope = serviceProvider.CreateScope();
        var appDbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _dbContext = appDbContext;
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
        _scope?.Dispose();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestType = typeof(TRequest);
        try
        {
            var response = await next();

            if (request is ActionLogger loggableRequest)
            {
                var actionName = requestType.Name;
                var descriptionAttribute = requestType.GetCustomAttribute(typeof(DescriptionAttribute), true);
                var description = string.Empty;
                if (descriptionAttribute != null)
                {
                    description = ((DescriptionAttribute)descriptionAttribute).Description;
                }

                await LogData(loggableRequest, description, actionName, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return response;
        }
        catch (Exception)
        {
            Dispose();
            throw;
        }
    }

    private async Task LogData(ActionLogger loggableRequest, string requestDescription, string actionName, CancellationToken cancellationToken)
    {
        var commonUserAction = GetCurrentActionLog(actionName, requestDescription);

        // Данные из свойств, помеченных атрибутом [LoggableProperty]
        var loggableProperties = LoggablePropertyHelper.ExtractLoggableProperties(loggableRequest);
        var logData = loggableProperties.ToDictionary(p => p.PropertyName, p => p.PropertyValue);

        // Данные из обработчика запроса, которые также необходимо залогировать
        var additionalData = loggableRequest.GetLogData();

        // Данные, записываемые в процессе выполения запроса имеют приоритет над данными,
        // записываемыми перед выполнением запроса, если поля имеют одинаковые имена
        additionalData.ToList().ForEach(x => logData[x.Key] = x.Value);

        var serializedCommonData = JsonSerializer.Serialize(logData);
        commonUserAction.Filter = JsonDocument.Parse(serializedCommonData);

        await _dbContext.ActionLogs.AddAsync(commonUserAction, cancellationToken);
    }

    private ActionLog GetCurrentActionLog(string actionName, string requestDescription)
    {
        return new ActionLog
        {
            ActionName = actionName,
            ActionDateTime = DateTime.UtcNow,
            IdentityUserId = _currentHttpContextAccessor.IdentityUserId,
            UserName = _currentHttpContextAccessor.UserName ?? string.Empty,
            UserSurname = _currentHttpContextAccessor.UserSurname ?? string.Empty,
            Description = requestDescription
        };
    }
}
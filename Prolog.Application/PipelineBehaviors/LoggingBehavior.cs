using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prolog.Abstractions.CommonModels;
using Prolog.Domain;
using Prolog.Domain.Entities;
using System.ComponentModel;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Prolog.Application.PipelineBehaviors;

public class LoggingBehaviour<TRequest, TResponse> : IDisposable, IPipelineBehavior<TRequest, TResponse>
    where TRequest : ILoggableAction
{
    private readonly ICurrentHttpContextAccessor _currentHttpContextAccessor;
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceScope _scope;

    public LoggingBehaviour(IServiceProvider serviceProvider, ICurrentHttpContextAccessor currentHttpContextAccessor)
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
        var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        try
        {
            if (_currentHttpContextAccessor.MethodName == "GET")
            {
                return await next();
            }
            var newUserAction = new ActionLog
            {
                ActionName = requestType.Name,
                ActionDateTime = DateTime.UtcNow,
                IdentityUserId = _currentHttpContextAccessor.IdentityUserId,
                UserName = _currentHttpContextAccessor.UserName ?? "",
                UserSurname = _currentHttpContextAccessor.UserSurname ?? "",
                RequestInfo = JsonSerializer.Serialize(request, options)
            };
            if (request is IEntityLoggableAction action)
            {
                newUserAction.EntityId = action.GetEntityId();
            }
            var attributes = requestType.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes.Length != 0)
            {
                var description = (DescriptionAttribute)attributes[0];
                var text = description.Description;
                newUserAction.Description = text;
            }

            var response = await next();

            await _dbContext.ActionLogs.AddAsync(newUserAction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return response;
        }
        catch (Exception)
        {
            Dispose();
            throw;
        }
    }
}
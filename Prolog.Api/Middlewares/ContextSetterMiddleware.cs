using Microsoft.AspNetCore.Authorization;
using Prolog.Abstractions.CommonModels;

namespace Prolog.Api.Middlewares;

public class ContextSetterMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ContextSetterMiddleware>();

    public async Task Invoke(HttpContext context, ICurrentHttpContextAccessor currentHttpContextAccessor)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
        {
            await next(context);
            return;
        }
        currentHttpContextAccessor.SetContext(context);
        await next(context);
    }
}
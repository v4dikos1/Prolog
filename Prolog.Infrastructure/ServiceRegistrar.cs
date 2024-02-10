using Microsoft.Extensions.DependencyInjection;

namespace Prolog.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterExternalInfrastructureServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        return services;
    }
}
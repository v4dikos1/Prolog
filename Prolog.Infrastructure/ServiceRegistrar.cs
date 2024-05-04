using Microsoft.Extensions.DependencyInjection;
using Prolog.Abstractions.Services;
using Prolog.Infrastructure.DaDataServices;

namespace Prolog.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterExternalInfrastructureServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        services.AddTransient<IDaDataService, DaDataService>();
        return services;
    }
}
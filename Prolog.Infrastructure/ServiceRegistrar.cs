using Microsoft.Extensions.DependencyInjection;
using Prolog.Abstractions.Services;
using Prolog.Infrastructure.DaDataServices;
using Prolog.Infrastructure.HttpClients;

namespace Prolog.Infrastructure;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterExternalInfrastructureServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        services.AddHttpClient<MapBoxHttpClient>();
        services.AddTransient<IDaDataService, DaDataService>();
        services.AddTransient<IMapBoxService, MapBoxService>();
        return services;
    }
}
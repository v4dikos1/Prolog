using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prolog.Abstractions.Services;
using Prolog.Services.Services;

namespace Prolog.Services;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterDomainInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IExternalSystemService, ExternalSystemService>();

        return services;
    }
}
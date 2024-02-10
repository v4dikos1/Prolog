using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Prolog.Services;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterDomainInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
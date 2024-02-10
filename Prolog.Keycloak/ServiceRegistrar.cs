using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Prolog.Core.Http.Extensions;
using Prolog.Core.Http.Features.TokenManagers;
using Prolog.Core.Http.Features.TokenManagers.CommonStrategies;
using Prolog.Core.Http.Features.TokenManagers.Models;
using Prolog.Keycloak.Abstractions;
using Prolog.Keycloak.Clients;
using Prolog.Keycloak.Models;
using Prolog.Keycloak.Services;

namespace Prolog.Keycloak;

public static class ServiceRegistrar
{
    public static IServiceCollection RegisterKeycloakServices(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var keycloakConfiguration = serviceProvider.GetRequiredService<IOptions<KeycloakConfigurationModel>>().Value;

        services.AddHttpClient<KeycloakHttpClient>();
        services.AddTransient<IIdentityService, KeycloakIdentityService>();
        services.RegisterJwtTokenManagerService(keycloakConfiguration.ExpirationTimeBySeconds);
        services.AddTransient<ITokenGenerateStrategy<OidcClientCredentialRequest>, OidcClientCredentialStrategy>();
        return services;
    }
}
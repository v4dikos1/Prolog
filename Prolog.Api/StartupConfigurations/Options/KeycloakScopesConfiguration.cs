using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Prolog.Api.StartupConfigurations.Models;
using Prolog.Core.Utils;

namespace Prolog.Api.StartupConfigurations.Options;

public class KeycloakScopesConfigurationSetup(IConfiguration configuration) : IConfigureOptions<KeycloakScopeConfigurationModel>
{
    public void Configure(KeycloakScopeConfigurationModel options)
    {
        var scopesConfiguration = configuration.GetSection("KeycloakScopesConfiguration");
        Validate(scopesConfiguration.Get<KeycloakScopeConfigurationModel>());
        scopesConfiguration.Bind(options);
    }

    private void Validate(KeycloakScopeConfigurationModel? config)
    {
        Defend.Against.Null(config, nameof(config));
        Defend.Against.NullOrEmpty(config.AdminScopeName, nameof(config.AdminScopeName));
    }
}
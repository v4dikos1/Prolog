using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Prolog.Abstractions.CommonModels.MapBoxService;
using Prolog.Core.Utils;

namespace Prolog.Api.StartupConfigurations.Options;

public class MapBoxConfiguration(IConfiguration configuration): IConfigureOptions<MapBoxServiceConfiguration>
{
    public void Configure(MapBoxServiceConfiguration options)
    {
        var mapBoxConfig = configuration.GetSection("MapBoxService");
        Validate(mapBoxConfig.Get<MapBoxServiceConfiguration>());
        mapBoxConfig.Bind(options);
    }

    private void Validate(MapBoxServiceConfiguration? config)
    {
        Defend.Against.Null(config, nameof(config));
        Defend.Against.NullOrEmpty(config.AccessToken, nameof(config.AccessToken));
    }
}
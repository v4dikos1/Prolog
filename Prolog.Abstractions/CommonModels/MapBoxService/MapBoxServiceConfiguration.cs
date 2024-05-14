namespace Prolog.Abstractions.CommonModels.MapBoxService;

public class MapBoxServiceConfiguration
{
    public required string BaseUrl { get; set; }
    public required string AccessToken { get; set; }
}
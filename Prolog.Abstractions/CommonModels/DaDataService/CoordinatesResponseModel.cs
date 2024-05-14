using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.DaDataService;

public class CoordinatesResponseModel
{
    [JsonPropertyName("geo_lon")]
    public string Longitude { get; set; } = string.Empty;

    [JsonPropertyName("geo_lat")]
    public string Latitude { get; set; } = string.Empty;
}
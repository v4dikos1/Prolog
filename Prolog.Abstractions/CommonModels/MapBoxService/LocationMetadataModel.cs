using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

public class LocationMetadataModel
{
    [JsonPropertyName("supplied_coordinate")]
    public required List<double> Coordinates { get; set; }
}
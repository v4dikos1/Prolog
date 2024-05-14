using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

public class StopModel
{
    [JsonPropertyName("location")]
    public required string LocationId { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("location_metadata")]
    public required LocationMetadataModel LocationMetadata { get; set; }
}
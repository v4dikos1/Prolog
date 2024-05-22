using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

public class DropOffTimeModel
{
    [JsonPropertyName("earliest")]
    public DateTimeOffset Earliest { get; set; }

    [JsonPropertyName("latest")]
    public DateTimeOffset Latest { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = "strict";
}
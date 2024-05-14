using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

public class RouteModel
{
    [JsonPropertyName("vehicle")]
    public required Guid VehicleId { get; set; }

    [JsonPropertyName("stops")]
    public required List<StopModel> Stops { get; set; }
}
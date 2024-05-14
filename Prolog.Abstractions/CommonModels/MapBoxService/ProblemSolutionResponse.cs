using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

public class ProblemSolutionResponse
{
    [JsonPropertyName("routes")]
    public required List<RouteModel> Routes { get; set; }
}
using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.DaDataService.Models.Query;

public class QueryBoundModel
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
}
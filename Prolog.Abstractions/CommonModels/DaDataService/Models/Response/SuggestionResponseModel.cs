using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.DaDataService.Models.Response;

public class SuggestionResponseModel
{
    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("unrestricted_value")]
    public string UnrestrictedValue { get; set; }

    [JsonPropertyName("data")]
    public DataResponseModel Data { get; set; }
}
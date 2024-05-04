using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.DaDataService.Models.Response;

public class AddressResponseModel
{
    [JsonPropertyName("suggestions")]
    public IEnumerable<SuggestionResponseModel> Suggestions { get; set; }
}
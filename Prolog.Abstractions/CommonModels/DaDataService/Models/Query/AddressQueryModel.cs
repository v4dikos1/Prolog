using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.DaDataService.Models.Query;

public class AddressQueryModel
{
    [JsonPropertyName("locations")]
    public IEnumerable<QueryLocationModel> Locations { get; set; }

    [JsonPropertyName("from_bound")]
    public QueryBoundModel FromBound { get; set; }

    [JsonPropertyName("to_bound")]
    public QueryBoundModel ToBound { get; set; }

    [JsonPropertyName("query")]
    public string Query { get; set; }

    [JsonPropertyName("restrict_value")]
    public bool? RestrictValue { get; set; }
}
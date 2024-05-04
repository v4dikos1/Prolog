using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.DaDataService.Models.Query;

public class QueryLocationModel
{
    [JsonPropertyName("region_fias_id")]
    public string RegionFiasId { get; set; }

    [JsonPropertyName("area_fias_id")]
    public string AreaFiasId { get; set; }

    [JsonPropertyName("city_fias_id")]
    public string CityFiasId { get; set; }

    [JsonPropertyName("settlement_fias_id")]
    public string SettlementFiasId { get; set; }

    [JsonPropertyName("street_fias_id")]
    public string StreetFiasId { get; set; }
}
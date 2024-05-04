using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.DaDataService.Models.Response;

public class DataResponseModel
{
    [JsonPropertyName("region_fias_id")]
    public string RegionFiasId { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("area_fias_id")]
    public string AreaFiasId { get; set; }

    [JsonPropertyName("area")]
    public string Area { get; set; }

    [JsonPropertyName("city_fias_id")]
    public string CityFiasId { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("settlement_fias_id")]
    public string SettlementFiasId { get; set; }

    [JsonPropertyName("settlement")]
    public string Settlement { get; set; }

    [JsonPropertyName("street_fias_id")]
    public string StreetFiasId { get; set; }

    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("house_fias_id")]
    public string HouseFiasId { get; set; }

    [JsonPropertyName("house")]
    public string House { get; set; }

    [JsonPropertyName("kladr_id")]
    public string KladrId { get; set; }
}
using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

/// <summary>
/// Модель транспортного средства
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Профиль транспорта
    /// </summary>
    [JsonPropertyName("routing_profile")]
    public string RoutingProfile { get; set; } = "mapbox/driving";

    /// <summary>
    /// Характеристики
    /// </summary>
    [JsonPropertyName("capacities")]
    public required CapacityModel Capacities { get; set; }

    /// <summary>
    /// Дата старта
    /// </summary>
    [JsonPropertyName("earliest_start")]
    public required string EarliestStart { get; set; }

    /// <summary>
    /// Дата окончания
    /// </summary>
    [JsonPropertyName("latest_end")]
    public required string LatestEnd { get; set; }
}
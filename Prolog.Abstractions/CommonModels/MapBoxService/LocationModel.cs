using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

/// <summary>
/// Модель локации
/// </summary>
public class LocationModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Координаты
    /// </summary>
    [JsonPropertyName("coordinates")]
    public required IEnumerable<double> Coordinates { get; set; }
}
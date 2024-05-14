using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

/// <summary>
/// Модель доставки
/// </summary>
public class ShipmentModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Склад
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; set; }

    /// <summary>
    /// Точка доставки
    /// </summary>
    [JsonPropertyName("to")]
    public required string To { get; set; }

    /// <summary>
    /// Размер
    /// </summary>
    [JsonPropertyName("size")]
    public required ShipmentSizeModel Size { get; set; }
}
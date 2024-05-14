using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

/// <summary>
/// Характеристики ТС
/// </summary>
public class CapacityModel
{
    /// <summary>
    /// Объем
    /// </summary>
    [JsonPropertyName("volume")]
    public required long Volume { get; set; }

    /// <summary>
    /// Вес
    /// </summary>
    [JsonPropertyName("weight")]
    public required long Weight { get; set; }

    /// <summary>
    /// Упаковки
    /// </summary>
    [JsonPropertyName("boxes")]
    public required long Boxes { get; set; }
}
using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

/// <summary>
/// Ответ на запрос на получение решения проблемы оптимизации
/// </summary>
public class SubmitProblemResponse
{
    /// <summary>
    /// Статус
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    /// <summary>
    /// Идентификатор решения
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }
}
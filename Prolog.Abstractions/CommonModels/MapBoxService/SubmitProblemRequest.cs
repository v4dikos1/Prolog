using Prolog.Core.Http.Features.HttpClients;
using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

/// <summary>
/// Запрос на поиск решения проблемы оптимизации
/// </summary>
public class SubmitProblemRequest: IHttpRequest<SubmitProblemResponse>
{
    /// <summary>
    /// Версия API
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;

    /// <summary>
    /// Список точек выдачи и складов
    /// </summary>
    [JsonPropertyName("locations")]
    public required List<LocationModel> Locations { get; set; }

    /// <summary>
    /// Транспортные средства
    /// </summary>
    [JsonPropertyName("vehicles")]
    public required List<VehicleModel> Vehicles { get; set; }

    /// <summary>
    /// Доставки
    /// </summary>
    [JsonPropertyName("shipments")]
    public required List<ShipmentModel> Shipments { get; set; }
}
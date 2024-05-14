using Prolog.Domain.Enums;

namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель маршрута
/// </summary>
public class RouteViewModel
{
    /// <summary>
    /// Порядковый номер в маршруте
    /// </summary>
    public required int Index { get; set; }

    /// <summary>
    /// Идентификатор точки
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Долгота 
    /// </summary>
    public string Longitude { get; set; } = string.Empty;

    /// <summary>
    /// Широта
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    /// <summary>
    /// Тип точки (0 - склад, 1 - клиент)
    /// </summary>
    public StopTypeEnum StopType { get; set; }
}
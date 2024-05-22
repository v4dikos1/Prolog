namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель связки Водитель-Транспорт
/// </summary>
public class DriverTransportBindModel
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required Guid DriverId { get; set; }

    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    public required Guid TransportId { get; set; }

    /// <summary>
    /// Идентификатор склада
    /// </summary>
    public required Guid StorageId { get; set; }
}
namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель водителя в списке заказов
/// </summary>
public class OrderDriverViewModel
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required Guid DriverId { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Номерной знак
    /// </summary>
    public required string LicencePlate { get; set; }

    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    public required Guid TransportId { get; set; }

    /// <summary>
    /// Дата начала
    /// </summary>
    public required DateTimeOffset StartDate { get; set; }

    /// <summary>
    /// Дата завершения
    /// </summary>
    public required DateTimeOffset EndDate { get; set; }

    /// <summary>
    /// Всего заказов
    /// </summary>
    public required int TotalOrdersCount { get; set; }

    /// <summary>
    /// Заказов выполнено
    /// </summary>
    public required int OrdersCompletedCount { get; set; }

    /// <summary>
    /// Расстояние в км
    /// </summary>
    public required decimal Distance { get; set; }
}
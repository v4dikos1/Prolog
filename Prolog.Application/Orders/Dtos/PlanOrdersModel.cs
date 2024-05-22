namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель планирования заявки
/// </summary>
public class PlanOrdersModel
{
    /// <summary>
    /// Дата начала доставки
    /// </summary>
    public required DateTimeOffset StartDate { get; set; }

    /// <summary>
    /// Дата окончания доставки
    /// </summary>
    public required DateTimeOffset EndDate { get; set; }

    /// <summary>
    /// Связки Водитель-Транспорт
    /// </summary>
    public required IEnumerable<DriverTransportBindModel> Binds { get; set; }

    public IEnumerable<long>? OrderIds { get; set; }
}
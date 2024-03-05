namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель заявки в списке, сгруппированном по водителю
/// </summary>
public class OrderListGroupedByDriverViewModel
{
    /// <summary>
    /// Водитель
    /// </summary>
    public OrderDriverViewModel? Driver { get; set; }

    /// <summary>
    /// Заявки
    /// </summary>
    public required IEnumerable<OrderListViewModel> Orders { get; set; }
}
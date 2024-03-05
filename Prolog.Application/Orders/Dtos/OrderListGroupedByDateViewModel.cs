namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель списка заявок
/// </summary>
public class OrderListGroupedByDateViewModel
{
    /// <summary>
    /// Дата
    /// </summary>
    public required DateTime OrderDate { get; set; }

    /// <summary>
    /// Число заявок
    /// </summary>
    public required int OrderCount { get; set; }

    /// <summary>
    /// Список заявок, сгруппированных по водителю
    /// </summary>
    public required IEnumerable<OrderListGroupedByDriverViewModel> OrdersGroupedByDriver { get; set; }

    //public required IEnumerable<OrderListViewModel> Orders { get; set; } 
}
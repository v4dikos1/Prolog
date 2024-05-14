namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель добавления заказа
/// </summary>
public class CreateOrderModel
{
    /// <summary>
    /// Идентификатор склада
    /// </summary>
    public required Guid StorageId { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Дата забора "c" (Формат: dd-mm-yyyyThh:ss+(-)hh:mm. Например, 05-03-2024T12:00+07:00)
    /// </summary>
    public required DateTimeOffset PickUpDateFrom { get; set; }
    
    /// <summary>
    /// Дата забора "до" (Формат: dd-mm-yyyyThh:ss+(-)hh:mm. Например, 05-03-2024T12:00+07:00)
    /// </summary>
    public required DateTimeOffset PickUpDateTo { get; set; }

    /// <summary>
    /// Дата доставки "с" (Формат: dd-mm-yyyyThh:ss+(-)hh:mm. Например, 05-03-2024T12:00+07:00)
    /// </summary>
    public required DateTimeOffset DeliveryDateFrom { get; set; }
    
    /// <summary>
    /// Дата доставки "до" (Формат: dd-mm-yyyyThh:ss+(-)hh:mm. Например, 05-03-2024T12:00+07:00)
    /// </summary>
    public required DateTimeOffset DeliveryDateTo { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required Guid CustomerId { get; set; }

    /// <summary>
    /// Стоимость доставки в рублях
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Идентификаторы товаров
    /// </summary>
    public required IEnumerable<ProductInOrderModel> Products { get; set; }
}
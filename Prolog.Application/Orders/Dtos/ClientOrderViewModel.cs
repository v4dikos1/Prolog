namespace Prolog.Application.Orders.Dtos;

public class ClientOrderViewModel
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required Guid ClientId { get; set; }

    /// <summary>
    /// Наименование клиента
    /// </summary>
    public required string ClientName { get; set; }

    /// <summary>
    /// Контактный телефон клиента
    /// </summary>
    public required string ClientPhone { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Координаты доставки
    /// </summary>
    public required string Coordinates { get; set; }
}
using Prolog.Domain.Enums;

namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель заявки в списке
/// </summary>
public class OrderListViewModel
{
    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    public required long Id { get; set; }
    
    /// <summary>
    /// Видимый идентификатор
    /// </summary>
    public required string VisibleId { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Идентификатор склада
    /// </summary>
    public required Guid StorageId { get; set; }

    /// <summary>
    /// Наименование склада
    /// </summary>
    public required string StorageName { get; set; }

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
    /// Стоимость доставки
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Объем
    /// </summary>
    public required decimal Volume { get; set; }

    /// <summary>
    /// Вес
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public required int Amount { get; set; }

    /// <summary>
    /// Дата забора (левый конец)
    /// </summary>
    public required DateTimeOffset PickUpStartDate { get; set; }

    /// <summary>
    /// Дата забора (правый конец)
    /// </summary>
    public required DateTimeOffset PickUpEndDate { get; set; }

    /// <summary>
    /// Дата забора (итоговая)
    /// </summary>
    public DateTimeOffset? DatePickedUp { get; set; }

    /// <summary>
    /// Дата доставки (левый конец)
    /// </summary>
    public DateTimeOffset? DeliveryStartDate { get; set; }

    /// <summary>
    /// Дата доставки (правый конец)
    /// </summary>
    public DateTimeOffset? DeliveryEndDate { get; set; }

    /// <summary>
    /// Дата доставки (итоговая)
    /// </summary>
    public DateTimeOffset? DateDelivered { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public OrderStatusEnum? Status { get; set; }
}
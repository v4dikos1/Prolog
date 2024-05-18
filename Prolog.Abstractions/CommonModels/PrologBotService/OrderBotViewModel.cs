namespace Prolog.Abstractions.CommonModels.PrologBotService;

public class OrderBotViewModel
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
    /// Наименование склада
    /// </summary>
    public required string StorageName { get; set; }
    public required string StorageAddress{ get; set; }

    /// <summary>
    /// Дата забора (левый конец)
    /// </summary>
    public required DateTimeOffset PickUpStartDate { get; set; }

    /// <summary>
    /// Дата забора (правый конец)
    /// </summary>
    public required DateTimeOffset PickUpEndDate { get; set; }

    /// <summary>
    /// Дата доставки (левый конец)
    /// </summary>
    public DateTimeOffset? DeliveryStartDate { get; set; }

    /// <summary>
    /// Дата доставки (правый конец)
    /// </summary>
    public DateTimeOffset? DeliveryEndDate { get; set; }
}
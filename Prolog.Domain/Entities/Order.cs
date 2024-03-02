using Prolog.Domain.Abstractions;
using Prolog.Domain.Enums;

namespace Prolog.Domain.Entities;

/// <summary>
/// Заявка
/// </summary>
public class Order: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
{
    /// <summary>
    /// Идентификатор внешней системы
    /// </summary>
    public required Guid ExternalSystemId { get; set; }

    /// <summary>
    /// Внешняя система
    /// </summary>
    public ExternalSystem ExternalSystem { get; set; } = null!;

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required Guid CustomerId { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public Customer Customer { get; set; } = null!;

    /// <summary>
    /// Идентификатор доставщика
    /// </summary>
    public Guid? DriverId { get; set; }

    /// <summary>
    /// Доставщик
    /// </summary>
    public Driver? Driver { get; set; }

    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    public Guid? TransportId { get; set; }

    /// <summary>
    /// Транспорт
    /// </summary>
    public Transport? Transport { get; set; }

    /// <summary>
    /// Тип
    /// </summary>
    public OrderTypeEnum Type { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Дата доставки с
    /// </summary>
    public required DateTimeOffset StartDate { get; set; }

    /// <summary>
    /// Дата доставки по
    /// </summary>
    public required DateTimeOffset EndDate { get; set; }

    /// <summary>
    /// Тип оплаты
    /// </summary>
    public PaymentTypeEnum PaymentType { get; set; }

    /// <summary>
    /// Цена доставки
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Скидка
    /// </summary>
    public int Discount { get; set; }
    
    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
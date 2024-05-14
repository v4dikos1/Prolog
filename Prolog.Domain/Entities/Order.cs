using Prolog.Domain.Abstractions;
using Prolog.Domain.Enums;

namespace Prolog.Domain.Entities;

/// <summary>
/// Заявка
/// </summary>
public class Order: BaseEntity<long>, IHasArchiveAttribute, IHasTrackDateAttribute
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
    /// Идентификатор склада
    /// </summary>
    public required Guid StorageId { get; set; }

    /// <summary>
    /// Склад
    /// </summary>
    public Storage Storage { get; set; } = null!;

    /// <summary>
    /// Состав заказа
    /// </summary>
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required Guid CustomerId { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public Customer Customer { get; set; } = null!;

    public Guid? DriverTransportBindId { get; set; }
    public DriverTransportBind? DriverTransportBind { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public Address Address { get; set; } = null!;

    /// <summary>
    /// Координаты доставки
    /// </summary>
    public required string Coordinates { get; set; }

    /// <summary>
    /// Дата доставки с
    /// </summary>
    public required DateTimeOffset DeliveryDateFrom { get; set; }

    /// <summary>
    /// Дата доставки по
    /// </summary>
    public required DateTimeOffset DeliveryDateTo { get; set; }

    /// <summary>
    /// Дата забора с
    /// </summary>
    public required DateTimeOffset PickUpDateFrom { get; set; }

    /// <summary>
    /// Дата забора по
    /// </summary>
    public required DateTimeOffset PickUpDateTo { get; set; }

    /// <summary>
    /// Цена доставки
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Статус заявки
    /// </summary>
    public OrderStatusEnum OrderStatus { get; set; }

    /// <summary>
    /// Дата заврешения заявки
    /// </summary>
    public DateTimeOffset? DateDelivered { get; set; }

    /// <summary>
    /// Идентификатор проблемы оптимизации
    /// </summary>
    public Guid? ProblemId { get; set; }

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
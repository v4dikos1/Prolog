using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

public class DriverTransportBind: BaseEntity<Guid>, IHasTrackDateAttribute
{
    public Guid TransportId { get; set; }
    public Transport Transport { get; set; } = null!;

    public Guid DriverId { get; set; }
    public Driver Driver { get; set; } = null!;

    public Guid StorageId { get; set; }
    public Storage Storage { get; set; } = null!;

    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    /// <summary>
    /// Всего заказов
    /// </summary>
    public int TotalOrdersCount { get; set; }

    /// <summary>
    /// Заказов выполнено
    /// </summary>
    public int OrdersCompletedCount { get; set; }

    /// <summary>
    /// Расстояние в км
    /// </summary>
    public decimal Distance { get; set; }

    public Guid ProblemId { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
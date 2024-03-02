using Prolog.Domain.Abstractions;
using Prolog.Domain.Enums;

namespace Prolog.Domain.Entities;

/// <summary>
/// Транспорт
/// </summary>
public class Transport: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
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
    /// Тип
    /// </summary>
    public TransportTypeEnum Type { get; set; }

    /// <summary>
    /// Объем
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Грузоподъемность
    /// </summary>
    public decimal Capacity { get; set; }

    /// <summary>
    /// Расход толплива на 1 км
    /// </summary>
    public decimal FuelConsumption { get; set; }

    /// <summary>
    /// Номерной знак
    /// </summary>
    public required string LicencePlate { get; set; }

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
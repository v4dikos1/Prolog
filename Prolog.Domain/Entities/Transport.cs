using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

/// <summary>
/// Транспорт
/// </summary>
public class Transport: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
{
    /// <summary>
    /// Идентификатор внешней системы
    /// </summary>
    public Guid ExternalSystemId { get; set; }

    /// <summary>
    /// Внешняя система
    /// </summary>
    public ExternalSystem ExternalSystem { get; set; } = null!;

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
    public string LicencePlate { get; set; } = string.Empty;

    /// <summary>
    /// Марка
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
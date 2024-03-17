using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

/// <summary>
/// Складское помещение
/// </summary>
public class Storage: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
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
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Адрес
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Координаты
    /// </summary>
    public string Coordinates { get; set; } = string.Empty;

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
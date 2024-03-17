using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

/// <summary>
/// Товар
/// </summary>
public class Product: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
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
    /// Код в системе партнера
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Вес
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Объем
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Цена (в копейках)
    /// </summary>
    public long Price { get; set; }

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
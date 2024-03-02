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
    public required Guid ExternalSystemId { get; set; }

    /// <summary>
    /// Внешняя система
    /// </summary>
    public ExternalSystem ExternalSystem { get; set; } = null!;

    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вес
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Цена (в копейках)
    /// </summary>
    public required long Price { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
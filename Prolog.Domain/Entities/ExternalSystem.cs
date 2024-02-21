using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

/// <summary>
/// Внешняя система
/// </summary>
public class ExternalSystem: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
{
    /// <summary>
    /// Идентификатор в сервисе идентификации
    /// </summary>
    public Guid IdentityId { get; set; }

    /// <summary>
    /// Наименование системы
    /// </summary>
    public required string Name { get; set; }

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
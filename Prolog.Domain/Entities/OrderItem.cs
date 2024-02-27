using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

/// <summary>
/// Элемент заявки
/// </summary>
public class OrderItem: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
{
    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    public required Guid OrderId { get; set; }

    /// <summary>
    /// Заявка
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Идентификатор конкретного товара со склада
    /// </summary>
    public required Guid ProductItemId { get; set; }

    /// <summary>
    /// Конкретный товар со склада
    /// </summary>
    public ProductItem ProductItem { get; set; } = null!;

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

/// <summary>
/// Конкретный товар со склада
/// </summary>
public class ProductItem: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public required Guid ProductId { get; set; }

    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Идентификатор склада
    /// </summary>
    public required Guid StorageId { get; set; }

    /// <summary>
    /// Идентификатор элемента заявки
    /// </summary>
    public Guid? OrderItemId { get; set; }

    /// <summary>
    /// Элемент заявки
    /// </summary>
    public OrderItem? OrderItem { get; set; }

    /// <summary>
    /// Склад
    /// </summary>
    public Storage Storage { get; set; } = null!;

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
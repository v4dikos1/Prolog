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
    public long OrderId { get; set; }

    /// <summary>
    /// Заявка
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public required Guid ProductId { get; set; }

    /// <summary>
    /// Товар
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Вес товара
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Объем товара
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Цена (в копейках)
    /// </summary>
    public long Price { get; set; }

    /// <summary>
    /// Количество товара
    /// </summary>
    public long Count { get; set; }

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
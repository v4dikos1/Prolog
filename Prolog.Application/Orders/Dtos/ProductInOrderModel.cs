namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель товара в заявке
/// </summary>
public class ProductInOrderModel
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public required Guid ProductId { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public long Count { get; set; } = 1;
}
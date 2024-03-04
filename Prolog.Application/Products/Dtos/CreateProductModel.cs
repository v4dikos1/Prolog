namespace Prolog.Application.Products.Dtos;

/// <summary>
/// Модель добавления товара
/// </summary>
public class CreateProductModel: UpdateProductModel
{
    /// <summary>
    /// Код товара
    /// </summary>
    public required string Code { get; set; }
}
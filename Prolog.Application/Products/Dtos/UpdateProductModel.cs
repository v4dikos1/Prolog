namespace Prolog.Application.Products.Dtos;

/// <summary>
/// Модель обновления товара
/// </summary>
public class UpdateProductModel
{
    /// <summary>
    /// Стоимость в рублях
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Вес в кг
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Объем в метрах кубических
    /// </summary>
    public required decimal Volume { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }
}
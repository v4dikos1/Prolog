namespace Prolog.Application.Products.Dtos;

/// <summary>
/// Модель товара в таблице
/// </summary>
public class ProductListViewModel
{
    /// <summary>
    /// Внутренний идентификатор
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Код
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вес
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Объем
    /// </summary>
    public required decimal Volume { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    public required decimal Price { get; set;}
}
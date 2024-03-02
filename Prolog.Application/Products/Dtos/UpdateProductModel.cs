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
    /// Вес
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Объем
    /// </summary>
    public required decimal Volume { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    // TODO: уточнить
    /// <summary>
    /// Идентификатор спец. транспорта
    /// </summary>
    public long? SpecialTransportId { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
}
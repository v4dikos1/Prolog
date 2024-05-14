namespace Prolog.Application.Orders.Dtos;

/// <summary>
/// Модель склада в заявке
/// </summary>
public class StorageOrderViewModel
{
    /// <summary>
    /// Идентификатор склада
    /// </summary>
    public required Guid StorageId { get; set; }

    /// <summary>
    /// Координаты склада
    /// </summary>
    public required string StorageCoordinates { get; set; }

    /// <summary>
    /// Наименование склада
    /// </summary>
    public required string StorageName { get; set; }
}
namespace Prolog.Application.Storages.Dtos;

/// <summary>
/// Модель склада для отображения в таблице
/// </summary>
public class StorageListViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Является ли основным
    /// </summary>
    public bool IsPrimary { get; set; }
}
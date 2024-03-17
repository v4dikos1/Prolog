namespace Prolog.Application.Storages.Dtos;

/// <summary>
/// Модель добавления/обновления склада
/// </summary>
public class CreateStorageModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }
}
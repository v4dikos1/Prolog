namespace Prolog.Application.Clients.Dtos;

/// <summary>
/// Модель добавления клиента
/// </summary>
public class CreateCustomerModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Почта
    /// </summary>
    public required string Email { get; set; }
}
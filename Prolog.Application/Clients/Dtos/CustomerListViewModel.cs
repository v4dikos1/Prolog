namespace Prolog.Application.Clients.Dtos;

/// <summary>
/// Модель клиента
/// </summary>
public class CustomerListViewModel
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required Guid Id { get; set; }

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
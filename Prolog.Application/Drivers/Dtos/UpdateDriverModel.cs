namespace Prolog.Application.Drivers.Dtos;

/// <summary>
/// Модель обновления водителя
/// </summary>
public class UpdateDriverModel
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
    /// Телеграм
    /// </summary>
    public required string Telegram { get; set; }

    /// <summary>
    /// Зарплата (рубли / час)
    /// </summary>
    public required long Salary { get; set; }
}
namespace Prolog.Application.Drivers.Dtos;

/// <summary>
/// Модель водителя в таблице
/// </summary>
public class DriverListViewModel
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
    /// Фамилия
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public required string Patronymic { get; set; }

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
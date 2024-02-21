namespace Prolog.Domain.Entities;

/// <summary>
/// Водитель
/// </summary>
public class Driver: BaseEntity<Guid>
{
    /// <summary>
    /// Идентификатор внешней системы
    /// </summary>
    public required Guid ExternalSystemId { get; set; }

    /// <summary>
    /// Внешняя система
    /// </summary>
    public ExternalSystem ExternalSystem { get; set; } = null!;

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
    /// Почта
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public DriverStatus Status { get; set; }
}
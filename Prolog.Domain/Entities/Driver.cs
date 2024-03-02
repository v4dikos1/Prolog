using Prolog.Domain.Abstractions;
using Prolog.Domain.Enums;

namespace Prolog.Domain.Entities;

/// <summary>
/// Доставщик
/// </summary>
public class Driver: BaseEntity<Guid>, IHasArchiveAttribute, IHasTrackDateAttribute
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
    /// Тип управляемых автомобилей
    /// </summary>
    public DriverTypeEnum Type { get; set; }

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
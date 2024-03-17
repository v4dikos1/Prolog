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
    public Guid ExternalSystemId { get; set; }

    /// <summary>
    /// Внешняя система
    /// </summary>
    public ExternalSystem ExternalSystem { get; set; } = null!;

    /// <summary>
    /// Ставка в час
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; } = string.Empty;

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Телеграм
    /// </summary>
    public string Telegram { get; set; } = string.Empty;

    public bool IsArchive { get; set; }
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
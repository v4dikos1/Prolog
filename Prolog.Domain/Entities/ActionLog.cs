using Prolog.Domain.Abstractions;

namespace Prolog.Domain.Entities;

/// <summary>
/// Логи действий пользователей
/// </summary>
public class ActionLog : BaseEntity<Guid>, IHasTrackDateAttribute
{
    /// <summary>
    /// Идентификатор логгируемой сущности
    /// </summary>
    public Guid? EntityId { get; set; }

    // <summary>
    // Идентификатор пользователя в сервисе идентификации
    // </summary>
    public string? IdentityUserId { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string UserSurname { get; set; } = string.Empty;

    /// <summary>
    /// Наименование метода/действия
    /// </summary>
    public string ActionName { get; set; } = string.Empty;

    /// <summary>
    /// Описание действия
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Информация о запросе
    /// </summary>
    public string RequestInfo { get; set; } = string.Empty;

    /// <summary>
    /// Дата действия
    /// </summary>
    public DateTimeOffset ActionDateTime { get; set; }

    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}
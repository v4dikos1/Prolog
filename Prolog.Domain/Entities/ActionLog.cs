using Prolog.Domain.Abstractions;
using System.Text.Json;

namespace Prolog.Domain.Entities;

/// <summary>
/// Логи действий пользователей
/// </summary>
public class ActionLog : BaseEntity<Guid>, IHasTrackDateAttribute, IDisposable
{
    /// <summary>
    /// Данные для фильтра
    /// </summary>
    public JsonDocument Filter { get; set; } = null!;

    // <summary>
    // Идентификатор пользователя в сервисе идентификации
    // </summary>
    public string? IdentityUserId { get; init; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; init; } = string.Empty;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string UserSurname { get; init; } = string.Empty;

    /// <summary>
    /// Наименование метода/действия
    /// </summary>
    public string ActionName { get; init; } = string.Empty;

    /// <summary>
    /// Описание действия
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Дата действия
    /// </summary>
    public DateTimeOffset ActionDateTime { get; init; }

    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }

    public void Dispose()
    {
        Filter.Dispose();
    }
}
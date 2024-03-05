using System.ComponentModel;

namespace Prolog.Domain.Enums;

/// <summary>
/// Статус фильтрации заявки
/// </summary>
public enum OrderFilterStatusEnum
{
    /// <summary>
    /// Входящие
    /// </summary>
    [Description("Входящие")]
    Incoming = 0,

    /// <summary>
    /// Активные
    /// </summary>
    [Description("Активные")]
    Active = 1,

    /// <summary>
    /// Выполненнные
    /// </summary>
    [Description("Выполененные")]
    Completed = 2,

    /// <summary>
    /// Лююые
    /// </summary>
    [Description("Любые")]
    Any = 3
}
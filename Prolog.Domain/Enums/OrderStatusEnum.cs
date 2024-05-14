using System.ComponentModel;

namespace Prolog.Domain.Enums;

/// <summary>
/// Статус заявки
/// </summary>
public enum OrderStatusEnum
{
    /// <summary>
    /// Входящая
    /// </summary>
    [Description("Входящая")]
    Incoming = 0,

    /// <summary>
    /// Активная
    /// </summary>
    [Description("Активная")]
    Active = 1,

    /// <summary>
    /// Запланированная
    /// </summary>
    [Description("Запланированная")]
    Planned = 4,

    /// <summary>
    /// Завершенная
    /// </summary>
    [Description("Завершенная")]
    Completed = 2,

    /// <summary>
    /// Отменена
    /// </summary>
    [Description("Отменена")]
    Cancelled = 3
}
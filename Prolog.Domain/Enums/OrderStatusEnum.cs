using System.ComponentModel;

namespace Prolog.Domain.Enums;

/// <summary>
/// Статус заявки
/// </summary>
public enum OrderStatusEnum
{
    /// <summary>
    /// Доставлен
    /// </summary>
    [Description("Доставлен")]
    Delivered = 0,

    /// <summary>
    /// Отменен
    /// </summary>
    [Description("Отменен")]
    Cancelled = 1
}
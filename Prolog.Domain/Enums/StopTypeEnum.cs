using System.ComponentModel;

namespace Prolog.Domain.Enums;

/// <summary>
/// Тип точки (остановки)
/// </summary>
public enum StopTypeEnum
{
    /// <summary>
    /// Склад
    /// </summary>
    [Description("Склад")]
    Storage = 0,

    /// <summary>
    /// Клиент
    /// </summary>
    [Description("Клиент")]
    Client = 1
}
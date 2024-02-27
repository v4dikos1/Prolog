using System.ComponentModel;

namespace Prolog.Domain.Enums;

/// <summary>
/// Статус водителя
/// </summary>
public enum DriverTypeEnum
{
    /// <summary>
    /// Грузовой
    /// </summary>
    [Description("Грузовой")]
    Cargo = 0,

    /// <summary>
    /// Легковой
    /// </summary>
    [Description("Легковой")]
    Passenger = 1
}
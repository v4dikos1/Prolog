using System.ComponentModel;

namespace Prolog.Domain.Enums;

/// <summary>
/// Тип транспорта
/// </summary>
public enum TransportTypeEnum
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
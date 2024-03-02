using System.ComponentModel;

namespace Prolog.Domain.Enums;

/// <summary>
/// Тип оплаты
/// </summary>
public enum PaymentTypeEnum
{
    /// <summary>
    /// Оплтата при получении наличными
    /// </summary>
    [Description("Оплтата при получении наличными")]
    PaymentInCash = 0,

    /// <summary>
    /// Оплата при получении безналом
    /// </summary>
    [Description("Оплата при получении безналом")]
    WireTransfer = 1,

    /// <summary>
    /// Оплата онлайн
    /// </summary>
    [Description("Оплата онлайн")]
    Online = 2
}
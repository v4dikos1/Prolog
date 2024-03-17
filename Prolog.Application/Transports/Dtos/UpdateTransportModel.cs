namespace Prolog.Application.Transports.Dtos;

/// <summary>
/// Модель обновления транспорта
/// </summary>
public class UpdateTransportModel
{
    /// <summary>
    /// Марка
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Объем, метры кубические
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Грузоподъемность, кг
    /// </summary>
    public decimal Capacity { get; set; }

    /// <summary>
    /// Расход толплива на 100 км, л
    /// </summary>
    public decimal FuelConsumption { get; set; }

    /// <summary>
    /// Номерной знак
    /// </summary>
    public required string LicencePlate { get; set; }
}
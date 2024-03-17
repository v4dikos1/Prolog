namespace Prolog.Application.Transports.Dtos;

/// <summary>
/// Модель транспорта в таблице
/// </summary>
public class TransportListViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required Guid Id { get; set; }

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
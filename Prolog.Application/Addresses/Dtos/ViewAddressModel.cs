namespace Prolog.Application.Addresses.Dtos;

/// <summary>
///     Модель адреса
/// </summary>
public class ViewAddressModel
{
    /// <summary>
    ///     Полное наименование адреса
    /// </summary>
    public string AddressFullName { get; set; } = null!;

    /// <summary>
    ///     Регион
    /// </summary>
    public string Region { get; set; } = null!;

    /// <summary>
    ///     ФИАС идентификатор региона
    /// </summary>
    public string RegionFiasId { get; set; } = null!;

    /// <summary>
    ///     Район в регионе
    /// </summary>
    public string Area { get; set; } = null!;

    /// <summary>
    ///     ФИАС идентификатор района
    /// </summary>
    public string AreaFiasId { get; set; } = null!;

    /// <summary>
    ///     Город
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    ///     ФИАС идентификатор города
    /// </summary>
    public string CityFiasId { get; set; } = null!;

    /// <summary>
    ///     Населенный пункт
    /// </summary>
    public string Settlement { get; set; } = null!;

    /// <summary>
    ///     ФИАС идентификатор населенного пункта
    /// </summary>
    public string SettlementFiasId { get; set; } = null!;

    /// <summary>
    ///     Улица
    /// </summary>
    public string Street { get; set; } = null!;

    /// <summary>
    ///     ФИАС идентификатор улицы
    /// </summary>
    public string StreetFiasId { get; set; } = null!;

    /// <summary>
    ///     Дом
    /// </summary>
    public string House { get; set; } = null!;

    /// <summary>
    ///     ФИАС идентификатор дома
    /// </summary>
    public string HouseFiasId { get; set; } = null!;

    /// <summary>
    ///     Идентификатор КЛАДР
    /// </summary>
    public string Kladr { get; set; } = null!;

    /// <summary>
    ///     Номер квартиры
    /// </summary>
    public string Apartment { get; set; } = null!; 
}
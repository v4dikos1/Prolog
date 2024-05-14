namespace Prolog.Domain.Entities;

public class Address
{
    public string Region { get; set; } = null!;
    public string RegionFiasId { get; set; } = null!;
    public string Area { get; set; } = null!;
    public string AreaFiasId { get; set; } = null!;
    public string City { get; set; } = null!;
    public string CityFiasId { get; set; } = null!;
    public string Settlement { get; set; } = null!;
    public string SettlementFiasId { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string StreetFiasId { get; set; } = null!;
    public string House { get; set; } = null!;
    public string HouseFiasId { get; set; } = null!;
    public string Kladr { get; set; } = null!;
    public string Apartment { get; set; } = null!;
    public string AddressFullName { get; set; } = null!;
}
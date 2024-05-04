using Prolog.Abstractions.CommonModels.DaDataService.Models.Response;
using Prolog.Application.Addresses.Dtos;
using Prolog.Application.Addresses.Mappers;

namespace Prolog.Application.Addresses.Mappers
{
    public partial class AddressMapper : IAddressMapper
    {
        public Address MapToAddress(SuggestionResponseModel p1)
        {
            return p1 == null ? null : new Address()
            {
                Region = p1.Data == null ? null : p1.Data.Region,
                RegionFiasId = p1.Data == null ? null : p1.Data.RegionFiasId,
                Area = p1.Data == null ? null : p1.Data.Area,
                AreaFiasId = p1.Data == null ? null : p1.Data.AreaFiasId,
                City = p1.Data == null ? null : p1.Data.City,
                CityFiasId = p1.Data == null ? null : p1.Data.CityFiasId,
                Settlement = p1.Data == null ? null : p1.Data.Settlement,
                SettlementFiasId = p1.Data == null ? null : p1.Data.SettlementFiasId,
                Street = p1.Data == null ? null : p1.Data.Street,
                StreetFiasId = p1.Data == null ? null : p1.Data.StreetFiasId,
                House = p1.Data == null ? null : p1.Data.House,
                HouseFiasId = p1.Data == null ? null : p1.Data.HouseFiasId,
                Kladr = p1.Data == null ? null : p1.Data.KladrId,
                AddressFullName = p1.UnrestrictedValue
            };
        }
        public ViewAddressModel MapToViewAddress(Address p2)
        {
            return p2 == null ? null : new ViewAddressModel()
            {
                AddressFullName = p2.AddressFullName,
                Region = p2.Region,
                RegionFiasId = p2.RegionFiasId,
                Area = p2.Area,
                AreaFiasId = p2.AreaFiasId,
                City = p2.City,
                CityFiasId = p2.CityFiasId,
                Settlement = p2.Settlement,
                SettlementFiasId = p2.SettlementFiasId,
                Street = p2.Street,
                StreetFiasId = p2.StreetFiasId,
                House = p2.House,
                HouseFiasId = p2.HouseFiasId,
                Kladr = p2.Kladr,
                Apartment = p2.Apartment
            };
        }
        public Address MapToAddress(ViewAddressModel p3)
        {
            return p3 == null ? null : new Address()
            {
                Region = p3.Region,
                RegionFiasId = p3.RegionFiasId,
                Area = p3.Area,
                AreaFiasId = p3.AreaFiasId,
                City = p3.City,
                CityFiasId = p3.CityFiasId,
                Settlement = p3.Settlement,
                SettlementFiasId = p3.SettlementFiasId,
                Street = p3.Street,
                StreetFiasId = p3.StreetFiasId,
                House = p3.House,
                HouseFiasId = p3.HouseFiasId,
                Kladr = p3.Kladr,
                Apartment = p3.Apartment,
                AddressFullName = p3.AddressFullName
            };
        }
        public ViewAddressModel MapToViewAddress(SuggestionResponseModel p4)
        {
            return p4 == null ? null : new ViewAddressModel()
            {
                AddressFullName = p4.UnrestrictedValue,
                Region = p4.Data == null ? null : p4.Data.Region,
                RegionFiasId = p4.Data == null ? null : p4.Data.RegionFiasId,
                Area = p4.Data == null ? null : p4.Data.Area,
                AreaFiasId = p4.Data == null ? null : p4.Data.AreaFiasId,
                City = p4.Data == null ? null : p4.Data.City,
                CityFiasId = p4.Data == null ? null : p4.Data.CityFiasId,
                Settlement = p4.Data == null ? null : p4.Data.Settlement,
                SettlementFiasId = p4.Data == null ? null : p4.Data.SettlementFiasId,
                Street = p4.Data == null ? null : p4.Data.Street,
                StreetFiasId = p4.Data == null ? null : p4.Data.StreetFiasId,
                House = p4.Data == null ? null : p4.Data.House,
                HouseFiasId = p4.Data == null ? null : p4.Data.HouseFiasId,
                Kladr = p4.Data == null ? null : p4.Data.KladrId
            };
        }
    }
}
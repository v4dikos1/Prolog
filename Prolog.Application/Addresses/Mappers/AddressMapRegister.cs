using Mapster;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Response;
using Prolog.Application.Addresses.Dtos;

namespace Prolog.Application.Addresses.Mappers;

internal class AddressMapRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SuggestionResponseModel, Address>()
            .Map(d => d.Area, src => src.Data.Area)
            .Map(d => d.City, src => src.Data.City)
            .Map(d => d.House, src => src.Data.House)
            .Map(d => d.Kladr, src => src.Data.KladrId)
            .Map(d => d.Region, src => src.Data.Region)
            .Map(d => d.Settlement, src => src.Data.Settlement)
            .Map(d => d.Street, src => src.Data.Street)
            .Map(d => d.AreaFiasId, src => src.Data.AreaFiasId)
            .Map(d => d.CityFiasId, src => src.Data.CityFiasId)
            .Map(d => d.RegionFiasId, src => src.Data.RegionFiasId)
            .Map(d => d.SettlementFiasId, src => src.Data.SettlementFiasId)
            .Map(d => d.StreetFiasId, src => src.Data.StreetFiasId)
            .Map(d => d.AddressFullName, src => src.UnrestrictedValue)
            .Map(d => d.HouseFiasId, src => src.Data.HouseFiasId);

        config.NewConfig<Address, ViewAddressModel>();

        config.NewConfig<ViewAddressModel, Address>();

        config.NewConfig<SuggestionResponseModel, ViewAddressModel>()
            .Map(d => d.Area, src => src.Data.Area)
            .Map(d => d.City, src => src.Data.City)
            .Map(d => d.House, src => src.Data.House)
            .Map(d => d.Kladr, src => src.Data.KladrId)
            .Map(d => d.Region, src => src.Data.Region)
            .Map(d => d.Settlement, src => src.Data.Settlement)
            .Map(d => d.Street, src => src.Data.Street)
            .Map(d => d.AreaFiasId, src => src.Data.AreaFiasId)
            .Map(d => d.CityFiasId, src => src.Data.CityFiasId)
            .Map(d => d.RegionFiasId, src => src.Data.RegionFiasId)
            .Map(d => d.SettlementFiasId, src => src.Data.SettlementFiasId)
            .Map(d => d.StreetFiasId, src => src.Data.StreetFiasId)
            .Map(d => d.AddressFullName, src => src.UnrestrictedValue)
            .Map(d => d.HouseFiasId, src => src.Data.HouseFiasId);
    }
}
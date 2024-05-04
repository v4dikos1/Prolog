using MediatR;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Query;
using Prolog.Abstractions.Services;
using Prolog.Application.Addresses.Dtos;
using Prolog.Application.Addresses.Mappers;
using Prolog.Application.Addresses.Queries;

namespace Prolog.Application.Addresses.Handlers;

public class SearchAddressHandler(IDaDataService daDataService, IAddressMapper addressMapper) :
    IRequestHandler<SearchAddressAreaQuery, IEnumerable<ViewAddressModel>>,
    IRequestHandler<SearchAddressBuildingQuery, IEnumerable<ViewAddressModel>>,
    IRequestHandler<SearchAddressCityQuery, IEnumerable<ViewAddressModel>>,
    IRequestHandler<SearchAddressRegionQuery, IEnumerable<ViewAddressModel>>,
    IRequestHandler<SearchAddressSettlementQuery, IEnumerable<ViewAddressModel>>,
    IRequestHandler<SearchAddressStreetQuery, IEnumerable<ViewAddressModel>>,
    IRequestHandler<SearchAddressByFullNameQuery, IEnumerable<ViewAddressModel>>
{
    public async Task<IEnumerable<ViewAddressModel>> Handle(SearchAddressAreaQuery request,
        CancellationToken cancellationToken)
    {
        var queryModel = new AddressQueryModel
        {
            Locations = new QueryLocationModel[] { new() { RegionFiasId = request.RegionFiasId } },
            FromBound = new QueryBoundModel { Value = "area" },
            ToBound = new QueryBoundModel { Value = "area" },
            Query = request.Query,
            RestrictValue = true
        };
        var suggestions = await daDataService.GetListSuggestionAddressByQuery(queryModel);
        var result = suggestions.Select(addressMapper.MapToViewAddress);

        return result;
    }

    public async Task<IEnumerable<ViewAddressModel>> Handle(SearchAddressBuildingQuery request,
        CancellationToken cancellationToken)
    {
        var queryModel = new AddressQueryModel
        {
            Locations = new QueryLocationModel[] { new() { StreetFiasId = request.StreetFiasId } },
            FromBound = new QueryBoundModel { Value = "house" },
            ToBound = new QueryBoundModel { Value = "house" },
            Query = request.Query,
            RestrictValue = null
        };
        var suggestions = await daDataService.GetListSuggestionAddressByQuery(queryModel);
        var result = suggestions.Select(addressMapper.MapToViewAddress);

        return result;
    }

    public async Task<IEnumerable<ViewAddressModel>> Handle(SearchAddressByFullNameQuery request,
        CancellationToken cancellationToken)
    {
        var queryModel = new AddressQueryModel
        {
            Locations = null,
            FromBound = null,
            ToBound = null,
            Query = request.Query,
            RestrictValue = true
        };
        var suggestions = await daDataService.GetListSuggestionAddressByQuery(queryModel);
        var result = suggestions.Select(addressMapper.MapToViewAddress);

        return result;
    }

    public async Task<IEnumerable<ViewAddressModel>> Handle(SearchAddressCityQuery request,
        CancellationToken cancellationToken)
    {
        var queryModel = new AddressQueryModel
        {
            Locations =
                new QueryLocationModel[]
                {
                    new() { RegionFiasId = request.RegionFiasId, AreaFiasId = request.AreaFiasId }
                },
            FromBound = new QueryBoundModel { Value = "city" },
            ToBound = new QueryBoundModel { Value = "city" },
            Query = request.Query,
            RestrictValue = true
        };
        var suggestions = await daDataService.GetListSuggestionAddressByQuery(queryModel);
        var result = suggestions.Select(addressMapper.MapToViewAddress);

        return result;
    }

    public async Task<IEnumerable<ViewAddressModel>> Handle(SearchAddressRegionQuery request,
        CancellationToken cancellationToken)
    {
        var queryModel = new AddressQueryModel
        {
            Locations = null,
            FromBound = new QueryBoundModel { Value = "region" },
            ToBound = new QueryBoundModel { Value = "region" },
            Query = request.Query,
            RestrictValue = true
        };
        var suggestions = await daDataService.GetListSuggestionAddressByQuery(queryModel);
        var result = suggestions.Select(addressMapper.MapToViewAddress);

        return result;
    }

    public async Task<IEnumerable<ViewAddressModel>> Handle(SearchAddressSettlementQuery request,
        CancellationToken cancellationToken)
    {
        var queryModel = new AddressQueryModel
        {
            Locations =
                new QueryLocationModel[]
                {
                    new() { AreaFiasId = request.AreaFiasId, CityFiasId = request.CityFiasId }
                },
            FromBound = new QueryBoundModel { Value = "settlement" },
            ToBound = new QueryBoundModel { Value = "settlement" },
            Query = request.Query,
            RestrictValue = true
        };
        var suggestions = await daDataService.GetListSuggestionAddressByQuery(queryModel);
        var result = suggestions.Select(addressMapper.MapToViewAddress);

        return result;
    }

    public async Task<IEnumerable<ViewAddressModel>> Handle(SearchAddressStreetQuery request,
        CancellationToken cancellationToken)
    {
        var queryModel = new AddressQueryModel
        {
            Locations = new QueryLocationModel[]
            {
                new() { CityFiasId = request.CityFiasId, SettlementFiasId = request.SettlementFiasId }
            },
            FromBound = new QueryBoundModel { Value = "street" },
            ToBound = new QueryBoundModel { Value = "street" },
            Query = request.Query,
            RestrictValue = true
        };
        var suggestions = await daDataService.GetListSuggestionAddressByQuery(queryModel);
        var result = suggestions.Select(addressMapper.MapToViewAddress);

        return result;
    }
}
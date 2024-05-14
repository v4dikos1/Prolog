using Prolog.Abstractions.CommonModels.DaDataService;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Query;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Response;

namespace Prolog.Abstractions.Services;

public interface IDaDataService
{
    Task<IEnumerable<SuggestionResponseModel>> GetListSuggestionAddressByQuery(AddressQueryModel queryModel);
    Task<SuggestionResponseModel> GetAddressByHouseFiasId(string fiasHouseId);

    Task<CoordinatesResponseModel> GetCoordinatesByAddress(string address);
}
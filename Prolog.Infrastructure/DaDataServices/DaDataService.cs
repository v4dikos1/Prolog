using Microsoft.Extensions.Configuration;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Query;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Response;
using Prolog.Abstractions.Services;
using Prolog.Infrastructure.HttpClients;

namespace Prolog.Infrastructure.DaDataServices;

public class DaDataService(IConfiguration configuration) : IDaDataService
{
    private readonly string _apiKey = configuration.GetSection("DaDataService:ApiKey").Value!;
    private readonly string _daDataServiceUrl = configuration.GetSection("DaDataService:BaseUrl").Value!;

    public async Task<IEnumerable<SuggestionResponseModel>> GetListSuggestionAddressByQuery(
        AddressQueryModel queryModel)
    {
        using var client = new DaDataHttpClient(_apiKey, _daDataServiceUrl);
        var suggestionList = await client.GetAddress(queryModel);
        return suggestionList.Suggestions;
    }

    public async Task<SuggestionResponseModel> GetAddressByHouseFiasId(string fiasHouseId)
    {
        using var client = new DaDataHttpClient(_apiKey, _daDataServiceUrl);
        var suggestionList = await client.GetAddressByHouseFiasId(fiasHouseId);
        return suggestionList.Suggestions.FirstOrDefault()!;
    }
}
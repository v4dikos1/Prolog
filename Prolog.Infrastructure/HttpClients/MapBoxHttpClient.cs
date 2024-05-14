using Ardalis.GuardClauses;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Prolog.Abstractions.CommonModels.MapBoxService;
using Prolog.Core.Http.Features.HttpClients;
using Prolog.Core.Utils;

namespace Prolog.Infrastructure.HttpClients;

internal class MapBoxHttpClient: BaseHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly MapBoxServiceConfiguration _configuration;

    public MapBoxHttpClient(HttpClient httpClient, IOptions<MapBoxServiceConfiguration> configuration) : base(httpClient)
    {
        httpClient.BaseAddress = new Uri(configuration.Value.BaseUrl);
        ServiceName = "MapBox";
        _httpClient = httpClient;
        _configuration = configuration.Value;
    }

    /// <summary>
    ///     GET запрос с query-параметрами.
    ///     Для передачи массива query-параметров использовать: new StringValues(arrayOfParameters)
    /// </summary>
    /// <typeparam name="TResponse">тип ответа</typeparam>
    /// <param name="url">url</param>
    /// <param name="queryParameters">список query-параметров</param>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>TResponse</returns>
    public async Task<TResponse> GetAsync<TResponse>(Uri url, IEnumerable<KeyValuePair<string, StringValues>> queryParameters,
        CancellationToken cancellationToken = default)
    {
        Defend.Against.Null(url, nameof(url));

        await SetAuthorization();
        var response =
            await _httpClient.GetAsync(QueryHelpers.AddQueryString(url.OriginalString, queryParameters),
                cancellationToken);
        return await GetResult<TResponse>(response);
    }

    protected override Task SetAuthorization()
    {
        return Task.CompletedTask;
    }
}
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Prolog.Core.Http.Features.HttpClients;
using Prolog.Core.Http.Features.TokenManagers;
using Prolog.Core.Http.Features.TokenManagers.Models;
using Prolog.Keycloak.Models;
using Prolog.Keycloak.Models.KeycloakApiModels;
using System.Text.Json;

namespace Prolog.Keycloak.Clients;

public class KeycloakHttpClient : BaseHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IAccessTokenManager _tokenManager;
    private new const string ServiceName = "Keycloak";
    private readonly KeycloakConfigurationModel _configuration;
    private readonly ITokenGenerateStrategy<OidcClientCredentialRequest> _credentialStrategy;

    public KeycloakHttpClient(HttpClient httpClient, IOptions<KeycloakConfigurationModel> configuration,
        IAccessTokenManager tokenManager, ITokenGenerateStrategy<OidcClientCredentialRequest> credentialStrategy) : base(httpClient)
    {
        _httpClient = httpClient;
        _configuration = configuration.Value;
        _tokenManager = tokenManager;
        _credentialStrategy = credentialStrategy;
        _httpClient.BaseAddress = new Uri(_configuration.BaseUrl);
    }

    protected override async Task<TResponse> GetResult<TResponse>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        await CheckResult(response, content);
        return JsonSerializer.Deserialize<TResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                     ?? throw new JsonException($"Ошибка десериализации при выполенении запроса {response.RequestMessage?.RequestUri}.");
    }

    protected override async Task CheckResult(HttpResponseMessage response, string? content = null)
    {
        if (!response.IsSuccessStatusCode)
        {
            content ??= await response.Content.ReadAsStringAsync();
            try
            {
                var responseModel = JsonSerializer.Deserialize<KeycloakBadResponseModel>(content);
                throw new ApplicationException(
                    $"Статус ошибки: {response.StatusCode}. Ошибка при обращении к сервису {ServiceName}: " +
                    responseModel?.Message);
            }
            catch (JsonException)
            {
                throw new HttpRequestException(
                    $"Статус ошибки: {response.StatusCode}. Ошибка при обращении к сервису {ServiceName}: " +
                    content);
            }
        }
    }

    protected override async Task SetAuthorization()
    {
        var tokenResponse = await _tokenManager
            .GetAccessToken(_credentialStrategy, new OidcClientCredentialRequest
            {
                ClientId = _configuration.ClientId,
                ClientSecret = _configuration.ClientSecret,
                Scopes = Scopes!,
                TokenEndpointUrl = _configuration.BaseUrl + $"/realms/master/protocol/openid-connect/token"
            });
        _httpClient.SetBearerToken(tokenResponse.AccessToken);
    }
}
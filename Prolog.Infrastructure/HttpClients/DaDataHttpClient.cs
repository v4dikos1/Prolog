﻿using Microsoft.Extensions.Logging;
using Prolog.Abstractions.CommonModels.DaDataService;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Query;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Response;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Prolog.Infrastructure.HttpClients;

public class DaDataHttpClient : IDisposable
{
    private readonly ILogger<DaDataHttpClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly HttpClient _addressHttpClient;

    public DaDataHttpClient(string apiKey, string baseUrl, ILogger<DaDataHttpClient> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey);
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _addressHttpClient = new HttpClient();
        _addressHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiKey);
        _addressHttpClient.DefaultRequestHeaders.Add("X-Secret", "6b4b874d4d8f250cfc5f4f5ae87247b4b5c47d0a");
        _addressHttpClient.BaseAddress = new Uri("https://cleaner.dadata.ru");
        _addressHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    public async Task<AddressResponseModel> GetAddress(AddressQueryModel queryModel)
    {
        var jsonString = JsonSerializer.Serialize(queryModel);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("suggestions/api/4_1/rs/suggest/address", content);
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AddressResponseModel>(result)!;
    }

    public async Task<AddressResponseModel> GetAddressByHouseFiasId(string fiasHouseId)
    {
        var query = new { query = fiasHouseId };
        var jsonString = JsonSerializer.Serialize(query);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("suggestions/api/4_1/rs/findById/fias", content);
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AddressResponseModel>(result)!;
    }

    public async Task<CoordinatesResponseModel> GetCoordinatesByAddress(string address)
    {
        var query = new List<string> { address };
        var jsonString = JsonSerializer.Serialize(query);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await _addressHttpClient.PostAsync("api/v1/clean/address", content);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError(jsonString);
            _logger.LogError(response.Content.ToString());
            throw new ApplicationException($"Ошибка во время запроса в DaData: {response.Content}");
        }

        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<CoordinatesResponseModel>>(result)!.First();
    }
}
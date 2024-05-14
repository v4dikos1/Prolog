using Microsoft.Extensions.Options;
using Prolog.Abstractions.CommonModels.MapBoxService;
using Prolog.Abstractions.Services;
using Prolog.Infrastructure.HttpClients;

namespace Prolog.Infrastructure.DaDataServices;

internal class MapBoxService(MapBoxHttpClient httpClient, IOptions<MapBoxServiceConfiguration> configuration) : IMapBoxService
{
    public async Task<SubmitProblemResponse> SubmitProblemAsync(SubmitProblemRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostWithResultAsync<SubmitProblemResponse>(
            new Uri($"optimized-trips/v2?access_token={configuration.Value.AccessToken}", UriKind.Relative), request,
            cancellationToken);
        return response;
    }

    public async Task<ProblemSolutionResponse> RetrieveSolutionAsync(RetrieveSolutionRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync<ProblemSolutionResponse>(
            new Uri($"optimized-trips/v2/{request.Id}?access_token={configuration.Value.AccessToken}",
                UriKind.Relative),
            cancellationToken);
        return response;
    }
}
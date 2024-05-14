using Prolog.Core.Http.Features.HttpClients;
using System.Text.Json.Serialization;

namespace Prolog.Abstractions.CommonModels.MapBoxService;

public class RetrieveSolutionRequest: IHttpRequest<ProblemSolutionResponse>
{
    /// <summary>
    /// Идентификатор решения
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }
}
using Prolog.Abstractions.CommonModels.MapBoxService;

namespace Prolog.Abstractions.Services;

public interface IMapBoxService
{
    /// <summary>
    /// Решить проблему оптимизации
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор решения</returns>
    Task<SubmitProblemResponse> SubmitProblemAsync(SubmitProblemRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получить решение проблемы оптимизации
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Решение проблемы оптимизации</returns>
    Task<ProblemSolutionResponse> RetrieveSolutionAsync(RetrieveSolutionRequest request,
        CancellationToken cancellationToken);
}
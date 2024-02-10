using Prolog.Core.Http.Features.TokenManagers.Models;

namespace Prolog.Core.Http.Features.TokenManagers;

/// <summary>
///     Интерфейс для описания способа получения токена доступа
/// </summary>
/// <typeparam name="TRequest">Тип запроса</typeparam>
public interface ITokenGenerateStrategy<in TRequest> where TRequest : BaseAccessTokenGenerateRequest
{
    Task<TokenResponseModel> GenerateTokenAsync(TRequest requestModel, CancellationToken cancellationToken);
}
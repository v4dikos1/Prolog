using Prolog.Domain.Entities;

namespace Prolog.Abstractions.Services;

public interface IExternalSystemService
{
    /// <summary>
    /// Получение пользователя или внешней системы по идентификатору в сервисе идентификации
    /// </summary>
    /// <param name="externalSystemId">Идентификатор в сервисе идентификации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Внешняя система</returns>
    Task<ExternalSystem> GetExternalSystemWithCheckExistsAsync(Guid externalSystemId, CancellationToken cancellationToken);
}
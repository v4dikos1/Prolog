using Microsoft.Extensions.DependencyInjection;
using Prolog.Core.Http.Features.TokenManagers;

namespace Prolog.Core.Http.Extensions;

public static class AccessTokenManagerExtensions
{
    /// <summary>
    ///     Регистрация в сервисах менеджера jwt-токенов (singleton)
    /// </summary>
    public static IServiceCollection RegisterJwtTokenManagerService(this IServiceCollection services, long expirationShiftInSeconds)
    {
        services.AddSingleton<IAccessTokenManager, AccessTokenManager>(x => new AccessTokenManager(expirationShiftInSeconds));
        return services;
    }
}
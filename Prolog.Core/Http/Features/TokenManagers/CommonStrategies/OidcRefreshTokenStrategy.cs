﻿using Ardalis.GuardClauses;
using IdentityModel.Client;
using Prolog.Core.Http.Features.TokenManagers.Models;
using Prolog.Core.Utils;

namespace Prolog.Core.Http.Features.TokenManagers.CommonStrategies;

public class OidcRefreshTokenStrategy(HttpClient httpClient) : ITokenGenerateStrategy<OidcRefreshTokenRequest>
{
    public async Task<TokenResponseModel> GenerateTokenAsync(OidcRefreshTokenRequest requestModel,
        CancellationToken cancellationToken)
    {
        Defend.Against.NullOrEmpty(requestModel.ClientId, nameof(requestModel.ClientId));
        Defend.Against.NullOrEmpty(requestModel.ClientSecret, nameof(requestModel.ClientSecret));
        Defend.Against.NullOrEmpty(requestModel.RefreshToken, nameof(requestModel.RefreshToken));
        ValidateScopes(requestModel.Scopes);
        var request = new RefreshTokenRequest()
        {
            RefreshToken = requestModel.RefreshToken,
            Address = requestModel.TokenEndpointUrl,
            ClientId = requestModel.ClientId,
            ClientSecret = requestModel.ClientSecret,
            Scope = requestModel.Scopes,
            GrantType = "refresh_token"
        };

        var tokenResponse = await httpClient.RequestRefreshTokenAsync(request, cancellationToken);

        if (tokenResponse.IsError || string.IsNullOrEmpty(tokenResponse.AccessToken))
        {
            throw new ApplicationException("Сервер идентификации в данный момент не доступен!",
                new Exception("Ошибка авторизации! " + tokenResponse.Error));
        }

        return new TokenResponseModel
        {
            AccessToken = tokenResponse.AccessToken,
            ExpireTime = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
            RefreshToken = tokenResponse.RefreshToken,
            Scopes = requestModel.Scopes
        };
    }

    private void ValidateScopes(string? scopes)
    {
        if (string.IsNullOrEmpty(scopes))
        {
            return;
        }

        if (!scopes.All(c => char.IsLetterOrDigit(c) || c == '.' || c == ' '))
        {
            throw new ArgumentException("Параметр \"scopes\" имеет неверный формат! ");
        }
    }
}
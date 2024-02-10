﻿using System.Text.Json.Serialization;

namespace Prolog.Keycloak.Models.KeycloakApiModels;

public class UserRepresentationModel : CreateKeyckloakUserModel
{
    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;
}
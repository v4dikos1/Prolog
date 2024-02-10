﻿using System.Text.Json.Serialization;

namespace Prolog.Keycloak.Models.KeycloakApiModels;

public class KeycloakClientSecretModel
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}
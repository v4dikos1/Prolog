﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prolog.Core.EntityFramework.Features.Encryption.Public.Abstractions;

namespace Prolog.Core.EntityFramework.Features.Encryption.Internal;

/// <summary>
/// Object for encrypted property.
/// </summary>
internal class EncryptedProperty
{
    /// <summary>
    /// Property builder.
    /// </summary>
    public PropertyBuilder<string> PropertyBuilder { get; set; } = default!;

    /// <summary>
    /// Crypto converter.
    /// </summary>
    public ICryptoConverter CryptoConverter { get; set; } = default!;

    /// <summary>
    /// Max property original value length (before encryption).
    /// </summary>
    public int? MaxLength { get; set; }

    /// <summary>
    /// Id of migration that property started to be encrypted from.
    /// </summary>
    public string? MigrationId { get; set; }
}

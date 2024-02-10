using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Prolog.Core.EntityFramework.Features.Encryption.Internal;
using Prolog.Core.EntityFramework.Features.Encryption.Public.Abstractions;
using Prolog.Core.Utils;

namespace Prolog.Core.EntityFramework.Features.Encryption.Public.Extensions;

/// <summary>
/// Extensions for <see cref="PropertyBuilder"/>.
/// </summary>
public static class PropertyBuilderExtensions
{
    /// <summary>
    /// Apply encryption to the property.
    /// ATTENTION! If there are already some data in DB that must be encrypted then you must provide Migration to encrypt existing data.
    /// !!! In this case ensure that you call <see cref="DatabaseFacadeExtensions.MigrateWithEncryptingMigratorAsync"/> or <see cref="propertyBuilder"/> on the database context.
    /// </summary>
    /// <param name="cryptoConverter">Property builder.</param>
    /// <param name="migrationType">Max possible length of the original property value. It will allow to count max encrypted property length and set database restrictions on that.</param>
    /// <param name="cryptoConverter">Crypto converter.</param>
    /// <param name="migrationType">Type of Migration that property started to be encrypted from.</param>
    public static PropertyBuilder EncryptedWith(
        this PropertyBuilder<string> propertyBuilder,
        ICryptoConverter cryptoConverter,
        int? maxLength = default,
        Type migrationType = default!)
    {
        Defend.Against.Null(propertyBuilder, nameof(propertyBuilder));
        Defend.Against.Null(cryptoConverter, nameof(cryptoConverter));

        MigrationAttribute? migrationAttribute = default!;
        if (migrationType is not null)
        {
            if (!migrationType.IsSubclassOf(typeof(Migration)))
            {
                throw new ArgumentException("Migration type must be inherited from Migration.", nameof(migrationType));
            }

            migrationAttribute = migrationType
                .GetCustomAttributes(typeof(MigrationAttribute), true)
                .SingleOrDefault()
                as MigrationAttribute;

            Defend.Against.Null(migrationAttribute, nameof(migrationAttribute), message: "Provided Migration type has no MigrationAttribute.");
        }

        // Add this value converter into migration query.
        EncryptingMigrator.AddEncryptedProperty(new EncryptedProperty
        {
            PropertyBuilder = propertyBuilder,
            CryptoConverter = cryptoConverter,
            MaxLength = maxLength,
            MigrationId = migrationAttribute?.Id,
        });

        return propertyBuilder;
    }

    /// <summary>
    /// Apply encryption to the property.
    /// ATTENTION! If there are already some data in DB that must be encrypted then you must provide Migration to encrypt existing data.
    /// !!! In this case ensure that you call <see cref="DatabaseFacadeExtensions.MigrateWithEncryptingMigratorAsync"/> or <see cref="DatabaseFacadeExtensions.MigrateWithEncryptingMigratorAsync"/> on the database context.
    /// </summary>
    /// <param name="propertyBuilder">Property builder.</param>
    /// <param name="maxLength">Max length of the encrypted property.</param>
    /// <param name="migrationType">Type of Migration that property started to be encrypted from.</param>
    /// <typeparam name="TCryptoConverter">Crypto converter type.</typeparam>
    public static PropertyBuilder EncryptedWith<TCryptoConverter>(this PropertyBuilder<string> propertyBuilder, int? maxLength = default, Type migrationType = default!)
        where TCryptoConverter : ICryptoConverter, new()
    {
        return EncryptedWith(propertyBuilder, new TCryptoConverter(), maxLength, migrationType);
    }
}

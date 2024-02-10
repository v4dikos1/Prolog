using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Prolog.Core.EntityFramework.Features.Encryption.Internal.ValueConverters;
using Prolog.Core.Utils;
using System.Data;

namespace Prolog.Core.EntityFramework.Features.Encryption.Internal;

/// <summary>
/// Migrator that provide logic for updating old values during migrations with encrypting values.
/// </summary>
internal static class EncryptingMigrator
{
    private static IEnumerable<string> pendingMigrations = Array.Empty<string>();
    private static readonly List<EncryptedProperty> EncryptedProperties = new();

    internal static void AddEncryptedProperty(EncryptedProperty encryptedProperty)
    {
        Defend.Against.Null(encryptedProperty, nameof(encryptedProperty));
        AddConverters(encryptedProperty);
        EncryptedProperties.Add(encryptedProperty);
    }

    private static void AddConverters(EncryptedProperty encryptedProperty)
    {
        // Apply encrypted conversion.
        var cryptoValueConverter = new CryptoValueConverter(encryptedProperty.CryptoConverter);
        encryptedProperty.PropertyBuilder.HasConversion(cryptoValueConverter);

        if (encryptedProperty.MaxLength.HasValue)
        {
            var maxEncryptedLength = encryptedProperty.CryptoConverter.GetMaximalOverheadedLength(encryptedProperty.MaxLength.Value);
            encryptedProperty.PropertyBuilder.HasMaxLength(maxEncryptedLength);
        }
    }

    [Obsolete]
    internal static async Task MigrateWithEncriptingMigratorAsync(DatabaseFacade databaseFacade, CancellationToken cancellationToken = default)
    {
        var dbContext = (databaseFacade as IDatabaseFacadeDependenciesAccessor).Context;
        pendingMigrations = dbContext.Database.GetPendingMigrations();
        await databaseFacade.MigrateAsync(cancellationToken);
        await MigrateEncryptedPropertiesAsync(dbContext);
    }

    /// <summary>
    /// Migrate old data for encrypted properties if needed.
    /// </summary>
    [Obsolete]
    private static async Task MigrateEncryptedPropertiesAsync(DbContext dbContext)
    {
        foreach (var encryptedProperty in EncryptedProperties)
        {
            // Convert old data.
            if (encryptedProperty.MigrationId is not null)
            {
                // Check that migration exists or not.
                var isValuesNeedsToBeEncrypted = pendingMigrations.Contains(encryptedProperty.MigrationId);

                if (isValuesNeedsToBeEncrypted)
                {
                    var propertyMetadata = encryptedProperty.PropertyBuilder.Metadata;

                    var entityTypeName = propertyMetadata.DeclaringEntityType.ClrType.FullName
                        ?? throw new Exception("Cannot define fullname of the entity type.");
                    var entityType = dbContext.Model.FindEntityType(entityTypeName)
                        ?? throw new Exception($"Cannot find entity type with name '{entityTypeName}'.");

                    var tableName = entityType.GetAnnotation("Relational:TableName").Value;
                    var pkProps = entityType.FindPrimaryKey()?.Properties.Select(_ => _.Name)
                        ?? throw new Exception("Cannot find primary key properties.");
                    var targetColumnName = propertyMetadata.Name;

                    dbContext.Database.OpenConnection();

                    // Select rows that needs to be updated with encryption.
                    IEnumerable<IDataRecord> rowsToUpdate;
                    using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        var columns = AtomicUtils.SafelyJoin(",", pkProps.Append(targetColumnName).Select(_ => $"\"{_}\""));
                        command.CommandText = $"SELECT {columns} FROM \"{tableName}\"";

                        using var result = await command.ExecuteReaderAsync();
                        rowsToUpdate = result.Cast<IDataRecord>().ToList();
                    }

                    // Update rows with encryption.
                    await dbContext.Database.BeginTransactionAsync();
                    foreach (var row in rowsToUpdate)
                    {
                        var whereValues = pkProps
                            .Select(propName => $"\"{propName}\"='{row.GetValue(row.GetOrdinal(propName))}'");
                        var newEncryptedValue = encryptedProperty.CryptoConverter.Encrypt(row.GetString(row.GetOrdinal(targetColumnName)));
                        await dbContext.Database.ExecuteSqlRawAsync($"UPDATE \"{tableName}\" SET \"{targetColumnName}\"='{newEncryptedValue}' WHERE {AtomicUtils.SafelyJoin(" and ", whereValues)}");
                    }
                    await dbContext.Database.CommitTransactionAsync();
                    dbContext.Database.CloseConnection();
                }
            }
        }

        EncryptedProperties.Clear();
    }
}

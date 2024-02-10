using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Prolog.Core.EntityFramework.Features.Encryption.Internal;
using Prolog.Core.Utils;

namespace Prolog.Core.EntityFramework.Features.Encryption.Public.Extensions;

public static class DatabaseFacadeExtensions
{
    /// <summary>
    /// Do migration with additional encryption of non-ecrypted properties.
    /// Use it if there are already some data in DB that must be encrypted.
    /// !!! ONLY PostgreSQL IS SUPPORTED. !!!
    /// </summary>
    [Obsolete("Obsolete")]
    public static Task MigrateWithEncryptingMigratorAsync(this DatabaseFacade databaseFacade, CancellationToken cancellationToken = default)
    {
        Defend.Against.Null(databaseFacade, nameof(databaseFacade));
        return EncryptingMigrator.MigrateWithEncriptingMigratorAsync(databaseFacade, cancellationToken);
    }
}

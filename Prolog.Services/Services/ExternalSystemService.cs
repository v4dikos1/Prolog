using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.Services;
using Prolog.Core.Exceptions;
using Prolog.Core.Utils;
using Prolog.Domain;
using Prolog.Domain.Entities;

namespace Prolog.Services.Services;

internal class ExternalSystemService(ApplicationDbContext dbContext): IExternalSystemService
{
    public async Task<ExternalSystem> GetExternalSystemWithCheckExistsAsync(Guid externalSystemId, CancellationToken cancellationToken)
    {
        Defend.Against.NullOrEmpty(externalSystemId, nameof(externalSystemId));

        var externalSystem = await dbContext.ExternalSystems
            .SingleOrDefaultAsync(x => !x.IsArchive && x.IdentityId == externalSystemId, cancellationToken)
            ?? throw new ObjectNotFoundException($"Пользователь или внешняя система с идентификатором {externalSystemId} не найдена!");
        return externalSystem;
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Storages.Dtos;
using Prolog.Application.Storages.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Storages.Handlers;

internal class StorageQueriesHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IStorageMapper storageMapper):
    IRequestHandler<GetStoragesListQuery, PagedResult<StorageListViewModel>>, IRequestHandler<GetStorageQuery, StorageViewModel>
{
    public async Task<PagedResult<StorageListViewModel>> Handle(GetStoragesListQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var storagesQuery = dbContext.Storages
            .AsNoTracking()
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Address)
            .ApplySearch(request, x => x.Name, x => x.Address.AddressFullName, x => x.Name);

        var storagesList = await storagesQuery
            .ApplyPagination(request)
        .ToListAsync(cancellationToken);

        var result = storagesList.Select(storageMapper.MapToListViewModel);
        return result.AsPagedResult(request, await storagesQuery.CountAsync(cancellationToken));
    }

    public async Task<StorageViewModel> Handle(GetStorageQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var existingStorage = await dbContext.Storages
            .AsNoTracking()
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => x.Id == request.StorageId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Склад с идентификатором {request.StorageId} не найден!");

        return storageMapper.MapToViewModel(existingStorage);
    }
}
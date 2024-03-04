using MediatR;
using Prolog.Application.Storages.Dtos;
using Prolog.Application.Storages.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Domain;

namespace Prolog.Application.Storages.Handlers;

internal class StorageQueriesHandler(ApplicationDbContext dbContext):
    IRequestHandler<GetStoragesListQuery, PagedResult<StorageListViewModel>>, IRequestHandler<GetStorageQuery, StorageViewModel>
{
    public async Task<PagedResult<StorageListViewModel>> Handle(GetStoragesListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<StorageViewModel> Handle(GetStorageQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
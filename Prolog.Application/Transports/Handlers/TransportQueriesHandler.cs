using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Transports.Dtos;
using Prolog.Application.Transports.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Transports.Handlers;

internal class TransportQueriesHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, ITransportMapper transportMapper):
    IRequestHandler<GetTransportsListQuery, PagedResult<TransportListViewModel>>,
    IRequestHandler<GetTransportQuery, TransportListViewModel>
{
    public async Task<PagedResult<TransportListViewModel>> Handle(GetTransportsListQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var transportQuery = dbContext.Transports
            .AsNoTracking()
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .OrderBy(x => x.LicencePlate)
            .ApplySearch(request, x => x.LicencePlate, x => x.Brand);

        var transportsList = await transportQuery
            .ApplyPagination(request)
        .ToListAsync(cancellationToken);

        var result = transportsList.Select(transportMapper.MapToListViewModel);
        return result.AsPagedResult(request, await transportQuery.CountAsync(cancellationToken));
    }

    public async Task<TransportListViewModel> Handle(GetTransportQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var existingTransport = await dbContext.Transports
            .AsNoTracking()
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => x.Id == request.TransportId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Транспортное средство с идентификатором {request.TransportId} не найдено!");

        return transportMapper.MapToListViewModel(existingTransport);
    }
}
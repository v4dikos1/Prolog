using MediatR;
using Prolog.Application.Transports.Dtos;
using Prolog.Application.Transports.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Application.Transports.Handlers;

internal class TransportQueriesHandler: IRequestHandler<GetTransportsListQuery, PagedResult<TransportListViewModel>>,
    IRequestHandler<GetTransportQuery, TransportListViewModel>
{
    public Task<PagedResult<TransportListViewModel>> Handle(GetTransportsListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TransportListViewModel> Handle(GetTransportQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
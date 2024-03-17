using MediatR;
using Prolog.Application.Drivers.Dtos;
using Prolog.Application.Drivers.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Application.Drivers.Handlers;

internal class DriversQueriesHandlers: IRequestHandler<GetDriversListQuery, PagedResult<DriverListViewModel>>,
    IRequestHandler<GetDriverQuery, DriverListViewModel>
{
    public Task<PagedResult<DriverListViewModel>> Handle(GetDriversListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DriverListViewModel> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
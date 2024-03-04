using MediatR;
using Prolog.Application.Clients.Dtos;
using Prolog.Application.Clients.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Domain;

namespace Prolog.Application.Clients.Handlers;

internal class CustomerQueriesHandler(ApplicationDbContext dbContext):
    IRequestHandler<GetCustomersLListQuery, PagedResult<CustomerListViewModel>>
{
    public async Task<PagedResult<CustomerListViewModel>> Handle(GetCustomersLListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Clients.Dtos;
using Prolog.Application.Clients.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Clients.Handlers;

internal class CustomerQueriesHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IClientMapper clientMapper):
    IRequestHandler<GetCustomersLListQuery, PagedResult<CustomerListViewModel>>, IRequestHandler<GetCustomerQuery, CustomerViewModel>
{
    public async Task<PagedResult<CustomerListViewModel>> Handle(GetCustomersLListQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var customersQuery = dbContext.Customers
            .AsNoTracking()
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .OrderBy(x => x.Name)
            .ThenBy(x => x.PhoneNumber)
            .ApplySearch(request, x => x.Name, x => x.PhoneNumber);

        var customersList = await customersQuery
            .ApplyPagination(request)
        .ToListAsync(cancellationToken);

        var result = customersList.Select(clientMapper.MapToListViewModel);
        return result.AsPagedResult(request, await customersQuery.CountAsync(cancellationToken));
    }

    public async Task<CustomerViewModel> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var existingCustomer = await dbContext.Customers
            .AsNoTracking()
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => x.Id == request.CustomerId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Клиент с идентификатором {request.CustomerId} не найден!");

        return clientMapper.MapToViewModel(existingCustomer);
    }
}
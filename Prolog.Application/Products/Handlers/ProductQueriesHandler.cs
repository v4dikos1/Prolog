using MediatR;
using Prolog.Application.Products.Dtos;
using Prolog.Application.Products.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Domain;

namespace Prolog.Application.Products.Handlers;

internal class ProductQueriesHandler(ApplicationDbContext dbContext):
    IRequestHandler<GetProductsListQuery, PagedResult<ProductListViewModel>>, IRequestHandler<GetProductQuery, ProductViewModel>
{
    public async Task<PagedResult<ProductListViewModel>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductViewModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
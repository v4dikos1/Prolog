using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Products.Dtos;
using Prolog.Application.Products.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Products.Handlers;

internal class ProductQueriesHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IProductMapper productMapper):
    IRequestHandler<GetProductsListQuery, PagedResult<ProductListViewModel>>, IRequestHandler<GetProductQuery, ProductViewModel>
{
    public async Task<PagedResult<ProductListViewModel>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var productsQuery = dbContext.Products
            .AsNoTracking()
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .OrderBy(x => x.Name)
            .ApplySearch(request, x => x.Name, x => x.Code);

        var productsList = await productsQuery
            .ApplyPagination(request)
            .ToListAsync(cancellationToken);

        var result = productsList.Select(productMapper.MapToListViewModel);
        return result.AsPagedResult(request, await productsQuery.CountAsync(cancellationToken));
    }

    public async Task<ProductViewModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var existingProduct = await dbContext.Products
            .AsNoTracking()
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => x.Id == request.ProductId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Товар с идентификатором {request.ProductId} не найден!");

        return productMapper.MapToViewModel(existingProduct);
    }
}
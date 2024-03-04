using MediatR;
using Prolog.Application.BaseModels;
using Prolog.Application.Products.Dtos;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using System.ComponentModel;

namespace Prolog.Application.Products.Queries;

[Description("Получение списка товаров")]
public class GetProductsListQuery: SearchablePagedQuery, IRequest<PagedResult<ProductListViewModel>>
{
    
}
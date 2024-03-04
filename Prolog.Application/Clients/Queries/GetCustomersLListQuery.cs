using MediatR;
using Prolog.Application.BaseModels;
using Prolog.Application.Clients.Dtos;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using System.ComponentModel;

namespace Prolog.Application.Clients.Queries;

[Description("Получение списка клиентов")]
public class GetCustomersLListQuery: SearchablePagedQuery, IRequest<PagedResult<CustomerListViewModel>>
{
}
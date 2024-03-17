using MediatR;
using Prolog.Application.BaseModels;
using Prolog.Application.Drivers.Dtos;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using System.ComponentModel;

namespace Prolog.Application.Drivers.Queries;

[Description("Получение списка водителей")]
public class GetDriversListQuery: SearchablePagedQuery, IRequest<PagedResult<DriverListViewModel>>
{
}
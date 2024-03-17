using MediatR;
using Prolog.Application.BaseModels;
using Prolog.Application.Transports.Dtos;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using System.ComponentModel;

namespace Prolog.Application.Transports.Queries;

[Description("Получение списка транспортных средств")]
public class GetTransportsListQuery: SearchablePagedQuery, IRequest<PagedResult<TransportListViewModel>>
{
}
using MediatR;
using Prolog.Application.BaseModels;
using Prolog.Application.Storages.Dtos;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using System.ComponentModel;

namespace Prolog.Application.Storages.Queries;

[Description("Получение списка складов")]
public class GetStoragesListQuery: SearchablePagedQuery, IRequest<PagedResult<StorageListViewModel>>
{

}
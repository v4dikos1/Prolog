using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.BaseModels;
using Prolog.Application.Orders.Dtos;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Domain.Enums;
using System.ComponentModel;

namespace Prolog.Application.Orders.Queries;

[Description("Получение списка заявок")]
public class GetOrdersListQuery: SearchablePagedQuery, IRequest<PagedResult<OrderListGroupedByDateViewModel>>
{
    /// <summary>
    /// Статус заявки (0 - получить входящие, 1 - активные, 2 - завершенные, 3 - получить все)
    /// </summary>
    [FromQuery]
    public required OrderFilterStatusEnum Status { get; set; } = OrderFilterStatusEnum.Incoming;

    /// <summary>
    /// Фильтр по дате начала
    /// </summary>
    [FromQuery]
    public DateTimeOffset? StartDate { get; set; }

    /// <summary>
    /// Фильтр по дате конца
    /// </summary>
    [FromQuery]
    public DateTimeOffset? EndDate { get; set; }
}
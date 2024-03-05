using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Api.StartupConfigurations;
using Prolog.Application.Orders.Commands;
using Prolog.Application.Orders.Dtos;
using Prolog.Application.Orders.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Api.Controllers;

[Route("api/admin/orders")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Policy = KeycloakAuthConfiguration.AdminApiPolicy)]
public class OrdersController(ISender sender): BaseController
{
    /// <summary>
    /// Добавление заявки
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор добавленной заявки</returns>
    [HttpPost]
    public async Task<CreatedOrUpdatedEntityViewModel<long>> AddOrder([FromQuery] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получение списка заявок по фильтрам
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список заявок</returns>
    [HttpGet]
    public async Task<PagedResult<OrderListGroupedByDateViewModel>> GetOrdersList([FromQuery] GetOrdersListQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Архивация входящих или активных заявок
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    public async Task ArchiveOrders([FromQuery] ArchiveOrdersCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Отмена активных заявок
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPatch]
    public async Task CancelOrders([FromQuery] CancelOrdersCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
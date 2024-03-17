using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Api.StartupConfigurations;
using Prolog.Application.Clients.Commands;
using Prolog.Application.Clients.Dtos;
using Prolog.Application.Clients.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Api.Controllers;

[Route("api/admin/customers")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Policy = KeycloakAuthConfiguration.AdminApiPolicy)]
public class CustomerController(ISender sender): BaseController
{
    /// <summary>
    /// Добавление клиента
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор клиента</returns>
    [HttpPost]
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> CreateCustomer([FromQuery] CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получение списка клиентов
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    public async Task<PagedResult<CustomerListViewModel>> GetCustomersListQuery(
        [FromQuery] GetCustomersLListQuery query, CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }


    /// <summary>
    /// Получение конкретного клиента
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель клиента</returns>
    [HttpGet("{CustomerId}")]
    public async Task<CustomerViewModel> GetCustomer([FromQuery] GetCustomerQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Архивация клиентов
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    public async Task ArchiveCustomers([FromQuery] ArchiveCustomersCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновление информации о клиенте
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{CustomerId}")]
    public async Task UpdateCustomer([FromQuery] UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
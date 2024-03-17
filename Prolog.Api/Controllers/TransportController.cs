using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Api.StartupConfigurations;
using Prolog.Application.Transports.Commands;
using Prolog.Application.Transports.Dtos;
using Prolog.Application.Transports.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Api.Controllers;

[Route("api/admin/transports")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Policy = KeycloakAuthConfiguration.AdminApiPolicy)]
public class TransportController(ISender sender): BaseController
{
    /// <summary>
    /// Получение списка транспортных средств
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список ТС</returns>
    [HttpGet]
    public async Task<PagedResult<TransportListViewModel>> GetTransportsList([FromQuery] GetTransportsListQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получение конкретнонго транспортного средства
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ТС</returns>
    [HttpGet("{TransportId}")]
    public async Task<TransportListViewModel> GetTransport([FromQuery] GetTransportQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавление транспортного средства
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор добавленного ТС</returns>
    [HttpPost]
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> AddTransport([FromQuery] AddTransportCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновление транспортного средства
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{TransportId}")]
    public async Task UpdateTransport([FromQuery] UpdateTransportCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Архивирование транспортного средства
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    public async Task ArchiveTransport([FromQuery] ArchiveTransportCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
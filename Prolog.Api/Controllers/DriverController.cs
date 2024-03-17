using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Api.StartupConfigurations;
using Prolog.Application.Drivers.Commands;
using Prolog.Application.Drivers.Dtos;
using Prolog.Application.Drivers.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Api.Controllers;

[Route("api/admin/drivers")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Policy = KeycloakAuthConfiguration.AdminApiPolicy)]
public class DriverController(ISender sender): BaseController
{
    /// <summary>
    /// Получение списка водителей
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список водителей</returns>
    [HttpGet]
    public async Task<PagedResult<DriverListViewModel>> GetDriversList([FromQuery] GetDriversListQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получение водителя
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Водитель</returns>
    [HttpGet("{DriverId}")]
    public async Task<DriverListViewModel> GetDriver([FromQuery] GetDriverQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавление водителя
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор добавленного водителя</returns>
    [HttpPost]
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> AddDriver([FromQuery] AddDriverCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновление водителя
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{DriverId}")]
    public async Task UpdateDriver([FromQuery] UpdateDriverCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Архивирование водителя
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    public async Task ArchiveDriver([FromQuery] ArchiveDriverCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
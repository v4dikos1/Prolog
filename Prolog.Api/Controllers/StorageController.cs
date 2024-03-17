using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Api.StartupConfigurations;
using Prolog.Application.Storages.Commands;
using Prolog.Application.Storages.Dtos;
using Prolog.Application.Storages.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Api.Controllers;

[Route("api/admin/storages")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Policy = KeycloakAuthConfiguration.AdminApiPolicy)]
public class StorageController(ISender sender): BaseController
{
    /// <summary>
    /// Добавление склада
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного склада</returns>
    [HttpPost]
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> CreateStorage([FromQuery] CreateStorageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получение списка складов
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список складов</returns>
    [HttpGet]
    public async Task<PagedResult<StorageListViewModel>> GetStoragesList([FromQuery] GetStoragesListQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получние конкртеного склада
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель конкретного склада</returns>
    [HttpGet("{StorageId}")]
    public async Task<StorageViewModel> GetStorage([FromQuery] GetStorageQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Архивирование складов
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete]
    public async Task ArchiveStorages([FromQuery] ArchiveStoragesCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    } 

    /// <summary>
    /// Обновление информации о складе
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{StorageId}")]
    public async Task UpdateStorage([FromQuery] UpdateStorageCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Api.StartupConfigurations;
using Prolog.Application.Products.Commands;
using Prolog.Application.Products.Dtos;
using Prolog.Application.Products.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;

namespace Prolog.Api.Controllers;

[Route("api/admin/products")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Policy = KeycloakAuthConfiguration.AdminApiPolicy)]
public class ProductsController(ISender sender): BaseController
{
    /// <summary>
    /// Добавление товара
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного товара</returns>
    [HttpPost]
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> CreateProduct([FromQuery] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получение списка товаров
    /// </summary>
    /// <param name="query">Моедль запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список товаров</returns>
    [HttpGet]
    public async Task<PagedResult<ProductListViewModel>> GetProductsList([FromQuery] GetProductsListQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получение товара
    /// </summary>
    /// <param name="query">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель товара</returns>
    [HttpGet("{ProductId}")]
    public async Task<ProductViewModel> GetProduct([FromQuery] GetProductQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    /// <summary>
    /// Обновление информации о товаре
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("{ProductId}")]
    public async Task UpdateProduct([FromQuery] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Импортирование товаров
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("import")]
    public async Task ImportProducts([FromQuery] ImportProductCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Архивирование товаров
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpDelete]
    public async Task RemoveProducts([FromQuery] ArchiveProductsCommand command, CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
    }
}
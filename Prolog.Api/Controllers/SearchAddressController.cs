using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prolog.Api.StartupConfigurations;
using Prolog.Application.Addresses.Dtos;
using Prolog.Application.Addresses.Queries;

namespace Prolog.Api.Controllers;

[Route("api/admin/addresses")]
[ApiExplorerSettings(GroupName = "admin")]
[Authorize(Policy = KeycloakAuthConfiguration.AdminApiPolicy)]
public class SearchAddressController(ISender sender) : BaseController
{
    /// <summary>
    /// Поиск по полному адресу
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("fullname")]
    public async Task<IEnumerable<ViewAddressModel>> SearchFullName([FromQuery] SearchAddressByFullNameQuery request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result;
    }

    /// <summary>
    ///     Поиск региона
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("region")]
    public async Task<IEnumerable<ViewAddressModel>> SearchRegion([FromQuery] SearchAddressRegionQuery request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result;
    }

    /// <summary>
    ///     Поиск района в регионе
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("area")]
    public async Task<IEnumerable<ViewAddressModel>> SearchArea([FromQuery] SearchAddressAreaQuery request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result;
    }

    /// <summary>
    ///     Поиск города
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("city")]
    public async Task<IEnumerable<ViewAddressModel>> SearchCity([FromQuery] SearchAddressCityQuery request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result;
    }

    /// <summary>
    ///     Поиск населенного пункта
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("settlement")]
    public async Task<IEnumerable<ViewAddressModel>> SearchSettlement([FromQuery] SearchAddressSettlementQuery request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result;
    }

    /// <summary>
    ///     Поиск улицы
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("street")]
    public async Task<IEnumerable<ViewAddressModel>> SearchStreet([FromQuery] SearchAddressStreetQuery request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result;
    }
    
    /// <summary>
    ///     Поиск дома
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("house")]
    public async Task<IEnumerable<ViewAddressModel>> SearchBuilding([FromQuery] SearchAddressBuildingQuery request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);
        return result;
    }
}
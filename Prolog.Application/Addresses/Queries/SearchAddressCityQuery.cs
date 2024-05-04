using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Addresses.Dtos;

namespace Prolog.Application.Addresses.Queries;

public class SearchAddressCityQuery : IRequest<IEnumerable<ViewAddressModel>>
{
    /// <summary>
    ///     Строка запроса
    /// </summary>
    [FromQuery]
    public required string Query { get; set; }

    /// <summary>
    ///     Идентификатор области
    /// </summary>
    [FromQuery]
    public required string AreaFiasId { get; set; }

    /// <summary>
    ///     Идентификатор региона
    /// </summary>
    [FromQuery]
    public required string RegionFiasId { get; set; }
}
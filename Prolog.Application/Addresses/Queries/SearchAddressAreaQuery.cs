using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Addresses.Dtos;

namespace Prolog.Application.Addresses.Queries;

public class SearchAddressAreaQuery : IRequest<IEnumerable<ViewAddressModel>>
{
    /// <summary>
    ///     Строка запроса
    /// </summary>
    [FromQuery]
    public required string Query { get; set; }

    /// <summary>
    ///     Идентификатор региона
    /// </summary>
    [FromQuery]
    public required string RegionFiasId { get; set; }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Addresses.Dtos;

namespace Prolog.Application.Addresses.Queries;

public class SearchAddressRegionQuery : IRequest<IEnumerable<ViewAddressModel>>
{
    /// <summary>
    ///     Строка запроса
    /// </summary>
    [FromQuery]
    public required string Query { get; set; }
}
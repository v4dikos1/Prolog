using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Addresses.Dtos;

namespace Prolog.Application.Addresses.Queries;

public class SearchAddressStreetQuery : IRequest<IEnumerable<ViewAddressModel>>
{
    /// <summary>
    ///     Строка запроса
    /// </summary>
    [FromQuery]
    public required string Query { get; set; }

    /// <summary>
    ///     Идентфикатор города
    /// </summary>
    [FromQuery]
    public required string CityFiasId { get; set; }

    /// <summary>
    ///     Идентификатор поселения
    /// </summary>
    [FromQuery]
    public required string SettlementFiasId { get; set; }
}
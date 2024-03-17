using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Transports.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Transports.Queries;

[Description("Получение транспорта")]
public class GetTransportQuery: IRequest<TransportListViewModel>
{
    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    [FromRoute]
    public Guid TransportId { get; set; }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Transports.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Transports.Commands;

[Description("Обновление траспорта")]
public class UpdateTransportCommand: IRequest
{
    /// <summary>
    /// Идентификатор транспорта
    /// </summary>
    [FromRoute]
    public required Guid TransportId { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required UpdateTransportModel Body { get; set; }
}
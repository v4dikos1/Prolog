using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Transports.Commands;

[Description("Архивация транспортных средств")]
public class ArchiveTransportCommand: IRequest
{
    /// <summary>
    /// Идентификаторы транспортных средств
    /// </summary>
    [FromQuery]
    public required IEnumerable<Guid> TransportIds { get; set; }
}
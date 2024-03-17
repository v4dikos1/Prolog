using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Drivers.Commands;

[Description("Архивация водителя")]
public class ArchiveDriverCommand: IRequest
{
    /// <summary>
    /// Идентификаторы водителей
    /// </summary>
    [FromQuery]
    public required IEnumerable<Guid> DriverIds { get; set; }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Clients.Commands;

[Description("Архивация клиентов")]
public class ArchiveCustomersCommand: IRequest
{
    /// <summary>
    /// Идентификаторы клиентов
    /// </summary>
    [FromQuery]
    public required IEnumerable<Guid> CustomerIds { get; set; }
}
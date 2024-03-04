using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Clients.Commands;

[Description("Архивация клиента")]
public class ArchiveCustomerCommand: IRequest
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [FromRoute]
    public required Guid CustomerId { get; set; }
}
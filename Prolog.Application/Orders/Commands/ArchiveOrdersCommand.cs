using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Orders.Commands;

[Description("Архивация заявок")]
public class ArchiveOrdersCommand: IRequest
{
    /// <summary>
    /// Идентификаторы заявок
    /// </summary>
    [FromQuery]
    public required IEnumerable<long> OrderIds { get; set; }
}
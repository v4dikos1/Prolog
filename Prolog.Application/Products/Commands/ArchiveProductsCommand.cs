using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Products.Commands;

[Description("Архивация товаров")]
public class ArchiveProductsCommand: IRequest
{
    /// <summary>
    /// Идентификаторы товаров для архивирования
    /// </summary>
    [FromQuery]
    public required IEnumerable<Guid> ProductIds { get; set; }
}
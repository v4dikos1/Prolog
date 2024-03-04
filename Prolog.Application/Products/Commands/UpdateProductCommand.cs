using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Products.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Products.Commands;

[Description("Обновление информации о товаре")]
public class UpdateProductCommand: IRequest
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    [FromRoute]
    public required Guid ProductId { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required UpdateProductModel Body { get; set; }
}
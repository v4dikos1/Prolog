using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Products.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Products.Queries;

[Description("Получение товара")]
public class GetProductQuery: IRequest<ProductViewModel>
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    [FromRoute]
    public required Guid ProductId { get; set; }
}
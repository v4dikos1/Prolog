using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Products.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Products.Commands;

[Description("Добавление товара")]
public class CreateProductCommand: IRequest<CreatedOrUpdatedEntityViewModel<Guid>>
{
    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required CreateProductModel Body { get; set; }
}
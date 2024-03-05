using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Orders.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Orders.Commands;

[Description("Добавление заявки")]
public class CreateOrderCommand: IRequest<CreatedOrUpdatedEntityViewModel<long>>
{
    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required CreateOrderModel Body { get; set; }
}
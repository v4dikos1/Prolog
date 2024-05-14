using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Orders.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Orders.Commands;

[Description("Запуск планирования")]
public class PlanOrdersCommand: IRequest
{
    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required PlanOrdersModel Body { get; set; }
}
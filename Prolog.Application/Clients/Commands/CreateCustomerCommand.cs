using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Clients.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Clients.Commands;

[Description("Добавлние клиента")]
public class CreateCustomerCommand: IRequest<CreatedOrUpdatedEntityViewModel<Guid>>
{
    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required CreateCustomerModel Body { get; set; }
}
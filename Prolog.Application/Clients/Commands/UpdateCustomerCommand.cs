using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Clients.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Clients.Commands;

[Description("Обновление информации о клиенте")]
public class UpdateCustomerCommand: IRequest
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [FromRoute]
    public required Guid CustomerId { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required UpdateCustomerModel Body { get; set; }
}
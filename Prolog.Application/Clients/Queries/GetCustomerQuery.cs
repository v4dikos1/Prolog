using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Clients.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Clients.Queries;

[Description("Получение конкртеного клиента")]
public class GetCustomerQuery: IRequest<CustomerViewModel>
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [FromRoute]
    public required Guid CustomerId { get; set; }
}
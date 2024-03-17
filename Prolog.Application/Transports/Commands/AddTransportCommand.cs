using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Transports.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Transports.Commands;

[Description("Добавление транспорта")]
public class AddTransportCommand: IRequest<CreatedOrUpdatedEntityViewModel<Guid>>
{
    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required UpdateTransportModel Body { get; set; }
}
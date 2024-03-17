using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Drivers.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Drivers.Commands;

[Description("Добавление водителя")]
public class AddDriverCommand: IRequest<CreatedOrUpdatedEntityViewModel<Guid>>
{
    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required UpdateDriverModel Body { get; set; }
}
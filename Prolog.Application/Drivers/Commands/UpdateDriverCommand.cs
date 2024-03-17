using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Drivers.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Drivers.Commands;

[Description("Обновление водителя")]
public class UpdateDriverCommand: IRequest
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    [FromRoute]
    public required Guid DriverId { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required UpdateDriverModel Body { get; set; }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Drivers.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Drivers.Queries;

[Description("Получение водителя")]
public class GetDriverQuery: IRequest<DriverListViewModel>
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    [FromRoute]
    public required Guid DriverId { get; set; }
}
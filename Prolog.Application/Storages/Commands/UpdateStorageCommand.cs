using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Storages.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Storages.Commands;

[Description("Обновление информации о складе")]
public class UpdateStorageCommand: IRequest
{
    /// <summary>
    /// Идентификатор склада
    /// </summary>
    [FromRoute]
    public required Guid StorageId { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required UpdateStorageModel Body { get; set; }
}
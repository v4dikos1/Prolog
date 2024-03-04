using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Storages.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Storages.Commands;

[Description("Добавление склада")]
public class CreateStorageCommand: IRequest<CreatedOrUpdatedEntityViewModel<Guid>>
{
    /// <summary>
    /// Тело запроса
    /// </summary>
    [FromBody]
    public required CreateStorageModel Body { get; set; }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Storages.Commands;

[Description("Архивация склада")]
public class ArchiveStorageCommand: IRequest
{
    /// <summary>
    /// Идентификатор склада
    /// </summary>
    [FromRoute]
    public required Guid StorageId { get; set; }
}
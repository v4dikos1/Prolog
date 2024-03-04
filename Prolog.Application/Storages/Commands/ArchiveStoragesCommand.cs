using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Storages.Commands;

[Description("Архивация складов")]
public class ArchiveStoragesCommand: IRequest
{
    /// <summary>
    /// Идентификаторы складов
    /// </summary>
    [FromQuery]
    public required IEnumerable<Guid> StorageIds { get; set; }
}
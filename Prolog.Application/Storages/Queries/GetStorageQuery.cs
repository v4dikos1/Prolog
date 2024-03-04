using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prolog.Application.Storages.Dtos;
using System.ComponentModel;

namespace Prolog.Application.Storages.Queries;

[Description("Получение модели склада")]
public class GetStorageQuery: IRequest<StorageViewModel>
{
    /// <summary>
    /// Идентификатор склада
    /// </summary>
    [FromRoute]
    public required Guid StorageId { get; set; }
}
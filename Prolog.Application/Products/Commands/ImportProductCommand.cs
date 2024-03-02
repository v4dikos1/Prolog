using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Prolog.Application.Products.Commands;

[Description("Импортирование товаров")]
public class ImportProductCommand: IRequest
{
    /// <summary>
    /// JSON файл с настройками проекта
    /// </summary>
    [FromForm]
    public required IFormFile File { get; set; }
}
using FluentValidation;
using Prolog.Application.Products.Commands;

namespace Prolog.Application.Products.Validators;

internal class ImportProductCommandValidator: AbstractValidator<ImportProductCommand>
{
    public ImportProductCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .WithMessage("Файл импорта не должен быть пустым!");
    }
}
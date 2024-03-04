using FluentValidation;
using Prolog.Application.Products.Commands;

namespace Prolog.Application.Products.Validators;

internal class ArchiveProductsCommandValidator: AbstractValidator<ArchiveProductsCommand>
{
    public ArchiveProductsCommandValidator()
    {
        RuleFor(x => x.ProductIds)
            .NotEmpty()
            .WithMessage("Список идентификаторов товаров не должен быть пустым!");
    }
}
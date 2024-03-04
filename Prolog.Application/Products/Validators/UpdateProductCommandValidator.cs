using FluentValidation;
using Prolog.Application.Products.Commands;

namespace Prolog.Application.Products.Validators;

internal class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Идентификатор товара не должен быть пустым!");

        RuleFor(x => x.Body)
            .NotNull()
            .WithMessage("Тело запроса не должно быть пустым!");

        RuleFor(x => x.Body.Name)
            .NotEmpty()
            .WithMessage("Наименование товара является обязательным полем!");

        RuleFor(x => x.Body.Price)
            .NotEmpty()
            .WithMessage("Цена товара является обязательным полем!");

        RuleFor(x => x.Body.Volume)
            .NotEmpty()
            .WithMessage("Объем товара является обязательным полем!");

        RuleFor(x => x.Body.Weight)
            .NotEmpty()
            .WithMessage("Вес товара является обязательным полем!");
    }
}
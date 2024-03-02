using FluentValidation;
using Prolog.Application.Products.Queries;

namespace Prolog.Application.Products.Validators;

internal class GetProductQueryValidator: AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Идентификатор товара не должен быть пустым!");
    }
}
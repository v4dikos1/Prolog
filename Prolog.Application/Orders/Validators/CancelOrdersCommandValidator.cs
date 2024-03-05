using FluentValidation;
using Prolog.Application.Orders.Commands;

namespace Prolog.Application.Orders.Validators;

internal class CancelOrdersCommandValidator: AbstractValidator<CancelOrdersCommand>
{
    public CancelOrdersCommandValidator()
    {
        RuleFor(x => x.OrderIds)
            .NotEmpty()
            .WithMessage("Список идентификаторов заявок не должен быть пустым!");
    }
}
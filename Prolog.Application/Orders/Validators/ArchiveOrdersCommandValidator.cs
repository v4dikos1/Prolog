using FluentValidation;
using Prolog.Application.Orders.Commands;

namespace Prolog.Application.Orders.Validators;

internal class ArchiveOrdersCommandValidator: AbstractValidator<ArchiveOrdersCommand>
{
    public ArchiveOrdersCommandValidator()
    {
        RuleFor(x => x.OrderIds)
            .NotEmpty()
            .WithMessage("Список идентификаторов заявок не должен быть пустым!");
    }
}
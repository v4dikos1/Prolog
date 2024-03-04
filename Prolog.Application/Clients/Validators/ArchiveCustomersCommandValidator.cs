using FluentValidation;
using Prolog.Application.Clients.Commands;

namespace Prolog.Application.Clients.Validators;

internal class ArchiveCustomersCommandValidator: AbstractValidator<ArchiveCustomersCommand>
{
    public ArchiveCustomersCommandValidator()
    {
        RuleFor(x => x.CustomerIds)
            .NotNull()
            .WithMessage("Список идентификаторов клиентов не должен быть пустым!");
    }
}
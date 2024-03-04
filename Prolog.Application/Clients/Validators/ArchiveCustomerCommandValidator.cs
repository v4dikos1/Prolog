using FluentValidation;
using Prolog.Application.Clients.Commands;

namespace Prolog.Application.Clients.Validators;

internal class ArchiveCustomerCommandValidator: AbstractValidator<ArchiveCustomerCommand>
{
    public ArchiveCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Идентификатор клиента является обязательным параметром!");
    }
}
using FluentValidation;
using Prolog.Application.Transports.Commands;

namespace Prolog.Application.Transports.Validators;

internal class ArchiveTransportCommandValidator: AbstractValidator<ArchiveTransportCommand>
{
    public ArchiveTransportCommandValidator()
    {
        RuleFor(x => x.TransportIds)
            .NotEmpty()
            .WithMessage("Список идентификаторов транспортных средств не должкен быть пустым!");
    }
}
using FluentValidation;
using Prolog.Application.Drivers.Commands;

namespace Prolog.Application.Drivers.Validators;

internal class ArchiveDriverCommandValidator: AbstractValidator<ArchiveDriverCommand>
{
    public ArchiveDriverCommandValidator()
    {
        RuleFor(x => x.DriverIds)
            .NotEmpty()
            .WithMessage("Список идентификаторов водетелей не должен быть пустым!");
    }
}
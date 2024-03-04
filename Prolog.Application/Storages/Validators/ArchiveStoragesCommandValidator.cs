using FluentValidation;
using Prolog.Application.Storages.Commands;

namespace Prolog.Application.Storages.Validators;

internal class ArchiveStoragesCommandValidator: AbstractValidator<ArchiveStoragesCommand>
{
    public ArchiveStoragesCommandValidator()
    {
        RuleFor(x => x.StorageIds)
            .NotEmpty()
            .WithMessage("Список идентификаторов складов не должен быть пустым!");
    }
}
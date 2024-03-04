using FluentValidation;
using Prolog.Application.Storages.Commands;

namespace Prolog.Application.Storages.Validators;

internal class ArchiveStorageCommandValidator: AbstractValidator<ArchiveStorageCommand>
{
    public ArchiveStorageCommandValidator()
    {
        RuleFor(x => x.StorageId)
            .NotEmpty()
            .WithMessage("Идентификатор склада является обязательным параметром!");
    }
}
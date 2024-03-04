using FluentValidation;
using Prolog.Application.Storages.Commands;

namespace Prolog.Application.Storages.Validators;

internal class UpdateStorageCommandValidator: AbstractValidator<UpdateStorageCommand>
{
    public UpdateStorageCommandValidator()
    {
        RuleFor(x => x.StorageId)
            .NotEmpty()
            .WithMessage("Идентификатор склада является обязательным параметром!");

        RuleFor(x => x.Body)
            .NotNull()
            .WithMessage("Тело запроса не может быть пустым!");

        RuleFor(x => x.Body.Name)
            .NotEmpty()
            .WithMessage("Наименование склада является обязательным полем!");

        RuleFor(x => x.Body.Address)
            .NotEmpty()
            .WithMessage("Адрес склада является обязательным полем!");
    }
}
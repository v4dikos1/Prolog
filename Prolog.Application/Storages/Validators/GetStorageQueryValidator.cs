using FluentValidation;
using Prolog.Application.Storages.Queries;

namespace Prolog.Application.Storages.Validators;

internal class GetStorageQueryValidator: AbstractValidator<GetStorageQuery>
{
    public GetStorageQueryValidator()
    {
        RuleFor(x => x.StorageId)
            .NotEmpty()
            .WithMessage("Идентификатор склада является обязательным параметром!");
    }
}
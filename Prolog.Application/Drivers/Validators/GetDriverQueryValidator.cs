using FluentValidation;
using Prolog.Application.Drivers.Queries;

namespace Prolog.Application.Drivers.Validators;

internal class GetDriverQueryValidator: AbstractValidator<GetDriverQuery>
{
    public GetDriverQueryValidator()
    {
        RuleFor(x => x.DriverId)
            .NotEmpty()
            .WithMessage("Идентификатор водителя является обязательным параметром!");
    }
}
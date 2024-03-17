using FluentValidation;
using Prolog.Application.Transports.Queries;

namespace Prolog.Application.Transports.Validators;

internal class GetTransportQueryValidator: AbstractValidator<GetTransportQuery>
{
    public GetTransportQueryValidator()
    {
        RuleFor(x => x.TransportId)
            .NotEmpty()
            .WithMessage("Идентификатор транспортного средства является обязательным парамтером!");
    }
}
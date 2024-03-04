using FluentValidation;
using Prolog.Application.Clients.Queries;

namespace Prolog.Application.Clients.Validators;

internal class GetCustomerQueryValidator: AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Идентификатор клиента является обязательным параметром!");
    }
}
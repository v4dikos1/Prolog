using FluentValidation;
using Prolog.Application.Clients.Commands;

namespace Prolog.Application.Clients.Validators;

internal class UpdateCustomerCommandValidator: AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Идентификатор клиента является обязательным параметром!");

        RuleFor(x => x.Body)
            .NotNull()
            .WithMessage("Тело запроса не должно быть пустым!");

        RuleFor(x => x.Body.Name)
            .NotEmpty()
            .WithMessage("Имя клиента является обязательным параметром!");

        RuleFor(x => x.Body.Name)
            .NotEmpty()
            .WithMessage("Фамилия клиента является обязательным параметром!");

        RuleFor(x => x.Body.PhoneNumber)
            .NotEmpty()
            .WithMessage("Номер телефона клиента является обязательным параметром!");

        RuleFor(x => x.Body.Email)
            .NotEmpty()
            .WithMessage("Почта клиента является обязательным параметром!");
    }
}
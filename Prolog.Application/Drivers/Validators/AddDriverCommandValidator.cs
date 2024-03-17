using FluentValidation;
using Prolog.Application.Drivers.Commands;

namespace Prolog.Application.Drivers.Validators;

internal class AddDriverCommandValidator: AbstractValidator<AddDriverCommand>
{
    public AddDriverCommandValidator()
    {
        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage("Тело запроса не должно быть пустым!");

        RuleFor(x => x.Body.Name)
            .NotEmpty()
            .WithMessage("Имя водителя является обязательным параметром!");

        RuleFor(x => x.Body.Surname)
            .NotEmpty()
            .WithMessage("Фамилия водителя является обязательным параметром!");

        RuleFor(x => x.Body.PhoneNumber)
            .NotEmpty()
            .WithMessage("Номер телефона водителя является обязательным параметром!");

        RuleFor(x => x.Body.Salary)
            .NotEmpty()
            .WithMessage("Ставка водителя является обязательным параметром!");

        RuleFor(x => x.Body.Telegram)
            .NotEmpty()
            .WithMessage("Телеграм контакт водителя является обязательным параметром!");
    }
}
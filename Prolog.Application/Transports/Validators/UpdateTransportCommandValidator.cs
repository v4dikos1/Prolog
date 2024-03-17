using FluentValidation;
using Prolog.Application.Transports.Commands;

namespace Prolog.Application.Transports.Validators;

internal class UpdateTransportCommandValidator: AbstractValidator<UpdateTransportCommand>
{
    public UpdateTransportCommandValidator()
    {
        RuleFor(x => x.TransportId)
            .NotEmpty()
            .WithMessage("Идентификатор транспорта является обязательным параметром!");

        RuleFor(x => x.Body)
            .NotEmpty()
            .WithMessage("Тело запроса не должно быть пустым!");

        RuleFor(x => x.Body.Brand)
            .NotEmpty()
            .WithMessage("Марка автомобиля является обязательным параметром!");

        RuleFor(x => x.Body.LicencePlate)
            .NotEmpty()
            .WithMessage("Номерной знак является обязательным параметром!");

        RuleFor(x => x.Body.Capacity)
            .NotEmpty()
            .WithMessage("Грузоподъемность является обязательным параметром!");

        RuleFor(x => x.Body.FuelConsumption)
            .NotEmpty()
            .WithMessage("Расход топлива является обязательным параметром!");

        RuleFor(x => x.Body.Volume)
            .NotEmpty()
            .WithMessage("Объем является обязательным параметром!");
    }
}
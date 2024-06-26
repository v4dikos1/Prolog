﻿using FluentValidation;
using Prolog.Application.Orders.Commands;

namespace Prolog.Application.Orders.Validators;

internal class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Body)
            .NotNull()
            .WithMessage("Тело запроса не должно быть пустым!");

        RuleFor(x => x.Body.StorageId)
            .NotEmpty()
            .WithMessage("Идентификатор склада является обязательным параметром!");

        RuleFor(x => x.Body.CustomerId)
            .NotEmpty()
            .WithMessage("Идентификатор клиента является обязательным параметром!");

        RuleFor(x => x.Body.Address)
            .NotEmpty()
            .WithMessage("Адрес доставки является обязательным параметром!");

        RuleFor(x => x.Body.Price)
            .NotEmpty()
            .WithMessage("Цена доставки является обязательным параметром!");

        RuleFor(x => x.Body.PickUpDateFrom)
            .NotEmpty()
            .WithMessage("Дата забора \"c\" является обязательным параметром!");
        
        RuleFor(x => x.Body.PickUpDateTo)
            .NotEmpty()
            .WithMessage("Дата забора \"до\" является обязательным параметром!");

        RuleFor(x => x.Body.Products)
            .NotEmpty()
            .WithMessage("Список идентификаторов товаров не должен быть пустым!");
    }
}
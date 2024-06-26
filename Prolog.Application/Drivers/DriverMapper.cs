﻿using Mapster;
using Prolog.Application.Drivers.Dtos;
using Prolog.Application.Orders.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Drivers;

[Mapper]
public interface IDriverMapper
{
    Driver MapToEntity((UpdateDriverModel model, Guid externalSystemId) src);
    Driver MapExisted(UpdateDriverModel model, Driver driver);
    DriverListViewModel MapToListViewModel(Driver driver);
    OrderDriverViewModel MapToOrderDriverViewModel(DriverTransportBind bind);
}

internal class DriverMapRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateDriverModel Model, Guid ExternalSystemId), Driver>()
            .Map(d => d.PhoneNumber, src => src.Model.PhoneNumber.ToLower())
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.Surname, src => src.Model.Surname)
            .Map(d => d.Patronymic, src => src.Model.Patronymic)
            .Map(d => d.PhoneNumber, src => src.Model.PhoneNumber)
            .Map(d => d.Telegram, src => src.Model.Telegram)
            .Map(d => d.Salary, src => src.Model.Salary)
            .Map(d => d.ExternalSystemId, src => src.ExternalSystemId);

        config.NewConfig<(UpdateDriverModel Model, Driver Existed), Driver>()
            .Map(d => d.PhoneNumber, src => src.Model.PhoneNumber.ToLower())
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.Surname, src => src.Model.Surname)
            .Map(d => d.Patronymic, src => src.Model.Patronymic)
            .Map(d => d.PhoneNumber, src => src.Model.PhoneNumber)
            .Map(d => d.Telegram, src => src.Model.Telegram)
            .Map(d => d.Salary, src => src.Model.Salary)
            .Map(d => d.ExternalSystemId, src => src.Existed.ExternalSystemId);

        config.NewConfig<Driver, DriverListViewModel>()
            .Map(d => d.PhoneNumber, src => src.PhoneNumber)
            .Map(d => d.Id, src => src.Id)
            .Map(d => d.Name, src => src.Name)
            .Map(d => d.Surname, src => src.Surname)
            .Map(d => d.Patronymic, src => src.Patronymic)
            .Map(d => d.Salary, src => src.Salary)
            .Map(d => d.Telegram, src => src.Telegram);

        config.NewConfig<DriverTransportBind, OrderDriverViewModel>()
            .Map(d => d.DriverId, src => src.Driver.Id)
            .Map(d => d.Name, src => src.Driver.Name)
            .Map(d => d.PhoneNumber, src => src.Driver.PhoneNumber)
            .Map(d => d.LicencePlate, src => src.Transport.LicencePlate)
            .Map(d => d.TransportId, src => src.Transport.Id)
            .Map(d => d.StartDate, src => src.StartDate)
            .Map(d => d.EndDate, src => src.EndDate)
            .Map(d => d.TotalOrdersCount, src => src.TotalOrdersCount)
            .Map(d => d.OrdersCompletedCount, src => src.OrdersCompletedCount)
            .Map(d => d.Distance, src => src.Distance);
    }
}
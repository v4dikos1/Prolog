using Mapster;
using Prolog.Application.Clients.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Clients;

[Mapper]
public interface IClientMapper
{
    Customer MapToEntity((CreateCustomerModel model, Guid externalSystemId) src);
    Customer MapExisted(UpdateCustomerModel model, Customer customer);
    CustomerListViewModel MapToListViewModel(Customer customer);
    CustomerViewModel MapToViewModel(Customer customer);
}

internal class CustomerMapRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateCustomerModel Model, Guid ExternalSystemId), Customer>()
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.PhoneNumber, src => src.Model.PhoneNumber)
            .Map(d => d.ExternalSystemId, src => src.ExternalSystemId);

        config.NewConfig<(UpdateCustomerModel Model, Customer Existed), Customer>()
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.PhoneNumber, src => src.Model.PhoneNumber)
            .Map(d => d.ExternalSystemId, src => src.Existed.ExternalSystemId);

        config.NewConfig<Customer, CustomerListViewModel>()
            .Map(d => d.PhoneNumber, src => src.PhoneNumber)
            .Map(d => d.Name, src => src.Name)
            .Map(d => d.Id, src => src.Id);

        config.NewConfig<Customer, CustomerViewModel>()
            .Map(d => d.PhoneNumber, src => src.PhoneNumber)
            .Map(d => d.Name, src => src.Name)
            .Map(d => d.Id, src => src.Id);
    }
}
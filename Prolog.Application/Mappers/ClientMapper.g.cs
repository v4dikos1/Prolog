using System;
using Prolog.Application.Clients;
using Prolog.Application.Clients.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Clients
{
    public partial class ClientMapper : IClientMapper
    {
        public Customer MapToEntity(ValueTuple<CreateCustomerModel, Guid> p1)
        {
            return new Customer()
            {
                ExternalSystemId = p1.Item2,
                Name = p1.Item1.Name,
                PhoneNumber = p1.Item1.PhoneNumber
            };
        }
        public Customer MapExisted(UpdateCustomerModel p2, Customer p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Customer result = p3 ?? new Customer();
            
            result.Name = p2.Name;
            result.PhoneNumber = p2.PhoneNumber;
            return result;
            
        }
        public CustomerListViewModel MapToListViewModel(Customer p4)
        {
            return p4 == null ? null : new CustomerListViewModel()
            {
                Id = p4.Id,
                Name = p4.Name,
                PhoneNumber = p4.PhoneNumber
            };
        }
        public CustomerViewModel MapToViewModel(Customer p5)
        {
            return p5 == null ? null : new CustomerViewModel()
            {
                Id = p5.Id,
                Name = p5.Name,
                PhoneNumber = p5.PhoneNumber
            };
        }
    }
}
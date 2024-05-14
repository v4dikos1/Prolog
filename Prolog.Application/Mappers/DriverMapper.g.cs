using System;
using Prolog.Application.Drivers;
using Prolog.Application.Drivers.Dtos;
using Prolog.Application.Orders.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Drivers
{
    public partial class DriverMapper : IDriverMapper
    {
        public Driver MapToEntity(ValueTuple<UpdateDriverModel, Guid> p1)
        {
            return new Driver()
            {
                ExternalSystemId = p1.Item2,
                Salary = (decimal)p1.Item1.Salary,
                Name = p1.Item1.Name,
                Surname = p1.Item1.Surname,
                Patronymic = p1.Item1.Patronymic,
                PhoneNumber = p1.Item1.PhoneNumber.ToLower(),
                Telegram = p1.Item1.Telegram
            };
        }
        public Driver MapExisted(UpdateDriverModel p2, Driver p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Driver result = p3 ?? new Driver();
            
            result.Salary = (decimal)p2.Salary;
            result.Name = p2.Name;
            result.Surname = p2.Surname;
            result.Patronymic = p2.Patronymic;
            result.PhoneNumber = p2.PhoneNumber;
            result.Telegram = p2.Telegram;
            return result;
            
        }
        public DriverListViewModel MapToListViewModel(Driver p4)
        {
            return p4 == null ? null : new DriverListViewModel()
            {
                Id = p4.Id,
                Name = p4.Name,
                Surname = p4.Surname,
                Patronymic = p4.Patronymic,
                PhoneNumber = p4.PhoneNumber,
                Telegram = p4.Telegram,
                Salary = (long)p4.Salary
            };
        }
        public OrderDriverViewModel MapToOrderDriverViewModel(DriverTransportBind p5)
        {
            return p5 == null ? null : new OrderDriverViewModel()
            {
                DriverId = funcMain1(p5.Driver == null ? null : (Guid?)p5.Driver.Id),
                Name = p5.Driver == null ? null : p5.Driver.Name,
                PhoneNumber = p5.Driver == null ? null : p5.Driver.PhoneNumber,
                LicencePlate = p5.Transport == null ? null : p5.Transport.LicencePlate,
                TransportId = funcMain2(p5.Transport == null ? null : (Guid?)p5.Transport.Id),
                StartDate = p5.StartDate,
                EndDate = p5.EndDate,
                TotalOrdersCount = p5.TotalOrdersCount,
                OrdersCompletedCount = p5.OrdersCompletedCount,
                Distance = p5.Distance
            };
        }
        
        private Guid funcMain1(Guid? p6)
        {
            return p6 == null ? default(Guid) : (Guid)p6;
        }
        
        private Guid funcMain2(Guid? p7)
        {
            return p7 == null ? default(Guid) : (Guid)p7;
        }
    }
}
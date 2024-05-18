using System;
using Prolog.Abstractions.CommonModels.PrologBotService;
using Prolog.Application.Orders;
using Prolog.Application.Orders.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Orders
{
    public partial class OrderMapper : IOrderMapper
    {
        public OrderListViewModel MapToViewModel(Order p1)
        {
            return p1 == null ? null : new OrderListViewModel()
            {
                Id = p1.Id,
                VisibleId = p1.Id.ToString("D8"),
                Client = funcMain1(new ClientOrderViewModel()
                {
                    Address = p1.Address.AddressFullName,
                    ClientId = p1.CustomerId,
                    ClientName = p1.Customer.Name,
                    ClientPhone = p1.Customer.PhoneNumber,
                    Coordinates = p1.Coordinates
                }),
                Storage = funcMain2(new StorageOrderViewModel()
                {
                    StorageId = p1.StorageId,
                    StorageCoordinates = p1.Storage.Coordinates,
                    StorageName = p1.Storage.Name
                }),
                Price = p1.Price,
                Volume = (decimal)0,
                Weight = (decimal)0,
                Amount = 0,
                PickUpStartDate = p1.PickUpDateFrom,
                PickUpEndDate = p1.PickUpDateTo,
                DeliveryStartDate = (DateTimeOffset?)p1.DeliveryDateFrom,
                DeliveryEndDate = (DateTimeOffset?)p1.DeliveryDateTo,
                DateDelivered = p1.DateDelivered,
                Status = p1.OrderStatus
            };
        }
        public RouteViewModel MapToViewModel(ProblemSolution p4)
        {
            return p4 == null ? null : new RouteViewModel()
            {
                Index = p4.Index,
                Id = p4.LocationId,
                Longitude = p4.Longitude,
                Latitude = p4.Latitude,
                StopType = p4.StopType
            };
        }
        public OrderBotViewModel MapToBotViewModel(Order p5)
        {
            return p5 == null ? null : new OrderBotViewModel()
            {
                Id = p5.Id,
                VisibleId = p5.Id.ToString("D8"),
                ClientName = p5.Customer == null ? null : p5.Customer.Name,
                ClientPhone = p5.Customer == null ? null : p5.Customer.PhoneNumber,
                Address = p5.Address == null ? null : p5.Address.AddressFullName,
                StorageName = p5.Storage == null ? null : p5.Storage.Name,
                StorageAddress = p5.Storage == null ? null : (p5.Storage.Address == null ? null : p5.Storage.Address.AddressFullName),
                PickUpStartDate = p5.PickUpDateFrom,
                PickUpEndDate = p5.PickUpDateTo,
                DeliveryStartDate = (DateTimeOffset?)p5.DeliveryDateFrom,
                DeliveryEndDate = (DateTimeOffset?)p5.DeliveryDateTo
            };
        }
        
        private ClientOrderViewModel funcMain1(ClientOrderViewModel p2)
        {
            return p2 == null ? null : new ClientOrderViewModel()
            {
                ClientId = p2.ClientId,
                ClientName = p2.ClientName,
                ClientPhone = p2.ClientPhone,
                Address = p2.Address,
                Coordinates = p2.Coordinates
            };
        }
        
        private StorageOrderViewModel funcMain2(StorageOrderViewModel p3)
        {
            return p3 == null ? null : new StorageOrderViewModel()
            {
                StorageId = p3.StorageId,
                StorageCoordinates = p3.StorageCoordinates,
                StorageName = p3.StorageName
            };
        }
    }
}
using Mapster;
using Prolog.Application.Orders.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Orders;

[Mapper]
public interface IOrderMapper
{
    OrderListViewModel MapToViewModel(Order order);
    RouteViewModel MapToViewModel(ProblemSolution problemSolution);
}

internal class OrderMapRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderListViewModel>()
            .Map(d => d.Id, src => src.Id)
            .Map(d => d.VisibleId, src => src.Id.ToString("D8"))
            .Map(d => d.Client,
                src => new ClientOrderViewModel
                {
                    Address = src.Address.AddressFullName,
                    ClientId = src.CustomerId,
                    ClientName = src.Customer.Name,
                    ClientPhone = src.Customer.PhoneNumber,
                    Coordinates = src.Coordinates
                })
            .Map(d => d.Storage,
                src => new StorageOrderViewModel
                {
                    StorageId = src.StorageId,
                    StorageCoordinates = src.Storage.Coordinates,
                    StorageName = src.Storage.Name
                })
            .Map(d => d.Price, src => src.Price)
            .Map(d => d.Volume, src => 0)
            .Map(d => d.Weight, src => 0)
            .Map(d => d.Amount, src => 0)
            .Map(d => d.PickUpStartDate, src => src.PickUpDateFrom)
            .Map(d => d.PickUpEndDate, src => src.PickUpDateTo)
            .Map(d => d.DeliveryStartDate, src => src.DeliveryDateFrom)
            .Map(d => d.DeliveryEndDate, src => src.DeliveryDateTo)
            .Map(d => d.DateDelivered, src => src.DateDelivered)
            .Map(d => d.Status, src => src.OrderStatus);

        config.NewConfig<ProblemSolution, RouteViewModel>()
            .Map(d => d.Id, src => src.LocationId)
            .Map(d => d.Index, src => src.Index)
            .Map(d => d.StopType, src => src.StopType)
            .Map(d => d.Latitude, src => src.Latitude)
            .Map(d => d.Longitude, src => src.Longitude);
    }
}
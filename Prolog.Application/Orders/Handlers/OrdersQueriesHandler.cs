using MediatR;
using Prolog.Application.Orders.Dtos;
using Prolog.Application.Orders.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Domain.Enums;

namespace Prolog.Application.Orders.Handlers;

internal class OrdersQueriesHandler: IRequestHandler<GetOrdersListQuery, PagedResult<OrderListGroupedByDateViewModel>>
{
    public async Task<PagedResult<OrderListGroupedByDateViewModel>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        if (request.Status == OrderFilterStatusEnum.Incoming)
        {
            return new PagedResult<OrderListGroupedByDateViewModel>
            {
                TotalItems = 6,
                ItemsOffset = 0,
                ItemsQuantity = 6,
                Items = new List<OrderListGroupedByDateViewModel>
                {
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-06-03"),
                        OrderCount = 3,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver = null,
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 1,
                                        VisibleId = "00000001",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T09:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T09:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T10:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T10:30+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 2,
                                        VisibleId = "00000002",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 2",
                                        ClientPhone = "+78005553536",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T12:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T15:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T15:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 3,
                                        VisibleId = "00000003",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 3",
                                        ClientPhone = "+78005553537",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T15:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T15:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T19:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T19:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-08-03"),
                        OrderCount = 3,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver = null,
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 6,
                                        VisibleId = "00000006",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-08-03T09:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-08-03T09:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-08-03T10:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-08-03T10:30+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 7,
                                        VisibleId = "00000007",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 2",
                                        ClientPhone = "+78005553536",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-08-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-08-03T12:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-08-03T15:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-08-03T15:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 8,
                                        VisibleId = "00000008",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 3",
                                        ClientPhone = "+78005553537",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-08-03T15:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-08-03T15:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-08-03T19:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-08-03T19:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        if (request.Status == OrderFilterStatusEnum.Active)
        {
            return new PagedResult<OrderListGroupedByDateViewModel>
            {
                TotalItems = 2,
                ItemsOffset = 0,
                ItemsQuantity = 2,
                Items = new List<OrderListGroupedByDateViewModel>
                {
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-06-03"),
                        OrderCount = 2,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver =
                                    new OrderDriverViewModel
                                    {
                                        DriverId = Guid.NewGuid(),
                                        Name = "Водитель Водитель Водитель 2",
                                        LicencePlate = "B111АА24",
                                        PhoneNumber = "+78005553535",
                                        TransportId = Guid.NewGuid(),
                                        Distance = 5M,
                                        StartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:04+07:00"),
                                        EndDate =
                                            DateTimeOffset.Parse("2024-06-03T22:00+07:00"),
                                        TotalOrdersCount = 2,
                                        OrdersCompletedCount = 1
                                    },
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 4,
                                        VisibleId = "00000004",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 2",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T12:30+07:00"),
                                        DatePickedUp =
                                            DateTimeOffset.Parse("2024-06-03T12:25+07:00"),
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T14:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T14:30+07:00"),
                                        DateDelivered =
                                            DateTimeOffset.Parse("2024-06-03T14:27+07:00"),
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 5,
                                        VisibleId = "00000005",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T19:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T19:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T21:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T21:40+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        if (request.Status == OrderFilterStatusEnum.Completed)
        {
            return new PagedResult<OrderListGroupedByDateViewModel>
            {
                TotalItems = 2,
                ItemsOffset = 0,
                ItemsQuantity = 2,
                Items = new List<OrderListGroupedByDateViewModel>
                {
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-05-03"),
                        OrderCount = 2,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver =
                                    new OrderDriverViewModel
                                    {
                                        DriverId = Guid.NewGuid(),
                                        Name = "Водитель Водитель Водитель 2",
                                        LicencePlate = "B111АА24",
                                        PhoneNumber = "+78005553535",
                                        TransportId = Guid.NewGuid(),
                                        Distance = 5M,
                                        StartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:04+07:00"),
                                        EndDate =
                                            DateTimeOffset.Parse("2024-06-03T22:00+07:00"),
                                        TotalOrdersCount = 2,
                                        OrdersCompletedCount = 1
                                    },
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 9,
                                        VisibleId = "00000009",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 2",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-05-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-05-03T12:30+07:00"),
                                        DatePickedUp =
                                            DateTimeOffset.Parse("2024-05-03T12:25+07:00"),
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-05-03T14:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-05-03T14:30+07:00"),
                                        DateDelivered =
                                            DateTimeOffset.Parse("2024-05-03T14:27+07:00"),
                                        Status = OrderStatusEnum.Delivered
                                    },
                                    new()
                                    {
                                        Id = 10,
                                        VisibleId = "00000010",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-05-03T19:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-05-03T19:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-05-03T21:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-05-03T21:40+07:00"),
                                        DateDelivered = null,
                                        Status = OrderStatusEnum.Cancelled
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        else
        {
            return new PagedResult<OrderListGroupedByDateViewModel>
            {
                TotalItems = 10,
                ItemsOffset = 0,
                ItemsQuantity = 10,
                Items = new List<OrderListGroupedByDateViewModel>
                {
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-06-03"),
                        OrderCount = 3,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver = null,
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 1,
                                        VisibleId = "00000001",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T09:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T09:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T10:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T10:30+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 2,
                                        VisibleId = "00000002",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 2",
                                        ClientPhone = "+78005553536",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T12:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T15:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T15:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 3,
                                        VisibleId = "00000003",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 3",
                                        ClientPhone = "+78005553537",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T15:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T15:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T19:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T19:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-08-03"),
                        OrderCount = 3,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver = null,
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 6,
                                        VisibleId = "00000006",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-08-03T09:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-08-03T09:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-08-03T10:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-08-03T10:30+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 7,
                                        VisibleId = "00000007",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 2",
                                        ClientPhone = "+78005553536",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-08-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-08-03T12:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-08-03T15:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-08-03T15:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 8,
                                        VisibleId = "00000008",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 3",
                                        ClientPhone = "+78005553537",
                                        Price = 550.58M,
                                        Volume = 5M,
                                        Weight = 0.2M,
                                        Amount = 1,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-08-03T15:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-08-03T15:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-08-03T19:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-08-03T19:50+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-06-03"),
                        OrderCount = 2,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver =
                                    new OrderDriverViewModel
                                    {
                                        DriverId = Guid.NewGuid(),
                                        Name = "Водитель Водитель Водитель 2",
                                        LicencePlate = "B111АА24",
                                        PhoneNumber = "+78005553535",
                                        TransportId = Guid.NewGuid(),
                                        Distance = 5M,
                                        StartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:04+07:00"),
                                        EndDate =
                                            DateTimeOffset.Parse("2024-06-03T22:00+07:00"),
                                        TotalOrdersCount = 2,
                                        OrdersCompletedCount = 1
                                    },
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 4,
                                        VisibleId = "00000004",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 2",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T12:30+07:00"),
                                        DatePickedUp =
                                            DateTimeOffset.Parse("2024-06-03T12:25+07:00"),
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T14:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T14:30+07:00"),
                                        DateDelivered =
                                            DateTimeOffset.Parse("2024-06-03T14:27+07:00"),
                                        Status = null
                                    },
                                    new()
                                    {
                                        Id = 5,
                                        VisibleId = "00000005",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-06-03T19:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-06-03T19:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-06-03T21:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-06-03T21:40+07:00"),
                                        DateDelivered = null,
                                        Status = null
                                    }
                                }
                            }
                        }
                    },
                    new()
                    {
                        OrderDate = DateTime.Parse("2024-05-03"),
                        OrderCount = 2,
                        OrdersGroupedByDriver = new List<OrderListGroupedByDriverViewModel>
                        {
                            new()
                            {
                                Driver =
                                    new OrderDriverViewModel
                                    {
                                        DriverId = Guid.NewGuid(),
                                        Name = "Водитель Водитель Водитель 2",
                                        LicencePlate = "B111АА24",
                                        PhoneNumber = "+78005553535",
                                        TransportId = Guid.NewGuid(),
                                        Distance = 5M,
                                        StartDate =
                                            DateTimeOffset.Parse("2024-06-03T12:04+07:00"),
                                        EndDate =
                                            DateTimeOffset.Parse("2024-06-03T22:00+07:00"),
                                        TotalOrdersCount = 2,
                                        OrdersCompletedCount = 1
                                    },
                                Orders = new List<OrderListViewModel>
                                {
                                    new()
                                    {
                                        Id = 9,
                                        VisibleId = "00000009",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 2",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-05-03T12:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-05-03T12:30+07:00"),
                                        DatePickedUp =
                                            DateTimeOffset.Parse("2024-05-03T12:25+07:00"),
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-05-03T14:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-05-03T14:30+07:00"),
                                        DateDelivered =
                                            DateTimeOffset.Parse("2024-05-03T14:27+07:00"),
                                        Status = OrderStatusEnum.Delivered
                                    },
                                    new()
                                    {
                                        Id = 10,
                                        VisibleId = "00000010",
                                        Address = "Красноярск, ул. Киренского д.32",
                                        StorageId = Guid.NewGuid(),
                                        StorageName = "Склад 1",
                                        ClientId = Guid.NewGuid(),
                                        ClientName = "Клиент 1",
                                        ClientPhone = "+78005553535",
                                        Price = 150,
                                        Volume = 0.5M,
                                        Weight = 0.2M,
                                        Amount = 2,
                                        PickUpStartDate =
                                            DateTimeOffset.Parse("2024-05-03T19:00+07:00"),
                                        PickUpEndDate =
                                            DateTimeOffset.Parse("2024-05-03T19:30+07:00"),
                                        DatePickedUp = null,
                                        DeliveryStartDate =
                                            DateTimeOffset.Parse("2024-05-03T21:00+07:00"),
                                        DeliveryEndDate =
                                            DateTimeOffset.Parse("2024-05-03T21:40+07:00"),
                                        DateDelivered = null,
                                        Status = OrderStatusEnum.Cancelled
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
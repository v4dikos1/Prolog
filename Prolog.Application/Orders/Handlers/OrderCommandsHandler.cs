using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Query;
using Prolog.Abstractions.CommonModels.MapBoxService;
using Prolog.Abstractions.Services;
using Prolog.Application.Addresses.Mappers;
using Prolog.Application.Orders.Commands;
using Prolog.Core.Exceptions;
using Prolog.Domain;
using Prolog.Domain.Entities;
using Prolog.Domain.Enums;
using System.Globalization;

namespace Prolog.Application.Orders.Handlers;

internal class OrderCommandsHandler(ICurrentHttpContextAccessor contextAccessor, ApplicationDbContext dbContext,
    IExternalSystemService externalSystemService, IDaDataService daDataService, IAddressMapper addressMapper, IMapBoxService mapBoxService):
    IRequestHandler<CreateOrderCommand, CreatedOrUpdatedEntityViewModel<long>>, IRequestHandler<PlanOrdersCommand>,
    IRequestHandler<RetrieveSolutionCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = (await externalSystemService.GetExternalSystemWithCheckExistsAsync(
            Guid.Parse(contextAccessor.IdentityUserId!),
            cancellationToken)).IdentityId;
      
        var existingStorage = await dbContext.Storages
            .Where(x => x.Id == request.Body.StorageId)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Склад с идентификатором {request.Body.StorageId} не найден!");
       
        var existingCustomer = await dbContext.Customers
            .Where(x => x.Id == request.Body.CustomerId)
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Клиент с идентификатором {request.Body.CustomerId} не найден!");

        var productsDictionary = request.Body.Products
            .ToDictionary(key => key.ProductId, value => value);
        var existingProducts = await dbContext.Products
            .Where(x => productsDictionary.Keys.Contains(x.Id))
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .ToListAsync(cancellationToken);

        var existingProductIds = existingProducts.Select(x => x.Id).ToList();
        var notFoundProducts = productsDictionary.Keys.Where(x => !existingProductIds.Contains(x)).ToList();
        if (notFoundProducts.Any())
        {
            throw new ObjectNotFoundException(
                $"Не найдены товары с идентификаторами: {string.Join(", ", notFoundProducts)}!");
        }

        var address = await GetAddress(request.Body.Address);
        var coordinates = await daDataService.GetCoordinatesByAddress(address.AddressFullName);

        var orderToCreate = new Order
        {
            ExternalSystemId = externalSystemId,
            CustomerId = existingCustomer.Id,
            StorageId = existingStorage.Id,
            Address = address,
            Coordinates = string.Join(" ", coordinates.Latitude, coordinates.Longitude),
            DeliveryDateFrom = request.Body.DeliveryDateFrom.ToUniversalTime(),
            DeliveryDateTo = request.Body.DeliveryDateTo.ToUniversalTime(),
            PickUpDateFrom = request.Body.PickUpDateFrom.ToUniversalTime(),
            PickUpDateTo = request.Body.PickUpDateTo.ToUniversalTime(),
            Price = request.Body.Price,
            OrderStatus = OrderStatusEnum.Incoming
        };
        var orderItemsToCreate = new List<OrderItem>();
        foreach (var existingProduct in existingProducts)
        {
            var orderItemToCreate = new OrderItem
            {
                Order = orderToCreate,
                ProductId = existingProduct.Id,
                Weight = existingProduct.Weight,
                Volume = existingProduct.Volume,
                Price = existingProduct.Price,
                Count = productsDictionary[existingProduct.Id].Count
            };
            orderItemsToCreate.Add(orderItemToCreate);
        }

        var createdOrder = await dbContext.AddAsync(orderToCreate, cancellationToken);
        await dbContext.AddRangeAsync(orderItemsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatedOrUpdatedEntityViewModel<long>(createdOrder.Entity.Id);
    }

    public async Task Handle(PlanOrdersCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = (await externalSystemService.GetExternalSystemWithCheckExistsAsync(
            Guid.Parse(contextAccessor.IdentityUserId!),
            cancellationToken)).IdentityId;

        var existingStorage = await dbContext.Storages
            .Where(x => x.Id == request.Body.StorageId)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Склад с идентификатором {request.Body.StorageId} не найден!");

        var driverIds = request.Body.Binds.Select(x => x.DriverId).Distinct().ToList();
        var existingDrivers = await dbContext.Drivers
            .Where(x => driverIds.Contains(x.Id))
            .Where(x => x.ExternalSystemId == externalSystemId)
            .ToListAsync(cancellationToken);
        var notFoundDrivers = driverIds.Where(x => !existingDrivers.Select(d => d.Id).Contains(x)).ToList();
        if (notFoundDrivers.Any())
        {
            throw new ObjectNotFoundException(
                $"Не найдены водители с идентификаторами: {string.Join(", ", notFoundDrivers)}!");
        }

        var transportIds = request.Body.Binds.Select(x => x.TransportId).Distinct().ToList();
        var existingTransports = await dbContext.Transports
            .Where(x => transportIds.Contains(x.Id))
            .Where(x => x.ExternalSystemId == externalSystemId)
            .ToListAsync(cancellationToken);
        var notFoundTransports = transportIds.Where(x => !existingTransports.Select(t => t.Id).Contains(x)).ToList();
        if (notFoundTransports.Any())
        {
            throw new ObjectNotFoundException(
                $"Не найдены транспортные средства с идентификаторами: {string.Join(", ", notFoundTransports)}!");
        }

        var startDate = request.Body.StartDate.ToUniversalTime();
        var endDate = request.Body.EndDate.ToUniversalTime();

        var ordersToPlan = await dbContext.Orders
            .Include(order => order.Items)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => x.OrderStatus == OrderStatusEnum.Incoming)
            .Where(x => startDate <= x.DeliveryDateFrom && endDate >= x.DeliveryDateTo)
            .ToListAsync(cancellationToken);

        var problemRequest = new SubmitProblemRequest
        {
            Locations = ordersToPlan.Select(x => new LocationModel
            {
                Name = x.Id.ToString(),
                Coordinates = x.Coordinates.Split(' ').Reverse().Select(c => double.Parse(c, CultureInfo.InvariantCulture)).ToList()
            }).ToList(),
            Shipments = ordersToPlan.Select(x => new ShipmentModel
            {
                Name = x.Id.ToString(),
                From = existingStorage.Id.ToString(),
                To = x.Id.ToString(),
                Size = new ShipmentSizeModel
                {
                    Boxes = x.Items.Sum(i => i.Count),
                    Volume = (long)x.Items.Sum(i => i.Volume),
                    Weight = (long)x.Items.Sum(i => i.Weight)
                }
            }).ToList(),
            Vehicles = existingTransports.Select(x => new VehicleModel
            {
                Name = x.Id.ToString(),
                Capacities = new CapacityModel
                {
                    Boxes = (long)x.Capacity,
                    Volume = (long)x.Volume,
                    Weight = (long)x.Capacity
                },
                EarliestStart = request.Body.StartDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"),
                LatestEnd = request.Body.EndDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'")
            }).ToList()
        };
        problemRequest.Locations.Add(new LocationModel
        {
            Name = existingStorage.Id.ToString(),
            Coordinates = existingStorage.Coordinates.Split(' ').Reverse()
                .Select(c => double.Parse(c, CultureInfo.InvariantCulture)).ToList()
        });

        var problem = await mapBoxService.SubmitProblemAsync(problemRequest, cancellationToken);

        foreach (var order in ordersToPlan)
        {
            order.OrderStatus = OrderStatusEnum.Active;
            order.ProblemId = problem.Id;
            order.StorageId = existingStorage.Id;
        }

        var bindsToCreate = new List<DriverTransportBind>();
        foreach (var bind in request.Body.Binds)
        {
            var driverTransportBind = new DriverTransportBind
            {
                DriverId = bind.DriverId,
                TransportId = bind.TransportId,
                ProblemId = problem.Id,
                StartDate = request.Body.StartDate.ToUniversalTime(),
                EndDate = request.Body.EndDate.ToUniversalTime()
            };
            bindsToCreate.Add(driverTransportBind);
        }

        await dbContext.AddRangeAsync(bindsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(RetrieveSolutionCommand request, CancellationToken cancellationToken)
    {
        var ordersToRetrieve = await dbContext.Orders
            .Where(x => x.OrderStatus == OrderStatusEnum.Active)
            .Where(x => x.ProblemId.HasValue)
            .Where(x => !x.IsArchive)
            .ToListAsync(cancellationToken);

        var problemIds = ordersToRetrieve.Select(x => x.ProblemId!.Value).Distinct().ToList();
        var solutionLists = new List<ProblemSolutionResponse>();
        var problemSolutionsToCreate = new List<ProblemSolution>();
        foreach (var problemId in problemIds)
        {
            var solution =
                await mapBoxService.RetrieveSolutionAsync(new RetrieveSolutionRequest { Id = problemId }, cancellationToken);
            solutionLists.Add(solution);

            var index = 0;
            foreach (var stop in solution.Routes.SelectMany(x => x.Stops))
            {
                var problemSolution = new ProblemSolution
                {
                    ProblemId = problemId,
                    LocationId = stop.LocationId,
                    Index = index,
                    StopType = stop.Type == "pickup" ? StopTypeEnum.Storage : StopTypeEnum.Client,
                    Latitude = stop.LocationMetadata.Coordinates[1].ToString(CultureInfo.InvariantCulture),
                    Longitude = stop.LocationMetadata.Coordinates[0].ToString(CultureInfo.InvariantCulture)
                };
                index += 1;
                problemSolutionsToCreate.Add(problemSolution);
            }
        }
        await dbContext.AddRangeAsync(problemSolutionsToCreate, cancellationToken);

        var solutionDictionary = solutionLists.SelectMany(x => x.Routes)
            .ToDictionary(key => key.VehicleId, value => value.Stops.Select(x => x.LocationId).ToList());

        var driverTransportBinds = await dbContext.DriverTransportBinds
            .Where(x => problemIds.Contains(x.ProblemId))
            .ToListAsync(cancellationToken);

        foreach (var order in ordersToRetrieve)
        {
            order.OrderStatus = OrderStatusEnum.Planned;
            var vehicleId = solutionDictionary.Single(x => x.Value.Contains(order.Id.ToString())).Key;
            var bind = driverTransportBinds.Single(x => x.TransportId == vehicleId);
            order.DriverTransportBindId = bind.Id;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<Address> GetAddress(string addressQuery)
    {
        var queryModel = new AddressQueryModel
        {
            Locations = null,
            FromBound = null,
            ToBound = null,
            Query = addressQuery,
            RestrictValue = true
        };
        var address = (await daDataService.GetListSuggestionAddressByQuery(queryModel)).FirstOrDefault();
        var result = addressMapper.MapToAddress(address);
        return result;
    }
}
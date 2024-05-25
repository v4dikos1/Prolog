using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prolog.Abstractions.CommonModels;
using Prolog.Abstractions.CommonModels.DaDataService;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Query;
using Prolog.Abstractions.CommonModels.MapBoxService;
using Prolog.Abstractions.Services;
using Prolog.Application.Addresses.Mappers;
using Prolog.Application.Orders.Commands;
using Prolog.Application.Orders.Dtos;
using Prolog.Core.EntityFramework.Extensions;
using Prolog.Core.Exceptions;
using Prolog.Domain;
using Prolog.Domain.Entities;
using Prolog.Domain.Enums;
using System.Globalization;

namespace Prolog.Application.Orders.Handlers;

internal class OrderCommandsHandler(ICurrentHttpContextAccessor contextAccessor, ApplicationDbContext dbContext,
    IExternalSystemService externalSystemService, IDaDataService daDataService, IAddressMapper addressMapper, IMapBoxService mapBoxService, ILogger<OrderCommandsHandler> logger):
    IRequestHandler<CreateOrderCommand, CreatedOrUpdatedEntityViewModel<long>>, IRequestHandler<PlanOrdersCommand>,
    IRequestHandler<RetrieveSolutionCommand>, IRequestHandler<CompleteOrderCommand>, IRequestHandler<ArchiveOrdersCommand>, IRequestHandler<CancelOrdersCommand>
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
        var coordinates = new CoordinatesResponseModel();
        try
        {
            coordinates = await daDataService.GetCoordinatesByAddress(address.AddressFullName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex.InnerException);
        }

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

        var storageIds = request.Body.Binds.Select(x => x.StorageId).Distinct().ToList();
        var existingStorages = await dbContext.Storages
            .Where(x => storageIds.Contains(x.Id))
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .ToListAsync(cancellationToken);
        var notFoundStorages = storageIds.Where(x => !existingStorages.Select(d => d.Id).Contains(x)).ToList();
        if (notFoundStorages.Any())
        {
            throw new ObjectNotFoundException(
                $"Не найдены склады с идентификаторами: {string.Join(", ", notFoundStorages)}!");
        }

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
            .WhereIf(request.Body.OrderIds != null, x => request.Body.OrderIds!.Contains(x.Id))
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
                From = x.StorageId.ToString(),
                To = x.Id.ToString(),
                Size = new ShipmentSizeModel
                {
                    Boxes = x.Items.Sum(i => i.Count),
                    Volume = (long)x.Items.Sum(i => i.Volume),
                    Weight = (long)x.Items.Sum(i => i.Weight)
                },
                PickUpTimes = new List<PickUpTimeModel>
                {
                    new()
                    {
                        Earliest = x.PickUpDateFrom,
                        Latest = x.PickUpDateTo
                    }
                },
                DropOffTimes = new List<DropOffTimeModel>
                {
                    new()
                    {
                        Earliest = x.DeliveryDateFrom,
                        Latest = x.DeliveryDateTo
                    }
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

        foreach (var existingStorage in existingStorages)
        {
            problemRequest.Locations.Add(new LocationModel
            {
                Name = existingStorage.Id.ToString(),
                Coordinates = existingStorage.Coordinates.Split(' ').Reverse()
                    .Select(c => double.Parse(c, CultureInfo.InvariantCulture)).ToList()
            });
        }
        
        var problem = await mapBoxService.SubmitProblemAsync(problemRequest, cancellationToken);

        foreach (var order in ordersToPlan)
        {
            order.OrderStatus = OrderStatusEnum.Active;
            order.ProblemId = problem.Id;
        }

        var bindsToCreate = new List<DriverTransportBind>();
        foreach (var bind in request.Body.Binds)
        {
            var driverTransportBind = new DriverTransportBind
            {
                DriverId = bind.DriverId,
                TransportId = bind.TransportId,
                ProblemId = problem.Id,
                StorageId = bind.StorageId,
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
            try
            {
                var solution =
                    await mapBoxService.RetrieveSolutionAsync(new RetrieveSolutionRequest { Id = problemId }, cancellationToken);
                solutionLists.Add(solution);

                foreach (var solutionRoute in solution.Routes)
                {
                    var index = 0;
                    foreach (var stop in solutionRoute.Stops)
                    {
                        var problemSolution = new ProblemSolution
                        {
                            VehicleId = solutionRoute.VehicleId,
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
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e.Message);
            }
        }
        await dbContext.AddRangeAsync(problemSolutionsToCreate, cancellationToken);

        var routes = solutionLists.SelectMany(x => x.Routes);
        var solutionDictionary = routes
            .GroupBy(key => key.VehicleId)
            .ToDictionary(key => key.Key, value => value.SelectMany(x => x.Stops).Select(x => x.LocationId).ToList());

        var driverTransportBinds = await dbContext.DriverTransportBinds
            .Where(x => problemIds.Contains(x.ProblemId))
            .ToListAsync(cancellationToken);

        foreach (var order in ordersToRetrieve)
        {
            var vehicleId = solutionDictionary.SingleOrDefault(x => x.Value.Contains(order.Id.ToString())).Key;
            if (vehicleId != Guid.Empty)
            {
                order.OrderStatus = OrderStatusEnum.Planned;
                var bind = driverTransportBinds.Single(x => x.TransportId == vehicleId && x.ProblemId == order.ProblemId);
                order.DriverTransportBindId = bind.Id;
            }
            else
            {
                order.OrderStatus = OrderStatusEnum.Incoming;
                order.ProblemId = null;
            }
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

    public async Task Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToComplete = await dbContext.Orders
            .Where(x => x.Id == request.OrderId)
            .SingleOrDefaultAsync(cancellationToken)
        ?? throw new ObjectNotFoundException($"Заявка с идентификатором {request.OrderId} не найдена!");

        orderToComplete.OrderStatus = OrderStatusEnum.Completed;
        orderToComplete.DateDelivered = DateTimeOffset.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(ArchiveOrdersCommand request, CancellationToken cancellationToken)
    {
        var ordersToArchive = await dbContext.Orders
            .Where(x => !x.IsArchive)
            .Where(x => request.OrderIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var problemIds = ordersToArchive
            .Where(x => x.ProblemId.HasValue)
            .Select(x => x.ProblemId!.Value)
            .Distinct().ToList();

        var ordersToPlanList = await dbContext.Orders
            .Where(x => x.OrderStatus == OrderStatusEnum.Planned)
            .Where(x => x.ProblemId.HasValue && problemIds.Contains(x.ProblemId.Value))
            .Where(x => !request.OrderIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var ordersToPlanGroupedByProblem = ordersToPlanList.GroupBy(x => x.ProblemId!.Value)
            .ToDictionary(key => key.Key, value => value.Select(x => x));
        var driverTransportBinds = await dbContext.DriverTransportBinds
            .Where(x => problemIds.Contains(x.ProblemId))
            .GroupBy(key => key.ProblemId)
            .ToDictionaryAsync(key => key.Key, 
                value => value.Select(x => x),
                cancellationToken);

        var commandsToSend = new List<PlanOrdersCommand>();
        foreach (var problemId in ordersToPlanGroupedByProblem.Keys)
        {
            var orders = ordersToPlanList.Where(x => x.ProblemId.HasValue && x.ProblemId!.Value == problemId);
            var ordersToPlanIds = orders.Select(x => x.Id).ToList();
            var binds = driverTransportBinds[problemId].Select(x => new DriverTransportBindModel
            {
                StorageId = x.StorageId,
                DriverId = x.DriverId,
                TransportId = x.TransportId
            });
            var planOrdersCommand = new PlanOrdersCommand
            {
                Body = new PlanOrdersModel
                {
                    Binds = binds,
                    OrderIds = ordersToPlanIds,
                    EndDate = driverTransportBinds[problemId].First().EndDate,
                    StartDate = driverTransportBinds[problemId].First().StartDate
                }
            };
            commandsToSend.Add(planOrdersCommand);
        }

        foreach (var order in ordersToPlanList)
        {
            order.ProblemId = null;
            order.OrderStatus = OrderStatusEnum.Incoming;
            order.DriverTransportBindId = null;
        }

        foreach (var order in ordersToArchive)
        {
            order.IsArchive = true;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        foreach (var command in commandsToSend)
        {
            await Handle(command, cancellationToken);
        }
    }

    public async Task Handle(CancelOrdersCommand request, CancellationToken cancellationToken)
    {
        var ordersToCancel = await dbContext.Orders
            .Where(x => !x.IsArchive)
            .Where(x => request.OrderIds.Contains(x.Id))
            .Where(x => x.OrderStatus == OrderStatusEnum.Planned)
            .ToListAsync(cancellationToken);

        var problemIds = ordersToCancel
            .Where(x => x.ProblemId.HasValue)
            .Select(x => x.ProblemId!.Value)
            .Distinct().ToList();

        var ordersToPlan = await dbContext.Orders
            .Where(x => x.OrderStatus == OrderStatusEnum.Planned)
            .Where(x => x.ProblemId.HasValue && problemIds.Contains(x.ProblemId.Value))
            .Where(x => !request.OrderIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var ordersToPlanGroupedByProblem = ordersToPlan.GroupBy(x => x.ProblemId!.Value)
            .ToDictionary(key => key.Key, value => value.Select(x => x));
        var driverTransportBinds = await dbContext.DriverTransportBinds
            .Where(x => problemIds.Contains(x.ProblemId))
            .GroupBy(key => key.ProblemId)
            .ToDictionaryAsync(key => key.Key,
                value => value.Select(x => x),
                cancellationToken);

        var commandsToSend = new List<PlanOrdersCommand>();
        foreach (var problemId in ordersToPlanGroupedByProblem.Keys)
        {
            var orders = ordersToPlan.Where(x => x.ProblemId!.Value == problemId);
            var ordersToPlanIds = orders.Select(x => x.Id).ToList();
            var binds = driverTransportBinds[problemId].Select(x => new DriverTransportBindModel
            {
                StorageId = x.StorageId,
                DriverId = x.DriverId,
                TransportId = x.TransportId
            });
            var planOrdersCommand = new PlanOrdersCommand
            {
                Body = new PlanOrdersModel
                {
                    Binds = binds,
                    OrderIds = ordersToPlanIds,
                    EndDate = driverTransportBinds[problemId].First().EndDate,
                    StartDate = driverTransportBinds[problemId].First().StartDate
                }
            };
            commandsToSend.Add(planOrdersCommand);
        }

        foreach (var order in ordersToPlan)
        {
            order.ProblemId = null;
            order.OrderStatus = OrderStatusEnum.Incoming;
            order.DriverTransportBindId = null;
        }

        foreach (var order in ordersToCancel)
        {
            order.ProblemId = null;
            order.DriverTransportBindId = null;
            order.OrderStatus = OrderStatusEnum.Incoming;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        foreach (var command in commandsToSend)
        {
            await Handle(command, cancellationToken);
        }
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Abstractions.Services;
using Prolog.Application.Drivers;
using Prolog.Application.Orders.Dtos;
using Prolog.Application.Orders.Queries;
using Prolog.Core.EntityFramework.Extensions;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Domain;
using Prolog.Domain.Enums;

namespace Prolog.Application.Orders.Handlers;

internal class OrdersQueriesHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor,
    IExternalSystemService externalSystemService, IOrderMapper orderMapper, IDriverMapper driverMapper):
    IRequestHandler<GetOrdersListQuery, PagedResult<OrderListGroupedByDateViewModel>>
{
    public async Task<PagedResult<OrderListGroupedByDateViewModel>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = (await externalSystemService.GetExternalSystemWithCheckExistsAsync(
            Guid.Parse(contextAccessor.IdentityUserId!),
            cancellationToken)).IdentityId;

        var orders = await dbContext.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .Include(x => x.Customer)
            .Include(x => x.Storage)
            .Include(x => x.DriverTransportBind)
            .ThenInclude(b => b!.Driver)
            .Include(x => x.DriverTransportBind)
            .ThenInclude(b => b!.Transport)
            .WhereIf(request.Status == OrderFilterStatusEnum.Incoming, x => x.OrderStatus == OrderStatusEnum.Incoming)
            .WhereIf(request.Status == OrderFilterStatusEnum.Active, x => x.OrderStatus == OrderStatusEnum.Planned)
            .WhereIf(request.Status == OrderFilterStatusEnum.Completed, x => x.OrderStatus == OrderStatusEnum.Completed)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .ToListAsync(cancellationToken);

        var problemIds = orders.Select(x => x.ProblemId).Distinct().ToList();
        var problemSolutions = await dbContext.ProblemSolutions
            .Where(x => problemIds.Contains(x.ProblemId))
            .ToListAsync(cancellationToken);

        var driversDictionary = orders
            .Where(x => x.DriverTransportBindId.HasValue)
            .Select(x => x.DriverTransportBind)
            .DistinctBy(x => x!.DriverId)
            .ToDictionary(key => key!.Driver.Id, value => value);

        var ordersGroupedByDate = orders
            .GroupBy(key => key.DeliveryDateFrom.Date, value => value)
            .ToDictionary(key => key.Key, value => value.Select(x => x));

        var orderModels = new List<OrderListGroupedByDateViewModel>();
        foreach (var orderGroup in ordersGroupedByDate)
        {
            var ordersGroupedByDriver = orderGroup.Value
                .GroupBy(x => x.DriverTransportBind?.DriverId)
                .ToDictionary(key => key.Key ?? Guid.Empty, value => value.Select(x => x));

            var ordersGroupedByDriverList = new List<OrderListGroupedByDriverViewModel>();
            foreach (var orderGroupByDriver in ordersGroupedByDriver)
            {
                var ordersProblemIds = orderGroupByDriver.Value.Where(x => x.ProblemId.HasValue).Select(x => x.ProblemId).Distinct();
                var problems = problemSolutions.Where(x => ordersProblemIds.Contains(x.ProblemId)).ToList();
                var orderGroupedByDriver = new OrderListGroupedByDriverViewModel
                {
                    Driver = orderGroupByDriver.Key != Guid.Empty ? 
                        driverMapper.MapToOrderDriverViewModel(driversDictionary[orderGroupByDriver.Key]!) : null,
                    Orders = orderGroupByDriver.Value.Select(orderMapper.MapToViewModel),
                    Routes = request.Status != OrderFilterStatusEnum.Incoming ? problems.Select(orderMapper.MapToViewModel) : new List<RouteViewModel>()
                };
                ordersGroupedByDriverList.Add(orderGroupedByDriver);
            }

            var orderModel = new OrderListGroupedByDateViewModel
            {
                OrderDate = orderGroup.Key,
                OrderCount = orderGroup.Value.Count(),
                OrdersGroupedByDriver = ordersGroupedByDriverList
            };

            orderModels.Add(orderModel);
        }

        return new PagedResult<OrderListGroupedByDateViewModel>
        {
            TotalItems = orders.Count,
            ItemsOffset = request.Offset ?? 0,
            ItemsQuantity = request.Limit ?? orders.Count,
            Items = orderModels
        };
    }
}
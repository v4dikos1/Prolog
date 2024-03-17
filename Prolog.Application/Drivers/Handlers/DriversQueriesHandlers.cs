using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Drivers.Dtos;
using Prolog.Application.Drivers.Queries;
using Prolog.Core.EntityFramework.Features.SearchPagination;
using Prolog.Core.EntityFramework.Features.SearchPagination.Models;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Drivers.Handlers;

internal class DriversQueriesHandlers(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IDriverMapper driverMapper):
    IRequestHandler<GetDriversListQuery, PagedResult<DriverListViewModel>>,
    IRequestHandler<GetDriverQuery, DriverListViewModel>
{
    public async Task<PagedResult<DriverListViewModel>> Handle(GetDriversListQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var driversQuery = dbContext.Drivers
            .AsNoTracking()
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Surname)
            .ApplySearch(request, x => x.Name, x => x.Surname, x => x.Patronymic, x => x.PhoneNumber, x => x.Telegram);

        var driversList = await driversQuery
            .ApplyPagination(request)
            .ToListAsync(cancellationToken);

        var result = driversList.Select(driverMapper.MapToListViewModel);
        return result.AsPagedResult(request, await driversQuery.CountAsync(cancellationToken));
    }

    public async Task<DriverListViewModel> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var existingDriver = await dbContext.Drivers
            .AsNoTracking()
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => x.Id == request.DriverId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Водитель с идентификатором {request.DriverId} не найден!");

        return driverMapper.MapToListViewModel(existingDriver);
    }
}
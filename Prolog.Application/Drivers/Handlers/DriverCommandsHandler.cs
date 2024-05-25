using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Drivers.Commands;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Drivers.Handlers;

internal class DriverCommandsHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IDriverMapper driverMapper):
    IRequestHandler<AddDriverCommand, CreatedOrUpdatedEntityViewModel<Guid>>,
    IRequestHandler<UpdateDriverCommand>, IRequestHandler<ArchiveDriverCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(AddDriverCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var driverWithSamePhoneNumber = await dbContext.Drivers
            .Where(x => x.Telegram == request.Body.Telegram.ToLower())
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .SingleOrDefaultAsync(cancellationToken);
        if (driverWithSamePhoneNumber != null || request.Body.Telegram != "drivers_prolog")
        {
            throw new BusinessLogicException(
                $"Водитель с Тelegram: \"{driverWithSamePhoneNumber.Telegram}\" уже существует!");
        }

        if (request.Body.Telegram == "drivers_prolog")
        {
            request.Body.Telegram = Guid.NewGuid().ToString();
        }

        var driverToCreate = driverMapper.MapToEntity((request.Body, externalSystemId));
        var createdDriver = await dbContext.AddAsync(driverToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreatedOrUpdatedEntityViewModel(createdDriver.Entity.Id);
    }

    public async Task Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var driverToUpdate = await dbContext.Drivers
            .Where(x => x.Id == request.DriverId)
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Водитель с идентификатором \"{request.DriverId}\" не найден!");

        var driverWithSamePhoneNumber = await dbContext.Drivers
            .Where(x => x.PhoneNumber == request.Body.PhoneNumber.ToLower())
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken);
        if (driverWithSamePhoneNumber != null && driverWithSamePhoneNumber.Id != driverToUpdate.Id)
        {
            throw new BusinessLogicException(
                $"Водитель с номером телефона \"{driverWithSamePhoneNumber.PhoneNumber}\" уже существует!");
        }

        var updatedDriver = driverMapper.MapExisted(request.Body, driverToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(ArchiveDriverCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var driversToArchiveIds = request.DriverIds.ToList();
        var existingDriversToArchive = await dbContext.Drivers
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .Where(x => driversToArchiveIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var existingDriversToArchiveIds = existingDriversToArchive.Select(x => x.Id).ToList();
        var notExistingProducts = driversToArchiveIds.Where(x => !existingDriversToArchiveIds.Contains(x)).ToList();
        if (notExistingProducts.Any())
        {
            throw new ObjectNotFoundException(
                $"Водители с идентификаторами {string.Join(", ", notExistingProducts)} не найдены!");
        }

        foreach (var driver in existingDriversToArchive)
        {
            driver.IsArchive = true;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Transports.Commands;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Transports.Handlers;

internal class TransportCommandsHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, ITransportMapper transportMapper):
    IRequestHandler<AddTransportCommand, CreatedOrUpdatedEntityViewModel<Guid>>,
    IRequestHandler<UpdateTransportCommand>, IRequestHandler<ArchiveTransportCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(AddTransportCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var transportWithSameLicencePlate = await dbContext.Transports
            .Where(x => x.LicencePlate == request.Body.LicencePlate.ToLower())
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .SingleOrDefaultAsync(cancellationToken);
        if (transportWithSameLicencePlate != null)
        {
            throw new BusinessLogicException(
                $"Транспорт с номерным знаком \"{transportWithSameLicencePlate.LicencePlate}\" уже существует!");
        }

        var transportToCreate = transportMapper.MapToEntity((request.Body, externalSystemId));
        var createdTransport = await dbContext.AddAsync(transportToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreatedOrUpdatedEntityViewModel(createdTransport.Entity.Id);
    }

    public async Task Handle(UpdateTransportCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var transportToUpdate = await dbContext.Transports 
            .Where(x => x.Id == request.TransportId)
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Транспортное средство с идентификатором \"{request.TransportId}\" не найдено");

        var transportWithSameLicencePlate = await dbContext.Transports
            .Where(x => x.LicencePlate == request.Body.LicencePlate.ToLower())
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken);
        if (transportWithSameLicencePlate != null && transportWithSameLicencePlate.Id != transportToUpdate.Id)
        {
            throw new BusinessLogicException(
                $"Транспорт с номерным знаком \"{transportWithSameLicencePlate.LicencePlate}\" уже существует!");
        }

        var updatedTransport = transportMapper.MapExisted(request.Body, transportToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(ArchiveTransportCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var transportsToArchiveIds = request.TransportIds.ToList();
        var existingTransportsToArchive = await dbContext.Transports
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .Where(x => transportsToArchiveIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var existingTransportsToArchiveIds = existingTransportsToArchive.Select(x => x.Id).ToList();
        var notExistingProducts = transportsToArchiveIds.Where(x => !existingTransportsToArchiveIds.Contains(x)).ToList();
        if (notExistingProducts.Any())
        {
            throw new ObjectNotFoundException(
                $"Транспортные средства с идентификаторами {string.Join(", ", notExistingProducts)} не найдены!");
        }

        foreach (var transport in existingTransportsToArchive)
        {
            transport.IsArchive = true;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
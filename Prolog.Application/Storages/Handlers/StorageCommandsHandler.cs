using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Storages.Commands;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Storages.Handlers;

internal class StorageCommandsHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IStorageMapper storageMapper):
    IRequestHandler<CreateStorageCommand, CreatedOrUpdatedEntityViewModel<Guid>>, IRequestHandler<UpdateStorageCommand>,
    IRequestHandler<ArchiveStoragesCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var storageToCreate = storageMapper.MapToEntity((request.Body, externalSystemId));
        var createdStorage = await dbContext.AddAsync(storageToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreatedOrUpdatedEntityViewModel(createdStorage.Entity.Id);
    }

    public async Task Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var storageToUpdate = await dbContext.Storages
            .Where(x => x.Id == request.StorageId)
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Склад с идентификатором \"{request.StorageId}\" не найден!");

        var updatedStorage = storageMapper.MapExisted(request.Body, storageToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(ArchiveStoragesCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var storagesToArchiveIds = request.StorageIds.ToList();
        var existingStoragesToArchive = await dbContext.Storages
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => !x.IsArchive)
            .Where(x => storagesToArchiveIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var existingStoragesToArchiveIds = existingStoragesToArchive.Select(x => x.Id).ToList();
        var notExistingProducts = storagesToArchiveIds.Where(x => !existingStoragesToArchiveIds.Contains(x)).ToList();
        if (notExistingProducts.Any())
        {
            throw new ObjectNotFoundException(
                $"Склады с идентификаторами {string.Join(", ", notExistingProducts)} не найдены!");
        }

        foreach (var storage in existingStoragesToArchive)
        {
            storage.IsArchive = true;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
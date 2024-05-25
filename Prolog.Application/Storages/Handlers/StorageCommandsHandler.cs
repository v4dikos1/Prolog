using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Query;
using Prolog.Abstractions.Services;
using Prolog.Application.Addresses.Mappers;
using Prolog.Application.Storages.Commands;
using Prolog.Core.Exceptions;
using Prolog.Domain;
using Prolog.Domain.Entities;

namespace Prolog.Application.Storages.Handlers;

internal class StorageCommandsHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor,
    IStorageMapper storageMapper, IDaDataService daDataService, IAddressMapper addressMapper):
    IRequestHandler<CreateStorageCommand, CreatedOrUpdatedEntityViewModel<Guid>>, IRequestHandler<UpdateStorageCommand>,
    IRequestHandler<ArchiveStoragesCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var storageToCreate = storageMapper.MapToEntity((request.Body, externalSystemId));
        var storageAddress = await GetAddress(request.Body.Address);
        storageToCreate.Address = storageAddress;

        var coordinates = await daDataService.GetCoordinatesByAddress(storageAddress.AddressFullName);
        storageToCreate.Coordinates = string.Join(" ", coordinates.Latitude, coordinates.Longitude);

        var createdStorage = await dbContext.AddAsync(storageToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreatedOrUpdatedEntityViewModel(createdStorage.Entity.Id);
    }

    public async Task Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);
        var storageAddress = await GetAddress(request.Body.Address);
        var storageToUpdate = await dbContext.Storages
            .Where(x => x.Id == request.StorageId)
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Склад с идентификатором \"{request.StorageId}\" не найден!");
        storageToUpdate.Address = storageAddress;
        var coordinates = await daDataService.GetCoordinatesByAddress(storageAddress.AddressFullName);
        storageToUpdate.Coordinates = string.Join(" ", coordinates.Latitude, coordinates.Longitude);
        var updatedStorage = storageMapper.MapExisted((request.Body, storageToUpdate));
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
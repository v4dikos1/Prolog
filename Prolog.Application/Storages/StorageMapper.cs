using Mapster;
using Prolog.Application.Storages.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Storages;

[Mapper]
public interface IStorageMapper
{
    Storage MapToEntity((CreateStorageModel model, Guid externalSystemId) src);
    Storage MapExisted(CreateStorageModel model, Storage storage);
    StorageListViewModel MapToListViewModel(Storage src);
    StorageViewModel MapToViewModel(Storage src);
}

public class StorageMapRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateStorageModel Model, Guid ExternalSystemId), Storage>()
            .Map(d => d.Address, src => src.Model.Address.ToLower())
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.ExternalSystemId, src => src.ExternalSystemId)
            .Map(d => d.Coordinates, src => string.Empty);

        config.NewConfig<(CreateStorageModel Model, Storage Storage), Storage>()
            .Map(d => d.Address, src => src.Model.Address.ToLower())
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.ExternalSystemId, src => src.Storage.ExternalSystemId)
            .Map(d => d.Coordinates, src => string.Empty);

        config.NewConfig<Storage, StorageListViewModel>()
            .Map(d => d.Name, src => src.Name)
            .Map(d => d.Address, src => src.Address)
            .Map(d => d.Id, src => src.Id);

        config.NewConfig<Storage, StorageViewModel>()
            .Map(d => d.Name, src => src.Name)
            .Map(d => d.Address, src => src.Address)
            .Map(d => d.Id, src => src.Id);
    }
}
using System;
using Prolog.Application.Storages;
using Prolog.Application.Storages.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Storages
{
    public partial class StorageMapper : IStorageMapper
    {
        public Storage MapToEntity(ValueTuple<CreateStorageModel, Guid> p1)
        {
            return new Storage()
            {
                ExternalSystemId = p1.Item2,
                Name = p1.Item1.Name,
                Coordinates = string.Empty
            };
        }
        public Storage MapExisted(ValueTuple<CreateStorageModel, Storage> p2)
        {
            return new Storage()
            {
                ExternalSystemId = p2.Item2.ExternalSystemId,
                Name = p2.Item1.Name,
                Coordinates = string.Empty
            };
        }
        public StorageListViewModel MapToListViewModel(Storage p3)
        {
            return p3 == null ? null : new StorageListViewModel()
            {
                Id = p3.Id,
                Name = p3.Name,
                Address = p3.Address == null ? null : p3.Address.AddressFullName
            };
        }
        public StorageViewModel MapToViewModel(Storage p4)
        {
            return p4 == null ? null : new StorageViewModel()
            {
                Id = p4.Id,
                Name = p4.Name,
                Address = p4.Address == null ? null : p4.Address.AddressFullName
            };
        }
    }
}
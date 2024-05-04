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
                Address = p1.Item1.Address,
                Coordinates = string.Empty
            };
        }
        public Storage MapExisted(CreateStorageModel p2, Storage p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Storage result = p3 ?? new Storage();
            
            result.Name = p2.Name;
            result.Address = p2.Address;
            return result;
            
        }
        public StorageListViewModel MapToListViewModel(Storage p4)
        {
            return p4 == null ? null : new StorageListViewModel()
            {
                Id = p4.Id,
                Name = p4.Name,
                Address = p4.Address
            };
        }
        public StorageViewModel MapToViewModel(Storage p5)
        {
            return p5 == null ? null : new StorageViewModel()
            {
                Id = p5.Id,
                Name = p5.Name,
                Address = p5.Address
            };
        }
    }
}
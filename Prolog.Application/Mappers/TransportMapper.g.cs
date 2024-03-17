using System;
using Prolog.Application.Transports;
using Prolog.Application.Transports.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Transports
{
    public partial class TransportMapper : ITransportMapper
    {
        public Transport MapToEntity(ValueTuple<UpdateTransportModel, Guid> p1)
        {
            return new Transport()
            {
                ExternalSystemId = p1.Item2,
                Volume = p1.Item1.Volume,
                Capacity = p1.Item1.Capacity,
                FuelConsumption = p1.Item1.FuelConsumption,
                LicencePlate = p1.Item1.LicencePlate.ToLower(),
                Brand = p1.Item1.Brand
            };
        }
        public Transport MapExisted(UpdateTransportModel p2, Transport p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Transport result = p3 ?? new Transport();
            
            result.Volume = p2.Volume;
            result.Capacity = p2.Capacity;
            result.FuelConsumption = p2.FuelConsumption;
            result.LicencePlate = p2.LicencePlate;
            result.Brand = p2.Brand;
            return result;
            
        }
        public TransportListViewModel MapToListViewModel(Transport p4)
        {
            return p4 == null ? null : new TransportListViewModel()
            {
                Id = p4.Id,
                Brand = p4.Brand,
                Volume = p4.Volume,
                Capacity = p4.Capacity,
                FuelConsumption = p4.FuelConsumption,
                LicencePlate = p4.LicencePlate
            };
        }
    }
}
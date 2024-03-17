using Mapster;
using Prolog.Application.Transports.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Transports;

[Mapper]
public interface ITransportMapper
{
    Transport MapToEntity((UpdateTransportModel Model, Guid ExternalSystemId) src); 
    Transport MapExisted(UpdateTransportModel model, Transport transport);

    TransportListViewModel MapToListViewModel(Transport transport);
}

internal class TransportMapRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateTransportModel Model, Guid ExternalSystemId), Transport>()
            .Map(d => d.Brand, src => src.Model.Brand)
            .Map(d => d.Capacity, src => src.Model.Capacity)
            .Map(d => d.Volume, src => src.Model.Volume)
            .Map(d => d.FuelConsumption, src => src.Model.FuelConsumption)
            .Map(d => d.LicencePlate, src => src.Model.LicencePlate.ToLower())
            .Map(d => d.ExternalSystemId, src => src.ExternalSystemId);

        config.NewConfig<(UpdateTransportModel Model, Transport Transport), Transport>()
            .Map(d => d.Brand, src => src.Model.Brand)
            .Map(d => d.Capacity, src => src.Model.Capacity)
            .Map(d => d.Volume, src => src.Model.Volume)
            .Map(d => d.FuelConsumption, src => src.Model.FuelConsumption)
            .Map(d => d.LicencePlate, src => src.Model.LicencePlate.ToLower())
            .Map(d => d.ExternalSystemId, src => src.Transport.ExternalSystemId);

        config.NewConfig<Transport, TransportListViewModel>()
            .Map(d => d.LicencePlate, src => src.LicencePlate)
            .Map(d => d.Brand, src => src.Brand)
            .Map(d => d.Capacity, src => src.Capacity)
            .Map(d => d.FuelConsumption, src => src.FuelConsumption)
            .Map(d => d.Id, src => src.Id)
            .Map(d => d.Volume, src => src.Volume);
    }
}
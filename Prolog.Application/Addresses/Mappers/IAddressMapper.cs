using Mapster;
using Prolog.Abstractions.CommonModels.DaDataService.Models.Response;
using Prolog.Application.Addresses.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Addresses.Mappers;

[Mapper]
public interface IAddressMapper
{
    public Address MapToAddress(SuggestionResponseModel model);
    public ViewAddressModel MapToViewAddress(Address data);
    public Address MapToAddress(ViewAddressModel model);
    public ViewAddressModel MapToViewAddress(SuggestionResponseModel model);
}
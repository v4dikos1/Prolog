using MediatR;
using Prolog.Application.Addresses.Dtos;

namespace Prolog.Application.Addresses.Queries;

public class SearchAddressByFullNameQuery : IRequest<IEnumerable<ViewAddressModel>>
{
    public required string Query { get; set; }
}
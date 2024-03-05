using MediatR;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Orders.Commands;

namespace Prolog.Application.Orders.Handlers;

internal class OrderCommandsHandler: IRequestHandler<CreateOrderCommand, CreatedOrUpdatedEntityViewModel<long>>
{
    public Task<CreatedOrUpdatedEntityViewModel<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
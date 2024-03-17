using MediatR;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Transports.Commands;

namespace Prolog.Application.Transports.Handlers;

internal class TransportCommandsHandler: IRequestHandler<AddTransportCommand, CreatedOrUpdatedEntityViewModel<Guid>>,
    IRequestHandler<UpdateTransportCommand>, IRequestHandler<ArchiveTransportCommand>
{
    public Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(AddTransportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Handle(UpdateTransportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Handle(ArchiveTransportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
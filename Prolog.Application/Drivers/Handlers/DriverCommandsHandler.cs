using MediatR;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Drivers.Commands;

namespace Prolog.Application.Drivers.Handlers;

internal class DriverCommandsHandler: IRequestHandler<AddDriverCommand, CreatedOrUpdatedEntityViewModel<Guid>>,
    IRequestHandler<UpdateDriverCommand>, IRequestHandler<ArchiveDriverCommand>
{
    public Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(AddDriverCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Handle(ArchiveDriverCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
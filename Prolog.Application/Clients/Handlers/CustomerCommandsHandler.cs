using MediatR;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Clients.Commands;
using Prolog.Domain;

namespace Prolog.Application.Clients.Handlers;

internal class CustomerCommandsHandler(ApplicationDbContext dbContext):
    IRequestHandler<CreateCustomerCommand, CreatedOrUpdatedEntityViewModel<Guid>>, IRequestHandler<ArchiveCustomerCommand>,
    IRequestHandler<ArchiveCustomersCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(ArchiveCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(ArchiveCustomersCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
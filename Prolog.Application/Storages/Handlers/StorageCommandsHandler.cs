using MediatR;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Storages.Commands;
using Prolog.Domain;

namespace Prolog.Application.Storages.Handlers;

internal class StorageCommandsHandler(ApplicationDbContext dbContext):
    IRequestHandler<CreateStorageCommand, CreatedOrUpdatedEntityViewModel<Guid>>, IRequestHandler<UpdateStorageCommand>,
    IRequestHandler<ArchiveStoragesCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(ArchiveStoragesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
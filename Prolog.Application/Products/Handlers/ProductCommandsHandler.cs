using MediatR;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Products.Commands;
using Prolog.Domain;

namespace Prolog.Application.Products.Handlers;

internal class ProductCommandsHandler(ApplicationDbContext dbContext):
    IRequestHandler<CreateProductCommand, CreatedOrUpdatedEntityViewModel<Guid>>, IRequestHandler<UpdateProductCommand>,
    IRequestHandler<ImportProductCommand>, IRequestHandler<ArchiveProductsCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(ImportProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(ArchiveProductsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
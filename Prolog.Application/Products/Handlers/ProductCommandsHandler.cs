using MediatR;
using Microsoft.EntityFrameworkCore;
using Prolog.Abstractions.CommonModels;
using Prolog.Application.Products.Commands;
using Prolog.Core.Exceptions;
using Prolog.Domain;

namespace Prolog.Application.Products.Handlers;

internal class ProductCommandsHandler(ApplicationDbContext dbContext, ICurrentHttpContextAccessor contextAccessor, IProductMapper productMapper):
    IRequestHandler<CreateProductCommand, CreatedOrUpdatedEntityViewModel<Guid>>, IRequestHandler<UpdateProductCommand>,
    IRequestHandler<ImportProductCommand>, IRequestHandler<ArchiveProductsCommand>
{
    public async Task<CreatedOrUpdatedEntityViewModel<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var productWithSameCode = await dbContext.Products
            .Where(x => x.Code == request.Body.Code)
            .Where(x => !x.IsArchive)
            .Where(x => x.ExternalSystemId == externalSystemId)
            .SingleOrDefaultAsync(cancellationToken);
        if (productWithSameCode != null)
        {
            throw new BusinessLogicException($"Товар с кодом \"{request.Body.Code}\" уже сушествует!");
        }

        var productToCreate = productMapper.MapToEntity((request.Body, externalSystemId));
        var createdProduct = await dbContext.AddAsync(productToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreatedOrUpdatedEntityViewModel(createdProduct.Entity.Id);
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var existingProduct = await dbContext.Products
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => x.Id == request.ProductId)
            .Where(x => !x.IsArchive)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new ObjectNotFoundException($"Товар с идентификатором {request.ProductId} не найден!");

        var productToUpdate = productMapper.MapExisted(request.Body, existingProduct);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(ImportProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(ArchiveProductsCommand request, CancellationToken cancellationToken)
    {
        var externalSystemId = Guid.Parse(contextAccessor.IdentityUserId!);

        var productsToArchiveIds = request.ProductIds.ToList();
        var existingProductsToArchive = await dbContext.Products
            .Where(x => x.ExternalSystemId == externalSystemId)
            .Where(x => productsToArchiveIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var existingProductsToArchiveIds = existingProductsToArchive.Select(x => x.Id).ToList();
        var notExistingProducts = productsToArchiveIds.Where(x => !existingProductsToArchiveIds.Contains(x)).ToList();
        if (notExistingProducts.Any())
        {
            throw new ObjectNotFoundException(
                $"Товары с идентификаторами {string.Join(", ", notExistingProducts)} не найдены!");
        }

        foreach (var product in existingProductsToArchive)
        {
            product.IsArchive = true;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
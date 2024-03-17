using Mapster;
using Prolog.Application.Products.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Products;

[Mapper]
public interface IProductMapper
{
    public Product MapToEntity((CreateProductModel model, Guid externalSystemId) src);
    public Product MapExisted(UpdateProductModel model, Product existedProduct);
    public ProductListViewModel MapToListViewModel(Product product);

    public ProductViewModel MapToViewModel(Product product);
}

internal class ProductMapRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateProductModel Model, Guid ExternalSystemId), Product>()
            .Map(d => d.Code, src => src.Model.Code)
            .Map(d => d.ExternalSystemId, src => src.ExternalSystemId)
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.Price, src => src.Model.Price)
            .Map(d => d.Volume, src => src.Model.Volume)
            .Map(d => d.Weight, src => src.Model.Weight);

        config.NewConfig<(UpdateProductModel Model, Product ExistedProduct), Product>()
            .Map(d => d.Code, src => src.ExistedProduct.Code)
            .Map(d => d.ExternalSystemId, src => src.ExistedProduct.ExternalSystemId)
            .Map(d => d.Name, src => src.Model.Name)
            .Map(d => d.Price, src => src.Model.Price)
            .Map(d => d.Volume, src => src.Model.Volume)
            .Map(d => d.Weight, src => src.Model.Weight);

        config.NewConfig<Product, ProductListViewModel>()
            .Map(d => d.Id, src => src.Id)
            .Map(d => d.Code, src => src.Code)
            .Map(d => d.Name, src => src.Name)
            .Map(d => d.Price, src => src.Price)
            .Map(d => d.Volume, src => src.Volume)
            .Map(d => d.Weight, src => src.Weight);

        config.NewConfig<Product, ProductViewModel>()
            .Map(d => d.Id, src => src.Id)
            .Map(d => d.Code, src => src.Code)
            .Map(d => d.Name, src => src.Name)
            .Map(d => d.Price, src => src.Price)
            .Map(d => d.Volume, src => src.Volume)
            .Map(d => d.Weight, src => src.Weight);
    }
}

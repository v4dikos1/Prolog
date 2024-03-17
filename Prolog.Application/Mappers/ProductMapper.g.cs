using System;
using Prolog.Application.Products;
using Prolog.Application.Products.Dtos;
using Prolog.Domain.Entities;

namespace Prolog.Application.Products
{
    public partial class ProductMapper : IProductMapper
    {
        public Product MapToEntity(ValueTuple<CreateProductModel, Guid> p1)
        {
            return new Product()
            {
                ExternalSystemId = p1.Item2,
                Code = p1.Item1.Code,
                Name = p1.Item1.Name,
                Weight = p1.Item1.Weight,
                Volume = p1.Item1.Volume,
                Price = (long)p1.Item1.Price
            };
        }
        public Product MapExisted(UpdateProductModel p2, Product p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Product result = p3 ?? new Product();
            
            result.Name = p2.Name;
            result.Weight = p2.Weight;
            result.Volume = p2.Volume;
            result.Price = (long)p2.Price;
            return result;
            
        }
        public ProductListViewModel MapToListViewModel(Product p4)
        {
            return p4 == null ? null : new ProductListViewModel()
            {
                Id = p4.Id,
                Code = p4.Code,
                Name = p4.Name,
                Weight = p4.Weight,
                Volume = p4.Volume,
                Price = (decimal)p4.Price
            };
        }
        public ProductViewModel MapToViewModel(Product p5)
        {
            return p5 == null ? null : new ProductViewModel()
            {
                Id = p5.Id,
                Code = p5.Code,
                Name = p5.Name,
                Weight = p5.Weight,
                Volume = p5.Volume,
                Price = (decimal)p5.Price
            };
        }
    }
}
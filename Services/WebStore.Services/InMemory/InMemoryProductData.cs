using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Servcies;
using WebStore.Services.Data;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            IEnumerable<Product> products = TestData.Products;
            if (Filter is null) return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Order = p.Order,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand is null ? null : new BrandDTO
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name
                }
            });
            if (Filter.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);
            if (Filter.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Order = p.Order,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand is null ? null : new BrandDTO
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name
                }
            }); 
        }

        public ProductDTO GetProductById(int id)
        {
            var product = TestData.Products.FirstOrDefault(p => p.Id == id);
            if (product is null) return null;
            return new ProductDTO
            {
                Id = product.Id,
                Order = product.Order,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand is null
                    ? null
                    : new BrandDTO
                    {
                        Id = product.Brand.Id,
                        Name = product.Brand.Name
                    }
            };
        }
    }
}

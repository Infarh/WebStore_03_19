using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Servcies;

namespace WebStore.Services.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _db;

        public SqlProductData(WebStoreContext DB)
        {
            _db = DB;
        }

        public IEnumerable<Section> GetSections() => _db.Sections.ToArray();

        public IEnumerable<Brand> GetBrands() => _db.Brands.ToArray();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            IQueryable<Product> products = _db.Products;
            if (Filter is null)
                return products.AsEnumerable().Select(p => new ProductDTO
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
                }).ToArray();
            if (Filter.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);

            if (Filter.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);

            return products.AsEnumerable().Select(p => new ProductDTO
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
            }).ToArray();
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _db.Products
               .Include(p => p.Brand)
               .Include(p => p.Section)
               .FirstOrDefault(p => p.Id == id);

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

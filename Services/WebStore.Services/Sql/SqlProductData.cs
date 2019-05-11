using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Servcies;
using WebStore.Services.Map;

namespace WebStore.Services.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _db;

        public SqlProductData(WebStoreContext db) => _db = db;

        public IEnumerable<Section> GetSections() => _db.Sections.ToArray();

        public IEnumerable<Brand> GetBrands() => _db.Brands.ToArray();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            IQueryable<Product> products = _db.Products;
            if (Filter?.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);

            return products.AsEnumerable().Select(ProductsMapper.ToDTO).ToArray();
        }

        public ProductDTO GetProductById(int id) =>
            _db.Products
               .Include(p => p.Brand)
               .Include(p => p.Section)
               .FirstOrDefault(p => p.Id == id)
               .ToDTO();
    }
}

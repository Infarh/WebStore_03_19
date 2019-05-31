using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Servcies;
using WebStore.Services.Data;
using WebStore.Services.Map;

namespace WebStore.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;
        public Section GetSectionById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public Brand GetBrandById(int id)
        {
            throw new System.NotImplementedException();
        }

        public PagedProductDTO GetProducts(ProductFilter Filter)
        {
            IEnumerable<Product> products = TestData.Products;

            if (Filter?.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);
            if (Filter?.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);

            var total_count = products.Count();

            if (Filter?.PageSize != null)
                products = products
                   .Skip((Filter.Page - 1) * (int) Filter.PageSize)
                   .Take((int) Filter.PageSize);

            return new PagedProductDTO
            {
                Products = products.Select(ProductsMapper.ToDTO),
                TotalCount = total_count
            }; 
        }

        public ProductDTO GetProductById(int id) => 
            TestData
               .Products
               .FirstOrDefault(p => p.Id == id)
               .ToDTO();
    }
}

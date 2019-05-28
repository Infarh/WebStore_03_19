using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Servcies;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, "api/products") { }

        public IEnumerable<Section> GetSections() => Get<List<Section>>($"{ServiceAddress}/sections");

        public Section GetSectionById(int id) => Get<Section>($"{ServiceAddress}/sections/{id}");

        public IEnumerable<Brand> GetBrands() => Get<List<Brand>>($"{ServiceAddress}/brands");

        public Brand GetBrandById(int id) => Get<Brand>($"{ServiceAddress}/brands/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter) => 
            Post(ServiceAddress, Filter)
               .Content
               .ReadAsAsync<List<ProductDTO>>()
               .Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{ServiceAddress}/{id}");
    }
}

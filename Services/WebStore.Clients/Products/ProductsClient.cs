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

        public IEnumerable<Section> GetSections()
        {
            return Get<List<Section>>($"{ServiceAddress}/sections");
        }

        public IEnumerable<Brand> GetBrands()
        {
            return Get<List<Brand>>($"{ServiceAddress}/brands");
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            var response = Post(ServiceAddress, Filter);
            return response.Content.ReadAsAsync<List<ProductDTO>>().Result;
        }

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{ServiceAddress}/{id}");
    }
}

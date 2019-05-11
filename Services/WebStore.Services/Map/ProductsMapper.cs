using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Map
{
    public static class ProductsMapper
    {
        public static void CopyTo(this Product product, ProductViewModel model)
        {
            model.Id = product.Id;
            model.Name = product.Name;
            model.Order = product.Order;
            model.ImageUrl = product.ImageUrl;
            model.Price = product.Price;
        }

        public static ProductViewModel CreateViewModel(this Product product)
        {
            var model = new ProductViewModel();
            product.CopyTo(model);
            return model;
        }

        public static void CopyTo(this ProductViewModel model, Product product)
        {
            product.Name = model.Name;
            product.Order = model.Order;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;
        }

        public static Product Create(this ProductViewModel model)
        {
            var product = new Product();
            model.CopyTo(product);
            return product;
        }

        public static ProductDTO ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.ToDTO()
        };

        public static Product FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.FromDTO()
        };
    }
}

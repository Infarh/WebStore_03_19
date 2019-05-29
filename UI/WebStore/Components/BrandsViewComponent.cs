using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;
using WebStore.Services.Map;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(string BrandId)
        {
            var brand_id = int.TryParse(BrandId, out var id) ? id : (int?) null;

            var brands = GetBrands();
            return View(new BrandCompleteViewModel
            {
                Brands = brands,
                CurrentBrandId = brand_id
            });
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _ProductData.GetBrands();
            return brands.Select(brand => brand.CreateModel());
        }
    }
}

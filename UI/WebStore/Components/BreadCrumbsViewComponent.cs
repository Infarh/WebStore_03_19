using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels.BreadCrumbs;
using WebStore.Interfaces.Servcies;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BreadCrumbsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(BreadCrumbsType Type, int id, BreadCrumbsType FromType)
        {
            if(!Enum.IsDefined(typeof(BreadCrumbsType), Type))
                throw new InvalidEnumArgumentException(nameof(Type), (int)Type, typeof(BreadCrumbsType));
            if (!Enum.IsDefined(typeof(BreadCrumbsType), FromType))
                throw new InvalidEnumArgumentException(nameof(FromType), (int)FromType, typeof(BreadCrumbsType));

            switch (Type)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Type), Type, null);
                case BreadCrumbsType.None:
                    break;
                case BreadCrumbsType.Section:
                    var section = _ProductData.GetSectionById(id);
                    return View(new []
                    {
                        new BreadCrumbsViewModel
                        {
                            BreadCrumbsType = Type,
                            Id = id.ToString(),
                            Name = section.Name
                        }
                    });
                case BreadCrumbsType.Brand:
                    var brand = _ProductData.GetBrandById(id);
                    return View(new []
                    {
                        new BreadCrumbsViewModel
                        {
                            BreadCrumbsType = Type,
                            Id = id.ToString(),
                            Name = brand.Name
                        }
                    });
                case BreadCrumbsType.Item:
                    break;
            }

            return View(new BreadCrumbsViewModel[0]);
        }

        private IEnumerable<BreadCrumbsViewModel> GetItemBreadCrumbs(
            int id,
            BreadCrumbsType FromType,
            BreadCrumbsType Type)
        {
            var item = _ProductData.GetProductById(id);
            var crumbs = new List<BreadCrumbsViewModel>();

            if (FromType == BreadCrumbsType.Section)
            {
                crumbs.Add(new BreadCrumbsViewModel
                {
                    BreadCrumbsType = FromType,
                    Id = item.Section.Id.ToString(),
                    Name = item.Section.Name
                });
            }
            else
                crumbs.Add(new BreadCrumbsViewModel
                {
                    BreadCrumbsType = FromType,
                    Id = item.Brand.Id.ToString(),
                    Name = item.Brand.Name
                });

            crumbs.Add(new BreadCrumbsViewModel
            {
                BreadCrumbsType = Type,
                Id = item.Id.ToString(),
                Name = item.Name
            });

            return crumbs;
        }
    }
}

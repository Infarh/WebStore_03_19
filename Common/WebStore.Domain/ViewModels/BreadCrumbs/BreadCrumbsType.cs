using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.ViewModels.BreadCrumbs
{
    public enum BreadCrumbsType
    {
        None,
        Section,
        Brand,
        /// <summary>Товар</summary>
        Item
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.MasterClass.Website.Models;
using UCommerce.Runtime;

namespace UCommerce.MasterClass.Website.Controllers
{
	public class CategoryController : System.Web.Mvc.Controller
	{
		public ActionResult Index()
		{
			var categoryViewModel = new CategoryViewModel();

            var currentCaregory = UCommerce.Runtime.SiteContext.Current.CatalogContext.CurrentCategory;

            categoryViewModel.Name = currentCaregory.DisplayName();
            categoryViewModel.Description = currentCaregory.Description();
            categoryViewModel.Products = MapProducts(UCommerce.Api.CatalogLibrary.GetProducts(currentCaregory));
            
			return View("/views/category.cshtml",categoryViewModel);
		}

        private IList<ProductViewModel> MapProducts(ICollection<UCommerce.EntitiesV2.Product> uCommerceProducts)
        {
            var productsToReturn = new List<ProductViewModel>();

            foreach(var uCommerceProduct in uCommerceProducts)
            {
                var productView = new ProductViewModel();
                productView.Sku = uCommerceProduct.Sku;
                productView.Name = uCommerceProduct.DisplayName();
                productView.LongDescription = uCommerceProduct.LongDescription();
                productView.Variants = MapProducts(uCommerceProduct.Variants);
                productView.PriceCalculation = UCommerce.Api.CatalogLibrary.CalculatePrice(uCommerceProduct);
                productView.Url = "/store/product?product=" + uCommerceProduct.Id;
                productsToReturn.Add(productView);
            }

            return productsToReturn;
        }
	}
}
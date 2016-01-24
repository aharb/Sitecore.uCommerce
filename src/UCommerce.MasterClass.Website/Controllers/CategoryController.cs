using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.MasterClass.Website.Models;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using UCommerce.Extensions;
using UCommerce.Api;

namespace UCommerce.MasterClass.Website.Controllers
{
	public class CategoryController : System.Web.Mvc.Controller
	{
		public ActionResult Index()
		{
			var categoryViewModel = new CategoryViewModel();

           UCommerce.EntitiesV2.Category currentUCommerceCategory = UCommerce.Runtime.SiteContext.Current.CatalogContext.CurrentCategory;

            categoryViewModel.Name = currentUCommerceCategory.DisplayName();
            categoryViewModel.Description = currentUCommerceCategory.Description();
            categoryViewModel.Products = MapProducts(UCommerce.Api.CatalogLibrary.GetProducts(currentUCommerceCategory));

			return View("/views/category.cshtml",categoryViewModel);
		}

        private IList<ProductViewModel> MapProducts(ICollection<UCommerce.EntitiesV2.Product> uCommerceProductsToMap)
        {
            var productsToReturn = new List<ProductViewModel>();

            foreach(var uCommerceProduct in uCommerceProductsToMap){
                var productView = new ProductViewModel();
                productView.Sku = uCommerceProduct.Sku;
                productView.VariantSku = uCommerceProduct.VariantSku;
                productView.Name = uCommerceProduct.DisplayName();
                productView.LongDescription = uCommerceProduct.LongDescription();
                productView.PriceCalculation =  UCommerce.Api.CatalogLibrary.CalculatePrice(uCommerceProduct);
                productView.Url = "/store/category/product?product=" + uCommerceProduct.ProductId;
                //productView.Url = CatalogLibrary.GetNiceUrlForProduct(uCommerceProduct);
                productView.Variants = MapProducts(uCommerceProduct.Variants);
                productsToReturn.Add(productView);
            }

            return productsToReturn;
        }
	}
}
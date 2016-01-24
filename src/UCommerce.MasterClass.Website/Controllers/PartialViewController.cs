using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.MasterClass.Website.Models;
using UCommerce.Api;
using UCommerce.Extensions;


namespace UCommerce.MasterClass.Website.Controllers
{
	public class PartialViewController : System.Web.Mvc.Controller
	{
		public ActionResult CategoryNavigation()
		{
			var categoryNavigation = new CategoryNavigationViewModel();
            categoryNavigation.Categories = MapUCommerceCategories(UCommerce.Api.CatalogLibrary.GetRootCategories());

			return View("/views/PartialViews/CategoryNavigation.cshtml", categoryNavigation);
		}

        private IList<CategoryViewModel> MapUCommerceCategories(ICollection <UCommerce.EntitiesV2.Category> uCommerceCategories)
        {
            var categoriesToReturn = new List<CategoryViewModel>();

            foreach(var uCommerceCategoryToMap in uCommerceCategories){
                var categoryViewModel = new CategoryViewModel();
                categoryViewModel.Name = uCommerceCategoryToMap.DisplayName();
                categoryViewModel.Url = "/store/category?category=" + uCommerceCategoryToMap.CategoryId;
                categoryViewModel.Categories = MapUCommerceCategories(UCommerce.Api.CatalogLibrary.GetCategories(uCommerceCategoryToMap));

                categoriesToReturn.Add(categoryViewModel);
            }


            return categoriesToReturn;

        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.MasterClass.Website.Models;

namespace UCommerce.MasterClass.Website.Controllers
{
	public class PartialViewController : System.Web.Mvc.Controller
	{
		public ActionResult CategoryNavigation()
		{
			var categoryNavigation = new CategoryNavigationViewModel();
            categoryNavigation.Categories = this.MapCategories(UCommerce.Api.CatalogLibrary.GetRootCategories());

			return View("/views/PartialViews/CategoryNavigation.cshtml", categoryNavigation);
		}

        private IList<CategoryViewModel> MapCategories(ICollection<UCommerce.EntitiesV2.Category> categories)
        {
            var cats = new List<CategoryViewModel>();

            foreach(var cat in categories)
            {
                var categoryViewModel = new CategoryViewModel();
                categoryViewModel.Name = cat.DisplayName();
                categoryViewModel.Url ="/store/category?category=" +cat.CategoryId;
                categoryViewModel.Categories = MapCategories(UCommerce.Api.CatalogLibrary.GetCategories(cat));

                cats.Add(categoryViewModel);
            }

            return cats;

        }
	}
}
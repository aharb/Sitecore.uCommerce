using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.MasterClass.Website.Models;

namespace UCommerce.MasterClass.Website.Controllers
{
	public class ShippingController : System.Web.Mvc.Controller
	{
		public ActionResult Index()
		{
			var shipping = new ShippingViewModel();

			return View("/Views/Shipping.cshtml", shipping);
		}

		[HttpPost]
		public ActionResult Index(ShippingViewModel shipping)
		{
			return Redirect("/store/payment");
		}
	}
}
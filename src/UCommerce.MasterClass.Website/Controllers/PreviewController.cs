using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.MasterClass.Website.Models;

namespace UCommerce.MasterClass.Website.Controllers
{
	public class PreviewController : System.Web.Mvc.Controller
	{
		public ActionResult Index()
		{
			PurchaseOrderViewModel model = MapOrder();
			return View("/Views/preview.cshtml", model);
		}

		private PurchaseOrderViewModel MapOrder()
		{
			var basketModel = new PurchaseOrderViewModel();

			return basketModel;
		}
		
		[HttpPost]
		public ActionResult Post()
		{
			return View("/Views/Complete.cshtml");
		}
	}
}
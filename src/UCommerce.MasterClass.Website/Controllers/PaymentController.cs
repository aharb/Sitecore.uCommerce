using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.MasterClass.Website.Models;

namespace UCommerce.MasterClass.Website.Controllers
{
	public class PaymentController : System.Web.Mvc.Controller
	{
		public ActionResult Index()
		{
			var payment = new PaymentViewModel();

			return View("/Views/Payment.cshtml", payment);
		}

		[HttpPost]
		public ActionResult Index(PaymentViewModel payment)
		{
			return Redirect("/store/preview");
		}

	}
}
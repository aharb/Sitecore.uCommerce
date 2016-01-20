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
            var shippingModel = new ShippingViewModel();


            shippingModel.AvailableShippingMethods = new List<SelectListItem>();
            var shippingCountry = TransactionLibrary.GetShippingInformation().Country;
            var avalableShippingMethods = TransactionLibrary.GetShippingMethods(shippingCountry);

            var selectedShipping = TransactionLibrary.GetBasket(false).PurchaseOrder.Shipments.FirstOrDefault();

            foreach (var avalableShippingMethod in avalableShippingMethods)
            {
                shippingModel.AvailableShippingMethods.Add(new SelectListItem()
                {
                    Selected = selectedShipping != null && selectedShipping.ShippingMethod.ShippingMethodId == avalableShippingMethod.ShippingMethodId,
                    Text = avalableShippingMethod.Name,
                    Value = avalableShippingMethod.ShippingMethodId.ToString()
                });
            }


            return View("/Views/Shipping.cshtml", shippingModel);
        }

        [HttpPost]
        public ActionResult Index(ShippingViewModel shipping)
        {
            return Redirect("/store/payment");
        }
    }
}
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
            var availableShippingMethods = TransactionLibrary.GetShippingMethods(shippingCountry);

            var selectedShipping = TransactionLibrary.GetBasket(false).PurchaseOrder.Shipments.FirstOrDefault();
            foreach (var availableShippingMethod in availableShippingMethods)
            {
                shippingModel.AvailableShippingMethods.Add(new SelectListItem()
                {
                    Selected = selectedShipping != null &&
                    selectedShipping.ShippingMethod.ShippingMethodId ==
                    availableShippingMethod.ShippingMethodId,
                    Text = availableShippingMethod.Name,
                    Value = availableShippingMethod.ShippingMethodId.ToString()
                });
            }

            return View("/Views/Shipping.cshtml", shippingModel);
        }

        [HttpPost]
        public ActionResult Index(ShippingViewModel shipping)
        {
            TransactionLibrary.CreateShipment(shipping.SelectedShippingMethodId, overwriteExisting: true);
            TransactionLibrary.ExecuteBasketPipeline();

            return Redirect("/store/payment");
        }
    }
}
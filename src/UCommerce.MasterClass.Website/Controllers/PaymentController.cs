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
            var paymentModel = new PaymentViewModel();

            paymentModel.AvailablePaymentMethods = new List<SelectListItem>();

            var shippingCountry = TransactionLibrary.GetShippingInformation().Country;

            var payment = TransactionLibrary.GetBasket(false).PurchaseOrder.Payments.FirstOrDefault();

            var avaiablePaymentmethods = TransactionLibrary.GetPaymentMethods(shippingCountry);

            foreach (var availblePaymentMethod in avaiablePaymentmethods)
            {
                paymentModel.AvailablePaymentMethods.Add(new SelectListItem()
                {
                    Selected = payment != null && payment.PaymentMethod.PaymentMethodId == availblePaymentMethod.PaymentMethodId,
                    Text = availblePaymentMethod.Name,
                    Value = availblePaymentMethod.PaymentMethodId.ToString()
                });
            }

            return View("/Views/Payment.cshtml", paymentModel);
        }

        [HttpPost]
        public ActionResult Index(PaymentViewModel payment)
        {
            UCommerce.Api.TransactionLibrary.CreatePayment(payment.SelectedPaymentMethodId, requestPayment: false);
            UCommerce.Api.TransactionLibrary.ExecuteBasketPipeline();
            return Redirect("/store/preview");
        }

    }
}
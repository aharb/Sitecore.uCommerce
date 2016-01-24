using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.MasterClass.Website.Models;

namespace UCommerce.MasterClass.Website.Controllers
{
    public class BasketController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var basketModel = MapBasket();
            // send email

            return View("/Views/Basket.cshtml", basketModel);
        }

        private PurchaseOrderViewModel MapBasket()
        {

            UCommerce.EntitiesV2.PurchaseOrder uCommerceOrder = TransactionLibrary.GetBasket(false).PurchaseOrder;
            var basketModel = new PurchaseOrderViewModel();
            basketModel.OrderTotal = new UCommerce.Money(uCommerceOrder.OrderTotal.GetValueOrDefault(), uCommerceOrder.BillingCurrency).ToString();

            foreach (var uCOmmerceOrderLine in uCommerceOrder.OrderLines)
            {
                basketModel.OrderLines.Add(new OrderlineViewModel()
                {
                    OrderLineId = uCOmmerceOrderLine.OrderLineId,
                    ProductName = uCOmmerceOrderLine.ProductName,
                    Quantity = uCOmmerceOrderLine.Quantity,
                    Sku = uCOmmerceOrderLine.Sku,
                    VariantSku = uCOmmerceOrderLine.VariantSku,
                    Total = new UCommerce.Money(uCOmmerceOrderLine.Total.GetValueOrDefault(),
                        uCommerceOrder.BillingCurrency).ToString()
                });

            }

            return basketModel;
        }

        [HttpPost]
        public ActionResult Index(PurchaseOrderViewModel model)
        {
            foreach (var orderLine in model.OrderLines)
            {

                var newQuantity = orderLine.Quantity;
                if (model.RemoveOrderlineId == orderLine.OrderLineId)
                {
                    newQuantity = 0;

                    UCommerce.Api.TransactionLibrary.UpdateLineItem(orderLine.OrderLineId, newQuantity);
                }

            }

            UCommerce.Api.TransactionLibrary.ExecuteBasketPipeline(); // everytime we modify the basket, execute pipeline
            return Index();
        }
    }
}
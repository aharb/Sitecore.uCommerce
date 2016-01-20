using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure.Runtime;
using UCommerce.MasterClass.Website.Models;
using UCommerce;

namespace UCommerce.MasterClass.Website.Controllers
{
    public class BasketController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var basketModel = MapBasket();

            return View("/Views/Basket.cshtml", basketModel);
        }

        private PurchaseOrderViewModel MapBasket()
        {
            UCommerce.EntitiesV2.PurchaseOrder uCommerceOrder = TransactionLibrary.GetBasket(false).PurchaseOrder;

            var basketModel = new PurchaseOrderViewModel();

            basketModel.OrderTotal = new UCommerce.Money(uCommerceOrder.OrderTotal.GetValueOrDefault(), uCommerceOrder.BillingCurrency).ToString();



            foreach (var uCommerceOrderLine in uCommerceOrder.OrderLines)
            {
                basketModel.OrderLines.Add(new OrderlineViewModel()
                {
                    OrderLineId = uCommerceOrderLine.OrderLineId,
                    ProductName = uCommerceOrderLine.ProductName,
                    Quantity = uCommerceOrderLine.Quantity,
                    Sku = uCommerceOrderLine.Sku,
                    VariantSku = uCommerceOrderLine.VariantSku,
                    Total = new UCommerce.Money(uCommerceOrderLine.Total.GetValueOrDefault(), uCommerceOrder.BillingCurrency).ToString()
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
                    UCommerce.Api.TransactionLibrary.ExecuteBasketPipeline();
                }
            }
            return Index();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Infrastructure.Runtime;
using UCommerce.MasterClass.Website.Models;
using UCommerce.Runtime;

namespace UCommerce.MasterClass.Website.Controllers
{
    public class ProductController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            ProductViewModel productModel = MapProduct(UCommerce.Runtime.SiteContext.Current.CatalogContext.CurrentProduct);
            return View("/views/product.cshtml", productModel);
        }

        [HttpPost]
        public ActionResult Index(AddToBasketViewModel model)
        {
            UCommerce.Api.TransactionLibrary.AddToBasket(1, model.Sku, model.VariantSku);
            return Index();
        }

        private ProductViewModel MapProduct(UCommerce.EntitiesV2.Product product)
        {
            ProductViewModel productToReturn = new ProductViewModel();
            productToReturn.Sku = product.Sku;
            productToReturn.VariantSku = product.VariantSku;
            productToReturn.LongDescription = product.LongDescription();
            productToReturn.Name = product.DisplayName();
            productToReturn.PriceCalculation = UCommerce.Api.CatalogLibrary.CalculatePrice(product);
            productToReturn.Url = "/store/category/product?product=" + product.ProductId;

            foreach (var variant in product.Variants)
            {
                productToReturn.Variants.Add(MapProduct(variant));
            }
            return productToReturn;
        }
    }
}
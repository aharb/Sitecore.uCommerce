using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.MasterClass.Website.Models;
using UCommerce.Runtime;
using UCommerce.EntitiesV2;
using UCommerce.Api;

namespace UCommerce.MasterClass.Website.Controllers
{
    public class ProductController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var currentProduct = UCommerce.Runtime.SiteContext.Current.CatalogContext.CurrentProduct;
            ProductViewModel productModel = MapProduct(currentProduct);

            return View("/views/product.cshtml", productModel);
        }

        private ProductViewModel MapProduct(UCommerce.EntitiesV2.Product product)
        {
            ProductViewModel productToReturn = new ProductViewModel();
            productToReturn.Sku = product.Sku;
            productToReturn.VariantSku = product.VariantSku;
            productToReturn.LongDescription = product.LongDescription();
            productToReturn.Name = product.DisplayName();
            productToReturn.PriceCalculation = UCommerce.Api.CatalogLibrary.CalculatePrice(product);

            foreach (var variant in product.Variants)
            {
                productToReturn.Variants.Add(MapProduct(variant));
            }

            return productToReturn;

        }

        [HttpPost]
        public ActionResult Index(AddToBasketViewModel model)
        {
            UCommerce.Api.TransactionLibrary.AddToBasket(1, model.Sku, model.VariantSku);
            return Index();
        }
    }
}
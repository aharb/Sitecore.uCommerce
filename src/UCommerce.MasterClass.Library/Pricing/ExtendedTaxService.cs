using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCommerce.Catalog;
using UCommerce.EntitiesV2;

namespace UCommerce.MasterClass.BusinessLogic.Pricing
{
    public class ExtendedTaxService : TaxService
    {
        private readonly IRepository<PriceGroup> _priceGroupRepository;
        public ExtendedTaxService(IRepository<PriceGroup> priceGroupRepository)
        {
            _priceGroupRepository = priceGroupRepository;
        }

        public override Money CalculateTax(EntitiesV2.Product product, EntitiesV2.PriceGroup priceGroup, Money unitPrice)
        {
            var productProperty = product.GetProperties()
                .FirstOrDefault(x => x.GetDefinitionField().DataType.DefinitionName == "PriceGroup");

            if (productProperty == null && product.ParentProduct != null)
            {
                // check variant
                productProperty = product.ParentProduct
                    .GetProperties()
                                .FirstOrDefault(x => x.GetDefinitionField().DataType.DefinitionName == "PriceGroup");
            }

            if (productProperty == null)
            {
                return base.CalculateTax(product, priceGroup, unitPrice);
            }

            int priceGroupId = -1;
            if (int.TryParse(productProperty.GetValue().ToString(), out priceGroupId))
            {
                var overridenPriceGroup = _priceGroupRepository.SingleOrDefault(x => x.PriceGroupId == priceGroupId);
                if (priceGroup != null)
                {
                    return CalculateTax(overridenPriceGroup, unitPrice);
                }
            }

            return base.CalculateTax(product, priceGroup, unitPrice);
        }
    }
}

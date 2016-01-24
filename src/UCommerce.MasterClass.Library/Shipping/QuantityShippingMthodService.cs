using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCommerce.Transactions.Shipping;

namespace UCommerce.MasterClass.BusinessLogic.Shipping
{
    public class QuantityShippingMthodService : IShippingMethodService
    {
        private readonly int _multiplier;
        public QuantityShippingMthodService(int multiplier)
        {
            _multiplier = multiplier;
        }
        public Money CalculateShippingPrice(EntitiesV2.Shipment shipment)
        {
            ValidateForShipping(shipment);
            var quantity = shipment.OrderLines.Sum(orderLine => orderLine.Quantity);
            return new Money(quantity * _multiplier, shipment.PurchaseOrder.BillingCurrency);
        }

        public bool ValidateForShipping(EntitiesV2.Shipment shipment)
        {
            return true;
        }
    }
}

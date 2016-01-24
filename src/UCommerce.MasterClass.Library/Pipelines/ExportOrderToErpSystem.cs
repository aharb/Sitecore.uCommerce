using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCommerce.Pipelines;

namespace UCommerce.MasterClass.BusinessLogic.Pipelines
{
    public class ExportOrderToErpSystem : IPipelineTask<UCommerce.EntitiesV2.PurchaseOrder>
    {
        private readonly string _path;
        public ExportOrderToErpSystem(string path)
        {
            _path = path;
        }

        public PipelineExecutionResult Execute(EntitiesV2.PurchaseOrder subject)
        {
            var orderNumber = subject.OrderNumber;
            var orderTotal = subject.OrderTotal;
            var billingCurrency = subject.BillingCurrency;

            File.AppendAllText(_path, string.Format("{0}: {1}", orderNumber, 
                new Money(orderTotal.GetValueOrDefault(), billingCurrency ).ToString()));
            
            return PipelineExecutionResult.Success;
        }
    }
}

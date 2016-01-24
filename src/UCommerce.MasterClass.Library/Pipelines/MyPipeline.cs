using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCommerce.Pipelines;
using UCommerce.Infrastructure.Logging;

namespace UCommerce.MasterClass.BusinessLogic.Pipelines
{
    public class MyPipeline : Pipeline<UCommerce.EntitiesV2.PurchaseOrder>
    {
        public MyPipeline(IPipelineTask<UCommerce.EntitiesV2.PurchaseOrder>[] tasks, ILoggingService loggingservice)
            : base(tasks, loggingservice)
        {
        }

        // How to use
        //  ObjectFactory.Instance.Resolve<IPipeline<PurchaseOrder>>("MyPipline");
    }
}

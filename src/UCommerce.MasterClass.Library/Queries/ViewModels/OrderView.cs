using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCommerce.MasterClass.BusinessLogic.Queries.ViewModels
{
    
        public class OrderView
        {
            public string CustomerFirstName { get; set; }

            public string CustomerEmail { get; set; }

            public string OrderStatus { get; set; }
            public decimal OrderTotal { get; set; }
        }
    
}

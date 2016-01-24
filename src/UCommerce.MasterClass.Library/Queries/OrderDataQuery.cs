using System.Collections.Generic;
using System.Linq;
using NHibernate.Transform;
using UCommerce.EntitiesV2;
using UCommerce.EntitiesV2.Queries;

using UCommerce.MasterClass.BusinessLogic.Queries.ViewModels;
using NHibernate;

namespace UCommerce.MasterClass.BusinessLogic.Queries
{
    public class OrderDataQuery:  ICannedQuery<OrderView>
    {

        public readonly OrderStatus _orderStatus;

        public OrderDataQuery(OrderStatus orderStatus)
        {
            _orderStatus = orderStatus;
        }
        public IEnumerable<OrderView> Execute(ISession session)
        {
            var result = session.CreateQuery(
                @"
                    Select order.OrderStatus.Name AS OrderStatus,
                    order.Customer.FirstName AS CustomerFirstName,
                    order.Customer.EmailAddress AS CustomerEmail,
                    order.OrderTotal as OrderTotal

                    FROM
                    PurchaseOrder order
                    WHERE 
                    order.OrderStatus = :orderStatus

                 "


                ).SetParameter("orderStatus", _orderStatus)
                 .SetResultTransformer(new AliasToBeanResultTransformer(typeof(OrderView)))
                 .Future<OrderView>()
                 .ToList();

            return result;
        }
    }
}

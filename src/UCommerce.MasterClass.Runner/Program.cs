using System;
using System.Linq;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
using UCommerce.MasterClass.BusinessLogic.Queries;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;

namespace UCommerce.MasterClass.Integration
{
    class Program
    {
        static void Main(string[] args)
        {
            // log4net.Config.BasicConfigurator.Configure();

            //var order = ObjectFactory
            //    .Instance
            //    .Resolve<IRepository<PurchaseOrder>>()
            //    .Select(new LatestOrderQuery()).ToList();

            // all entities can access data in the database using static API
            //var orders = UCommerce.EntitiesV2.PurchaseOrder.All().ToList();

            //IRepository<UCommerce.EntitiesV2.PurchaseOrder> orderRepository =
            //    ObjectFactory.Instance.Resolve<IRepository<UCommerce.EntitiesV2.PurchaseOrder>>();

            // Grabs order using the repository
            //var orders = orderRepository.Select().ToList();
            //ISessionProvider sessionProvider = ObjectFactory.Instance.Resolve<ISessionProvider>();
            // use NHIbernate to expand the scope of the session rather than using using() and limit the scope
            // [3:46:11 PM] Ahmad Harb: Nhibernate  in order to do any query on the database you should use the "Nhibernate Session", it's a static object that is intillized when the project starts . and what is he doing here, he grap the Nhibernate session object from the Object factory.  and write an nhibarnate query over the web database
            //ISession session = sessionProvider.GetSession();
            //IList<PurchaseOrder> order = session.Query<UCommerce.EntitiesV2.PurchaseOrder>().
            //    Where(x => x.Customer != null).ToList();


            //var product = session.Query<Product>().FirstOrDefault();
            //product.Name = "Hello World";
            //product.Save();

            // order[0].Delete(); // 

            //var products = ObjectFactory.Instance.Resolve<IRepository<Product>>().
            //    Select(new ProductByPropertiesQuery("ShowOnHomepage", "True")).ToList();

            //var orderLineRepository = ObjectFactory.Instance.Resolve<IRepository<OrderLine>>();
            //var productRepository = ObjectFactory.Instance.Resolve<IRepository<Product>>();

            //IQueryable<Product> query =
            //    from orderLine in orderLineRepository.Select()
            //    join product in productRepository.Select()
            //        on new { orderLine.Sku, orderLine.VariantSku }
            //        equals
            //        new { product.Sku, product.VariantSku }
            //    select product;

            //var result = query.ToList();

            // cahce provider

            //OrderStatus stats = OrderStatus.Get((int)OrderStatusCode.NewOrder);
            //var result = ObjectFactory.Instance.Resolve<IRepository<OrderView>>().Select(new OrderDataQuery(stats)).ToList();
            //ObjectFactory.Instance.Resolve<ICacheProvider>().ClearCache();
        }
    }
}

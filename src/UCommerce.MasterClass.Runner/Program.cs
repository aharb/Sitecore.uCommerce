using System;
using System.Linq;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
using UCommerce.MasterClass.BusinessLogic.Queries;
using System.Collections.Generic;
using NHibernate;

namespace UCommerce.MasterClass.Integration
{
	class Program
	{
		static void Main(string[] args)
		{
			log4net.Config.BasicConfigurator.Configure();
			
            // using IOC container 
			UCommerce.EntitiesV2.PurchaseOrder order = ObjectFactory
				.Instance
				.Resolve<IRepository<PurchaseOrder>>()
				.Select(new LatestOrderQuery()).FirstOrDefault();

            // using static API
            var oreders = UCommerce.EntitiesV2.PurchaseOrder.All().ToList();


            IRepository<UCommerce.EntitiesV2.PurchaseOrder> orederRepository =
                ObjectFactory.Instance.Resolve<IRepository<UCommerce.EntitiesV2.PurchaseOrder>>();

            // Grbs order using the repository
            List<PurchaseOrder> orders = orederRepository.Select().ToList();

            ISessionProvider sessionProvider = ObjectFactory.Instance.Resolve<ISessionProvider>();

            ISession session = sessionProvider.GetSession();



            //using custom query 
            var products = ObjectFactory.Instance.Resolve<IRepository<Product>>().Select(new ProductByPropertiesQuery("ShowOnHomePage", "True"));



		}
	}
}

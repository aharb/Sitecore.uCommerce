using System;
using System.Linq;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
using UCommerce.MasterClass.BusinessLogic.Queries;

namespace UCommerce.MasterClass.Integration
{
	class Program
	{
		static void Main(string[] args)
		{
			log4net.Config.BasicConfigurator.Configure();
			
			var order = ObjectFactory
				.Instance
				.Resolve<IRepository<PurchaseOrder>>()
				.Select(new LatestOrderQuery()).ToList();

			
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCommerce.EntitiesV2;
using UCommerce.Content;
using System.Web;

namespace UCommerce.MasterClass.BusinessLogic.SiteContext
{
    public class ExtendedCatalogContext : UCommerce.Runtime.CatalogContext
    {
        public ExtendedCatalogContext(
            IDomainService domainService,
            IRepository<ProductCatalogGroup> productCatalogGroupRepository,
            IRepository<ProductCatalog> productCatalogRepository,
            IRepository<PriceGroup> priceGroupRerpository
            )
            : base(domainService, productCatalogGroupRepository, productCatalogRepository, priceGroupRerpository)
        {

        }

        public override string CurrentCatalogName
        {
            get
            {
                if (UserIsloggedIn())
                {
                    return "Private";
                }
                return base.CurrentCatalogName;
            }
            set
            {
                base.CurrentCatalogName = value;
            }
        }

        private bool UserIsloggedIn()
        {
            return HttpContext.Current.Request.QueryString["IsLoggedIn"] != null;
        }
    }
}

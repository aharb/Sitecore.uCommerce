using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCommerce.EntitiesV2.Queries;
using NHibernate;
using NHibernate.Linq;

namespace UCommerce.MasterClass.BusinessLogic.Queries
{
    public class ProductByPropertiesQuery : ICannedQuery<UCommerce.EntitiesV2.Product>
    {
        private readonly string _fieldName;
        private readonly string _propertyValue;

        public ProductByPropertiesQuery(string fieldName, string propertyValue)
        {
            _fieldName = fieldName;
            _propertyValue = propertyValue;
        }
        public IEnumerable<EntitiesV2.Product> Execute(NHibernate.ISession session)
        {
            return session.Query<UCommerce.EntitiesV2.Product>()
                .Where(product => product.ProductProperties
                    .Any(property => property.Value == _propertyValue &&
                    property.ProductDefinitionField.Name == _fieldName))
                .ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using UCommerce.EntitiesV2;
using UCommerce.Presentation.Web.Controls;

namespace UCommerce.MasterClass.BusinessLogic.DataTypes
{
    public class PriceGroupControlFactory : IControlFactory
    {
        private readonly IRepository<PriceGroup> _priceGroupRepository;
        public PriceGroupControlFactory(IRepository<PriceGroup> priceGroupRepository)
        {

            _priceGroupRepository = priceGroupRepository;
        }

        public System.Web.UI.Control GetControl(EntitiesV2.Definitions.IProperty property)
        {
            var priceGroups = _priceGroupRepository.Select().ToList();
            var dropDownList = new SafeDropDownList();

            foreach (var priceGroup in priceGroups)
            {
                var listItem = new ListItem();
                listItem.Text = priceGroup.Name;
                listItem.Value = priceGroup.PriceGroupId.ToString();
                listItem.Selected = property.GetValue().ToString() == priceGroup.PriceGroupId.ToString();
                dropDownList.Items.Add(listItem);
            }

            return dropDownList;
        }

        public bool Supports(EntitiesV2.DataType dataType)
        {
            return dataType.DefinitionName == "PriceGroup";
        }
    }
}

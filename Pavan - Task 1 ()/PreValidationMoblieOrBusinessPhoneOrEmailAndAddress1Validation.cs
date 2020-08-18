using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;

namespace Pavan___Task_1___
{
    public class PreValidationMoblieOrBusinessPhoneOrEmailAndAddress1Validation : IPlugin
    {
        private bool attributeHasStringValue(string attributeName, Entity entity)
        {
            bool result = false;
            if (entity != null && entity.Attributes.Contains(attributeName))
            {
                //var attributeValue = entity[attributeName];
                var attributeValue = entity.GetAttributeValue<string>(attributeName);
                if (!string.IsNullOrEmpty(attributeValue))
                {
                    result = true;
                }

            }
            return result;
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity) 
            {
                var entity = (Entity)context.InputParameters["Target"];

                if (context.PrimaryEntityName == "contact")
                {
                    if (!attributeHasStringValue("mobilephone", entity) && !attributeHasStringValue("telephone1", entity) && !attributeHasStringValue("emailaddress1", entity))
                    {
                        throw new InvalidPluginExecutionException("Please fill in either your mobile number, business phone or email before saving!");
                    }
                    if (!attributeHasStringValue("address1_line1", entity) && !attributeHasStringValue("address1_line2", entity) && !attributeHasStringValue("address1_line3", entity))
                    {
                        throw new InvalidPluginExecutionException("Please fill in address  before saving!");
                    }
                }
            }
        }
    }


}
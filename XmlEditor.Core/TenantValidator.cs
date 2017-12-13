using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    public static class TenantValidator
    {
        public static void ValidateTenant(ObservableCollection<Resource> resourceList)
        {
            int index = 0;
            foreach (Resource tenant in resourceList)
            {
                if (tenant.Tenant.ToLower() == "root" || tenant.Tenant.ToLower() == "sop" || tenant.Tenant.ToLower() == "ptk" || tenant.Tenant.ToLower() == "folksam")
                {

                    Validator.ValidatorList.Add(new Validator
                    {
                        ValidTenant = true,
                        
                    });
                }
                else
                {
                    Validator.ValidatorList.Add(new Validator
                    {
                        ValidTenant = false,
                       
                    });
                }
                index++;
            }

        }
    }
}

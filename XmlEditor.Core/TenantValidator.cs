using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    public class TenantValidator
    {
        public void ValidateTenant(ObservableCollection<Resource> resourceList)
        {
            int index = 0;
            foreach (Resource tenant in resourceList)
            {
                if (tenant.Tenant.ToLower() == "root" || tenant.Tenant.ToLower() == "sop" || tenant.Tenant.ToLower() == "ptk" || tenant.Tenant.ToLower() == "folksam")
                {

                    IsValid.valid.Add(new IsValid
                    {
                        ValidTenant = true,
                        Index = index
                    });
                }
                else
                {
                    IsValid.valid.Add(new IsValid
                    {
                        ValidTenant = false,
                        Index = index
                    });
                }
                index++;
            }

        }
    }
}

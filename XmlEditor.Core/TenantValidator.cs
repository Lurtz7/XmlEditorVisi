﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    public class TenantValidator
    {
        public IsValid ValidateTenant(ObservableCollection<Resource> resourceList)
        {
            IsValid isTenantValid = new IsValid();
            
            int index = 0;            
            foreach (Resource tenant in resourceList)
            {
                if (tenant.Tenant.ToLower() != "root" || tenant.Tenant.ToLower() != "sop" || tenant.Tenant.ToLower() != "ptk" || tenant.Tenant.ToLower() != "folksam")
                {
                    isTenantValid.Valid = false;
                    isTenantValid.Index = index;
                }
                else
                {
                    isTenantValid.Valid = true;
                    isTenantValid.Index = index;
                }
                index++;
            }
            return isTenantValid;
        }
    }
}

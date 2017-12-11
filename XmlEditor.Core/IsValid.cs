using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XmlEditor.Core
{
    public class IsValid
    {
        public bool ValidTenant { get; set; }
        public bool ValidLanguage { get; set; }
        public bool ValidDate { get; set; }
        public bool Valid { get; set; }

        public int Index { get; set; }

        List<IsValid> valid = new List<IsValid>();

        TenantValidator tenant = new TenantValidator();
        LanguageValidator language = new LanguageValidator();
        DateValidator date = new DateValidator();

        public void Validator(ObservableCollection<Resource> resourceList)
        {
            var validateTenant = tenant.ValidateTenant(resourceList);
            //var validateLanguage = tenant.ValidateLanguage(resourceList);
            //var validateDate = tenant.ValidateDate(resourceList);

            foreach (var item in validateTenant)
            {
                valid.Add(new IsValid
                {
                    //ValidLanguage = validateLanguage[item.Index];
                    //ValidDate = validateDate[item.Index];
                    ValidTenant = item.ValidTenant,

                });
                //if (valid.ValidTenant && valid.ValidLanguage && valid.ValidDate)
                //{
                //    Valid = true;
                //}
            }

        }
    }
}

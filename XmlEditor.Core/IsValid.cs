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

        public static List<IsValid> valid = new List<IsValid>();

        TenantValidator tenant = new TenantValidator();
        LanguageValidator language = new LanguageValidator();
        DateValidator date = new DateValidator();

        public void Validator(ObservableCollection<Resource> resourceList)
        {
            tenant.ValidateTenant(resourceList);
            language.ValidateLanguage(resourceList);
            date.ValidateDate(resourceList);

            foreach (var item in valid)
            {                
                if (item.ValidTenant && item.ValidDate && item.ValidLanguage)
                {
                    item.Valid = true;
                }
            }
        }
    }
}

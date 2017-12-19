using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XmlEditor.Core
{
    public class Validator
    {
        public bool ValidTenant { get; set; }
        public bool ValidLanguage { get; set; }
        public bool ValidDate { get; set; }
        public bool IsValid { get; set; }


        public static List<Validator> ValidatorList { get; } = new List<Validator>();


        public static Validator Validate(Resource resource)
        {
            if (resource.Language != null)
            {

                ObservableCollection<Resource> resourceList = new ObservableCollection<Resource>
            {
                resource
            };

                Validate(resourceList);
                return ValidatorList.First();
            }
            return null;
        }

        public static void Validate(ObservableCollection<Resource> resourceList)
        {
            ValidatorList.Clear();
            TenantValidator.ValidateTenant(resourceList);
            LanguageValidator.ValidateLanguage(resourceList);
            DateValidator.ValidateDate(resourceList);

            foreach (var item in ValidatorList)
            {
                if (item.ValidTenant && item.ValidDate && item.ValidLanguage)
                {
                    item.IsValid = true;
                }
            }
        }
    }
}

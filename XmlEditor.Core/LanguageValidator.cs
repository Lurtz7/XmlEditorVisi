using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    public static class LanguageValidator
    {
        public static void ValidateLanguage(ObservableCollection<Resource> resourceList)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            for (int i = 0; i < resourceList.Count; i++)
            {
                Validator.ValidatorList[i].ValidLanguage = resourceList[i].Language == "1";

                foreach (CultureInfo culture in cultures)
                {
                    if (resourceList[i].Language.Length == 5)
                    {
                        if (resourceList[i].Language.Equals(culture.Name, StringComparison.InvariantCulture))
                        {
                            Validator.ValidatorList[i].ValidLanguage = true;
                            break;

                        }
                    }

                }
            }
        }
    }
}

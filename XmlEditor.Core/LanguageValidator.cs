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
    public class LanguageValidator
    {
        public void ValidateLanguage(ObservableCollection<Resource> resourceList)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            for (int i = 0; i < resourceList.Count; i++)
            {
                IsValid.valid[i].ValidLanguage = resourceList[i].Language == "1";

                foreach (CultureInfo culture in cultures)
                {
                    if (resourceList[i].Language.Equals(culture.Name, StringComparison.InvariantCulture))
                    {
                        IsValid.valid[i].ValidLanguage = true;
                        break;
                    }
                    //else
                    //{
                    //    IsValid.valid[i].ValidLanguage = false;
                    //}
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    public class LanguageValidator
    {
        public static List<IsValid> CheckIfValidLanguage(List<Resource> resourceList)
        {

            List<IsValid> validList = new List<IsValid>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            int index = 0;

            IsValid valid = new IsValid();
            foreach (var item in resourceList)
            {
                valid.Valid = false;
                foreach (CultureInfo culture in cultures)
                {
                    if (item.Language == culture.Name)
                    {

                        valid.Valid = true;
                        valid.Index = index;
                        break;
                    }
                }
                if (valid.Valid == false)
                {

                    valid.Index = index;
                    valid.Valid = false;


                }
                validList.Add(valid);
                index++;
            }



            return validList;

        }
    }
}

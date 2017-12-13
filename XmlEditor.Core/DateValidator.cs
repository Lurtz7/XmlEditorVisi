using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XmlEditor.Core
{
    public static class DateValidator
    {
        public static void ValidateDate(ObservableCollection<Resource> resourceList)
        {
            for (int i = 0; i < resourceList.Count; i++)
            {             
                if (DateTime.TryParse(resourceList[i].DateChange,out DateTime dt))                
                    Validator.ValidatorList[i].ValidDate = true; 
                else
                    Validator.ValidatorList[i].ValidDate = false;
            }            
        }
    }
}

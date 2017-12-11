using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XmlEditor.Core
{
    public class DateValidator
    {
        public void ValidateDate(ObservableCollection<Resource> resourceList)
        {
            for (int i = 0; i < resourceList.Count; i++)
            {             
                if (DateTime.TryParse(resourceList[i].DateChange,out DateTime dt))                
                    IsValid.valid[i].ValidDate = true; 
                else
                    IsValid.valid[i].ValidDate = false;
            }            
        }
    }
}

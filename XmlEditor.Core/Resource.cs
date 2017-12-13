using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    public class Resource : IEditableObject, INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Tenant { get; set; }
        public string GenericKey { get; set; }
        public string DateChange { get; set; }
        public string ResourceData { get; set; }


        private Resource backupCopy;
        private bool inEdit;


        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as Resource;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.Name = backupCopy.Name;
            this.Language = backupCopy.Language;
            this.Tenant = backupCopy.Tenant;
            this.GenericKey = backupCopy.GenericKey;
            this.DateChange = backupCopy.DateChange;
            this.ResourceData = backupCopy.ResourceData;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
              
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
                
            }
        }
    }
}

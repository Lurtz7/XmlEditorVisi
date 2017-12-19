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
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string language;
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                if (language == value) return;
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
        private string tenant;
        public string Tenant
        {
            get
            {
                return tenant;
            }
            set
            {
                if (tenant == value) return;
                tenant = value;
                OnPropertyChanged(nameof(Tenant));
            }
        }
        private string genericKey;
        public string GenericKey
        {
            get
            {
                return genericKey;
            }
            set
            {
                if (genericKey == value) return;
                genericKey = value;
                OnPropertyChanged(nameof(GenericKey));
            }
        }
        private string dateChange;
        public string DateChange
        {
            get
            {
                return dateChange;
            }
            set
            {
                if (dateChange == value) return;
                dateChange = value;
                OnPropertyChanged(nameof(DateChange));
            }
        }
        private string resourceData;
        public string ResourceData
        {
            get
            {
                return resourceData;
            }
            set
            {
                if (resourceData == value) return;
                resourceData = value;
                OnPropertyChanged(nameof(ResourceData));
            }
        }       
        
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
                if (backupCopy != null)
                {
                    string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    DateChange = dateTime;
                }
            }
        }
    }
}

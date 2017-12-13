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
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if (_language == value) return;
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
        private string _tenant;
        public string Tenant
        {
            get
            {
                return _tenant;
            }
            set
            {
                if (_tenant == value) return;
                _tenant = value;
                OnPropertyChanged(nameof(Tenant));
            }
        }
        private string _genericKey;
        public string GenericKey
        {
            get
            {
                return _genericKey;
            }
            set
            {
                if (_genericKey == value) return;
                _genericKey = value;
                OnPropertyChanged(nameof(GenericKey));
            }
        }
        private string _datechange;
        public string DateChange
        {
            get
            {
                return _datechange;
            }
            set
            {
                if (_datechange == value) return;
                _datechange = value;
                OnPropertyChanged(nameof(DateChange));
            }
        }
        private string _resourcedata;
        public string ResourceData
        {
            get
            {
                return _resourcedata;
            }
            set
            {
                if (_resourcedata == value) return;
                _resourcedata = value;
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
                //string dateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                //DateChange = dateTime;
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
                
            }
        }
    }
}

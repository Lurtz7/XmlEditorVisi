using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XmlEditor.Core
{
    public class ResourceList : ObservableCollection<Resource>
    {
        public string FileName { get; set; }

        public ResourceList(Resource[] resources)
        {
            foreach (var item in resources)
            {
                this.Add(new Resource
                {
                    Name = item.Name,
                    Language = item.Language,
                    DateChange = item.DateChange,
                    GenericKey = item.GenericKey,
                    ResourceData = item.ResourceData,
                    Tenant = item.Tenant
                });
            }
        }
    }
}

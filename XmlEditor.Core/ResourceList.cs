using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XmlEditor.Core
{
    public class ResourceList : ObservableCollection<Resource>
    {
        //LocalFileRepository repository = new LocalFileRepository();

        //MainWindow main = new MainWindow();

        public string FileName { get; set; }

        //public ResourceList()
        //{
        //    this.Add(new Resource
        //    {
        //        Name = "aasdas",
        //        Language = "aasdas",
        //        DateChange = "aasdas",
        //        GenericKey = "aasdas",
        //        ResourceData = "aasdas",
        //        Tenant = "aasdas"
        //    });
        //}


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

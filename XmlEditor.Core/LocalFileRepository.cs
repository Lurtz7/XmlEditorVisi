using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Xml.Linq;


namespace XmlEditor.Core
{
    public class LocalFileRepository
    {
        IsValid isItValid = new IsValid();
        static public Resource[] GetXmlFile(string path)
        {
            XDocument document = XDocument.Load(path);


            var resourceElements = document
                .Element("Resources")
                .Elements("Resource")
                .Select(e => new Resource
                {
                    Name = e.Element("Name").Value,
                    ResourceData = e.Element("ResourceData").Value,
                    DateChange = e.Element("DateChange").Value,
                    Language = e.Element("Language").Value,
                    GenericKey = e.Element("GenericKey").Value,
                    Tenant = e.Element("Tenant").Value,



                })
                .ToArray();

            return resourceElements;
        }


        public bool SaveXmlFile(string fileName, List<Resource> resourceList)
        {
            bool save = true;
            foreach (var item in IsValid.valid)
            {
                if (item.Valid == false)
                {
                    save = false;
                }
                
            }

            if (save)
            {
                XDocument document = XDocument.Load(fileName);

                XElement xmlElements = new XElement("Resources", resourceList.Select(i => new XElement("Resource",
                    new XElement("Name", i.Name),
                       new XElement("Language", i.Language),
                       new XElement("Tenant", i.Tenant),
                       new XElement("GenericKey", i.GenericKey),
                       new XElement("DateChange", i.DateChange),
                       new XElement("ResourceData", i.ResourceData))));
                document.ReplaceNodes(xmlElements);
                document.Save(fileName);
                return true;
            }

            else
            {
                string messageBoxText = "Cant save,wrong input in table. Try correct it!";
                string caption = "XmlEditor 1.0";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                return false;
                
            }

        }




    }
}

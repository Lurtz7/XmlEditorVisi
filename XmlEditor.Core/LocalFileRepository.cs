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
            bool save = Validator.ValidatorList.TrueForAll(o => o.IsValid);

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
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XmlEditor.Core
{
    public class LocalFileRepository
    {
        Dictionary<string, LanguageEnum> toDictionary; // = new Dictionary<string, LanguageEnum>
        Dictionary<LanguageEnum, string> fromDictionary; // = new Dictionary<string, LanguageEnum>
        Dictionary<string, GenericKeyEnum> tokeyDictionary; // = new Dictionary<string, LanguageEnum>
        Dictionary<GenericKeyEnum, string > fromKeyDictionary; // = new Dictionary<string, LanguageEnum>


        public LocalFileRepository()
        {
            toDictionary = new Dictionary<string, LanguageEnum>();
            toDictionary.Add("sv-SE", LanguageEnum.Swedish);
            toDictionary.Add("en-US", LanguageEnum.English);
            toDictionary.Add("da-DK", LanguageEnum.Danish);

            fromDictionary = new Dictionary<LanguageEnum, string>();
            fromDictionary.Add(LanguageEnum.Swedish, "sv-SE");
            fromDictionary.Add(LanguageEnum.English, "en-US");
            fromDictionary.Add(LanguageEnum.Danish, "da-DK");

            tokeyDictionary = new Dictionary<string, GenericKeyEnum>();
            tokeyDictionary.Add("*", GenericKeyEnum.key);

            fromKeyDictionary = new Dictionary<GenericKeyEnum, string >();
            fromKeyDictionary.Add(GenericKeyEnum.key, "*");
        }

        public Resource[] GetXmlFile(string path)
        {
            XDocument document = XDocument.Load(path);


           

            var resourceElements = document
                .Element("Resources")
                .Elements("Resource")
                .Select(e => new Resource
                {
                    Name = e.Element("Name").Value,
                    ResourceData = e.Element("ResourceData").Value,
                    DateChange = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Language = toDictionary[e.Element("Language").Value],
                    GenericKey = tokeyDictionary[e.Element("GenericKey").Value],
                    Tenant = ConvertToTenant(e.Element("Tenant").Value),



                })
                .ToArray();

            return resourceElements;
        }

        private TenantEnum ConvertToTenant(string value)
        {
            switch (value)
            {
                case "root":
                    return TenantEnum.root;

                case "sop":
                    return TenantEnum.sop;

                case "PTK":
                    return TenantEnum.PTK;

                case "Folksam":
                    return TenantEnum.Folksam;
                default:
                    throw new Exception($"Unknown Tenant string: {value}");
            }
        }

        public void SaveXmlFile(string fileName, List<Resource> resourceList)
        {
            XDocument document = XDocument.Load(fileName);


            XElement xmlElements = new XElement("Resources", resourceList.Select(i => new XElement("Resource",
                new XElement("Name", i.Name + "Test"),
                   new XElement("Language", fromDictionary[i.Language]),
                   new XElement("Tenant", i.Tenant),
                   new XElement("GenericKey", fromKeyDictionary[i.GenericKey]),
                   new XElement("DateChange", i.DateChange),
                   new XElement("ResourceData", i.ResourceData))));
            document.ReplaceNodes(xmlElements);
            document.Save(fileName);
        }

        
    }
}

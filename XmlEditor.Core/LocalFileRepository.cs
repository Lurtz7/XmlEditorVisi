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
                    //Language = ConvertToLanguage(e.Element("Language").Value),
                    Language = toDictionary[e.Element("Language").Value],
                    GenericKey = ConvertToGenericKey(e.Element("GenericKey").Value),
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
                   new XElement("Language", i.Language),
                   new XElement("Tenant", i.Tenant),
                   new XElement("GenericKey", i.GenericKey),
                   new XElement("DateChange", i.DateChange),
                   new XElement("ResourceData", i.ResourceData))));
            document.ReplaceNodes(xmlElements);
            document.Save(fileName);
        }

        private GenericKeyEnum ConvertToGenericKey(string value)
        {
            switch (value)
            {
                case "*":
                    return GenericKeyEnum.key;

                
                default:
                    throw new Exception($"Unknown generickey string: {value}");
            }
        }

        private LanguageEnum ConvertToLanguage(string value)
        {
            switch (value)
            {
                case "sv-SE":
                    return LanguageEnum.Swedish;

                case "en-US":
                    return LanguageEnum.English;

                case "da-DK":
                    return LanguageEnum.Danish;

                default:
                    throw new Exception($"Unknown language string: {value}");
            }
        }
    }
}

using System;
using System.Linq;
using System.Xml.Linq;

namespace XmlEditor.Core
{
    public class LocalFileRepository
    {
        static void GetXmlFile()
        {
            XDocument document = XDocument.Load(@"C:\Project\TestXml\TestXml\Resource.sv-SE.xml");


            var resourceElements = document
                .Element("Resources")
                .Elements("Resource")
                .Select(e => new Resource
                {
                    Name = e.Element("Name").Value,
                    ResourceData = e.Element("ResourceData").Value

                });


            foreach (var item in resourceElements)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.ResourceData);
            }
        }
    }
}

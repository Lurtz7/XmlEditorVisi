using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    public class Resource 
    {
        public string Name { get; set; }
        public LanguageEnum Language { get; set; }
        public TenantEnum Tenant { get; set; }
        public GenericKeyEnum GenericKey { get; set; }
        public string DateChange { get; set; }
        public string ResourceData { get; set; }
    
      
    }
}

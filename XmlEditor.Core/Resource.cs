using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlEditor.Core
{
    class Resource
    {
        public string Name { get; set; }
        public string ResourceData { get; set; }
        public LanguageEnum Language { get; set; }
        public TenantEnum Tenant { get; set; }
        public GenericKeyEnum GenericKey { get; set; }
        public DateTime DateChange { get; set; }
    
        //public override string ToString()
        //{
         

        //    return base.ToString();
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.Utils
{
    public class DynamicObject
    {
        private IDictionary<string, object> m_ExObject;

        public object this[string propertyName]
        {
            get
            {
                return Get(propertyName);
            }
            set
            {
                Set(propertyName, value);
            }
        }

        public string Type { get; private set; }

        public DynamicObject(string type)
        {
            m_ExObject = new ExpandoObject();
            Type = type;
        }

        public bool Has(string propertyName)
        {
            return m_ExObject.ContainsKey(propertyName);
        }

        public object Get(string propertyName)
        {
            return m_ExObject[propertyName];
        }

        public void Set(string propertyName, object value)
        {
            m_ExObject[propertyName] = value;
        }
    }
}

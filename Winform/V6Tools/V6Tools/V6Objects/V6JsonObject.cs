using System;
using V6Tools.V6Convert;

namespace V6Tools.V6Objects
{
    public class V6JsonObject
    {
        protected string DateTimeFormat = null;
        public V6JsonObject(string dateTimeFormat = null)
        {
            DateTimeFormat = dateTimeFormat;
        }

        public virtual string ToJson()
        {
            return V6JsonConverter.ClassToJson(this, DateTimeFormat);
        }

        public virtual string ToJson(string dateTimeFormat)
        {
            return V6JsonConverter.ClassToJson(this, dateTimeFormat);
        }
        
        public string ToXml()
        {
            return V6XmlConverter.ClassToXml(this);
        }
    }
}

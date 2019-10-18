using V6Tools.V6Convert;

namespace V6Tools.V6Objects
{
    public class V6JsonObject
    {
        public virtual string ToJson()
        {
            return V6JsonConverter.ClassToJson(this);
        }
        
        public string ToXml()
        {
            return V6XmlConverter.ClassToXml(this);
        }
    }
}

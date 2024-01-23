using System;
using System.Collections.Generic;
using System.Reflection;
using V6Tools.V6Convert;

namespace V6Tools.V6Objects
{
    /// <summary>
    /// Khi chuyển to xml, nếu là property sẽ có tag name bao bọc, còn field thì không.
    /// </summary>
    public class V6JsonObject : V6Object
    {
        public virtual string ToJson()
        {
            return V6JsonConverter.ClassToJson(this, DateTimeFormat);
        }

        /// <summary>
        /// Chuyển thành chuỗi json. DateTime format hoặc null hoặc VIETTEL.
        /// </summary>
        /// <param name="dateTimeFormat">null hoặc yyyMMdd tùy ý hoặc VIETTEL(millisecond from 1900)</param>
        /// <returns></returns>
        public virtual string ToJson(string dateTimeFormat)
        {
            return V6JsonConverter.ClassToJson(this, dateTimeFormat);
        }
        
        /// <summary>
        /// Khi chuyển to xml, nếu là property sẽ có tag name bao bọc, còn field thì không.
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            return V6XmlConverter.ClassToXml(this);
        }


        //public void SetPropertiesValue(IDictionary<string, string> dic)
        //{
        //    foreach (PropertyInfo propertyInfo in GetType().GetProperties())
        //    {
        //        if (dic.ContainsKey(propertyInfo.Name) && propertyInfo.CanWrite)
        //        {
        //            var value = dic[propertyInfo.Name];
        //            if (propertyInfo.PropertyType == typeof(string))
        //            {
        //                propertyInfo.SetValue(this, value, null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(int))
        //            {
        //                propertyInfo.SetValue(this, int.Parse(value), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(float))
        //            {
        //                propertyInfo.SetValue(this, float.Parse(value), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(decimal))
        //            {
        //                propertyInfo.SetValue(this, decimal.Parse(value), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(DateTime))
        //            {
        //                propertyInfo.SetValue(this, DateTime.ParseExact(value, "dd/MM/yyyy", null), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(bool))
        //            {
        //                propertyInfo.SetValue(this, value == "1", null);
        //            }
        //        }
        //        else if (dic.ContainsKey(propertyInfo.Name.ToUpper()) && propertyInfo.CanWrite)
        //        {
        //            var value = dic[propertyInfo.Name.ToUpper()];
        //            if (propertyInfo.PropertyType == typeof(string))
        //            {
        //                propertyInfo.SetValue(this, value, null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(int))
        //            {
        //                propertyInfo.SetValue(this, int.Parse(value), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(float))
        //            {
        //                propertyInfo.SetValue(this, float.Parse(value), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(decimal))
        //            {
        //                propertyInfo.SetValue(this, decimal.Parse(value), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(DateTime))
        //            {
        //                propertyInfo.SetValue(this, DateTime.ParseExact(value, "dd/MM/yyyy", null), null);
        //            }
        //            else if (propertyInfo.PropertyType == typeof(bool))
        //            {
        //                propertyInfo.SetValue(this, ObjectAndString.ObjectToBool(value), null);
        //            }
        //        }
        //    }
        //}

        //public Dictionary<string, object> ToDICTIONARY()
        //{
        //    Dictionary<string, object> RESULT = new Dictionary<string, object>();
        //    foreach (PropertyInfo property in GetType().GetProperties())
        //    {
        //        if (property.CanRead && property.CanWrite)
        //        {
        //            object value = property.GetValue(this, null);
        //            if (value is bool)
        //            {
        //                value = (bool)value ? "1" : "0";
        //            }
        //            RESULT[property.Name.ToUpper()] = value;
        //        }
        //    }
        //    return RESULT;
        //}

        /// <summary>
        /// Trả về 1 chuỗi DIC dạng KEY:Value;KEY2:value2...
        /// </summary>
        /// <returns></returns>
        public string ToStringDICTIONARY()
        {
            string result = "";
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(this, null);
                    if (value is bool)
                    {
                        value = (bool)value ? "1" : "0";
                    }
                    result += ";" + property.Name.ToUpper() + ":" + value;
                }
            }
            if (result.Length > 1) result = result.Substring(1);
            return result;
        }
    }
}

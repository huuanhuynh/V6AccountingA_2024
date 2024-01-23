using System;
using System.Collections.Generic;
using System.Reflection;
using V6Tools.V6Convert;

namespace V6Tools.V6Objects
{
    /// <summary>
    /// Class có sẵn các hàm chuyển đổi thành DICTIONARY và StringDICTIONARY.
    /// </summary>
    public class V6Object
    {
        /// <summary>
        /// Định dạng ngày tháng để convert.
        /// </summary>
        protected string DateTimeFormat = "dd/MM/yyyy";
        /// <summary>
        /// Class có sẵn các hàm chuyển đổi thành DICTIONARY và StringDICTIONARY.
        /// </summary>
        public V6Object()
        {

        }
        /// <summary>
        /// Class có sẵn các hàm chuyển đổi thành DICTIONARY và StringDICTIONARY.
        /// </summary>
        /// <param name="dateTimeFormat">dd/MM/yyyy</param>
        public V6Object(string dateTimeFormat = null)
        {
            DateTimeFormat = dateTimeFormat;
        }

        /// <summary>
        /// Gán giá trị từ DIC vào các property { get; set; }
        /// </summary>
        /// <param name="DIC"></param>
        public void SetPropertiesValue(IDictionary<string, string> DIC)
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                if (DIC.ContainsKey(propertyInfo.Name) && propertyInfo.CanWrite)
                {
                    var value = DIC[propertyInfo.Name];
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(this, value, null);
                    }
                    else if (propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(this, int.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(float))
                    {
                        propertyInfo.SetValue(this, float.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(double))
                    {
                        propertyInfo.SetValue(this, double.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        propertyInfo.SetValue(this, decimal.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        propertyInfo.SetValue(this, DateTime.ParseExact(value, DateTimeFormat, null), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyInfo.SetValue(this, value == "1", null);
                    }
                }
                else if (DIC.ContainsKey(propertyInfo.Name.ToUpper()) && propertyInfo.CanWrite)
                {
                    var value = DIC[propertyInfo.Name.ToUpper()];
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(this, value, null);
                    }
                    else if (propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(this, int.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(float))
                    {
                        propertyInfo.SetValue(this, float.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(double))
                    {
                        propertyInfo.SetValue(this, double.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        propertyInfo.SetValue(this, decimal.Parse(value), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        propertyInfo.SetValue(this, DateTime.ParseExact(value, "dd/MM/yyyy", null), null);
                    }
                    else if (propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyInfo.SetValue(this, ObjectAndString.ObjectToBool(value), null);
                    }
                }
            }
        }

        public Dictionary<string, object> ToDICTIONARY()
        {
            Dictionary<string, object> RESULT = new Dictionary<string, object>();
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(this, null);
                    if (value is bool)
                    {
                        value = (bool)value ? "1" : "0";
                    }
                    RESULT[property.Name.ToUpper()] = value;
                }
            }
            return RESULT;
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using V6Tools.V6Convert;

namespace V6ThuePost
{
    public class AutoXml
    {
        protected XmlNode node;

        public AutoXml(XmlNode node)
        {
            this.node = node;
            foreach (XmlNode item in node.ChildNodes)
            {
                SetPropertyValue(item);
            }

            if (node.Attributes != null)
            foreach (XmlAttribute item in node.Attributes)
            {
                SetPropertyValue(item);
            }
        }

        public void SetPropertyValue(XmlNode node)
        {
            PropertyInfo info = GetType().GetProperty(node.Name);
            if (info != null && info.CanWrite)
            {
                if (info.PropertyType == typeof(AutoXml))
                {
                    // tự gen ở ctor
                    //if (item.Name == nameof(TTKhac))
                    //{
                    //    TTKhac = new TTKhac(item);
                    //}
                }
                else if (info.PropertyType == typeof(string))
                {
                    info.SetValue(this, node.InnerText, null);
                }
                else if (info.PropertyType == typeof(int))
                {
                    info.SetValue(this, int.Parse(node.InnerText), null);
                }
                else if (info.PropertyType == typeof(float))
                {
                    info.SetValue(this, float.Parse(node.InnerText), null);
                }
                else if (info.PropertyType == typeof(double))
                {
                    info.SetValue(this, double.Parse(node.InnerText), null);
                }
                else if (info.PropertyType == typeof(decimal))
                {
                    //info.SetValue(this, decimal.Parse(node.InnerText), null);
                    info.SetValue(this, ObjectAndString.StringToDecimal(node.InnerText), null);
                }
                else if (info.PropertyType == typeof(DateTime))
                {
                    string text = node.InnerText;
                    if (text.Contains("-"))
                    {
                        info.SetValue(this, DateTime.ParseExact(node.InnerText, "yyyy-MM-dd", null), null);
                    }
                    else
                    {
                        info.SetValue(this, ObjectAndString.StringToDate(text), null);
                    }
                    
                }
                else if (info.PropertyType == typeof(bool))
                {
                    info.SetValue(this, ObjectAndString.ObjectToBool(node.InnerText), null);
                }
            }
        }

        public void SetPropertiesValue(IDictionary<string, string> dic)
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                if (dic.ContainsKey(propertyInfo.Name) && propertyInfo.CanWrite)
                {
                    var value = dic[propertyInfo.Name];
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
                else if (dic.ContainsKey(propertyInfo.Name.ToUpper()) && propertyInfo.CanWrite)
                {
                    var value = dic[propertyInfo.Name.ToUpper()];
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
    }
}

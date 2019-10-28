using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace V6ThuePostXmlApi
{
    public class XmlConverter
    {
        #region ===== GENERATE XML =====

        /// <summary>
        /// Biến JsonObject (kiểu object trong project này) thành chuỗi xml.
        /// </summary>
        /// <param name="o">Đối tượng cần chuyển</param>
        /// <param name="objectName">Tag bao bọc object.
        /// <para>Nếu không truyền hoặc truyền null thì objectName sẽ lấy GetType().Name = tên class.</para>
        /// <para>Nếu truyền rỗng ("") thì không có tag objectName bao bọc xml trả về.</para>
        /// <para>Nếu truyền một chuỗi thì tag bao bọc object sẽ là chuỗi đó.</para>
        /// </param>
        /// <returns></returns>
        public static string ClassToXml(Object o, string objectName = null)
        {
            if (objectName == null) objectName = o.GetType().Name;
            string result = "";
            // "\n<!-- PROPERTIES -->";
            // Đối với properties sẽ có tag_name bao bọc.
            foreach (PropertyInfo property in o.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(o, null);
                    result += string.Format("\n<{0}>{1}</{0}>", property.Name, ObjectToXml(value, 1));
                }
            }

            // result += "\n\n<!-- FIELDS -->";
            // Còn field sẽ không có tag_name bao bọc.
            foreach (FieldInfo field in o.GetType().GetFields())
            {
                object value = field.GetValue(o);
                result += "\n" + ObjectToXml(value);
            }

            return objectName == "" ? result : string.Format("<{0}>\n{1}\n\n</{0}>\n", objectName, result);
        }

        /// <summary>
        /// Trả về chuỗi dữ liệu, không có râu ria gì khác.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tab">Format thụt đầu dòng, mỗi tab = \t</param>
        /// <returns></returns>
        public static string ObjectToXml(object value, int tab = 0)
        {
            //if (value == null) return "null";

            string result = "";

            if (value is IDictionary<string, object>)
            {
                result = DictionaryToXml((IDictionary<string, object>)value, tab);
            }
            else if (value is string)
            {
                result = FixXmlValueChar(value.ToString());
            }
            else if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                //DateTime now = DateTime.Now;
                //DateTime date_time = new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
                //Miliseconds from 1970
                //result = string.Format("<{0}>{1}</{0}>", name, (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds);
                //yyyy/MM/dd
                result = date.ToString("dd/MM/yyyy");
            }
            else if (value is IEnumerable)
            {
                result = ListToXml((IEnumerable)value);
            }
            else if (IsNumber(value))
            {
                if (value is decimal || value is double || value is float)
                {
                    result = Convert.ToDecimal(value).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    result = FixXmlValueChar(value.ToString());
                }
            }
            else if (value is bool)
            {
                result += (bool)value ? "true" : "false";
            }
            else// object
            {
                if (value != null) result = ClassToXml(value);
                else result = "";
            }
            
            return result;
        }

        static Dictionary<string, string> XmlChars = new Dictionary<string, string>()
        {
            {"'", "&apos;"},    // Dấu nháy đơn (')
            {"\"", "&quot;"},   // Dấu nháy kép (")
            {"&", "&amp;"},     // Dấu và       (&)
            {"<", "&lt;"},      // Dấu nhỏ hơn  (<)
            {">", "&gt;"},      // Dấu lớn hơn  (>)
            
            {"/", "&#47;"},
            {"[", "&#91;"},
            {"\\", "&#92;"},
            {"]", "&#93;"},
        }; 
        /// <summary>
        /// Sửa lỗi nội dung ký tự đặt biệt.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string FixXmlValueChar(string xml)
        {
            foreach (KeyValuePair<string, string> i in XmlChars)
            {
                xml = xml.Replace(i.Key, i.Value);
                xml = xml.Replace(i.Value + i.Value.Substring(1), i.Value);
            }
            return xml;
        }

        public static string ListToXml(IEnumerable value)
        {
            string result = "";
            int i = 0;
            foreach (object o in value)
            {
                string objectName = o.GetType().Name;
                result += string.Format("\n<!-- {0}_{1} -->", objectName , i++);
                result += string.Format("\n{0}", ObjectToXml(o));
            }
            //if (result.Length > 1) result = result.Substring(1);// bỏ \n

            return result;
        }

        /// <summary>
        /// Tạo xml danh sách &lt;name>value&lt;/name>. Nếu name truyền vào khác null thì tất cả sẽ được bao bọc bởi tag name.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tab"></param>
        /// <returns></returns>
        public static string DictionaryToXml(IDictionary<string, object> value, int tab = 0)
        {
            string result = "\n";
            string tabString = "";
            for (int i = 0; i < tab; i++)
            {
                tabString += "\t";
            }

            foreach (KeyValuePair<string, object> item in value)
            {
                result += string.Format("\n{2}<{0}>{1}</{0}>", item.Key, ObjectToXml(item.Value), tabString);
            }
            if (result.Length > 0) result = result.Substring(1);
            
            return result + "\n";
        }

        #endregion ===== GENERATE XML =====

        public static bool IsNumber(object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
}

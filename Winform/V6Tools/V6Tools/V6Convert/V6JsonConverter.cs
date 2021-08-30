using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using V6Tools.V6Objects;

namespace V6Tools.V6Convert
{
    public static class V6JsonConverter
    {
        public static string DateTimeFormat = null;
        //JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
        /// <summary>
        /// <para>Chuyển 1 đối tượng dạng Class thành json.</para>
        /// <para>Các property hoặc field sẽ là 1 phần tử bên trong json object. Ví dụ: {"Property1":"value", "Field1":"value"}</para>
        /// <para>"value" có thể là 1 "string" hoặc number hoặc {Class}</para>
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dateTimeFormat">null mặc định.</param>
        /// <returns></returns>
        public static string ClassToJson(object o, string dateTimeFormat)
        {
            var of = DateTimeFormat;
            DateTimeFormat = dateTimeFormat;
            string result = "";
            foreach (PropertyInfo property in o.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(o, null);
                    result += ",\n" + ValueToJson(property.Name, value);
                }
            }

            foreach (FieldInfo field in o.GetType().GetFields())
            {
                object value = field.GetValue(o);
                result += ",\n" + ValueToJson(field.Name, value);
            }

            if (result.Length > 0) result = result.Substring(1);
            DateTimeFormat = of;
            return "{" + result + "\n}";
        }


        private static string ValueToJson(string name, object value)
        {
            string result = "";

            result = string.Format("\"{0}\":{1}", name, ObjectToJson(value, DateTimeFormat));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        public static string ObjectToJson(object value, string dateTimeFormat)
        {
            var of = DateTimeFormat;
            DateTimeFormat = dateTimeFormat;
            if (value == null || value is DBNull) return "null";

            string result = "";

            if (value is V6JsonObject)
            {
                result = ((V6JsonObject)value).ToJson();
            }
            else if (value is IDictionary<string, object>)
            {
                result = DictionaryToJson((IDictionary<string, object>)value);
            }
            else if (value is string)
            {
                // chuyển dấu \ thành \\
                string ss = value.ToString().Replace(@"\", @"\\");
                // chuyển dấu " thành \"
                result = "\"" + ss.Replace("\"", "\\\"") + "\"";
            }
            else if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                //DateTime now = DateTime.Now;
                //DateTime date_time = new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
                if (string.IsNullOrEmpty(dateTimeFormat)) // Kiểu gửi lên hóa đơn điện tử Viettel
                {
                    result = "" + (long) (date - new DateTime(1970, 1, 1)).TotalMilliseconds;
                }
                else if (dateTimeFormat.ToUpper() == "VIETTEL")
                {
                    result = "" + (long) (date - new DateTime(1970, 1, 1, 6, 59, 0)).TotalMilliseconds;
                }
                else if (dateTimeFormat.ToUpper() == "VIETTELNOW")
                {
                    result = "" + (long)(date - new DateTime(1970, 1, 1, 7, 00, 0)).TotalMilliseconds;

                    //var newvalue = date - new DateTime(1970, 1, 1, 7, 00, 0);
                    ////newvalue = newvalue.Add(new TimeSpan(date.Hour, date.Minute, 0));
                    //newvalue = newvalue.Add(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0));
                    //result = "" + (long)newvalue.TotalMilliseconds;
                }
                else if (dateTimeFormat.ToUpper() == "VIETTEL1")
                {
                    result = "" + (long) (date - new DateTime(1970, 1, 1, 6, 59, 0)).TotalMilliseconds;
                }
                else if (dateTimeFormat.ToUpper() == "VIETTEL7")
                {
                    result = "" + (long) (date - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
                }
                else
                {
                    result = "\"" + date.ToString(dateTimeFormat) + "\"";
                }
            }
            //else if (value is Boolean)
            //{

            //}
            else if (value is IEnumerable)
            {
                result = ListToJson((IEnumerable)value);
            }
            else if (ObjectAndString.IsNumber(value))
            {
                if (value is decimal || value is double || value is float)
                {
                    result = Convert.ToDecimal(value).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    result = value.ToString();
                }
            }
            else if (value is bool)
            {
                result += (bool)value ? "true" : "false";
            }
            else if (value.GetType().IsClass) // object
            {
                result = ClassToJson(value, dateTimeFormat);
            }
            else
            {
                result = "\"" + value + "\"";
            }
            DateTimeFormat = of;
            return result;
        }

        /// <summary>
        /// Đưa 1 list hoặc mảng về dạng chuỗi json [value1,"value2",{object}...]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ListToJson(IEnumerable value)
        {
            string result = "";
            foreach (object o in value)
            {
                result += "," + ObjectToJson(o, DateTimeFormat);
            }
            if (result.Length > 0) result = result.Substring(1);
            return "[" + result + "]";
        }

        /// <summary>
        /// Chuyển 1 từ điển dữ liệu thành chuỗi json {"name":"value",...}
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string DictionaryToJson(IDictionary<string, object> value)
        {
            string result = "";
            foreach (KeyValuePair<string, object> item in value)
            {
                result += "," + ValueToJson(item.Key, item.Value);
            }
            if (result.Length > 0) result = result.Substring(1);
            return "{\n" + result + "\n}";
        }
    }
}

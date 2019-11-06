using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace V6ThuePostBkavApi.PostObjects
{
    public class JsonObjectBkav
    {
        public virtual string ToJson()
        {
            string result = "";
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(this, null);
                    result += ",\n" + ValueToJson(property.Name, value);
                }
            }

            foreach (FieldInfo field in GetType().GetFields())
            {
                object value = field.GetValue(this);
                result += ",\n" + ValueToJson(field.Name, value);
            }

            if (result.Length > 0) result = result.Substring(1);
            return "{" + result + "\n}";
        }

        private string ValueToJson(string name, object value)
        {
            string result = "";

            result = string.Format("\"{0}\":{1}", name, ObjectToJson(value));

            return result;
        }

        private string ObjectToJson(object value)
        {
            if (value == null || value is DBNull) return "\"\"";//Bkav không xử lý null.

            string result = "";
            
            if (value is JsonObjectBkav)
            {
                result = ((JsonObjectBkav)value).ToJson();
            }
            else if (value is IDictionary<string, object>)
            {
                result = DictionaryToJson((IDictionary<string, object>)value);
            }
            else if (value is string)
            {
                result = "\"" + value.ToString().Replace("\"", "\\\"") + "\"";
            }
            else if (value is DateTime)
            {
                DateTime date = (DateTime) value;
                //DateTime now = DateTime.Now;
                //DateTime date_time = new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
                //result = "" + (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds;
                result = JsonConvert.ToString(date, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.RoundtripKind);
            }
            //else if (value is Boolean)
            //{
                
            //}
            else if (value is IEnumerable)
            {
                result = ListToJson((IEnumerable)value);
            }
            else if (IsNumber(value))
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
            else
            {
                result = "\"" + value + "\"";
            }
            return result;
        }

        //public static string DToString(DateTime value)
        //{
        //    return JsonConvert.ToString(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.RoundtripKind);
        //}

        //public static string ToString(DateTime value, DateFormatHandling format, DateTimeZoneHandling timeZoneHandling)
        //{
        //    DateTime dateTime = DateTimeUtils.EnsureDateTime(value, timeZoneHandling);
        //    using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
        //    {
        //        stringWriter.Write('"');
        //        DateTimeUtils.WriteDateTimeString((TextWriter)stringWriter, dateTime, format, (string)null, CultureInfo.InvariantCulture);
        //        stringWriter.Write('"');
        //        return stringWriter.ToString();
        //    }
        //}

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

        private string ListToJson(IEnumerable value)
        {
            string result = "";
            foreach (object o in value)
            {
                result += "," + ObjectToJson(o);
            }
            if (result.Length > 0) result = result.Substring(1);
            return "[" + result + "]";
        }

        private string DictionaryToJson(IDictionary<string, object> value)
        {
            string result = "";
            foreach (KeyValuePair<string, object> item in value)
            {
                result += ",\n" + ValueToJson(item.Key, item.Value);
            }
            if (result.Length > 0) result = result.Substring(2);
            return "{\n" + result + "\n}";
        }
    }
}

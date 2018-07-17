using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace V6ThuePostManager.Viettel.PostObjects
{
    public class JsonObject
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

            

            return result;
        }

        private string ObjectToJson(object value)
        {
            if (value == null) return "null";
            if (value == DBNull.Value) return "null";

            string result = "";
            
            if (value is JsonObject)
            {
                result = ((JsonObject)value).ToJson();
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
                result = "" + (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds;
            }
            else if (value is IEnumerable)
            {
                result = ListToJson((IEnumerable)value);
            }
            else if (IsNumber(value))
            {
                result = "" + value;
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
                result += "," + ValueToJson(item.Key, item.Value);
            }
            if (result.Length > 0) result = result.Substring(1);
            return "{\n" + result + "\n}";
        }
    }
}

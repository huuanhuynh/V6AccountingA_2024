using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;



namespace V6Tools.V6Convert
{
    public static class ObjectAndString
    {
        public static DateTime ObjectToDate(object o, string dateFormat = "dd/MM/yyyy")
        {
            var result = new DateTime(1900, 1, 1);
            if (o == null) return result;
            
            switch (o.GetType().ToString())
            {
                case "System.DateTime":
                    result = (DateTime)o;
                    break;
                default:
                    try
                    {
                        result = DateTime.ParseExact(o.ToString(), dateFormat, null);
                    }
                    catch
                    {
                        
                    }
                    break;
            }
            return result;
        }

        public static bool ObjectToBool(object o)
        {
            if (o == null) return false;
            string s = o.ToString().ToLower();
            return s == "1" || s == "true" || s == "yes";
        }
        public static decimal ObjectToDecimal(object o)
        {
            var result = 0m;
            if (o == null) return result;
            switch (o.GetType().ToString())
            {
                case "System.DateTime":
                    result = ((DateTime)o).DayOfYear;
                    break;
                case "System.Byte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Decimal":
                case "System.Double":
                    result = (decimal) o;
                    break;
                default:
                    decimal.TryParse(o.ToString(), out result);
                    break;
            }
            return result;
        }
        public static int ObjectToInt(object o)
        {
            int result = 0;
            if (o == null) return result;
            switch (o.GetType().ToString())
            {
                case "System.DateTime":
                    result = ((DateTime)o).DayOfYear;
                    break;
                case "System.Byte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Decimal":
                case "System.Double":
                    result = (int)o;
                    break;
                default:
                    int.TryParse(o.ToString(), out result);
                    break;
            }
            return result;
        }
        public static string ObjectToString(object o, string dateFormat = "dd/MM/yyyy")
        {
            if (o == null) return "";
            string result = "";
            switch (o.GetType().ToString())
            {
                case "System.DateTime":
                    result = ((DateTime) o).ToString(dateFormat);
                    break;
                default:
                    result = o.ToString();
                    break;
            }
            return result;
        }

        public static DateTime StringToDate(string s, string dateFormat)
        {
            DateTime d = DateTime.MinValue;
            try
            {
                d = DateTime.ParseExact(s, dateFormat, null);
            }
            catch
            {
                
            }
            return d;
        }

        public static decimal StringToDecimal(string s)
        {
            decimal v = 0;
            decimal.TryParse(s, out v);
            return v;
        }


        public static object ObjectTo(Type type, object value)
        {
            if (type == typeof (string) || type == typeof(char))
            {
                return ObjectToString(value);
            }
            else if (type == typeof (DateTime))
            {
                return ObjectToDate(value);
            }
            else if (type == typeof (bool))
            {
                return ObjectToBool(value);
            }
            else// Kieu so
            {
                decimal d = ObjectToDecimal(value);
                int i = ObjectToInt(value);
                string stype = type.ToString();
                switch (stype)
                {   
                    case "System.Byte":
                    case"System.Nullable`1[System.Byte]":
                        return (Byte) i;
                    case "System.SByte":
                    case "System.Nullable`1[System.SByte]":
                        return (SByte) i;
                    case "System.Int32":
                    case "System.Nullable`1[System.Int32]":
                        return (Int32) i;
                    case "System.UInt32":
                    case "System.Nullable`1[System.UInt32]":
                        return (UInt32) i;
                    case "System.Int64":
                    case "System.Nullable`1[System.Int64]":
                        return (Int64) i;
                    case "System.UInt64":
                    case "System.Nullable`1[System.UInt64]":
                        return (UInt64) i;
                    case "System.Int16":
                    case "System.Nullable`1[System.Int16]":
                        return (Int16) i;
                    case "System.UInt16":
                    case "System.Nullable`1[System.UInt16]":
                        return (UInt16)i;
                        break;
                    case "System.Decimal":
                    case "System.Nullable`1[System.Decimal]":
                        return (Decimal) d;
                    case "System.Double":
                    case "System.Nullable`1[System.Double]":
                        return (Double) d;
                    case "System.Single":
                    case "System.Nullable`1[System.Single]":
                        return (Single) d;
                        break;
                    case "System.Nullable`1[System.DateTime]":
                        return ObjectToDate(value);
                        break;
                    case "System.Guid":
                        return new Guid();
                    default:
                        string ssss = type.ToString();
                        return value;
                        break;
                }
                return ObjectToInt(value);
            }
            return value;
        }
    }
}

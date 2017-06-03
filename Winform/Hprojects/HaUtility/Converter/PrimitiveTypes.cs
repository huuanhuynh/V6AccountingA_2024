using System;
using System.Globalization;

namespace HaUtility.Converter
{
    public static class PrimitiveTypes
    {
        public static string SystemDecimalSymbol = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static bool IsNumberType(Type dataType)
        {
            if (dataType == typeof(int)
                    || dataType == typeof(decimal)
                    || dataType == typeof(double)
                    || dataType == typeof(long)
                    || dataType == typeof(short)
                    || dataType == typeof(float)
                    || dataType == typeof(Int16)
                    || dataType == typeof(Int32)
                    || dataType == typeof(Int64)
                    || dataType == typeof(uint)
                    || dataType == typeof(UInt16)
                    || dataType == typeof(UInt32)
                    || dataType == typeof(UInt64)
                    || dataType == typeof(byte)
                    || dataType == typeof(sbyte)
                    || dataType == typeof(Single))
            {
                return true;
            }
            return false;
        }

        public static string StringToSystemDecimalSymbolStringNumber(string numberString)
        {
            return numberString.Replace(",", SystemDecimalSymbol).Replace(".", SystemDecimalSymbol);
        }

        /// <summary>
        /// Trả về DateTime?
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static DateTime? ObjectToDate(object o, string dateFormat = "dd/MM/yyyy")
        {
            var result = DateTime.Now;
            if (o == null) return null;
            if (o == DBNull.Value) return null;
            
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
                        // ignored
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
            if (o is DBNull) return 0;
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
                    result = Convert.ToDecimal(o);
                    break;
                default:
                    decimal.TryParse(o.ToString().Replace(",",SystemDecimalSymbol).Replace(".",SystemDecimalSymbol), out result);
                    //decimal.TryParse(o.ToString(), NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture, out result);
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
                case "System.Char":
                case "System.Byte":
                case "System.SByte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.UInt16":
                case "System.UInt32":
                case "System.UInt64":
                case "System.Decimal":
                case "System.Double":
                case "System.Single":
                    result = Convert.ToInt32(o);
                    break;
                default:
                    int.TryParse(o.ToString(), out result);
                    break;
            }
            return result;
        }

        public static float ObjectToFloat(object o)
        {
            return ToObject<float>(o);
        }

        public static string ObjectToString(object o, string dateFormat = "dd/MM/yyyy")
        {
            if (o == null || o == DBNull.Value) return "";
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

        public static DateTime StringToDate(string s, string dateFormat = "dd/MM/yyyy")
        {
            DateTime d = DateTime.MinValue;
            try
            {
                d = DateTime.ParseExact(s, dateFormat, null);
            }
            catch
            {
                // ignored
            }
            return d;
        }

        public static decimal StringToDecimal(string s)
        {
            decimal v = 0;
            s = s.Replace(",", SystemDecimalSymbol).Replace(".", SystemDecimalSymbol);
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
                if (value == null || value == "" || value == DBNull.Value)
                    return DateTime.Now;
                    //return DBNull.Value;
                return ObjectToDate(value);
            }
            else if (type == typeof (bool))
            {
                return ObjectToBool(value);
            }
            else// Kieu so
            {
                decimal d = ObjectToDecimal(value);
                string stype = type.ToString();
                switch (stype)
                {   
                    case "System.Byte":
                    case"System.Nullable`1[System.Byte]":
                        return Convert.ToByte(d);
                    case "System.SByte":
                    case "System.Nullable`1[System.SByte]":
                        return Convert.ToSByte(d);
                    case "System.Int32":
                    case "System.Nullable`1[System.Int32]":
                        return Convert.ToInt32(d);
                    case "System.UInt32":
                    case "System.Nullable`1[System.UInt32]":
                        return Convert.ToUInt32(d);
                    case "System.Int64":
                    case "System.Nullable`1[System.Int64]":
                        return Convert.ToInt64(d);
                    case "System.UInt64":
                    case "System.Nullable`1[System.UInt64]":
                        return Convert.ToUInt64(d);
                    case "System.Int16":
                    case "System.Nullable`1[System.Int16]":
                        return Convert.ToInt16(d);
                    case "System.UInt16":
                    case "System.Nullable`1[System.UInt16]":
                        return Convert.ToUInt16(d);
                    case "System.Decimal":
                    case "System.Nullable`1[System.Decimal]":
                        return Convert.ToDecimal(d);
                    case "System.Double":
                    case "System.Nullable`1[System.Double]":
                        return Convert.ToDouble(d);
                    case "System.Single":
                    case "System.Nullable`1[System.Single]":
                        return Convert.ToSingle(d);
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

        public static T ToObject<T>(object o) where T:new()
        {
            return (T)ObjectTo(typeof(T), o);
        }
    }
}

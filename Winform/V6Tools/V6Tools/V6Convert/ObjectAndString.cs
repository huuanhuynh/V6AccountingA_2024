using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace V6Tools.V6Convert
{
    public static class ObjectAndString
    {
        public static string SystemDecimalSymbol = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;


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

        public static bool IsStringType(Type dataType)
        {
            if (dataType == typeof(string))
            {
                return true;
            }
            return false;
        }

        public static bool IsDateTimeType(Type dataType)
        {
            if (dataType == typeof(DateTime))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Chuyển số thành chuỗi có định dạng
        /// </summary>
        /// <param name="number">Giá trị</param>
        /// <param name="decimals">Số chữ số phần thập phân</param>
        /// <param name="decimalSeparator">Dấu cách phần thập phân</param>
        /// <param name="thousandSeparator">Dấu cách phần nghìn</param>
        /// <returns></returns>
        public static string NumberToString(decimal number, int decimals, string decimalSeparator, string thousandSeparator = " ")
        {
            if (string.IsNullOrEmpty(decimalSeparator))
            {
                throw new Exception("DecimalSeparator empty.");
            }

            string N0 = "N";
            if (decimals >= 0) N0 += decimals;
            var numberString = number.ToString(N0, CultureInfo.InvariantCulture);//= 19,950,000.00
            numberString = numberString.Replace(".", "#");
            numberString = numberString.Replace(",", thousandSeparator);
            numberString = numberString.Replace("#", decimalSeparator);
            return numberString;
        }

        /// <summary>
        /// Chuyển số thành chuỗi có định dạng
        /// </summary>
        /// <param name="number">Giá trị</param>
        /// <param name="decimals">Số chữ số phần thập phân</param>
        /// <param name="decimalSeparator">Dấu cách phần thập phân</param>
        /// <param name="thousandSeparator">Dấu cách phần nghìn</param>
        /// <returns></returns>
        public static string NumberToString(object number, int decimals, string decimalSeparator,string thousandSeparator = " ")
        {
            return NumberToString(ObjectToDecimal(number), decimals, decimalSeparator, thousandSeparator);
        }

        /// <summary>
        /// <para>Chuyển chuỗi số có dấu cách thập phân về đúng dấu . hoặc , theo hệ thống để dùng hàm Convert.</para>
        /// <para>.</para>
        /// </summary>
        /// <param name="numberString">Chuỗi số</param>
        /// <returns>Trả về chuổi số không còn dấu cách phần nghìn, dấu thập phân theo hệ thống máy tính.</returns>
        public static string StringToSystemDecimalSymbolStringNumber(string numberString)
        {
            numberString = numberString.Replace(" ", "");
            var indexOfComma = numberString.IndexOf(",", StringComparison.Ordinal);
            var indexOfDot = numberString.IndexOf(".", StringComparison.Ordinal);
            if (indexOfComma > 0 && indexOfComma < indexOfDot)
            {
                numberString = numberString.Replace(",", "");
            }
            else if (indexOfDot > 0 && indexOfDot < indexOfComma)
            {
                numberString = numberString.Replace(".", "");
            }
            return numberString.Replace(",", SystemDecimalSymbol).Replace(".", SystemDecimalSymbol);
        }

        /// <summary>
        /// Trả về DateTime?
        /// </summary>
        /// <param name="o">chuỗi ngày phải có định dạng dd/MM/yyyy</param>
        /// <param name="dateFormat">Không còn tác dụng</param>
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
                        //result = DateTime.ParseExact(o.ToString().Trim(), dateFormat, null);

                        var t = o.ToString().Trim();
                        t = t.Replace("_", "");
                        var index1 = t.IndexOf('/');
                        var index2 = t.LastIndexOf('/');
                        var yearString = t.Substring(index2 + 1);
                        var monthString = t.Substring(index1 + 1, index2 - index1 - 1);
                        var dayString = t.Substring(0, index1);
                        
                        var day = int.Parse(dayString);
                        var month = int.Parse(monthString);
                        var year = int.Parse(yearString);
                        result = new DateTime(year, month, day);
                    }
                    catch
                    {
                        if (dateFormat != null && dateFormat.Length > 1)
                        {
                            if (!DateTime.TryParseExact(o.ToString().Trim(), dateFormat, null, DateTimeStyles.None, out result)) return null;
                        }
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Convert ngày tháng không null.
        /// </summary>
        /// <param name="o">chuỗi ngày phải có định dạng dd/MM/yyyy</param>
        /// <param name="dateFormat">Không còn tác dụng</param>
        /// <returns></returns>
        public static DateTime ObjectToFullDateTime(object o, string dateFormat = "dd/MM/yyyy")
        {
            var date = ObjectToDate(o, dateFormat);
            if (date == null) return DateTime.Now;
            return (DateTime) date;
        }

        /// <summary>
        /// 1/true/yes
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool ObjectToBool(object o)
        {
            if (o is DBNull) return false;
            if (o == null) return false;
            string s = o.ToString().Trim().ToLower();
            return s == "1" || s == "true" || s == "yes";
        }
        public static decimal ObjectToDecimal(object o)
        {
            var result = 0m;
            if (o is DBNull) return 0;
            if (o == null) return result;
            if (o is bool)
            {
                return (bool) o ? 1 : 0;
            }
            if (o is decimal)
            {
                result = (decimal)o;
                return result;
            }
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
                    decimal.TryParse(StringToSystemDecimalSymbolStringNumber(o.ToString()), out result);
                    break;
            }
            return result;
        }

        public static int ObjectToInt(object o)
        {
            decimal result = 0;
            if (o is DBNull) return 0;
            if (o == null) return 0;
            if (o is bool)
            {
                return (bool)o ? 1 : 0;
            }
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
                    decimal.TryParse(StringToSystemDecimalSymbolStringNumber(o.ToString()), out result);
                    break;
            }
            return (int)result;
        }

        public static long ObjectToInt64(object o)
        {
            long result = 0;
            if (o is DBNull) return 0;
            if (o == null) return 0;
            if (o is bool)
            {
                return (bool)o ? 1 : 0;
            }
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
                    result = Convert.ToInt64(o);
                    break;
                default:
                    long.TryParse(StringToSystemDecimalSymbolStringNumber(o.ToString()), out result);
                    break;
            }
            return result;
        }

        /// <summary>
        /// chuyển một object thành chuỗi hiển thị. (chưa có kiểu số)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dateFormat">Chỉ có tác dụng khi o đưa vào kiểu ngày. Cần update thêm cho kiểu số</param>
        /// <returns></returns>
        public static string ObjectToString(object o, string dateFormat = "dd/MM/yyyy")
        {
            if (o == null || o == DBNull.Value) return "";
            string result = "";
            Type t = o.GetType();
            //if (IsNumberType(t))
            //{
            //    //Chua xu ly.
            //    if (t == typeof(decimal) || t == typeof(double) || t == typeof(float))
            //    {
            //        result = NumberToString(ObjectToDecimal(o), 2, ",", " ");
            //    }
            //    else
            //    {
            //        result = o.ToString();
            //    }
            //}
            //if (result != "") return result;

            if (o is List<string>)
            {
                var lo = (List<string>) o;
                foreach (string s in lo)
                {
                    result += ";" + s;
                }
                if (result.Length > 1) result = result.Substring(1);
                return result;
            }

            switch (t.ToString())
            {
                case "System.DateTime":
                    result = ((DateTime) o).ToString(dateFormat);
                    if (!dateFormat.Contains(" "))
                    {
                        result = result.Replace(" ", "");
                    }
                    break;
                default:
                    result = ObjectToXml(o);
                    break;
            }
            return result;
        }

        public static DateTime StringToDate(string s, string dateFormat = "d/M/yyyy")
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
            decimal.TryParse(StringToSystemDecimalSymbolStringNumber(s), out v);
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
                if (value == null || value == "" || value == DBNull.Value) return DBNull.Value;
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
                    case "System.Nullable`1[System.Byte]":
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
                        break;
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

        public static T ToObject<T>(object o)// where T:new()
        {
            return (T)ObjectTo(typeof(T), o);
        }

        /// <summary>
        /// So sánh 2 giá trị
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="oper"></param>
        /// <param name="value"></param>
        /// <param name="any_type">Bỏ qua kiểu, chuyển kiểu value về giống obj. Ví dụ giá trị 1 và chuỗi "1" là bằng nhau.</param>
        /// <returns></returns>
        public static bool CheckCondition(object obj, string oper, object value, bool any_type)
        {
            if (any_type)
            {
                return CheckCondition(obj, oper, ObjectTo(obj.GetType(), value));
            }
            else
            {
                return CheckCondition(obj, oper, value);
            }
        }
        
        /// <summary>
        /// So sánh 2 giá trị
        /// </summary>
        /// <param name="obj">Giá trị cần so sánh.</param>
        /// <param name="oper">Biểu thức so sánh.</param>
        /// <param name="value">So với giá trị này.</param>
        /// <returns></returns>
        public static bool CheckCondition(object obj, string oper, object value)
        {
            if (oper == "=") return Equals(obj, value);
            if (oper == "<>") return !Equals(obj, value);
            if (oper == ">")
            {
                if (IsNumberType(obj.GetType()))
                {
                    return ObjectToDecimal(obj) > ObjectToDecimal(value);
                }
                else
                {
                    return String.CompareOrdinal(obj.ToString().Trim(), value.ToString().Trim()) > 0;
                }
            }
            if (oper == ">=")
            {
                if (IsNumberType(obj.GetType()))
                {
                    return ObjectToDecimal(obj) >= ObjectToDecimal(value);
                }
                else
                {
                    return String.CompareOrdinal(obj.ToString().Trim(), value.ToString().Trim()) >= 0;
                }
            }
            if (oper == "<")
            {
                if (IsNumberType(obj.GetType()))
                {
                    return ObjectToDecimal(obj) < ObjectToDecimal(value);
                }
                else
                {
                    return String.CompareOrdinal(obj.ToString().Trim(), value.ToString().Trim()) < 0;
                }
            }
            if (oper == "<=")
            {
                if (IsNumberType(obj.GetType()))
                {
                    return ObjectToDecimal(obj) <= ObjectToDecimal(value);
                }
                else
                {
                    return String.CompareOrdinal(obj.ToString().Trim(), value.ToString().Trim()) <= 0;
                }
            }
            return false;
        }

        /// <summary>
        /// Tách chuỗi thành mảng bằng ; hoặc ,
        /// </summary>
        /// <param name="s">Chuỗi cần tách</param>
        /// <param name="removeEmptyEntries">Tùy chọn bỏ phần tử rỗng, mặc định bỏ</param>
        /// <returns></returns>
        public static string[] SplitString(string s, bool removeEmptyEntries = true)
        {
            return string.IsNullOrEmpty(s) ? new string[]{} : s.Split(s.Contains(";") ? new []{';'} : new []{','},
                removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }
        
        public static string[] SplitStringBy(string s, char splitChar, bool removeEmptyEntries = true)
        {
            return string.IsNullOrEmpty(s) ? new string[]{} : s.Split(new []{splitChar},
                removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

        public static float StringToFloat(string s)
        {
            return ToObject<float>(s);
        }

        /// <summary>
        /// Chuyển chuỗi R,G,B hoặc #FFFFFF hoặc ColorName(ex:Red) thành màu.
        /// </summary>
        /// <param name="rgb">R,G,B hoặc #XXXXXX hoặc Red</param>
        /// <returns></returns>
        public static Color StringToColor(string rgb)
        {
            int r = 0, g = 0, b = 0;
            if (rgb != null && rgb.Contains(","))
            {
                string[] sss = rgb.Split(',');
                r = ObjectToInt(sss[0]);
                if (sss.Length > 1) g = ObjectToInt(sss[1]);
                if (sss.Length > 2) b = ObjectToInt(sss[2]);
            }
            else if (rgb != null && rgb.StartsWith("#"))
            {
                var convert_from_string = (new ColorConverter()).ConvertFromString(rgb);
                if (convert_from_string != null)
                {
                    Color color = (Color) convert_from_string;
                    return color;
                }
            }
            else
            {
                try
                {
                    if (rgb != null) return Color.FromName(rgb);
                    else return Color.Empty;
                }
                catch (Exception)
                {
                    //
                }
            }
            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            return Color.FromArgb(r, g, b);
        }
        
        public static DataSet XmlStringToDataSet(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml)) return null;
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xml));
                return ds;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("XmlStringToDataSet: " + xml, ex);
                return null;
            }
        }

        /// <summary>
        /// Cash về IDictionary, không null.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ObjectToDictionary(object tag)
        {
            try
            {
                if (tag is IDictionary<string, object>) return (IDictionary<string, object>) tag;
                if (tag is string) return StringToDictionary(tag.ToString());
            }
            catch (Exception)
            {
                
            }
            return new Dictionary<string, object>();
        }

        private static IDictionary<string, object> StringToDictionary(string tag, char group_char = ';', char element_char = ':')
        {
            Dictionary<string,object> result = new Dictionary<string, object>();
            string[] sss = string.IsNullOrEmpty(tag) ? new string[]{} : tag.Split(new []{group_char}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in sss)
            {
                string[] ss = s.Split(new[] { element_char }, StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length == 1)
                {
                    result.Add("VALUE", ss[0]);
                }
                else if (ss.Length > 1)
                {
                    result.Add(ss[0].Trim().ToUpper(), ss[1]);
                }
            }
            return result;
        }

        /// <summary>
        /// Loại bỏ tất cả các ký tự khoảng trắng và khoảng trắng đặc biệt ở đầu chuỗi. Loại bỏ ký tự đặc biệt bên trong chuỗi.
        /// </summary>
        /// <param name="toString"></param>
        /// <param name="moreSpecialChars"></param>
        /// <returns></returns>
        public static string TrimStartSpecial(string toString, string moreSpecialChars = null)
        {
            string specialChars = "\r﻿　" + moreSpecialChars;//"A◀﻿▶"//Sau \r là \u65279, là ký tự giữa 2 tam giác ◀﻿▶, và sau đó là \u12288
            foreach (char special_char in specialChars)
            {
                toString = toString.Replace("" + special_char, "");
            }
            //Loại bỏ khoảng trắng đầu chuỗi.
            while (toString.Length>0 && char.IsWhiteSpace(toString[0]))
            {
                toString = toString.Substring(1);
            }
            
            return toString;
        }

        /// <summary>
        /// Loại bỏ tất cả các ký tự khoảng trắng và khoảng trắng đặc biệt ở đầu và cuối chuỗi. Loại bỏ ký tự đặc biệt bên trong chuỗi.
        /// </summary>
        /// <param name="toString"></param>
        /// <param name="moreSpecialChars"></param>
        /// <returns></returns>
        public static string TrimSpecial(string toString, string moreSpecialChars = null)
        {
            string specialChars = "\r﻿　" + moreSpecialChars;//"A◀﻿▶"//Sau \r là \u65279, là ký tự giữa 2 tam giác ◀﻿▶, và sau đó là \u12288
            foreach (char special_char in specialChars)
            {
                toString = toString.Replace("" + special_char, "");
            }
            for (int i = toString.Length - 1; i >= 0; i--)
            {
                char i_char = toString[i];
                if (char.IsWhiteSpace(i_char))
                {
                    toString = toString.Replace("" + i_char, "");
                    if (i > toString.Length) i = toString.Length;
                }
            }
            //toString = toString.Trim();
            
            return toString;
        }


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
            try
            {
                if (objectName == null) objectName = o.GetType().Name;
                string result = "";

                if (o is Control)
                {
                    var c = (Control)o;
                    result += string.Format("Text:{0}, Value:{1}", c.Text, GetObjectProperty(c, "Value"));

                    return result;
                }


                // "\n<!-- PROPERTIES -->";
                // Đối với properties sẽ có tag_name bao bọc.
                foreach (PropertyInfo property in o.GetType().GetProperties())
                {
                    if (property.CanRead && property.CanWrite)
                    {
                        object value = property.GetValue(o, null);
                        if (value != null)
                            result += string.Format("\n<{0}>{1}</{0}>", property.Name, ObjectToXml(value, 1));
                    }
                }

                // result += "\n\n<!-- FIELDS -->";
                // Còn field sẽ không có tag_name bao bọc.
                int field_count = 0;
                foreach (FieldInfo field in o.GetType().GetFields())
                {
                    field_count++;
                    object value = null;
                    if (!(o is DBNull)) value = field.GetValue(o);
                    if (value != null)
                        result += "\n" + value;

                    if (field_count == 10) break;
                }

                if (result == "") result = o.ToString();

                return objectName == "" ? result : string.Format("<{0}>\n{1}\n</{0}>\n", objectName, result);
            }
            catch (Exception ex)
            {
                return string.Format("<{0}:ex>\n{1}\n</{0}:ex>\n", objectName, ex.Message);
            }
        }

        public static object GetObjectProperty(object o, string propertyName)
        {
            object result = null;

            var pi = o.GetType().GetProperty(propertyName);
            if (pi != null && pi.CanRead)
            {
                result = pi.GetValue(o, null);
                return result;
            }
            var fi = o.GetType().GetField(propertyName);
            if (fi != null)
            {
                if (!(o is DBNull)) result = fi.GetValue(o);
                return result;
            }

            foreach (PropertyInfo property in o.GetType().GetProperties())
            {
                if ((string.Compare(property.Name, propertyName, StringComparison.InvariantCultureIgnoreCase) == 0) && property.CanRead)
                {
                    result = property.GetValue(o, null);
                    return result;
                }
            }

            foreach (FieldInfo field in o.GetType().GetFields())
            {
                if (string.Compare(field.Name, propertyName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    if (!(o is DBNull)) result = field.GetValue(o);
                    return result;
                }
            }
            
            //throw new Exception("Property not found!");
            return result;
        }

        /// <summary>
        /// Trả về chuỗi dữ liệu, không có râu ria gì khác.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tab">Format thụt đầu dòng, mỗi tab = \t</param>
        /// <returns></returns>
        public static string ObjectToXml(object value, int tab = 0)
        {
            string result = "";

            if (value is DataTable)
            {
                result = DataTableToXml((DataTable) value);
            }
            else if (value is IDictionary<string, object>)
            {
                result = DictionaryToXml((IDictionary<string, object>)value, tab);
            }
            else if (value is string || value is DBNull || value == null)
            {
                result = "" + value;
                //result = FixXmlValueChar(value.ToString());
            }
            else if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                //DateTime now = DateTime.Now;
                //DateTime date_time = new DateTime(date.Year, date.Month, date.Day, now.Hour, now.Minute, now.Second);
                //Miliseconds from 1970
                //result = string.Format("<{0}>{1}</{0}>", name, (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds);
                //yyyy/MM/dd
                result = date.ToString("dd/MM/yyyy").Replace(" ", "");
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
                    result = value.ToString();
                    //result = FixXmlValueChar(value.ToString());
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
            //{"'", "&apos;"},    // Dấu nháy đơn (') &#39;
            //{"\"", "&quot;"},   // Dấu nháy kép (") &#34;
            {"&", "&amp;"},     // Dấu và       (&)
            {"<", "&lt;"},      // Dấu nhỏ hơn  (<)
            {">", "&gt;"},      // Dấu lớn hơn  (>)
            
            //{"+", "&#43;"},
            //{"/", "&#47;"},
            //{"[", "&#91;"},
            //{"\\", "&#92;"},
            //{"]", "&#93;"},
        };
        /// <summary>
        /// Sửa lỗi nội dung ký tự đặt biệt.
        /// </summary>
        /// <param name="xmlValue"></param>
        /// <returns></returns>
        private static string FixXmlValueChar(string xmlValue)
        {
            foreach (KeyValuePair<string, string> i in XmlChars)
            {
                xmlValue = xmlValue.Replace(i.Key, i.Value);
                xmlValue = xmlValue.Replace(i.Value + i.Value.Substring(1), i.Value);
            }
            return xmlValue;
        }

        public static string ListToXml(IEnumerable value)
        {
            string result = "";
            int i = 0;
            foreach (object o in value)
            {
                string objectName = o.GetType().Name;
                result += string.Format("\n<!-- {0}_{1} -->", objectName, i++);
                result += string.Format("\n{0}", ObjectToXml(o));
            }
            //if (result.Length > 1) result = result.Substring(1);// bỏ \n

            return result;
        }

        public static string DataTableToXml(DataTable data)
        {
            string result = "";
            result += string.Format("[DataTable] have {0} row[s].", data.Rows.Count);
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
    }
}

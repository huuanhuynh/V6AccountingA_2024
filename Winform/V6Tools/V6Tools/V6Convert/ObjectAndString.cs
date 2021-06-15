using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
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

        public static bool IsDateTime(object value)
        {
            if (value is DateTime
            || value is DateTime? && value != null)
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
        /// <param name="show0">Mặc định không hiện số 0</param>
        /// <returns></returns>
        public static string NumberToString(decimal number, int decimals, string decimalSeparator, string thousandSeparator = " ", bool show0 = false)
        {
            if (number == 0 && !show0) return "";
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
        /// <param name="show0">Mặc định không hiện số 0</param>
        /// <returns></returns>
        public static string NumberToString(object number, int decimals, string decimalSeparator, string thousandSeparator = " ", bool show0 = false)
        {
            return NumberToString(ObjectToDecimal(number), decimals, decimalSeparator, thousandSeparator, show0);
        }

        /// <summary>
        /// <para>Chuyển chuỗi số có dấu cách thập phân về đúng dấu . hoặc , theo hệ thống để dùng hàm Convert.</para>
        /// <para>.</para>
        /// </summary>
        /// <param name="numberString">Chuỗi số</param>
        /// <returns>Trả về chuổi số không còn dấu cách phần nghìn, dấu thập phân theo hệ thống máy tính.</returns>
        public static string StringToSystemDecimalSymbolStringNumber(string numberString)
        {
            numberString = "" + numberString;
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
        /// <param name="dateFormat">Chuỗi định dạng ngày.</param>
        /// <returns></returns>
        public static DateTime? ObjectToDate(object o, string dateFormat = "dd/MM/yyyy")
        {
            DateTime? result = null;
            if (o == null) return null;
            if (o == DBNull.Value) return null;
            if (o.ToString().Trim() == string.Empty) return null;
            
            switch (o.GetType().ToString())
            {
                case "System.DateTime":
                    result = (DateTime)o;
                    break;
                default:
                    try
                    {
                        var t = o.ToString().Trim();
                        var index1 = t.IndexOf('/');
                        var index2 = t.LastIndexOf('/');
                        double d0 = 0d;
                        if (index1 == -1 && t.Length < 10 && double.TryParse(t, out d0))
                        {
                            try
                            {
                                var d = DateTime.FromOADate(d0);
                                return d.Date;
                            }
                            catch
                            {
                                
                            }
                        }

                        if (string.IsNullOrEmpty(dateFormat) || dateFormat.ToUpper() == "DD/MM/YYYY")
                        {
                            t = t.Replace("_", "");
                            
                            var yearString = t.Substring(index2 + 1);
                            var monthString = t.Substring(index1 + 1, index2 - index1 - 1);
                            var dayString = t.Substring(0, index1);

                            var day = int.Parse(dayString);
                            var month = int.Parse(monthString);
                            var year = int.Parse(yearString);
                            result = new DateTime(year, month, day);
                            return result;
                        }

                        if (dateFormat.Length > 1)
                        {
                            DateTime result1;
                            if (DateTime.TryParseExact(o.ToString().Trim(), dateFormat, null, DateTimeStyles.None, out result1))
                                return result1;
                        }
                        // if TryParseExact failed.
                        {
                            // parse dd/MM/yyyy.
                            t = t.Replace("_", "");
                            
                            var yearString = t.Substring(index2 + 1);
                            var monthString = t.Substring(index1 + 1, index2 - index1 - 1);
                            var dayString = t.Substring(0, index1);

                            var day = int.Parse(dayString);
                            var month = int.Parse(monthString);
                            var year = int.Parse(yearString);
                            result = new DateTime(year, month, day);
                            return result;
                        }
                    }
                    catch
                    {
                        try
                        {
                            var d = DateTime.FromOADate(Convert.ToDouble(o));
                            return d.Date;
                        }
                        catch
                        {
                            
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
            if (ObjectToDecimal(o) == 1m) return true;
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
        /// chuyển một object thành chuỗi hiển thị.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="formatString">vd dd/MM/yyyy  (hoặc cho kiểu số N2)</param>
        /// <returns></returns>
        public static string ObjectToString(object o, string formatString = null)
        {
            if (o == null || o == DBNull.Value) return "";
            string result = "";
            Type t = o.GetType();
            if (IsNumberType(t))
            {
                var value = ObjectToDecimal(o);
                if (string.IsNullOrEmpty(formatString))
                {
                    result = value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    result = value.ToString(formatString, CultureInfo.InvariantCulture);
                }
                return result;
                //if (t == typeof(decimal) || t == typeof(double) || t == typeof(float))
                //{
                //    result = NumberToString(ObjectToDecimal(o), 2, ",", " ");
                //}
                //else
                //{
                //    result = o.ToString();
                //}
            }
            
            
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
            else if (o is Guid)
            {
                result = o.ToString();
                return result;
            }

            switch (t.ToString())
            {
                case "System.DateTime":
                    if (string.IsNullOrEmpty(formatString)) formatString = "dd/MM/yyyy";
                    DateTime date_object = (DateTime) o;
                    result = date_object.ToString(formatString);
                    if (!formatString.Contains(" "))
                    {
                        result = result.Replace(" ", "");
                    }

                    if (formatString.Contains("/") && !result.Contains("/"))
                    {
                        string result0 = formatString;
                        result0 = result0.Replace("dd", date_object.Day.ToString("00"));
                        result0 = result0.Replace("d", date_object.Day.ToString("00"));
                        result0 = result0.Replace("MM", date_object.Month.ToString("00"));
                        result0 = result0.Replace("M", date_object.Month.ToString("00"));
                        result0 = result0.Replace("yyyy", date_object.Year.ToString("0000"));
                        result0 = result0.Replace("yy", date_object.Year.ToString("00"));

                        result0 = result0.Replace("HH", date_object.ToString("HH"));
                        result0 = result0.Replace("H", date_object.ToString("H"));
                        result0 = result0.Replace("hh", date_object.ToString("hh"));
                        result0 = result0.Replace("h", date_object.ToString("h"));
                        result0 = result0.Replace("mm", date_object.ToString("mm"));
                        result0 = result0.Replace("m", date_object.ToString("m"));
                        result0 = result0.Replace("ss", date_object.ToString("ss"));
                        result0 = result0.Replace("s", date_object.ToString("s"));

                        result = result0;
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
            if (string.IsNullOrEmpty(s)) return v;
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

        public static T ToClass<T>(this IDictionary<string, object> dataDic0) where T : new()
        {
            var t = new T();
            var dataDic = dataDic0.ToUpperKeys();
            foreach (PropertyInfo propertyInfo in t.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    //object value = GetFormValue(container, propertyInfo.Name);
                    object o = "";
                    if (dataDic.ContainsKey(propertyInfo.Name.ToUpper()))
                        o = dataDic[propertyInfo.Name.ToUpper()];
                    var value = ObjectTo(propertyInfo.PropertyType, o);
                    try
                    {
                        propertyInfo.SetValue(t, value, null);
                    }
                    catch
                    {

                    }
                }
            }
            return t;
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

        public static string[] SplitStringBy(string s, char[] splitChars, bool removeEmptyEntries = true)
        {
            return string.IsNullOrEmpty(s) ? new string[] { } : s.Split(splitChars,
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

        public static IDictionary<string, object> StringToDictionary(string tag, char group_char = ';', char element_char = ':')
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
        
        public static IDictionary<string, string> StringToStringDictionary(string tag, char group_char = ';', char element_char = ':')
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] sss = string.IsNullOrEmpty(tag) ? new string[]{} : tag.Split(new []{group_char}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in sss)
            {
                int index = s.IndexOf(element_char);
                if (index > 0)
                {
                    result.Add(s.Substring(0, index), s.Substring(index+1));
                }
                else
                {
                    result.Add(s, s);
                }
                //string[] ss = s.Split(new[] { element_char }, StringSplitOptions.RemoveEmptyEntries);
                //if (ss.Length == 1)
                //{
                //    result.Add("VALUE", ss[0]);
                //}
                //else if (ss.Length > 1)
                //{
                //    result.Add(ss[0].Trim().ToUpper(), ss[1]);
                //}
            }
            return result;
        }

        /// <summary>
        /// Loại bỏ tất cả các ký tự khoảng trắng và khoảng trắng đặc biệt ở đầu chuỗi. Loại bỏ ký tự đặc biệt bên trong chuỗi.
        /// <para>Ký tự đặc biệt đang biết: \u65279 \u12288</para>
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
        /// Loại bỏ tất cả các ký tự xuống dòng, khoảng trắng và khoảng trắng đặc biệt ở đầu và cuối chuỗi. Loại bỏ ký tự đặc biệt bên trong chuỗi.
        /// <para>Ký tự đặc biệt đang biết: \u65279 \u12288</para>
        /// </summary>
        /// <param name="toString"></param>
        /// <param name="moreSpecialChars"></param>
        /// <returns></returns>
        public static string TrimSpecial(string toString, string moreSpecialChars = null)
        {
            string specialChars = "﻿　" + moreSpecialChars;//"A◀﻿▶"//specialChars chứa \u65279, là ký tự giữa 2 tam giác ◀﻿▶, và sau đó là \u12288
            foreach (char special_char in specialChars)
            {
                toString = toString.Replace("" + special_char, "");
            }
            toString = toString.Replace("\r\n", "");
            toString = toString.Replace("\n", "");
            toString = toString.Replace("\r", "");
            while (toString.Length > 0 && char.IsWhiteSpace(toString[0]))
            {
                toString = toString.Substring(1);
            }
            while (toString.Length > 0 && char.IsWhiteSpace(toString[toString.Length-1]))
            {
                toString = toString.Substring(0, toString.Length-1);
            }
            //for (int i = toString.Length - 1; i >= 0; i--)
            //{
            //    char i_char = toString[i];
            //    if (char.IsWhiteSpace(i_char))
            //    {
            //        toString = toString.Replace("" + i_char, "");
            //        if (i > toString.Length) i = toString.Length;
            //    }
            //}
            toString = toString.Trim();
            
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

        /// <summary>
        /// Kết hợp kiểm tra IsNullOrEmpty, khoảng trắng và 0
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNoValue(object o)
        {
            if (o == null) return true;
            if (o is string)
            {
                return "" == o.ToString().Trim();
            }

            if (IsNumberType(o.GetType()))
            {
                return 0 == ObjectToDecimal(o);
            }
            
            return "" == o.ToString().Trim();
        }


        static string ABC = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static int ABC_lengh = ABC.Length;
        static char GetABC(char A, int l, int i)
        {
            int i1 = (A + l*i + i) % ABC.Length;
            return ABC[i1];
        }

        static char GetChar(char A)
        {
            return ABC[A % ABC_lengh];
        }

        static string HConvert(string input)
        {
            string result = "";
            foreach (char c in input)
            {
                result += GetChar(c);
            }

            return result;
        }
        static char Dichj(char A, int d)
        {
            int A_index = ABC.IndexOf(A);
            int D_index = A_index + d;
            while (D_index<0)
            {
                D_index += ABC_lengh;
            }
            return ABC[D_index % ABC_lengh];
        }
        /// <summary>
        /// Tạo chuỗi mã khác biệt.
        /// </summary>
        /// <param name="input">Dữ liệu vào.</param>
        /// <param name="length_out">Độ dài chuỗi ra.</param>
        /// <returns></returns>
        public static string Hash1(string input, int length_out)
        {
            string result0 = "";
            char[] result1 = new char[length_out];
            // Tạo chuỗi ban đầu.
            for (int i = 0; i < length_out; i++)
            {
                result1[i] = GetABC(input[(i+length_out) % input.Length], length_out, i);
                result0 += result1[i];
            }
            // Xáo trộn (hash)
            for (int i = 0; i < length_out; i++)
            {
                result1[i] = ABC[(ABC.IndexOf(result1[i]) + input[(length_out - i)%input.Length]) % ABC_lengh];
            }

            return new string(result1);
        }

        public static string Hash2(string input0, int length_out)
        {
            string input_convert = HConvert(input0);
            string out_0 = "";
            if (input_convert.Length < length_out)
            {
                out_0 = input_convert;
                while (out_0.Length<length_out)
                {
                    out_0 += input_convert.Length;
                }

                if (out_0.Length > length_out)
                {
                    out_0 = out_0.Substring(0, length_out);
                }
            }
            else
            {
                char[] list_out_0 = new char[length_out];

                out_0 = input_convert.Substring(0, length_out);
                list_out_0 = out_0.ToCharArray();
                string rest = input_convert.Substring(length_out);
                for (int i = 0; i < rest.Length; i++)
                {
                    char c = rest[i];
                    // dịch out0
                    list_out_0[i] = Dichj(list_out_0[i], c);
                }
                out_0 = new string(list_out_0);
            }

            // XOR
            char[] list_out_1 = out_0.ToCharArray();
            for (int i = 0; i < length_out; i++)
            {
                int dich_x = list_out_1[i] ^ i ^ length_out;
                //if (dich_x % 2 == 0)
                    list_out_1[i] = Dichj(list_out_1[i], dich_x);
                //else list_out_1[i] = Dichj(list_out_1[i], -dich_x);
            }

            // KET QUA

            string result = new string(list_out_1);
            return result;

        }


        private static string lap_day = "~!@#$%^&*()";
        public static string Hash3(string input0, int length_out)
        {
            //input0 += lap_day;

            string input_convert = HConvert(input0);
            string out_0 = "";
            if (input_convert.Length < length_out)
            {
                int old_length = input_convert.Length;
                out_0 = input_convert;
                while (out_0.Length < length_out)
                {
                    //out_0 += ABC.Substring(old_length);
                    out_0 += lap_day;
                }

                if (out_0.Length > length_out)
                {
                    out_0 = out_0.Substring(0, length_out);
                }

                var list_out_0 = out_0.ToCharArray();
                string rest0 = out_0.Substring(old_length);
                while (rest0.Length > 0)
                {
                    string rest2 = "";
                    if (rest0.Length > length_out)
                    {
                        rest2 = rest0.Substring(0, length_out);
                        rest0 = rest0.Substring(length_out);
                    }
                    else
                    {
                        rest2 = rest0;
                        rest0 = "";
                    }

                    for (int i = 0; i < rest2.Length; i++)
                    {
                        char c = rest2[i];
                        // dịch out0
                        list_out_0[i] = Dichj(list_out_0[i], c);
                    }
                }

                out_0 = new string(list_out_0);
            }
            else
            {
                char[] list_out_0 = new char[length_out];

                out_0 = input_convert.Substring(0, length_out);
                list_out_0 = out_0.ToCharArray();
                string rest0 = input_convert.Substring(length_out);
                while (rest0.Length>0)
                {
                    string rest2 = "";
                    if (rest0.Length > length_out)
                    {
                        rest2 = rest0.Substring(0, length_out);
                        rest0 = rest0.Substring(length_out);
                    }
                    else
                    {
                        rest2 = rest0;
                        rest0 = "";
                    }

                    for (int i = 0; i < rest2.Length; i++)
                    {
                        char c = rest2[i];
                        // dịch out0
                        list_out_0[i] = Dichj(list_out_0[i], c);
                    }
                }
                
                out_0 = new string(list_out_0);
            }

            return out_0;
        }

        static int hebase32 = 32;
        private static int hephan = 90 - 32 + 1;


        /// <summary>
        /// 32-126 (hệ thập phân)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static long PrintASCIIstringToNumber(string input)
        {
            long value = 0;
            for (int i = input.Length-1; i >= 0; i--)
            {
                char c = input[i];
                value += (c-32)*(int)Math.Pow(hephan, i);
            }

            return value;
        }

        private static string NumberToHeBase(long number)
        {
            Stack<long> stack = new Stack<long>();
            while (number>0)
            {
                stack.Push(number%hephan);
                number /= hephan;
            }

            string result = "";
            while (stack.Count>0)
            {
                result += ABC[(int) stack.Pop()];
            }

            return result;
        }
        public static string Hash4(string input, int length_out)
        {
            long number = PrintASCIIstringToNumber(input);
            string ssss = NumberToHeBase(number);
            return ssss;
        }

        public static string Hash5(string input, int length_out)
        {
            string _11111111 = CreateMD5_1111111(input);//.Substring(0, length_out);
            int take_bit = 11;
            string _ABC = ABC_from_11111(_11111111, take_bit);
            if (_ABC.Length > length_out) _ABC = _ABC.Substring(0, length_out);
            else _ABC = _ABC.PadLeft(length_out, ABC[0]);
            return _ABC;
        }

        private static string ABC_from_11111(string _11111111, int take_bit)
        {
            string result = "";
            while (_11111111.Length>0)
            {
                string _11111;
                if (_11111111.Length > take_bit)
                {
                    _11111 = _11111111.Substring(0, take_bit);
                    _11111111 = _11111111.Substring(take_bit);
                }
                else
                {
                    _11111 = _11111111;
                    _11111111 = "";
                }

                result += ABC[Convert.ToInt32(_11111, 2) % ABC_lengh];
            }

            return result;
        }

        public static string CreateMD5_1111111(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                //StringBuilder sb = new StringBuilder();
                string result = "";
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    //sb.Append(hashBytes[i].ToString(""));
                    // Append 7bit
                    result += Convert.ToString(hashBytes[i], 2).PadLeft(7, '0');
                    //sb.Append(Convert.ToString(hashBytes[i], 64));
                }
                
                return result;
            }
        }
    }
}

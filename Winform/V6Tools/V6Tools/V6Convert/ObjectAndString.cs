using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;


namespace V6Tools.V6Convert
{
    public static class ObjectAndString
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

            var numberString = number.ToString("N" + decimals, CultureInfo.InvariantCulture);//= 19,950,000.00
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
            if (o == null) return false;
            string s = o.ToString().Trim().ToLower();
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
                    decimal.TryParse(StringToSystemDecimalSymbolStringNumber(o.ToString()), out result);
                    break;
            }
            return result;
        }

        public static int ObjectToInt(object o)
        {
            decimal result = 0;
            if (o == null) return 0;
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
            if (o == null) return 0;
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
            if (IsNumberType(o.GetType()))
            {
                //Chua xu ly.
            }
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

        public static float ObjectToFloat(string s)
        {
            return ToObject<float>(s);
        }

        public static Color RGBStringToColor(string rgb)
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
            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            return Color.FromArgb(r, g, b);
        }

        public static DataSet XmlStringToDataSet(string xml)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xml));
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace V6Tools.V6Convert
{
    public class Number
    {
        /// <summary>
        /// Tính giá trị biểu thức hỗ trợ +-*/ Round, Int, ^. 
        /// </summary>
        /// <param name="bieu_thuc"></param>
        /// <param name="DATA"></param>
        /// <returns></returns>
        public static decimal GiaTriBieuThuc(string bieu_thuc, IDictionary<string, object> DATA)
        {
            if (string.IsNullOrEmpty(bieu_thuc)) return 0;
            if (DATA != null) DATA = DATA.ToUpperKeys();
            //alert(bieu_thuc);
            bieu_thuc = bieu_thuc.Replace(" ", ""); //Bỏ hết khoảng trắng
            bieu_thuc = bieu_thuc.Replace("--", "+"); //loại bỏ lặp dấu -
            bieu_thuc = bieu_thuc.Replace("+-", "-");
            bieu_thuc = bieu_thuc.Replace("-+", "-");
            bieu_thuc = bieu_thuc.Replace("++", "+"); //loại bỏ lặp dấu +
            bieu_thuc = bieu_thuc.Replace("*+", "*");
            bieu_thuc = bieu_thuc.Replace("/+", "/");
            bieu_thuc = bieu_thuc.Replace("\n", "");

            //xử lý Int();
            bieu_thuc = bieu_thuc.ToUpper();
            int intOpenIndex = bieu_thuc.IndexOf("INT(", StringComparison.Ordinal);
            if (intOpenIndex >= 0)
            {
                var iopen = bieu_thuc.IndexOf('(', 0);
                var iclose = bieu_thuc.Length;
                for (var i = iopen; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '(') iopen = i;
                    else if (bieu_thuc[i] == ')')
                    {
                        iclose = i;
                        break;
                    }
                }
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, intOpenIndex);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c

                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + (int)GiaTriBieuThuc(b, DATA) + after + c, DATA);
            }

            // Xử lý căn bậc 2
            int sqrtOpenIndex = bieu_thuc.IndexOf("SQRT(", StringComparison.Ordinal);
            if (sqrtOpenIndex >= 0)
            {
                var iopen = bieu_thuc.IndexOf('(', 0);
                var iclose = bieu_thuc.Length;
                for (var i = iopen; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '(') iopen = i;
                    else if (bieu_thuc[i] == ')')
                    {
                        iclose = i;
                        break;
                    }
                }
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, sqrtOpenIndex);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c

                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + Sqrt(GiaTriBieuThuc(b, DATA)) + after + c, DATA);
            }

            //xử lý Round();
            bieu_thuc = bieu_thuc.ToUpper();
            int roundOpenIndex = bieu_thuc.IndexOf("ROUND(", StringComparison.Ordinal);
            if (roundOpenIndex >= 0)
            {
                var iopen = bieu_thuc.IndexOf('(', 0);
                var iclose = bieu_thuc.Length;
                for (var i = iopen; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '(') iopen = i;
                    else if (bieu_thuc[i] == ')')
                    {
                        iclose = i;
                        break;
                    }
                }
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                //alert("sau ) la: " + bieu_thuc[iclose + 1]);
                //alert("IndexOf: " + ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0)));
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, roundOpenIndex);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c
                var phayindex = b.LastIndexOf(',');
                var round1 = b.Substring(0, phayindex);
                var round2 = b.Substring(phayindex + 1);

                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + Math.Round(GiaTriBieuThuc(round1, DATA), (int)GiaTriBieuThuc(round2, DATA), MidpointRounding.AwayFromZero) + after + c, DATA);//RoundV(giatribt(a),giatribt(b))
            }
            //xử lý phép toán trong ngoặc trước.
            if (bieu_thuc.IndexOf('(', 0) >= 0 || bieu_thuc.IndexOf(')', 0) >= 0)
            {
                var iopen = bieu_thuc.IndexOf('(', 0);
                var iclose = bieu_thuc.Length;
                for (var i = iopen; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '(') iopen = i;
                    else if (bieu_thuc[i] == ')')
                    {
                        iclose = i;
                        break;
                    }
                }
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                //alert("sau ) la: " + bieu_thuc[iclose + 1]);
                //alert("IndexOf: " + ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0)));
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, iopen);
                if (a.Trim() == "") before = "";
                int b_length = iclose - iopen - 1;
                var b = bieu_thuc.Substring(iopen + 1, b_length); //a(b)c
                string c = "";
                if(iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + GiaTriBieuThuc(b, DATA) + after + c, DATA);//RoundV(giatribt(a),giatribt(b))
            }

            // có phép cộng trong biểu thức
            if (bieu_thuc.IndexOf('+') >= 0)
            {

                var values = bieu_thuc.Split('+');
                decimal sum = 0;
                for (var i = 0; i < values.Length; i++)
                {
                    sum += GiaTriBieuThuc(values[i], DATA);
                }
                return sum;

                //        var sp = bieu_thuc.LastIndexOf('+');//split point
                //        var values1 = bieu_thuc.Substring(0, sp);
                //        var values2 = bieu_thuc.Substring(sp + 1);
                //        return GiaTriBieuThuc(values1) + GiaTriBieuThuc(values2);
            }

            // làm cho hết phép cộng rồi tới phép trừ    ////////////////////////// xử lý số âm hơi vất vả.
            if ((bieu_thuc.Split('-').Length - 1) >
                (bieu_thuc.Split(new[] { "*-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "/-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "^-" }, StringSplitOptions.None).Length - 3))
            {

                var sp = bieu_thuc.IndexOf('-');
                //tim vi tri sp cuoi khong có */^ dung truoc
                for (var i = sp; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '-' && "*/^".IndexOf(bieu_thuc[i - 1]) < 0)
                    {
                        sp = i;
                    }
                }
                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) - GiaTriBieuThuc(values2, DATA);
            }

            //phép nhân
            if (bieu_thuc.IndexOf('*', 0) >= 0)
            {

                var values = bieu_thuc.Split('*');
                decimal sum = 1;
                for (var i = 0; i < values.Length; i++)
                {
                    sum *= GiaTriBieuThuc(values[i], DATA);
                }
                return sum;

                //Phép Round (Round012.345)

                //        var sp = bieu_thuc.LastIndexOf('*') > bieu_thuc.LastIndexOf("*-") ? bieu_thuc.LastIndexOf('*') : bieu_thuc.LastIndexOf("*-");
                //        var values1 = bieu_thuc.Substring(0, sp);
                //        var values2 = bieu_thuc.Substring(sp + 1);
                //        return GiaTriBieuThuc(values1) * GiaTriBieuThuc(values2);
            }
            if (bieu_thuc.IndexOf('/', 0) >= 0)
            {

                var sp = bieu_thuc.LastIndexOf('/') > bieu_thuc.LastIndexOf("/-")
                    ? bieu_thuc.LastIndexOf('/')
                    : bieu_thuc.LastIndexOf("/-");

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) / GiaTriBieuThuc(values2, DATA);
            }
            if (bieu_thuc.IndexOf('^', 0) >= 0)
            {
                var sp = bieu_thuc.LastIndexOf('^');// < bieu_thuc.LastIndexOf("^-")
                    //? bieu_thuc.LastIndexOf('^')
                    //: bieu_thuc.LastIndexOf("^-");

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return (decimal)Math.Pow((double)GiaTriBieuThuc(values1, DATA), (double)GiaTriBieuThuc(values2, DATA));
            }
            // giai thừa
            if (bieu_thuc.IndexOf('!', 0) > 0)
            {
                var sp = bieu_thuc.LastIndexOf('!');
                var values1 = bieu_thuc.Substring(0, sp);
                return factorial((int)GiaTriBieuThuc(values1, DATA));
            }
            else if (bieu_thuc.Trim() == "")
            {
                return 0;
            }
            else
            {
                string BIEU_THUC = bieu_thuc.Trim().ToUpper();
                if (DATA != null && DATA.ContainsKey(BIEU_THUC))
                {
                    return ObjectAndString.ObjectToDecimal(DATA[BIEU_THUC]);
                }
                if (BIEU_THUC.Length >= 29)
                {
                    throw new NotFiniteNumberException("Số quá lớn!");
                }
                return ObjectAndString.ObjectToDecimal(BIEU_THUC);
            }
        }

        private static object Sqrt(decimal value)
        {
            return Math.Sqrt((Double)value);
        }

        private static int factorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            var f = 1;
            for (var i = 2; i <= n; i++)
            {
                f *= i;
            }
            return f;
        }

        /// <summary>
        /// Chuyển chuỗi thành số.
        /// </summary>
        /// <param name="s">chuỗi</param>
        /// <param name="yourNumberDecimalSeparator">Dấu cách phần thập phân mà chuỗi đang sử dụng.</param>
        /// <returns>số decimal</returns>
        public static decimal ToDecimalV6(string s, string yourNumberDecimalSeparator)
        {
            return decimal.Parse(s.Replace(yourNumberDecimalSeparator, System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
        }

        /// <summary>
        /// Chuyển chuỗi thành số. chấp nhận hai dấu cách phần thập phân là (.) và (,).
        /// </summary>
        /// <param name="s">Chuỗi</param>
        /// <returns>Số kiểu decimal</returns>
        public static decimal ToDecimalV6(string s)
        {
            s = s.Replace(",", ".");
            return decimal.Parse(s.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
        }

        /// <summary>
        /// Chuyển chuỗi thành số.
        /// </summary>
        /// <param name="s">Chuỗi</param>
        /// <param name="yourNumberDecimalSeparator">Dấu cách phần thập phân mà chuỗi đang sử dụng.</param>
        /// <returns>Số kiểu interger</returns>
        public static int ToIntV6(string s, string yourNumberDecimalSeparator)
        {
            return (int)ToDecimalV6(s, yourNumberDecimalSeparator);
        }

        /// <summary>
        /// Chuyển chuỗi thành số.
        /// </summary>
        /// <param name="s">chuỗi</param>
        /// <returns>số kiểu interger</returns>
        public static int ToIntV6(string s)
        {
            return (int)ToDecimalV6(s);
        }

    }
}

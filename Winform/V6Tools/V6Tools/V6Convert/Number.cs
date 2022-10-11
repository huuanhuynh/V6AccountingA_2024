using System;
using System.Collections.Generic;
using System.Globalization;

namespace V6Tools.V6Convert
{
    public class Number
    {
        /// <summary>
        /// Tìm vị trí dấu đóng ngoặc
        /// </summary>
        /// <param name="bieu_thuc">Biểu thức đại số (giới hạn trong chương trình này).</param>
        /// <param name="iopen">Vị trí dấu mở ngoặc biết trước, nếu cho sai, hàm vẫn cho là có mở ngoặc ngay vị trí đó.</param>
        /// <returns>Vị trí đóng ngoặc hoặc vị độ dài chuỗi (lastindex+1) nếu không tìm thấy.</returns>
        private static int FindCloseBrackets(string bieu_thuc, int iopen)
        {
            int length = bieu_thuc.Length;
            if (iopen < 0 || iopen >= length)
            {
                throw new Exception("iopen out of range!");
            }

            int opencount = 1;
            for (var i = iopen + 1; i < bieu_thuc.Length; i++)
            {
                if (bieu_thuc[i] == '(')
                {
                    opencount++;
                }
                else if (bieu_thuc[i] == ')')
                {
                    opencount--;
                    if (opencount == 0)
                    {
                        return i;
                    }
                }
            }
            return bieu_thuc.Length;
        }

        /// <summary>
        /// Liền trước biểu thức trong ngoặc, hàm hoặc số là những dấu này thì không gán phép nhân
        /// </summary>
        private static string NoMultiplicationBefore = "+-*/(,^";
        /// <summary>
        /// Liền sau biểu thức trong ngoặc, hàm hoặc số là những dấu này thì không gán phép nhân
        /// </summary>
        private static string NoMultiplicationAfter = "+-*/),^!";

        /// <summary>
        /// Cố tính giá trị biểu thức, nếu có kết quả không lỗi trả về true, có lỗi trả về false.
        /// </summary>
        /// <param name="bieu_thuc"></param>
        /// <param name="DATA"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GiaTriBieuThucTry(string bieu_thuc, IDictionary<string, object> DATA, out decimal value)
        {
            try
            {
                value = GiaTriBieuThuc(bieu_thuc, DATA);
                return true;
            }
            catch
            {
                value = 0;
                return false;
            }
        }

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

            bieu_thuc = bieu_thuc.ToUpper();
            //function open index
            int foi = -1;

            #region === INT(x) ===
            foi = bieu_thuc.IndexOf("INT(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a int(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";
                
                return GiaTriBieuThuc(a + before + (int)GiaTriBieuThuc(b, DATA) + after + c, DATA);
            }
            #endregion === INT(x) ===
            
            #region === SIN(x) ===
            foi = bieu_thuc.IndexOf("SIN(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a sin(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                var result = GiaTriBieuThuc(a + before + ((decimal)Math.Sin((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
                return result;
            }
            #endregion === SIN(x) ===
            
            #region === COS(x) ===
            foi = bieu_thuc.IndexOf("COS(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a cos(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + ((decimal)Math.Cos((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === COS(x) ===
            
            #region === TAN(x) ===
            foi = bieu_thuc.IndexOf("TAN(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a tan(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + ((decimal)Math.Tan((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === TAN(x) ===
            
            #region === COT(x) ===
            foi = bieu_thuc.IndexOf("COT(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a cot(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + (1m/(decimal)Math.Tan((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === COT(x) ===

            #region === SQRT(x) ===
            foi = bieu_thuc.IndexOf("SQRT(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 4;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a sqrt(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + Sqrt(GiaTriBieuThuc(b, DATA)).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === SQRT(x) ===

            #region === ROUND(x,a) ===
            foi = bieu_thuc.LastIndexOf("ROUND(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 5;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a round(b1,b2) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                var phayindex = b.LastIndexOf(',');
                var b1 = b.Substring(0, phayindex);
                var b2 = b.Substring(phayindex + 1);

                return GiaTriBieuThuc(
                    a + before
                    + Math.Round(GiaTriBieuThuc(b1, DATA), (int)GiaTriBieuThuc(b2, DATA), MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture)
                    + after + c, DATA);
            }
            #endregion === ROUND(x,a) ===
            
            #region === MOD(x,Y) ===
            foi = bieu_thuc.LastIndexOf("MOD(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a mod(b1,b2) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                var phayindex = b.LastIndexOf(',');
                var b1 = b.Substring(0, phayindex);
                var b2 = b.Substring(phayindex + 1);

                return GiaTriBieuThuc(
                    a + before
                    + (GiaTriBieuThuc(b1, DATA) % (int)GiaTriBieuThuc(b2, DATA)).ToString(CultureInfo.InvariantCulture)
                    + after + c, DATA);
            }
            #endregion === MOD(x,a) ===

            #region === () xử lý phép toán trong ngoặc trong cùng trước. ===
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
                // a (b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, iopen);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";
                
                return GiaTriBieuThuc(a + before + GiaTriBieuThuc(b, DATA).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === () ===

            #region === + ===
            if (bieu_thuc.IndexOf('+') > 0 &&
                (bieu_thuc.Split('+').Length - 1) >
                (bieu_thuc.Split(new[] { "*+" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "/+" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "^+" }, StringSplitOptions.None).Length - 3))
            {

                var sp = bieu_thuc.IndexOf('+');
                //tim vi tri sp cuoi khong có */^ đứng truoc
                for (var i = sp; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '+' && "*/^".IndexOf(bieu_thuc[i - 1]) < 0)
                    {
                        sp = i;
                    }
                }
                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) + GiaTriBieuThuc(values2, DATA);
            }
            #endregion === + ===

            #region === - === làm cho hết phép cộng rồi tới phép trừ    ////////////////////////// xử lý số âm hơi vất vả.
            if (bieu_thuc.IndexOf('-') > 0 &&
                (bieu_thuc.Split('-').Length - 1) >
                (bieu_thuc.Split(new[] { "*-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "/-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "^-" }, StringSplitOptions.None).Length - 3))
            {

                var sp = bieu_thuc.IndexOf('-');
                //tim vi tri sp cuoi khong có */^ đứng truoc
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
            #endregion === - ===

            #region === * ===//phép nhân
            if (bieu_thuc.IndexOf('*', 0) >= 0)
            {
                var values = bieu_thuc.Split('*');
                decimal sum = 1;
                for (var i = 0; i < values.Length; i++)
                {
                    sum *= GiaTriBieuThuc(values[i], DATA);
                }
                return sum;
            }
            #endregion === * ===

            #region === / ===  Chia
            if (bieu_thuc.IndexOf('/', 0) >= 0)
            {

                var sp = bieu_thuc.LastIndexOf('/') > bieu_thuc.LastIndexOf("/-", StringComparison.InvariantCulture)
                    ? bieu_thuc.LastIndexOf('/')
                    : bieu_thuc.LastIndexOf("/-", StringComparison.InvariantCulture);

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) / GiaTriBieuThuc(values2, DATA);
            }
            #endregion === / ===

            #region === % ===   MOD(x,y) chia lấy dư
            if (bieu_thuc.IndexOf('%', 0) >= 0)
            {

                var sp = bieu_thuc.LastIndexOf('%') > bieu_thuc.LastIndexOf("%-", StringComparison.InvariantCulture)
                    ? bieu_thuc.LastIndexOf('%')
                    : bieu_thuc.LastIndexOf("%-", StringComparison.InvariantCulture);

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) % GiaTriBieuThuc(values2, DATA);
            }
            #endregion === % ===

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
                    if (DATA[BIEU_THUC] == DBNull.Value || DATA[BIEU_THUC] == null) return 0m;
                    if (ObjectAndString.IsNumberType(DATA[BIEU_THUC].GetType()))
                    {
                        return ObjectAndString.ObjectToDecimal(DATA[BIEU_THUC]);
                    }
                    else
                    {
                        throw new NotFiniteNumberException("NAN!");
                    }
                }
                int pointindex = BIEU_THUC.IndexOf('.');
                while (pointindex>=0 && BIEU_THUC.Length>pointindex && (BIEU_THUC.EndsWith("0")||BIEU_THUC.EndsWith(".")))
                {
                    BIEU_THUC = BIEU_THUC.Substring(0, BIEU_THUC.Length - 1);
                }
                if (BIEU_THUC.Length >= 29)
                {
                    throw new NotFiniteNumberException("Số quá dài!");
                }
                decimal temp;
                if (decimal.TryParse(ObjectAndString.StringToSystemDecimalSymbolStringNumber(BIEU_THUC), out temp))
                {
                    return temp;
                    ObjectAndString.ObjectToDecimal(BIEU_THUC);
                }
                else
                {
                    throw new NotFiniteNumberException("NAN!");
                }
            }
        }

        private static decimal Sqrt(decimal value)
        {
            return (decimal)Math.Sqrt((double)value);
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

        /// <summary>
        /// Tính độ dài cung tròn
        /// </summary>
        /// <param name="D">Khoảng cách 2 đỉnh cung.</param>
        /// <param name="h"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        public static double DoDaiCung(double D, double h, out double R)
        {
            double result = 0;
            R = -1;
            try
            {
                double d = D / 2;
                double c = Math.Sqrt(Math.Pow(d, 2) + Math.Pow(h, 2));

                double Arad = Math.Acos(d / c);

                double A2rad = Math.PI / 2 - (Arad * 2);
                double cosA2 = Math.Cos(A2rad);
                R = d / cosA2;

                result = (4 * Arad) * R;

                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
            return -1;
        }

    }
}

namespace V6Tools.V6Convert
{
    public class Number
    {
        #region ==== Number ====

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
        #endregion

    }
}

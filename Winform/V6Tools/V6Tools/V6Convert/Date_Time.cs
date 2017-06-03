using System;

namespace V6Tools.V6Convert
{
    public class Date_Time
    {
        /// <summary>
        /// Chuyển chuỗi thành ngày tháng...
        /// </summary>
        /// <param name="date">chuỗi</param>
        /// <param name="yourFormat">d/M/yyyy</param>
        /// <returns></returns>
        public static DateTime ToDateTimeV6(string date, string yourFormat)
        {
            return DateTime.ParseExact(date, yourFormat, null);
        }
    }
}

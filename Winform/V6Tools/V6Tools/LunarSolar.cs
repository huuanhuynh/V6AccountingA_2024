using System;
using System.Collections.Generic;
using System.Text;

namespace V6Tools
{
    public static class LunarSolar
    {
        //Attribute VB_Name = "LunarSolar"
        //Option Explicit
        //Const PI As Double = 3.14159265358979 ' Atn(1) * 4

        /// <summary>
        /// <para>Compute the (integral) Julian day number of day dd/mm/yyyy, i.e., the number</para>
        /// <para>of days between 1/1/4713 BC (Julian calendar) and dd/mm/yyyy.</para>
        /// <para>Formula from http://www.tondering.dk/claus/calendar.html</para>
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="mm"></param>
        /// <param name="yy"></param>
        /// <returns></returns>
        public static long JdFromDate(long dd, long mm, long yy)
        {
            double a = 0;
            long y = 0;
            long m = 0;
            long jd = 0;
            a = (int)((14 - mm) / 12);
            y = (long) (yy + 4800 - a);
            m = (long) (mm + 12 * a - 3);
            jd = dd + (int)((153 * m + 2) / 5) + 365 * y + (int)(y / 4) - (int)(y / 100) + (int)(y / 400) - 32045;
            if (jd < 2299161)
            {
                jd = dd + (int)((153 * m + 2) / 5) + 365 * y + (int)(y / 4) - 32083;
            }
            return jd;
        }

        /// <summary>
        /// Convert a Julian day number to day/month/year. Parameter jd is an integer
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static DateTime JdToDate(long jd)
        {
            long a = 0;
            long b = 0;
            long c = 0;
            long d = 0;
            long e = 0;
            long m = 0;
            long Day = 0;
            long Month = 0;
            long Year = 0;
            // After 5/10/1582, Gregorian calendar
            if ((jd > 2299160))
            {
                a = jd + 32044;
                b = (int)((4 * a + 3) / 146097);
                c = a - (int)((b * 146097) / 4);
            }
            else
            {
                b = 0;
                c = jd + 32082;
            }
            d = (int)((4 * c + 3) / 1461);
            e = c - (int)((1461 * d) / 4);
            m = (int)((5 * e + 2) / 153);
            Day = e - (int)((153 * m + 2) / 5) + 1;
            Month = m + 3 - 12 * (int)(m / 10);
            Year = b * 100 + d - 4800 + (int)(m / 10);
            //return Array(Day, Month, Year);
            return new DateTime((int) Year, (int) Month, (int) Day);
        }

        // Compute the time of the k-th new moon after the new moon of 1/1/1900 13:52 UCT
        // (measured as the number of days since 1/1/4713 BC noon UCT,
        // e.g., 2451545.125 is 1/1/2000 15:00 UTC).
        // 
        // 
        /// <summary>
        /// <para></para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        /// <param name="k"></param>
        /// <returns>Returns a floating number, e.g., 2415079.9758617813 for k=2 or 2414961.935157746 for k=-2</returns>
        public static double NewMoon(long k)
        {
            double T = 0;
            double T2 = 0;
            double T3 = 0;
            double dr = 0;
            double Jd1 = 0;
            double m = 0;
            double Mpr = 0;
            double F = 0;
            double C1 = 0;
            double deltat = 0;
            double JdNew = 0;
            T = k / 1236.85;
            // Time in Julian centuries from 1900 January 0.5
            T2 = T * T;
            T3 = T2 * T;
            dr = Math.PI / 180;
            Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 1.55E-07 * T3;
            Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr);
            // Mean new moon
            m = 359.2242 + 29.10535608 * k - 3.33E-05 * T2 - 3.47E-06 * T3;
            // Sun's mean anomaly
            Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 1.236E-05 * T3;
            // Moon's mean anomaly
            F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 2.39E-06 * T3;
            // Moon's argument of latitude
            C1 = (0.1734 - 0.000393 * T) * Math.Sin(m * dr) + 0.0021 * Math.Sin(2 * dr * m);
            C1 = C1 - 0.4068 * Math.Sin(Mpr * dr) + 0.0161 * Math.Sin(dr * 2 * Mpr);
            C1 = C1 - 0.0004 * Math.Sin(dr * 3 * Mpr);
            C1 = C1 + 0.0104 * Math.Sin(dr * 2 * F) - 0.0051 * Math.Sin(dr * (m + Mpr));
            C1 = C1 - 0.0074 * Math.Sin(dr * (m - Mpr)) + 0.0004 * Math.Sin(dr * (2 * F + m));
            C1 = C1 - 0.0004 * Math.Sin(dr * (2 * F - m)) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
            C1 = C1 + 0.001 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + m));
            if ((T < -11))
            {
                deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 8.45E-06 * T3 - 8.1E-08 * T * T3;
            }
            else
            {
                deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
            }
            JdNew = Jd1 + C1 - deltat;
            return JdNew;
        }

        // Compute the longitude of the sun at any time.
        // Parameter: floating number jdn, the number of days since 1/1/4713 BC noon

        public static double SunLongitude(double jdn)
        {
            double T = 0;
            double T2 = 0;
            double dr = 0;
            double m = 0;
            double L0 = 0;
            double DL = 0;
            double L = 0;
            T = (jdn - 2451545) / 36525;
            // Time in Julian centuries from 2000-01-01 12:00:00 GMT
            T2 = T * T;
            dr = Math.PI / 180;
            // degree to radian
            m = 357.5291 + 35999.0503 * T - 0.0001559 * T2 - 4.8E-07 * T * T2;
            // mean anomaly, degree
            L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2;
            // mean longitude, degree
            DL = (1.9146 - 0.004817 * T - 1.4E-05 * T2) * Math.Sin(dr * m);
            DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * m) + 0.00029 * Math.Sin(dr * 3 * m);
            L = L0 + DL;
            // true longitude, degree
            L = L * dr;
            L = L - Math.PI * 2 * ((int)(L / (Math.PI * 2)));
            // Normalize to (0, 2*PI)
            return L;
        }

        // Compute sun position at midnight of the day with the given Julian day number.
        // The time zone if the time difference between local time and UTC: 7.0 for UTC+7:00.
        // The function returns a number between 0 and 11.
        // From the day after March equinox and the 1st major term after March equinox,
        // 0 is returned. After that, return 1, 2, 3 ...
        public static long GetSunLongitude(double dayNumber, byte timeZone)
        {
            return (int)(SunLongitude(dayNumber - 0.5 - (float)timeZone / 24) / Math.PI * 6);
        }

        // Compute the day of the k-th new moon in the given time zone.
        // The time zone if the time difference between local time and UTC: 7.0 for UTC+7:00
        public static long getNewMoonDay(long k, long timeZone)
        {
            return (long)(NewMoon(k) + 0.5 + (float)timeZone / 24);
        }

        // Find the day that starts the luner month 11 of the given year
        // for the given time zone
        public static long GetLunarMonth11(long yy, byte timeZone)
        {
            long k = 0;
            double off = 0;
            long nm = 0;
            double sunLong = 0;
            //' off = jdFromDate(31, 12, yy) - 2415021.076998695
            off = JdFromDate(31, 12, yy) - 2415021;
            k = (int)(off / 29.530588853);
            nm = getNewMoonDay(k, timeZone);
            sunLong = GetSunLongitude(nm, timeZone);
            // sun longitude at local midnight
            if ((sunLong >= 9))
            {
                nm = getNewMoonDay(k - 1, timeZone);
            }
            return nm;
        }

        // Find the index of the leap month after the month starting on the day a11.
        public static long GetLeapMonthOffset(double a11, byte timeZone)
        {
            long k = 0;
            long last = 0;
            long Arc = 0;
            long I = 0;
            k = (int)((a11 - 2415021.07699869) / 29.530588853 + 0.5);
            I = 1;
            // We start with the month following lunar month 11
            Arc = GetSunLongitude(getNewMoonDay(k + I, timeZone), timeZone);
            do
            {
                last = Arc;
                I = I + 1;
                Arc = GetSunLongitude(getNewMoonDay(k + I, timeZone), timeZone);
            } while ((Arc != last && I < 14));
            return I - 1;
        }

        // Comvert solar date dd/mm/yyyy to the corresponding lunar date
        public static LunarDate Solar2Lunar(DateTime date, byte timeZone = 7)
        {
            return Solar2Lunar(date.Day, date.Month, date.Year, timeZone);
        }
        public static LunarDate Solar2Lunar(long dd, long mm, long yy = 0, byte timeZone = 7)
        {
            long k = 0;
            long diff = 0;
            long leapMonthDiff = 0;
            long dayNumber = 0;
            double monthStart = 0;
            long a11 = 0;
            long b11 = 0;
            double lunarDay = 0;
            long lunarMonth = 0;
            long leapMonth = 0;
            long lunarYear = 0;
            bool lunarLeap = false;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (yy == 0)
                yy = DateTime.Now.Year;
            dayNumber = JdFromDate(dd, mm, yy);
            k = (int)((dayNumber - 2415021.07699869) / 29.530588853);
            monthStart = getNewMoonDay(k + 1, timeZone);
            if ((monthStart > dayNumber))
            {
                monthStart = getNewMoonDay(k, timeZone);
            }
            // alert(dayNumber + " -> " + monthStart)
            a11 = GetLunarMonth11(yy, timeZone);
            b11 = a11;
            if (a11 >= monthStart)
            {
                lunarYear = yy;
                a11 = GetLunarMonth11(yy - 1, timeZone);
            }
            else
            {
                lunarYear = yy + 1;
                b11 = GetLunarMonth11(yy + 1, timeZone);
            }
            lunarDay = dayNumber - monthStart + 1;
            diff = (int)((monthStart - a11) / 29);
            lunarMonth = diff + 11;
            
            if (b11 - a11 > 365) // Nếu có tháng nhuận?
            {
                leapMonthDiff = GetLeapMonthOffset(a11, timeZone);
                leapMonth = leapMonthDiff + 10;
                if ((diff >= leapMonthDiff)) // Nếu tháng đã tới (hoặc qua) tháng nhuận
                {
                    lunarMonth = diff + 10;
                    
                    if (diff == leapMonthDiff)
                        lunarLeap = true;
                }
            }
            if (lunarMonth > 12) lunarMonth = lunarMonth - 12;
            if (leapMonth > 12) leapMonth = leapMonth - 12;
            if (lunarMonth >= 11 && diff < 4)
                lunarYear = lunarYear - 1;
            //return string.Format("{0:00}/{1:00}/{2:0000} ÂL", lunarDay, lunarMonth, lunarYear) + (lunarLeap ? " (" + lunarMonth + " N)" : "");
            return new LunarDate((int)lunarYear, (int)lunarMonth, (int)lunarDay, lunarLeap, (int)leapMonth);
        }

        // Convert a lunar date to the corresponding solar date
        /// <summary>
        /// Chuyển ngày âm lịch sang ngày dương lịch
        /// </summary>
        /// <param name="lunarDay"></param>
        /// <param name="lunarMonth"></param>
        /// <param name="lunarYear"></param>
        /// <param name="lunarLeap">Tháng này có phải tháng nhuận không?</param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTime Lunar2Solar(long lunarDay, long lunarMonth, long lunarYear = 0, bool lunarLeap = false, byte timeZone = 7)
        {
            DateTime resultDate;
            long k = 0;
            long a11 = 0;
            long b11 = 0;
            long off = 0;
            long leapOff = 0;
            long LeapMonth = 0;
            long monthStart = 0;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (lunarYear == 0)
                lunarYear = DateTime.Now.Year;
            if ((lunarMonth < 11))
            {
                a11 = GetLunarMonth11(lunarYear - 1, timeZone);
                b11 = GetLunarMonth11(lunarYear, timeZone);
            }
            else
            {
                a11 = GetLunarMonth11(lunarYear, timeZone);
                b11 = GetLunarMonth11(lunarYear + 1, timeZone);
            }
            k = (int)(0.5 + (a11 - 2415021.07699869) / 29.530588853);
            off = lunarMonth - 11;
            if ((off < 0))
                off = off + 12;
            if ((b11 - a11 > 365))
            {
                leapOff = GetLeapMonthOffset(a11, timeZone);
                LeapMonth = leapOff - 2;
                if (LeapMonth < 0)
                    LeapMonth = LeapMonth + 12;
                if (lunarLeap && lunarMonth != LeapMonth)
                {
                    resultDate = new DateTime(0,0,0);// Array(0, 0, 0);
                    return resultDate;
                }
                else if ((lunarLeap || off >= leapOff))
                {
                    off = off + 1;
                }
            }
            monthStart = getNewMoonDay(k + off, timeZone);
            resultDate = JdToDate(monthStart + lunarDay - 1);
            return resultDate;
        }
    }

    public class LunarDate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lunarYear"></param>
        /// <param name="lunarMonth"></param>
        /// <param name="lunarDay"></param>
        /// <param name="lunarLeap">Tháng này là tháng nhuận?</param>
        /// <param name="leapMonth">Nhuận tháng thứ mấy?</param>
        internal LunarDate(int lunarYear, int lunarMonth, int lunarDay, bool lunarLeap = false, int leapMonth = 0)
        {
            LunarYear = lunarYear;
            LunarMonth = lunarMonth;
            LunarDay = lunarDay;
            IsLeap = lunarLeap;
            LeapMonth = leapMonth;
        }

        public LunarDate(DateTime solarDate)
        {
            LunarDate lunar = LunarSolar.Solar2Lunar(solarDate);
            LunarDay = lunar.LunarDay;
            LunarMonth = lunar.LunarMonth;
            LunarYear = lunar.LunarYear;
            IsLeap = lunar.IsLeap;
            LeapMonth = lunar.LeapMonth;
        }
        public int LunarDay { get; set; }
        public int LunarMonth { get; set; }
        public int LunarYear { get; set; }
        private readonly string[] can = { "Canh", "Tân", "Nhâm", "Quý", "Giáp", "Ất", "Bính", "Đinh", "Mậu", "Kỷ" };
        private readonly string[] chi = { "Thân", "Dậu", "Tuất", "Hợi", "Tí", "Sửu", "Dần", "Mão", "Thìn", "Tỵ", "Ngọ", "Mùi" };
        public string YearString { get { return string.Format("{0} {1}", can[LunarYear%10], chi[LunarYear%12]); } }
        /// <summary>
        /// Tháng này là tháng nhuận?
        /// </summary>
        public bool IsLeap { get; set; }
        /// <summary>
        /// Tháng nhuận, 0 nếu không có.
        /// </summary>
        public int LeapMonth { get; set; }

        /// <summary>
        /// Ngày {0} tháng {1} năm {2}
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Ngày {0} tháng {1} năm {2}", LunarDay, LunarMonth, YearString);
        }
    }
}

using System;
using System.ComponentModel;
using V6Tools;
using System.Data;
using System.Globalization;
using Microsoft.Win32;
using V6SqlConnect;



//using System.Threading.Tasks;

namespace V6Init
{
    public static class V6Setting
    {
        /// <summary>
        /// InvariantCulture (dấu . ngăn cách phần thập phân).
        /// </summary>
        public static NumberFormatInfo V6_number_format_info
        {
            get
            {
                if (nfi == null)
                {
                    nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                    nfi.NumberGroupSeparator = V6Options.M_NUM_SEPARATOR;
                    nfi.NumberDecimalSeparator = V6Options.M_NUM_POINT;
                }
                return nfi;
            }
        }
        private static NumberFormatInfo nfi;

        /// <summary>
        /// Dấu cách phần thập phân của hệ thống (windows).
        /// </summary>
        public static string SystemDecimalSeparator
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator; }
        }

        /// <summary>
        /// Cài đặt hiển thị thông tin trên menu
        /// </summary>
        public static bool ViewMenuStatus = true;

        public static int RoundTienNt
        {
            get { return V6Options.M_ROUND_NT; }
        }

        public static int RoundTien
        {
            get { return V6Options.M_ROUND; }
        }

        public static int RoundNum
        {
            get { return V6Options.M_ROUND; }
        }

        public static int RoundGia
        {
            get { return V6Options.M_ROUND_GIA; }
        }

        public static int RoundGiaNt
        {
            get { return V6Options.M_ROUND_GIA_NT; }
        }

        public static int DecimalsNumber = 2;

        public static void LoadSetting(int userID)
        {
            //V6LoginInfo.Login()
            
            var o = SqlConnect.ExecuteScalar(CommandType.Text, "Select TOP 1 MA_KHO FROM ALKHO WHERE "
                + " (dbo.VFA_Inlist_MEMO(ma_kho, '" + V6Login.UserInfo["r_kho"] + "')=1 or 1="
                + (V6Login.IsAdmin?1:0)+")"
            + " AND LOAI_KHO='1'");

            if (o != null)
            {
                M_Ma_kho_default = o.ToString().Trim();
            }
            else
            {
                M_Ma_kho_default = "";
            }

        }

        public static string M_Ma_kho_default { get; set; }

        public static bool IsDesignTime
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }
        public static bool IsRunTime
        {
            get { return LicenseManager.UsageMode != LicenseUsageMode.Designtime; }
        }

        public static bool IsWordInstalled()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Winword.exe");
            if (key != null)
            {
                key.Close();
            }
            return key != null;
        }
        public static bool IsExcelInstalled()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\excel.exe");
            if (key != null)
            {
                key.Close();
            }
            return key != null;
        }

        private static string _V6Special = "";
        /// <summary>
        /// Đánh dấu đặc biệt.Triple,Fixinvoicevvar
        /// </summary>
        public static string V6Special
        {
            get
            {
                if (string.IsNullOrEmpty(_V6Special))
                {
                    _V6Special = UtilityHelper.ReadRegistry("V6SPECIAL");
                    if (string.IsNullOrEmpty(_V6Special))
                    {
                        V6Special = "1";
                    }
                }
                return _V6Special;
            }
            set
            {
                _V6Special = value;
                UtilityHelper.WriteRegistry("V6SPECIAL", value);
            }
        }

        public static bool Triple
        {
            get { return V6Special.Contains("Triple"); }
        }
        public static bool Fixinvoicevvar
        {
            get { return V6Special.Contains("Fixinvoicevvar"); }
        }

        private static string _language = "";
        /// <summary>
        /// Tùy chọn ngôn ngữ của V6 được lưu trong Registry.
        /// </summary>
        public static string Language
        {
            get
            {
                if (string.IsNullOrEmpty(_language))
                {
                    _language = UtilityHelper.ReadRegistry("V6LANGUAGE");
                    if (string.IsNullOrEmpty(_language))
                    {
                        Language = "V";
                    }
                }
                return _language;
            }
            set
            {
                _language = value;
                UtilityHelper.WriteRegistry("V6LANGUAGE", value);
            }
        }

        public static bool IsVietnamese
        {
            get { return Language == "V"; }
        }

        private static string _lastUserW = "";
        public static string LASTUSERW
        {
            get
            {
                if (string.IsNullOrEmpty(_lastUserW))
                {
                    _lastUserW = UtilityHelper.ReadRegistry("LASTUSERW") ?? "";
                }
                return _lastUserW;
            }
            set
            {
                _lastUserW = value;
                UtilityHelper.WriteRegistry("LASTUSERW", value);
            }
        }
        public static int M_Nam_bd
        {
            get
            {
                var _Nam_bd = Convert.ToInt32(SqlConnect.ExecuteScalar(CommandType.Text, "Select Nam_bd from AlStt"));
                return _Nam_bd;
            }
        }
        public static DateTime M_Ngay_ks
        {
            get
            {
                var _Ngay_ks = Convert.ToDateTime(SqlConnect.ExecuteScalar(CommandType.Text, "Select Ngay_ks from AlStt"));
                return _Ngay_ks;
            }
        }
        public static DateTime M_Ngay_dk
        {
            get
            {
                var _Ngay_ks = Convert.ToDateTime(SqlConnect.ExecuteScalar(CommandType.Text, "Select Ngay_dk from AlStt"));
                return _Ngay_ks;
            }
        }
        public static DateTime M_Ngay_ck
        {
            get
            {
                var _Ngay_ks = Convert.ToDateTime(SqlConnect.ExecuteScalar(CommandType.Text, "Select Ngay_ck from AlStt"));
                return _Ngay_ks;
            }
        }

        public static DateTime M_Ngay_ky1
        {
            get
            {
                var _Ngay_ks = Convert.ToDateTime(SqlConnect.ExecuteScalar(CommandType.Text, "Select Ngay_ky1 from AlStt"));
                return _Ngay_ks;
            }
        }

        public static int M_Size_ct
        {
            get
            {
                var _Size_ct = Convert.ToInt32(SqlConnect.ExecuteScalar(CommandType.Text, "Select TOP 1 SIZE_CT from ALCT WHERE SIZE_CT>0"));
                if (_Size_ct == 0) _Size_ct = 12;
 
                return _Size_ct;
            }

        }

        public static DateTime M_SV_DATE = DateTime.Today;
        
        public static DateTime M_ngay_ct1 = M_SV_DATE;
        public static DateTime M_ngay_ct2 = M_SV_DATE;
        public static int M_NAM = M_SV_DATE.Year;
        public static int M_KY1 = M_SV_DATE.Month;
        public static int M_KY2 = M_SV_DATE.Month;
        public static string Ten_dvcsx;
        public static string Ten_dvcs2x;
        public static string Dia_chix;
        public static string Dia_chi2x;
        public static string Dien_thoai;
        /// <summary>
        /// Lấy từ dvcs login.
        /// </summary>
        public static string Ma_so_thue { get; set; }

        public static DataRow DataDVCS;

        public static string M_TK { get; set; }
        public static string M_TK_CN { get; set; }
        public static string M_Ma_kh { get; set; }
        public static string M_Ma_kho { get; set; }

        //public static bool ConvertAbc ToUnicode { get { return false; } }


        /// <summary>
        /// Lấy dữ liệu từ
        /// </summary>
        public static GetDataMode GetDataMode { get; set; }

        public static int YearFilter { get; set; }
        public static int MonthFilter { get; set; }
        
    }
}

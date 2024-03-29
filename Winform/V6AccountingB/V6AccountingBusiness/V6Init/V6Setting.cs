﻿using System;
using System.ComponentModel;
using V6Tools;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using V6SqlConnect;



//using System.Threading.Tasks;

namespace V6Init
{
    public static class V6Setting
    {
        /// <summary>
        /// Thư mục IMPORT_EXCEL
        /// </summary>
        public static readonly string IMPORT_EXCEL = "IMPORT_EXCEL";
        /// <summary>
        /// Thư mục EMPORT_EXCEL_SYSTEM
        /// </summary>
        public static readonly string EMPORT_EXCEL_SYSTEM = "EMPORT_EXCEL_SYSTEM";
        /// <summary>
        /// Thư mục Reports
        /// </summary>
        public static readonly string V6ReportsFolder = "Reports";
        /// <summary>
        /// <para>Đường dẫn thư mục tạm của chương trình trên máy client.</para>
        /// <para>File sẽ bị xóa khi khởi động chương trình.</para>
        /// </summary>
        public static readonly string V6SoftLocalAppData_Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "V6Soft");
        public static readonly string V6ATempLocalAppData_Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "V6ATemp");
        /// <summary>
        /// Thư mục tạm trong appdata trên máy.
        /// </summary>
        public static readonly string SystemUserAppDataLocalTemp_Directory = Path.GetTempPath();
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
            get { return SystemCulture.NumberFormat.NumberDecimalSeparator; }
        }

        /// <summary>
        /// Cài đặt hiển thị thông tin trên menu
        /// </summary>
        public static bool ViewMenuStatus = true;

        public static int RoundTienNt
        {
            get { return V6Options.M_ROUND_NT; }
        }
        public static int RoundSL
        {
            get { return V6Options.M_ROUND_SL; }
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

            var o = SqlConnect.ExecuteScalar(
                CommandType.Text,
                "Select TOP 1 MA_KHO FROM ALKHO WHERE "
                + " (dbo.VFA_Inlist_MEMO(ma_kho, '" + V6Login.UserInfo["r_kho"] + "')=1 or 1="
                + (V6Login.IsAdmin ? 1 : 0) + ")" + " AND LOAI_KHO='1'"
                + (V6Login.MadvcsCount == 1 ? " AND MA_DVCS='" + V6Login.Madvcs + "'" : ""));

            M_Ma_kho_default = (o??"").ToString().Trim();

            M_Ma_kho = M_Ma_kho_default;
        }

        public static string M_Ma_kho_default { get; private set; }

        public static bool NotLoggedIn
        {
            get { return !IsLoggedIn; }
        }
        [DefaultValue(false)]
        public static bool IsLoggedIn { get; set; }

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
        /// Đánh dấu đặc biệt.Triple,FixInvoiceVvar
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

        public static bool V6Special_AllowAdd
        {
            get { return V6Special.Contains("AllowAdd"); }
        }

        public static bool Triple
        {
            get { return V6Special.Contains("Triple"); }
        }
        /// <summary>
        /// Gán dữ liệu liên quan của các vVar textbox trong InvoiceControl.
        /// </summary>
        public static bool FixInvoiceVvar
        {
            get { return V6Special.Contains("FixInvoiceVvar"); }
        }
        /// <summary>
        /// Bật tắt ghi log ở những vị trí không phát sinh lỗi để kiểm tra chương trình.
        /// </summary>
        public static bool WriteExtraLog
        {
            get { return V6Special.Contains("WriteExtraLog"); }
        }

        private static string _language = "";
        /// <summary>
        /// Tùy chọn ngôn ngữ của V6 được lưu trong Registry. V hoặc E
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

        private static string _report_language = "";
        /// <summary>
        /// Tùy chọn ngôn ngữ report của V6 được lưu trong Registry. V E B
        /// </summary>
        public static string ReportLanguage
        {
            get
            {
                if (string.IsNullOrEmpty(_report_language))
                {
                    _report_language = UtilityHelper.ReadRegistry("V6RLANGUAGE");
                    if (string.IsNullOrEmpty(_report_language))
                    {
                        Language = "V";
                    }
                }
                return _report_language;
            }
            set
            {
                _report_language = value;
                UtilityHelper.WriteRegistry("V6RLANGUAGE", value);
            }
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

        private static string _oldPrinter = null;
        public static string OLDPRINTER
        {
            get
            {
                if (string.IsNullOrEmpty(_lastUserW))
                {
                    _oldPrinter = UtilityHelper.ReadRegistry("OLDPRINTER") ?? "";
                }
                return _oldPrinter;
            }
            set
            {
                _oldPrinter = value;
                UtilityHelper.WriteRegistry("OLDPRINTER", value);
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
        public static DateTime M_NGAY_BD
        {
            get
            {
                var _Ngay_bd = Convert.ToDateTime(SqlConnect.ExecuteScalar(CommandType.Text, "Select Ngay_dn from AlStt"));
                return _Ngay_bd;
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

        /// <summary>
        /// Ngày server lúc login.
        /// </summary>
        public static DateTime M_SV_DATE = DateTime.Today;
        /// <summary>
        /// Biến giữ giá trị người dùng nhập vào.
        /// </summary>
        public static DateTime M_ngay_ct1 = M_SV_DATE;
        /// <summary>
        /// Biến giữ giá trị người dùng nhập vào.
        /// </summary>
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
        public static CultureInfo SystemCulture { get; set; }

        public static object GetValueNull(string name)
        {
            try
            {
                string NAME = name.ToUpper();
                Type t = typeof(V6Setting);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (FieldInfo fi in fields)
                {
                    if(fi.Name.ToUpper() == NAME)
                    return fi.GetValue(null);
                }

                PropertyInfo[] properties = t.GetProperties(BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo fi in properties)
                {
                    if (fi.Name.ToUpper() == NAME)
                        return fi.GetValue(null, null);
                }
            }
            catch (Exception)
            {
                
            }
            return null;
        }
    }
}

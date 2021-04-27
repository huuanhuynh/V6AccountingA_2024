using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using V6SqlConnect;
using V6Structs;

namespace V6Init
{
    public static class V6Options
    {

        /// <summary>
        /// Lấy lên tất cả value
        /// </summary>
        public static void LoadValue()
        {
            try
            {
                V6SelectResult selectResult = SqlConnect.Select("V6Option", "name,type,val,defaul");

                if (selectResult.Data != null)
                {
                    _values = new SortedDictionary<string, string>();
                    _defaul = new SortedDictionary<string, string>();
                    _types = new SortedDictionary<string, string>();
                    foreach (DataRow row in selectResult.Data.Rows)
                    {
                        _values[row["name"].ToString().Trim().ToUpper()] = row["val"].ToString().Trim();
                        _defaul[row["name"].ToString().Trim().ToUpper()] = row["defaul"].ToString().Trim();
                        _types[row["name"].ToString().Trim().ToUpper()] = row["type"].ToString().Trim();
                    }
                }
            }
            catch
            {
                _values = null;
            }

        }

        private static SortedDictionary<string, string> _values;
        private static SortedDictionary<string, string> _defaul;
        private static SortedDictionary<string, string> _types;
        public static bool Test = true;

        

        /// <summary>
        /// [UPPER_FIELD] = Trimmed value
        /// </summary>
        public static SortedDictionary<string, string> V6OptionValues
        {
            get
            {
                if (_values == null || _values.Count == 0)
                    LoadValue();
                return _values;
            }
        }

        public static bool ContainsKey(string key)
        {
            return V6OptionValues.ContainsKey(key.ToUpper());
        }

        public static string GetValue(string name)
        {
            try
            {
                return V6OptionValues[name.ToUpper()];
            }
            catch (Exception ex)
            {
                throw new Exception(name + " " + ex.Message);
            }
        }
        public static string GetDefaul(string name)
        {
            try
            {
                return _defaul[name.ToUpper()];
            }
            catch (Exception ex)
            {
                throw new Exception(name + " " + ex.Message);
            }
        }
        /// <summary>
        /// Lấy giá trị không gây lỗi, không có trả về null;
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValueNull(string name)
        {
            try
            {
                return V6OptionValues[name.ToUpper()];
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static void SetValue(string name, string value)
        {
            try
            {
                V6OptionValues[name.ToUpper()] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(name + " " + ex.Message);
            }
        }

        public static SortedDictionary<string, string> V6OptionTypes
        {
            get
            {
                if (_types == null || _types.Count == 0)
                    LoadValue();
                return _types;
            }
        }

        /// <summary>
        /// Số ký hiệu phiên bản trong SQL
        /// </summary>
        public static string CurrentVersion
        {
            get
            {
                return GetValue("M_V6SOFT_VER");
            }
        }

        public static string MODULE_ID
        {
            get { return GetValue("MODULE_ID"); }
            set { V6OptionValues["MODULE_ID"] = value; }
        }
       
        private static string m_num_separator;
        /// <summary>
        /// Dấu cách phần nghìn
        /// </summary>
        public static string M_NUM_SEPARATOR
        {
            get
            {
                if (string.IsNullOrEmpty(m_num_separator))
                {
                    if (V6OptionValues == null)
                    {
                        m_num_separator = " ";
                    }
                    else
                    {
                        m_num_separator = GetValue("M_NUM_SEPARATOR") == ""
                            ? " "
                            : GetValue("M_NUM_SEPARATOR");
                    }

                }
                return m_num_separator;
            }
        }

        /// <summary>
        /// Giới hạn số dòng dữ liệu đọc từ Excel.
        /// </summary>
        public static int M_MAXROWS_EXCEL
        {
            get { return Convert.ToInt32(GetValueNull("M_MAXROWS_EXCEL")??"0"); }
        }

        private static string m_num_point;
        /// <summary>
        /// Dấu cách phần thập phân (lẻ) (,)
        /// </summary>
        public static string M_NUM_POINT
        {
            get
            {
                if (string.IsNullOrEmpty(m_num_point))
                {
                    if (V6OptionValues == null)
                    {
                        m_num_point = ",";
                    }
                    else
                    {
                        m_num_point = GetValue("M_NUM_POINT") == ""
                            ? ","
                            : GetValue("M_NUM_POINT");
                    }

                }
                return m_num_point;
            }
        }

        private static string m_ma_nt0;

        /// <summary>
        /// Đường dẫn thư mục tạm.
        /// </summary>
        public static string K_TMP
        {
            get
            {
                if (string.IsNullOrEmpty(_k_tmp))
                {
                    _k_tmp = GetValue("K_TMP");
                }
                return _k_tmp;
            }
        }
        private static string _k_tmp;

        public static string M_MA_NT0
        {
            get
            {
                if (string.IsNullOrEmpty(m_ma_nt0))
                {
                    if (V6OptionValues == null)
                    {
                        m_ma_nt0 = "VND";
                    }
                    else
                    {
                        m_ma_nt0 = GetValue("M_MA_NT0") == ""
                            ? "VND"
                            : GetValue("M_MA_NT0");
                    }

                }
                return m_ma_nt0;
            }
        }

        public static int M_IP_R_TIEN
        {
            get { return Convert.ToInt32(GetValue("M_IP_R_TIEN").Substring(1)); }
        }

        public static int M_IP_R_TIENNT
        {
            get { return Convert.ToInt32(GetValue("M_IP_R_TIENNT").Substring(1)); }
        }

        public static int M_IP_R_SL
        {
            get { return Convert.ToInt32(GetValue("M_IP_R_SL").Substring(1)); }
        }

        public static int M_IP_R_GIA
        {
            get { return Convert.ToInt32(GetValue("M_IP_R_GIA").Substring(1)); }
        }

        public static int M_IP_R_GIANT
        {
            get { return Convert.ToInt32(GetValue("M_IP_R_GIANT").Substring(1)); }
        }

        /// <summary>
        /// config 2 ký tự "11" Check nguyên tắc mst / check tồn tại trong dữ liệu.
        /// </summary>
        public static string M_QLY_MA_SO_THUE
        {
            get { return GetValue("M_QLY_MA_SO_THUE").Trim(); }
        }

        /// <summary>
        /// start,length MA_KHO;MA_DAY;MA_KE;VI_TRI => 0,1;1,2;4,2;7,2
        /// </summary>
        public static string M_VITRI_CODEDAY_INDEX
        {
            get
            {
                if (string.IsNullOrEmpty(_M_VITRI_CODEDAY_INDEX))
                {
                    _M_VITRI_CODEDAY_INDEX = GetValue("M_VITRI_CODEDAY_INDEX");
                }
                return _M_VITRI_CODEDAY_INDEX;
            }
        }
        private static string _M_VITRI_CODEDAY_INDEX;


        public static float M_R_FONTSIZE
        {
            get
            {
                if (m_r_fontsize0 > 5) return m_r_fontsize0;
                m_r_fontsize0 = (float)Convert.ToDouble(GetValue("M_R_FONTSIZE"), CultureInfo.InvariantCulture);
                return m_r_fontsize0;
            }
        }

        private static float m_r_fontsize0 = 0f;

        /// <summary>
        /// nếu không có cấu hình thì = 6
        /// </summary>
        public static string M_RULE_PASS
        {
            get
            {
                var s = GetValueNull("M_RULE_PASS");
                if (string.IsNullOrEmpty(s)) s = "6";
                return s;
            }
        }

        /// <summary>
        /// Roun all-> Numeric 0,1,2...
        /// </summary>
        public static int M_ROUND
        {
            get
            {
                if (V6OptionValues == null) return 0;
                return Convert.ToInt32(GetValue("M_ROUND"));
            }
        }

        public static int M_ROUND_NT
        {
            get
            {
                if (V6OptionValues == null) return 2;
                return Convert.ToInt32(GetValue("M_ROUND_NT"));
            }
        }
        
        public static int M_ROUND_SL
        {
            get
            {
                if (V6OptionValues == null) return 3;
                return Convert.ToInt32(GetValue("M_ROUND_SL"));
            }
        }

        public static int M_ROUND_NT0
        {
            get
            {
                if (V6OptionValues == null) return 2;
                return Convert.ToInt32(GetValue("M_ROUND_NT0"));
            }
        }

        public static int M_ROUND_GIA
        {
            get
            {
                if (V6OptionValues == null) return 4;
                return Convert.ToInt32(GetValue("M_ROUND_GIA"));
            }
        }

        public static int M_ROUND_GIA_NT
        {
            get
            {
                if (V6OptionValues == null) return 4;
                return Convert.ToInt32(GetValue("M_ROUND_GIA_NT"));
            }
        }

        public static int M_ROUND_GIA_NT0
        {
            get
            {
                if (V6OptionValues == null) return 4;
                return Convert.ToInt32(GetValue("M_ROUND_GIA_NT0"));
            }
        }

        /// <summary>
        /// Cho xuất âm. 0: Không cho phép xuất âm. 1: Cho phép xuất âm.
        /// </summary>
        public static string M_CHK_XUAT
        {
            get
            {
                if (V6OptionValues == null) return "1";
                return GetValue("M_CHK_XUAT");
            }
        }
        
        public static int M_IP_TIEN
        {
            get
            {
                if (V6OptionValues == null) return 0;
                else return Convert.ToInt32(GetValue("M_IP_TIEN").Substring(1));
            }
        }

        public static int M_IP_TIEN_NT
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(GetValue("M_IP_TIEN_NT").Substring(1));
            }
        }

        public static int M_IP_SL
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(GetValue("M_IP_SL").Substring(1));
            }
        }

        public static int M_IP_GIA
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(GetValue("M_IP_GIA").Substring(1));
            }
        }

        public static int M_IP_GIA_NT
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(GetValue("M_IP_GIA_NT").Substring(1));
            }
        }

        public static int M_IP_TY_GIA
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(GetValue("M_IP_TY_GIA").Substring(1));
            }
        }
        
        /// <summary>
        /// Cắt bỏ phần cuối Barcode
        /// </summary>
        public static int M_BARCODE_ELENGTH
        {
            get
            {
                if (V6OptionValues == null) return 0;
                else return Convert.ToInt32(GetValue("M_BARCODE_ELENGTH"));
            }
        }
        /// <summary>
        /// Tự động lưu xuống chi tiết khi chọn ma_vt
        /// </summary>
        public static bool M_AUTO_SAVEDETAIL
        {
            get
            {
                if (V6OptionValues == null) return false;
                return "1" == GetValue("M_AUTO_SAVEDETAIL");
            }
        }
        
        public static bool M_AUTO_MODEM_SMS
        {
            get
            {
                if (V6OptionValues == null) return false;
                return "1" == GetValue("M_AUTO_MODEM_SMS");
            }
        }
        /// <summary>
        /// Cộng dồn khi lưu xuống chi tiết
        /// </summary>
        public static bool M_ADD_DETAILQUANTITY
        {
            get
            {
                if (V6OptionValues == null) return false;
                return "1" == GetValue("M_ADD_DETAILQUANTITY");
            }
        }
        /// <summary>
        /// Thời gian khóa password tính bằng phút
        /// </summary>
        public static int M_LOCK_TIME
        {
            get
            {
                if (V6OptionValues == null) return 0;
                return Convert.ToInt32("0" + GetValue("M_LOCK_TIME"));
            }
        }

        public static string M_OPEN_EXCEL
        {
            get
            {
                if (V6OptionValues == null) return "";
                return GetValue("M_OPEN_EXCEL");
            }
        }
        public static bool AutoOpenExcel { get { return M_OPEN_EXCEL == "1"; } }

        /// <summary>
        /// Kiểu tính ck_km
        /// <para>01: Không tự động, tự chọn hết khi tính.</para>
        /// <para>02: Không tự động, kq>2 hiển thị form chọn chương trình.</para>
        /// <para>11: Tự động, tự chọn hết khi tính.</para>
        /// <para>12: Tự động, kq>2 hiển thị form chọn chương trình.</para>
        /// </summary>
        public static string M_SOA_TINH_CK_KM
        {
            get
            {
                if (V6OptionValues == null) return "";
                return GetValue("M_SOA_TINH_CK_KM");
            }
        }

        public static string M_SUA_BC
        {
            get
            {
                if (V6OptionValues == null) return "";
                return GetValueNull("M_SUA_BC");
            }
        }
        
        /// <summary>
        /// 111 tương ứng Menu3Log/EditLogInvoice/EditLogList
        /// </summary>
        public static string M_USER_LOG
        {
            get
            {
                if (V6OptionValues == null) return "";
                return GetValueNull("M_USER_LOG") + "";
            }
        }

        public static bool Menu3Log
        {
            get
            {
                string m_user_log = M_USER_LOG;
                if (m_user_log.Length > 0 || m_user_log[0] == '1')
                {
                    return true;
                }

                return false;
            }
        }

        public static bool SaveEditLogInvoice
        {
            get
            {
                string m_user_log = M_USER_LOG;
                if (m_user_log.Length > 1 || m_user_log[1] == '1')
                {
                    return true;
                }

                return false;
            }
        }

        public static bool SaveEditLogList
        {
            get
            {
                string m_user_log = M_USER_LOG;
                if (m_user_log.Length > 2 || m_user_log[2] == '1')
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Định nghĩa Font report Name;Size;Style
        /// </summary>
        public static object M_RFONTNAME
        {
            get
            {
                var s = GetValue("M_RFONTNAME");
                if (string.IsNullOrEmpty(s))
                {
                    s = GetDefaul("M_RFONTNAME");
                }

                return s;
            }
        }
        /// <summary>
        /// Định nghĩa Font report title Name;Size;Style
        /// </summary>
        public static object M_RTFONT
        {
            get
            {
                var s = GetValue("M_RTFONT");
                if (string.IsNullOrEmpty(s))
                {
                    s = GetDefaul("M_RTFONT");
                }

                return s;
            }
        }
        /// <summary>
        /// Định nghĩa Font report sign Name;Size;Style
        /// </summary>
        public static object M_RSFONT
        {
            get
            {
                var s = GetValue("M_RSFONT");
                if (string.IsNullOrEmpty(s))
                {
                    s = GetDefaul("M_RSFONT");
                }

                return s;
            }
        }
    }

    public enum GetDataMode
    {
        API, Local
    }
}

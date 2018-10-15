using System;
using System.Collections.Generic;
using System.Data;
using V6SqlConnect;
using V6Structs;

namespace V6Init
{
    public static class V6Options
    {

        /// <summary>
        /// Lấy lên tất cả value
        /// </summary>
        private static void LoadValue()
        {
            try
            {
                V6SelectResult selectResult = SqlConnect.Select("V6Option", "name,type,val");

                if (selectResult.Data != null)
                {
                    value = new SortedDictionary<string, string>();
                    type = new SortedDictionary<string, string>();
                    foreach (DataRow row in selectResult.Data.Rows)
                    {
                        value[row["name"].ToString().Trim().ToUpper()] = row["val"].ToString().Trim();
                        type[row["name"].ToString().Trim().ToUpper()] = row["type"].ToString().Trim();
                    }
                }
            }
            catch
            {
                value = null;
            }

        }

        private static SortedDictionary<string, string> value;
        private static SortedDictionary<string, string> type;
        public static bool Test = true;

        

        /// <summary>
        /// [UPPER_FIELD] = Trimmed value
        /// </summary>
        public static SortedDictionary<string, string> V6OptionValues
        {
            get
            {
                if (value == null || value.Count == 0)
                    LoadValue();
                return value;
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
        
        public static void SetValue(string name, string value)
        {
            try
            {
                V6OptionTypes[name.ToUpper()] = value;
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
                if (type == null || type.Count == 0)
                    LoadValue();
                return type;
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
        /// Cho phép xuất âm?
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
                return Convert.ToInt32(GetValue("M_LOCK_TIME"));
            }
        }
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
        
    }

    public enum GetDataMode
    {
        API, Local
    }
}

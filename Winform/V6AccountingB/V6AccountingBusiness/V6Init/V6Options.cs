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

        private static SortedDictionary<string, string> value = null;
        private static SortedDictionary<string, string> type = null;
        public static bool Test = true;

        

        /// <summary>
        /// [UPPER_FIELD]
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
            get { return V6OptionValues["MODULE_ID"]; }
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
                        m_num_separator = V6OptionValues["M_NUM_SEPARATOR"] == ""
                            ? " "
                            : V6OptionValues["M_NUM_SEPARATOR"];
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
                        m_num_point = V6OptionValues["M_NUM_POINT"] == ""
                            ? ","
                            : V6OptionValues["M_NUM_POINT"];
                    }

                }
                return m_num_point;
            }
        }

        private static string m_ma_nt0;
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
                        m_ma_nt0 = V6OptionValues["M_MA_NT0"] == ""
                            ? "VND"
                            : V6OptionValues["M_MA_NT0"];
                    }

                }
                return m_ma_nt0;
            }
        }

        public static int M_IP_R_TIEN
        {
            get { return Convert.ToInt32(V6OptionValues["M_IP_R_TIEN"].Substring(1)); }
        }

        public static int M_IP_R_TIENNT
        {
            get { return Convert.ToInt32(V6OptionValues["M_IP_R_TIENNT"].Substring(1)); }
        }

        public static int M_IP_R_SL
        {
            get { return Convert.ToInt32(V6OptionValues["M_IP_R_SL"].Substring(1)); }
        }

        public static int M_IP_R_GIA
        {
            get { return Convert.ToInt32(V6OptionValues["M_IP_R_GIA"].Substring(1)); }
        }

        public static int M_IP_R_GIANT
        {
            get { return Convert.ToInt32(V6OptionValues["M_IP_R_GIANT"].Substring(1)); }
        }

        /// <summary>
        /// Roun all-> Numeric 0,1,2...
        /// </summary>
        public static int M_ROUND
        {
            get
            {
                if (V6OptionValues == null) return 0;
                return Convert.ToInt32(V6OptionValues["M_ROUND"]);
            }
        }

        public static int M_ROUND_NT
        {
            get
            {
                if (V6OptionValues == null) return 2;
                return Convert.ToInt32(V6OptionValues["M_ROUND_NT"]);
            }
        }

        public static int M_ROUND_NT0
        {
            get
            {
                if (V6OptionValues == null) return 2;
                return Convert.ToInt32(V6OptionValues["M_ROUND_NT0"]);
            }
        }

        public static int M_ROUND_GIA
        {
            get
            {
                if (V6OptionValues == null) return 4;
                return Convert.ToInt32(V6OptionValues["M_ROUND_GIA"]);
            }
        }

        public static int M_ROUND_GIA_NT
        {
            get
            {
                if (V6OptionValues == null) return 4;
                return Convert.ToInt32(V6OptionValues["M_ROUND_GIA_NT"]);
            }
        }

        public static int M_ROUND_GIA_NT0
        {
            get
            {
                if (V6OptionValues == null) return 4;
                return Convert.ToInt32(V6OptionValues["M_ROUND_GIA_NT0"]);
            }
        }

        public static string M_CHK_XUAT
        {
            get
            {
                if (V6OptionValues == null) return "1";
                return V6OptionValues["M_CHK_XUAT"];
            }
        }
        public static int M_IP_TIEN
        {
            get
            {
                if (V6OptionValues == null) return 0;
                else return Convert.ToInt32(V6OptionValues["M_IP_TIEN"].Substring(1));
            }
        }

        public static int M_IP_TIEN_NT
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(V6OptionValues["M_IP_TIEN_NT"].Substring(1));
            }
        }

        public static int M_IP_SL
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(V6OptionValues["M_IP_SL"].Substring(1));
            }
        }

        public static int M_IP_GIA
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(V6OptionValues["M_IP_GIA"].Substring(1));
            }
        }

        public static int M_IP_GIA_NT
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(V6OptionValues["M_IP_GIA_NT"].Substring(1));
            }
        }

        public static int M_IP_TY_GIA
        {
            get
            {
                if (V6OptionValues == null) return 2;
                else return Convert.ToInt32(V6OptionValues["M_IP_TY_GIA"].Substring(1));
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
                else return Convert.ToInt32(V6OptionValues["M_BARCODE_ELENGTH"]);
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
                return "1" == V6OptionValues["M_AUTO_SAVEDETAIL"];
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
                return "1" == V6OptionValues["M_ADD_DETAILQUANTITY"];
            }
        }
    }

    public enum GetDataMode
    {
        API, Local
    }
}

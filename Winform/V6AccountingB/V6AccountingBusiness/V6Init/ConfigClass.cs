using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Init
{
    public class Config
    {
        public IDictionary<string, object> DATA;

        public Config()
        {

        }
        public Config(IDictionary<string, object> data)
        {
            if (data != null && data.Count > 0)
            {
                DATA = data;
                NoInfo = false;
            }
        }
        [DefaultValue(true)]
        public bool NoInfo { get; internal set; }
        public bool HaveInfo { get { return !NoInfo; } }
        public bool Error { get; set; }

        public DateTime GetDate(string name)
        {
            name = name.ToUpper();
            if (DATA != null && DATA.ContainsKey(name))
            {
                var date = ObjectAndString.ObjectToDate(DATA[name]);
                if(date == null) return DateTime.MinValue;
                return date.Value;
            }
            return DateTime.MinValue;
        }

        public SortedDictionary<string, string> _extraInfor = null;
        public void LoadExtraInfor()
        {
            _extraInfor = new SortedDictionary<string, string>();
            string s = "" + GetString("EXTRA_INFOR");
            if (s != "")
            {
                var sss = s.Split(';');
                foreach (string ss in sss)
                {
                    int indexOf = ss.IndexOf(":", StringComparison.Ordinal);
                    if (indexOf > 0)
                    {
                        _extraInfor[ss.Substring(0, indexOf).ToUpper()] = ss.Substring(indexOf + 1);
                    }
                }
            }
        }

        public string GET_EXTRA_INFOR(string key)
        {
            if (!_extraInfor.ContainsKey(key) && !_extraInfor.ContainsKey(key.ToUpper())) throw new Exception("No key: " + key);
            if (_extraInfor.ContainsKey(key)) return _extraInfor[key];
            return _extraInfor[key.ToUpper()];
        }

        public int GetInt(string name)
        {
            name = name.ToUpper();
            if (DATA != null && DATA.ContainsKey(name))
            {
                return ObjectAndString.ObjectToInt(DATA[name]);
            }
            return 0;
        }

        /// <summary>
        /// Lấy thông tin theo trường, Trim().
        /// </summary>
        /// <param name="name">Tên trường dữ liệu</param>
        /// <returns></returns>
        public string GetString(string name)
        {
            name = name.ToUpper();
            if (DATA != null && DATA.ContainsKey(name))
            {
                return ("" + DATA[name]).Trim();
            }
            return null;
        }
    }

    public class AlbcConfig : Config
    {
        public AlbcConfig(IDictionary<string, object> data)
            : base(data)
        {
            LoadExtraInfor();
        }

        public AlbcConfig()
        {
            NoInfo = true;
            Error = true;
        }

        public int STT { get { return GetInt("STT"); } }
        public string MA_FILE { get { return GetString("MA_FILE"); } }
        public string NHOM_BC { get { return GetString("NHOM_BC"); } }
        public string MAU { get { return GetString("MAU"); } }
        public string LAN { get { return GetString("LAN"); } }
        public string REPORT { get { return GetString("REPORT"); } }
        public string CAPTION { get { return GetString("CAPTION"); } }
        public string CAPTION2 { get { return GetString("CAPTION2"); } }
        public string TITLE { get { return GetString("TITLE"); } }
        public int ISUSER { get { return GetInt("ISUSER"); } }
        public int DEFAULT { get { return GetInt("DEFAULT"); } }
        public string FILTER { get { return GetString("FILTER"); } }
        public string SCRIPT { get { return GetString("SCRIPT"); } }
        public string SUBREPORT { get { return GetString("SUBREPORT"); } }
        public string REFREPORT { get { return GetString("REFREPORT"); } }
        public int MAU_TU_IN { get { return GetInt("MAU_TU_IN"); } }
        /// <summary>
        /// Số int của 2bin ví dụ 2 = 10(2) chkCheckPrint = 1, chkND51(mauhd) = 0, 3=11(2)
        /// <para>check > 0 để tăng sl_in</para>
        /// </summary>
        public int ND51 { get { return GetInt("ND51"); } }
        /// <summary>
        /// Chỉ in 1 lần. Bit 1 của ND51.
        /// </summary>
        public bool ND51_IN1
        {
            get { return (ND51 & (1<<1)) > 0; }
        }
        /// <summary>
        /// Là mẫu hóa đơn. Bit 0 của ND51.
        /// </summary>
        public bool ND51_HD
        {
            get { return (ND51 & 1) > 0; }
        }
        public DateTime DUPDATE { get { return GetDate("DUPDATE"); } }
        public int STATUS { get { return GetInt("STATUS"); } }
        public string RPT_DIR { get { return GetString("RPT_DIR"); } }
        public int HIDDEN { get { return GetInt("HIDDEN"); } }
        public int SO_LIEN { get { return GetInt("SO_LIEN"); } }
        public int SL_DONG { get { return GetInt("SL_DONG"); } }
        public int PRE_SHOW { get { return GetInt("PRE_SHOW"); } }
        public DateTime DATE0 { get { return GetDate("DATE0"); } }
        public string TIME0 { get { return GetString("TIME0"); } }
        public int USER_ID0 { get { return GetInt("USER_ID0"); } }
        public DateTime DATE2 { get { return GetDate("DATE2"); } }
        public string TIME2 { get { return GetString("TIME2"); } }
        public int USER_ID2 { get { return GetInt("USER_ID2"); } }
        public string UID { get { return GetString("UID"); } }
        public string EXCEL2 { get { return GetString("EXCEL2"); } }
        public string EXCEL1 { get { return GetString("EXCEL1"); } }
        public string LIEN_TUC { get { return GetString("LIEN_TUC"); } }
        public string FIELDV { get { return GetString("FIELDV"); } }
        public string VALUEV { get { return GetString("VALUEV"); } }
        public string BOLD_YN { get { return GetString("BOLD_YN"); } }
        public string COLOR_YN { get { return GetString("COLOR_YN"); } }
        public string COLORV { get { return GetString("COLORV"); } }
        public string PRINTER_DEF { get { return GetString("PRINTER_DEF"); } }
        public string PRINTER_YN { get { return GetString("PRINTER_YN"); } }
        public string R_PRINT2 { get { return GetString("R_PRINT2"); } }
        public int INHERIT_CH { get { return GetInt("INHERIT_CH"); } }
        public string RIGHT_YN { get { return GetString("RIGHT_YN"); } }
        public string GRDS_V1 { get { return GetString("GRDS_V1"); } }
        public string GRDF_V1 { get { return GetString("GRDF_V1"); } }
        public string GRDS_V2 { get { return GetString("GRDS_V2"); } }
        public string GRDF_V2 { get { return GetString("GRDF_V2"); } }
        public string GRDH_LANG_V1 { get { return V6Setting.IsVietnamese ? GRDHV_V1 : GRDHE_V1; } }
        public string GRDHV_V1 { get { return GetString("GRDHV_V1"); } }
        public string GRDHE_V1 { get { return GetString("GRDHE_V1"); } }
        public string GRDHV_V2 { get { return GetString("GRDHV_V2"); } }
        public string GRDHE_V2 { get { return GetString("GRDHE_V2"); } }
        public string GRDT_V1 { get { return GetString("GRDT_V1"); } }
        public string GRDT_V2 { get { return GetString("GRDT_V2"); } }
        public string OPERV { get { return GetString("OPERV"); } }
        public string EXCEL2_VIEW { get { return GetString("EXCEL2_VIEW"); } }
        public int FSTART { get { return GetInt("FSTART"); } }
        public int FFIXCOLUMN { get { return GetInt("FFIXCOLUMN"); } }
        public string RELOAD_DATA { get { return GetString("RELOAD_DATA"); } }
        /// <summary>
        /// Thông tin các Parameter thêm cho Báo cáo. 
        /// <para>Extra_para: R_MA_KH là tên parameter trong rpt,</para>
        /// <para>Field là dữ liệu table2 hoặc filterLine...</para>
        /// <para>Ptype:TABLE2, FILTER-lấy dữ liệu của filter,</para>
        /// <para>FILTER_BROTHER-lấy dữ liệu của filterLine.Data,</para>
        /// <para>PARENT - chưa định nghĩa.</para>
        /// </summary>
        public string EXTRA_PARA { get { return GetString("EXTRA_PARA"); } }
        /// <summary>
        /// xml code động.
        /// </summary>
        public string MMETHOD { get { return GetString("MMETHOD"); } }
        public string FIELD_MAX { get { return GetString("FIELD_MAX"); } }
        public int DROP_MAX { get { return GetInt("DROP_MAX"); } }
        public int ROW_MAX { get { return GetInt("ROW_MAX"); } }
        public string FIELD_S { get { return GetString("FIELD_S"); } }
        public string VALUE_S { get { return GetString("VALUE_S"); } }
        public string OPER_S { get { return GetString("OPER_S"); } }
        public SortedDictionary<string, string> EXTRA_INFOR
        {
            get
            {
                if (_extraInfor == null || _extraInfor.Count == 0)
                {
                    LoadExtraInfor();
                }
                return _extraInfor;
            }
        }
        /// <summary>
        /// Zoom mặc định cho crystalreport khi in chứng từ. Nếu chưa cấu hình thì mặc định là 100.             
        /// <para>Crystal: 1 PageWidth, 2 AllPage, >=50 is %</para>
        /// <para>Xtra: 1 = PageWidth, 2 AllPage, >=50 is this %</para>
        /// </summary>
        public int EXTRA_INFOR_PRINTVCZOOM
        {
            get
            {
                int result = 100;
                if (EXTRA_INFOR.ContainsKey("PRINTVCZOOM")) result = ObjectAndString.ObjectToInt(EXTRA_INFOR["PRINTVCZOOM"]);
                return result;
            }
        }
        public int FROZENV { get { return GetInt("FROZENV"); } }

        
    }

    public class AlctConfig : Config
    {
        public AlctConfig(IDictionary<string, object> data)
            : base(data)
        {
        }

        public AlctConfig()
        {
            NoInfo = true;
            Error = true;
        }
        
        /// <summary>
        /// m_phdbf
        /// </summary>
        public string TableNameAM
        {
            get
            {
                return GetString("m_phdbf");
            }
        }
        /// <summary>
        /// m_ctdbf
        /// </summary>
        public string TableNameAD
        {
            get
            {
                return GetString("m_ctdbf");
            }
        }
        /// <summary>
        /// m_list_ct
        /// </summary>
        public string TableNameADlist
        {
            get
            {
                return GetString("m_list_ct");
            }
        }

        public string MODULE_ID { get { return GetString("MODULE_ID"); } }
        public string MA_PHAN_HE { get { return GetString("MA_PHAN_HE"); } }
        public string MA_CT { get { return GetString("MA_CT"); } }
        public string TEN_CT { get { return GetString("TEN_CT"); } }
        public string TEN_CT2 { get { return GetString("TEN_CT2"); } }
        public string MA_CT_ME { get { return GetString("MA_CT_ME"); } }
        public string SO_CT { get { return GetString("SO_CT"); } }
        public string M_MA_NK { get { return GetString("M_MA_NK"); } }
        public string M_MA_GD { get { return GetString("M_MA_GD"); } }
        public string M_MA_TD { get { return GetString("M_MA_TD"); } }
        public string MA_NT { get { return GetString("MA_NT"); } }
        public string TIEU_DE_CT { get { return GetString("TIEU_DE_CT"); } }
        public string TIEU_DE2 { get { return GetString("TIEU_DE2"); } }
        public string SO_LIEN { get { return GetString("SO_LIEN"); } }
        public string MA_CT_IN { get { return GetString("MA_CT_IN"); } }
        public string FORM { get { return GetString("FORM"); } }
        public string FORMCODE { get { return GetString("FORMCODE"); } }
        public string STT_CT_NKC { get { return GetString("STT_CT_NKC"); } }
        public string STT_CTNTXT { get { return GetString("STT_CTNTXT"); } }
        public string CT_NXT { get { return GetString("CT_NXT"); } }
        public string M_PHDBF { get { return GetString("M_PHDBF"); } }
        public string M_CTDBF { get { return GetString("M_CTDBF"); } }
        public string M_STATUS { get { return GetString("M_STATUS"); } }
        public string POST_TYPE { get { return GetString("POST_TYPE"); } }
        public string M_SL_CT0 { get { return GetString("M_SL_CT0"); } }
        public string M_TRUNG_SO { get { return GetString("M_TRUNG_SO"); } }
        public string M_LOC_NSD { get { return GetString("M_LOC_NSD"); } }
        public string M_ONG_BA { get { return GetString("M_ONG_BA"); } }
        public string M_NGAY_CT { get { return GetString("M_NGAY_CT"); } }
        public string PROCEDUR { get { return GetString("PROCEDUR"); } }
        public string DATE2 { get { return GetString("DATE2"); } }
        public string TIME2 { get { return GetString("TIME2"); } }
        public string USER_ID2 { get { return GetString("USER_ID2"); } }
        public string STT { get { return GetString("STT"); } }
        public string M_MA_TD2 { get { return GetString("M_MA_TD2"); } }
        public string M_MA_TD3 { get { return GetString("M_MA_TD3"); } }
        public string M_NGAY_TD1 { get { return GetString("M_NGAY_TD1"); } }
        public string M_SL_TD1 { get { return GetString("M_SL_TD1"); } }
        public string M_SL_TD2 { get { return GetString("M_SL_TD2"); } }
        public string M_SL_TD3 { get { return GetString("M_SL_TD3"); } }
        public string M_GC_TD1 { get { return GetString("M_GC_TD1"); } }
        public string M_GC_TD2 { get { return GetString("M_GC_TD2"); } }
        public string M_GC_TD3 { get { return GetString("M_GC_TD3"); } }
        public string POST2 { get { return GetString("POST2"); } }
        public string POST3 { get { return GetString("POST3"); } }
        public string M_NGAY_TD2 { get { return GetString("M_NGAY_TD2"); } }
        public string M_NGAY_TD3 { get { return GetString("M_NGAY_TD3"); } }
        public string DK_CTGS { get { return GetString("DK_CTGS"); } }
        public string KH_YN { get { return GetString("KH_YN"); } }
        public string CC_YN { get { return GetString("CC_YN"); } }
        public string NV_YN { get { return GetString("NV_YN"); } }
        public string MA_CT_OLD { get { return GetString("MA_CT_OLD"); } }
        public string M_PH_OLD { get { return GetString("M_PH_OLD"); } }
        public string M_BP_BH { get { return GetString("M_BP_BH"); } }
        public string M_MA_NVIEN { get { return GetString("M_MA_NVIEN"); } }
        public bool XtraReport { get { return M_MA_NVIEN == "1"; } }
        public string M_MA_VV { get { return GetString("M_MA_VV"); } }
        public string M_MA_HD { get { return GetString("M_MA_HD"); } }
        public string M_MA_KU { get { return GetString("M_MA_KU"); } }
        public string M_MA_PHI { get { return GetString("M_MA_PHI"); } }
        public string M_MA_VITRI { get { return GetString("M_MA_VITRI"); } }
        public string M_MA_LO { get { return GetString("M_MA_LO"); } }
        public string M_MA_BPHT { get { return GetString("M_MA_BPHT"); } }
        public string M_MA_SP { get { return GetString("M_MA_SP"); } }
        public string M_K_POST { get { return GetString("M_K_POST"); } }
        public string TK_NO { get { return GetString("TK_NO"); } }
        public string TK_CO { get { return GetString("TK_CO"); } }
        public string M_MA_LNX { get { return GetString("M_MA_LNX"); } }
        public string M_HSD { get { return GetString("M_HSD"); } }
        public string M_MA_SONB { get { return GetString("M_MA_SONB"); } }
        public string M_SXOA_NSD { get { return GetString("M_SXOA_NSD"); } }
        public string SIZE_CT { get { return GetString("SIZE_CT"); } }
        public string THEM_IN { get { return GetString("THEM_IN"); } }
        public string PHANDAU { get { return GetString("PHANDAU"); } }
        public string PHANCUOI { get { return GetString("PHANCUOI"); } }
        public string DINHDANG { get { return GetString("DINHDANG"); } }
        public string UID { get { return GetString("UID"); } }
        public string M_GTDBF { get { return GetString("M_GTDBF"); } }
        public string ADV_AM { get { return GetString("ADV_AM"); } }
        public string ADV_AD { get { return GetString("ADV_AD"); } }
        public string DROP_MAX { get { return GetString("DROP_MAX"); } }
        public string M_KTDBF { get { return GetString("M_KTDBF"); } }
        public string GRDS_AM { get { return GetString("GRDS_AM"); } }
        public string GRDF_AM { get { return GetString("GRDF_AM"); } }
        public string GRDS_AD { get { return GetString("GRDS_AD"); } }
        public string GRDF_AD { get { return GetString("GRDF_AD"); } }
        public string GRDHV_AM { get { return GetString("GRDHV_AM"); } }
        public string GRDHE_AM { get { return GetString("GRDHE_AM"); } }
        public string GRDHV_AD { get { return GetString("GRDHV_AD"); } }
        public string GRDHE_AD { get { return GetString("GRDHE_AD"); } }
        public string GRDT_AM { get { return GetString("GRDT_AM"); } }
        public string GRDT_AD { get { return GetString("GRDT_AD"); } }
        /// <summary>
        /// 1 Thông thường. 2 Có thêm LeftPanel
        /// </summary>
        public string TYPE_VIEW { get { return GetString("TYPE_VIEW"); } }
        public string GRDS_Q1 { get { return GetString("GRDS_Q1"); } }
        public string GRDF_Q1 { get { return GetString("GRDF_Q1"); } }
        public string GRDHV_Q1 { get { return GetString("GRDHV_Q1"); } }
        public string GRDHE_Q1 { get { return GetString("GRDHE_Q1"); } }
        public string MA_POST { get { return GetString("MA_POST"); } }
        public string NGAY_KS_KY { get { return GetString("NGAY_KS_KY"); } }
        public string AM_TEMPLATE { get { return GetString("AM_TEMPLATE"); } }
        public string AD_TEMPLATE { get { return GetString("AD_TEMPLATE"); } }
        public string MMETHOD { get { return GetString("MMETHOD"); } }
        public string M_LIST_CT { get { return GetString("M_LIST_CT"); } }
        public string WRITE_LOG { get { return GetString("WRITE_LOG"); } }
        public SortedDictionary<string, string> EXTRA_INFOR
        {
            get
            {
                if (_extraInfor == null || _extraInfor.Count == 0)
                {
                    LoadExtraInfor();
                }
                return _extraInfor;
            }
        }
        public string KEY_AM2TH { get { return GetString("KEY_AM2TH"); } }


    }

    public class AldmConfig : Config
    {
        public string ma_dm;
        public AldmConfig(IDictionary<string, object> data)
            : base(data)
        {
        }

        /// <summary>
        /// Tạo object rỗng.
        /// </summary>
        public AldmConfig()
        {
            NoInfo = true;
            Error = true;
        }

        public int STT { get { return GetInt("STT"); } }
        public string NHOM_DM { get { return GetString("NHOM_DM"); } }
        public string ATTRIBUTE { get { return GetString("ATTRIBUTE"); } }
        /// <summary>
        /// Mã dùng cho textbox lookup (giống vvar).
        /// </summary>
        public string MA_DM { get { return GetString("MA_DM"); } }
        public string TEN_DM { get { return GetString("TEN_DM"); } }
        public string TABLE_NAME { get { return GetString("TABLE_NAME"); } }
        public string TABLE_VIEW { get { return GetString("TABLE_VIEW"); } }
        /// <summary>
        /// Các trường khóa của danh mục.
        /// </summary>
        public string KEY { get { return GetString("KEY"); } }
        /// <summary>
        /// Tên trường mã của danh mục.
        /// </summary>
        public string VALUE { get { return GetString("VALUE"); } }
        public string STATUS { get { return GetString("STATUS"); } }
        public string ALCT { get { return GetString("ALCT"); } }
        public string F_CT { get { return GetString("F_CT"); } }
        public string F_GT { get { return GetString("F_GT"); } }
        public string F_ARA00 { get { return GetString("F_ARA00"); } }
        /// <summary>
        /// Công thức 1. vd THANH_TIEN=SO_LUONG*DON_GIA.
        /// <para>Đã có xử lý tính nối tiếp ở xuly48.</para>
        /// <para>Ví dụ gõ SO_MOI sẽ tính SO_LUONG, => tính tiếp THANH_TIEN.</para>
        /// </summary>
        public string CACH_TINH1 { get { return GetString("CACH_TINH1"); } }
        public string CACH_TINH2 { get { return GetString("CACH_TINH2"); } }
        public string CACH_TINH3 { get { return GetString("CACH_TINH3"); } }
        public string CAPTION { get { return GetString("CAPTION"); } }
        public string CAPTION2 { get { return GetString("CAPTION2"); } }
        public string POST { get { return GetString("POST"); } }
        public string ORDER { get { return GetString("ORDER"); } }
        public string SEARCH { get { return GetString("SEARCH"); } }
        public string F_SEARCH { get { return GetString("F_SEARCH"); } }
        public string SEARCH0 { get { return GetString("SEARCH0"); } }
        public string FILTER { get { return GetString("FILTER"); } }
        /// <summary>
        /// Field:EorR:CVvar;Field:E:N2;Field,E:D0;Field:R:D1 Định nghĩa trường edit.
        /// </summary>
        public string FIELD { get { return GetString("FIELD"); } }
        public string FIELD2 { get { return GetString("FIELD2"); } }
        /// <summary>
        /// BOLD=1
        /// </summary>
        public string FIELDV { get { return GetString("FIELDV"); } }
        public string OPERV { get { return GetString("OPERV"); } }
        public string VALUEV { get { return GetString("VALUEV"); } }
        public string COLORV { get { return GetString("COLORV"); } }
        public string A_FIELD { get { return GetString("A_FIELD"); } }
        public string A_FIELD2 { get { return GetString("A_FIELD2"); } }
        /// <summary>
        /// Trường chọn để lọc trong DanhMucView.
        /// </summary>
        public string FILTER_FIELD { get { return GetString("FILTER_FIELD"); } }
        public string NROW { get { return GetString("NROW"); } }
        public string TITLE { get { return GetString("TITLE"); } }
        public string TITLE2 { get { return GetString("TITLE2"); } }
        public string TITLE_U { get { return GetString("TITLE_U"); } }
        public string TITLE_U2 { get { return GetString("TITLE_U2"); } }
        public string LOOKUP { get { return GetString("LOOKUP"); } }
        /// <summary>
        /// Trường mã của bảng khác ánh xạ qua Table_key (;) AlmapCQT 2 trường thay thế from;to.
        /// </summary>
        public string DOI_MA { get { return GetString("DOI_MA"); } }
        public string V_TYPE { get { return GetString("V_TYPE"); } }
        public string I_LOOKUP { get { return GetString("I_LOOKUP"); } }
        public string MA_PHAN_HE { get { return GetString("MA_PHAN_HE"); } }
        public string HIDE_YN { get { return GetString("HIDE_YN"); } }
        public string GRD_COL { get { return GetString("GRD_COL"); } }
        public string CLASS { get { return GetString("CLASS"); } }
        public string NXT { get { return GetString("NXT"); } }
        public string F2 { get { return GetString("F2"); } }
        /// <summary>
        /// Cấu hình có sử dụng F3 để sửa khi đang chọn lookup.
        /// </summary>
        public bool F3 { get { return GetString("F3") == "1"; } }
        /// <summary>
        /// Cấu hình có sử dụng F4 để thêm khi đang chọn lookup.
        /// </summary>
        public bool F4 { get { return GetString("F4") == "1"; } }
        /// <summary>
        ///Cho phép bấm  F6
        /// </summary>
        public bool F6 { get { return GetString("F6") == "1"; } }
        public bool CHECK_ADMIN { get { return GetString("CHECK_ADMIN") == "1"; } }
        public bool CHECK_LONG { get { return GetString("CHECK_LONG") == "1"; } }
        public bool CHECK_V6 { get { return GetString("CHECK_V6") == "1"; } }
        public string CTRL_F4 { get { return GetString("CTRL_F4"); } }
        public string DMETHOD { get { return GetString("DMETHOD"); } }
        public string DUPDATE { get { return GetString("DUPDATE"); } }
        public string TRANSFORM { get { return GetString("TRANSFORM"); } }
        public string STT13 { get { return GetString("STT13"); } }
        public string F6_TABLE { get { return GetString("F6_TABLE"); } }
        public string F8_TABLE { get { return GetString("F8_TABLE"); } }
        public string DMFIX { get { return GetString("DMFIX"); } }
        public string IN_TYPE { get { return GetString("IN_TYPE"); } }
        public string LOOKUP_POS { get { return GetString("LOOKUP_POS"); } }
        public string FORM { get { return GetString("FORM"); } }
        public string EXPR1 { get { return GetString("EXPR1"); } }
        public string UID { get { return GetString("UID"); } }
        /// <summary>
        /// Trường lấy dữ liệu hiển thị.
        /// </summary>
        public string F_NAME { get { return GetString("F_NAME"); } }
        public string B_FIELD { get { return GetString("B_FIELD"); } }
        public string B_FIELD2 { get { return GetString("B_FIELD2"); } }
        public string GRDS_V1 { get { return GetString("GRDS_V1"); } }
        public string GRDF_V1 { get { return GetString("GRDF_V1"); } }
        /// <summary>
        /// Tiêu đề tương ứng GRDS_V1.
        /// </summary>
        public string GRDH_LANG_V1 { get { return V6Setting.IsVietnamese ? GRDHV_V1 : GRDHE_V1; } }
        public string GRDHV_V1 { get { return GetString("GRDHV_V1"); } }
        public string GRDHE_V1 { get { return GetString("GRDHE_V1"); } }
        /// <summary>
        /// Cột không sum gridViewSummary
        /// </summary>
        public string GRDT_V1 { get { return GetString("GRDT_V1"); } }
        public bool IsGroup { get { return GetString("IS_GROUP") == "1"; } }
        public bool IS_ALDM { get { return GetString("IS_ALDM") == "1"; } }

        public string TABLE_KEY { get { return GetString("TABLE_KEY"); } }
        /// <summary>
        /// Thông tin nhóm. DataTable,IdField,FieldNhom
        /// </summary>
        public string L_ALDM { get { return GetString("L_ALDM"); } }
        public string Vorder { get { return GetString("ORDER"); } }

        public string ADV_FILTER { get { return GetString("ADV_FILTER"); } }
        public bool BOLD_YN { get { return GetString("BOLD_YN") == "1"; } }
        public bool COLOR_YN { get { return GetString("COLOR_YN") == "1"; } }
        public int FROZENV { get { return GetInt("FROZENV"); } }
        public bool INCREASE_YN { get { return GetString("INCREASE_YN") == "1"; } }
        public string VName { get { return GetString("VNAME"); } }
        public string VName2 { get { return GetString("VNAME2"); } }
        /// <summary>
        /// Code form tùy chỉnh cho từng khách hàng của V6 (Dùng form khác cho cùng 1 danh mục).
        /// </summary>
        public string FormCode { get { return GetString("FORMCODE"); } }
        /// <summary>
        /// Dic dang MA_VT:XXXX;FIELD2:ABC
        /// </summary>
        public string VALID_CHARS { get { return GetString("VALID_CHARS"); } }
        
        public SortedDictionary<string, string> EXTRA_INFOR
        {
            get
            {
                if (_extraInfor == null || _extraInfor.Count == 0)
                {
                    LoadExtraInfor();
                }
                return _extraInfor;
            }
        }
        /// <summary>
        /// Lay mot phan tu trong EXTRA_INFOR, ko co tra ve null.
        /// </summary>
        /// <param name="KEY"></param>
        /// <returns></returns>
        public string EXTRA_INFOR_NULL(string KEY)
        {
            KEY = KEY.ToUpper();
            if (!EXTRA_INFOR.ContainsKey(KEY)) return null;
            return "" + EXTRA_INFOR[KEY];
        }

        private Dictionary<string, V6FieldInfo> _v6FieldInfos = new Dictionary<string, V6FieldInfo>();
        public Dictionary<string, V6FieldInfo> V6FieldInfos
        {
            get
            {
                if (_v6FieldInfos.Count == 0) GetV6FieldInfos();
                return _v6FieldInfos;
            }
        }

        private void GetV6FieldInfos()
        {
            try
            {
                var fields = ObjectAndString.SplitString(GRDS_V1);
                var captions = ObjectAndString.SplitString(GRDH_LANG_V1);
                var formats = ObjectAndString.SplitString(GRDF_V1);

                for (int i = 0; i < fields.Length; i++)
                {
                    string FIELD_NAME = fields[i].Trim().ToUpper();
                    string caption = captions[i].Trim();
                    string format = formats[i].Trim();

                    _v6FieldInfos[FIELD_NAME] = new V6FieldInfo(){FieldName = FIELD_NAME, FieldCaption = caption, FieldFormat = format};
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        
    }

    public class UserConfig : Config
    {
        public UserConfig(IDictionary<string, object> data)
            : base(data)
        {
        }

        
        
        public string Module_id { get { return GetString("Module_id"); } }
        
        public decimal user_id { get { return GetInt("user_id"); } }
        
        
        public string user_name { get { return GetString("user_name"); } }
        
        public string user_pre { get { return GetString("user_pre"); } }
        
        public string password { get { return GetString("password"); } }
        public string comment { get { return GetString("comment"); } }
        public bool is_admin { get { return GetInt("is_admin") == 1; } }
        public bool is_Vadmin { get { return GetInt("is_Vadmin") == 1; } }
        public string rights { get { return GetString("rights"); } }
        public decimal del_yn { get { return GetInt("del_yn"); } }
        public string r_del { get { return GetString("r_del"); } }
        
        public string r_edit { get { return GetString("r_edit"); } }
        
        public string r_add { get { return GetString("r_add"); } }
        public decimal user_id2 { get { return GetInt("user_id2"); } }
        
        public byte user_acc { get { return (byte)GetInt("user_acc"); } }
        
        public byte user_inv { get { return (byte)GetInt("user_inv"); } }
        
        public byte Pagedefa { get { return (byte)GetInt("Pagedefa"); } }
        
        public string R_view { get { return GetString("R_view"); } }
        
        public string R_print { get { return GetString("R_print"); } }
        
        public byte user_sale { get { return (byte)GetInt("user_sale"); } }
        
        public string level { get { return GetString("level"); } }
        
        public string R_dvcs { get { return GetString("R_dvcs"); } }
        
        public string UID { get { return GetString("UID"); } }
        public string CODE_USER { get { return GetString("CODE_USER"); } }
        
        public string R_kho { get { return GetString("R_kho"); } }
        public decimal Inherit_id { get { return GetInt("Inherit_id"); } }
        
        public byte Inherit_ch { get { return (byte)GetInt("Inherit_ch"); } }
        
        public string R_sonb { get { return GetString("R_sonb"); } }
        public string Inherit_type { get { return GetString("Inherit_type"); } }
        public DateTime pass_date { get { return GetDate("pass_date"); } }
        
        public string Pass_Exp { get { return GetString("Pass_Exp"); } }
        public string XML_INFOR { get { return GetString("XML_INFOR"); } }
        
        public string r_copy { get { return GetString("r_copy"); } }  
    }

    public class V6FieldInfo
    {
        public string FieldName { get; set; }
        public string FieldCaption { get; set; }
        public string FieldFormat { get; set; }
    }

    public class AlreportConfig : Config
    {
        public AlreportConfig(IDictionary<string, object> data)
            : base(data)
        {
        }

        /// <summary>
        /// Tạo object rỗng.
        /// </summary>
        public AlreportConfig()
        {
            NoInfo = true;
            Error = true;
        }

        public string ten { get { return GetString("ten"); } }
        public string ten2 { get { return GetString("ten2"); } }
        public string loai_bc { get { return GetString("loai_bc"); } }
        public string mo_ta { get { return GetString("mo_ta"); } }
        public string vitri { get { return GetString("vitri"); } }
        public string proc { get { return GetString("proc"); } }
        public string vbrowse1 { get { return GetString("vbrowse1"); } }
        public string ebrowse1 { get { return GetString("ebrowse1"); } }
        public string l_tk_no { get { return GetString("l_tk_no"); } }
        public string l_tk_co { get { return GetString("l_tk_co"); } }
        public string donvitinh { get { return GetString("donvitinh"); } }
        public string user_id { get { return GetString("user_id"); } }
        /// <summary>
        /// Các Defineinfo Name Value cách nhau bằng dấu ~ (Name:Số lượng;Value:Ten_vt,So_luong1,So_luong2~...)
        /// </summary>
        public string Combo_data { get { return GetString("Combo_data"); } }
        public string type { get { return GetString("type"); } }
        public bool F3 { get { return GetString("F3") == "1"; } }
        public bool F5 { get { return GetString("F5") == "1"; } }
        public bool F7 { get { return GetString("F7") == "1"; } }
        public string F_KEYS { get { return GetString("F_KEYS"); } }
        /// <summary>
        /// Kiểm tra chuỗi F có nằm trong F_KEYS hay không (giữa các chấm phẩy ;).
        /// </summary>
        /// <param name="F"></param>
        /// <returns></returns>
        public bool F_KEYS_CHECK(string F)
        {
            string s = ";" + F_KEYS + ";";
            return s.Contains(";" + F + ";");
        }
        public string UID { get { return GetString("UID"); } }
        /// <summary>
        /// ~Name:NGAY;Ptype:TABLE2;Field:R_DMY 
        /// </summary>
        public string Extra_para { get { return GetString("Extra_para"); } }
        public string ADVANCE { get { return GetString("ADVANCE"); } }
        public string MMETHOD { get { return GetString("MMETHOD"); } }
        public bool VIEWSUM { get { return GetString("VIEWSUM") == "1"; } }

    }

    public class Alreport1Config : Config
    {
        public Alreport1Config(IDictionary<string, object> data)
            : base(data)
        {
        }

        public string ma_bc { get { return GetString("MA_BC"); } }
        public string stt_filter { get { return GetString("STT_FILTER"); } }
        public string stt_key { get { return GetString("STT_KEY"); } }
        public string ten { get { return GetString("TEN"); } }
        public string filter { get { return GetString("FILTER"); } }
        public string loai_key { get { return GetString("LOAI_KEY"); } }
        public string key1 { get { return GetString("KEY1"); } }
        public string key2 { get { return GetString("KEY2"); } }
        public string key3 { get { return GetString("KEY3"); } }
        public string key4 { get { return GetString("KEY4"); } }
        public string UID { get { return GetString("UID"); } }
        public string user_id2 { get { return GetString("USER_ID2"); } }
        public string ten2 { get { return GetString("TEN2"); } }
        public string uid_ct { get { return GetString("UID_CT"); } }
        public string DMETHOD { get { return GetString("DMETHOD"); } }
        public string filter_m { get { return GetString("FILTER_M"); } }
        public string DMETHOD_M { get { return GetString("DMETHOD_M"); } }
    }


    public class V6lookupConfig : Config
    {
        public V6lookupConfig(IDictionary<string, object> data)
            : base(data)
        {
            
        }

        public V6lookupConfig()
        {
        }

        /// <summary>
        /// Lấy các trường lọc danh mục view field:vvar:filter:table:like
        /// </summary>
        /// <returns>Mảng các trường vFields hoặc eFields</returns>
        public string[] GetDefaultLookupFields
        {
            get
            {
                string lang = V6Setting.Language;
                string result = GetString(lang + "Fields");
                return ObjectAndString.SplitString(result);
            }
        }
        public string DOI_MA { get { return GetString("DOI_MA"); } }
        public string vVar { get { return GetString("vVar"); } }
        /// <summary>
        /// TableName
        /// </summary>
        public string vMa_file { get { return GetString("vMa_file"); } }
        /// <summary>
        /// như vMa_file
        /// </summary>
        public string TableName { get { return GetString("vMa_file"); } }
        public string VName { get { return GetString("VName"); } }
        public string VName2 { get { return GetString("VName2"); } }
        public string vOrder { get { return GetString("vOrder"); } }
        /// <summary>
        /// MainField
        /// </summary>
        public string vValue { get { return GetString("vValue"); } }
        public string vLfScatter { get { return GetString("vLfScatter"); } }
        public string vWidths { get { return GetString("vWidths"); } }
        public string vFields { get { return GetString("vFields"); } }
        public string eFields { get { return GetString("eFields"); } }
        public string vHeaders { get { return GetString("vHeaders"); } }
        public string eHeaders { get { return GetString("eHeaders"); } }
        public string vUpdate { get { return GetString("vUpdate"); } }
        public string vTitle { get { return GetString("vTitle"); } }
        public string eTitle { get { return GetString("eTitle"); } }
        public string VTitlenew { get { return GetString("VTitlenew"); } }
        public string ETitlenew { get { return GetString("ETitlenew"); } }
        public bool Large_yn { get { return GetString("Large_yn") == "1"; } }
        public string v1Title { get { return GetString("v1Title"); } }
        public string e1Title { get { return GetString("e1Title"); } }
        public string V_Search { get { return GetString("V_Search"); } }
        public string ListTable { get { return GetString("ListTable"); } }
        public string INITFILTER { get { return GetString("INITFILTER"); } }
        public string V_HIDE { get { return GetString("V_HIDE"); } }
        public bool LOAD_AUTO { get { return GetString("LOAD_AUTO") == "1"; } }
        public string GRDS_V1 { get { return GetString("GRDS_V1"); } }
        public string GRDF_V1 { get { return GetString("GRDF_V1"); } }
        public string GRDHV_V1 { get { return GetString("GRDHV_V1"); } }
        public string GRDHE_V1 { get { return GetString("GRDHE_V1"); } }
        public bool F3 { get { return GetString("F3") == "1"; } }
        public bool F4 { get { return GetString("F4") == "1"; } }
        public string Filter_All { get { return GetString("Filter_All"); } }
        public string FILTER_FIELD { get { return GetString("FILTER_FIELD"); } }
        /// <summary>
        /// BOLD=1
        /// </summary>
        public string FIELDV { get { return GetString("FIELDV"); } }
        public string OPERV { get { return GetString("OPERV"); } }
        public string VALUEV { get { return GetString("VALUEV"); } }
        public string COLORV { get { return GetString("COLORV"); } }
        public bool BOLD_YN { get { return GetString("BOLD_YN") == "1"; } }
        public bool COLOR_YN { get { return GetString("COLOR_YN") == "1"; } }
        public int FROZENV { get { return GetInt("FROZENV"); } }
    }

    public class V6ValidConfig : Config
    {
        public V6ValidConfig(IDictionary<string, object> data)
            : base(data)
        {
        }

        public V6ValidConfig()
        {
        }

        public int stt { get { return GetInt("stt"); } }
        public string nhom { get { return GetString("nhom"); } }
        public int attribute { get { return GetInt("attribute"); } }
        public string ma_ct { get { return GetString("ma_ct"); } }
        public string ma { get { return GetString("ma"); } }
        public string ten { get { return GetString("ten"); } }
        public string table_name { get { return GetString("table_name"); } }
        public string table_view { get { return GetString("table_view"); } }
        /// <summary>
        /// Các trường chi tiết bắt buộc nhập liệu.
        /// </summary>
        public string A_field { get { return GetString("A_field"); } }
        /// <summary>
        /// Trường vvar cần kiểm tra hợp lệ dữ liệu.
        /// </summary>
        public string A_field2 { get { return GetString("A_field2"); } }
        public string A_field3 { get { return GetString("A_field3"); } }
        public string A_field4 { get { return GetString("A_field4"); } }
        public string UID { get { return GetString("UID"); } }

    }

    public class ALIM2XLS_CONFIG : Config
    {
        public ALIM2XLS_CONFIG(IDictionary<string, object> data) : base(data)
        {
        }

        public SortedDictionary<string, string> EXTRA_INFOR
        {
            get
            {
                if (_extraInfor == null || _extraInfor.Count == 0)
                {
                    LoadExtraInfor();
                }
                return _extraInfor;
            }
        }

        public string MA_CT { get { return GetString("MA_CT"); } }

        /// <summary>
        /// Lay mot phan tu trong EXTRA_INFOR, ko co tra ve null.
        /// </summary>
        /// <param name="KEY"></param>
        /// <returns></returns>
        public string EXTRA_INFOR_NULL(string KEY)
        {
            KEY = KEY.ToUpper();
            if (!EXTRA_INFOR.ContainsKey(KEY)) return null;
            return "" + EXTRA_INFOR[KEY];
        }
    }


    public class ConfigManager
    {
        public static AlbcConfig GetAlbcConfig(string mau, string lan, string ma_file, string report)
        {
            AlbcConfig lstConfig = null;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@p0", ma_file),
                    new SqlParameter("@p1", mau),
                    new SqlParameter("@p2", lan),
                    new SqlParameter("@p3", report),
                };
                var executeResult = V6BusinessHelper.Select("ALBC", "*", "ma_file=@p0 and mau=@p1 and lan=@p2 and report=@p3", "", "", param);
                
                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AlbcConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig = new AlbcConfig();
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        public static AlbcConfig GetAlbcConfig_reportfile(string mau, string lan, string report)
        {
            AlbcConfig lstConfig = null;
            try
            {
                SqlParameter[] param =
                {
                    //new SqlParameter("@p0", ma_file),
                    new SqlParameter("@p1", mau),
                    new SqlParameter("@p2", lan),
                    new SqlParameter("@p3", report),
                };
                var executeResult = V6BusinessHelper.Select("ALBC", "*", "mau=@p1 and lan=@p2 and report=@p3", "", "", param);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AlbcConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig = new AlbcConfig();
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        public static AlbcConfig GetAlbcConfigByMA_FILE(string ma_file)
        {
            AlbcConfig lstConfig = null;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@p0", ma_file)
                };
                var executeResult = V6BusinessHelper.Select("ALBC", "*", "ma_file=@p0", "", "", param);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AlbcConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig = new AlbcConfig();
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        public static UserConfig GetUserConfig(string userName)
        {   
            var data = SqlConnect.SelectV6User(userName);
            UserConfig config = new UserConfig(data.ToDataDictionary());
            return config;
        }

        public static AlctConfig GetAlctConfig(string ma_ct)
        {
            AlctConfig config = new AlctConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", ma_ct) };
                var executeResult = V6BusinessHelper.Select("Alct", "*", "Ma_ct=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    config = new AlctConfig(row.ToDataDictionary());
                }
                else
                {
                    config.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                config.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return config;
        }

        public static AldmConfig GetAldmConfig(string ma_dm)
        {
            AldmConfig lstConfig = null;
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", ma_dm) };
                var executeResult = V6BusinessHelper.Select("Aldm", "*", "Ma_dm=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AldmConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig = new AldmConfig();
                    lstConfig.NoInfo = true;
                    lstConfig.ma_dm = ma_dm;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        public static AldmConfig GetAldmConfigByTableName(string table_name)
        {
            AldmConfig lstConfig = new AldmConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", table_name) };
                var executeResult = V6BusinessHelper.Select("Aldm", "*", "Table_name=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AldmConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }


        public static AlreportConfig GetAlreportConfig(string ma_bc)
        {
            AlreportConfig lstConfig = new AlreportConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", ma_bc) };
                var executeResult = V6BusinessHelper.Select("ALREPORT", "*", "Ma_bc=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AlreportConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        /// <summary>
        /// Lấy thông tin V6Valid cho chứng từ theo ma_ct.
        /// </summary>
        /// <param name="ma_ct"></param>
        /// <param name="attribute">2 detail1 3 danh mục 4 detail3</param>
        /// <returns></returns>
        public static V6ValidConfig GetV6ValidConfig(string ma_ct, int attribute)
        {
            V6ValidConfig lstConfig = new V6ValidConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p1", ma_ct), new SqlParameter("@p2", attribute) };
                var executeResult = V6BusinessHelper.Select("V6Valid", "*", "[ma_ct]=@p1 and [attribute]=@p2", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6ValidConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        /// <summary>
        /// Lấy thông tin V6Valid cho danh mục theo tableName.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static V6ValidConfig GetV6ValidConfigDanhMuc(string tableName)
        {
            V6ValidConfig lstConfig = new V6ValidConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", tableName) };
                var executeResult = V6BusinessHelper.Select("V6Valid", "*", "attribute=3 and [TABLE_NAME]=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6ValidConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(String.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }
    }
}

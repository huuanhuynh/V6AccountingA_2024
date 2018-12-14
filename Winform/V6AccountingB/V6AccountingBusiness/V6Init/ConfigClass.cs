using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
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
            DATA = data;
            NoInfo = false;
        }
        [DefaultValue(true)]
        public bool NoInfo { get; internal set; }
        public bool HaveInfo { get { return !NoInfo; } }
        public bool Error { get; set; }

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
                return DATA[name].ToString().Trim();
            }
            return null;
        }
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

        public string TableNameAM
        {
            get
            {
                return GetString("m_phdbf");
            }
        }
        public string TableNameAD
        {
            get
            {
                return GetString("m_ctdbf");
            }
        }
        public string TableNameADlist
        {
            get
            {
                return GetString("m_list_ct");
            }
        }
    }

    public class AldmConfig : Config
    {
        public AldmConfig(IDictionary<string, object> data)
            : base(data)
        {
        }

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
        public string CAPTION { get { return GetString("CAPTION"); } }
        public string CAPTION2 { get { return GetString("CAPTION2"); } }
        public string POST { get { return GetString("POST"); } }
        public string ORDER { get { return GetString("ORDER"); } }
        public string SEARCH { get { return GetString("SEARCH"); } }
        public string F_SEARCH { get { return GetString("F_SEARCH"); } }
        public string SEARCH0 { get { return GetString("SEARCH0"); } }
        public string FILTER { get { return GetString("FILTER"); } }
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
        public string GRDHV_V1 { get { return GetString("GRDHV_V1"); } }
        public string GRDHE_V1 { get { return GetString("GRDHE_V1"); } }
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
    }

    public class V6lookupConfig : Config
    {
        public V6lookupConfig(IDictionary<string, object> data)
            : base(data)
        {
            V6TableName = V6TableHelper.ToV6TableName(vMa_file);
        }

        public V6lookupConfig()
        {
        }

        /// <summary>
        /// Lấy các trường lọc danh mục view
        /// </summary>
        /// <returns>Mảng các trường vFields hoặc eFields</returns>
        public string[] GetDefaultLookupFields
        {
            get
            {
                string lang = V6Setting.Language;
                string result = GetString(lang + "Fields");
                return result.Split(',');
            }
        }
        public V6TableName V6TableName { get; private set; }
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
        public string A_field { get { return GetString("A_field"); } }
        public string A_field2 { get { return GetString("A_field2"); } }
        public string UID { get { return GetString("UID"); } }

    }


    public class ConfigManager
    {
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
            AldmConfig lstConfig = new AldmConfig();
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

        /// <summary>
        /// Lấy thông tin V6Valid cho chứng từ theo ma_ct.
        /// </summary>
        /// <param name="ma_ct"></param>
        /// <param name="attribute"></param>
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

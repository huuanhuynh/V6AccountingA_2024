﻿using System.Collections.Generic;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls
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
        }

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
        /// Lấy thông tin theo trường
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

    public class AldmConfig : Config
    {
        public AldmConfig(IDictionary<string, object> data)
            : base(data)
        {
        }

        public AldmConfig()
        {
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

        public string vVar { get { return GetString("vVar"); } }
        public string vMa_file { get { return GetString("vMa_file"); } }
        public string vOrder { get { return GetString("vOrder"); } }
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
        public string Large_yn { get { return GetString("Large_yn"); } }
        public string v1Title { get { return GetString("v1Title"); } }
        public string e1Title { get { return GetString("e1Title"); } }
        public string V_Search { get { return GetString("V_Search"); } }
        public string ListTable { get { return GetString("ListTable"); } }
        public string INITFILTER { get { return GetString("INITFILTER"); } }
        public string V_HIDE { get { return GetString("V_HIDE"); } }
        public string LOAD_AUTO { get { return GetString("LOAD_AUTO"); } }
        public string GRDS_V1 { get { return GetString("GRDS_V1"); } }
        public string GRDF_V1 { get { return GetString("GRDF_V1"); } }
        public string GRDHV_V1 { get { return GetString("GRDHV_V1"); } }
        public string GRDHE_V1 { get { return GetString("GRDHE_V1"); } }
        public string F3 { get { return GetString("F3"); } }
        public string F4 { get { return GetString("F4"); } }
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

    public class StandardConfig
    {
        public string VLfScatter { get; set; }
        public string VWidths = "";
        public string VFields;
        public string EFields;
        public string VHeaders;
        public string EHeaders;
        public string VUpdate;
        public string VTitle;
        public string ETitle;
        public bool LargeYn;
        public string V1Title;
        public string E1Title;
        /// <summary>
        /// Thông số các trường vSearch ("ma_vt,ten_vt..")
        /// </summary>
        public string VSearch;
        public string Vvar;
        private string _tableName;
        /// <summary>
        /// Tên bảng dạng chuỗi.
        /// </summary>
        public string TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
                _v6TableName = V6TableHelper.ToV6TableName(_tableName);
            }
        }

        private V6TableName _v6TableName = V6TableName.Notable;
        /// <summary>
        /// Tên bảng đã khai báo. Sẽ là NoTable nếu chưa khai báo.
        /// </summary>
        public V6TableName V6TableName { get { return _v6TableName; } }
        public string Vorder;
        /// <summary>
        /// Field Name
        /// </summary>
        public string FieldName;
        public bool LoadAutoComplete { get; set; }
        /// <summary>
        /// Cờ đánh dấu không có thông tin trong V6Lookup
        /// </summary>
        public bool NoInfo { get; set; }

        public bool Error { get; set; }
        /// <summary>
        /// Cấu hình có sử dụng F3 để sửa khi đang chọn lookup.
        /// </summary>
        public bool F3 { get; set; }
        /// <summary>
        /// Cấu hình có sử dụng F4 để thêm khi đang chọn lookup.
        /// </summary>
        public bool F4 { get; set; }

        public string VTitlenew;
        public string ETitlenew;

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

    
    
}

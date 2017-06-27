using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using V6AccountingBusiness;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    public class V6ControlsHelper
    {
        public static bool DisableLookup { get; set; }

        /// <summary>
        /// Hàm cổ trong Standar DAO
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="lstTable"></param>
        /// <returns></returns>
        public static DataTable KiemTraBangTonTai(string tableName, List<MyDataTable> lstTable)
        {
            if (tableName != "" && lstTable != null)
            {
                if (tableName.IndexOf("@", StringComparison.Ordinal) == -1)
                {
                    tableName = "@" + tableName;
                }
                if (lstTable.All(item => item.TableName != tableName)) return null;
                var result = lstTable.Find(tbl => tbl.TableName == tableName);
                return result.ObjTable;
            }
            else
            {
                throw new ArgumentException("KiemTraBangTonTai : tham số không hợp lệ");
            }
        }
        /// <summary>
        /// Lấy thông tin trong bảng V6Lookup
        /// </summary>
        /// <param name="vVar"></param>
        /// <returns></returns>
        public static StandardConfig LayThongTinCauHinh(string vVar)
        {
            var lstConfig = new StandardConfig();
            try
            {
                SqlParameter[] plist = {new SqlParameter("@p", vVar)};
                var executeResult = V6BusinessHelper.Select("V6Lookup", "*", "vVar=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];

                    lstConfig.Vvar = row["VVar"].ToString().Trim();
                    lstConfig.TableName = row["vMa_file"].ToString().Trim();
                    lstConfig.Vorder = row["vOrder"].ToString().Trim();
                    lstConfig.FieldName = row["vValue"].ToString().Trim();
                    lstConfig.VLfScatter = row["vLfScatter"].ToString().Trim();
                    lstConfig.VWidths = (row["vWidths"].ToString().Trim());
                    lstConfig.VFields = (row["vFields"].ToString().Trim());
                    lstConfig.EFields = row["eFields"].ToString().Trim();
                    lstConfig.VHeaders = (row["vHeaders"].ToString().Trim());
                    lstConfig.EHeaders = (row["eHeaders"].ToString().Trim());
                    lstConfig.VUpdate = (row["vUpdate"].ToString().Trim());
                    lstConfig.VTitle = (row["vTitle"].ToString().Trim());
                    lstConfig.ETitle = (row["eTitle"].ToString().Trim());
                    lstConfig.VTitlenew = (row["VTitlenew"].ToString().Trim());
                    lstConfig.ETitlenew = (row["ETitlenew"].ToString().Trim());
                    lstConfig.LargeYn = Convert.ToInt32(row["Large_yn"]) == 1;
                    lstConfig.LoadAutoComplete = row["LOAD_AUTO"].ToString().Trim() == "1";
                    try
                    {
                        lstConfig.V1Title = (row["v1Title"].ToString().Trim());
                    }
                    catch
                    {
                        lstConfig.V1Title = ("Không có tiêu đề!");
                    }
                    try
                    {
                        lstConfig.E1Title = (row["e1Title"].ToString().Trim());
                    }
                    catch
                    {
                        lstConfig.E1Title = ("No title!");
                    }
                    try
                    {
                        lstConfig.VSearch = (row["v_Search"].ToString().Trim());
                    } //index 18
                    catch
                    {
                        lstConfig.VSearch = ("1=1 or 'a' ");
                    }
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
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
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
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
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }
        
        public static String[] SliptString(string inputString, char typeChar)
        {
            if (inputString != "" && typeChar != ' ')
            {
                return inputString.Split(typeChar);
            }
            
            throw new ArgumentException("SliptString : tham số không hợp lệ");
        }
        public static void ThietLapTruongHienThiTrongDataGridView(
                DataGridView dgv,
                string lstStringFieldName,
                string lstStringFieldHeaders,
                string lstStringFieldWidth          )
        {
            if (String.IsNullOrEmpty(lstStringFieldWidth)) lstStringFieldWidth = "100";

            if (lstStringFieldHeaders != "" && lstStringFieldName != "" && lstStringFieldWidth != "")
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].Visible = false;
                }

                // Lấy các cột được phép hiển thị trong DataGridView
                try
                {
                    String[] lstFieldName = SliptString(lstStringFieldName, ',');
                    List<string> lstFieldHeader = SliptString(lstStringFieldHeaders, ',').ToList();
                    List<string> lstFieldWidth = SliptString(lstStringFieldWidth, ',').ToList();
                    while (lstFieldWidth.Count < lstFieldName.Length)
                    {
                        lstFieldWidth.Add(lstFieldWidth[0]);
                    }
                    while (lstFieldHeader.Count < lstFieldName.Length)
                    {
                        lstFieldHeader.Add(lstFieldName[lstFieldHeader.Count]);
                    }
                    //int numColumns = dgv.Columns.Count - 1;
                    //int displayNum = lstDisplayHeader.Length - 1;
                    for (int i = 0; i < lstFieldName.Length; i++)
                    {
                        var field = lstFieldName[i].Trim();
                        var dataGridViewColumn = dgv.Columns[field];
                        if (dataGridViewColumn != null)
                        {
                            dataGridViewColumn.Visible = true;
                            dataGridViewColumn.DisplayIndex = i;
                            dataGridViewColumn.HeaderText = lstFieldHeader[i];
                            dataGridViewColumn.Width = Int32.Parse(lstFieldWidth[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("ThietLapTruongHienThiTrongDataGridView:\n" + ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("V6ControlsHelper.ThietLapTruongHienThiTrongDataGridView : tham số không hợp lệ");
            }
        }
    }

    public class Config
    {
        protected IDictionary<string, object> _data;

        public Config()
        {
            
        }
        public Config(IDictionary<string, object> data)
        {
            _data = data;
        }

        public bool NoInfo { get; set; }
        public bool Error { get; set; }

        public int GetInt(string name)
        {
            if (_data != null && _data.ContainsKey(name))
            {
                return ObjectAndString.ObjectToInt(_data[name]);
            }
            return 0;
        }
        public string GetString(string name)
        {
            if (_data != null && _data.ContainsKey(name))
            {
                return _data[name].ToString().Trim();
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
        public string A_FIELD { get { return GetString("A_FIELD"); } }
        public string A_FIELD2 { get { return GetString("A_FIELD2"); } }
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
        public string F3 { get { return GetString("F3"); } }
        public string F4 { get { return GetString("F4"); } }
        public string CTRL_F4 { get { return GetString("CTRL_F4"); } }
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
        public V6TableName V6TableName{get { return _v6TableName; }}
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

        public string VTitlenew;
        public string ETitlenew;
        
    }

    
}

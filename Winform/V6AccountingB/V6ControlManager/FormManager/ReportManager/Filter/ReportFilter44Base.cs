using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportD;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    /// <summary>
    /// <para>Filter control động.</para>
    /// <para>Các filter line được định nghĩa.</para>
    /// <para>Dữ liệu gán lên line từ Fparent.</para>
    /// </summary>
    public partial class ReportFilter44Base : FilterBase
    {
        private string _program;
        
        public ReportFilter44Base()
        {
            InitializeComponent();

            if (V6Login.MadvcsCount <= 1)
            {
                //txtMaDvcs.Enabled = false;
            }

            SetHideFields(RTien);
            Ready();
        }
        
        public ReportFilter44Base(string program)
        {
            InitializeComponent();
            _program = program;
            if (V6Login.MadvcsCount <= 1)
            {
                //txtMaDvcs.Enabled = false;
            }
            SetHideFields(RTien);
            LoadProgramInfo();// ... F3,F5,...
            SetParentRowEvent += ReportFilter44Base_SetParentRowEvent;
            Ready();
        }

        void ReportFilter44Base_SetParentRowEvent(IDictionary<string, object> row)
        {
            try
            {
                foreach (Control control in groupBox1.Controls)
                {
                    var line = control as FilterLineDynamic;
                    if (line == null || line.DefineInfo.Fparent == null) continue;

                    var PARENT_FIELD = line.DefineInfo.Fparent.ToUpper();
                    if (row.ContainsKey(PARENT_FIELD))
                    {
                        line.SetValue(ObjectAndString.ObjectToString(row[PARENT_FIELD]));
                        if (line.DefineInfo.Enabled == false)
                        {
                            line.IsSelected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ReportFilter44Base_SetParentRowEvent", ex);
            }
        }

        private void LoadProgramInfo()
        {
            try
            {
                SqlParameter[] plist = { new SqlParameter("@ma_bc", _program), };
                var data = V6BusinessHelper.Select("ALREPORT", "*", "ma_bc=@ma_bc", "", "", plist).Data;
                if (data.Rows.Count == 1)
                {
                    var row = data.Rows[0];
                    ComboboxData = row["Combo_data"].ToString().Trim();
                    ExtraParameterInfo = row["Extra_para"].ToString().Trim();
                    F3 = ObjectAndString.ObjectToBool(row["F3"]);
                    F5 = ObjectAndString.ObjectToBool(row["F5"]);
                    F7 = ObjectAndString.ObjectToBool(row["F7"]);
                    ViewSum = ObjectAndString.ObjectToBool(row["ViewSum"]);
                    Alreport_advance = row["Advance"].ToString().Trim();
                    _status2Text = row[V6Setting.IsVietnamese ? "vbrowse1" : "ebrowse1"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadProgramInfo", ex);
            }
        }

        private string _status2Text = "";
        public override void SetStatus2Text()
        {
            if (string.IsNullOrEmpty(_status2Text))
            {
                base.SetStatus2Text();
            }
            else
            {
                V6ControlFormHelper.SetStatusText2(_status2Text);
            }
        }

        public void SetHideFields(string Loaitien)
        {
            if (Loaitien == "VN")
            {
                GridViewHideFields = new SortedDictionary<string, string>
                {
                    {"TAG", "TAG"},
                };
            }
            else 
            {
                GridViewHideFields = new SortedDictionary<string, string>
                {
                    {"TAG", "TAG"},
                };
            }
        }

        

        //private string MAU = "", LAN = "";
        private string ComboboxData = "";
        private string ExtraParameterInfo = "";

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {
            if (lineNgay_ct1 != null)
            V6Setting.M_ngay_ct1 = ObjectAndString.ObjectToFullDateTime(lineNgay_ct1.ObjectValue);
            if (lineNgay_ct2 != null)
            V6Setting.M_ngay_ct2 = ObjectAndString.ObjectToFullDateTime(lineNgay_ct2.ObjectValue);

            var parent00 = Parent.Parent.Parent;
            if (parent00 is ReportD99ViewBase)
            {
                var parent99 = (ReportD99ViewBase) parent00;
                MAU = parent99.MAU;
                LAN = parent99.LAN;
            }
            if (parent00 is ReportR44ViewBase)
            {
                var parent44 = (ReportR44ViewBase)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
            }
            if (parent00 is ReportR45ViewBase)
            {
                var parent44 = (ReportR45ViewBase)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
            }
            if (parent00 is ReportRWWView2Base)
            {
                var parent44 = (ReportRWWView2Base)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
            }

            if (lineMauBC != null)
                lineMauBC.SetValue(MAU == "VN" ? "0" : "1");
            if (lineLAN != null)
                lineLAN.SetValue(LAN);
            if (lineUserID != null)
                lineUserID.SetValue(V6Login.UserId);

            var and = radAnd.Checked;
            var and_or = and ? " and" : " or ";
            var result = new List<SqlParameter>();
            result.AddRange(InitFilters);
            //var resultDic = new Dictionary<string,object>();

            // Duyệt qua danh sách parameter name, tạo giá trị cho từng cái một.
            foreach (KeyValuePair<string, object> item in ParameterNameList)
            {
                string parameterName = ("" + item.Value).Trim();
                if (parameterName == "") continue;
                string string_value = "";
                object object_value = "";
                List<string> in_fieldList_Alkh = new List<string>();
                List<string> in_fieldList_Alvt = new List<string>();
                List<string> in_fieldList_Altk = new List<string>();
                List<string> in_fieldList_Alvv = new List<string>();
                List<string> in_fieldList_Alphi = new List<string>();
                List<string> in_fieldList_Alhd = new List<string>();
                List<string> in_fieldList_Alku = new List<string>();
                List<string> in_fieldList_Alsp = new List<string>();
                
                List<string> in_fieldList_Allo = new List<string>();
                List<string> in_fieldList_Albp = new List<string>();
                List<string> in_fieldList_Alnvien = new List<string>();
                List<string> in_fieldList_Albpht = new List<string>();
                List<string> in_fieldList_Althau = new List<string>();
                List<string> in_fieldList_Alsonb = new List<string>();
                List<string> in_fieldList_Aldvcs = new List<string>();
                List<string> in_fieldList_Alkho = new List<string>();

                string last_key = "";
                foreach (Control control in groupBox1.Controls)
                {
                    if (control is FilterGroup)
                    {
                        var group = control as FilterGroup;
                        if (group.DefineInfo.Key1.ToUpper() != parameterName.ToUpper()) continue;

                        last_key = group.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "A1"://1value
                                object_value = group.StringValue;
                                break;
                            case "11"://1value
                                {
                                    object_value = group.ObjectValue;
                                }
                                break;
                        }
                    }
                    else if (control is FilterLineDynamic)
                    {
                        var line = control as FilterLineDynamic;
                        if (line.DefineInfo.Key1.ToUpper() != parameterName.ToUpper()) continue;

                        if (line.CheckNotEmpty && line.StringValue == "")
                        {
                            throw new Exception(line.FieldName);
                        }
                        last_key = line.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "10"://ngay
                                if (line.IsSelected)
                                    string_value = line.StringValue;
                                break;
                            case "A1"://1value
                                object_value = line.StringValue;
                                break;
                            case "11"://1value
                                if (line.IsSelected)
                                {
                                    object_value = line.ObjectValue;
                                }
                                else
                                {
                                    object_value = null;
                                }
                                break;
                            case "21"://in alkh
                                if (line.IsSelected)
                                    in_fieldList_Alkh.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "22"://in alvt
                                if (line.IsSelected)
                                    in_fieldList_Alvt.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "23"://in altk
                                if (line.IsSelected)
                                    in_fieldList_Altk.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "24"://in alvv
                                if (line.IsSelected)
                                    in_fieldList_Alvv.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "25"://in alphi
                                if (line.IsSelected)
                                    in_fieldList_Alphi.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "26"://in alhd
                                if (line.IsSelected)
                                    in_fieldList_Alhd.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "27"://in alku
                                if (line.IsSelected)
                                    in_fieldList_Alku.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "28"://in alsp
                                if (line.IsSelected)
                                    in_fieldList_Alsp.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;

                            case "29": //in allo
                                if (line.IsSelected)
                                    in_fieldList_Allo.Add(line.FieldName);
                                break;
                            case "2A": //in albp
                                if (line.IsSelected)
                                    in_fieldList_Albp.Add(line.FieldName);
                                break;
                            case "2B": //in alnvien
                                if (line.IsSelected)
                                    in_fieldList_Alnvien.Add(line.FieldName);
                                break;
                            case "2C": //in albpht
                                if (line.IsSelected)
                                    in_fieldList_Albpht.Add(line.FieldName);
                                break;
                            case "2D": //in althau
                                if (line.IsSelected)
                                    in_fieldList_Althau.Add(line.FieldName);
                                break;
                            case "2E": //in alsonb
                                if (line.IsSelected)
                                    in_fieldList_Alsonb.Add(line.FieldName);
                                break;
                            case "2F": //in aldvcs
                                if (line.IsSelected)
                                    in_fieldList_Aldvcs.Add(line.FieldName);
                                break;
                            case "2G": //in alkho
                                if (line.IsSelected)
                                    in_fieldList_Alkho.Add(line.FieldName);
                                break;

                            case "30"://advance, cộng dồn
                                if (line.IsSelected)
                                    string_value = string.Format("{0}{1} {2}", string_value, and_or, line.Query);
                                break;
                        }// end switch
                    }
                }// end for lines

                switch (last_key)
                {
                    case "10": //ngay
                        result.Add(new SqlParameter(parameterName, string_value));
                        break;
                    case "A1": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "11": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "21": //in alkh
                    case "22": //in alvt
                    case "23": //in altk
                    case "24": //in alvv
                    case "25": //in alphi
                    case "26": //in alhd
                    case "27": //in alku
                    case "28": //in alsp(alvt)
                    
                    case "29": //in allo
                    case "2A": //in albp
                    case "2B": //in alnvien
                    case "2C": //in albpht
                    case "2D": //in althau
                    case "2E": //in alsonb
                    case "2F": //in aldvcs
                    case "2G": //in alkho
                        
                    case "30": //advance
                        var advance_key_string = GenAdvanceKeyString(and, string_value, in_fieldList_Alkh, in_fieldList_Alvt, in_fieldList_Altk, in_fieldList_Alvv,
                            in_fieldList_Alphi, in_fieldList_Alhd, in_fieldList_Alku, in_fieldList_Alsp,
                            in_fieldList_Allo, in_fieldList_Albp, in_fieldList_Alnvien, in_fieldList_Albpht,
                            in_fieldList_Althau, in_fieldList_Alsonb, in_fieldList_Aldvcs, in_fieldList_Alkho
                            );
                        result.Add(new SqlParameter(item.Key, advance_key_string));
                        break;
                }
            }//End for ParameterNameList

            foreach (KeyValuePair<string, object> item in ParameterNameList2)
            {
                string parameterName = ("" + item.Value).Trim();
                if (parameterName == "") continue;
                string string_value = "";
                object object_value = "";
                List<string> in_fieldList_Alkh = new List<string>();
                List<string> in_fieldList_Alvt = new List<string>();
                List<string> in_fieldList_Altk = new List<string>();
                List<string> in_fieldList_Alvv = new List<string>();
                List<string> in_fieldList_Alphi = new List<string>();
                List<string> in_fieldList_Alhd = new List<string>();
                List<string> in_fieldList_Alku = new List<string>();
                List<string> in_fieldList_Alsp = new List<string>();

                List<string> in_fieldList_Allo = new List<string>();
                List<string> in_fieldList_Albp = new List<string>();
                List<string> in_fieldList_Alnvien = new List<string>();
                List<string> in_fieldList_Albpht = new List<string>();
                List<string> in_fieldList_Althau = new List<string>();
                List<string> in_fieldList_Alsonb = new List<string>();
                List<string> in_fieldList_Aldvcs = new List<string>();
                List<string> in_fieldList_Alkho = new List<string>();
                
                string last_key = "";
                foreach (Control control in groupBox1.Controls)
                {
                    if (control is FilterGroup)
                    {
                        var group = control as FilterGroup;
                        if (group.DefineInfo.Key1.ToUpper() != parameterName.ToUpper()) continue;

                        last_key = group.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "A1"://1value
                                object_value = group.StringValue;
                                break;
                            case "11"://1value
                                {
                                    object_value = group.ObjectValue;
                                }
                                break;
                        }
                    }
                    else if (control is FilterLineDynamic)
                    {
                        var line = control as FilterLineDynamic;
                        if (line == null) continue;
                        if (line.DefineInfo.Key2.ToUpper() != parameterName.ToUpper()) continue;

                        if (line.CheckNotEmpty && line.StringValue == "")
                        {
                            throw new Exception(line.FieldName);
                        }
                        last_key = line.DefineInfo.Loai_key;

                        switch (last_key)
                        {
                            case "10"://ngay
                                if (line.IsSelected)
                                    string_value = line.StringValue;
                                break;
                            case "A1"://1value
                                object_value = line.StringValue;
                                break;
                            case "11"://1value
                                if (line.IsSelected)
                                {
                                    object_value = line.ObjectValue;
                                }
                                else
                                {
                                    object_value = null;
                                }
                                break;
                            case "21"://in alkh
                                if (line.IsSelected)
                                    in_fieldList_Alkh.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "22"://in alvt
                                if (line.IsSelected)
                                    in_fieldList_Alvt.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "23"://in altk
                                if (line.IsSelected)
                                    in_fieldList_Altk.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "24"://in alvv
                                if (line.IsSelected)
                                    in_fieldList_Alvv.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "25"://in alphi
                                if (line.IsSelected)
                                    in_fieldList_Alphi.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "26"://in alhd
                                if (line.IsSelected)
                                    in_fieldList_Alhd.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "27"://in alku
                                if (line.IsSelected)
                                    in_fieldList_Alku.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;
                            case "28"://in alsp
                                if (line.IsSelected)
                                    in_fieldList_Alsp.Add(line.FieldName);
                                //string_value = line.StringValue;
                                break;

                            case "29": //in allo
                                if (line.IsSelected)
                                    in_fieldList_Allo.Add(line.FieldName);
                                break;
                            case "2A": //in albp
                                if (line.IsSelected)
                                    in_fieldList_Albp.Add(line.FieldName);
                                break;
                            case "2B": //in alnvien
                                if (line.IsSelected)
                                    in_fieldList_Alnvien.Add(line.FieldName);
                                break;
                            case "2C": //in albpht
                                if (line.IsSelected)
                                    in_fieldList_Albpht.Add(line.FieldName);
                                break;
                            case "2D": //in althau
                                if (line.IsSelected)
                                    in_fieldList_Althau.Add(line.FieldName);
                                break;
                            case "2E": //in alsonb
                                if (line.IsSelected)
                                    in_fieldList_Alsonb.Add(line.FieldName);
                                break;
                            case "2F": //in aldvcs
                                if (line.IsSelected)
                                    in_fieldList_Aldvcs.Add(line.FieldName);
                                break;
                            case "2G": //in alkho
                                if (line.IsSelected)
                                    in_fieldList_Alkho.Add(line.FieldName);
                                break;

                            case "30"://advance, cộng dồn
                                if (line.IsSelected)
                                    string_value = string.Format("{0}{1} {2}", string_value, and_or, line.Query);
                                break;
                        }// end switch
                    }
                }// end for lines

                switch (last_key)
                {
                    case "10": //ngay
                        result.Add(new SqlParameter(parameterName, string_value));
                        break;
                    case "A1": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "11": //1value
                        result.Add(new SqlParameter(parameterName, object_value));
                        break;
                    case "21": //in alkh
                    //param_value = line.StringValue;
                    case "22": //in alvt
                    //param_value = line.StringValue;
                    case "23": //in altk
                    //param_value = line.StringValue;
                    case "24": //in alvv
                    //param_value = line.StringValue;
                    case "25": //in alphi
                    case "26": //in alhd
                    case "27": //in alku
                    case "28": //in alsp(alvt)

                    case "29": //in allo
                    case "2A": //in albp
                    case "2B": //in alnvien
                    case "2C": //in albpht
                    case "2D": //in althau
                    case "2E": //in alsonb
                    case "2F": //in aldvcs
                    case "2G": //in alkho

                    case "30": //advance
                        var advance_key_string = GenAdvanceKeyString(and, string_value, in_fieldList_Alkh, in_fieldList_Alvt, in_fieldList_Altk, in_fieldList_Alvv,
                            in_fieldList_Alphi, in_fieldList_Alhd, in_fieldList_Alku, in_fieldList_Alsp,
                            in_fieldList_Allo, in_fieldList_Albp, in_fieldList_Alnvien, in_fieldList_Albpht,
                            in_fieldList_Althau, in_fieldList_Alsonb, in_fieldList_Aldvcs, in_fieldList_Alkho
                            );
                        result.Add(new SqlParameter(item.Key, advance_key_string));
                        break;
                }
            }//End for ParameterNameList2

            return result;
        }

        

        

        
        public override DataTable GenTableForReportType()
        {
            //var ComboboxData = "Name:Số lượng;Value:Ten_vt,So_luong1,So_luong2~Name:Tiền;Value:Ten_vt,Tien1,Tien2";
            //string reportSoluongColumns = "Ten_vt,So_luong1,So_luong2", reportTienColumns = "Ten_vt,Tien1,Tien2";

            if (string.IsNullOrEmpty(ComboboxData)) return null;
            var sss = ComboboxData.Split('~');
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Value", typeof(string));

            foreach (string ss in sss)
            {
                DefineInfo di = new DefineInfo(ss);
                if(string.IsNullOrEmpty(di.Name) || string.IsNullOrEmpty(di.Value)) continue;

                DataRow newRow = tbl.NewRow();
                newRow["Name"] = di.Name;
                newRow["Value"] = di.Value;
                tbl.Rows.Add(newRow);
            }

            return tbl;
        }

        public override void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
            AddExtraReportParameter();
        }

        /// <summary>
        /// Tạo các tham số mở rộng cho sql.
        /// </summary>
        private void AddExtraReportParameter()
        {
            RptExtraParameters = new SortedDictionary<string, object>();
            try
            {
                string errors = "";
                //Sample Info:
                //Name:M_TEN_CTY;NOTEMPTY:1;Ptype:FILTER_BROTHER;Field:MA_DVCS;Fname:TEN_DVCS   // Lấy TEN_DVCS theo MA_DVCS trong FilterControl.
                //~Name:NGAY;Ptype:TABLE2;Field:R_DMY                                           // Lấy R_DMY trong table2 data.
                //~Name:SOTIENVIETBANGCHU_TIENBAN;Ptype:TABLE2;FIELD:CON_PT                     // Đọc số tiền trong table2 chỉ với Name = SOTIENVIETBANGCHU_TIENBAN
                //~Name:SOTIENVIETBANGCHU_TIENBANNT;Ptype:TABLE2;FIELD:DU_CK                    // Đọc số tiền trong table2 chỉ với Name = SOTIENVIETBANGCHU_TIENBANNT

                if (string.IsNullOrEmpty(ExtraParameterInfo)) return;
                var sss = ExtraParameterInfo.Split('~');
                
                foreach (string ss in sss)
                {
                    DefineInfo di = new DefineInfo(ss);
                    if (string.IsNullOrEmpty(di.Name)) continue;

                    if (di.Ptype.ToUpper() == "TABLE2")
                    {
                        var dataTable2 = _ds.Tables[1];
                        var _tbl2Row = dataTable2.Rows[0];
                        // Tuanmh 18/08/2017 Upper -> Match Parameter
                        if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBANNT")
                        {
                            var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);// "T_TIEN_NT2_IN"]);
                            var ma_nt = V6Options.M_MA_NT0;
                            if (dataTable2.Columns.Contains("MA_NT"))
                            {
                                ma_nt = (_tbl2Row["MA_NT"] ?? ma_nt).ToString().Trim();
                            }
                            RptExtraParameters[di.Name] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, LAN, ma_nt);
                        }
                        else if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBAN")
                        {
                            var t_tien2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);//"T_TIEN2_IN"]);
                            RptExtraParameters[di.Name] = V6BusinessHelper.MoneyToWords(t_tien2_in, LAN,
                                V6Options.M_MA_NT0);
                        }
                        else
                        {

                            RptExtraParameters[di.Name.ToUpper()] = _tbl2Row[di.Field];
                        }
                    }
                    else if (di.Ptype.ToUpper() == "PARENT")
                    {
                        
                    }
                    else if (di.Ptype.ToUpper() == "ONEVALUE")
                    {
                        RptExtraParameters[di.Name.ToUpper()] = di.Fname;
                    }
                    else if (di.Ptype.ToUpper() == "FILTER")
                    {
                        foreach (Control control in groupBox1.Controls)
                        {
                            var line = control as FilterLineDynamic;
                            if (line != null && line.FieldName.ToUpper() == di.Field.ToUpper())
                            {
                                if (line.IsSelected)
                                {
                                    //Bỏ qua giá trị rỗng.
                                    if (di.NotEmpty && string.IsNullOrEmpty("" + line.ObjectValue)) continue;
                                    RptExtraParameters[di.Name] = line.ObjectValue;
                                }
                                else
                                {
                                    if (di.NotEmpty) continue;

                                    if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                        RptExtraParameters[di.Name] = 0;
                                    else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                        RptExtraParameters[di.Name] = new DateTime(1900, 1, 1);
                                    else
                                        RptExtraParameters[di.Name] = "";
                                }
                                break;
                            }
                        }
                    }
                    else if (di.Ptype.ToUpper() == "FILTER_BROTHER")
                    {
                        bool not_found = true;
                        foreach (Control control in groupBox1.Controls)
                        {
                            var line = control as FilterLineDynamic;
                            if (line != null && line.FieldName.ToUpper() == di.Field.ToUpper())
                            {
                                not_found = false;
                                var vvar_data = line._vtextBox.Data;
                                if (line.IsSelected==false)
                                {
                                    vvar_data = null;
                                }

                                if (vvar_data != null && vvar_data.Table.Columns.Contains(di.Fname))
                                {
                                    if (line.IsSelected)
                                    {
                                        //Bỏ qua giá trị rỗng.
                                        if (di.NotEmpty && string.IsNullOrEmpty("" + line.ObjectValue)) continue;
                                        RptExtraParameters[di.Name] = vvar_data[di.Fname];
                                    }
                                    else
                                    {
                                        if (di.NotEmpty) continue;

                                        if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                            RptExtraParameters[di.Name] = 0;
                                        else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                            RptExtraParameters[di.Name] = new DateTime(1900, 1, 1);
                                        else
                                            RptExtraParameters[di.Name] = "";
                                    }
                                }
                                else
                                {
                                    if (di.NotEmpty) continue;
                                    // Tuanmh Null loi
                                    if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                        RptExtraParameters[di.Name] = 0;
                                    else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                        RptExtraParameters[di.Name] = new DateTime(1900, 1, 1);
                                    else
                                        RptExtraParameters[di.Name] = "";
                                }
                                break;
                            }
                        }
                        if (not_found) errors += "\nKhông tìm thấy Field: " + di.Field;
                    }
                    else
                    {
                        errors += "Ptype not support: " + di.Ptype;
                    }
                }

                if (errors.Length > 0)
                {
                    this.ShowErrorMessage(GetType() + ".AddExtraReportParameter: " + errors);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddExtraReportParameter", ex);
            }
        }

        public override void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            string showFields = "";
            string formatStrings = "";
            string headerString = "";
            if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
            {
                var data = _ds.Tables[1];
                if (data.Columns.Contains("GRDS_V1")) showFields = data.Rows[0]["GRDS_V1"].ToString();
                if (data.Columns.Contains("GRDF_V1")) formatStrings = data.Rows[0]["GRDF_V1"].ToString();
                var f = V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1";
                if (data.Columns.Contains(f)) headerString = data.Rows[0][f].ToString();
            }
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
        }

    }
}

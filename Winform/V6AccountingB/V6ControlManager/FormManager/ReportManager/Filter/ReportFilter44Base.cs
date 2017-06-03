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
            Ready();
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
                    F3 = row["F3"].ToString() == "1";
                    F5 = row["F5"].ToString() == "1";
                    F7 = row["F7"].ToString() == "1";
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadProgramInfo", ex);
            }
        }

        //public override void SetStatus2Text()
        //{
        //    V6ControlFormHelper.SetStatusText2("F3:...");
        //}

        public void SetHideFields(string Loaitien)
        {
            if (Loaitien == "VN")
            {
                _hideFields = new SortedDictionary<string, string>
                {
                    {"TAG", "TAG"},
                };
            }
            else 
            {
                _hideFields = new SortedDictionary<string, string>
                {
                    {"TAG", "TAG"},
                };
            }
        }

        public Dictionary<string, object> ParameterNameList = new Dictionary<string, object>(); 
        public Dictionary<string, object> ParameterNameList2 = new Dictionary<string, object>();
        public FilterLineDynamic lineNgay_ct1 = null;
        public FilterLineDynamic lineNgay_ct2 = null;
        public FilterLineDynamic lineMauBC = null;
        public FilterLineDynamic lineLAN = null;

        private string MAU = "", LAN = "";
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
                if (lineMauBC != null)
                    lineMauBC.SetValue(MAU == "VN" ? "0" : "1");
                if (lineLAN != null)
                    lineLAN.SetValue(LAN);
            }
            if (parent00 is ReportR44ViewBase)
            {
                var parent44 = (ReportR44ViewBase)parent00;
                MAU = parent44.MAU;
                LAN = parent44.LAN;
                if (lineMauBC != null)
                    lineMauBC.SetValue(MAU == "VN" ? "0" : "1");
                if (lineLAN != null)
                    lineLAN.SetValue(LAN);
            }
            

            var and = radAnd.Checked;
            var and_or = and ? " and" : " or ";
            var result = new List<SqlParameter>();
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
                string string_in_Alkh = "";
                string string_in_Alvt = "";
                string string_in_Altk = "";
                string string_in_Alvv = "";
                string last_key = "";
                foreach (Control control in groupBox1.Controls)
                {
                    var line = control as FilterLineDynamic;
                    if (line == null) continue;
                    if (line.DefineInfo.Key1 != parameterName) continue;

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

                        case "30"://advance, cộng dồn
                            if(line.IsSelected)
                            string_value = string.Format("{0}{1} {2}", string_value, and_or, line.Query);
                            break;
                    }// end switch
                }// end for lines

                switch (last_key)
                {
                    case "10": //ngay
                        result.Add(new SqlParameter(parameterName, string_value));
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
                    case "30": //advance
                        if (in_fieldList_Alkh.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Alkh, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Alkh = string.Format("{0} MA_KH in (select ma_kh from alkh where {1} )",
                                    and_or, key0);
                            }
                        }
                        if (in_fieldList_Alvt.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Alvt, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Alvt = string.Format("{0} MA_VT in (select ma_vt from alvt where {1} )",
                                    and_or, key0);
                            }
                        }
                        if (in_fieldList_Altk.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Altk, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Altk = string.Format("{0} TK in (select tk from altk where {1} )",
                                    and_or, key0);
                            }
                        }
                        if (in_fieldList_Alvv.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Alvv, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Alvv = string.Format("{0} MA_VV in (select ma_vv from alvv where {1} )",
                                    and_or, key0);
                            }
                        }

                        var last_string_value = string_value + string_in_Alkh + string_in_Alvt + string_in_Altk + string_in_Alvv;
                        if (last_string_value.Length > 4) last_string_value = last_string_value.Substring(4);
                        result.Add(new SqlParameter(item.Key, last_string_value));
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
                string string_in_Alkh = "";
                string string_in_Alvt = "";
                string string_in_Altk = "";
                string string_in_Alvv = "";
                string last_key = "";
                foreach (Control control in groupBox1.Controls)
                {
                    var line = control as FilterLineDynamic;
                    if (line == null) continue;
                    if (line.DefineInfo.Key2 != parameterName) continue;

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

                        case "30"://advance, cộng dồn
                            if (line.IsSelected)
                                string_value = string.Format("{0}{1} {2}", string_value, and_or, line.Query);
                            break;
                    }// end switch
                }// end for lines

                switch (last_key)
                {
                    case "10": //ngay
                        result.Add(new SqlParameter(parameterName, string_value));
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
                    case "30": //advance
                        if (in_fieldList_Alkh.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Alkh, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Alkh = string.Format("{0} MA_KH in (select ma_kh from alkh where {1} )",
                                    and_or, key0);
                            }
                        }
                        if (in_fieldList_Alvt.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Alvt, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Alvt = string.Format("{0} MA_VT in (select ma_vt from alvt where {1} )",
                                    and_or, key0);
                            }
                        }
                        if (in_fieldList_Altk.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Altk, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Altk = string.Format("{0} TK in (select tk from altk where {1} )",
                                    and_or, key0);
                            }
                        }
                        if (in_fieldList_Alvv.Count > 0)
                        {
                            var key0 = GetFilterStringByFields(in_fieldList_Alvv, and);
                            if (!string.IsNullOrEmpty(key0))
                            {
                                string_in_Alvv = string.Format("{0} MA_VV in (select ma_vv from alvv where {1} )",
                                    and_or, key0);
                            }
                        }

                        var last_string_value = string_value + string_in_Alkh + string_in_Alvt + string_in_Altk + string_in_Alvv;
                        if (last_string_value.Length > 4) last_string_value = last_string_value.Substring(4);
                        result.Add(new SqlParameter(item.Key, last_string_value));
                        break;
                }
            }//End for ParameterNameList2

            return result;
        }


        public void AddLineControls(FilterLineDynamic lineControl)
        {
            lineControl.Width = groupBox1.Width - 10;
            groupBox1.Controls.Add(lineControl);

            if (lineControl.DefineInfo.Visible && groupBox1.Height - 10 < lineControl.Bottom)
            {
                Height = groupBox1.Top + lineControl.Bottom + 20;
            }
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

        private void AddExtraReportParameter()
        {
            RptExtraParameters = new SortedDictionary<string, object>();
            try
            {
                

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
                        if (di.Name == "SoTienVietBangChu_TienBanNt")
                        {
                            var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);// "T_TIEN_NT2_IN"]);
                            var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
                            RptExtraParameters[di.Name] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, LAN, ma_nt);
                        }
                        else if (di.Name == "SoTienVietBangChu_TienBan")
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
                    else if (di.Ptype.ToUpper() == "FILTER")
                    {
                        foreach (Control control in groupBox1.Controls)
                        {
                            var line = control as FilterLineDynamic;
                            if (line != null && line.FieldName.ToUpper() == di.Field.ToUpper())
                            {
                                RptExtraParameters[di.Name] = line.ObjectValue;
                                break;
                            }
                        }
                    }
                    else if (di.Ptype.ToUpper() == "FILTER_BROTHER")
                    {
                        foreach (Control control in groupBox1.Controls)
                        {
                            var line = control as FilterLineDynamic;
                            if (line != null && line.FieldName.ToUpper() == di.Field.ToUpper())
                            {
                                var vvar_data = line._vtextBox.Data;
                                if (vvar_data != null && vvar_data.Table.Columns.Contains(di.Fname))
                                {
                                    RptExtraParameters[di.Name] = vvar_data[di.Fname];
                                }
                                else
                                {
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
                    }
                    else
                    {
                        
                    }
                }

                
                //var parent = FindParent<>() as ;
                //if (_ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)//&& parent != null)
                //{
                //    //var dataTable2 = _ds.Tables[1];
                //    //var _tbl2Row = dataTable2.Rows[0];
                //    if (dataTable2.Columns.Contains("T_TIEN_NT2_IN") && dataTable2.Columns.Contains("MA_NT"))
                //    {
                //        var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row["T_TIEN_NT2_IN"]);
                //        var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
                //        RptExtraParameters["SoTienVietBangChu_TienBanNt"] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, LAN, ma_nt);
                //    }

                //    if (dataTable2.Columns.Contains("T_TIEN2_IN"))
                //    {
                //        var t_tien2_in = ObjectAndString.ObjectToDecimal(_tbl2Row["T_TIEN2_IN"]);
                //        RptExtraParameters["SoTienVietBangChu_TienBan"] = V6BusinessHelper.MoneyToWords(t_tien2_in, LAN,
                //            V6Options.M_MA_NT0);
                //    }

                //}


                //foreach (var VARIABLE in "".Split(','))
                //{
                //    if (VARIABLE == "M_ABC")
                //    {
                //        RptExtraParameters[VARIABLE] = "ABC";
                //    }
                //}
            }
            catch (Exception)
            {
                
            }
        }

        private DataSet _ds;
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

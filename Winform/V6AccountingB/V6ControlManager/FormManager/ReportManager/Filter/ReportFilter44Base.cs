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
        private string Alreport_advance = "";
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
                    if (line == null) continue;

                    var PARENT_FIELD = line.DefineInfo.Fparent.ToUpper();
                    if (row.ContainsKey(PARENT_FIELD))
                    {
                        line.SetValue(ObjectAndString.ObjectToString(row[PARENT_FIELD]));
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
                    F3 = row["F3"].ToString() == "1";
                    F5 = row["F5"].ToString() == "1";
                    F7 = row["F7"].ToString() == "1";
                    Alreport_advance = row["Advance"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadProgramInfo", ex);
            }
        }

        //public override void SetStatus2Text()
        //{
        //      if()base.else
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

                string last_key = "";
                foreach (Control control in groupBox1.Controls)
                {
                    var line = control as FilterLineDynamic;
                    if (line == null) continue;
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
                        
                    case "30": //advance
                        var advance_key_string = GenAdvanceKeyString(and, string_value, in_fieldList_Alkh, in_fieldList_Alvt, in_fieldList_Altk, in_fieldList_Alvv,
                            in_fieldList_Alphi, in_fieldList_Alhd, in_fieldList_Alku, in_fieldList_Alsp);
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
                
                string last_key = "";
                foreach (Control control in groupBox1.Controls)
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

                    case "30": //advance
                        var advance_key_string = GenAdvanceKeyString(and, string_value, in_fieldList_Alkh, in_fieldList_Alvt, in_fieldList_Altk, in_fieldList_Alvv,
                            in_fieldList_Alphi, in_fieldList_Alhd, in_fieldList_Alku, in_fieldList_Alsp);
                        result.Add(new SqlParameter(item.Key, advance_key_string));
                        break;
                }
            }//End for ParameterNameList2

            return result;
        }

        /// <summary>
        /// Tạo key advance
        /// </summary>
        /// <param name="and"></param>
        /// <param name="string_value">Key lẻ của loại key 30</param>
        /// <param name="in_fieldList_Alkh"></param>
        /// <param name="in_fieldList_Alvt"></param>
        /// <param name="in_fieldList_Altk"></param>
        /// <param name="in_fieldList_Alvv"></param>
        /// <param name="in_fieldList_Alphi"></param>
        /// <param name="in_fieldList_Alhd"></param>
        /// <param name="in_fieldList_Alku"></param>
        /// <param name="in_fieldList_Alsp"></param>
        /// <returns></returns>
        private string GenAdvanceKeyString(bool and, string string_value,
            List<string> in_fieldList_Alkh, List<string> in_fieldList_Alvt, List<string> in_fieldList_Altk, List<string> in_fieldList_Alvv,
            List<string> in_fieldList_Alphi, List<string> in_fieldList_Alhd, List<string> in_fieldList_Alku, List<string> in_fieldList_Alsp)
        {
            var string_in_Alkh = GenkeyStringInTable(in_fieldList_Alkh, and, "MA_KH", "ma_kh", "alkh");
            var string_in_Alvt = GenkeyStringInTable(in_fieldList_Alvt, and, "MA_VT", "ma_vt", "alvt");
            var string_in_Altk = GenkeyStringInTable(in_fieldList_Altk, and, "TK", "tk", "altk");
            var string_in_Alvv = GenkeyStringInTable(in_fieldList_Alvv, and, "MA_VV", "ma_vv", "alvv");

            var string_in_Alphi = GenkeyStringInTable(in_fieldList_Alphi, and, "MA_PHI", "ma_phi", "alphi");
            var string_in_Alhd = GenkeyStringInTable(in_fieldList_Alhd, and, "MA_HD", "ma_hd", "alhd");
            var string_in_Alku = GenkeyStringInTable(in_fieldList_Alku, and, "MA_KU", "ma_ku", "alku");
            var string_in_Alsp = GenkeyStringInTable(in_fieldList_Alsp, and, "MA_SP", "ma_vt", "alvt");

            var last_string_value = string_value + string_in_Alkh + string_in_Alvt + string_in_Altk + string_in_Alvv
                + string_in_Alphi + string_in_Alhd + string_in_Alku + string_in_Alsp;
            if (last_string_value.Length > 4) last_string_value = last_string_value.Substring(4);
            return last_string_value;
        }

        /// <summary>
        /// Tạo key advance in cho một bảng.
        /// </summary>
        /// <param name="in_fieldList_Alsp">Danh sách các field where</param>
        /// <param name="and"></param>
        /// <param name="FIELD_in"></param>
        /// <param name="select_field"></param>
        /// <param name="from_table"></param>
        /// <returns></returns>
        private string GenkeyStringInTable(List<string> in_fieldList_Alsp, bool and, string FIELD_in, string select_field, string from_table)
        {
            var result = "";
            
            if (in_fieldList_Alsp!=null && in_fieldList_Alsp.Count > 0)
            {
                var and_or = and ? " and" : " or ";
                var key0 = GetFilterStringByFields(in_fieldList_Alsp, and);
                if (!string.IsNullOrEmpty(key0))
                {
                    result = string.Format("{0} {2} in (select {3} from {4} where {1} )",
                        and_or, key0, FIELD_in, select_field, from_table);
                }
            }

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

            if (lineControl.DefineInfo.Loai_key == "A1" && lineControl._checkBox != null)
            {
                if (lineControl._checkBox.Checked) Advance = Alreport_advance;
                lineControl._checkBox.CheckedChanged += delegate
                {
                    if (lineControl._checkBox.Checked)
                    {
                        Advance = Alreport_advance;
                    }
                    else
                    {
                        Advance = "";
                    }
                };
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
                        // Tuanmh 18/08/2017 Upper -> Match Parameter
                        if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBANNT")
                        {
                            var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);// "T_TIEN_NT2_IN"]);
                            var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
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
                        foreach (Control control in groupBox1.Controls)
                        {
                            var line = control as FilterLineDynamic;
                            if (line != null && line.FieldName.ToUpper() == di.Field.ToUpper())
                            {
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

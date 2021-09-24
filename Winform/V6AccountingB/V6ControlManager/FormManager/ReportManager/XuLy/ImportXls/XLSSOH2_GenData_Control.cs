using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XLSSOH2_GenData_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        private DataTable _tbl0;
        private int _fixColumn;
        private Config _config = null;
        public string M_SOA_MULTI_VAT = "0";
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private string check = null;

        public XLSSOH2_GenData_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            try
            {
                M_SOA_MULTI_VAT = V6Options.GetValue("M_SOA_MULTI_VAT");
            }
            catch (Exception)
            {
                
            }
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("F9: {0}", V6Text.Text("CHUYEN"));
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }


        protected override void LoadData0()
        {
            try
            {
                FilterControl.UpdateValues();
                if (string.IsNullOrEmpty(FilterControl.String1))
                {
                    return;
                }
                if (!File.Exists(FilterControl.String1))
                {
                    this.ShowMessage(V6Text.NotExist);
                    return;
                }

                _tbl0 = Excel_File.Sheet1ToDataTable(FilterControl.String1, 0, V6Options.M_MAXROWS_EXCEL);
                check = null;

                //Check1: chuyen ma, String12 A to U
                if (FilterControl.Check1)
                {
                    if (!string.IsNullOrEmpty(FilterControl.String2) && !string.IsNullOrEmpty(FilterControl.String3))
                    {
                        var from = "A";
                        if (FilterControl.String2.StartsWith("TCVN3")) from = "A";
                        if (FilterControl.String2.StartsWith("VNI")) from = "V";
                        var to = "U";
                        if (FilterControl.String3.StartsWith("TCVN3")) to = "A";
                        if (FilterControl.String3.StartsWith("VNI")) to = "V";
                        _tbl0 = Data_Table.ChuyenMaTiengViet(_tbl0, from, to);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(V6Text.Text("NoFromTo"));
                    }
                }

                // CHECK DATA
                // Thêm cột CHECK
                if (!_tbl0.Columns.Contains("CHECK"))
                {
                    _tbl0.Columns.Add("CHECK", typeof(string));
                }
                // Check MA_VT
                foreach (DataRow row in _tbl0.Rows)
                {
                    string value = row["MA_VT"].ToString().Trim();
                    var notexist = V6BusinessHelper.IsValidOneCode_Full("ALVT", 1, "MA_VT", value, value);
                    row["CHECK"] = notexist ? "0" : "1";
                }

                // FIX DATA

                // COLUMNS TO ROWS

                _tbl = null;
                var alim2xls = V6BusinessHelper.Select("ALIM2XLS", "top 1 *", "MA_CT='SOH'").Data;
                if (alim2xls != null && alim2xls.Rows.Count > 0)
                {
                    _config = new Config(alim2xls.Rows[0].ToDataDictionary());
                    _fixColumn = _config.GetInt("VBROWSE1");
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoDefine + "ALIM2XLS:SOH");
                    return;
                }
                
                _tbl = new DataTable("DataG");
                // Tao cot fix
                for (int i = 0; i < _fixColumn; i++)
                {
                    var column_i = _tbl0.Columns[i];
                    var FIELD = column_i.ColumnName.ToUpper();
                    _tbl.Columns.Add(FIELD, column_i.DataType);
                }
                // Them cot MA_KH
                if (!_tbl.Columns.Contains("MA_KH"))
                {
                    _tbl.Columns.Add("MA_KH", typeof(string));
                }
                // Them cot CHECK
                if (!_tbl.Columns.Contains("CHECK"))
                {
                    _tbl.Columns.Add("CHECK", typeof(string));
                }
                // Them cot SO_LUONG1
                if (!_tbl.Columns.Contains("SO_LUONG1"))
                {
                    _tbl.Columns.Add("SO_LUONG1", typeof (decimal));
                }
                // Them du lieu
                for (int i = _fixColumn; i < _tbl0.Columns.Count - 1; i++) // -1 bỏ qua cột CHECK
                {
                    var column_i = _tbl0.Columns[i];
                    string MA_KH = column_i.ColumnName.Trim().ToUpper();
                    var value = MA_KH;
                    var notexist = V6BusinessHelper.IsValidOneCode_Full("ALKH", 1, "MA_KH", value, value);
                    string CHECK_KH = notexist ? "0" : "1";
                    // Đã check vật tư // check thêm khách hàng

                    for (int j = 0; j < _tbl0.Rows.Count; j++)
                    {
                        var row_j = _tbl0.Rows[j];
                        var ma_vt = row_j["MA_VT"].ToString().Trim();
                        if (ma_vt == "") continue;
                        var so_luong1 = ObjectAndString.ObjectToDecimal(row_j[MA_KH]);
                        if(so_luong1 == 0) continue;

                        var newRow = _tbl.NewRow();
                        for (int c = 0; c < _fixColumn; c++)
                        {
                            var column_c = _tbl0.Columns[c];
                            var FIELD = column_c.ColumnName.ToUpper();
                            newRow[FIELD] = row_j[FIELD];
                        }
                        newRow["MA_KH"] = MA_KH;
                        string CHECK_VT = row_j["CHECK"].ToString();
                        newRow["CHECK"] = (CHECK_KH == "1" && CHECK_VT == "1") ? "1" : "0";
                        newRow["SO_LUONG1"] = so_luong1;
                        //_dataG.AddRow(newRowDic);
                        _tbl.Rows.Add(newRow);
                    }
                }

                // CHECK DATA FIELD
                if (!_tbl.Columns.Contains("TIEN_NT2"))
                {
                    _tbl.Columns.Add("TIEN_NT2", typeof(decimal));
                }
                if (!_tbl.Columns.Contains("THUE_NT"))
                {
                    _tbl.Columns.Add("THUE_NT", typeof(decimal));
                }
                if (!_tbl.Columns.Contains("TIEN_NT"))
                {
                    _tbl.Columns.Add("TIEN_NT", typeof(decimal));
                }
                if (!_tbl.Columns.Contains("TY_GIA"))
                {
                    _tbl.Columns.Add("TY_GIA", typeof(decimal));
                }
                if (!_tbl.Columns.Contains("TIEN2"))
                {
                    _tbl.Columns.Add("TIEN2", typeof (decimal));
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["TIEN2"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["TIEN_NT2"])*
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }
                if (!_tbl.Columns.Contains("THUE"))
                {
                    _tbl.Columns.Add("THUE", typeof(decimal));
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["THUE"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["THUE_NT"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }
                if (!_tbl.Columns.Contains("TIEN"))
                {
                    _tbl.Columns.Add("TIEN", typeof(decimal));
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["TIEN"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["TIEN_NT"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }

                All_Objects["data"] = _tbl;
                InvokeFormEvent(FormDynamicEvent.DYNAMICFIXEXCEL);

                _executesuccess = true;
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
            }
        }

        public string CheckDataInGridView()
        {
            string check = null;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["CHECK"].Value.ToString() == "0")
                    {
                        check += "CHECK=0";
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".CheckDataInGridView", ex);
            }
            return check;
        }

        protected override void LoadDataFinish()
        {
            try
            {
                //string[] data_fields = "MA_KH,MA_VT,MA_KHO_I".Split(',');
                //string[] check_fields = "MA_KH,MA_VT,MA_KHO".Split(',');
                //string[] check_tables = "ALKH,ALVT,ALKHO".Split(',');
                //this.ShowWarningMessage("Thời gian check lâu do tạo nhiều dòng!");
                check += CheckDataInGridView();

                //var alim2xls = V6BusinessHelper.Select("ALIM2XLS", "top 1 *", "MA_CT='SOH'").Data;
                if (_config != null && _config.HaveInfo)
                {
                    var khoa = _config.GetString("KHOA").Split(',');
                    var lost_fields = "";
                    foreach (string field in khoa)
                    {
                        if (!_tbl.Columns.Contains(field))
                        {
                            check += string.Format("{0} {1}", V6Text.NoData, field);
                            lost_fields += ", " + field;
                        }
                    }
                    if (lost_fields.Length > 2)
                    {
                        lost_fields = lost_fields.Substring(2);
                        this.ShowWarningMessage(V6Text.Text("DULIEUBITHIEU") + ": " + lost_fields);
                    }
                }
                else
                {
                    check += V6Text.NoDefine + " alim2xls";
                }

                if (!string.IsNullOrEmpty(check))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadDataFinish", ex);
            }
        }

        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (!string.IsNullOrEmpty(check))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check);
                    return;
                }
                if (_tbl != null)
                {
                    if (_tbl.Columns.Contains(ID_FIELD) && _tbl.Columns.Contains(NAME_FIELD))
                    {
                        LockButtons();
                        
                        Timer timerF9 = new Timer {Interval = 1000};
                        timerF9.Tick += tF9_Tick;
                        remove_list_d = new List<DataRow>();
                        Thread t = new Thread(F9Thread);
                        t.SetApartmentState(ApartmentState.STA);
                        CheckForIllegalCrossThreadCalls = false;
                        t.IsBackground = true;
                        t.Start();
                        timerF9.Start();
                        V6ControlFormHelper.SetStatusText("F9 running");
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(string.Format("{0} {1} {2} {3}", V6Text.Text("DULIEUBITHIEU"), ID_FIELD, V6Text.Text("AND"), NAME_FIELD));
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMessage(V6Text.Text("NODATA"));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private bool f9Running;
        private string f9Message = "";
        private string f9MessageAll = "";
        V6Invoice91 Invoice = new V6Invoice91();
        private IDictionary<string, object> AM_DATA;
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9MessageAll = "";
                string makho = "";
                string madvcs = "";

                //Gom chi tiet theo SO_CT va NGAY_CT
                Dictionary<string, List<DataRow>> data_dictionary = new Dictionary<string, List<DataRow>>();
                DateTime? dateMin = null, dateMax = null;
                foreach (DataRow row in _tbl.Rows)
                {
                    var date = ObjectAndString.ObjectToFullDateTime(row["NGAY_CT"]);
                    if (dateMin == null || date < dateMin)
                    {
                        dateMin = date;
                    }
                    if (dateMax == null || date > dateMax)
                    {
                        dateMax = date;
                    }

                    // Tuanmh them 08/03/2019
                    makho = row["MA_KHO_I"].ToString().Trim().ToUpper();
                    madvcs = row["MA_DVCS"].ToString().Trim().ToUpper();

                    string ma_kh = row["MA_KH"].ToString().Trim().ToUpper();
                    string ngay_ct = date.ToString("yyyyMMdd");
                    if (ma_kh != "" && ngay_ct != "")
                    {
                        var key = ma_kh + ":" + ngay_ct;
                        if (data_dictionary.ContainsKey(key))
                        {
                            data_dictionary[key].Add(row);
                        }
                        else
                        {
                            data_dictionary.Add(key, new List<DataRow> {row});
                        }
                    }
                    else
                    {
                        f9Message += "Kiểm tra [MA_KH].";
                    }
                }

                //Check data
                if (FilterControl.Check2)
                {
                    //Delete excel data dateMin dateMax
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Ngay_ct1", dateMin.Value.ToString("yyyyMMdd")),
                        new SqlParameter("@Ngay_ct2", dateMax.Value.ToString("yyyyMMdd")),
                        new SqlParameter("@Ma_ct", Invoice.Mact),
                        new SqlParameter("@UserID", V6Login.UserId),
                        new SqlParameter("@KeyAM", "IMTYPE='X'")
                    };
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_SOH_DELETE_ALL", plist);
                }

                //Xử lý từng nhóm dữ liệu
                foreach (KeyValuePair<string, List<DataRow>> item in data_dictionary)
                {
                    var data_rows = item.Value;
                    try
                    {
                        AM_DATA = GET_AM_Data(data_rows, "SO_LUONG,SO_LUONG1,TIEN_NT2,TIEN_NT,TIEN2,TIEN,THUE_NT,THUE,CK_NT,CK,GG_NT,GG", "MA_NX");

                        var sttRec = V6BusinessHelper.GetNewSttRec(Invoice.Mact);

                        string ma_sonb;
                        DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(AM_DATA["NGAY_CT"]);
                        var so_ct = V6BusinessHelper.GetNewSoCt_date(Invoice.Mact, ngay_ct, "1", madvcs, makho, sttRec, V6Login.UserId, out ma_sonb);

                        AM_DATA["STT_REC"] = sttRec;
                        AM_DATA["SO_CT"] = so_ct;
                        AM_DATA["MA_SONB"] = ma_sonb;
                        var AD1_List = GET_AD1_List(data_rows, sttRec);
                        
                        if (Invoice.InsertInvoice(AM_DATA, AD1_List))
                        {
                            f9Message += V6Text.Added + item.Key;
                            //Danh dau xóa data.
                            foreach (DataRow remove_row in item.Value)
                            {
                                remove_list_d.Add(remove_row);
                            }
                        }
                        else
                        {
                            f9Message += item.Key + ": " + V6Text.AddFail + Invoice.V6Message;
                            f9MessageAll += item.Key + ": " + V6Text.AddFail + Invoice.V6Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        f9Message += item.Key + ": " + ex.Message;
                        f9MessageAll += item.Key + ": " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                f9Message = "F9Thread: " + ex.Message;
            }
            //
            
            f9Running = false;
        }


        private IDictionary<string, object> GET_AM_Data(List<DataRow> dataRows, string sumColumns, string maxColumns)
        {
            try
            {
                //Tính sum max
                sumColumns = "," + sumColumns.ToUpper() + ",";
                maxColumns = "," + maxColumns.ToUpper() + ",";
                var am_row = _tbl.NewRow();
                int rowindex = -1;
                foreach (DataRow row in dataRows)
                {
                    rowindex++;
                    foreach (DataColumn column in _tbl.Columns)
                    {
                        var FIELD = column.ColumnName.ToUpper();
                        if (sumColumns.Contains("," + FIELD + ","))
                        {
                            am_row[FIELD] = (am_row[FIELD] == null ? 0 : ObjectAndString.ObjectToDecimal(am_row[FIELD])) +
                                            ObjectAndString.ObjectToDecimal(row[FIELD]);
                        }
                        else if (maxColumns.Contains("," + FIELD + ","))
                        {
                            if (am_row[FIELD] == null) am_row[FIELD] = row[FIELD];
                            else
                            {
                                if (column.DataType == typeof(string))
                                {
                                    var a = row[FIELD].ToString().Trim();
                                    var b = am_row[FIELD].ToString().Trim();
                                    if (string.CompareOrdinal(a, b) > 0)
                                        am_row[FIELD] = row[FIELD];
                                }
                                else
                                {
                                    if (ObjectAndString.ObjectToDecimal(row[FIELD]) >
                                        ObjectAndString.ObjectToDecimal(am_row[FIELD]))
                                        am_row[FIELD] = row[FIELD];
                                }
                            }
                        }
                        else
                        {
                            if (rowindex == 0) am_row[FIELD] = row[FIELD];
                            else if(!ObjectAndString.IsNoValue(row[FIELD])) am_row[FIELD] = row[FIELD];
                        }
                    }
                }

                //Thêm dữ liệu khác.
                IDictionary<string, object> AM = am_row.ToDataDictionary();
                AM["IMTYPE"] = "X";
                AM["MA_CT"] = Invoice.Mact;
                //AM["MA_NX"] = "111";
                //AM["MA_NT"] = "VND";
                //AM["TY_GIA"] = 1;
                AM["KIEU_POST"] = "2";


                var datakh = V6BusinessHelper.Select(V6TableName.Alkh,
                        new SortedDictionary<string, object>() { { "MA_KH", AM["MA_KH"] } },
                        "*", "", "").Data;
                if (datakh != null && datakh.Rows.Count > 0)
                {
                    var datadickh = datakh.Rows[0].ToDataDictionary();
                    AM["TEN_KH"] = datadickh["TEN_KH"];
                    AM["DIA_CHI"] = datadickh["DIA_CHI"];
                    AM["MA_SO_THUE"] = datadickh["MA_SO_THUE"];
                }
                else
                {
                    AM["TEN_KH"] = "TEN_KH";
                    AM["DIA_CHI"] = "DIA_CHI";
                    AM["MA_SO_THUE"] = "MA_SO_THUE";
                }

                var t_tien_nt2 = 0m;
                var t_gg_nt = 0m;
                var t_ck_nt = 0m;
                var t_thue_nt = 0m;
                var t_vc_nt = 0m;
                var ty_gia = 1m;
                if (AM.ContainsKey("TY_GIA"))
                {
                    ty_gia = ObjectAndString.ObjectToDecimal(AM["TY_GIA"]);
                }
                //SO_LUONG,SO_LUONG1,TIEN_NT0,TIEN_NT,TIEN0,TIEN,THUE_NT,THUE,CK_NT,CK,GG_NT,GG
                if (AM.ContainsKey("SO_LUONG")) AM["T_SO_LUONG"] = AM["SO_LUONG"];
                if (AM.ContainsKey("SO_LUONG1")) AM["TSO_LUONG1"] = AM["SO_LUONG1"];
                if (AM.ContainsKey("TIEN_NT2"))
                {
                    AM["T_TIEN_NT2"] = AM["TIEN_NT2"];
                    t_tien_nt2 = ObjectAndString.ObjectToDecimal(AM["T_TIEN_NT2"]);
                }
                if (AM.ContainsKey("TIEN_NT")) AM["T_TIEN_NT"] = AM["TIEN_NT"];
                if (AM.ContainsKey("TIEN2")) AM["T_TIEN2"] = AM["TIEN2"];
                if (AM.ContainsKey("TIEN")) AM["T_TIEN"] = AM["TIEN"];
                if (AM.ContainsKey("THUE_NT"))
                {
                    AM["T_THUE_NT"] = AM["THUE_NT"];
                    t_thue_nt = ObjectAndString.ObjectToDecimal(AM["T_THUE_NT"]);
                }
                if (AM.ContainsKey("THUE")) AM["T_THUE"] = AM["THUE"];
                if (AM.ContainsKey("CK_NT"))
                {
                    AM["T_CK_NT"] = AM["CK_NT"];
                    t_ck_nt = ObjectAndString.ObjectToDecimal(AM["T_CK_NT"]);
                }
                if (AM.ContainsKey("CK")) AM["T_CK"] = AM["CK"];
                if (AM.ContainsKey("GG_NT"))
                {
                    AM["T_GG_NT"] = AM["GG_NT"];
                    t_gg_nt = ObjectAndString.ObjectToDecimal(AM["T_GG_NT"]);
                }
                if (AM.ContainsKey("GG")) AM["T_GG"] = AM["GG"];

                var t_tt_nt = t_tien_nt2 - t_gg_nt - t_ck_nt + t_thue_nt + t_vc_nt;
                AM["T_TT_NT"] = t_tt_nt;
                AM["T_TT"] = t_tt_nt * ty_gia;

                if (M_SOA_MULTI_VAT == "0")
                {
                    if (AM.ContainsKey("MA_THUE"))
                    {
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@ma_thue", AM["MA_THUE"].ToString()),
                        };
                        var althue = V6BusinessHelper.Select("ALTHUE", "*", "MA_THUE=@ma_thue", "", "", plist).Data;
                        if (althue.Rows.Count > 0)
                        {
                            var row_thue = althue.Rows[0];
                            AM["THUE_SUAT"] = row_thue["THUE_SUAT"];
                            AM["TK_THUE_CO"] = row_thue["TK_THUE_CO"];
                        }
                        else
                        {
                            AM["THUE_SUAT"] = 0m;
                            AM["TK_THUE_CO"] = "";
                        }
                    }
                    else
                    {
                        AM["THUE_SUAT"] = 0m;
                        AM["TK_THUE_CO"] = "";
                    }
                }

                //fIX
                if (!AM.ContainsKey("TK_THUE_NO")) AM["TK_THUE_NO"] = AM["MA_NX"];
                if (!AM.ContainsKey("DIEN_GIAI")) AM["DIEN_GIAI"] = "";
                if (!AM.ContainsKey("NGAY_LCT")) AM["NGAY_LCT"] = AM["NGAY_CT"];
                if (!AM.ContainsKey("T_TIEN2")) AM["T_TIEN2"] = 0;
                if (!AM.ContainsKey("T_THUE")) AM["T_THUE"] =0 ;
                if (!AM.ContainsKey("T_THUE_NT")) AM["T_THUE_NT"] = 0;

                return AM;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<IDictionary<string, object>> GET_AD1_List(List<DataRow> dataRows, string sttRec)
        {
            var result = new List<IDictionary<string, object>>();
            for (int i = 0; i < dataRows.Count; i++)
            {
                var one = dataRows[i].ToDataDictionary(sttRec);
                one["MA_CT"] = Invoice.Mact;
                one["STT_REC0"] = ("00000" + (i+1)).Right(5);
                if (one.ContainsKey("SO_LUONG1")) one["SO_LUONG"] = one["SO_LUONG1"];
                
                if (one.ContainsKey("MA_NT"))
                {
                    var one_maNt = one["MA_NT"].ToString().Trim();
                    
                    if (one.ContainsKey("GIA_NT1")) one["GIA_NT"] = one["GIA_NT1"];
                    if (one.ContainsKey("GIA_NT21")) one["GIA_NT2"] = one["GIA_NT21"];
                    

                    if (one_maNt == V6Options.M_MA_NT0)
                    {
                        if (one.ContainsKey("GIA_NT1")) one["GIA"] = one["GIA_NT1"];
                        if (one.ContainsKey("GIA_NT1")) one["GIA1"] = one["GIA_NT1"];
                        if (one.ContainsKey("GIA_NT21")) one["GIA21"] = one["GIA_NT21"];
                        if (one.ContainsKey("GIA_NT21")) one["GIA2"] = one["GIA_NT21"];

                        if (one.ContainsKey("TIEN_NT")) one["TIEN"] = one["TIEN_NT"];
                        if (one.ContainsKey("TIEN_NT2")) one["TIEN2"] = one["TIEN_NT2"];
                        if (one.ContainsKey("THUE_NT")) one["THUE"] = one["THUE_NT"];
                    }
                    else
                    {
                        if (one.ContainsKey("TY_GIA"))
                        {
                            var one_tygia = ObjectAndString.ObjectToDecimal(one["TY_GIA"]);
                            if (one_tygia == 0) one_tygia = 1;
                            if (one.ContainsKey("GIA_NT1")) one["GIA"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT1"])*one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT1")) one["GIA1"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT1"])*one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT21")) one["GIA21"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT21"]) * one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT21")) one["GIA2"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT21"]) * one_tygia, V6Setting.RoundGia);


                            if (one.ContainsKey("TIEN_NT")) one["TIEN"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["TIEN_NT"])*one_tygia, V6Setting.RoundTien);
                            if (one.ContainsKey("TIEN_NT2")) one["TIEN2"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["TIEN_NT2"]) * one_tygia, V6Setting.RoundTien);
                            if (one.ContainsKey("THUE_NT")) one["THUE"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["THUE_NT"]) * one_tygia, V6Setting.RoundTien);
                        }
                    }
                }

                
                
                //Lay thong tin vt
                if (one.ContainsKey("MA_VT"))
                {
                    var ma_vt = one["MA_VT"].ToString().Trim();
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@maVt", ma_vt), 
                    };
                    var vt_data = V6BusinessHelper.Select("ALVT", "*", "MA_VT=@maVt", "", "", plist).Data;
                    if (vt_data != null && vt_data.Rows.Count > 0)
                    {
                        var vt_row_data = vt_data.Rows[0].ToDataDictionary();
                        one["DVT1"] = vt_row_data["DVT"];
                        one["HE_SO1"] = 1;
                        one["HE_SO1T"] = 1;
                        one["HE_SO1M"] = 1;
                        one["TK_VT"] = vt_row_data["TK_VT"];
                        one["DVT"] = vt_row_data["DVT"];
                        one["TK_DT"] = vt_row_data["TK_DT"];
                        one["TK_GV"] = vt_row_data["TK_GV"];
                    }
                }

                result.Add(one);
            }
            return result;
        }

        /// <summary>
        /// Nhập thuế tự động
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="sttRec"></param>
        /// <returns></returns>
        

        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                //Remove
                while (remove_list_d.Count > 0)
                {
                    _tbl.Rows.Remove(remove_list_d[0]);
                    remove_list_d.RemoveAt(0);
                }

                var cError = f9Message;
                if (cError.Length > 0)
                {
                    f9Message = f9Message.Substring(cError.Length);
                    V6ControlFormHelper.SetStatusText(cError);
                    V6ControlFormHelper.ShowMainMessage("F9 running: " + cError);
                }
            }
            else
            {
                ((Timer)sender).Stop();
                UnlockButtons();

                //Remove
                while (remove_list_d.Count > 0)
                {
                    _tbl.Rows.Remove(remove_list_d[0]);
                    remove_list_d.RemoveAt(0);
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }

                //btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F9 finish " + f9Message);
                V6ControlFormHelper.ShowMainMessage("F9 finish! " + f9Message);
                V6ControlFormHelper.ShowInfoMessage("F9 finish: " + f9MessageAll, 500, this);
                if (f9MessageAll.Length > 0)
                {
                    Logger.WriteToLog(V6Login.ClientName + " " + GetType() + "XLS_SOH2 F9 " + f9MessageAll);
                }
                f9Message = "";
                f9MessageAll = "";
            }
        }
        #endregion xử lý F9

    }
}

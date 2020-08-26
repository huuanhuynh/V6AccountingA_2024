using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
    public class XLSIND_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private string check = null;

        public XLSIND_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(string.Format("F9: {0}", V6Text.Text("CHUYEN")));
        }

        protected override void MakeReport2()
        {
            try
            {
                FilterControl.UpdateValues();

                _tbl = Excel_File.Sheet1ToDataTable(FilterControl.String1, 0, V6Options.M_MAXROWS_EXCEL);
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
                        _tbl = Data_Table.ChuyenMaTiengViet(_tbl, from, to);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(V6Text.Text("NoFromTo"));
                    }
                }
                //FIX DATA
                if (!_tbl.Columns.Contains("TY_GIA"))
                {
                    _tbl.Columns.Add("TY_GIA", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "TY_GIA", 1m);
                }
                
                if (!_tbl.Columns.Contains("TIEN_NT0"))
                {
                    _tbl.Columns.Add("TIEN_NT0", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "TIEN_NT0", 0m);
                }
                if (!_tbl.Columns.Contains("TIEN0"))
                {
                    _tbl.Columns.Add("TIEN0", typeof(decimal));
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["TIEN0"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["TIEN_NT0"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }
                if (!_tbl.Columns.Contains("TIEN_NT"))
                {
                    _tbl.Columns.Add("TIEN_NT", typeof(decimal));
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["TIEN_NT"] = row["TIEN_NT0"];

                    }
                }
                if (!_tbl.Columns.Contains("TIEN"))
                {
                    _tbl.Columns.Add("TIEN", typeof(decimal));
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["TIEN"] = row["TIEN0"];

                    }
                }

                All_Objects["_data"] = _tbl;
                All_Objects["data"] = _tbl.Copy();
                InvokeFormEvent(FormDynamicEvent.DYNAMICFIXEXCEL);
                InvokeFormEvent("AFTERFIXDATA");
                //
                dataGridView1.DataSource = _tbl;

                var alim2xls = V6BusinessHelper.Select("ALIM2XLS", "top 1 *", "MA_CT='IND'").Data;
                if (alim2xls != null && alim2xls.Rows.Count > 0)
                {
                    var config_row = alim2xls.Rows[0];
                    var khoa = ObjectAndString.SplitString(config_row["KHOA"].ToString().Trim());
                    var id_check = ObjectAndString.SplitString(config_row["ID_CHECK"].ToString().Trim());
                    var lost_fields = "";
                    foreach (string field in khoa)
                    {
                        if (!_tbl.Columns.Contains(field))
                        {
                            check += string.Format("{0} {1}", V6Text.NoData, field);
                            lost_fields += ", " + field;
                        }
                    }
                    // Trim khoảng trắng thừa và ký tự đặc biệt trong mã.
                    foreach (DataRow row in _tbl.Rows)
                    {
                        foreach (string field in id_check)
                        {
                            if (_tbl.Columns.Contains(field))
                                if (row[field] is string)
                                {
                                    row[field] = ObjectAndString.TrimSpecial(row[field].ToString());
                                }
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
                string[] data_fields = "MA_KH,MA_VT,MA_KHO_I".Split(',');
                string[] check_fields = "MA_KH,MA_VT,MA_KHO".Split(',');
                string[] check_tables = "ALKH,ALVT,ALKHO".Split(',');
                check += V6ControlFormHelper.CheckDataInGridView(dataGridView1, data_fields, check_fields, check_tables, true);

                if (!string.IsNullOrEmpty(check))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check);
                    return;
                }
            }
            catch (Exception)
            {
                // ignored
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
                        chkAutoSoCt_Checked = FilterControl.Check3;

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
        private string f9Error = "";
        private string f9ErrorAll = "";
        V6Invoice74 Invoice = new V6Invoice74();
        private bool chkAutoSoCt_Checked = false;
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9ErrorAll = "";
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

                    string soct_or_makh = row["SO_CT"].ToString().Trim().ToUpper();
                    if (chkAutoSoCt_Checked)
                    {
                        soct_or_makh = row["MA_KH"].ToString().Trim().ToUpper();
                    }
                    string ngay_ct = date.ToString("yyyyMMdd");
                    if (soct_or_makh != "" && ngay_ct != "")
                    {
                        var key = string.Format("[{0}]_[{1}]", soct_or_makh, ngay_ct);
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
                        f9Error += "Có dòng rỗng";
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
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IND_DELETE_ALL", plist);
                }

                //Xử lý từng nhóm dữ liệu
                foreach (KeyValuePair<string, List<DataRow>> item in data_dictionary)
                {
                    var data_rows = item.Value;
                    try
                    {
                        IDictionary<string, object> AM = GET_AM_Data(data_rows, "SO_LUONG1,SO_LUONG,TIEN_NT,TIEN", "");
                        var sttRec = V6BusinessHelper.GetNewSttRec(Invoice.Mact);
                        if (chkAutoSoCt_Checked) // Tự động tạo số chứng từ.
                        {
                            string ma_sonb;
                            DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(AM["NGAY_CT"]);
                            var so_ct = V6BusinessHelper.GetNewSoCt_date(Invoice.Mact, ngay_ct, "1", madvcs, makho, sttRec, V6Login.UserId, out ma_sonb);
                            AM["SO_CT"] = so_ct;
                            AM["MA_SONB"] = ma_sonb;
                        }
                        AM["STT_REC"] = sttRec;
                        var AD1_List = GET_AD1_List(data_rows, sttRec);

                        All_Objects["AM"] = AM;
                        All_Objects["AD"] = AD1_List;
                        InvokeFormEvent("BEFOREINSERT");
                        if (Invoice.InsertInvoice(AM, AD1_List))
                        {
                            f9Error += V6Text.Added + item.Key;
                            //Danh dau xóa data.
                            foreach (DataRow remove_row in item.Value)
                            {
                                remove_list_d.Add(remove_row);
                            }
                        }
                        else
                        {
                            f9Error += item.Key + ": " + V6Text.AddFail + Invoice.V6Message;
                            f9ErrorAll += item.Key + ": " + V6Text.AddFail + Invoice.V6Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        f9Error += item.Key + ": " + ex.Message;
                        f9ErrorAll += item.Key + ": " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                f9Error = "F9Thread: " + ex.Message;
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
                foreach (DataRow row in dataRows)
                {
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
                            am_row[FIELD] = row[FIELD];
                        }
                    }
                }

                //Thêm dữ liệu khác.
                var AM = am_row.ToDataDictionary();
                AM["IMTYPE"] = "X";
                AM["MA_CT"] = Invoice.Mact;
                AM["KIEU_POST"] = 2;

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


                var t_tien_nt = 0m;
                var ty_gia = 1m;
                var t_tien = 0m;
                
                if (AM.ContainsKey("TY_GIA"))
                {
                    ty_gia = ObjectAndString.ObjectToDecimal(AM["TY_GIA"]);
                }
                //SO_LUONG,SO_LUONG1,TIEN_NT0,TIEN_NT,TIEN0,TIEN,THUE_NT,THUE,CP_NT,CP,CK_NT,CK,GG_NT,GG
                if (AM.ContainsKey("SO_LUONG")) AM["T_SO_LUONG"] = AM["SO_LUONG"];
                if (AM.ContainsKey("SO_LUONG1")) AM["TSO_LUONG1"] = AM["SO_LUONG1"];
                if (AM.ContainsKey("TIEN_NT"))
                {
                    AM["T_TIEN_NT"] = AM["TIEN_NT"];
                    t_tien_nt = ObjectAndString.ObjectToDecimal(AM["T_TIEN_NT"]);
                }
                if (AM.ContainsKey("TIEN")) AM["T_TIEN"] = AM["TIEN"];
                
                if (AM.ContainsKey("T_TIEN"))
                {
                    t_tien = ObjectAndString.ObjectToDecimal(AM["T_TIEN"]);
                }
                

                //SO_LUONG,SO_LUONG1,TIEN_NT0,TIEN_NT,TIEN0,TIEN,THUE_NT,THUE,CK_NT,CK,GG_NT,GG
                if (AM.ContainsKey("SO_LUONG")) AM["T_SO_LUONG"] = AM["SO_LUONG"];
                if (AM.ContainsKey("SO_LUONG1")) AM["TSO_LUONG1"] = AM["SO_LUONG1"];
                if (AM.ContainsKey("TIEN_NT")) AM["T_TIEN_NT"] = AM["TIEN_NT"];
                if (AM.ContainsKey("TIEN")) AM["T_TIEN"] = AM["TIEN"];
                
                
                AM["T_TT_NT"] = t_tien_nt;
                AM["T_TT"] = t_tien;

                //fIX
                if (!AM.ContainsKey("NGAY_LCT")) AM["NGAY_LCT"] = AM["NGAY_CT"];


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
                one["STT_REC0"] = ("00000" + (i + 1)).Right(5);
                if (one.ContainsKey("SO_LUONG1")) one["SO_LUONG"] = one["SO_LUONG1"];
                if (one.ContainsKey("TIEN_NT0")) one["TIEN_NT"] = one["TIEN_NT0"];

                if (one.ContainsKey("MA_NT"))
                {
                    var one_maNt = one["MA_NT"].ToString().Trim();

                    if (one.ContainsKey("GIA_NT01")) one["GIA_NT"] = one["GIA_NT01"];
                    if (one.ContainsKey("GIA_NT01")) one["GIA_NT0"] = one["GIA_NT01"];
                    if (one.ContainsKey("GIA_NT01")) one["GIA_NT1"] = one["GIA_NT01"];

                    if (one_maNt == V6Options.M_MA_NT0)
                    {
                        if (one.ContainsKey("GIA_NT01")) one["GIA"] = one["GIA_NT01"];
                        if (one.ContainsKey("GIA_NT01")) one["GIA01"] = one["GIA_NT01"];
                        if (one.ContainsKey("GIA_NT01")) one["GIA0"] = one["GIA_NT01"];
                        if (one.ContainsKey("GIA_NT01")) one["GIA1"] = one["GIA_NT01"];

                        if (one.ContainsKey("TIEN_NT0")) one["TIEN"] = one["TIEN_NT0"];
                        if (one.ContainsKey("TIEN_NT0")) one["TIEN0"] = one["TIEN_NT0"];
                        if (one.ContainsKey("THUE_NT")) one["THUE"] = one["THUE_NT"];
                    }
                    else
                    {
                        if (one.ContainsKey("TY_GIA"))
                        {
                            var one_tygia = ObjectAndString.ObjectToDecimal(one["TY_GIA"]);
                            if (one_tygia == 0) one_tygia = 1;
                            if (one.ContainsKey("GIA_NT01")) one["GIA"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"]) * one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT01")) one["GIA0"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"]) * one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT01")) one["GIA1"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"]) * one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT01")) one["GIA01"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"]) * one_tygia, V6Setting.RoundGia);


                            if (one.ContainsKey("TIEN_NT0")) one["TIEN"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["TIEN_NT0"]) * one_tygia, V6Setting.RoundTien);
                            if (one.ContainsKey("TIEN_NT0")) one["TIEN0"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["TIEN_NT0"]) * one_tygia, V6Setting.RoundTien);
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
                        one["HE_SO1T"] = 1;
                        one["HE_SO1M"] = 1;
                        one["TK_VT"] = vt_row_data["TK_VT"];
                        one["DVT"] = vt_row_data["DVT"];
                    }
                }

                result.Add(one);
            }
            return result;
        }

        //private List<IDictionary<string, object>> GET_AD2_List(List<DataRow> dataRows, string sttRec)
        //{
        //    List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
        //    for (int i = 0; i < dataRows.Count; i++)
        //    {
        //        var one = dataRows[i].ToDataDictionary(sttRec);
        //        one["STT_REC0"] = ("00000" + (i + 1)).Right(5);
        //        one["MA_CT"] = Invoice.Mact;

        //        result.Add(one);
        //    }
        //    return result;
        //}

        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                //Remove
                while (remove_list_d.Count>0)
                {
                    _tbl.Rows.Remove(remove_list_d[0]);
                    remove_list_d.RemoveAt(0);
                }

                var cError = f9Error;
                if (cError.Length > 0)
                {
                    f9Error = f9Error.Substring(cError.Length);
                    V6ControlFormHelper.SetStatusText(cError);
                    V6ControlFormHelper.ShowMainMessage("F9 running: " + cError);
                }
            }
            else
            {
                ((Timer)sender).Stop();
                UnlockButtons();
                InvokeFormEvent(FormDynamicEvent.AFTERF9);
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
                SetStatusText("F9 finish " + f9Error);
                ShowMainMessage("F9 finish! " + f9Error);
                this.ShowInfoMessage("F9 finish: " + f9ErrorAll);

                f9Error = "";
                f9ErrorAll = "";
            }
        }
        #endregion xử lý F9

        
    }
}

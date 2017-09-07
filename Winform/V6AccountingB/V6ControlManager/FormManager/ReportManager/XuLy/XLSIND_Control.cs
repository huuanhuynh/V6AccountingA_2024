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
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XLSIND_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        private DataTable data;
        private List<DataRow> rows_for_remove;
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private bool check = false;

        public XLSIND_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9 Chuyển");
        }

        protected override void MakeReport2()
        {
            try
            {
                FilterControl.UpdateValues();

                data = Excel_File.Sheet1ToDataTable(FilterControl.String1);
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
                        data = Data_Table.ChuyenMaTiengViet(data, from, to);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage("Chưa chọn mã nguồn và đích.");
                    }
                }
                dataGridView1.DataSource = data;
                string[] data_fields = "MA_KH,MA_VT".Split(',');
                string[] check_fields = "MA_KH,MA_VT".Split(',');
                string[] check_tables = "ALKH,ALVT".Split(',');
                check = V6ControlFormHelper.CheckDataInGridView(dataGridView1, data_fields, check_fields, check_tables);

                if (!check)
                {
                    this.ShowWarningMessage("Kiểm tra dữ liệu!");
                    return;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #region ==== Xử lý F8 ==== Đang chạy giống F9 (copy chưa sửa lại)
        protected override void XuLyF8()
        {
            return;
            try
            {
                Timer tF8 = new Timer();
                tF8.Interval = 500;
                tF8.Tick += tF8_Tick;
                Thread t = new Thread(F8Thread);
                CheckForIllegalCrossThreadCalls = false;
                t.IsBackground = true;
                t.Start();
                tF8.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF8: " + ex.Message);
            }
        }

        private bool F8Running;
        private string F8Error = "";
        private string F8ErrorAll = "";
        private void F8Thread()
        {
            F8Running = true;
            F8ErrorAll = "";

            int i = 0, stt = 0;

            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++; stt++;
                try
                {
                    if (row.Cells[ID_FIELD].Value != DBNull.Value && row.Cells[ID_FIELD].Value.ToString().Trim() != ""
                        && row.Cells[NAME_FIELD].Value != DBNull.Value && row.Cells[NAME_FIELD].Value.ToString().Trim() != "")
                    {
                        var dataDic = new SortedDictionary<string, object>();
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            dataDic.Add(dataGridView1.Columns[j].DataPropertyName, row.Cells[j].Value);
                        }

                        if (V6BusinessHelper.Insert(V6TableName.Alkh, dataDic))
                        {
                            remove_list_g.Add(row);
                        }
                        else
                        {
                            F8Error += "Dòng " + stt + " thêm không được.";
                            F8ErrorAll += "Dòng " + stt + " thêm không được.";
                        }
                    }
                    else
                    {
                        F8Error += "Dòng " + stt + " không có đủ MA_KH và TEN_KH.";
                        F8ErrorAll += "Dòng " + stt + " không có đủ MA_KH và TEN_KH.";
                    }
                }
                catch (Exception ex)
                {

                    F8Error += "Dòng " + stt + ": " + ex.Message;
                    F8ErrorAll += "Dòng " + stt + ": " + ex.Message;
                }

            }
            F8Running = false;
        }

        void tF8_Tick(object sender, EventArgs e)
        {
            if (F8Running)
            {
                var cError = F8Error;
                F8Error = F8Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F8 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                //btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F8 running "
                    + (F8Error.Length > 0 ? "Error: " : "")
                    + F8Error);

                this.ShowErrorMessage(GetType() + ".F8 finish "
                    + (F8ErrorAll.Length > 0 ? "Error: " : "")
                    + F8ErrorAll);
                V6ControlFormHelper.ShowMainMessage("F8 finish!");
            }
        }
        #endregion xử lý F8


        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (!check)
                {
                    this.ShowWarningMessage("Kiểm tra dữ liệu!");
                    return;
                }
                if (data != null)
                {
                    if (data.Columns.Contains(ID_FIELD) && data.Columns.Contains(NAME_FIELD))
                    {
                        Timer timerF9 = new Timer {Interval = 1000};
                        timerF9.Tick += tF9_Tick;
                        rows_for_remove = new List<DataRow>();
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
                        V6ControlFormHelper.ShowMessage(string.Format("Dữ liệu không có {0} và {1}", ID_FIELD, NAME_FIELD));
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMessage("Chưa có dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
            }
        }

        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        V6Invoice74 Invoice = new V6Invoice74();
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9ErrorAll = "";

                int i = 0, stt = 0;



                //Gom chi tiet theo SO_CT va NGAY_CT
                Dictionary<string, List<DataRow>> data_dictionary = new Dictionary<string, List<DataRow>>();
                DateTime? dateMin = null, dateMax = null;
                foreach (DataRow row in data.Rows)
                {
                    var date = ObjectAndString.ObjectToDate(row["NGAY_CT"]);
                    if (date != null)
                    {
                        if (dateMin == null || date < dateMin)
                        {
                            dateMin = date;
                        }

                        if (dateMax == null || date > dateMax)
                        {
                            dateMax = date;
                        }
                    }
                    string so_ct = row["SO_CT"].ToString().Trim().ToUpper();
                    string ngay_ct = date.Value.ToString("yyyyMMdd");
                    if (so_ct != "" && ngay_ct != "")
                    {
                        var key = so_ct + ngay_ct;
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
                        //row.DefaultCellStyle.BackColor = Color.Red;
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
                        SortedDictionary<string, object> AM = GET_AM_Data(data_rows, "SO_LUONG1,TIEN_NT0", "");
                        var sttRec = V6BusinessHelper.GetNewSttRec(Invoice.Mact);
                        AM["STT_REC"] = sttRec;
                        var AD1_List = GET_AD1_List(data_rows, sttRec);
                        
                        if (Invoice.InsertInvoice(AM, AD1_List))
                        {
                            f9Error += "Đã thêm: " + item.Key;
                            //Danh dau xóa data.
                            foreach (DataRow remove_row in item.Value)
                            {
                                rows_for_remove.Add(remove_row);
                            }
                        }
                        else
                        {
                            f9Error += item.Key + ": " + "Thêm lỗi " + Invoice.V6Message;
                            f9ErrorAll += item.Key + ": " + "Thêm lỗi " + Invoice.V6Message;
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


        private SortedDictionary<string, object> GET_AM_Data(List<DataRow> dataRows, string sumColumns, string maxColumns)
        {
            try
            {
                //Tính sum max
                sumColumns = "," + sumColumns.ToUpper() + ",";
                maxColumns = "," + maxColumns.ToUpper() + ",";
                var am_row = data.NewRow();
                foreach (DataRow row in dataRows)
                {
                    foreach (DataColumn column in data.Columns)
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
                AM["MA_NX"] = "111";
                AM["MA_NT"] = "VND";
                AM["TY_GIA"] = 1;
                AM["KIEU_POST"] = 1;

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

                if (AM.ContainsKey("TT")) AM["T_TT"] = AM["TT"];
                
                if (AM.ContainsKey("THUE")) AM["T_THUE"] = AM["THUE"];
                

                return AM;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<SortedDictionary<string, object>> GET_AD1_List(List<DataRow> dataRows, string sttRec)
        {
            var result = new List<SortedDictionary<string, object>>();
            for (int i = 0; i < dataRows.Count; i++)
            {
                var one = dataRows[i].ToDataDictionary(sttRec);
                one["STT_REC0"] = ("00000" + (i+1)).Right(5);
                one["MA_CT"] = Invoice.Mact;

                result.Add(one);
            }
            return result;
        }

        private List<SortedDictionary<string, object>> GET_AD2_List(List<DataRow> dataRows, string sttRec)
        {
            var result = new List<SortedDictionary<string, object>>();
            for (int i = 0; i < dataRows.Count; i++)
            {
                var one = dataRows[i].ToDataDictionary(sttRec);
                one["STT_REC0"] = ("00000" + (i + 1)).Right(5);
                one["MA_CT"] = Invoice.Mact;

                result.Add(one);
            }
            return result;
        }

        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                //Remove
                while (rows_for_remove.Count>0)
                {
                    data.Rows.Remove(rows_for_remove[0]);
                    rows_for_remove.RemoveAt(0);
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

                //Remove
                while (rows_for_remove.Count > 0)
                {
                    data.Rows.Remove(rows_for_remove[0]);
                    rows_for_remove.RemoveAt(0);
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

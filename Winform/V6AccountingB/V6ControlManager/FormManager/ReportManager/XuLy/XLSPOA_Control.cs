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
    public class XLSPOA_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        private DataTable data;
        private List<DataRow> rows_for_remove;
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private bool check = false;

        public XLSPOA_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
                //FIX DATA
                if (!data.Columns.Contains("TIEN0"))
                {
                    data.Columns.Add("TIEN0", typeof (decimal));
                    foreach (DataRow row in data.Rows)
                    {
                        row["TIEN0"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["TIEN_NT0"])*
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }
                if (!data.Columns.Contains("THUE"))
                {
                    data.Columns.Add("THUE", typeof(decimal));
                    foreach (DataRow row in data.Rows)
                    {
                        row["THUE"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["THUE_NT"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }

                dataGridView1.DataSource = data;

                var alim2xls = V6BusinessHelper.Select("ALIM2XLS", "top 1 *", "MA_CT='POA'").Data;
                if (alim2xls != null && alim2xls.Rows.Count > 0)
                {
                    var khoa = alim2xls.Rows[0]["KHOA"].ToString().Trim().Split(',');
                    var lost_fields = "";
                    foreach (string field in khoa)
                    {
                        if (!data.Columns.Contains(field))
                        {
                            check = false;
                            lost_fields += ", " + field;
                        }
                    }
                    if (lost_fields.Length > 2)
                    {
                        lost_fields = lost_fields.Substring(2);
                        this.ShowWarningMessage("Dữ liệu thiếu: " + lost_fields);
                    }
                }
                else
                {
                    check = false;
                }

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
                remove_list_g = new List<DataGridViewRow>();
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
                            //i--;
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
                RemoveGridViewRow();
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
                        LockButtons();
                        
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
        V6Invoice71 Invoice = new V6Invoice71();
        private SortedDictionary<string, object> AM_DATA; 
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
                        var key = so_ct + ":" + ngay_ct;
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
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_POA_DELETE_ALL", plist);
                }

                //Xử lý từng nhóm dữ liệu
                foreach (KeyValuePair<string, List<DataRow>> item in data_dictionary)
                {
                    var data_rows = item.Value;
                    try
                    {
                        AM_DATA = GET_AM_Data(data_rows, "SO_LUONG,SO_LUONG1,TIEN_NT0,TIEN_NT,TIEN0,TIEN,THUE_NT,THUE,CK_NT,CK,GG_NT,GG", "MA_NX");

                        var sttRec = V6BusinessHelper.GetNewSttRec(Invoice.Mact);
                        AM_DATA["STT_REC"] = sttRec;
                        var AD1_List = GET_AD1_List(data_rows, sttRec);
                        var AD2_List = GET_AD2_List(data_rows, sttRec);

                        if (Invoice.InsertInvoice(AM_DATA, AD1_List, AD2_List, new List<SortedDictionary<string, object>>()))//!!!!!!!!
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

                //SO_LUONG,SO_LUONG1,TIEN_NT0,TIEN_NT,TIEN0,TIEN,THUE_NT,THUE,CK_NT,CK,GG_NT,GG
                if (AM.ContainsKey("SO_LUONG")) AM["T_SO_LUONG"] = AM["SO_LUONG"];
                if (AM.ContainsKey("SO_LUONG1")) AM["TSO_LUONG1"] = AM["SO_LUONG1"];
                if (AM.ContainsKey("TIEN_NT0")) AM["T_TIEN_NT0"] = AM["TIEN_NT0"];
                if (AM.ContainsKey("TIEN_NT")) AM["T_TIEN_NT"] = AM["TIEN_NT"];
                if (AM.ContainsKey("TIEN0")) AM["T_TIEN0"] = AM["TIEN0"];
                if (AM.ContainsKey("TIEN")) AM["T_TIEN"] = AM["TIEN"];
                if (AM.ContainsKey("THUE_NT")) AM["T_THUE_NT"] = AM["THUE_NT"];
                if (AM.ContainsKey("THUE")) AM["T_THUE"] = AM["THUE"];
                if (AM.ContainsKey("CK_NT")) AM["T_CK_NT"] = AM["CK_NT"];
                if (AM.ContainsKey("CK")) AM["T_CK"] = AM["CK"];
                if (AM.ContainsKey("GG_NT")) AM["T_GG_NT"] = AM["GG_NT"];
                if (AM.ContainsKey("GG")) AM["T_GG"] = AM["GG"];

                if (AM.ContainsKey("MA_THUE"))
                {
                    SqlParameter[] plist=
                    {
                        new SqlParameter("@ma_thue",AM["MA_THUE"].ToString()), 
                    };
                    var althue30 = V6BusinessHelper.Select("ALTHUE30", "*", "MA_THUE=@ma_thue","","",plist).Data;
                    if (althue30.Rows.Count > 0)
                    {
                        var row_thue = althue30.Rows[0];
                        AM["THUE_SUAT"] = row_thue["THUE_SUAT"];
                        AM["TK_THUE_NO"] = row_thue["TK_THUE_NO"];
                    }
                    else
                    {
                        AM["THUE_SUAT"] = 0m;
                        AM["TK_THUE_NO"] = "";
                    }
                }
                else
                {
                    AM["THUE_SUAT"] = 0m;
                    AM["TK_THUE_NO"] = "";
                }

                //fIX
                if (!AM.ContainsKey("DIEN_GIAI")) AM["DIEN_GIAI"] = "";
                if (!AM.ContainsKey("NGAY_LCT")) AM["NGAY_LCT"] = AM["NGAY_CT"];
                if (!AM.ContainsKey("T_TIEN0")) AM["T_TIEN0"] = "";
                if (!AM.ContainsKey("T_THUE")) AM["DIEN_GIAI"] = "";
                if (!AM.ContainsKey("T_THUE_NT")) AM["DIEN_GIAI"] = "";

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
                one["MA_CT"] = Invoice.Mact;
                one["STT_REC0"] = ("00000" + (i+1)).Right(5);
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
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"])*one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT01")) one["GIA0"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"])*one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT01")) one["GIA1"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"]) * one_tygia, V6Setting.RoundGia);
                            if (one.ContainsKey("GIA_NT01")) one["GIA01"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT01"]) * one_tygia, V6Setting.RoundGia);


                            if (one.ContainsKey("TIEN_NT0")) one["TIEN"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["TIEN_NT0"])*one_tygia, V6Setting.RoundTien);
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
                        one["HE_SO1"] = 1;
                        one["TK_VT"] = vt_row_data["TK_VT"];
                        one["DVT"] = vt_row_data["DVT"];
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
        private List<SortedDictionary<string, object>> GET_AD2_List(List<DataRow> dataRows, string sttRec)
        {
            var result = new List<SortedDictionary<string, object>>();
            var newRow = new SortedDictionary<string,object>();
            newRow["SO_CT"] = AM_DATA["SO_CT"];
            newRow["NGAY_CT"] = AM_DATA["NGAY_CT"];
            newRow["NGAY_CT0"] = AM_DATA["NGAY_CT0"];
            newRow["NGAY_LCT"] = AM_DATA["NGAY_LCT"];
            newRow["SO_CT0"] = AM_DATA["SO_CT0"];
            
            newRow["SO_SERI0"] = AM_DATA["SO_SERI0"];
            newRow["MA_KH"] = AM_DATA["MA_KH"];
            newRow["TEN_KH"] = AM_DATA["TEN_KH"];
            newRow["DIA_CHI"] = AM_DATA["DIA_CHI"];
            newRow["MA_SO_THUE"] = AM_DATA["MA_SO_THUE"];
            newRow["TEN_VT"] = AM_DATA["DIEN_GIAI"];
            //Ten_vt,so_luong,gia,t_tien
            newRow["T_TIEN"] = AM_DATA["T_TIEN0"];
            newRow["T_TIEN_NT"] = AM_DATA["T_TIEN_NT0"];
            newRow["T_THUE"] = AM_DATA["T_THUE"];
            newRow["T_THUE_NT"] = AM_DATA["T_THUE_NT"];
            newRow["MA_THUE"] = AM_DATA["MA_THUE"];
            newRow["THUE_SUAT"] = AM_DATA["THUE_SUAT"];
            newRow["TK_THUE_NO"] = AM_DATA["TK_THUE_NO"];
            newRow["TK_DU"] = AM_DATA["MA_NX"];
            newRow["MAU_BC"] = 1;

            newRow["MA_CT"] = Invoice.Mact;
            newRow["STT_REC"] = sttRec;
            newRow["STT_REC0"] = "00001";

            if (newRow.ContainsKey("T_THUE_NT") && ObjectAndString.ObjectToDecimal(newRow["T_THUE_NT"]) > 0)
            {
                result.Add(newRow);
            }

            return result;
        }


        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                //Remove
                while (rows_for_remove.Count > 0)
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
                UnlockButtons();

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
                V6ControlFormHelper.SetStatusText("F9 finish " + f9Error);
                V6ControlFormHelper.ShowMainMessage("F9 finish! " + f9Error);
                V6ControlFormHelper.ShowInfoMessage("F9 finish: " + f9ErrorAll, 500, this);
                if (f9ErrorAll.Length > 0)
                {
                    Logger.WriteToLog(V6Login.ClientName + " " + GetType() + "XLS_POA F9 " + f9ErrorAll);
                }
                f9Error = "";
                f9ErrorAll = "";
            }
        }
        #endregion xử lý F9

    }
}

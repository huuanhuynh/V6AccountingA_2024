﻿using System;
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
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XLSTA1_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        private DataTable data;
        private List<DataRow> rows_for_remove;
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private bool check = false;

        public XLSTA1_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
                if (string.IsNullOrEmpty(FilterControl.String1))
                {
                    return;
                }
                if (!File.Exists(FilterControl.String1))
                {
                    this.ShowMessage(V6Text.NotExist);
                    return;
                }

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
                if (!data.Columns.Contains("PS_CO"))
                {
                    data.Columns.Add("PS_CO", typeof (decimal));
                    foreach (DataRow row in data.Rows)
                    {
                        row["PS_CO"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["PS_CO_NT"])*
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }
                //if (!data.Columns.Contains("THUE"))
                //{
                //    data.Columns.Add("THUE", typeof(decimal));
                //    foreach (DataRow row in data.Rows)
                //    {
                //        row["THUE"] =
                //            V6BusinessHelper.Vround(
                //                ObjectAndString.ObjectToDecimal(row["THUE_NT"]) *
                //                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                //    }
                //}
                //if (!data.Columns.Contains("TIEN"))
                //{
                //    data.Columns.Add("TIEN", typeof(decimal));
                //    foreach (DataRow row in data.Rows)
                //    {
                //        row["TIEN"] =
                //            V6BusinessHelper.Vround(
                //                ObjectAndString.ObjectToDecimal(row["TIEN_NT"]) *
                //                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                //    }
                //}

                dataGridView1.DataSource = data;
                string[] data_fields = "MA_KH_I,TK_I".Split(',');
                string[] check_fields = "MA_KH,TK".Split(',');
                string[] check_tables = "ALKH,ALTK".Split(',');
                check = V6ControlFormHelper.CheckDataInGridView(dataGridView1, data_fields, check_fields, check_tables);

                var alim2xls = V6BusinessHelper.Select("ALIM2XLS", "top 1 *", "MA_CT='TA1'").Data;
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

                if (!check)
                {
                    this.ShowWarningMessage("Kiểm tra dữ liệu!");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
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
        private string f9Message = "";
        private string f9MessageAll = "";
        V6Invoice41 Invoice = new V6Invoice41();
        private SortedDictionary<string, object> AM_DATA; 
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9MessageAll = "";

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
                        f9Message += "Có dòng rỗng";
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
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_TA1_DELETE_ALL", plist);
                }

                //Xử lý từng nhóm dữ liệu
                foreach (KeyValuePair<string, List<DataRow>> item in data_dictionary)
                {
                    var data_rows = item.Value;
                    try
                    {
                        AM_DATA = GET_AM_Data(data_rows, "PS_CO_NT,PS_CO", "TK");

                        var sttRec = V6BusinessHelper.GetNewSttRec(Invoice.Mact);
                        AM_DATA["STT_REC"] = sttRec;
                        var AD1_List = GET_AD1_List(data_rows, sttRec);
                        
                        if (Invoice.InsertInvoice(AM_DATA, AD1_List,new List<SortedDictionary<string, object>>()))//!!!!!!!!
                        {
                            f9Message += "Đã thêm: " + item.Key;
                            //Danh dau xóa data.
                            foreach (DataRow remove_row in item.Value)
                            {
                                rows_for_remove.Add(remove_row);
                            }
                        }
                        else
                        {
                            f9Message += item.Key + ": " + "Thêm lỗi " + Invoice.V6Message;
                            f9MessageAll += item.Key + ": " + "Thêm lỗi " + Invoice.V6Message;
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


                //var datakh = V6BusinessHelper.Select(V6TableName.Alkh,
                //        new SortedDictionary<string, object>() { { "MA_KH", AM["MA_KH"] } },
                //        "*", "", "").Data;
                //if (datakh != null && datakh.Rows.Count > 0)
                //{
                //    var datadickh = datakh.Rows[0].ToDataDictionary();
                //    AM["TEN_KH"] = datadickh["TEN_KH"];
                //    AM["DIA_CHI"] = datadickh["DIA_CHI"];
                //    AM["MA_SO_THUE"] = datadickh["MA_SO_THUE"];
                //}
                //else
                //{
                //    AM["TEN_KH"] = "TEN_KH";
                //    AM["DIA_CHI"] = "DIA_CHI";
                //    AM["MA_SO_THUE"] = "MA_SO_THUE";
                //}

                var t_ps_co_nt = 0m;
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
                
                if (AM.ContainsKey("PS_CO_NT"))
                {
                    AM["T_PS_CO_NT"] = AM["PS_CO_NT"];
                    t_ps_co_nt = ObjectAndString.ObjectToDecimal(AM["T_PS_CO_NT"]);
                }
                if (AM.ContainsKey("TIEN_NT")) AM["T_TIEN_NT"] = AM["TIEN_NT"];
                if (AM.ContainsKey("PS_CO")) AM["T_PS_CO"] = AM["PS_CO"];
                if (AM.ContainsKey("TIEN")) AM["T_TIEN"] = AM["TIEN"];
                

                var t_tt_nt = t_ps_co_nt ;
                AM["T_TT_NT"] = t_tt_nt;
                AM["T_TT"] = t_tt_nt * ty_gia;

             
             

                //fIX
                if (!AM.ContainsKey("DIEN_GIAI")) AM["DIEN_GIAI"] = "";
                if (!AM.ContainsKey("NGAY_LCT")) AM["NGAY_LCT"] = AM["NGAY_CT"];
                if (!AM.ContainsKey("T_PS_CO")) AM["T_PS_CO"] = 0;
             

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
                                
                if (one.ContainsKey("MA_NT"))
                {
                    var one_maNt = one["MA_NT"].ToString().Trim();
                    
                    
                    

                    if (one_maNt == V6Options.M_MA_NT0)
                    {
                    
                        if (one.ContainsKey("TIEN_NT")) one["TIEN"] = one["TIEN_NT"];
                        if (one.ContainsKey("PS_CO_NT")) one["PS_CO"] = one["PS_CO_NT"];
                    
                    }
                    else
                    {
                        if (one.ContainsKey("TY_GIA"))
                        {
                            var one_tygia = ObjectAndString.ObjectToDecimal(one["TY_GIA"]);
                            if (one_tygia == 0) one_tygia = 1;
                            
                            if (one.ContainsKey("TIEN_NT")) one["TIEN"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["TIEN_NT"])*one_tygia, V6Setting.RoundTien);
                            if (one.ContainsKey("PS_CO_NT")) one["PS_CO"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["PS_CO_NT"]) * one_tygia, V6Setting.RoundTien);
                            
                        }
                    }
                }

                
                
                //Lay thong tin vt
                if (one.ContainsKey("TK_I"))
                {
                    var TK_I = one["TK_I"].ToString().Trim();
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@p1", TK_I), 
                    };
                    var vt_data = V6BusinessHelper.Select("ALTK", "*", "LOAI_TK=1 AND TK=@p1", "", "", plist).Data;
                    if (vt_data != null && vt_data.Rows.Count > 0)
                    {
                        var vt_row_data = vt_data.Rows[0].ToDataDictionary();
                        //one["DVT1"] = vt_row_data["DVT"];
                        //one["HE_SO1"] = 1;
                        //one["TK_VT"] = vt_row_data["TK_VT"];
                        //one["DVT"] = vt_row_data["DVT"];
                        //one["TK_DT"] = vt_row_data["TK_DT"];
                        //one["TK_GV"] = vt_row_data["TK_GV"];
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
                while (rows_for_remove.Count > 0)
                {
                    data.Rows.Remove(rows_for_remove[0]);
                    rows_for_remove.RemoveAt(0);
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
                V6ControlFormHelper.SetStatusText("F9 finish " + f9Message);
                V6ControlFormHelper.ShowMainMessage("F9 finish! " + f9Message);
                V6ControlFormHelper.ShowInfoMessage("F9 finish: " + f9MessageAll, 500, this);
                if (f9MessageAll.Length > 0)
                {
                    this.WriteToLog(GetType() + "XLS_TA1 F9", f9MessageAll);
                }
                f9Message = "";
                f9MessageAll = "";
            }
        }
        #endregion xử lý F9

    }
}

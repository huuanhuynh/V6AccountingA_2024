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
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XLSAP1_Control : XuLyBase
    {
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        //private const string _id_list = "";
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private string check = null;
        
        public XLSAP1_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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

                FixData();
                All_Objects["_data"] = _tbl;
                All_Objects["data"] = _tbl.Copy();
                InvokeFormEvent("AFTERFIXDATA");
                //
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        private void FixData()
        {
            try
            {
                if (_tbl == null) return;
                //FIX DATA
                if (!_tbl.Columns.Contains("TY_GIA"))
                {
                    _tbl.Columns.Add("TY_GIA", typeof (decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "TY_GIA", 1m);
                }
                if (!_tbl.Columns.Contains("MA_THUE"))
                {
                    _tbl.Columns.Add("MA_THUE", typeof(string));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "MA_THUE", "");
                }
                if (!_tbl.Columns.Contains("THUE_NT"))
                {
                    _tbl.Columns.Add("THUE_NT", typeof (decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "THUE_NT", 0m);
                }
                
                if (!_tbl.Columns.Contains("THUE"))
                {
                    _tbl.Columns.Add("THUE", typeof (decimal));
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["THUE"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["THUE_NT"])*
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }
                if (!_tbl.Columns.Contains("TIEN_NT"))
                {
                    _tbl.Columns.Add("TIEN_NT", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "TIEN_NT", 0m);
                }
                if (!_tbl.Columns.Contains("TIEN"))
                {
                    _tbl.Columns.Add("TIEN", typeof(decimal));
                    //V6ControlFormHelper.UpdateDKlist(_data, "TIEN_NT", 0m);
                    foreach (DataRow row in _tbl.Rows)
                    {
                        row["TIEN"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["TIEN_NT"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);

                    }
                }

                All_Objects["_data"] = _tbl;
                All_Objects["data"] = _tbl.Copy();
                InvokeFormEvent(FormDynamicEvent.DYNAMICFIXEXCEL);
                dataGridView1.DataSource = _tbl;

                var alim2xls = V6BusinessHelper.Select("ALIM2XLS", "top 1 *", "MA_CT='AP1'").Data;
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
                            if(_tbl.Columns.Contains(field))
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

                string[] data_fields = "MA_KH,TK_VT".Split(',');
                string[] check_fields = "MA_KH,TK".Split(',');
                string[] check_tables = "ALKH,ALTK".Split(',');
                check += V6ControlFormHelper.CheckDataInGridView(dataGridView1, data_fields, check_fields, check_tables);

                if (!string.IsNullOrEmpty(check))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixData", ex);
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
        private string f9Message = "";
        private string f9ErrorAll = "";
        V6Invoice31 Invoice = new V6Invoice31();
        private IDictionary<string, object> AM_DATA;
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
                        if (chkAutoSoCt_Checked)
                        {
                            f9Message += "Kiểm tra [SO_CT]_[MA_KH].";
                        }
                        else
                        {
                            f9Message += "Kiểm tra [SO_CT].";
                        }
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
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_AP1_DELETE_ALL", plist);
                }

                //Xử lý từng nhóm dữ liệu
                foreach (KeyValuePair<string, List<DataRow>> item in data_dictionary)
                {
                    var data_rows = item.Value;
                    try
                    {
                        AM_DATA = GET_AM_Data(data_rows, "SO_LUONG,TIEN_NT,TIEN,THUE_NT,THUE", "MA_NX");

                        var sttRec = V6BusinessHelper.GetNewSttRec(Invoice.Mact);
                        if (chkAutoSoCt_Checked) // Tự động tạo số chứng từ.
                        {
                            string ma_sonb;
                            DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(AM_DATA["NGAY_CT"]);
                            // Tuanmh 08/03/2019 them madvcs,makho, V6Login.UserId
                            var so_ct = V6BusinessHelper.GetNewSoCt_date(Invoice.Mact, ngay_ct, "1", madvcs, makho, sttRec, V6Login.UserId, out ma_sonb);
                            AM_DATA["SO_CT"] = so_ct;
                            AM_DATA["MA_SONB"] = ma_sonb;
                        }
                        AM_DATA["STT_REC"] = sttRec;
                        var AD1_List = GET_AD1_List(data_rows, sttRec);
                        var AD2_List = GET_AD2_List(data_rows, sttRec);

                        All_Objects["AM"] = AM_DATA;
                        All_Objects["AD"] = AD1_List;
                        All_Objects["AD2"] = AD2_List;
                        InvokeFormEvent("BEFOREINSERT");
                        if (Invoice.InsertInvoice(AM_DATA, AD1_List, AD2_List, new List<IDictionary<string, object>>()))
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
                            f9ErrorAll += item.Key + ": " + V6Text.AddFail + Invoice.V6Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        f9Message += item.Key + ": " + ex.Message;
                        f9ErrorAll += item.Key + ": " + ex.Message;
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

                var t_tien_nt = 0m;
                var t_gg_nt = 0m;
                var t_ck_nt = 0m;
                var t_thue_nt = 0m;
                var t_cp_nt = 0m;
                var t_vc_nt = 0m;
                var ty_gia = 1m;
                var t_tien = 0m;
                var t_thue = 0m;
                if (AM.ContainsKey("TY_GIA"))
                {
                    ty_gia = ObjectAndString.ObjectToDecimal(AM["TY_GIA"]);
                }
                //SO_LUONG,SO_LUONG1,TIEN_NT0,TIEN_NT,TIEN0,TIEN,THUE_NT,THUE,CP_NT,CP,CK_NT,CK,GG_NT,GG
                if (AM.ContainsKey("SO_LUONG")) AM["T_SO_LUONG"] = AM["SO_LUONG"];
                
                if (AM.ContainsKey("TIEN_NT")) AM["T_TIEN_NT"] = AM["TIEN_NT"];
                if (AM.ContainsKey("TIEN")) AM["T_TIEN"] = AM["TIEN"];
                if (AM.ContainsKey("THUE_NT"))
                {
                    AM["T_THUE_NT"] = AM["THUE_NT"];
                    t_thue_nt = ObjectAndString.ObjectToDecimal(AM["T_THUE_NT"]);
                }
                if (AM.ContainsKey("THUE")) AM["T_THUE"] = AM["THUE"];
               

                //SO_LUONG,SO_LUONG1,TIEN_NT0,TIEN_NT,TIEN0,TIEN,THUE_NT,THUE,CK_NT,CK,GG_NT,GG
                if (AM.ContainsKey("SO_LUONG")) AM["T_SO_LUONG"] = AM["SO_LUONG"];
                if (AM.ContainsKey("TIEN_NT")) AM["T_TIEN_NT"] = AM["TIEN_NT"];
                if (AM.ContainsKey("TIEN")) AM["T_TIEN"] = AM["TIEN"];
                if (AM.ContainsKey("THUE_NT")) AM["T_THUE_NT"] = AM["THUE_NT"];
                if (AM.ContainsKey("THUE")) AM["T_THUE"] = AM["THUE"];

                if (AM.ContainsKey("T_TIEN_NT"))
                {
                    t_tien_nt = ObjectAndString.ObjectToDecimal(AM["T_TIEN_NT"]);
                }
                if (AM.ContainsKey("T_TIEN"))
                {
                    t_tien = ObjectAndString.ObjectToDecimal(AM["T_TIEN"]);
                }
                if (AM.ContainsKey("T_THUE"))
                {
                    t_thue = ObjectAndString.ObjectToDecimal(AM["T_THUE"]);
                }

                var t_tt_nt = t_tien_nt - t_gg_nt - t_ck_nt + t_thue_nt + t_cp_nt + t_vc_nt;
                AM["T_TT_NT"] = t_tt_nt;
                AM["T_TT"] = t_tien + t_thue;

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
                if (!AM.ContainsKey("T_TIEN0")) AM["T_TIEN0"] = 0;
                if (!AM.ContainsKey("T_THUE")) AM["T_THUE"] = 0;
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
                
                if (one.ContainsKey("MA_NT"))
                {
                    var one_maNt = one["MA_NT"].ToString().Trim();
                    
                    if (one_maNt == V6Options.M_MA_NT0)
                    {
                        if (one.ContainsKey("TIEN_NT")) one["TIEN"] = one["TIEN_NT"];
                        if (one.ContainsKey("THUE_NT")) one["THUE"] = one["THUE_NT"];
                    }
                    else
                    {
                        if (one.ContainsKey("TY_GIA"))
                        {
                            var one_tygia = ObjectAndString.ObjectToDecimal(one["TY_GIA"]);
                            if (one_tygia == 0) one_tygia = 1;
                            if (one.ContainsKey("GIA_NT")) one["GIA"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["GIA_NT"])*one_tygia, V6Setting.RoundGia);
                            

                            if (one.ContainsKey("TIEN_NT")) one["TIEN"] =
                                V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one["TIEN_NT"])*one_tygia, V6Setting.RoundTien);
                            
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
        private List<IDictionary<string, object>> GET_AD2_List(List<DataRow> dataRows, string sttRec)
        {
            var result = new List<IDictionary<string, object>>();
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
            newRow["T_TIEN"] = AM_DATA["T_TIEN"];
            newRow["T_TIEN_NT"] = AM_DATA["T_TIEN_NT"];
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

            if (newRow.ContainsKey("T_THUE_NT") && ObjectAndString.ObjectToDecimal(newRow["T_THUE_NT"]) != 0)
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
                V6ControlFormHelper.SetStatusText("F9 finish " + f9Message);
                V6ControlFormHelper.ShowMainMessage("F9 finish! " + f9Message);
                V6ControlFormHelper.ShowInfoMessage("F9 finish: " + f9ErrorAll, 500, this);
                if (f9ErrorAll.Length > 0)
                {
                    Logger.WriteToLog(V6Login.ClientName + " " + GetType() + "XLS_AP1 F9 " + f9ErrorAll);
                }
                f9Message = "";
                f9ErrorAll = "";
            }
        }
        #endregion xử lý F9

    }
}

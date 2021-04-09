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
using V6Controls.Forms.Viewer;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6SyncLibrary2021;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class V6IMDATA2TH1_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private string check_string = null;
        private Timer timerAutoF9;
        private System.ComponentModel.IContainer components;
        public string M_SOA_MULTI_VAT = "0";
        
        bool AUTOF9
        {
            get
            {
                return FilterControl.ObjectDictionary.ContainsKey("AUTOF9") &&
                       ObjectAndString.ObjectToBool(FilterControl.ObjectDictionary["AUTOF9"]);
            }
        } 

        public V6IMDATA2TH1_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            try
            {
                InitializeComponent();
                M_SOA_MULTI_VAT = V6Options.GetValue("M_SOA_MULTI_VAT");
                SetDefaultTime();
            }
            catch (Exception)
            {

            }
        }

        private void SetDefaultTime()
        {
            try
            {
                var date1 = FilterControl.GetControlByName("dateNgay_ct1") as DateTimePicker;
                var date2 = FilterControl.GetControlByName("dateNgay_ct2") as DateTimePicker;
                if (date1 != null && EXTRA_INFOR.ContainsKey("TIME1"))
                {
                    var HHmm = ObjectAndString.SplitStringBy(EXTRA_INFOR["TIME1"], ':');
                    var date1_value = new DateTime(date1.Value.Year, date1.Value.Month, date1.Value.Day,
                        0, ObjectAndString.ObjectToInt(HHmm[1]), 00);

                    date1.Value = date1_value.AddHours((double)Number.GiaTriBieuThuc(HHmm[0], null));
                }
                if (date2 != null && EXTRA_INFOR.ContainsKey("TIME2"))
                {
                    var HHmm = ObjectAndString.SplitStringBy(EXTRA_INFOR["TIME2"], ':');
                    var date2_value = new DateTime(date2.Value.Year, date2.Value.Month, date2.Value.Day,
                        0, ObjectAndString.ObjectToInt(HHmm[1]), 00);

                    date2.Value = date2_value.AddHours((double) Number.GiaTriBieuThuc(HHmm[0], null));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "SetDefaultTime", ex);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(string.Format("F9: {0}", V6Text.Text("CHUYEN")));
        }

        public DataTable _columnsMapper_AMAD;
        public DataTable _columnsMapper_ALVT;

        protected override void MakeReport2()
        {
            try
            {
                //@ngay_ct1 char(8),
                //@ngay_ct2 char(8),
                //@ngay_ct3 char(8),
                //@ma_dvcs nvarchar(max),
                //@dele_yn char(1),
                //@auto_yn char(1),
                //@auto_f9 char(1),
                //@user_id int=0
                FilterControl.UpdateValues();
                var plist = new List<SqlParameter>();
                plist.Add(new SqlParameter("@ngay_ct1", FilterControl.Date1.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@ngay_ct2", FilterControl.Date2.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@ngay_ct3", FilterControl.Date3.ToString("yyyyMMdd")));
                plist.Add(new SqlParameter("@ma_dvcs", FilterControl.String1));
                plist.Add(new SqlParameter("@dele_yn", FilterControl.Check1 ? "1" : "0"));
                plist.Add(new SqlParameter("@auto_yn", FilterControl.Check2 ? "1" : "0"));
                plist.Add(new SqlParameter("@auto_f9", FilterControl.Check3 ? "1" : "0"));
                plist.Add(new SqlParameter("@HHFrom", (int)FilterControl.Number1));
                plist.Add(new SqlParameter("@HHTo", (int)FilterControl.Number2));
                plist.Add(new SqlParameter("@user_id", V6Login.UserId));
                var ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, plist.ToArray());
                _tbl = ds.Tables[0];
                
                                
                All_Objects["_data"] = _tbl;
                All_Objects["data"] = _tbl.Copy();
                InvokeFormEvent(FormDynamicEvent.DYNAMICFIXEXCEL);
                InvokeFormEvent("AFTERFIXDATA");
                
                dataGridView1.DataSource = _tbl;
                FormatGridViewBase();
                FormatGridViewExtern();
                
                if (!AUTOF9 && !string.IsNullOrEmpty(check_string))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check_string);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ShowMainMessage("V6IMDATA2 " + ex.Message);
                this.WriteExLog("V6IMDATA2.MakeReport2", ex);
            }
        }

        private void FIX_DATA_COLUMNS()
        {
            if (!_tbl.Columns.Contains("TY_GIA"))
                {
                    _tbl.Columns.Add("TY_GIA", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "TY_GIA", 1m);
                }
                if (!_tbl.Columns.Contains("LOAI_CK"))
                {
                    _tbl.Columns.Add("LOAI_CK", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "LOAI_CK", 1m);
                }
                if (!_tbl.Columns.Contains("THUE_NT"))
                {
                    _tbl.Columns.Add("THUE_NT", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "THUE_NT", 0m);
                }
                if (!_tbl.Columns.Contains("TIEN_NT"))
                {
                    _tbl.Columns.Add("TIEN_NT", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_tbl, "TIEN_NT", 0m);
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
        }

        private void CHANGE_CODE_AU()
        {
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
        }

        private void CHECK_REMOVE_DELETE_DATA()
        {
            try
            {
                // Check data.
                string REFKEY_FIELD = EXTRA_INFOR["REFKEY"];
                SortedDictionary<string, string> testeDictionary = new SortedDictionary<string, string>();
                List<DataRow> removeRows = new List<DataRow>();
                foreach (DataRow row in _tbl.Rows)
                {
                    // select check xoas
                    string refKey = row[REFKEY_FIELD].ToString();
                    if (testeDictionary.ContainsKey(refKey.ToUpper()))
                    {
                        continue;
                    }
                    else
                    {
                        testeDictionary.Add(refKey.ToUpper(), refKey);
                        decimal SL_UD1_SUM = V6BusinessHelper.TinhTongDieuKien(_tbl, "SL_UD1", REFKEY_FIELD, refKey, true);
                        // select check xoa
                        var data_check_am81 = V6BusinessHelper.Select("AM81", "*", string.Format(" {0} = '{1}' and KIEU_POST<>'0'", REFKEY_FIELD, refKey)).Data;
                        if (data_check_am81.Rows.Count > 0)
                        {
                            // xoa _tbl // xoa AM81 ??
                            removeRows.Add(row);

                            // XÓA KHI CHƯA NHẬN F9
                            //if (FilterControl.Check2) // check xoa //// if filter contrl check
                            //{
                            //    var row_am81 = data_check_am81.Rows[0];
                            //    decimal SL_UD1_AM81 = ObjectAndString.ObjectToDecimal(row_am81["SL_UD1"]);
                            //    if (SL_UD1_AM81 != SL_UD1_SUM)
                            //    {
                            //        // xoa AM81 ???
                            //        string stt_rec_am81 = row_am81["STT_REC"].ToString();
                            //        string ma_ct_am81 = row_am81["MA_CT"].ToString();
                            //        SqlParameter[] plist = new []
                            //        {
                            //            new SqlParameter("@Stt_rec", stt_rec_am81),
                            //            new SqlParameter("@Ma_ct", ma_ct_am81),
                            //            new SqlParameter("@UserID", V6Login.UserId),
                            //        };
                            //        V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_SOH_DELETE_MAIN", plist);
                            //        // Delete xong không remove nữa.
                            //        removeRows.Remove(row);
                            //    }
                            //}
                        }
                    }

                }

                while (removeRows.Count > 0)
                {
                    _tbl.Rows.Remove(removeRows[0]);
                    removeRows.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CHECK_REMOVE_DELETE_DATA", ex);
            }
        }


        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (!string.IsNullOrEmpty(check_string))
                {
                    if (!AUTOF9) this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check_string);
                    return;
                }
                if (_tbl != null)
                {
                    FilterControl.UpdateValues();
                    //if (_tbl.Columns.Contains(ID_FIELD) && _tbl.Columns.Contains(NAME_FIELD))
                    {
                        LockButtons();
                        //chkAutoSoCt_Checked = ObjectAndString.ObjectToBool(FilterControl.ObjectDictionary["AUTOSOCT"]);
                        chkAutoSoCt_Checked = FilterControl.Check2;

                        Timer timerF9 = new Timer {Interval = 1000};
                        timerF9.Tick += tF9_Tick;
                        remove_list_d = new List<DataRow>();
                        Thread t = new Thread(F9Thread_AMAD);
                        t.SetApartmentState(ApartmentState.STA);
                        CheckForIllegalCrossThreadCalls = false;
                        t.IsBackground = true;
                        t.Start();
                        timerF9.Start();

                        if (Visible) V6ControlFormHelper.SetStatusText("F9 running ");
                    }
                    //else
                    //{
                    //    V6ControlFormHelper.ShowMainMessage(string.Format("V6IMDATA2 {0} {1} {2} {3}", V6Text.Text("DULIEUBITHIEU"), ID_FIELD, V6Text.Text("AND"), NAME_FIELD));
                    //}
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage("V6IMDATA2 " + V6Text.Text("NODATA"));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private bool f9Running;
        /// <summary>
        /// Thông báo tức thời trong Status1.
        /// </summary>
        private string f9Message = "";
        /// <summary>
        /// Thông báo cuối cùng sau khi chạy xong.
        /// </summary>
        private string f9MessageAll = "";
        V6Invoice91 Invoice = new V6Invoice91();
        private IDictionary<string, object> AM_DATA;
        private bool chkAutoSoCt_Checked = false;
        MyThread newMyThread;
        private void F9Thread_AMAD()
        {
            try
            {
                f9Running = true;
                f9MessageAll = "";

                var ALFCOPY2LIST = V6BusinessHelper.Select("ALFCOPY2LIST", "*", "MA_FILE='"+_reportProcedure+"'").Data;
                var ALFCOPY2DATA = V6BusinessHelper.Select("ALFCOPY2DATA", "*", "MA_FILE='" + _reportProcedure + "'").Data;
                newMyThread = new MyThread(DatabaseConfig.ConnectionString, DatabaseConfig.ConnectionString2_TH, DatabaseConfig.ServerName, 0, _tbl.Rows[0]);
                newMyThread.ALFCOPY2LIST = ALFCOPY2LIST;
                newMyThread.ALFCOPY2DATA = ALFCOPY2DATA;
                newMyThread.ThrowExceptionEvent += newMyThread_ThrowExceptionEvent;
                
                newMyThread.Start();
            }
            catch (Exception ex)
            {
                f9Message = "F9Thread_AMAD: " + ex.Message;
            }
            //
            
            f9Running = false;
        }

        void newMyThread_ThrowExceptionEvent(Exception ex)
        {
            ShowMainMessage(ex.Message);
        }


        private const string TABLE_NAME = "ALVT", ID_FIELD_ALVT = "MA_VT", NAME_FIELD_ALVT = "TEN_VT";
        private const string CHECK_FIELDS = "MA_VT", IDS_CHECK = "MA_VT", TYPE_CHECK = "01";//S Cách nhau bởi (;)
        private int total, index;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string[] check_field_list = { };
        private void F9Thread_ALVT()
        {
            return;
            // Đè biến
            DataTable _tbl = this._tbl2; // đổi biến cho nhanh khi copy code.
            try
            {
                f9Running = true;
                f9ErrorAll = "";

                if (_tbl == null)
                {
                    f9ErrorAll = V6Text.Text("INVALIDDATA");
                    goto End;
                }

                int stt = 0;
                total = _tbl.Rows.Count;
                var id_list = IDS_CHECK.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < total; i++)
                {
                    DataRow row = _tbl.Rows[i];
                    index = i;
                    stt++;
                    try
                    {
                        var check_ok = true;
                        foreach (string field in check_field_list)
                        {
                            if (row[field] == null || row[field] == DBNull.Value || row[field].ToString().Trim() == "")
                            {
                                check_ok = false;
                                break;
                            }
                        }
                        if (check_ok)
                        {
                            var dataDic = row.ToDataDictionary();
                            //fix dataDic id fields.
                            foreach (string id in id_list)
                            {
                                if (dataDic[id] is string)
                                {
                                    dataDic[id] = ObjectAndString.TrimSpecial(dataDic[id].ToString());
                                }
                            }
                            foreach (string id in check_field_list)
                            {
                                if (dataDic[id] is string)
                                {
                                    dataDic[id] = ObjectAndString.TrimSpecial(dataDic[id].ToString());
                                }
                            }

                            var ID0 = dataDic[id_list[0]].ToString().Trim();
                            var exist = false;
                            switch (TYPE_CHECK)
                            {
                                case "01":
                                    exist = _categories.IsExistOneCode_List(TABLE_NAME, id_list[0], ID0);
                                    break;
                            }

                            if (FilterControl.Check2) //Chỉ cập nhập mã mới.
                            {
                                if (!exist)
                                {
                                    if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                                    {
                                        var ma_vt_new = dataDic["MA_VT"].ToString().Trim();
                                        V6BusinessHelper.UpdateAlqddvt(ma_vt_new, ma_vt_new);
                                        remove_list_d.Add(row);
                                    }
                                    else
                                    {
                                        var s = string.Format("Dòng {0,3}-ID:{1} {2}", stt, ID0, V6Text.Text("ADD0"));
                                        f9Error += s;
                                        f9ErrorAll += s;
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                if (exist) //Xóa cũ thêm mới.
                                {
                                    var keys = new SortedDictionary<string, object>();
                                    foreach (string field in id_list)
                                    {
                                        keys.Add(field.ToUpper(), row[field]);
                                    }
                                    _categories.Delete(TABLE_NAME, keys);
                                }

                                if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                                {
                                    var ma_vt_new = dataDic["MA_VT"].ToString().Trim();
                                    V6BusinessHelper.UpdateAlqddvt(ma_vt_new, ma_vt_new);
                                    remove_list_d.Add(row);
                                }
                                else
                                {
                                    var s = string.Format("Dòng {0,3}-ID:{1} {2}", stt, ID0, V6Text.Text("ADD0"));
                                    f9Error += s;
                                    f9ErrorAll += s;
                                }
                            }
                        }
                        else
                        {
                            var s = string.Format(V6Text.Text("DONG0KOCODU1"), stt, CHECK_FIELDS);
                            f9Error += s;
                            f9ErrorAll += s;
                        }
                    }
                    catch (Exception ex)
                    {
                        f9Error += "Dòng " + stt + ": " + ex.Message;
                        f9ErrorAll += "Dòng " + stt + ": " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                f9Error += ex.Message;
                f9ErrorAll += ex.Message;
            }

        End: ;
        //f9Running = false; // false ở F9Thread_AMAD
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
                AM["IMTYPE"] = "O";
                AM["MA_CT"] = Invoice.Mact;
                //AM["MA_NX"] = "111";
                //AM["MA_NT"] = "VND";
                //AM["TY_GIA"] = 1;
                
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

                var t_tien2 = 0m;
                var t_gg = 0m;
                var t_ck = 0m;
                var t_thue = 0m;
                var t_vc = 0m;

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

                if (AM.ContainsKey("T_TIEN2"))
                {
                    t_tien2 = ObjectAndString.ObjectToDecimal(AM["T_TIEN2"]);
                }
                if (AM.ContainsKey("T_THUE"))
                {
                    t_thue = ObjectAndString.ObjectToDecimal(AM["T_THUE"]);
                }
                if (AM.ContainsKey("T_CK"))
                {
                    t_ck = ObjectAndString.ObjectToDecimal(AM["T_CK"]);
                }
                if (AM.ContainsKey("T_GG"))
                {
                    t_gg = ObjectAndString.ObjectToDecimal(AM["T_GG"]);
                }
                

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

                var t_tt_nt = t_tien_nt2 - t_gg_nt - t_ck_nt + t_thue_nt + t_vc_nt;
                AM["T_TT_NT"] = t_tt_nt;
                AM["T_TT"] = t_tien2 - t_gg - t_ck + t_thue + t_vc;

                return AM;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GET_AM_Data", ex);
                return null;
            }
        }

        private List<IDictionary<string, object>> GET_AD1_List(List<DataRow> dataRows, string sttRec)
        {
            var result = new List<IDictionary<string, object>>();
            for (int i = 0; i < dataRows.Count; i++)
            {
                IDictionary<string, object> one = dataRows[i].ToDataDictionary(sttRec);
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
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running || newMyThread._Status == Status.Running)
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
                    V6ControlFormHelper.ShowMainMessage("V6IMDATA2 F9 running: " + cError);
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
                V6ControlFormHelper.SetStatusText("V6IMDATA2 F9 finish " + newMyThread._Status + newMyThread._Message);
                V6ControlFormHelper.ShowMainMessage("V6IMDATA2 F9 finish! " + newMyThread._Status + newMyThread._Message);
                //V6ControlFormHelper.ShowInfoMessage("F9 finish: " + f9MessageAll, 500, this);
                if (f9MessageAll.Length > 0)
                {
                    Logger.WriteToLog(V6Login.ClientName + " " + GetType() + "F9 " + f9MessageAll);
                }
                f9Message = "";
                f9MessageAll = "";
            }
        }
        #endregion xử lý F9

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerAutoF9 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerAutoF9
            // 
            this.timerAutoF9.Enabled = true;
            this.timerAutoF9.Interval = 1000;
            this.timerAutoF9.Tick += new System.EventHandler(this.timerAutoF9_Tick);
            // 
            // V6IMDATA2TH1_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "V6IMDATA2TH1_Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private int _autoF9count = 0;
        private void timerAutoF9_Tick(object sender, EventArgs e)
        {
            try
            {
                _autoF9count++;
                if (InTimeAutoF9())
                {
                    _autoF9count = 0;
                    btnNhan.PerformClick();
                    XuLyF9();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GET_AM_Data", ex);
            }
        }

        private bool InTimeAutoF9()
        {
            if (!AUTOF9) return false;
            if (!FilterControl.ObjectDictionary.ContainsKey("AUTOF9TIME")) return false;
            int minute = ObjectAndString.ObjectToInt(FilterControl.ObjectDictionary["AUTOF9TIME"]);
            if (_autoF9count < minute * 60) return false;
            return true;
        }
    }
}

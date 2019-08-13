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
    public class XLSGL1_2_Control : XuLyBase
    {
        private const string ID_FIELD = "SO_CT", NAME_FIELD = "NGAY_CT";
        private DataTable _data;
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private string check = null;

        public XLSGL1_2_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
                if (string.IsNullOrEmpty(FilterControl.String1))
                {
                    return;
                }
                if (!File.Exists(FilterControl.String1))
                {
                    this.ShowMessage(V6Text.NotExist);
                    return;
                }

                _data = Excel_File.Sheet1ToDataTable(FilterControl.String1);
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
                        _data = Data_Table.ChuyenMaTiengViet(_data, from, to);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(V6Text.Text("NoFromTo"));
                    }
                }
                //FIX DATA
                if (!_data.Columns.Contains("TY_GIA"))
                {
                    _data.Columns.Add("TY_GIA", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_data, "TY_GIA", 1m);
                }
                if (!_data.Columns.Contains("TIEN_NT"))
                {
                    _data.Columns.Add("TIEN_NT", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_data, "TIEN_NT", 0m);
                }
                if (!_data.Columns.Contains("T_TIEN"))
                {
                    _data.Columns.Add("T_TIEN", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_data, "T_TIEN", 0m);
                    foreach (DataRow row in _data.Rows)
                    {
                        row["T_TIEN"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["TIEN_NT"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);
                    }
                }

                if (!_data.Columns.Contains("T_THUE_NT"))
                {
                    _data.Columns.Add("T_THUE_NT", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_data, "T_THUE_NT", 0m);
                }
                if (!_data.Columns.Contains("T_THUE"))
                {
                    _data.Columns.Add("T_THUE", typeof(decimal));
                    V6ControlFormHelper.UpdateDKlist(_data, "T_THUE", 0m);
                    foreach (DataRow row in _data.Rows)
                    {
                        row["T_THUE"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["T_THUE_NT"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);
                    }
                }
                if (!_data.Columns.Contains("TIEN_NT"))
                {
                    _data.Columns.Add("TIEN_NT", typeof(decimal));
                }
                if (!_data.Columns.Contains("TIEN"))
                {
                    _data.Columns.Add("TIEN", typeof(decimal));
                    foreach (DataRow row in _data.Rows)
                    {
                        row["TIEN"] =
                            V6BusinessHelper.Vround(
                                ObjectAndString.ObjectToDecimal(row["TIEN_NT"]) *
                                ObjectAndString.ObjectToDecimal(row["TY_GIA"]), V6Setting.RoundTien);
                    }
                }
                if (!_data.Columns.Contains("PS_NO_NT"))
                {
                    _data.Columns.Add("PS_NO_NT", typeof(decimal));
                    foreach (DataRow row in _data.Rows)
                    {
                        row["PS_NO_NT"] = ObjectAndString.ObjectToDecimal(row["TIEN_NT"]);
                    }
                }
                if (!_data.Columns.Contains("PS_CO_NT"))
                {
                    _data.Columns.Add("PS_CO_NT", typeof(decimal));
                    foreach (DataRow row in _data.Rows)
                    {
                        row["PS_CO_NT"] = ObjectAndString.ObjectToDecimal(row["TIEN_NT"]);
                    }
                }
                
                if (!_data.Columns.Contains("PS_NO"))
                {
                    _data.Columns.Add("PS_NO", typeof(decimal));
                }
                if (!_data.Columns.Contains("PS_CO"))
                {
                    _data.Columns.Add("PS_CO", typeof(decimal));
                }
                if (!_data.Columns.Contains("MA_KH_NO"))
                {
                    _data.Columns.Add("MA_KH_NO", typeof(string));
                    V6ControlFormHelper.UpdateDKlist(_data, "MA_KH_NO", "");
                }
                if (!_data.Columns.Contains("MA_KH_CO"))
                {
                    _data.Columns.Add("MA_KH_CO", typeof(string));
                    V6ControlFormHelper.UpdateDKlist(_data, "MA_KH_CO", "");
                }
                if (!_data.Columns.Contains("TK_I"))
                {
                    _data.Columns.Add("TK_I", typeof(string));
                    V6ControlFormHelper.UpdateDKlist(_data, "TK_I", "");
                }
                if (!_data.Columns.Contains("TK_NO"))
                {
                    _data.Columns.Add("TK_NO", typeof(string));
                    V6ControlFormHelper.UpdateDKlist(_data, "TK_NO", "");
                }
                if (!_data.Columns.Contains("TK_CO"))
                {
                    _data.Columns.Add("TK_CO", typeof(string));
                    V6ControlFormHelper.UpdateDKlist(_data, "TK_CO", "");
                }

                All_Objects["_data"] = _data;
                All_Objects["data"] = _data.Copy();
                InvokeFormEvent(FormDynamicEvent.DYNAMICFIXEXCEL);
                InvokeFormEvent("AFTERFIXDATA");
                //

                dataGridView1.DataSource = _data;
                
                //string mact = FilterControl.ObjectDictionary["MA_CT"].ToString().Trim();
                //var alim2xls = V6BusinessHelper.Select("ALIM2XLS", "top 1 *", "MA_CT='" + mact + "'").Data;
                //if (alim2xls != null && alim2xls.Rows.Count > 0)
                {
                    //var config_row = alim2xls.Rows[0];
                    var khoa = ObjectAndString.SplitString("MA_SONB,MA_KH_NO,MA_KH_CO,TK_NO,TK_CO,MA_NT,TY_GIA,TIEN_NT,TIEN");// ObjectAndString.SplitString(config_row["KHOA"].ToString().Trim());
                    var id_check = ObjectAndString.SplitString("MA_SONB,MA_KH_NO,MA_KH_CO,TK_NO,TK_CO,MA_NT");// ObjectAndString.SplitString(config_row["ID_CHECK"].ToString().Trim());
                    var lost_fields = "";
                    foreach (string field in khoa)
                    {
                        if (!_data.Columns.Contains(field))
                        {
                            check += string.Format("{0} {1}", V6Text.NoData, field);
                            lost_fields += ", " + field;
                        }
                    }
                    // Trim khoảng trắng thừa và ký tự đặc biệt trong mã.
                    foreach (DataRow row in _data.Rows)
                    {
                        foreach (string field in id_check)
                        {
                            if (_data.Columns.Contains(field))
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
                //else
                //{
                //    check += V6Text.NoDefine + " alim2xls";
                //}

                string[] data_fields = "MA_KH_NO,MA_KH_CO,TK_NO,TK_CO".Split(',');
                string[] check_fields = "MA_KH,MA_KH,TK,TK".Split(',');
                string[] check_tables = "ALKH,ALKH,ALTK,ALTK".Split(',');
                check += V6ControlFormHelper.CheckDataInGridView(dataGridView1, data_fields, check_fields, check_tables);

                if (!string.IsNullOrEmpty(check))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
            }
        }

        
        #region ==== Xử lý F9 ====
        private string _mactF9 = "";
        protected override void XuLyF9()
        {
            try
            {
                if (!string.IsNullOrEmpty(check))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check);
                    return;
                }
                if (_data != null)
                {
                    if (_data.Columns.Contains(ID_FIELD) && _data.Columns.Contains(NAME_FIELD))
                    {
                        _mactF9 = FilterControl.ObjectDictionary["MA_CT"].ToString().Trim();
                        LockButtons();
                        chkAutoSoCt_Checked = FilterControl.Check3;

                        Timer timerF9 = new Timer { Interval = 1000 };
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
        //V6Invoice11 Invoice = new V6Invoice11();
        private IDictionary<string, object> AM_DATA;
        private bool chkAutoSoCt_Checked = false;
        private void F9Thread()
        {
            try
            {
                V6Invoice11 Invoice = new V6Invoice11(_mactF9);
                f9Running = true;
                f9MessageAll = "";
                string makho = "";
                string madvcs = "";

                //Gom chi tiet theo SO_CT va NGAY_CT
                Dictionary<string, List<DataRow>> data_dictionary = new Dictionary<string, List<DataRow>>();
                DateTime? dateMin = null, dateMax = null;
                foreach (DataRow row in _data.Rows)
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
                    //makho = row["MA_KHO_I"].ToString().Trim().ToUpper();
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
                            data_dictionary.Add(key, new List<DataRow> { row });
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
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_" + _mactF9 + "_DELETE_ALL", plist);
                }

                //Xử lý từng nhóm dữ liệu
                foreach (KeyValuePair<string, List<DataRow>> item in data_dictionary)
                {
                    var data_rows = item.Value;
                    try
                    {
                        AM_DATA = GET_AM_Data(data_rows, "TIEN_NT,TIEN", "", _mactF9);

                        var sttRec = V6BusinessHelper.GetNewSttRec(Invoice.Mact);
                        if (chkAutoSoCt_Checked) // Tự động tạo số chứng từ.
                        {
                            string ma_sonb;
                            DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(AM_DATA["NGAY_CT"]);
                            var so_ct = V6BusinessHelper.GetNewSoCt_date(Invoice.Mact, ngay_ct, "1", madvcs, makho, sttRec, V6Login.UserId, out ma_sonb);
                            AM_DATA["SO_CT"] = so_ct;
                            AM_DATA["MA_SONB"] = ma_sonb;
                        }
                        AM_DATA["STT_REC"] = sttRec;
                        var AD1_List = GET_AD1_List(data_rows, sttRec, _mactF9);
                        var AD2_List = GET_AD2_List(data_rows, sttRec, _mactF9);

                        All_Objects["AM"] = AM_DATA;
                        All_Objects["AD"] = AD1_List;
                        All_Objects["AD2"] = AD2_List;
                        InvokeFormEvent("BEFOREINSERT");
                        if (Invoice.InsertInvoice(AM_DATA, AD1_List, AD2_List))
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


        private IDictionary<string, object> GET_AM_Data(List<DataRow> dataRows, string sumColumns, string maxColumns, string maCT)
        {
            try
            {
                //Tính sum max
                sumColumns = "," + sumColumns.ToUpper() + ",";
                maxColumns = "," + maxColumns.ToUpper() + ",";
                var am_row = _data.NewRow();
                foreach (DataRow row in dataRows)
                {
                    foreach (DataColumn column in _data.Columns)
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
                IDictionary<string, object> AM = am_row.ToDataDictionary();
                AM["IMTYPE"] = "X";
                AM["MA_CT"] = maCT;
                //AM["MA_NX"] = "111";
                //AM["MA_NT"] = "VND";
                //AM["TY_GIA"] = 1;
                AM["KIEU_POST"] = "2";


                var t_ps_no_nt = 0m;
                var t_ps_co_nt = 0m;
                var t_gg_nt = 0m;
                var t_ck_nt = 0m;
                var t_vc_nt = 0m;
                var ty_gia = 1m;
                var t_ps_no = 0m;
                var t_ps_co = 0m;
                
                if (AM.ContainsKey("TY_GIA"))
                {
                    ty_gia = ObjectAndString.ObjectToDecimal(AM["TY_GIA"]);
                }
                
                if (AM.ContainsKey("TIEN_NT"))
                {
                    AM["T_PS_NO_NT"] = AM["TIEN_NT"];
                    t_ps_no_nt = ObjectAndString.ObjectToDecimal(AM["T_PS_NO_NT"]);
                }
                if (AM.ContainsKey("TIEN"))
                {
                    AM["T_PS_NO"] = AM["TIEN"];
                    t_ps_no = ObjectAndString.ObjectToDecimal(AM["T_PS_NO"]);
                }
                
                if (!AM.ContainsKey("DIEN_GIAI")) AM["DIEN_GIAI"] = "";
                if (!AM.ContainsKey("NGAY_LCT")) AM["NGAY_LCT"] = AM["NGAY_CT"];
                if (!AM.ContainsKey("T_PS_NO")) AM["T_PS_NO"] = 0;
                
                
                AM["T_PS_NO_NT"] = t_ps_no_nt;
                AM["T_PS_CO_NT"] = t_ps_no_nt;
                AM["T_PS_NO"] = t_ps_no;
                AM["T_PS_CO"] = t_ps_no;

                return AM;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GET_AM_Data", ex);
                return null;
            }
        }

        private List<IDictionary<string, object>> GET_AD1_List(List<DataRow> dataRows, string sttRec, string maCT)
        {
            var result = new List<IDictionary<string, object>>();
            for (int i = 0; i < dataRows.Count; i++)
            {
                var one1 = dataRows[i].ToDataDictionary(sttRec);
                var one2 = dataRows[i].ToDataDictionary(sttRec);
                one1["MA_CT"] = maCT;
                one2["MA_CT"] = maCT;
                one1["STT_REC0"] = ("00000" + (i*2 + 1)).Right(5);
                one2["STT_REC0"] = ("00000" + (i*2 + 2)).Right(5);
                one1["NH_DK"] = "" + (i+1);
                one2["NH_DK"] = "" + (i+1);
                one1["TK_I"] = one1["TK_NO"];
                one2["TK_I"] = one2["TK_CO"];
                one1["MA_KH_I"] = one1["MA_KH_NO"];
                one2["MA_KH_I"] = one2["MA_KH_CO"];

                if (one1.ContainsKey("MA_NT"))
                {
                    var one_maNt = one1["MA_NT"].ToString().Trim();
                    
                    if (one_maNt == V6Options.M_MA_NT0)
                    {
                        if (one1.ContainsKey("PS_NO_NT"))
                        {
                            one1["PS_NO_NT"] = one1["TIEN_NT"];
                            one1["PS_NO"] = one1["TIEN_NT"];
                            one1["PS_CO_NT"] = 0m;
                            one1["PS_CO"] = 0m;

                            one2["PS_CO_NT"] = one2["TIEN_NT"];
                            one2["PS_CO"] = one2["TIEN_NT"];
                            one2["PS_NO_NT"] = 0m;
                            one2["PS_NO"] = 0m;
                        }
                    }
                    else
                    {
                        one1["PS_NO_NT"] = one1["TIEN_NT"];
                        one1["PS_CO_NT"] = 0m;
                        one2["PS_CO_NT"] = one2["TIEN_NT"];
                        one2["PS_NO_NT"] = 0m;
                        

                        if (one1.ContainsKey("TY_GIA"))
                        {
                            var one_tygia = ObjectAndString.ObjectToDecimal(one1["TY_GIA"]);
                            if (one_tygia == 0) one_tygia = 1;

                            if (one1.ContainsKey("TIEN_NT"))
                            {
                                one1["PS_NO"] = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one1["TIEN_NT"]) * one_tygia, V6Setting.RoundTien);
                                one1["PS_CO"] = 0m;

                                one2["PS_CO"] = V6BusinessHelper.Vround(ObjectAndString.ObjectToDecimal(one2["TIEN_NT"]) * one_tygia, V6Setting.RoundTien);
                                one2["PS_NO"] = 0m;
                            }
                        }
                    }
                }

                result.Add(one1);
                result.Add(one2);
            }
            return result;
        }
        
        private List<IDictionary<string, object>> GET_AD2_List(List<DataRow> dataRows, string sttRec, string maCT)
        {
            var result = new List<IDictionary<string, object>>();
            for (int i = 0; i < dataRows.Count; i++)
            {
                var one = dataRows[i].ToDataDictionary(sttRec);

                if (ObjectAndString.ObjectToDecimal(one["T_TIEN_NT"]) + ObjectAndString.ObjectToDecimal(one["T_THUE_NT"]) == 0)
                {
                    continue;
                }

                one["MA_CT"] = maCT;
                one["STT_REC0"] = ("00000" + (i + 1)).Right(5);
                
                one["SO_CT"] = AM_DATA["SO_CT"];
                one["NGAY_CT"] = AM_DATA["NGAY_CT"];
                one["NGAY_LCT"] = AM_DATA["NGAY_LCT"];
                

                result.Add(one);
            }
            return result;
        }

        private List<IDictionary<string, object>> GET_AD2_List0(List<DataRow> dataRows, string sttRec, string maCT)
        {
            var result = new List<IDictionary<string, object>>();
            var newRow = new SortedDictionary<string, object>();
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
            newRow["TEN_VT"] = AM_DATA["TEN_VT"];
            //Ten_vt,so_luong,gia,t_tien
            newRow["T_TIEN"] = AM_DATA["T_TIEN"];
            newRow["T_TIEN_NT"] = AM_DATA["T_TIEN_NT"];
            newRow["T_THUE"] = AM_DATA["T_THUE"];
            newRow["T_THUE_NT"] = AM_DATA["T_THUE_NT"];
            newRow["MA_THUE"] = AM_DATA["MA_THUE"];
            newRow["THUE_SUAT"] = AM_DATA["THUE_SUAT"];
            newRow["TK_THUE_NO"] = AM_DATA["TK_THUE_NO"];
            newRow["TK_DU"] = AM_DATA["TK_DU"];
            newRow["MAU_BC"] = AM_DATA["MAU_BC"];

            newRow["MA_CT"] = maCT;
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
                    _data.Rows.Remove(remove_list_d[0]);
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
                    _data.Rows.Remove(remove_list_d[0]);
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
                    Logger.WriteToLog(V6Login.ClientName + " " + GetType() + "XLS_" + _mactF9 + " F9 " + f9MessageAll);
                }
                f9Message = "";
                f9MessageAll = "";
            }
        }
        #endregion xử lý F9

    }
}

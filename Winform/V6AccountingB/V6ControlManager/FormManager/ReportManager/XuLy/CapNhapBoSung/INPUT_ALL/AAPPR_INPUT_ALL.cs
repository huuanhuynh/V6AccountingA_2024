using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_INPUT_ALL : XuLyBase
    {
        public AAPPR_INPUT_ALL(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            dataGridView1.Control_S = true;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(string.Format("F4: {0}. Ctrl+F4: {2}. F9: {1}.", V6Text.Text("BOSUNGTHONGTIN"), V6Text.Text("BOSUNGTTNCT"), V6Text.Text("SUACHITIET")));
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            ResetInvoice();
            base.MakeReport2();
        }

        private void ResetInvoice()
        {
            try
            {
                string ma_ct = FilterControl.ObjectDictionary["MA_CT"].ToString();
                switch (ma_ct)
                {
                    case "SOA":
                        invoice = new V6Invoice81();
                        break;
                    case "POA":
                        invoice = new V6Invoice71();
                        break;
                    case "POH":
                        invoice = new V6Invoice92();
                        break;
                    case "SOH":
                        invoice = new V6Invoice91();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ResetInvoice", ex);
            }
        }

        private IDictionary<string, string> GetExtraInfor(string infoText)
        {
            var _extraInfor = new SortedDictionary<string, string>();
            _extraInfor.AddRange(ObjectAndString.StringToStringDictionary(infoText));
            return _extraInfor;
        }

        public override void FormatGridViewExtern()
        {
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, invoice.AlctConfig.GRDS_AM, invoice.AlctConfig.GRDF_AM,
                V6Setting.IsVietnamese ? invoice.AlctConfig.GRDHV_AM : invoice.AlctConfig.GRDHE_AM);
        }

        protected override void FormatGridView2()
        {
            V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, invoice.AlctConfig.GRDS_AD, invoice.AlctConfig.GRDF_AD,
                V6Setting.IsVietnamese ? invoice.AlctConfig.GRDHV_AD : invoice.AlctConfig.GRDHE_AD);
        }

        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        //private string TxtMa_bp_Text, TxtMa_nvien_Text;
        private AAPPR_INPUT_ALL_F9 form_f9;

        protected override void XuLyF9()
        {
            try
            {
                if (dataGridView1.DataSource == null || dataGridView1.RowCount == 0)
                {
                    ShowMainMessage(V6Text.NoData);
                    return;
                }
                string extra_infor = "";
                AlctConfig alctConfig = ConfigManager.GetAlctConfig(FilterControl.ObjectDictionary["MA_CT"].ToString());
                var extra_infors = GetExtraInfor(alctConfig.GetString("EXTRA_INFOR"));
                if (extra_infors.ContainsKey("FIELDS_F9")) extra_infor = extra_infors["FIELDS_F9"];
                form_f9 = new AAPPR_INPUT_ALL_F9(extra_infor);
                All_Objects["FORM_F9"] = form_f9;
                All_Objects["MA_CT"] = FilterControl.ObjectDictionary["MA_CT"];
                InvokeFormEvent(FormDynamicEvent.F9);
                if (form_f9.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                //TxtMa_bp_Text = form_f9.TxtMa_bp.Text.Trim();
                //TxtMa_nvien_Text = form_f9.TxtMa_nvien.Text.Trim();
                //if (TxtMa_bp_Text == "" && TxtMa_nvien_Text == "")
                //{
                //    return;
                //}

                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                Thread t = new Thread(F9Thread);
                t.SetApartmentState(ApartmentState.STA);
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                t.IsBackground = true;
                t.Start();
                tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }
        private void F9Thread()
        {
            f9Running = true;
            f9ErrorAll = "";

            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //var rowData = row.ToDataDictionary();
                        var am = new SortedDictionary<string, object>();
                        string c_sttRec = row.Cells["STT_REC"].Value.ToString().Trim();
                        _message = "F9: " + c_sttRec;
                        //if (TxtMa_bp_Text != "") am["MA_BP"] = TxtMa_bp_Text;
                        //if (TxtMa_nvien_Text != "") am["MA_NVIEN"] = TxtMa_nvien_Text;
                        // thêm giá trị động vào am data.
                        string LFields = "", LValues = "", LTypes = "";
                        foreach (KeyValuePair<string, AAPPR_INPUT_ALL_F9.ListValueItem> item in form_f9.ListValue)
                        {
                            am[item.Key] = item.Value.Value;
                            LFields += ";" + item.Value.Field;
                            LValues += ";" + ObjectAndString.ObjectToString(item.Value.Value, "yyyyMMdd");
                            LTypes += ";" + item.Value.type;
                        }
                        if (LFields.Length > 0) LFields = LFields.Substring(1);
                        if (LValues.Length > 0) LValues = LValues.Substring(1);
                        if (LTypes.Length > 0) LTypes = LTypes.Substring(1);
                        
                        //var keys = new SortedDictionary<string, object> {{"Stt_rec", c_sttRec}};
                        //var result = V6BusinessHelper.UpdateSimple(invoice.AM_TableName, am, keys);
                        //if (result == 1)
                        {
                            SqlParameter[] plist =
                            {
                                new SqlParameter("@Stt_rec", c_sttRec),
                                new SqlParameter("@Ma_ct", FilterControl.ObjectDictionary["MA_CT"]),
                                new SqlParameter("@Set_ma_xuly", FilterControl.Kieu_post),
                                new SqlParameter("@LFields", LFields),//Giá trị các trường động cách nhau bằng ;
                                new SqlParameter("@LValues", LValues),
                                new SqlParameter("@LTypes", LTypes),
                                new SqlParameter("@user_id", V6Login.UserId),
                            };
                            V6BusinessHelper.ExecuteProcedure("AAPPR_INPUT_ALLF9", plist);
                        }
                        //else
                        //{
                        //    f9Error += "Update: " + result;
                        //    f9ErrorAll += "Update: " + result;
                        //}

                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }
            }
            f9Running = false;
        }
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running " + _message
                    + (cError.Length > 0 ? " Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F9 " + V6Text.Finish);
            }
        }
        #endregion xulyF9

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                XuLySuaThongTinChungTuChiTiet();
                return;
            }

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var currentRow = dataGridView1.CurrentRow;
                    if (dataGridView1.Columns.Contains("Stt_rec") && dataGridView1.Columns.Contains("Ma_ct"))
                    {
                        var selectedMaCt = currentRow.Cells
                            ["Ma_ct"].Value.ToString().Trim();
                        var selectedSttRec = currentRow.Cells
                            ["Stt_rec"].Value.ToString().Trim();

                        if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt))
                        {
                            var plist = new List<SqlParameter>
                            {
                                new SqlParameter("@stt_rec", selectedSttRec),
                                new SqlParameter("@maCT", selectedMaCt)
                            };
                            var alctRow = V6BusinessHelper.Select("Alct", "m_phdbf,m_ctdbf",
                                "ma_CT = '" + selectedMaCt + "'").Data.Rows[0];
                            var amName = alctRow["m_phdbf"].ToString().Trim();

                            if (amName != "")
                            {
                                var am = V6BusinessHelper.Select(amName, "*", "STT_REC=@stt_rec and MA_CT=@maCT",
                                    "", "", plist.ToArray()).Data;

                                var fText = V6Text.Text("GCCT");
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                string extra_infor = "";
                                if (EXTRA_INFOR.ContainsKey("FIELDS_F4")) extra_infor = EXTRA_INFOR["FIELDS_F4"];
                                var hoaDonForm = new AAPPR_INPUT_ALL_F4(selectedSttRec, am.Rows[0], extra_infor);

                                f.Controls.Add(hoaDonForm);
                                hoaDonForm.Disposed += delegate
                                {
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
                                SetStatus2Text();
                            }
                        }
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.EditDenied);
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }

        private void XuLySuaThongTinChungTuChiTiet()
        {
            try
            {
                // Sua nhieu dong
                var cRow = dataGridView1.CurrentRow;
                if (cRow == null) return;

                string showFields = invoice.EXTRA_INFOR.ContainsKey("ADFIELDS") ? invoice.EXTRA_INFOR["ADFIELDS"] : "";
                string keyFields = "UID"; // "STT_REC,STT_REC0"
                string tableName = invoice.AD_TableName;// invoice.Mact + "_REPLACE";
                var f = new DataEditorForm(this, dataGridView2.DataSource, tableName, showFields, keyFields, V6Text.Edit + " " + V6TableHelper.V6TableCaption(tableName, V6Setting.Language), false, false, true, true);
                All_Objects["dataGridView"] = f.DataGridView;
                InvokeFormEvent(FormDynamicEvent.SUANHIEUDONG);
                f.ShowDialog(this);
                if (f.HaveChange)
                {
                    //this.ShowMessage("HaveChange");
                    _sttRec = cRow.Cells["STT_REC"].Value.ToString();
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Stt_rec", _sttRec),
                        new SqlParameter("@Ma_ct", "POH"),
                        new SqlParameter("@user_id", V6Login.UserId),
                        new SqlParameter("@Set_ma_xuly", FilterControl.Kieu_post),
                    };
                    V6BusinessHelper.ExecuteProcedure("AAPPR_INPUT_ALL_UPDATE_AD", plist);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLySuaThongTinChungTuChiTiet", ex);
            }
        }

        V6InvoiceBase invoice = null;
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAD(sttRec);
                dataGridView2.DataSource = data;
                _tbl2 = data;
                if (data == null)
                {
                    this.WriteToLog(GetType() + ".ViewDetails", "data is null.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }
    }
}

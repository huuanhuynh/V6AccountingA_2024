using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_SOA1 : XuLyBase
    {
        public AAPPR_SOA1(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            dataGridView1.Control_S = true;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F4: Bổ sung thông tin., F9. Bổ sung thông tin nhiều chứng từ ");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }
        
        #region ==== Xử lý F8 ====

        private bool F8Running;
        private string F8Error = "";
        private string F8ErrorAll = "";
        protected override void XuLyF8()
        {
            try
            {
                V6ControlFormHelper.ShowMessage("Chưa xử lý.");
                return;
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
        private void F8Thread()
        {
            F8Running = true;
            F8ErrorAll = "";

            int i = 0;
            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //string soct = row.Cells["So_ct"].Value.ToString();
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@Set_kieu_post", FilterControl.Kieu_post),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "F8", plist);
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {

                    F8Error += ex.Message;
                    F8ErrorAll += ex.Message;
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
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F8 finish "
                    + (F8ErrorAll.Length > 0 ? "Error: " : "")
                    + F8ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F8 Xử lý xong!");
            }
        }
        #endregion xulyF8


        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string TxtMa_bp_Text, TxtMa_nvien_Text;

        protected override void XuLyF9()
        {
            try
            {
                AAPPR_SOA1_F9 form = new AAPPR_SOA1_F9();
                if (form.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                TxtMa_bp_Text = form.TxtMa_bp.Text.Trim();
                TxtMa_nvien_Text = form.TxtMa_nvien.Text.Trim();
                if (TxtMa_bp_Text == "" && TxtMa_nvien_Text == "")
                {
                    return;
                }

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
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
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
                        var rowData = row.ToDataDictionary();
                        var am = new SortedDictionary<string, object>();
                        string c_sttRec = row.Cells["STT_REC"].Value.ToString().Trim();
                        _message = "F9: " + c_sttRec;
                        if (TxtMa_bp_Text != "") am["MA_BP"] = TxtMa_bp_Text;
                        if (TxtMa_nvien_Text != "") am["MA_NVIEN"] = TxtMa_nvien_Text;

                        var keys = new SortedDictionary<string, object> {{"Stt_rec", c_sttRec}};
                        var result = V6BusinessHelper.UpdateSimple(V6TableName.Am81, am, keys);
                        if (result == 1)
                        {
                            SqlParameter[] plist =
                            {
                                new SqlParameter("@Stt_rec", c_sttRec),
                                new SqlParameter("@Ma_ct", "SOA"),
                            };
                            V6BusinessHelper.ExecuteProcedure("AAPPR_SOA1_UPDATE", plist);
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

                V6ControlFormHelper.ShowMainMessage("F9 Xử lý xong!");
            }
        }
        #endregion xulyF9

        protected override void XuLyBoSungThongTinChungTuF4()
        {
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

                                var fText = "Ghi chú chứng từ";
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var hoaDonForm = new AAPPR_SOA1_F4(selectedSttRec, am.Rows[0]);

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
                        this.ShowWarningMessage("Không được phép sửa chi tiết!");
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyBase XuLyBoSungThongTinChungTuF4:\n" + ex.Message);
            }
        }

        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAd81(sttRec);
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

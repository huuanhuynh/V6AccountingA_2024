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
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_SOA_IN3 : XuLyBase
    {
        public AAPPR_SOA_IN3(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F4: Cho phép in lại (kẹt giấy), F10: Cho phép in lại (kẹt giấy) hàng loạt");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }
        
        #region ==== Xử lý F10 ====

        private bool f10Running;
        private string f10Message = "";
        private string f10ErrorAll = "";

        protected override void XuLyF10()
        {
            try
            {
                var fText = "Cho phép in lại (kẹt giấy)";
                var f = new V6Form
                {
                    Text = fText,
                    AutoSize = true,
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    Height = 200, Width = 300,
                    ShowInTaskbar = false, MinimizeBox = false, MaximizeBox = false
                };

                var hoaDonForm = new AAPPR_SOA_IN3F10();
                
                hoaDonForm.OkEvent += (value,e) =>
                {
                    //Check value = checked return
                    if (value is bool && (bool) value) return;
                    //FilterControl.Check1 = checked ;

                    Timer t10 = new Timer();
                    t10.Interval = 500;
                    t10.Tick += tF10_Tick;
                    CheckForIllegalCrossThreadCalls = false;
                    Thread t = new Thread(F10Thread);
                    t.SetApartmentState(ApartmentState.STA);
                    t.IsBackground = true;
                    t.Start();
                    t10.Start();
                };
                f.Controls.Add(hoaDonForm);
                hoaDonForm.Disposed += delegate
                {
                    f.Dispose();
                };

                f.ShowDialog(this);
                SetStatus2Text();

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF10", ex);
            }
        }
        private void F10Thread()
        {
            f10Running = true;
            f10ErrorAll = "";
            var program = _program + "F10";// Invoice.PrintReportProcedure;

            int i = 0, count = 0;
            while (i < dataGridView1.Rows.Count)
            {
                var row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        
                        
                        var sttRec = (row.Cells["Stt_rec"].Value ?? "").ToString();

                        var keys = new SortedDictionary<string, object> {{"STT_REC", sttRec}};
                        var data = new SortedDictionary<string, object> {{"SL_IN", 0}};
                        if (V6BusinessHelper.UpdateSimple("AM81", data, keys) > 0)
                        {
                            count++;
                            try
                            {
                                SqlParameter[] plist =
                                {
                                    new SqlParameter("@STT_REC", sttRec),
                                    new SqlParameter("@User_id", V6Login.UserId),
                                };
                                V6BusinessHelper.ExecuteProcedureNoneQuery(program, plist);
                            }
                            catch (Exception ex)
                            {
                                f10Message += "Execute Proc error: " ;
                                f10ErrorAll += "Execute Proc error: " + ex.Message;
                            }
                        }
                        else
                        {
                            f10Message += "Update error ";
                        }
                    }
                }
                catch (Exception ex)
                {
                    f10ErrorAll += ex.Message;
                }
            }
            f10Running = false;
        }

        void tF10_Tick(object sender, EventArgs e)
        {
            if (f10Running)
            {
                var cMessage = f10Message;
                f10Message = f10Message.Substring(cMessage.Length);
                V6ControlFormHelper.SetStatusText("F10 running " + cMessage);
            }
            else
            {
                ((Timer)sender).Stop();
                FilterControl.Check1 = true ;
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F10 Xử lý xong!");
                btnNhan.PerformClick();
            }
        }
        #endregion xulyF10

        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAd81(sttRec);
                dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }

        
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

                                var fText = "Cho phép in lại";
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle,
                                    ShowInTaskbar = false, MinimizeBox = false, MaximizeBox = false
                                };

                                var hoaDonForm = new AAPPR_SOA_IN3F3(selectedSttRec, am.Rows[0]);
                                hoaDonForm.UpdateSuccessEvent += data =>
                                {
                                    currentRow.Cells["SL_IN"].Value = data["SL_IN"];
                                    //V6ControlFormHelper.UpdateGridViewRow(currentRow, data);
                                };
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
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }
    }
}

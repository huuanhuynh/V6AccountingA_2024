using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_CA1_IN1 : XuLyBase
    {
        public AAPPR_CA1_IN1(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(" F9: In ");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        #region ==== Xử lý F9 ====

        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string _oldDefaultPrinter, _PrinterName;
        private int _PrintCopies;
        private bool printting;
        protected override void XuLyF9()
        {
            try
            {
                if (FilterControl.Check1)
                {
                    if (this.ShowConfirmMessage(V6Text.Text("ASKINLIENTUC")) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();
                var printerst = V6ControlFormHelper.ChoosePrinter(this, _oldDefaultPrinter);
                if (printerst != null)
                {
                    _PrinterName = printerst.PrinterName;
                    _PrintCopies = printerst.Copies;
                    V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                    printting = true;
                    //Print(_PrinterName);

                    Timer tF9 = new Timer();
                    tF9.Interval = 500;
                    tF9.Tick += tF9_Tick;
                    CheckForIllegalCrossThreadCalls = false;
                    remove_list_g = new List<DataGridViewRow>();
                    Thread t = new Thread(F9Thread);
                    t.SetApartmentState(ApartmentState.STA);
                    t.IsBackground = true;
                    t.Start();
                    tF9.Start();
                }
                else
                {
                    printting = false;
                }
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
            var Invoice = new V6Invoice51();
            var program = _program + "F9";// Invoice.PrintReportProcedure;
            //var repFile = Invoice.Alct["FORM"].ToString().Trim();
            var repTitle = Invoice.Alct["TIEU_DE_CT"].ToString().Trim();
            var repTitle2 = Invoice.Alct["TIEU_DE2"].ToString().Trim();
            
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {

                        var sttRec = (row.Cells["Stt_rec"].Value ?? "").ToString();

                        var c = new InChungTuViewBase(Invoice, program, program, _reportFile, repTitle, repTitle2,
                            "", "", "", sttRec);
                        c.PrintMode = FilterControl.Check1 ? V6PrintMode.AutoPrint : V6PrintMode.DoNoThing;
                        c.PrinterName = _PrinterName;
                        c.PrintCopies = _PrintCopies;
                        c.Report_Stt_rec = sttRec;
                        c.TTT = ObjectAndString.ObjectToDecimal(row.Cells["T_TT"].Value);
                        c.TTT_NT = ObjectAndString.ObjectToDecimal(row.Cells["T_TT_NT"].Value);
                        c.MA_NT = row.Cells["MA_NT"].Value.ToString().Trim();
                        c.Dock = DockStyle.Fill;
                        //c.xong = false;
                        //c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                        //{
                        //    if (hoadon_nd51 == 1)
                        //    {
                        //        var sql = "Update Am76 Set Sl_in = Sl_in+1 Where Stt_rec=@p";
                        //        SqlConnect.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter("@p", stt_rec));
                        //    }
                        //    sender.Dispose();
                        //};

                        var f = new V6Form();
                        f.StartPosition = FormStartPosition.CenterScreen;
                        f.WindowState = FormWindowState.Maximized;
                        f.Text = V6Text.PrintCA1;
                        f.Controls.Add(c);
                        //c.MakeReport(auto, _printerName, (int)numSoLien.Value);

                        c.Disposed += delegate
                        {
                            //c.xong = true;
                            f.Close();
                        };

                        f.ShowDialog(this);
                        
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
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;

                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length>0?"Error: ":"")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.Image = btnNhanImage;
                btnNhan.PerformClick();
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F9 " + V6Text.Finish);
            }
        }
        #endregion xulyF9

        #region ==== Xử lý F10 ====

        private bool f10Running;
        private string f10Message = "";
        private string f10ErrorAll = "";

        protected override void XuLyF10()
        {
            try
            {
                var fText = "Nhập số ct bắt đầu";
                var f = new V6Form
                {
                    Text = fText,
                    AutoSize = true,
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    Height = 200, Width = 300,
                    ShowInTaskbar = false, MinimizeBox = false, MaximizeBox = false
                };

                var hoaDonForm = new AAPPR_SOA_F10();
                var so_ctx = hoaDonForm.So_ctx.Trim();
                if (so_ctx == "")
                {
                    if (!string.IsNullOrEmpty(so_ctx_temp))
                    {
                        try
                        {
                            var int_so_ctx = int.Parse(so_ctx_temp);
                            if (int_so_ctx > 0)
                            {
                                var s0 = "".PadRight(so_ctx_temp.Length, '0');
                                var s = (s0 + (int_so_ctx + 1)).Right(so_ctx_temp.Length);
                                hoaDonForm.So_ctx = s;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.WriteExLog(GetType() + ".XuLyF10", ex, ProductName);
                        }
                    }
                }

                hoaDonForm.OkEvent += (value,e) =>
                {
                    so_ctx_temp = value.ToString().Trim();
                    if (string.IsNullOrEmpty(so_ctx_temp))
                    {
                        this.ShowWarningMessage("Không được bỏ trống số ct bắt đầu!");
                        return;
                    }

                    FilterControl.String1 = so_ctx_temp;

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
                    f.Close();
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
                        if (count > 0)
                        {
                            //Cộng
                            var int_so_ctx = int.Parse(so_ctx_temp);
                            if (int_so_ctx > 0)
                            {
                                var s0 = "".PadRight(so_ctx_temp.Length, '0');
                                var s = (s0 + (int_so_ctx + 1)).Right(so_ctx_temp.Length);
                                so_ctx_temp = s;
                            }
                        }

                        f10Message += so_ctx_temp;
                        var sttRec = (row.Cells["Stt_rec"].Value ?? "").ToString();

                        var keys = new SortedDictionary<string, object> {{"STT_REC", sttRec}};
                        var data = new SortedDictionary<string, object> {{"SO_CTX", so_ctx_temp}};
                        if (V6BusinessHelper.UpdateSimple("AM81", data, keys) > 0)
                        {
                            count++;
                            row.Cells["SO_CTX"].Value = so_ctx_temp;
                            try
                            {
                                SqlParameter[] plist =
                                {
                                    new SqlParameter("@STT_REC", sttRec),
                                    new SqlParameter("@SO_CTX", so_ctx_temp),
                                    new SqlParameter("@User_id", V6Login.UserId),
                                };
                                V6BusinessHelper.ExecuteProcedureNoneQuery(program, plist);
                            }
                            catch (Exception ex)
                            {
                                f10Message += "Execute Proc error: " + so_ctx_temp;
                                f10ErrorAll += "Execute Proc error: " + ex.Message;
                            }
                            
                        }
                        else
                        {
                            f10Message += "Update error " + so_ctx_temp;
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
                FilterControl.String1 = so_ctx_temp;
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                SetStatusText("F10 " + V6Text.Finish);
            }
        }
        #endregion xulyF10

        V6Invoice51 invoice = new V6Invoice51();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAD(sttRec);
                dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_CA1 ViewDetails: " + ex.Message);
            }
        }

        private string so_ctx_temp;
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

                                var fText = V6Text.Text("GCCT");
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle,
                                    ShowInTaskbar = false, MinimizeBox = false, MaximizeBox = false
                                };

                                var hoaDonForm = new AAPPR_SOA_F4(selectedSttRec, am.Rows[0], amName);
                                hoaDonForm.txtGhiChu02.Enabled = false;

                                var so_ctx = hoaDonForm.So_ctx.Trim();
                                if (so_ctx == "")
                                {
                                    if (so_ctx_temp != null)
                                    {
                                        try
                                        {
                                            var int_so_ctx = int.Parse(so_ctx_temp);
                                            if (int_so_ctx > 0)
                                            {
                                                var s0 = "".PadRight(so_ctx_temp.Length, '0');
                                                var s = (s0 + (int_so_ctx + 1)).Right(so_ctx_temp.Length);
                                                hoaDonForm.So_ctx = s;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, ProductName);
                                        }
                                    }
                                }

                                hoaDonForm.UpdateSuccessEvent += data =>
                                {
                                    currentRow.Cells["GHI_CHU01"].Value = data["GHI_CHU01"];
                                    currentRow.Cells["GHI_CHU02"].Value = data["GHI_CHU02"];

                                    currentRow.Cells["SO_CTX"].Value = data["SO_CTX"];
                                    so_ctx_temp = data["SO_CTX"].ToString().Trim();
                                    FilterControl.String1 = so_ctx_temp;
                                };
                                f.Controls.Add(hoaDonForm);
                                hoaDonForm.Disposed += delegate
                                {
                                    f.Close();
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
    }
}

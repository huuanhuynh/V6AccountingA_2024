using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AAPPR_SOA_IN2 : XuLyBase
    {
        public AAPPR_SOA_IN2(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Phím cách chọn,F4: Bổ sung thông tin, F9: In phiếu xuất kho");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
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
                        var selectedMaCt = currentRow.Cells["Ma_ct"].Value.ToString().Trim();
                        var selectedSttRec = currentRow.Cells["Stt_rec"].Value.ToString().Trim();

                        if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt))
                        {
                            var plist = new List<SqlParameter>
                            {
                                new SqlParameter("@stt_rec", selectedSttRec),
                                new SqlParameter("@maCT", selectedMaCt)
                            };
                            var alctRow = V6BusinessHelper.Select("Alct", "m_phdbf,m_ctdbf", "ma_CT = '" + selectedMaCt + "'").Data.Rows[0];
                            
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

                                var hoaDonForm = new AAPPR_SOA1_F4(selectedSttRec, am.Rows[0], EXTRA_INFOR["FIELDS_F4"]);

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
                _oldDefaultPrinter = V6Tools.PrinterStatus.GetDefaultPrinterName();

                PrintDialog p = new PrintDialog();
                p.AllowCurrentPage = false;
                p.AllowPrintToFile = false;
                p.AllowSelection = false;
                p.AllowSomePages = false;
                p.PrintToFile = false;
                p.UseEXDialog = true; //Fix win7
                //if (choosePrinter)
                {
                    DialogResult dr = p.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        _PrinterName = p.PrinterSettings.PrinterName;
                        _PrintCopies = p.PrinterSettings.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                        printting = true;
                        //Print(_PrinterName);
                        remove_list_g = new List<DataGridViewRow>();
                        Timer tF9 = new Timer();
                        tF9.Interval = 500;
                        tF9.Tick += tF9_Tick;
                        Thread t = new Thread(F9Thread);
                        t.SetApartmentState(ApartmentState.STA);
                        CheckForIllegalCrossThreadCalls = false;
                        t.IsBackground = true;
                        t.Start();
                        tF9.Start();

                    }
                    else
                    {
                        printting = false;
                    }
                }
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
            var Invoice = new V6Invoice81();
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
                        c.Text = "In phiếu xuất kho";
                        c.Report_Stt_rec = sttRec;
                        c.TTT = ObjectAndString.ObjectToDecimal(row.Cells["T_TT"].Value);
                        c.TTT_NT = ObjectAndString.ObjectToDecimal(row.Cells["T_TT_NT"].Value);
                        c.MA_NT = row.Cells["MA_NT"].Value.ToString().Trim();
                        c.Dock = DockStyle.Fill;
                        //c.xong = false;
                        c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                        {
                            if (hoadon_nd51 == 1)
                            {
                                var sql = "Update Am81 Set Sl_in = Sl_in+1 Where Stt_rec=@p";
                                SqlConnect.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter("@p", stt_rec));
                            }
                            sender.Dispose();
                        };

                        var f = new V6Form();
                        f.StartPosition = FormStartPosition.CenterScreen;
                        f.WindowState = FormWindowState.Maximized;
                        f.Text = V6Text.PrintIXA;
                        f.Controls.Add(c);

                        c.Disposed += delegate
                        {
                            //c.xong = true;
                            f.Close();
                        };

                        c.PrintMode = FilterControl.Check1 ? V6PrintMode.AutoPrint : V6PrintMode.DoNoThing;
                        c.PrintCopies = _PrintCopies;

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
                btnNhan.PerformClick();
                try
                {
                    V6Tools.PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F9 Xử lý xong!");
            }
        }
        #endregion xulyF9

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
    }
}

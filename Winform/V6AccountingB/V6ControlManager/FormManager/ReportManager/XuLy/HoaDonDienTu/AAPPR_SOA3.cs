using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using PdfiumViewer;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Init;
using V6ThuePostManager;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    /// <summary>
    /// Chuyển sang hóa đơn điện tử.
    /// </summary>
    public class AAPPR_SOA3 : XuLyBase
    {
        V6InvoiceBase invoice = null;

        public AAPPR_SOA3(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            dataGridView1.Control_S = true;
            PostManager.ResetWS();
        }

        private void AAPPR_SOA3_Load(object sender, EventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "F9: In liên tục.";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
            try
            {
                if (FilterControl.String3 == "SOA") invoice = new V6Invoice81();
                else if (FilterControl.String3 == "SOB") invoice = new V6Invoice82();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MakeReport2", ex);
            }
        }

        //protected override void XuLyHienThiFormSuaChungTuF3()
        //{
        //    try
        //    {
        //        AAPPR_SOA3_ViewPDF view = new AAPPR_SOA3_ViewPDF();
        //        view.ShowDialog(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorException(this.GetType() + ".XuLyHienThiFormSuaChungTuF3", ex);
        //    }
        //}

        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private Button btnTestViewPdf;
        private Button btnPrintF7;
        private string f9MessageAll = "";
        protected override void XuLyF9()
        {
            try
            {
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
            f9MessageAll = "";

            //Select printer.
            var printerst = V6ControlFormHelper.ChoosePrinter(this, "", false);
            if (printerst == null)
            {
                f9Running = false;
                return;
            }
            //WebBrowser webBrowser1 = null;

            string return_file_name = "";
            string tableName = "V6MAPINFO";
            
            string keys = "UID,MA_TD1";//+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
            //var map_table = V6BusinessHelper.Select(tableName, "*", "LOAI = '_reportFile' and (MA_TD1='" + FilterControl.String1 + "' or ma_td1='0' or ma_td1='') order by GROUPNAME,GC_TD1").Data;

            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                string error = null;
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //Lấy giá trị
                        //string mode = row.Cells["Kieu_in"].Value.ToString();
                        string soct = row.Cells["So_ct"].Value.ToString().Trim();
                        //string dir = row.Cells["Dir_in"].Value.ToString().Trim();
                        //string file = row.Cells["File_in"].Value.ToString().Trim();
                        //VPA_GET_V6MAPINFO @Loai   @Ma_td1 @Ma_ct  @Stt_rec    @Ma_dvcs    @User_id    @Advance
                        SqlParameter[] plist0 =
                        {
                            new SqlParameter("@Loai", _reportFile),
                            new SqlParameter("@MA_TD1", FilterControl.String1),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_dvcs", row.Cells["MA_DVCS"].Value.ToString()),
                            new SqlParameter("@User_ID", V6Login.UserId),
                            new SqlParameter("@Advance", ""),
                        };
                        var map_table = V6BusinessHelper.ExecuteProcedure("VPA_GET_V6MAPINFO", plist0).Tables[0];
                        
                        // Download
                        // 1:VIETTEL 2:VNPT 3:BKAV 4:VNPT_TOKEN 5:SOFT_DREAMS
                        if (FilterControl.String1 == "1")
                        {
                            string pattern = row.Cells["MA_MAUHD"].Value.ToString().Trim();
                            string serial = row.Cells["SO_SERI"].Value.ToString().Trim();
                            string invoiceNo = row.Cells["SO_CT"].Value.ToString().Trim();
                            DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(row.Cells["NGAY_CT"].Value);
                            string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                            string pi = row.Cells["Part_infos"].Value.ToString().Trim();

                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = invoiceNo,
                                InvoiceDate = ngay_ct,
                                Fkey_hd = fkey_hd,
                                Saved_Partner_infos = pi,
                                Pattern = pattern,
                                Serial = serial,
                                Mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"],
                            };
                            return_file_name = PostManager.PowerDownloadPDF(pmparams1, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                f9Error += error;
                                f9ErrorAll += error;
                                f9MessageAll += error;
                                continue;
                            }
                        }
                        else if (FilterControl.String1 == "2" || FilterControl.String1 == "4")
                        {
                            string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                            //var download = PostManager.DownloadInvFkeyNoPay(fkey_hd);
                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = soct,
                                Fkey_hd = fkey_hd,
                                //Pattern = pattern,
                                Mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"],
                            };
                            return_file_name = PostManager.PowerDownloadPDF(pmparams1, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                f9Error += error;
                                f9ErrorAll += error;
                                f9MessageAll += error;
                                continue;
                            }
                        }
                        else if (FilterControl.String1 == "3")
                        {
                            string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = soct,
                                Fkey_hd = fkey_hd,
                                Mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"],
                            };
                            return_file_name = PostManager.PowerDownloadPDF(pmparams1, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                f9Error += error;
                                f9ErrorAll += error;
                                f9MessageAll += error;
                                continue;
                            }
                        }
                        else if (FilterControl.String1 == "5")
                        {
                            string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                            string pattern = row.Cells["MA_MAUHD"].Value.ToString().Trim();
                            string serial = row.Cells["SO_SERI"].Value.ToString().Trim();
                            //var download = PostManager.DownloadInvFkeyNoPay(fkey_hd);
                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = soct,
                                Fkey_hd = fkey_hd,
                                Pattern = pattern,
                                Serial = serial,
                                Mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"],
                            };
                            return_file_name = PostManager.PowerDownloadPDF(pmparams1, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                f9Error += error;
                                f9ErrorAll += error;
                                f9MessageAll += error;
                                continue;
                            }
                        }
                        else if (FilterControl.String1 == "6")
                        {
                            string v6_partner_id = row.Cells["V6PARTNER_ID"].Value.ToString().Trim();
                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = soct,
                                V6PartnerID = v6_partner_id,
                                Mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"],
                            };
                            return_file_name = PostManager.PowerDownloadPDF(pmparams1, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                f9Error += error;
                                f9ErrorAll += error;
                                f9MessageAll += error;
                                continue;
                            }
                        }
                        else if (FilterControl.String1 == "8")
                        {
                            string v6_partner_id = row.Cells["V6PARTNER_ID"].Value.ToString().Trim();
                            var pmparams = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = soct,
                                //InvoiceDate = ngay_ct,
                                V6PartnerID = v6_partner_id,
                                //Pattern = pattern,
                                //Fkey_hd = fkey_hd,
                                Mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"],
                            };
                            return_file_name = PostManager.PowerDownloadPDF(pmparams, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                f9Error += error;
                                f9ErrorAll += error;
                                f9MessageAll += error;
                                continue;
                            }
                        }
                        else
                        {
                            this.ShowInfoMessage(V6Text.NotSupported);
                        }

                        // Update

                        // In
                        string ext = Path.GetExtension(return_file_name).ToLower();
                        if (ext == ".pdf")
                        {
                            PdfDocument pdfDocument1 = PdfDocument.Load(return_file_name);
                            using (PrintDocument printDocument = pdfDocument1.CreatePrintDocument(PdfPrintMode.ShrinkToMargin))
                            {
                                printDocument.PrinterSettings = printerst;
                                //printDialog.Document = printDocument;
                                //printDialog.AllowSomePages = true;
                                //printDocument = printDocument;
                                //printDialog.UseEXDialog = true;
                                //printDocument.PrinterSettings.FromPage = 1;
                                //printDocument.PrinterSettings.ToPage = pdfViewer1.Document.PageCount;
                                printDocument.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("A4",810,1100);
                                printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4",810,1100);
                                try
                                {
                                    if (printDocument.PrinterSettings.FromPage <= pdfDocument1.PageCount)
                                    {
                                        printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4",810,1100);
                                        printDocument.Print();
                                    }
                                }
                                catch(Exception ex)
                                {
                                    f9Error += ex.Message;
                                    f9ErrorAll += ex.Message;
                                    f9MessageAll += ex.Message;
                                }
                            }
                        }
                        else if (ext == ".html")
                        {
                            HtmlViewerForm view = new HtmlViewerForm(return_file_name, return_file_name, true);
                            view.AutoPrint = true;
                            view.ShowDialog(this);
                        }

                        
                        // Update

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                                new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                                new SqlParameter("@Set_so_ct", ""),
                                new SqlParameter("@Set_fkey_hd", ""),
                                new SqlParameter("@MA_TD1", FilterControl.String1),
                                //new SqlParameter("@Partner_infors", paras.Result.PartnerInfors),
                                new SqlParameter("@User_ID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure + "_UPDATE", plist);
                        
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                    f9MessageAll += ex.Message;
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
                    + cError + _message);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                string message = "F9 " + V6Text.Finish + " " + (f9ErrorAll.Length > 0 ? "Error: " : "") + f9ErrorAll;
                V6ControlFormHelper.SetStatusText(message);
                V6ControlFormHelper.ShowMainMessage(message);
                this.ShowMessage("F9 " + V6Text.Finish + " " + f9MessageAll, 300);
            }
        }
        #endregion xulyF9

        private void btnTestViewPdf_Click(object sender, EventArgs e)
        {
            bool ctrl_is_down = (ModifierKeys & Keys.Control) == Keys.Control;
            if (ctrl_is_down)
            {
                btnTestViewXml_Click(sender, e);
                return;
            }
            
            try
            {
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                
                if (dataGridView1.DataSource == null || dataGridView1.CurrentRow == null)
                {
                    return;
                }

                var row = dataGridView1.CurrentRow;

                string mode = V6Options.V6OptionValues["M_HDDT_TYPE_PRINT"];
                if (shift_is_down)
                {
                    mode = mode == "1" ? "2" : "1";
                }
                //Download selected einvoice
                //, error = "", sohoadon = "", id = "";
                string return_file_name = "";
                //var map_table = V6BusinessHelper.Select(tableName, "*", "LOAI = '_reportFile' and (MA_TD1='" + FilterControl.String1 + "' or ma_td1='0' or ma_td1='') order by GROUPNAME,GC_TD1").Data;
                SqlParameter[] plist0 =
                {
                    new SqlParameter("@Loai", _reportFile),
                    new SqlParameter("@MA_TD1", FilterControl.String1),
                    new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                    new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                    new SqlParameter("@Ma_dvcs", row.Cells["MA_DVCS"].Value.ToString()),
                    new SqlParameter("@User_ID", V6Login.UserId),
                    new SqlParameter("@Advance", ""),
                };
                var map_table = V6BusinessHelper.ExecuteProcedure("VPA_GET_V6MAPINFO", plist0).Tables[0];

                string seri = row.Cells["SO_SERI"].Value.ToString().Trim();
                string invoiceNo = row.Cells["SO_CT"].Value.ToString().Trim();
                string v6_partner_id = row.Cells["V6PARTNER_ID"].Value.ToString().Trim();
                string pattern = row.Cells["MA_MAUHD"].Value.ToString().Trim();
                string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                DateTime ngay_ct = ObjectAndString.ObjectToFullDateTime(row.Cells["NGAY_CT"].Value);
                string part_infos = row.Cells["Part_infos"].Value.ToString().Trim();

                var pmparams = new PostManagerParams
                {
                    AM_data = row.ToDataDictionary(),
                    DataSet = map_table.DataSet,
                    Branch = FilterControl.String1,
                    Serial = seri,
                    InvoiceNo = invoiceNo,
                    InvoiceDate = ngay_ct,
                    V6PartnerID = v6_partner_id,
                    Pattern = pattern,
                    Fkey_hd = fkey_hd,
                    Saved_Partner_infos = part_infos,
                    Mode = mode,
                };
                string error;
                return_file_name = PostManager.PowerDownloadPDF(pmparams, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    this.ShowErrorMessage(error);
                    return;
                }

                string ext = Path.GetExtension(return_file_name).ToLower();
                if (ext == ".pdf")
                {
                    PDF_ViewPrintForm view = new PDF_ViewPrintForm(return_file_name);
                    view.ShowDialog(this);
                }
                else if (ext == ".html")
                {
                    HtmlViewerForm view = new HtmlViewerForm(return_file_name, return_file_name, false);
                    view.ShowDialog(this);
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnTestViewPdf_Click", ex);
            }
        }

        private void btnPrintF7_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Copy từ SOA2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestViewXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.DataSource == null || dataGridView1.CurrentRow == null)
                {
                    return;
                }
                bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                var row = dataGridView1.CurrentRow;

                //string mode = row.Cells["Kieu_in"].Value.ToString();
                string soct = row.Cells["So_ct"].Value.ToString().Trim();
                string dir = row.Cells["Dir_in"].Value.ToString().Trim();
                string file = row.Cells["File_in"].Value.ToString().Trim();
                string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                string inputPattern = "";
                // nhập mẫu để lấy thông tin Metadata Viettel
                StringInput inputForm = new StringInput("Nhập mẫu hóa đơn. vd 1/001", "1/001");
                if (shift_is_down && inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    inputPattern = inputForm.InputString;
                }

                SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@HoaDonMau","0"),
                            new SqlParameter("@isInvoice","1"),
                            new SqlParameter("@ReportFile",""),
                            new SqlParameter("@MA_TD1", FilterControl.String1),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                DataSet ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure + "F9xml", plist);
                //DataTable data0 = ds.Tables[0];
                string result = "";//, error = "", sohoadon = "", id = "";
                var paras = new PostManagerParams
                {
                    DataSet = ds,
                    Mode = "TestView" + (shift_is_down ? "_Shift" : ""),
                    Pattern = inputPattern,
                    Branch = FilterControl.String1,
                    Dir = dir,
                    FileName = file,
                    Key_Down = "TestView" + (shift_is_down ? "_Shift" : ""),
                    RptFileFull = ReportFileFull,
                    Fkey_hd = fkey_hd,
                };
                result = PostManager.PowerPost(paras);
                Clipboard.SetText(result);
                //this.ShowMessage(result);
                AAPPR_SOA2_ViewXml viewer = new AAPPR_SOA2_ViewXml(result);
                viewer.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnTestViewPdf_Click", ex);
            }
        }

        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAD(sttRec);
                dataGridView2.DataSource = data;
                dataGridView2.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA3 ViewDetails: " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.btnTestViewPdf = new System.Windows.Forms.Button();
            this.btnPrintF7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestViewPdf
            // 
            this.btnTestViewPdf.Location = new System.Drawing.Point(185, 30);
            this.btnTestViewPdf.Name = "btnTestViewPdf";
            this.btnTestViewPdf.Size = new System.Drawing.Size(60, 23);
            this.btnTestViewPdf.TabIndex = 22;
            this.btnTestViewPdf.Text = "Einvoice";
            this.toolTipV6FormControl.SetToolTip(this.btnTestViewPdf, "Shift : đảo mode, Ctrl : xml");
            this.btnTestViewPdf.UseVisualStyleBackColor = true;
            this.btnTestViewPdf.Click += new System.EventHandler(this.btnTestViewPdf_Click);
            // 
            // btnPrintF7
            // 
            this.btnPrintF7.Location = new System.Drawing.Point(250, 30);
            this.btnPrintF7.Name = "btnPrintF7";
            this.btnPrintF7.Size = new System.Drawing.Size(55, 23);
            this.btnPrintF7.TabIndex = 25;
            this.btnPrintF7.Text = "Print";
            this.btnPrintF7.UseVisualStyleBackColor = true;
            this.btnPrintF7.Click += new System.EventHandler(this.btnPrintF7_Click);
            // 
            // AAPPR_SOA3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.btnPrintF7);
            this.Controls.Add(this.btnTestViewPdf);
            this.Name = "AAPPR_SOA3";
            this.Load += new System.EventHandler(this.AAPPR_SOA3_Load);
            this.Controls.SetChildIndex(this.btnSuaTTMauBC, 0);
            this.Controls.SetChildIndex(this.btnThemMauBC, 0);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.btnTestViewPdf, 0);
            this.Controls.SetChildIndex(this.btnPrintF7, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
    }

}

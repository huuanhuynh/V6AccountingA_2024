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
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9: In liên tục.");
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
            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowSomePages = false;//
            //printDialog.Document = printDocument;
            printDialog.UseEXDialog = true;
            //printDialog.Document.PrinterSettings.FromPage = 1;
            //printDialog.Document.PrinterSettings.ToPage = pdfDocument1.PageCount;
            if (printDialog.ShowDialog((IWin32Window)this.FindForm()) != DialogResult.OK)
                return;
            WebBrowser webBrowser1 = null;

            string return_file_name = "";
            string tableName = "V6MAPINFO";
            
            string keys = "UID,MA_TD1";//+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
            //var map_table = V6BusinessHelper.Select(tableName, "*", "LOAI = 'AAPPR_SOA2' and (MA_TD1='" + FilterControl.String1 + "' or ma_td1='0' or ma_td1='') order by GROUPNAME,GC_TD1").Data;

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
                            new SqlParameter("@Loai", "AAPPR_SOA2"),
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
                            string invoiceNo = serial + row.Cells["SO_CT"].Value.ToString().Trim();
                            string strIssueDate = ObjectAndString.ObjectToString(row.Cells["NGAY_CT"].Value, "yyyyMMddHHmmss");

                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = invoiceNo,
                                Pattern = pattern,
                                Serial = serial,
                                strIssueDate = strIssueDate,
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
                                printDocument.PrinterSettings = printDialog.PrinterSettings;
                                printDialog.Document = printDocument;
                                //printDialog.AllowSomePages = true;
                                //printDialog.Document = printDocument;
                                //printDialog.UseEXDialog = true;
                                //printDialog.Document.PrinterSettings.FromPage = 1;
                                //printDialog.Document.PrinterSettings.ToPage = pdfViewer1.Document.PageCount;
                                printDialog.Document.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("A4",810,1100);
                                printDialog.Document.DefaultPageSettings.PaperSize = new PaperSize("A4",810,1100);
                                try
                                {
                                    if (printDialog.Document.PrinterSettings.FromPage <= pdfDocument1.PageCount)
                                    {
                                        printDialog.Document.DefaultPageSettings.PaperSize = new PaperSize("A4",810,1100);
                                        printDialog.Document.Print();
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
                            var setPrinterOk = PrinterStatus.SetDefaultPrinter(printDialog.PrinterSettings.PrinterName);
                            HtmlViewerForm view = new HtmlViewerForm(return_file_name, return_file_name, true);
                            view.AutoPrint = true;
                            view.ShowDialog(this);

                            //webBrowser1.Navigate(return_file_name);
                            //while(webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                            //{
                            //    //Application.DoEvents();
                            //    Thread.Sleep(1000);
                            //}
                            //webBrowser1.Print();

                            //// Create a WebBrowser instance. 
                            //webBrowser1 = new WebBrowser();
                            //// Add an event handler that prints the document after it loads.
                            //webBrowser1.DocumentCompleted += (sender, args) =>
                            //{
                            //    // Print the document now that it is fully loaded.
                            //    ((WebBrowser)sender).Print();
                            //    // Dispose the WebBrowser now that the task is complete. 
                            //    ((WebBrowser)sender).Dispose();
                            //};
                            //// Set the Url property to load the document.
                            //webBrowser1.Url = new Uri(return_file_name);
                        

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
                    if (mode == "0") mode = "1";
                    else if (mode == "1") mode = "0";
                }
                //Download selected einvoice
                //, error = "", sohoadon = "", id = "";
                string return_file_name = "";
                string tableName = "V6MAPINFO";
                string keys = "UID,MA_TD1";//+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
                //var map_table = V6BusinessHelper.Select(tableName, "*", "LOAI = 'AAPPR_SOA2' and (MA_TD1='" + FilterControl.String1 + "' or ma_td1='0' or ma_td1='') order by GROUPNAME,GC_TD1").Data;
                SqlParameter[] plist0 =
                {
                    new SqlParameter("@Loai", "AAPPR_SOA2"),
                    new SqlParameter("@MA_TD1", FilterControl.String1),
                    new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                    new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                    new SqlParameter("@Ma_dvcs", row.Cells["MA_DVCS"].Value.ToString()),
                    new SqlParameter("@User_ID", V6Login.UserId),
                    new SqlParameter("@Advance", ""),
                };
                var map_table = V6BusinessHelper.ExecuteProcedure("VPA_GET_V6MAPINFO", plist0).Tables[0];

                string invoiceNo = row.Cells["SO_SERI"].Value.ToString().Trim() + row.Cells["SO_CT"].Value.ToString().Trim();
                string v6_partner_id = row.Cells["V6PARTNER_ID"].Value.ToString().Trim();
                string pattern = row.Cells["MA_MAUHD"].Value.ToString().Trim();
                string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                string strIssueDate = ObjectAndString.ObjectToString(row.Cells["NGAY_CT"].Value, "yyyyMMddHHmmss");
                
                var pmparams = new PostManagerParams
                {
                    DataSet = map_table.DataSet,
                    Branch = FilterControl.String1,
                    InvoiceNo = invoiceNo,
                    V6PartnerID = v6_partner_id,
                    Pattern = pattern,
                    Fkey_hd = fkey_hd,
                    strIssueDate = strIssueDate,
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

                var row = dataGridView1.CurrentRow;

                //string mode = row.Cells["Kieu_in"].Value.ToString();
                string soct = row.Cells["So_ct"].Value.ToString().Trim();
                string dir = row.Cells["Dir_in"].Value.ToString().Trim();
                string file = row.Cells["File_in"].Value.ToString().Trim();
                string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();

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
                    Mode = "TestView",
                    Branch = FilterControl.String1,
                    Dir = dir,
                    FileName = file,
                    Key_Down = "TestView",
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
            this.SuspendLayout();
            // 
            // btnTestViewPdf
            // 
            this.btnTestViewPdf.Location = new System.Drawing.Point(190, 30);
            this.btnTestViewPdf.Name = "btnTestViewPdf";
            this.btnTestViewPdf.Size = new System.Drawing.Size(111, 23);
            this.btnTestViewPdf.TabIndex = 22;
            this.btnTestViewPdf.Text = "Xem Einvoice";
            this.toolTipV6FormControl.SetToolTip(this.btnTestViewPdf, "Shift : đảo mode, Ctrl : xml");
            this.btnTestViewPdf.UseVisualStyleBackColor = true;
            this.btnTestViewPdf.Click += new System.EventHandler(this.btnTestViewPdf_Click);
            // 
            // AAPPR_SOA3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.btnTestViewPdf);
            this.Name = "AAPPR_SOA3";
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.btnTestViewPdf, 0);
            this.ResumeLayout(false);

        }

        
    }

}

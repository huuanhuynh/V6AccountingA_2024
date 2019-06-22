﻿using System;
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
using V6Init;
using V6ThuePostManager;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    /// <summary>
    /// Chuyển sang hóa đơn điện tử.
    /// </summary>
    public class AAPPR_SOA3 : XuLyBase
    {
        public string MAU
        {
            get
            {
                return FilterControl != null ? FilterControl.String2 : "";
            }
        }

        public string LAN
        {
            get { return "V"; }
        }
        private string ReportFileFull
        {
            get
            {
                var result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + ReportFile + ".rpt";//ReportFile co su thay doi khi chon o combobox
                if (!File.Exists(result))
                {
                    result = @"Reports\"
                       + MAU + @"\"
                       + LAN + @"\"
                       + _reportFile + ".rpt";//_reportFile gốc
                }
                return result;
            }
        }
        

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
        }

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {
                AAPPR_SOA3_ViewPDF view = new AAPPR_SOA3_ViewPDF();
                view.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(this.GetType() + ".XuLyHienThiFormSuaChungTuF3", ex);
            }
        }

        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private Button btnTestView;
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

            string pdf_file = "";
            string tableName = "V6MAPINFO";
            string keys = "UID,MA_TD1";//+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
            var map_table = V6BusinessHelper.Select(tableName, "*", "LOAI = 'AAPPR_SOA2' and (MA_TD1='" + FilterControl.String1 + "' or ma_td1='0' or ma_td1='') order by date0,time0").Data;

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
                        //string soct = row.Cells["So_ct"].Value.ToString().Trim();
                        //string dir = row.Cells["Dir_in"].Value.ToString().Trim();
                        //string file = row.Cells["File_in"].Value.ToString().Trim();
                        
                       
                        // Download
                        // 1:VIETTEL 2:VNPT 3:BKAV
                        if (FilterControl.String1 == "1")
                        {
                            string invoiceNo = row.Cells["SO_SERI"].Value.ToString().Trim() + row.Cells["SO_CT"].Value.ToString().Trim();
                            string parttern = row.Cells["MA_MAUHD"].Value.ToString().Trim();

                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                InvoiceNo = invoiceNo,
                                Parttern = parttern,
                            };
                            pdf_file = PostManager.PowerDownloadPDF(pmparams1, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                f9Error += error;
                                f9ErrorAll += error;
                                f9MessageAll += error;
                                continue;
                            }
                        }
                        else if (FilterControl.String1 == "2")
                        {
                            string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();
                            //var download = PostManager.DownloadInvFkeyNoPay(fkey_hd);
                            var pmparams1 = new PostManagerParams
                            {
                                DataSet = map_table.DataSet,
                                Branch = FilterControl.String1,
                                //InvoiceNo = invoiceNo,
                                Fkey_hd = fkey_hd,
                                //Parttern = parttern,
                            };
                            pdf_file = PostManager.PowerDownloadPDF(pmparams1, out error);
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
                                //InvoiceNo = invoiceNo,
                                Fkey_hd = fkey_hd,
                            };
                            pdf_file = PostManager.PowerDownloadPDF(pmparams1, out error);
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
                        PdfDocument pdfDocument1 = PdfDocument.Load(pdf_file);
                        using (PrintDocument printDocument = pdfDocument1.CreatePrintDocument(PdfPrintMode.ShrinkToMargin))
                        {
                            printDocument.PrinterSettings = printDialog.PrinterSettings;
                            printDialog.Document = printDocument;
                            try
                            {
                                if (printDialog.Document.PrinterSettings.FromPage <= pdfDocument1.PageCount)
                                    printDialog.Document.Print();
                            }
                            catch(Exception ex)
                            {
                                f9Error += ex.Message;
                                f9ErrorAll += ex.Message;
                                f9MessageAll += ex.Message;
                            }
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

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "_UPDATE", plist);
                        
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

        private void btnTestView_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.DataSource == null || dataGridView1.CurrentRow == null)
                {
                    return;
                }

                var row = dataGridView1.CurrentRow;

                //Download selected einvoice
                //, error = "", sohoadon = "", id = "";
                string pdf_file = "";
                string tableName = "V6MAPINFO";
                string keys = "UID,MA_TD1";//+ma_td1   1:VIETTEL    2:VNPT    3:BKAV
                var map_table = V6BusinessHelper.Select(tableName, "*", "LOAI = 'AAPPR_SOA2' and (MA_TD1='" + FilterControl.String1 + "' or ma_td1='0' or ma_td1='') order by date0,time0").Data;

                string invoiceNo = row.Cells["SO_SERI"].Value.ToString().Trim() + row.Cells["SO_CT"].Value.ToString().Trim();
                string parttern = row.Cells["MA_MAUHD"].Value.ToString().Trim();
                string fkey_hd = row.Cells["fkey_hd"].Value.ToString().Trim();

                var pmparams = new PostManagerParams
                {
                    DataSet = map_table.DataSet,
                    Branch = FilterControl.String1,
                    InvoiceNo = invoiceNo,
                    Parttern = parttern,
                    Fkey_hd = fkey_hd,
                };
                string error;
                pdf_file = PostManager.PowerDownloadPDF(pmparams, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    this.ShowErrorMessage(error);
                    return;
                }

                AAPPR_SOA3_ViewPDF view = new AAPPR_SOA3_ViewPDF(pdf_file);
                view.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnTestView_Click", ex);
            }
        }

        V6Invoice81 invoice = new V6Invoice81();
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
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA3 ViewDetails: " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.btnTestView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestView
            // 
            this.btnTestView.Location = new System.Drawing.Point(190, 28);
            this.btnTestView.Name = "btnTestView";
            this.btnTestView.Size = new System.Drawing.Size(111, 23);
            this.btnTestView.TabIndex = 22;
            this.btnTestView.Text = "Bản thể hiện";
            this.btnTestView.UseVisualStyleBackColor = true;
            this.btnTestView.Click += new System.EventHandler(this.btnTestView_Click);
            // 
            // AAPPR_SOA3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.btnTestView);
            this.Name = "AAPPR_SOA3";
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.btnTestView, 0);
            this.ResumeLayout(false);

        }

        
    }

}
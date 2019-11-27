using System;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Xsl;
using PdfiumViewer;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_SOA3_ViewPDF : V6Form
    {
        private string _fileName = @"D:\TEST\0100109106-566-DC_19E0000039\C1NC0QFQE-DC_19E0000039.pdf";
        public AAPPR_SOA3_ViewPDF()
        {
            InitializeComponent();
        }
        
        public AAPPR_SOA3_ViewPDF(string fileName)
        {
            InitializeComponent();
            _fileName = fileName;
        }
        
        private void Form_Load(object sender, EventArgs e)
        {
            LoadInvoiceViettel();
        }

        private void LoadInvoiceViettel()
        {
            try
            {
                pdfViewer1.CreateGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                pdfViewer1.Document = PdfDocument.Load(this, _fileName);
                pdfViewer1.ZoomMode = PdfViewerZoomMode.FitBest;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(this.GetType() + ".LoadInvoice", ex);
            }
        }

        public void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;

        private void button1_Click(object sender, EventArgs e)
        {
            //var d = pdfViewer1.Document.CreatePrintDocument(PdfPrintMode.ShrinkToMargin);
            //d.Print();
            try
            {
                using (PrintDialog printDialog = new PrintDialog())
                {
                    using (PrintDocument printDocument = pdfViewer1.Document.CreatePrintDocument(pdfViewer1.DefaultPrintMode))
                    {
                        printDialog.AllowSomePages = true;
                        printDialog.Document = printDocument;
                        printDialog.UseEXDialog = true;
                        printDialog.Document.PrinterSettings.FromPage = 1;
                        printDialog.Document.PrinterSettings.ToPage = pdfViewer1.Document.PageCount;
                        if (printDialog.ShowDialog((IWin32Window)this.FindForm()) != DialogResult.OK)
                            return;
                        try
                        {
                            if (printDialog.Document.PrinterSettings.FromPage <= pdfViewer1.Document.PageCount)
                                printDialog.Document.Print();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(this.GetType() + ".button1_Click", ex);
            }
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }

        private void AAPPR_SOA3_ViewPDF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pdfViewer1.Document != null) pdfViewer1.Document.Dispose();
        }
        
    }
}

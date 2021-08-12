using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using PdfiumViewer;

namespace V6Controls.Forms.Viewer
{
    public partial class PDF_ViewPrintForm : V6Form
    {
        private string _fileName = @"D:\TEST\0100109106-566-DC_19E0000039\C1NC0QFQE-DC_19E0000039.pdf";
        public PDF_ViewPrintForm()
        {
            InitializeComponent();
        }
        
        public PDF_ViewPrintForm(string fileName)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //var d = pdfViewer1.Document.CreatePrintDocument(PdfPrintMode.ShrinkToMargin);
            //d.Print();
            try
            {
                using (PrintDialog printDialog = new PrintDialog())
                {
                    using (PrintDocument printDocument = pdfViewer1.Document.CreatePrintDocument(pdfViewer1.DefaultPrintMode))
                    {
                        //pageForPrint = 0;
                        //printDocument.PrintPage += new PrintPageEventHandler(HandlePrinting);

                        printDialog.AllowSomePages = true;
                        printDialog.Document = printDocument;
                        printDialog.UseEXDialog = true;
                        printDialog.Document.PrinterSettings.FromPage = 1;
                        printDialog.Document.PrinterSettings.ToPage = pdfViewer1.Document.PageCount;
                        if (printDocument.PrinterSettings.DefaultPageSettings.PaperSize.Width > 825)
                            printDocument.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("A4",800,1100);
                        if (printDocument.DefaultPageSettings.PaperSize.Width > 825)
                            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 800, 1100);
                        if (printDialog.ShowDialog((IWin32Window) this.FindForm()) != DialogResult.OK)
                        {
                            return;
                        }
                        try
                        {
                            if (printDialog.Document.PrinterSettings.FromPage <= pdfViewer1.Document.PageCount)
                            {
                                if(printDocument.DefaultPageSettings.PaperSize.Width>825)
                                    printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4",800,1100);

                                // Define a PrintPage event handler and start printing.
                                printDialog.Document.Print();
                            }
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

        //private int pageForPrint = 0;
        //void HandlePrinting(object sender, PrintPageEventArgs e)
        //{
        //    //Print next page
        //    if (pageForPrint < pdfViewer1.Document.PageCount - 1)
        //    {
        //        pageForPrint++;
        //        e.HasMorePages = true;
        //    }
        //}
        
        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }

        private void AAPPR_SOA3_ViewPDF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pdfViewer1.Document != null) pdfViewer1.Document.Dispose();
        }

        private void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            try
            {
                var saveFile = V6ControlFormHelper.ChooseSaveFile(this, "PDF|*.pdf", Path.GetFileName(_fileName));
                if (!string.IsNullOrEmpty(saveFile))
                {
                    File.Copy(_fileName, saveFile);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(this.GetType() + ".btnDownloadPDF_Click", ex);
            }
        }
        
    }
}

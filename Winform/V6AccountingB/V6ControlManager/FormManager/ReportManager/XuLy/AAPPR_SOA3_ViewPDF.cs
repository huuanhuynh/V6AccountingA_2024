using System;
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
        public AAPPR_SOA3_ViewPDF()
        {
            InitializeComponent();
        }
        
        private void Form_Load(object sender, EventArgs e)
        {
            LoadInvoiceViettel();
        }

        private void LoadInvoiceViettel()
        {
            try
            {
                var fileName = @"D:\TEST\0100109106-566-DC_19E0000039\C1NC0QFQE-DC_19E0000039.pdf";
                pdfViewer1.Document = PdfDocument.Load(this, fileName);
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
            var d = pdfViewer1.Document.CreatePrintDocument(PdfPrintMode.ShrinkToMargin);
            d.Print();
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }
        
    }
}

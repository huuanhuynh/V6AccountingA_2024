using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Xsl;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_SOA3_View : V6Form
    {
        public AAPPR_SOA3_View()
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
                string outfile = Path.Combine(Application.StartupPath, @"Invoice\Test.html");
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(@"D:\TEST\0100109106-566-DC_19E0000039\invoice.xsl");
                transform.Transform(@"D:\TEST\0100109106-566-DC_19E0000039\ZC1NC0QFQE-DC_19E0000039.xml", outfile);
                webBrowser1.Navigate(outfile);
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
            webBrowser1.Print();
        }
        
    }
}

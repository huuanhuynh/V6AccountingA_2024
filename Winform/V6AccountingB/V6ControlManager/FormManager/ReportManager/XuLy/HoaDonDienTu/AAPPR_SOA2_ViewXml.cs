using System;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Xsl;
using PdfiumViewer;
using V6Controls;
using V6Controls.Forms;
using System.Collections;
using System.Collections.Generic;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_SOA2_ViewXml : V6Form
    {
        private string _xml_string = "";
        public IDictionary<string, object> MoreInfos = null;
        public AAPPR_SOA2_ViewXml()
        {
            InitializeComponent();
        }
        
        public AAPPR_SOA2_ViewXml(string xmlString)
        {
            InitializeComponent();
            _xml_string = xmlString;
        }
        
        private void Form_Load(object sender, EventArgs e)
        {
            LoadViewContent();
            ViewMoreInfos();
        }

        private void LoadViewContent()
        {
            try
            {
                richTextBox1.Text = _xml_string;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(this.GetType() + ".LoadInvoice", ex);
            }
        }

        private void ViewMoreInfos()
        {
            try
            {
                if (MoreInfos != null)
                {
                    foreach (var item in MoreInfos)
                    {
                        richTextBox2.AppendText(item.Key + ": " + item.Value + "\n");
                    }
                }
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
            try
            {
                using (PrintDialog printDialog = new PrintDialog())
                {
                    
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(this.GetType() + ".btnPrint_Click", ex);
            }
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }

        private void AAPPR_SOA2_ViewXml_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        
    }
}

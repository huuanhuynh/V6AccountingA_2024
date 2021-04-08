using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_EINVOICE1_F6 : V6Form
    {
        public AAPPR_EINVOICE1_F6()
        {
            InitializeComponent();
            MyInit();
        }

        /// <summary>
        /// Dòng đang đứng.
        /// </summary>
        public DataGridViewRow SelectedGridViewRow { get { return xuLyBase1.dataGridView1.CurrentRow; } }

        private void MyInit()
        {
            try
            {
                xuLyBase1._reportProcedure = "AAPPR_EINVOICE1F6";
                xuLyBase1.AddFilterControl("AAPPR_EINVOICE1");
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
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
        
    }
}

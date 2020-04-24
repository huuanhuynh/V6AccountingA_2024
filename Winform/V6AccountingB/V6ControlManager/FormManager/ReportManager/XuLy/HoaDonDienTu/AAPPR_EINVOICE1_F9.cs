using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_EINVOICE1_F9 : V6Form
    {
        public AAPPR_EINVOICE1_F9()
        {
            InitializeComponent();
            MyInit();
        }

        public string SelectedMode { get { return cboAlxuly.SelectedValue.ToString().Trim(); } }

        private void MyInit()
        {
            try
            {
                //Load Alxuly
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_CT", "S14");
                keys.Add("STATUS", 1);
                DataTable data = V6BusinessHelper.Select("Alxuly", keys, "*", "", "STT").Data;

                cboAlxuly.ValueMember = "Ma_xuly";
                cboAlxuly.DisplayMember = "Ten_xuly";
                cboAlxuly.DataSource = data;
                cboAlxuly.ValueMember = "Ma_xuly";
                cboAlxuly.DisplayMember = "Ten_xuly";
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

        private void cboAlxuly_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTenXuLy.Text = "";
            if (cboAlxuly.SelectedItem != null)
            {
                DataRowView row = cboAlxuly.SelectedItem as DataRowView;
                if (row != null)
                {
                    lblTenXuLy.Text = row.Row["TEN_XULY"].ToString().Trim();
                }
            }
        }
        
    }
}

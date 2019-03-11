using System;
using System.Data;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong
{
    public partial class ChangeDVCSControl : V6Control
    {
        public ChangeDVCSControl()
        {
            InitializeComponent();
        }

        private void ChangeDVCSControl_Load(object sender, EventArgs e)
        {
            LoadAgentData();
        }

        private void LoadAgentData()
        {
            try
            {
                var key = V6Login.IsAdmin
                    ? ""
                    : V6Login.MadvcsTotal > 0 ? "dbo.VFA_Inlist_MEMO(ma_dvcs, '" + V6Login.UserInfo["r_dvcs"] + "')=1" : "";

                DataTable agentData = V6Login.GetAgentTable(key);
                V6Login.MadvcsCount = agentData.Rows.Count;
                cboAgent.ValueMember = "Ma_dvcs";
                cboAgent.DisplayMember = V6Setting.IsVietnamese ? "Name" : "Name2";
                cboAgent.DataSource = agentData;
                cboAgent.ValueMember = "Ma_dvcs";
                cboAgent.DisplayMember = V6Setting.IsVietnamese ? "Name" : "Name2";

                cboAgent.SelectedValue = V6Login.Madvcs;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadAgentData", ex);
            }
        }
        
        private void ChangeLoginDVCS()
        {
            try
            {
                V6Login.Madvcs = cboAgent.SelectedValue.ToString().Trim();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChangeLoginDVCS", ex);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Dispose();
                return true;
            }
            return base.DoHotKey0(keyData);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            ChangeLoginDVCS();
            Dispose();
        }
    }
}

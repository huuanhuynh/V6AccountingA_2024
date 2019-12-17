using System;
using V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6MenuAddEditForm : AddEditControlVirtual
    {
        public V6MenuAddEditForm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
            //txtval.Focus();
        }
        public override void DoBeforeEdit()
        {
           
        }

        public override void ValidateData()
        {
           
        }

        public override void V6CtrlF12Execute()
        {
            ShowTopLeftMessage("V6 Confirm ......OK....");
            txtVBAR.Enabled = true;
            txtVBAR.ReadOnly = false;

            txtVBAR2.Enabled = true;
            txtVBAR2.ReadOnly = false;

            txtV2ID.Enabled = true;
            txtV2ID.ReadOnly = false;

            txtJOBID.Enabled = true;
            txtJOBID.ReadOnly = false;

            txtITEMID.Enabled = true;
            txtITEMID.ReadOnly = false;

            chkhide_yn.Enabled = true;
            chkQuickRun.Enabled = true;
            txtcodeform.Enabled = true;
            txtcodeform.ReadOnly = false;

            txtSttBox.Enabled = true;
            txtMaCt.Enabled = true;
            txtCode.Enabled = true;
            txtNhatKy.Enabled = true;
            txtKey1.Enabled = true;
            txtPicture.Enabled = true;
            txtKey3.Enabled = true;

            btnMenuHide.Enabled = true;
            btnMenuHide.Visible = true;
            base.V6CtrlF12Execute();
        }

        public override void V6CtrlF12ExecuteUndo()
        {
            txtVBAR.Enabled = false;
            txtVBAR.ReadOnly = true;

            txtVBAR2.Enabled = false;
            txtVBAR2.ReadOnly = true;

            txtV2ID.Enabled = false;
            txtV2ID.ReadOnly = true;

            txtJOBID.Enabled = false;
            txtJOBID.ReadOnly = true;

            txtITEMID.Enabled = false;
            txtITEMID.ReadOnly = true;

            chkhide_yn.Enabled = false;
            chkQuickRun.Enabled = false;
            txtcodeform.Enabled = false;
            txtcodeform.ReadOnly = true;

            txtSttBox.Enabled = false;
            txtMaCt.Enabled = false;
            txtCode.Enabled = false;
            txtNhatKy.Enabled = false;
            txtKey1.Enabled = false;
            txtPicture.Enabled = false;
            txtKey3.Enabled = false;
        }

        private void btnMenuHide_Click(object sender, System.EventArgs e)
        {
            try
            {
                V6MenuHideYN form = new V6MenuHideYN(Mode);
                form.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnMenuHide_Click", ex);
            }
        }

    }
}

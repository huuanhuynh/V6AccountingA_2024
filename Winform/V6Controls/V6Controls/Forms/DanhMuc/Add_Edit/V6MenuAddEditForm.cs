﻿using System;
using System.Collections.Generic;
using V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen;
using V6SqlConnect;

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
            ShowMainMessage("V6 Confirm ......OK....");
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
            txtPRO_OLD.Enabled = true;
            txtPRO_OLD.ReadOnly = false;

            txtSttBox.Enabled = true;
            txtMaCt.Enabled = true;
            txtCode.Enabled = true;
            txtNhatKy.Enabled = true;
            txtKey1.Enabled = true;
            txtPicture.Enabled = true;
            txtKey3.Enabled = true;
            txtPageX.Enabled = true;

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
            txtPRO_OLD.Enabled = false;
            txtPRO_OLD.ReadOnly = true;

            txtSttBox.Enabled = false;
            txtMaCt.Enabled = false;
            txtCode.Enabled = false;
            txtNhatKy.Enabled = false;
            txtKey1.Enabled = false;
            txtPicture.Enabled = false;
            txtKey3.Enabled = false;
            txtPageX.Enabled = false;
        }

        private void btnMenuHide_Click(object sender, EventArgs e)
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

        private void btnMenuPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mode != V6Structs.V6Mode.Edit) return;
                string path = "";
                Dictionary<string, object> keys = new Dictionary<string, object>();
                keys["Itemid"] = "A0000000";
                keys[txtV2ID.AccessibleName] = txtV2ID.Text;
                var menu0 = SqlConnect.SelectOneRow("V6Menu", keys);
                if (menu0.Rows.Count > 0)
                {
                    path += "/" + menu0.Rows[0]["Vbar"];
                }
                keys["Itemid"] = "B0000000";
                keys[txtJOBID.AccessibleName] = txtJOBID.Text;
                var menu1 = SqlConnect.SelectOneRow("V6Menu", keys);
                if (menu1.Rows.Count > 0)
                {
                    path += "/" + menu1.Rows[0]["Vbar"];
                }

                path += "/" + DataOld["HOTKEY"] + "." + txtVBAR.Text;

                this.ShowInfoMessage(path);
                SetStatusText(path);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
        }
    }
}

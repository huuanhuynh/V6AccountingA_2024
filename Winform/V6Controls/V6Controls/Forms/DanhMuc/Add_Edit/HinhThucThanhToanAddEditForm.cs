﻿using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class HinhThucThanhToanAddEditForm : AddEditControlVirtual
    {
        public HinhThucThanhToanAddEditForm()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            TxtTk_no.FilterStart = true;
            TxtTk_no.SetInitFilter("loai_tk=1");
            
        }
        public override void DoBeforeEdit()
        {
            try
            {
                var v = Categories.IsExistOneCode_List("ARI70,ARS20", "Ma_httt", txtMaHttt.Text);
                txtMaHttt.Enabled = !v;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("HTTT DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaHttt.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaHttt.Text;
            if (txtTenHttt.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenHttt.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_HTTT", txtMaHttt.Text.Trim(), DataOld["MA_HTTT"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaHttt.Text + "=" + txtMaHttt.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_HTTT", txtMaHttt.Text.Trim(), txtMaHttt.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaHttt.Text + "=" + txtMaHttt.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

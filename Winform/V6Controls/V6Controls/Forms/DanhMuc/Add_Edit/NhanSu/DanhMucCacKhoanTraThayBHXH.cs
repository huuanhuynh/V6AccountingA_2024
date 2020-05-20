﻿using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucCacKhoanTraThayBHXH : AddEditControlVirtual
    {
        public DanhMucCacKhoanTraThayBHXH()
        {
            InitializeComponent();
            MyInit();
        }
        public void MyInit()
        {

        }
        public override void DoBeforeEdit()
        {
            try
            {
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_TTBH", txtMaKhoan.Text);
                txtMaKhoan.Enabled = !v;

            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }   
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKhoan.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaKhoan.Text;
            if (txtTenKhoan.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenKhoan.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_TTBH",
                 txtMaKhoan.Text.Trim(), DataOld["MA_TTBH"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaKhoan.Text + "=" + txtMaKhoan.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_TTBH",
                 txtMaKhoan.Text.Trim(), txtMaKhoan.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaKhoan.Text + "=" + txtMaKhoan.Text;
            }
            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

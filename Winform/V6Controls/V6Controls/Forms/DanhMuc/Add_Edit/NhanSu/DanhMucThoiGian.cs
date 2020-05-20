﻿using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucThoiGian : AddEditControlVirtual
    {
        public DanhMucThoiGian()
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
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_TG", txtMaTg.Text);
                txtMaTg.Enabled = !v;

            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaTg.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaTG.Text;
            if (txtTenTG.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenTG.Text;

            if (Mode == V6Mode.Edit)
            {

                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_TG",
                 txtMaTg.Text.Trim(), DataOld["MA_TG"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaTG.Text + "=" + txtMaTg.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_TG",
                    txtMaTg.Text.Trim(), txtMaTg.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaTG.Text + "=" + txtMaTg.Text;
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

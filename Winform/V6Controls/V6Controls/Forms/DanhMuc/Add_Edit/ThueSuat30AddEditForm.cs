﻿using System;
using V6AccountingBusiness;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ThueSuat30AddEditForm : AddEditControlVirtual
    {
        public ThueSuat30AddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            try
            {
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_THUE", txtma_thue.Text);
                txtma_thue.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("ThueSuat30AddEditForm DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtma_thue.Text.Trim() == "" || txtten_thue.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_THUE",
                    txtma_thue.Text.Trim(), DataOld["MA_THUE"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_THUE = " + txtma_thue.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_THUE",
                    txtma_thue.Text.Trim(), txtma_thue.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_THUE = " + txtma_thue.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

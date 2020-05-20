using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ChietKhauAddEditForm : AddEditControlVirtual
    {
        public override void DoBeforeEdit()
        {
            try
            {
                txtMa_loai_ck.ExistRowInTable();
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_CK", TXTma_ck.Text);
                TXTma_ck.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("ChietKhauAddEditForm DisableWhenEdit " + ex.Message);
            }
        }

        public override void DoBeforeAdd()
        {
            txtMa_loai_ck.ExistRowInTable();
        }

        public ChietKhauAddEditForm()
        {
            InitializeComponent();
        }
       
        public override void ValidateData()
        {
            var errors = "";
            if (TXTma_ck.Text.Trim() == "" || TXTten_ck.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_CK",
                    TXTma_ck.Text.Trim(), DataOld["MA_CK"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_CK = " + TXTma_ck.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_CK",
                    TXTma_ck.Text.Trim(), TXTma_ck.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_CK = " + TXTma_ck.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

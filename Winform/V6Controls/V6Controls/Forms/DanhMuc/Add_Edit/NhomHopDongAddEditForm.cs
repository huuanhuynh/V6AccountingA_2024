using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomHopDongAddEditForm : AddEditControlVirtual
    {
        public NhomHopDongAddEditForm()
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
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_NH", txtma_nh.Text);
                txtma_nh.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("NhomHopDongAddEditForm DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtma_nh.Text.Trim() == "" || txtten_nh.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_NH",
                    txtma_nh.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_NH = " + txtma_nh.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_NH",
                    txtma_nh.Text.Trim(), txtma_nh.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_NH = " + txtma_nh.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

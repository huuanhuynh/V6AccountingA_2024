using System;
using V6AccountingBusiness;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomVuViecAddEditForm : AddEditControlVirtual
    {
        public NhomVuViecAddEditForm()
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
                //var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_NH", Txtma_nh.Text);
                var v = Categories.IsExistAllCode_List(_MA_DM, "MA_NH", Txtma_nh.Text);
                Txtma_nh.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("NhomVuViecAddEditForm  DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_nh.Text.Trim() == "" || TxtTen_nh.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_NH",
                    Txtma_nh.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_NH = " + Txtma_nh.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_NH",
                    Txtma_nh.Text.Trim(), Txtma_nh.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_NH = " + Txtma_nh.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

using System;
using V6AccountingBusiness;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class PhiAddEditForm : AddEditControlVirtual
    {
        public PhiAddEditForm()
        {
            InitializeComponent();
            Txtnh_phi1.SetInitFilter("Loai_nh=1");
            Txtnh_phi2.SetInitFilter("Loai_nh=2");
            Txtnh_phi3.SetInitFilter("Loai_nh=3");

        }
        public override void DoBeforeEdit()
        {
            try
            {
                System.Collections.Generic.IDictionary<string, object> keys = new System.Collections.Generic.Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_PHI", TxtMa_phi.Text);
                TxtMa_phi.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("Phi DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_phi.Text.Trim() == "" || TxtTen_phi.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Structs.V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_PHI",
                    TxtMa_phi.Text.Trim(), DataOld["MA_PHI"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_PHI = " + TxtMa_phi.Text.Trim());
            }
            else if (Mode == V6Structs.V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_PHI",
                    TxtMa_phi.Text.Trim(), TxtMa_phi.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_PHI = " + TxtMa_phi.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

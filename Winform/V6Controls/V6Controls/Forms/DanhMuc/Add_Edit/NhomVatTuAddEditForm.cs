using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomVatTuAddEditForm : AddEditControlVirtual
    {
        public NhomVatTuAddEditForm()
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
                //var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_NH", txtMa_nh.Text);
                var v = Categories.IsExistAllCode_List(_MA_DM, "MA_NH", txtMa_nh.Text);
                txtMa_nh.Enabled = !v;
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("NhomVatTuAddEditForm DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_nh.Text.Trim() == "" || txtTen_nh.Text.Trim() == "")
                errors += V6Text.CheckInfor + " !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_NH",
                    txtMa_nh.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_NH = " + txtMa_nh.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_NH",
                    txtMa_nh.Text.Trim(), txtMa_nh.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.DataExist
                                        + "MA_NH = " + txtMa_nh.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

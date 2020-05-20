using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class BoPhanHachToanAddEditForm : AddEditControlVirtual
    {
        public BoPhanHachToanAddEditForm()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            try
            {
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_BPHT", txtMa_bpht.Text);
                txtMa_bpht.Enabled = !v;

                if (!V6Login.IsAdmin && TxtMa_dvcs.Text.ToUpper() != V6Login.Madvcs.ToUpper())
                {
                    TxtMa_dvcs.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_bpht.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaBpht.Text;
            if (txtten_bpht.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenBpht.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_BPHT", txtMa_bpht.Text.Trim(), DataOld["MA_BPHT"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaBpht.Text + "=" + txtMa_bpht.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_BPHT",
                 txtMa_bpht.Text.Trim(), txtMa_bpht.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaBpht.Text + "=" + txtMa_bpht.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

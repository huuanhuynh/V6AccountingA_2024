using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucCaLamViecForm : AddEditControlVirtual
    {
        public DanhMucCaLamViecForm()
        {
            InitializeComponent();

            MyInit();
        }

        public void MyInit()
        {
            //      txtMaNhCa.Enabled = false;
        }

        public override void DoBeforeEdit()
        {
            try
            {
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_NHCA", txtMaNhCa.Text);
                txtMaNhCa.Enabled = !v;

            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaNhCa.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaCa.Text;
            if (txtTenNhCa.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenCa.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_NHCA",
                 txtMaNhCa.Text.Trim(), DataOld["MA_NHCA"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaCa.Text + "=" + txtMaNhCa.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_NHCA",
                 txtMaNhCa.Text.Trim(), txtMaNhCa.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaCa.Text + "=" + txtMaNhCa.Text;
            }
            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

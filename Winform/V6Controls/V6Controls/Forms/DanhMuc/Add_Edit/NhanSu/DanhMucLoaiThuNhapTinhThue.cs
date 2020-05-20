using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;
using V6Tools;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucLoaiThuNhapTinhThue : AddEditControlVirtual
    {
        public DanhMucLoaiThuNhapTinhThue()
        {
            InitializeComponent();
            MyInit();
        }

        public void MyInit()
        {
       //     txtMaNhCa.Enabled = false;
        }

        public override void DoBeforeAdd()
        {
        }
        public override void DoBeforeEdit()
        {
            try
            {
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_DM", _MA_DM);
                var aldm = ConfigManager.GetAldmConfig(_MA_DM);
                var v = Categories.IsExistOneCode_List(aldm.F8_TABLE, "MA_LOAI_TN", txtMa_loai_tn.Text);
                txtMa_loai_tn.Enabled = !v;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BPHT DisableWhenEdit " + ex.Message);
            }
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_loai_tn.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaLoai.Text;
            if (txtTenLoaiTN.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenLoai.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_LOAI_TN", 
                 txtMa_loai_tn.Text.Trim(), DataOld["MA_LOAI_TN"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaLoai.Text + "=" + txtMa_loai_tn.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_LOAI_TN",
                    txtMa_loai_tn.Text.Trim(), txtMa_loai_tn.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaLoai.Text + "=" + txtMa_loai_tn.Text;
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

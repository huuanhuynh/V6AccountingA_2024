using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class SanPhamTrungGianAddEditForm : AddEditControlVirtual
    {
        public SanPhamTrungGianAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtma_vttg.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (txtten_vttg.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTen.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0, "MA_VTTG",
                 txtma_vttg.Text.Trim(), DataOld["MA_VTTG"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "=" + txtma_vttg.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "MA_VTTG",
                 txtma_vttg.Text.Trim(), txtma_vttg.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "=" + txtma_vttg.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

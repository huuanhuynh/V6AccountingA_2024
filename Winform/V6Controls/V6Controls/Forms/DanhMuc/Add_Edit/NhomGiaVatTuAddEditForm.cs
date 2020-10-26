using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomGiaVatTuAddEditForm : AddEditControlVirtual
    {
        public NhomGiaVatTuAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_nh.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaNhom.Text;
            if (txtten_nh.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblName.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_NH",
                 txtMa_nh.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaNhom.Text + "=" + txtMa_nh.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_NH",
                 txtMa_nh.Text.Trim(), txtMa_nh.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaNhom.Text + "=" + txtMa_nh.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }

}

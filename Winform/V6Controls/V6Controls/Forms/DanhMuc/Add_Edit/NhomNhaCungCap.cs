using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomNhaCungCap : AddEditControlVirtual
    {
        public NhomNhaCungCap()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaNh.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaNhKH.Text;
            if (txtTenNh.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenNh.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0, "MA_NH",
                 txtMaNh.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaNhKH.Text + "=" + txtMaNh.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "MA_NH",
                 txtMaNh.Text.Trim(), txtMaNh.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaNhKH.Text + "=" + txtMaNh.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

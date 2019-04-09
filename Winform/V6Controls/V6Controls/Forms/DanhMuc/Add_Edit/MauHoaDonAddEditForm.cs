using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class MauHoaDonAddEditForm : AddEditControlVirtual
    {
        public MauHoaDonAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaMauHD.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaMauHD.Text;
            if (txtTenMauHD.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenMauHD.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_MAUHD",
                 txtMaMauHD.Text.Trim(), DataOld["MA_MAUHD"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaMauHD.Text + "=" + txtMaMauHD.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_MAUHD",
                 txtMaMauHD.Text.Trim(), txtMaMauHD.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaMauHD.Text + "=" + txtMaMauHD.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

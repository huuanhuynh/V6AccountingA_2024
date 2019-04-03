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
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_MAUHD = " + txtMaMauHD.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_MAUHD",
                 txtMaMauHD.Text.Trim(), txtMaMauHD.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_MAUHD = " + txtMaMauHD.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

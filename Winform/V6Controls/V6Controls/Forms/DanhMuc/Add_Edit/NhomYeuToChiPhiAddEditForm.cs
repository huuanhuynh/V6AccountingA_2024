using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomYeuToChiPhiAddEditForm : AddEditControlVirtual
    {
        public NhomYeuToChiPhiAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtNhom.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (txtten_nhom.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "NHOM",
                 txtNhom.Text.Trim(), DataOld["NHOM"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "NHOM = " + txtNhom.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "NHOM",
                 txtNhom.Text.Trim(), txtNhom.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "NHOM = " + txtNhom.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);

        }
    }
}

using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomGiaKhachHangAddEditForm : AddEditControlVirtual
    {
        public NhomGiaKhachHangAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTMA_NH.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TXTten_nh.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NH",
                 TXTMA_NH.Text.Trim(), DataOld["MA_NH"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_NH = " + TXTMA_NH.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NH",
                 TXTMA_NH.Text.Trim(), TXTMA_NH.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_NH = " + TXTMA_NH.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

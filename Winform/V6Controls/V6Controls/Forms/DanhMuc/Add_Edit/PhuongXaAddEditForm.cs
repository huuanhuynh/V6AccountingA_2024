using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class PhuongXaAddEditForm : AddEditControlVirtual
    {
        public PhuongXaAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtma_phuong.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtten_ph.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_PHUONG",
                 txtma_phuong.Text.Trim(), DataOld["MA_PHUONG"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_PHUONG = " + txtma_phuong.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_PHUONG",
                 txtma_phuong.Text.Trim(), txtma_phuong.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_PHUONG = " + txtma_phuong.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}


using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class QuanHuyenAddEditForm : AddEditControlVirtual
    {
        public QuanHuyenAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTma_quan.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtten_quan.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_QUAN",
                 TXTma_quan.Text.Trim(), DataOld["MA_QUAN"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_QUAN = " + TXTma_quan.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_QUAN",
                 TXTma_quan.Text.Trim(), TXTma_quan.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_QUAN = " + TXTma_quan.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

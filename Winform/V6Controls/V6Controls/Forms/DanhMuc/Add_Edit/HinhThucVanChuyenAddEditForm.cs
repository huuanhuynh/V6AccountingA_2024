using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class HinhThucVanChuyenAddEditForm : AddEditControlVirtual
    {
        public HinhThucVanChuyenAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTma_htvc.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TXTten_htvc.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_HTVC",
                 TXTma_htvc.Text.Trim(), DataOld["MA_HTVC"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_HTVC = " + TXTma_htvc.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_HTVC",
                 TXTma_htvc.Text.Trim(), TXTma_htvc.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_HTVC = " + TXTma_htvc.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

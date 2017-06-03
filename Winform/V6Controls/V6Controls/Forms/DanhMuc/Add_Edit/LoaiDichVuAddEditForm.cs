using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class LoaiDichVuAddEditForm : AddEditControlVirtual
    {
        public LoaiDichVuAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
        }

      
        public override void ValidateData()
        {
            var errors = "";
            if (txtma_loai.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (txtten_loai.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_LOAI",
                 txtma_loai.Text.Trim(), DataOld["MA_LOAI"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: " + "MA_LOAI = " + txtma_loai.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_LOAI",
                 txtma_loai.Text.Trim(), txtma_loai.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_LOAI = " + txtma_loai.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

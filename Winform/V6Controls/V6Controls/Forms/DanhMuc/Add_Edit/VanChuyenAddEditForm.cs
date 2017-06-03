using System;
using V6AccountingBusiness;
using V6Structs;


namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class VanChuyenAddEditForm : AddEditControlVirtual
    {
        public VanChuyenAddEditForm()
        {
            InitializeComponent();
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtma_vc.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtten_vc.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_VC",
                 txtma_vc.Text.Trim(), DataOld["MA_VC"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_VC = " + txtma_vc.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_VC",
                 txtma_vc.Text.Trim(), txtma_vc.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_VC = " + txtma_vc.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

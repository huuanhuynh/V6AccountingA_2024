using V6Structs;
using System;
using V6AccountingBusiness;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucNhomCongForm : AddEditControlVirtual
    {
        public DanhMucNhomCongForm()
        {
            InitializeComponent();
            MyInit();
        }
        public void MyInit()
        {

            }
        public override void DoBeforeAdd()
        {
         
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtManhom.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtten_nhom.Text.Trim() == "")
                errors += "Chưa nhập tên!\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NHOM",
                 txtManhom.Text.Trim(), DataOld["MA_NHOM"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_NHOM = " + txtManhom.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NHOM",
                 txtManhom.Text.Trim(), txtManhom.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_NHOM = " + txtManhom.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

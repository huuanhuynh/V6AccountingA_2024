using V6Structs;
using System;
using V6AccountingBusiness;
using V6Init;

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
            if (txtMaNhom.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaNhom.Text;
            if (txtTenNhom.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenNhom.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NHOM",
                 txtMaNhom.Text.Trim(), DataOld["MA_NHOM"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_NHOM = " + txtMaNhom.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NHOM",
                 txtMaNhom.Text.Trim(), txtMaNhom.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_NHOM = " + txtMaNhom.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

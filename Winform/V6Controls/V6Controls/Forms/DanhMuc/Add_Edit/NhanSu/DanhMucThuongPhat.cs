using System.Data.SqlClient;
using V6Structs;
using System;
using V6Init;
using V6AccountingBusiness;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucThuongPhat : AddEditControlVirtual
    {
        public DanhMucThuongPhat()
        {
            InitializeComponent();
            MyInit();
        }
        public void MyInit()
        {

        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaLoai.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtTenLoai.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_LOAI",
                 txtMaLoai.Text.Trim(), DataOld["MA_LOAI"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_LOAI = " + txtMaLoai.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_LOAI",
                 txtMaLoai.Text.Trim(), txtMaLoai.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: " + "MA_LOAI = " + txtMaLoai.Text.Trim());
            }
            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

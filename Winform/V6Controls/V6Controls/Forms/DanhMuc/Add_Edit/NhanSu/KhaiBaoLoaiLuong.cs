using System.Data.SqlClient;
using V6AccountingBusiness;
using System;

using V6Structs;
using V6Init;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class KhaiBaoLoaiLuong : AddEditControlVirtual
    {
        public KhaiBaoLoaiLuong()
        {
            InitializeComponent();
            MyInit();
        }

        public void MyInit()
        {
       //     txtMaNhCa.Enabled = false;
        }

        public override void DoBeforeAdd()
        {
        }
        public override void DoBeforeEdit()
        {
            txtMa_loai_luong.ReadOnly = true;
            //txtManhCa.ExistRowInTable();
            //txtMaCong.ExistRowInTable();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_loai_luong.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtTenLoaiLg.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
             
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_LOAI_LG",
                 txtMa_loai_luong.Text.Trim(), DataOld["MA_LOAI_LG"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_LOAI_LG = " + txtMa_loai_luong.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_LOAI_LG",
                    txtMa_loai_luong.Text.Trim(), txtMa_loai_luong.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                        + "MA_LOAI_LG = " + txtMa_loai_luong.Text.Trim());
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

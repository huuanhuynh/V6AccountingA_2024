using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Structs;
using System;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class KhaiBaoKiTinhLuong : AddEditControlVirtual
    {
        public KhaiBaoKiTinhLuong()
        {
            InitializeComponent();
            MyInit();
        }
        public void MyInit()
        {

        }
        public override void DoBeforeAdd()
        {
            txtMaTg.ExistRowInTable();
        }
        public override void DoBeforeEdit()
        {
            txtMaTg.ExistRowInTable();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaTg.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
          

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_TG",
                 txtMaTg.Text.Trim(), DataOld["MA_TG"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_TG = " + txtMaTg.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_TG",
                 txtMaTg.Text.Trim(), txtMaTg.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TG = " + txtMaTg.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

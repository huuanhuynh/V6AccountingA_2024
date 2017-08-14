using V6AccountingBusiness;
using V6Structs;
using System;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucLoaiThuNhapTinhThue : AddEditControlVirtual
    {
        public DanhMucLoaiThuNhapTinhThue()
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
          //  txtMa_loai_tn.ReadOnly = true;
            //txtManhCa.ExistRowInTable();
            //txtMaCong.ExistRowInTable();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_loai_tn.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtTenLoaiTN.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {

                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_LOAI_TN", 
                 txtMa_loai_tn.Text.Trim(), DataOld["MA_LOAI_TN"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_LOAI_TN = " + txtMa_loai_tn.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_LOAI_TN",
                    txtMa_loai_tn.Text.Trim(), txtMa_loai_tn.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                        + "MA_LOAI_TN = " + txtMa_loai_tn.Text.Trim());
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

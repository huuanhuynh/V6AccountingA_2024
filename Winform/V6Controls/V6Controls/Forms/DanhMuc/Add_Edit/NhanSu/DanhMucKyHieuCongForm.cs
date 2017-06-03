using System.Data.SqlClient;
using V6AccountingBusiness;
using System;
using V6Structs;
using V6Init;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucKyHieuCongForm : AddEditControlVirtual
    {
        public DanhMucKyHieuCongForm()
        {
            InitializeComponent();
            MyInit();
        }

        public void MyInit()
        {
            txtMaCong.Enabled = false;
        }

        public override void DoBeforeAdd()
        {
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaCong.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtten_cong.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {

                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CONG",
                 txtMaCong.Text.Trim(), DataOld["MA_CONG"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_CONG = " + txtMaCong.Text.Trim());

                bool a = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CONG_IN",
               TXTma_cong_in.Text.Trim(), DataOld["MA_CONG_IN"].ToString());
                if (!a)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_CONG_IN = " + TXTma_cong_in.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CONG",
                    txtMaCong.Text.Trim(), txtMaCong.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                        + "MA_CONG = " + txtMaCong.Text.Trim());

                bool a = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CONG_IN",
                  TXTma_cong_in.Text.Trim(), txtMaCong.Text.Trim());
                if (!a)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                        + "MA_CONG_IN = " + TXTma_cong_in.Text.Trim());
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

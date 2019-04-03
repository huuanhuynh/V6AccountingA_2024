using V6AccountingBusiness;
using System;
using V6Init;
using V6Structs;

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
                errors += V6Text.Text("CHUANHAP") + " " + lblMaCong.Text;
            if (txtTenCong.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenCong.Text;

            if (Mode == V6Mode.Edit)
            {

                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CONG",
                 txtMaCong.Text.Trim(), DataOld["MA_CONG"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_CONG = " + txtMaCong.Text.Trim());

                bool a = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CONG_IN",
               txtMaCongIn.Text.Trim(), DataOld["MA_CONG_IN"].ToString());
                if (!a)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_CONG_IN = " + txtMaCongIn.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CONG",
                    txtMaCong.Text.Trim(), txtMaCong.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                        + "MA_CONG = " + txtMaCong.Text.Trim());

                bool a = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CONG_IN",
                  txtMaCongIn.Text.Trim(), txtMaCong.Text.Trim());
                if (!a)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                        + "MA_CONG_IN = " + txtMaCongIn.Text.Trim());
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

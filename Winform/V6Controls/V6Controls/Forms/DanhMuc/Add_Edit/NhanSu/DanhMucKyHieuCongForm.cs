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
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaCong.Text + "=" + txtMaCong.Text;

                bool a = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CONG_IN",
               txtMaCongIn.Text.Trim(), DataOld["MA_CONG_IN"].ToString());
                if (!a) errors += V6Text.DataExist + V6Text.EditDenied + lblMaCongIn.Text + "=" + txtMaCongIn.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CONG",
                    txtMaCong.Text.Trim(), txtMaCong.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaCong.Text + "=" + txtMaCong.Text;

                bool a = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CONG_IN",
                  txtMaCongIn.Text.Trim(), txtMaCong.Text.Trim());
                if (!a) errors += V6Text.DataExist + V6Text.AddDenied + lblMaCongIn.Text + "=" + txtMaCongIn.Text;
            }
            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

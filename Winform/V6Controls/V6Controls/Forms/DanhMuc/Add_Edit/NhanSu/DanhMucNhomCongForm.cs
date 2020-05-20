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
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_NHOM",
                 txtMaNhom.Text.Trim(), DataOld["MA_NHOM"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaNhom.Text + "=" + txtMaNhom.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_NHOM",
                 txtMaNhom.Text.Trim(), txtMaNhom.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaNhom.Text + "=" + txtMaNhom.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

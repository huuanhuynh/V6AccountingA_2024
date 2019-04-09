using V6AccountingBusiness;
using System;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucChiTietCaLamViecForm : AddEditControlVirtual
    {
        public DanhMucChiTietCaLamViecForm()
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
            txtManhCa.ExistRowInTable();
            txtMaCong.ExistRowInTable();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtManhCa.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaCa.Text;
            if (txtMaCong.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaCong.Text;
            if (txtTenCa.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenCa.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NHCA",
                    txtManhCa.Text.Trim(), DataOld["MA_NHCA"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaCa.Text + "=" + txtManhCa.Text;
                b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CA",
                    txtMa_ca.Text.Trim(), DataOld["MA_CA"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaCa.Text + "=" + txtMa_ca.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NHCA",
                 txtManhCa.Text.Trim(), txtManhCa.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaCa.Text + "=" + txtManhCa.Text;
                b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CA", txtMa_ca.Text.Trim(), txtMa_ca.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaCa.Text + "=" + txtMa_ca.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

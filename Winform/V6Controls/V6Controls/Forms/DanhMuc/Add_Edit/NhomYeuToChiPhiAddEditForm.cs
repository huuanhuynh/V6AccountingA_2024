using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhomYeuToChiPhiAddEditForm : AddEditControlVirtual
    {
        public NhomYeuToChiPhiAddEditForm()
        {
            InitializeComponent();
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtNhom.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblNhom.Text;
            if (txtten_nhom.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblName.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "NHOM",
                 txtNhom.Text.Trim(), DataOld["NHOM"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblNhom.Text + "=" + txtNhom.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "NHOM",
                 txtNhom.Text.Trim(), txtNhom.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblNhom.Text + "=" + txtNhom.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);

        }
    }
}

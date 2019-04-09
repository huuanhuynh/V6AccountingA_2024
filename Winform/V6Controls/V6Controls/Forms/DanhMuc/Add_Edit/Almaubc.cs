using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Almaubc : AddEditControlVirtual
    {
        public Almaubc()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            txtMaMauBc.Enabled = false;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaMauBc.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaMauBC.Text;
            if (txtTenMauBc.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenMauBC.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_MAUBC", txtMaMauBc.Text.Trim(), DataOld["MA_MAUBC"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaMauBC.Text + "=" + txtMaMauBc.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_MAUBC",
                 txtMaMauBc.Text.Trim(), txtMaMauBc.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaMauBC.Text + "=" + txtMaMauBc.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

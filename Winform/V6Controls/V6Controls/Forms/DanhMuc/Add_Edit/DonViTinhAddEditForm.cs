using System;
using System.Collections.Generic;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class DonViTinhAddEditForm : AddEditControlVirtual
    {
        public DonViTinhAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ALVT,ARI70", "Dvt", txtDVT.Text);
            txtDVT.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtDVT.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblDVT.Text;
            if (txtTenDVT.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenDVT.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0, "DVT", txtDVT.Text,
                    DataOld["DVT"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblDVT.Text + "=" + txtDVT.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "DVT", txtDVT.Text, "");
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblDVT.Text + "=" + txtDVT.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

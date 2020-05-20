using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NhaCungCapAddEditForm : AddEditControlVirtual
    {
        public NhaCungCapAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaKH.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaKH.Text;
            if (txtTenKH.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenKH.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0, "MA_KH",
                 txtMaKH.Text.Trim(), DataOld["MA_KH"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaKH.Text + "=" + txtMaKH.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "MA_KH",
                 txtMaKH.Text.Trim(), txtMaKH.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaKH.Text + "=" + txtMaKH.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }

}

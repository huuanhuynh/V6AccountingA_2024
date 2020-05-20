using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class PhuongXaAddEditForm : AddEditControlVirtual
    {
        public PhuongXaAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtma_phuong.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (txtten_ph.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblName.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0, "MA_PHUONG",
                 txtma_phuong.Text.Trim(), DataOld["MA_PHUONG"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "=" + txtma_phuong.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "MA_PHUONG",
                 txtma_phuong.Text.Trim(), txtma_phuong.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "=" + txtma_phuong.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

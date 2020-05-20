using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class YeuToChiPhiSPDHAddEditForm : AddEditControlVirtual
    {
        public YeuToChiPhiSPDHAddEditForm()
        {
            InitializeComponent();
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_ytcp.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMa.Text;
            if (txtTen_ytcp.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTen.Text;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 0, "MA_YTCP",
                 txtMa_ytcp.Text.Trim(), DataOld["MA_YTCP"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMa.Text + "=" + txtMa_ytcp.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM.ToString(), 1, "MA_YTCP",
                 txtMa_ytcp.Text.Trim(), txtMa_ytcp.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMa.Text + "=" + txtMa_ytcp.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void txtloai_pb_TextChanged(object sender, EventArgs e)
        {
            if (txtloai_pb.Text == "3")
            {
                txtDS_YTCP.ReadOnly = false;
            }
            else
            {
                txtDS_YTCP.ReadOnly = true;
                txtDS_YTCP.Text = "";
            }
        }
    }
}

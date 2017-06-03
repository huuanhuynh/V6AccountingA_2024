using System;
using V6AccountingBusiness;
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
                errors += "Chưa nhập mã!\r\n";
            if (txtTen_ytcp.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_YTCP",
                 txtMa_ytcp.Text.Trim(), DataOld["MA_YTCP"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_YTCP = " + txtMa_ytcp.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_YTCP",
                 txtMa_ytcp.Text.Trim(), txtMa_ytcp.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_YTCP = " + txtMa_ytcp.Text.Trim());
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

using System;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AlmaubcCt : AddEditControlVirtual
    {
        public AlmaubcCt()
        {
            InitializeComponent();
        }

        public override void DoBeforeEdit()
        {
            //txtMaMauBc.Enabled = false;
            txtkind.Value = txtcach_tinh.Text.Trim() == "" ? 1 : 0;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaSo.Text.Trim() == "")
                errors += "Chưa nhập mã số!\r\n";

            if (Mode == V6Mode.Edit)
            {
                //bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "LOAI_YT",
                // txtMaMauBc.Text.Trim(), DataOld["LOAI_YT"].ToString());
                //if (!b)
                //    throw new Exception("Không được sửa mã đã tồn tại: "
                //                                    + "LOAI_YT = " + txtMaMauBc.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                //bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "LOAI_YT",
                // txtMaMauBc.Text.Trim(), txtMaMauBc.Text.Trim());
                //if (!b)
                //    throw new Exception("Không được thêm mã đã tồn tại: "
                //                                    + "LOAI_YT = " + txtMaMauBc.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void txtkind_V6LostFocusNoChange(object sender)
        {
            

        }

        private void txtkind_TextChanged(object sender, EventArgs e)
        {
            if (txtkind.Value == 1)
            {
                txtcach_tinh.Text = "";
                txtcach_tinh.Enabled = false;
                txttk.Enabled = true;
            }
            else
            {
                txttk.Text = "";
                txttk.Enabled = false;
                txtcach_tinh.Enabled = true;
            }
        }

        private void AlmaubcCt_Load(object sender, EventArgs e)
        {
            var switch_text = "";
            if (txtMauBc.Text.Length >= 5) switch_text = txtMauBc.Text.Substring(0, 5);
            switch (switch_text)
            {
                case "GLTCB":
                    groupBoxC.Visible = false;
                    groupBoxB.Left = groupBox1.Left;
                    break;
                case "GLTCC":
                    groupBoxB.Visible = false;
                    groupBoxC.Left = groupBox1.Left;
                    break;
            }
        }
    }
}

using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class ChietKhauAddEditForm : AddEditControlVirtual
    {
        public ChietKhauAddEditForm()
        {
            InitializeComponent();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTma_ck.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TXTten_ck.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_CK",
                 TXTma_ck.Text.Trim(), DataOld["MA_CK"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_CK = " + TXTma_ck.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_CK",
                 TXTma_ck.Text.Trim(), TXTma_ck.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_CK = " + TXTma_ck.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

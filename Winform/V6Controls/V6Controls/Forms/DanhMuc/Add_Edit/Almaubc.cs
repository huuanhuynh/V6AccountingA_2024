using System;
using V6AccountingBusiness;
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
                errors += "Chưa nhập mã!\r\n";
            if (txtTenMauBc.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_MAUBC",
                 txtMaMauBc.Text.Trim(), DataOld["MA_MAUBC"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_MAUBC = " + txtMaMauBc.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_MAUBC",
                 txtMaMauBc.Text.Trim(), txtMaMauBc.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_MAUBC = " + txtMaMauBc.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

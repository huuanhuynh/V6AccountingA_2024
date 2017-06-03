using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class BoPhanHachToanAddEditForm : AddEditControlVirtual
    {
        public BoPhanHachToanAddEditForm()
        {
            InitializeComponent();
        }


        public override void ValidateData()
        {
            var errors = "";
            if (txtMa_bpht.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtten_bpht.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_BPHT",
                 txtMa_bpht.Text.Trim(), DataOld["MA_BPHT"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_BPHT = " + txtMa_bpht.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_BPHT",
                 txtMa_bpht.Text.Trim(), txtMa_bpht.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_BPHT = " + txtMa_bpht.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

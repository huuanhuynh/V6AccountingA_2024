using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TuDienNguoiDungDinhNghiaAddEditForm : AddEditControlVirtual
    {
        public TuDienNguoiDungDinhNghiaAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_td.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TxtTen_td.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_TD",
                 Txtma_td.Text.Trim(), DataOld["MA_TD"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TD = " + Txtma_td.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_TD",
                 Txtma_td.Text.Trim(), Txtma_td.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TD = " + Txtma_td.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

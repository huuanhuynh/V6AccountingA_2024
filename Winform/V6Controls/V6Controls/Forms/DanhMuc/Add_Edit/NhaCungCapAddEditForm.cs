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
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_KH",
                 txtMaKH.Text.Trim(), DataOld["MA_KH"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_KH = " + txtMaKH.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_KH",
                 txtMaKH.Text.Trim(), txtMaKH.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_KH = " + txtMaKH.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }

}

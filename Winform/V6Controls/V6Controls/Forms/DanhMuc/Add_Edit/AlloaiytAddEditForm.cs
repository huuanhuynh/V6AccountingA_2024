using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class AlloaiytAddEditForm : AddEditControlVirtual
    {
        public AlloaiytAddEditForm()
        {
            InitializeComponent();
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtLoai_yt.Text.Trim() == "")
                errors += "Chưa nhập loại !\r\n";
            if (txtghi_chu.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "LOAI_YT",
                 txtLoai_yt.Text.Trim(), DataOld["LOAI_YT"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "LOAI_YT = " + txtLoai_yt.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "LOAI_YT",
                 txtLoai_yt.Text.Trim(), txtLoai_yt.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "LOAI_YT = " + txtLoai_yt.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

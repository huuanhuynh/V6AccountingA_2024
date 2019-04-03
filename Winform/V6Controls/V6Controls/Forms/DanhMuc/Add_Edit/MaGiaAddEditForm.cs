using System;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class MaGiaAddEditForm : AddEditControlVirtual
    {
        public MaGiaAddEditForm()
        {
            InitializeComponent();
            txtMaGia0.SetInitFilter("loai='1'");
        }

        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ALKH,AM81", "MA_GIA", txtMaGia.Text);
            txtMaGia.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtMaGia.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaGia.Text;
            if (txtTenGia.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblDienGiai.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_GIA",
                 txtMaGia.Text.Trim(), DataOld["MA_GIA"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_GIA = " + txtMaGia.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_GIA",
                 txtMaGia.Text.Trim(), txtMaGia.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_GIA = " + txtMaGia.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

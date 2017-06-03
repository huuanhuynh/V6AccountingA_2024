using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class BoPhanSuDungTangSuatAddEditForm : AddEditControlVirtual
    {
        public BoPhanSuDungTangSuatAddEditForm()
        {
            InitializeComponent();
        }
         public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ALTS", "MA_BP", txtma_bp.Text);
            txtma_bp.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtma_bp.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (txtma_bp.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_BP",
                 txtma_bp.Text.Trim(), DataOld["MA_BP"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_BP = " + txtma_bp.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_BP",
                 txtma_bp.Text.Trim(), txtma_bp.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_BP = " + txtma_bp.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

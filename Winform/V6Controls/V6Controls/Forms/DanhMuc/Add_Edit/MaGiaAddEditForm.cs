using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class MaGiaAddEditForm : AddEditControlVirtual
    {
        public MaGiaAddEditForm()
        {
            InitializeComponent();
            Txtma_gia0.SetInitFilter("loai='1'");
        }

        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ALKH,AM81", "MA_GIA", TxtMa_gia.Text);
            TxtMa_gia.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (TxtMa_gia.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (txtten_gia.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_GIA",
                 TxtMa_gia.Text.Trim(), DataOld["MA_GIA"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_GIA = " + TxtMa_gia.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_GIA",
                 TxtMa_gia.Text.Trim(), TxtMa_gia.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_GIA = " + TxtMa_gia.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

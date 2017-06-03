using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class LyDoTangGiamCCDCAddEditForm : AddEditControlVirtual
    {
        public LyDoTangGiamCCDCAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ALCC", "MA_TG_CC", Txtma_tg_cc.Text);
            Txtma_tg_cc.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_tg_cc.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (TxtTen_tg_cc.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";
            if (txtloai_tg_cc.Text.Trim() == "")
                errors += "Chưa loại !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_TG_CC",
                 Txtma_tg_cc.Text.Trim(), DataOld["MA_TG_CC"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TG_CC = " + Txtma_tg_cc.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_TG_CC",
                 Txtma_tg_cc.Text.Trim(), Txtma_tg_cc.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_TG_CC = " + Txtma_tg_cc.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

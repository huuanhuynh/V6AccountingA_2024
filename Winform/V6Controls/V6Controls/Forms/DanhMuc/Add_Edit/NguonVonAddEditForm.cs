using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NguonVonAddEditForm : AddEditControlVirtual
    {
        public NguonVonAddEditForm()
        {
            InitializeComponent();
        }
        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ADALTS", "MA_NV", Txtma_nv.Text);
            Txtma_nv.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_nv.Text.Trim() == "")
                errors += "Chưa nhập mã !\r\n";
            if (Txtten_nv.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_NV",
                 Txtma_nv.Text.Trim(), DataOld["MA_NV"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_NV = " + Txtma_nv.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_NV",
                 Txtma_nv.Text.Trim(), Txtma_nv.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_NV = " + Txtma_nv.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }
    }
}

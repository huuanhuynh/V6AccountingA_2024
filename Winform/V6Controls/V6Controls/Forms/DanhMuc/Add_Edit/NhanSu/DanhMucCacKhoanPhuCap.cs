using V6Structs;
using System;
using V6AccountingBusiness;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucCacKhoanPhuCap : AddEditControlVirtual
    {
        public DanhMucCacKhoanPhuCap()
        {
            InitializeComponent();

            MyInit();
        }

        public void MyInit()
        {
            //      txtMaNhCa.Enabled = false;
        }

        public override void DoBeforeAdd()
        {
        }
        public override void ValidateData()
        {
            var errors = "";
            if (txtMaPC.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblMaPC.Text;
            if (txtTenPC.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAP") + " " + lblTenPC.Text;

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_PC",
                 txtMaPC.Text.Trim(), DataOld["MA_PC"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_PC = " + txtMaPC.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_PC",
                 txtMaPC.Text.Trim(), txtMaPC.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: " + "MA_PC = " + txtMaPC.Text.Trim());
            }
            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

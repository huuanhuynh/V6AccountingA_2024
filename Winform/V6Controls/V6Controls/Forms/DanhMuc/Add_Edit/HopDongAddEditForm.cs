using System;
using V6AccountingBusiness;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class HopDongAddEditForm : AddEditControlVirtual
    {
        public HopDongAddEditForm()
        {
            InitializeComponent();
        }


        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ARA00,ARI70", "MA_HD", Txtma_hd.Text);
            Txtma_hd.Enabled = !v;
        }

        public override void ValidateData()
        {
            var errors = "";
            if (Txtma_hd.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (txtTen_hd.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";
          
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_HD",
                 Txtma_hd.Text.Trim(), DataOld["MA_HD"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_HD = " + Txtma_hd.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_HD",
                 Txtma_hd.Text.Trim(), Txtma_hd.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_HD = " + Txtma_hd.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}

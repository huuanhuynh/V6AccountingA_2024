using System;
using V6AccountingBusiness;
using V6Structs;
namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class Aldm : AddEditControlVirtual
    {
        public Aldm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
           // txtval.Focus();
        }
        public override void DoBeforeEdit()
        {
            if (Mode == V6Mode.Edit)
            {
               
            }

        }
  
        public override void V6F3Execute()
        {
            
        }
        public override void ValidateData()
        {
            var errors = "";
            if (TXTMA_DM.Text.Trim() == "")
                errors += "Chưa nhập mã!\r\n";
            if (TXTTEN_DM.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_DM",
                 TXTMA_DM.Text.Trim(), DataOld["MA_DM"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_DM = " + TXTMA_DM.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_DM",
                 TXTMA_DM.Text.Trim(), TXTMA_DM.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_DM = " + TXTMA_DM.Text.Trim());
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

    }
}

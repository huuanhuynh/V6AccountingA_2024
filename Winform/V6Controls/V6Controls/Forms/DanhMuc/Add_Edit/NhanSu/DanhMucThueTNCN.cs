using V6AccountingBusiness;
using V6Structs;
using System;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class DanhMucThueTNCN : AddEditControlVirtual
    {
        public DanhMucThueTNCN()
        {
            InitializeComponent();
            MyInit();
        }

        public void MyInit()
        {
          //  txtMaCong.Enabled = false;
        }

        public override void DoBeforeAdd()
        {

        }

        public override void DoBeforeEdit()
        {
           
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtStt.Text.Trim() == "")
                errors += V6Text.ChuaNhapSTT;


            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "STT",
                 txtStt.Text.Trim(), DataOld["STT"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblSTT.Text + "=" + txtStt.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "STT",
                 txtStt.Text.Trim(), txtStt.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblSTT.Text + "=" + txtStt.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

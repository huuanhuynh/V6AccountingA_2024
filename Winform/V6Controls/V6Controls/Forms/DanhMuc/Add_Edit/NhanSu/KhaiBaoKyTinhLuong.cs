using V6AccountingBusiness;
using V6Structs;
using System;
using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class KhaiBaoKyTinhLuong : AddEditControlVirtual
    {
        public KhaiBaoKyTinhLuong()
        {
            InitializeComponent();
            MyInit();
        }
        public void MyInit()
        {

        }
        public override void DoBeforeAdd()
        {
            txtMaTg.ExistRowInTable();
        }
        public override void DoBeforeEdit()
        {
            txtMaTg.ExistRowInTable();
        }
        public override void DoBeforeView()
        {
            txtMaTg.ExistRowInTable();
        }
        public override void ValidateData()
        {
            var errors = "";

            if (txtNam.Value <= 0)
            {
                errors += V6Text.CheckInfor + " !\r\n";
            }
            if (txtMaTg.Text.Trim() == "")
                errors += V6Text.CheckInfor + "\r\n";
           
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_TG",
                 txtMaTg.Text.Trim(), DataOld["MA_TG"].ToString());
                if (!b) errors += V6Text.DataExist + V6Text.EditDenied + lblMaTg.Text + "=" + txtMaTg.Text;
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_TG",
                 txtMaTg.Text.Trim(), txtMaTg.Text.Trim());
                if (!b) errors += V6Text.DataExist + V6Text.AddDenied + lblMaTg.Text + "=" + txtMaTg.Text;
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        private void txtKy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {
                
            }
        }

        private void txtThag_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }

       

        private void txtKy_Leave(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 1;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }

        private void txtThag_Leave(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 1;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }

        private void txtNam_Leave(object sender, EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value <= 0) txt.Value = V6Setting.M_SV_DATE.Year;
            }
            catch (Exception)
            {

            }
        }
    }
}

using System;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class TinhTrangDichVuAddEditForm : AddEditControlVirtual
    {
        public TinhTrangDichVuAddEditForm()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtten_tt.Text.Trim() == "")
                errors += "Chưa nhập tên !\r\n";
            if (errors.Length > 0) throw new Exception(errors);
        }

     
    }
}

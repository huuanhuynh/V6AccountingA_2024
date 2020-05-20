using System.Collections.Generic;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NamTaiChinhAddEditForm : AddEditControlVirtual
    {
        public NamTaiChinhAddEditForm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
            
        }
        public override void DoBeforeEdit()
        {

        }
        public override void ValidateData()
        {
            
        }

        public override SortedDictionary<string, object> GetData()
        {
            TxtNam_bd.Value = TxtNgay_ky1.Date.Year;
            return base.GetData();
        }

        private void TxtNgay_ky1_Leave(object sender, System.EventArgs e)
        {
            TxtNam_bd.Value = TxtNgay_ky1.Date.Year;
        }
    }
}

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6SoftAddEditForm : AddEditControlVirtual
    {
        public V6SoftAddEditForm()
        {
            InitializeComponent();
        }

        private void KhachHangFrom_Load(object sender, System.EventArgs e)
        {
            txtval.Focus();
        }
        public override void DoBeforeEdit()
        {

        }
        public override void ValidateData()
        {
           
        }
    }
}

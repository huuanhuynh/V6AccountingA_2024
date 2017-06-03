using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6lookupForm : AddEditControlVirtual
    {
        public V6lookupForm()
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
        public override void ValidateData()
        {
           
        }
        public override void V6F3Execute()
        {
            
        }

    }
}

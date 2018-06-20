using System.Windows.Forms;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class V6Alct3AddEditForm : AddEditControlVirtual
    {
        public V6Alct3AddEditForm()
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
                var loai = DataOld["LOAI"].ToString();
                if(loai=="1")
                {
                    ChkVisible.Enabled = false;
                    TxtForder.Enabled = false;
                    TxtWidth.Enabled = false;

                }
            }

        }
        public override void ValidateData()
        {
           
        }
        public override void V6F3Execute()
        {
            if (f3count == 3)
            {
                f3count = 0;
                if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                {
                    ShowTopLeftMessage("V6 Confirm ......OK....");
                    ChkVisible.Enabled = true;
                    TxtForder.Enabled = true;
                    TxtWidth.Enabled = true;

                }
            }
            else
            {
                ChkVisible.Enabled = false;
                TxtForder.Enabled = false;
                TxtWidth.Enabled = false;
            }
        }
    }
}

using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NoRightAddEdit : AddEditControlVirtual
    {
        public string NoRightInfo { get { return txtNoRightInfo.Text; } set { txtNoRightInfo.Text = value; } }
        public NoRightAddEdit()
        {
            InitializeComponent();
        }
        public NoRightAddEdit(string text)
        {
            InitializeComponent();
            label1.Text = V6Text.NoRight;
            txtNoRightInfo.Text = (text);
        }
    }
}

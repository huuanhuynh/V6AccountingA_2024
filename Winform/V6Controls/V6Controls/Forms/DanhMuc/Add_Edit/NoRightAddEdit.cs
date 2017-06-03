using V6Init;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class NoRightAddEdit : AddEditControlVirtual
    {
        public NoRightAddEdit()
        {
            InitializeComponent();
        }
        public NoRightAddEdit(string text)
        {
            InitializeComponent();
            label1.Text = V6Text.NoRight;
            textBox1.Text = (text);
        }
    }
}

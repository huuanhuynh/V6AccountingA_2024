using V6Init;

namespace V6Controls.Forms
{
    public partial class V6UserControlNoRight : V6FormControl
    {
        public V6UserControlNoRight()
        {
            InitializeComponent();
        }
        public V6UserControlNoRight(string text)
        {
            InitializeComponent();
            label1.Text = V6Text.NotSupported;
            textBox1.Text = (text);
        }
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("", "ST2" + ItemID);
        }
    }
}

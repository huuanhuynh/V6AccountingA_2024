using System;
using System.Windows.Forms;
using V6Init;

namespace V6Controls.Forms
{
    public partial class V6UserControlEmpty : V6FormControl
    {
        public V6UserControlEmpty()
        {
            InitializeComponent();
        }
        public V6UserControlEmpty(string text)
        {
            InitializeComponent();
            label1.Text = V6Text.NotSupported;
            textBox1.Text = (text);
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                var p = Parent;
                Dispose();
                if (p is Form) p.Dispose();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage("Close " + ex.Message);
            }
        }
    }
}

using System;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormLogView : V6Form
    {

        public FormLogView()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = V6ControlFormHelper.LastActionListString;
            richTextBox2.Text = V6ControlFormHelper.LastErrorListString;
        }
    }
}

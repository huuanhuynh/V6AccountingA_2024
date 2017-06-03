using System.Windows.Forms;

namespace V6Controls
{
    public class V6LabelTextBox:TextBox
    {
        public V6LabelTextBox()
        {
            InitializeComponent();
            ParentChanged += V6LabelTextBox_ParentChanged;
            KeyDown += V6LabelTextBox_KeyDown;
        }

        void V6LabelTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
            }
        }

        void V6LabelTextBox_ParentChanged(object sender, System.EventArgs e)
        {
            if(Parent!=null) BackColor = Parent.BackColor;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // V6LabelTextBox
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReadOnly = true;
            this.Tag = "readonly";
            this.ResumeLayout(false);

        }
    }
}

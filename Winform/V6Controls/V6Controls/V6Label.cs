using System;
using System.Windows.Forms;
using V6Controls.Controls.Label;
using V6Init;

namespace V6Controls
{
    /// <summary>
    /// Kế thừa Label, thêm hàm gọi sự kiện Click (PerformClick)
    /// </summary>
    public class V6Label : Label
    {
        public V6Label()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gọi sự kiện Click
        /// </summary>
        public void PerformClick()
        {
            OnClick(new EventArgs()); //InvokeOnClick(this,new EventArgs());
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // V6Label
            // 
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.V6Label_MouseDoubleClick);
            this.ResumeLayout(false);

        }

        private void V6Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (V6Setting.V6Special.Contains("Triple"))
            if (e.Button == MouseButtons.Right && !string.IsNullOrEmpty(AccessibleDescription))
            {
                new FormChangeControlLanguageText(this).ShowDialog(this);
            }
        }
    }
}

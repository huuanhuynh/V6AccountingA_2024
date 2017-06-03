using System;
using System.Windows.Forms;

namespace V6Controls
{
    public class V6HiddenButton:Button
    {
        public V6HiddenButton()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // V6HiddenButton
            // 
            this.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UseVisualStyleBackColor = false;
            this.MouseLeave += new System.EventHandler(this.V6HiddenButton_MouseLeave);
            this.MouseEnter += new System.EventHandler(this.V6HiddenButton_MouseEnter);
            this.ResumeLayout(false);

        }

        private void V6HiddenButton_MouseEnter(object sender, EventArgs e)
        {
            this.FlatStyle = FlatStyle.Standard;
        }

        private void V6HiddenButton_MouseLeave(object sender, EventArgs e)
        {
            this.FlatStyle = FlatStyle.Flat;
        }
    }
}

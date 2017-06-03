using System;
using System.Windows.Forms;

namespace H_Controls.Controls
{
    public class IndexComboBox:ComboBox
    {
        public IndexComboBox()
        {
            KeyDown += ColorTextBox_KeyDown;
        }

        /// <summary>
        /// Gọi sự kiện click
        /// </summary>
        public void PerformClick()
        {
            OnClick(new EventArgs());
        }
        protected virtual void ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}

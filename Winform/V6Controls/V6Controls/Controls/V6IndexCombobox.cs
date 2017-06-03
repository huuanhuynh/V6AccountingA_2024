using System;
using System.Windows.Forms;

namespace V6Controls.Controls
{
    public class V6IndexComboBox:ComboBox
    {
        public V6IndexComboBox()
        {
            KeyDown += V6ColorTextBox_KeyDown;
        }
        public void PerformClick()
        {
            OnClick(new EventArgs());//InvokeOnClick(this,new EventArgs());
        }
        protected virtual void V6ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}

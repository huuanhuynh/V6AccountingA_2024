using System;
using System.Drawing;
using System.Windows.Forms;

namespace V6Controls
{
    /// <summary>
    /// Combobox dùng Datasource.
    /// </summary>
    public class V6ComboBox : ComboBox
    {
        public V6ComboBox()
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

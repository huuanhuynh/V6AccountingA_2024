using System;
using System.Windows.Forms;

namespace V6Controls
{
    public class V6CheckBox : CheckBox
    {
        public V6CheckBox()
        {
            KeyDown += SendTab_KeyDown;
        }
        /// <summary>
        /// Gọi sự kiện Click
        /// </summary>
        public void PerformClick()
        {
            OnClick(new EventArgs()); //InvokeOnClick(this,new EventArgs());
        }
        private bool _detroysenkey = false;
        public bool Carry;

        public void DestroySendkey()
        {
            _detroysenkey = true;
        }
        protected virtual void SendTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (_detroysenkey) return;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}

using System;
using System.Windows.Forms;

namespace H_Controls.Controls
{
    /// <summary>
    /// Kế thừa Label, thêm hàm gọi sự kiện Click (PerformClick)
    /// </summary>
    public class LabelH : Label
    {
        /// <summary>
        /// Gọi sự kiện Click
        /// </summary>
        public void PerformClick()
        {
            OnClick(new EventArgs()); //InvokeOnClick(this,new EventArgs());
        }
    }
}

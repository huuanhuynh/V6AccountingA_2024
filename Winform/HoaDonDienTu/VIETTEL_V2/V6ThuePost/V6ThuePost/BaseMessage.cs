﻿using System.Windows.Forms;

namespace V6ThuePost
{
    public static class BaseMessage
    {
        public static DialogResult Show(string text, int showTime = 0, IWin32Window owner = null)
        {
            var mbox = new BaseMessageForm(text, null, showTime);
            return mbox.ShowDialog(owner);
        }

        public static DialogResult Show(string text, string caption, int showTime = 0, IWin32Window owner = null)
        {
            var mbox = new BaseMessageForm(text, caption, showTime);
            return mbox.ShowDialog(owner);
        }

        public static DialogResult Show(string text, string caption, int showTime, MessageBoxButtons buttons, IWin32Window owner = null)
        {
            var mbox = new BaseMessageForm(text, caption, showTime, buttons);
            return mbox.ShowDialog(owner);
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="showTime">The time to display the message box by percent seconds.</param>
        /// <param name="buttons"/>One of the System.Windows.Forms.MessageBoxButtons values that specifies which
        ///      buttons to display in the message box.
        /// <param name="icon"></param>
        /// <param name="owner"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult Show(string text, string caption, int showTime, MessageBoxButtons buttons, MessageBoxIcon icon, IWin32Window owner = null)
        {
            var mbox = new BaseMessageForm(text, caption, showTime, buttons, icon);
            return mbox.ShowDialog(owner);
        }
        
        /// <summary>
        /// Hiển thị hộp thông báo.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="showTime">The time to display the message box by percent seconds.</param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton">0-Mặc định, 1-Nút thứ nhất, 2-Nút thứ hai</param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, int showTime, MessageBoxButtons buttons, MessageBoxIcon icon,
            int defaultButton, IWin32Window owner = null)
        {
            var mbox = new BaseMessageForm(text, caption, showTime, buttons, icon, defaultButton);
            return mbox.ShowDialog(owner);
        }
    }
}

using System.Windows.Forms;

namespace H_Controls
{
    public static class HMessage
    {
        public static DialogResult Show(string text)
        {
            return Show((IWin32Window)null, text);
        }
        
        public static DialogResult Show(IWin32Window owner, string text)
        {
            var mbox = new HMessageForm(text);
            return mbox.ShowDialog(owner);
        }
        
        public static DialogResult Show(string text, string caption)
        {
            var mbox = new HMessageForm(text, caption);
            return mbox.ShowDialog();
        }
        
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            var mbox = new HMessageForm(text, caption, buttons);
            return mbox.ShowDialog();
        }
        
        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons"/>One of the System.Windows.Forms.MessageBoxButtons values that specifies which
        ///      buttons to display in the message box.
        /// <param name="icon"></param>
        /// <returns>DialogResult</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var mbox = new HMessageForm(text, caption, buttons, icon);
            return mbox.ShowDialog();
        }
        
        
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
            int defaultButton)
        {
            var mbox = new HMessageForm(text, caption, buttons, icon, defaultButton);
            return mbox.ShowDialog();
        }
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon,
            int defaultButton)
        {
            var mbox = new HMessageForm(text, caption, buttons, icon, defaultButton);
            return mbox.ShowDialog();
        }
    }
}

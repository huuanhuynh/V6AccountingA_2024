using System.Windows.Forms;

namespace H_Controls
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        protected bool do_hot_key;
        public virtual void DoHotKey(Keys keyData)
        {
            try
            {
                do_hot_key = true;
                DoHotKey0(keyData);
            }
            catch
            {
                // ignored
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (do_hot_key)//chặn lại nếu gọi hotkey từ form cha.
                {
                    do_hot_key = false;
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                if (DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public virtual bool DoHotKey0(Keys keyData)
        {
            return HControlHelper.DoKeyCommand(this, keyData);
        }

    }
}

using System;
using System.Windows.Forms;

namespace V6Controls.Forms
{
    public partial class StringInput : V6Form
    {
        public StringInput()
        {
            InitializeComponent();
            txtInput.Focus();
        }
        
        public StringInput(string title, string value0)
        {
            InitializeComponent();
            Text = title;
            txtInput.Text = value0;
            txtInput.SelectionStart = txtInput.TextLength;
            txtInput.Focus();
        }

        /// <summary>
        /// Chuỗi đã nhập vào.
        /// </summary>
        public string InputString { get { return txtInput.Text; } }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DialogResult = DialogResult.OK;
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return base.DoHotKey0(keyData);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonCANCEL_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

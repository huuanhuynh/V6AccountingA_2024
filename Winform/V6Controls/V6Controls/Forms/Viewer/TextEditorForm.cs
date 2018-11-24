using System;
using System.Data;
using System.Windows.Forms;

namespace V6Controls.Forms.Viewer
{
    public partial class TextEditorForm : V6Form
    {
        public TextEditorForm()
        {
            InitializeComponent();
        }

        public TextEditorForm(string content, bool readOnly = false)
        {
            InitializeComponent();
            try
            {
                Content = content;
                richTextBox1.ReadOnly = readOnly;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("MyInit", ex);
            }
        }

        public string Content
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }
        
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public bool HaveChanged { get; set; }

        public event Action<Keys> HotKeyAction;
        protected virtual void OnHotKeyAction(Keys keyData)
        {
            var handler = HotKeyAction;
            if (handler != null) handler(keyData);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            else if (keyData == Keys.PageDown)
            {
                
            }
            else if (keyData == Keys.PageUp)
            {
                
            }

            OnHotKeyAction(keyData);
            return base.DoHotKey0(keyData);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            HaveChanged = true;
        }
        
    }
}

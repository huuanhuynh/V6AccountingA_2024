using System;
using System.Threading;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;

namespace V6Controls.Controls.Label
{
    internal partial class FormChangeControlLanguageText : Form
    {
        public FormChangeControlLanguageText()
        {
            InitializeComponent();
            MyInit();
        }
        public FormChangeControlLanguageText(Control label)
        {
            InitializeComponent();
            _label = label;
            MyInit();
        }

        private Control _label;
        public string NewText { get { return textBox1.Text; } }

        private void MyInit()
        {
            if (V6Setting.Language != "V")
            {
                Text = "Change text";
                label1.Text = "Text";
                button1.Text = "Ok";
            }
            textBox1.Text = _label.Text;
        }

        private void Accept()
        {
            try
            {
                _label.Text = NewText;
                updateText = NewText;
                updateId = _label.AccessibleDescription;
                var T = new Thread(UpdateDatabase);
                T.IsBackground = true;
                T.Start();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Accept", ex);
            }
        }

        private string updateText, updateId;


        private void UpdateDatabase()
        {
            try
            {
                if (CorpLan.Update(updateId, updateText))
                {
                    V6ControlFormHelper.SetStatusText(V6Text.UpdateSuccess);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".UpdateDatabase", ex);
            }
        }

        private void FormChangeV6LabelText_Load(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }  

        private void button1_Click(object sender, EventArgs e)
        {
            Accept();
            DialogResult = DialogResult.OK;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
              
    }
}

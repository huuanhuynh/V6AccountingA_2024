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
            //Thay đổi nội dung text bằng tiếng Anh
            if (V6Setting.Language != "V")
            {
                Text = "Change text";
                label1.Text = "Text";
                button1.Text = "Ok";

                columnHeader1.Text = "Name";
                columnHeader2.Text = "Value";
            }
            textBox1.Text = _label.Text;
            ShowControlInfo();
            if (V6Setting.IsVietnamese)
            {
                checkBox1.Checked = true;
                checkBox1.Visible = true;
            }
        }

        private void ShowControlInfo()
        {
            try
            {
                ListViewItem nameItem = new ListViewItem(new[] {V6Setting.IsVietnamese ? "Tên đối tượng" : "Object Name", _label.Name});
                listView1.Items.Add(nameItem);
                ListViewItem nameAccDes = new ListViewItem(new[] {"AccessibleDescription", _label.AccessibleDescription});
                listView1.Items.Add(nameAccDes);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowControlInfo", ex);
            }
        }

        private void Accept()
        {
            try
            {
                _label.Text = NewText;
                updateText = NewText;
                change_v = checkBox1.Checked ? "1" : "0";
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

        private string updateText, updateId, change_v;


        private void UpdateDatabase()
        {
            try
            {
                if (CorpLan.Update(updateId, V6Setting.Language, updateText, change_v))
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

        private void checkBox1_MouseMove(object sender, MouseEventArgs e)
        {
            V6ControlFormHelper.SetStatusText("Dịch tiếng Việt.");
        }
              
    }
}

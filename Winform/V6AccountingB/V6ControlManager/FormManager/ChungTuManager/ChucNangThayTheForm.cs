using System;
using System.Globalization;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class ChucNangThayTheForm : V6Form
    {
        private bool _isnum;
        private V6ColorTextBox _source_textbox;
        /// <summary>
        /// 1: Thay thế
        /// </summary>
        public readonly int _ThayThe = 1;
        /// <summary>
        /// 2: Đảo ngược
        /// </summary>
        public readonly int _DaoNguoc = 2;

        /// <summary>
        /// 1: Thay thế, 2: Đảo ngược
        /// </summary>
        public int ChucNangDaChon { get; private set; }
        public string Value { get { return textBox1.Text; } }
        public ChucNangThayTheForm()
        {
            InitializeComponent();
            MyInit();
        }
        public ChucNangThayTheForm(bool isnum, V6ColorTextBox sourceTextBox)
        {
            _isnum = isnum;
            _source_textbox = sourceTextBox;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                CreateCopyTextBox();
                if (_isnum)
                {
                    radDaoNguoc.Checked = true;
                    textBox1.Enabled = false;
                }
                else
                {
                    radThayThe.Checked = true;
                    radDaoNguoc.Enabled = false;
                    textBox1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void CreateCopyTextBox()
        {
            if (_source_textbox == null) return;
            try
            {
                if (_source_textbox is V6VvarTextBox)
                {
                    var source = _source_textbox as V6VvarTextBox;
                    V6VvarTextBox newTextBox = new V6VvarTextBox();
                    newTextBox.VVar = source.VVar;
                    newTextBox.Size = textBox1.Size;
                    newTextBox.Location = textBox1.Location;
                    newTextBox.TabIndex = textBox1.TabIndex;
                    newTextBox.TextChanged += delegate(object sender, EventArgs args)
                    {
                        textBox1.Text = newTextBox.Text;
                    };
                    newTextBox.Text = source.Text;
                    Controls.Add(newTextBox);
                    textBox1.Visible = false;
                    
                    textBox1.EnabledChanged += delegate(object sender, EventArgs args)
                    {
                        newTextBox.Enabled = textBox1.Enabled;
                    };
                }
                else if (_source_textbox is V6NumberTextBox)
                {
                    var source = _source_textbox as V6NumberTextBox;
                    V6NumberTextBox newTextBox = new V6NumberTextBox();
                    newTextBox.Size = textBox1.Size;
                    newTextBox.Location = textBox1.Location;
                    newTextBox.TabIndex = textBox1.TabIndex;
                    newTextBox.StringValueChange += delegate(object sender, StringValueChangeEventArgs args)
                    {
                        textBox1.Text = newTextBox.Value.ToString(CultureInfo.InvariantCulture);
                    };
                    newTextBox.Value = source.Value;
                    Controls.Add(newTextBox);
                    textBox1.Visible = false;
                    
                    textBox1.EnabledChanged += delegate(object sender, EventArgs args)
                    {
                        newTextBox.Enabled = textBox1.Enabled;
                    };
                }
                else if (_source_textbox is V6DateTimeColor)
                {
                    var source = _source_textbox as V6DateTimeColor;
                    V6DateTimeColor newTextBox = new V6DateTimeColor();
                    newTextBox.Size = textBox1.Size;
                    newTextBox.Location = textBox1.Location;
                    newTextBox.TabIndex = textBox1.TabIndex;
                    newTextBox.TextChanged += delegate(object sender, EventArgs args)
                    {
                        textBox1.Text = newTextBox.Text;
                    };
                    newTextBox.Value = source.Value;
                    Controls.Add(newTextBox);
                    textBox1.Visible = false;

                    textBox1.EnabledChanged += delegate(object sender, EventArgs args)
                    {
                        newTextBox.Enabled = textBox1.Enabled;
                    };
                }
                else
                {
                    textBox1.Text = _source_textbox.Text;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateCopyTextBox", ex);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radThayThe.Checked)
            {
                ChucNangDaChon = _ThayThe;
                textBox1.Enabled = true;
            }
            else if (radDaoNguoc.Checked)
            {
                ChucNangDaChon = _DaoNguoc;
                textBox1.Enabled = false;
            }
        }

    }
}

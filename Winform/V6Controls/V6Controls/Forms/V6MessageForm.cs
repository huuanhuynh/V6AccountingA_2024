using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    internal partial class V6MessageForm : V6Form
    {
        public V6MessageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khởi tạo hộp thông báo.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="showTime">Thời gian hiển thị tính bằng phần trăm giây.</param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton">0-Mặc định, 1-Nút thứ nhất, 2-Nút thứ hai</param>
        public V6MessageForm(string text, string caption = null, int showTime = 0,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            int defaultButton = 0)
        {
            InitializeComponent();
            _text = text;
            _caption = caption;
            _showTime = showTime;
            _buttons = buttons;
            _icon = icon;
            _defaultButton = defaultButton;
            MyInit();
        }

        private readonly string _text;
        private string _caption;
        private readonly MessageBoxButtons _buttons;
        private readonly MessageBoxIcon _icon;
        private readonly int _defaultButton;
        private Button _buttonDefault;
        private readonly int _showTime;

        private void MyInit()
        {
            if (V6Setting.Language == "V")
            {
                buttonYES.Text = "&Có";
                buttonNO.Text = "&Không";
                buttonOK.Text = "&Nhận";
                buttonCANCEL.Text = "&Hủy";
            }
            else
            {
                buttonYES.Text = "&Yes";
                buttonNO.Text = "&No";
                buttonOK.Text = "&Ok";
                buttonCANCEL.Text = "&Cancel";
            }
            //pictureBox1.Image = System.Drawing.SystemIcons.Error;
            switch (_buttons)
            {
                case MessageBoxButtons.OK:
                    buttonOK.Visible = true;
                    _buttonDefault = buttonOK;
                    break;
                case MessageBoxButtons.OKCancel:
                    buttonOK.Visible = true;
                    buttonCANCEL.Visible = true;

                    if (_defaultButton == 1)
                    {
                        buttonOK.Select();
                        _buttonDefault = buttonOK;
                    }
                    else
                    {
                        buttonCANCEL.Select();
                        _buttonDefault = buttonCANCEL;
                    }
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    break;
                case MessageBoxButtons.YesNoCancel:
                    buttonYES.Visible = true;
                    buttonYES.Left = button1.Left;
                    buttonNO.Visible = true;
                    buttonNO.Left = button2.Left;
                    buttonCANCEL.Visible = true;
                    buttonCANCEL.Left = button3.Left;

                    if (_defaultButton == 1)
                    {
                        buttonYES.Select();
                        _buttonDefault = buttonYES;
                    }
                    else if (_defaultButton == 2)
                    {
                        buttonNO.Select();
                        _buttonDefault = buttonNO;
                    }
                    else
                    {
                        buttonCANCEL.Select();
                        _buttonDefault = buttonCANCEL;
                    }
                    break;

                case MessageBoxButtons.YesNo:
                    buttonYES.Visible = true;
                    buttonNO.Visible = true;

                    if (_defaultButton == 1)
                    {
                        buttonYES.Select();
                        _buttonDefault = buttonYES;
                    }
                    else
                    {
                        buttonNO.Select();
                        _buttonDefault = buttonNO;
                    }
                    break;
                case MessageBoxButtons.RetryCancel:
                    break;
                
            }

            switch (_icon)
            {
                case MessageBoxIcon.None:
                    pictureBox1.Visible = false;
                    lblMessage.Left = pictureBox1.Left;
                    if (_caption == null) _caption = V6Setting.Language == "V" ? "Thông báo" : "Message";
                    break;
                case MessageBoxIcon.Error://Stop
                    pictureBox1.Image = Properties.Resources.Error_48x48_72;
                    if (_caption == null) _caption = V6Setting.Language == "V" ? "Lỗi!" : "Error!";
                    break;
                case MessageBoxIcon.Question:
                    pictureBox1.Image = Properties.Resources.Question;
                    if (_caption == null) _caption = V6Setting.Language == "V" ? "Xác nhận" : "Confirm";
                    break;
                case MessageBoxIcon.Warning:
                    pictureBox1.Image = Properties.Resources.Warning_48x48_72;
                    if (_caption == null) _caption = V6Setting.Language == "V" ? "Cảnh báo!" : "Warning!";
                    break;
                case MessageBoxIcon.Information:
                    pictureBox1.Image = Properties.Resources.Info_48x48_72;
                    if (_caption == null) _caption = V6Setting.Language == "V" ? "Thông tin!" : "Information!";
                    break;
            }

            Text = _caption;
            lblMessage.Text = _text;
            if (lblMessage.Height > 30)
            {
                lblMessage.Top -= 10;
            }
            if (_buttons == MessageBoxButtons.OK)
            {
                buttonOK.Left = Width / 2 - buttonOK.Width / 2;
            }
            if (lblMessage.Bottom > panel1.Top)
            {
                panel1.Top = lblMessage.Top + lblMessage.Height;
            }

            if (_showTime > 0)
            {
                timer1.Start();
            }
        }

        private void V6MessageForm_Load(object sender, System.EventArgs e)
        {
            //V6ControlFormHelper.ApplyControlTripleClick(lblMessage.Parent);
        }

        Point old_right_mouseup_location = new Point();
        private int right_count;
        private void lblMessage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Location == old_right_mouseup_location)
                {
                    right_count++;
                    if (right_count == 3)
                    {
                        DoAddEditCorplan();
                    }
                }
                else
                {
                    old_right_mouseup_location = e.Location;
                    right_count = 1;
                }
            }
            else
            {
                old_right_mouseup_location = new Point();
                right_count = 0;
            }
        }

        private void DoAddEditCorplan()
        {
            try
            {
                if (string.IsNullOrEmpty(lblMessage.AccessibleDescription) || lblMessage.AccessibleDescription == ".") return;
                if (new ConfirmPasswordV6().ShowDialog(this) != DialogResult.OK) return;

                IDictionary<string, object> key = new Dictionary<string, object>();
                string ID = lblMessage.AccessibleDescription;
                key["ID"] = ID;

                FormAddEdit form;
                if (V6BusinessHelper.CheckDataExist("CorpLan", key))
                {
                    form = new FormAddEdit("CorpLan", V6Mode.Edit, key, null);
                }
                else
                {
                    IDictionary<string, object> data = new Dictionary<string, object>(key);
                    data["D"] = lblMessage.Text;
                    data["V"] = lblMessage.Text;
                    data["E"] = lblMessage.Text;
                    if (ID.Length > 9)
                    {
                        data["SFILE"] = ID.Substring(0, ID.Length - 9);
                        data["CTYPE"] = ID.Substring(data["SFILE"].ToString().Length, 1);
                    }
                    form = new FormAddEdit("CorpLan", V6Mode.Add, null, data);
                }

                form.InitFormControl(this);
                (form.FormControl as CorpLanAddEditForm).AutoID = false;
                (form.FormControl as CorpLanAddEditForm).LockID(true);
                form.ShowDialog(this);
                if (form.UpdateSuccess || form.InsertSuccess)
                {
                    lblMessage.Text = form.Data[V6Login.SelectedLanguage].ToString();
                    if (V6Setting.IsVietnamese && !ObjectAndString.ObjectToBool(form.Data["CHANGE_V"]))
                    {
                        CorpLan.RemoveText(ID);
                    }
                    else
                    {
                        CorpLan.SetText(ID, lblMessage.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(ex);
            }
        }

        public override void DoHotKey(Keys keyData)
        {
            try
            {
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
                if (DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Sẽ click lên button hoặc V6label có Tag = keyData.ToString()
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns>true nếu có lick/ false nếu không</returns>
        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return V6ControlFormHelper.DoKeyCommand(this, keyData);
        }

        private int _showTimeCount;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            _showTimeCount ++;
            Opacity = 1d - (double)_showTimeCount/_showTime;
            if (_showTimeCount >= _showTime)
            {
                timer1.Stop();
                if(_buttonDefault != null) _buttonDefault.PerformClick();
                //DialogResult = DialogResult.Cancel;
            }
        }

        
    }
}

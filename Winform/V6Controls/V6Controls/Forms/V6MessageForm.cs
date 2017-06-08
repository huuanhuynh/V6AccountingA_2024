using System.Windows.Forms;
using V6Init;

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
        private int _showTime = 0;

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
                    break;
                case MessageBoxButtons.OKCancel:
                    buttonOK.Visible = true;
                    buttonCANCEL.Visible = true;

                    if (_defaultButton == 1)
                        buttonOK.Select();
                    else buttonCANCEL.Select();
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

                    if (_defaultButton == 1) buttonYES.Select();
                    else if (_defaultButton == 2) buttonNO.Select();
                    else buttonCANCEL.Select();
                    break;

                case MessageBoxButtons.YesNo:
                    buttonYES.Visible = true;
                    buttonNO.Visible = true;

                    if (_defaultButton == 1)
                        buttonYES.Select();
                    else buttonNO.Select();
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

        public override void DoHotKey(Keys keyData)
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
                //Nếu đã thực hiện lệnh bên trên gửi xuống thì không chạy DoHotKey0
                if (do_hot_key)
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

        /// <summary>
        /// Sẽ click lên button hoặc V6label có Tag = keyData.ToString()
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns>true nếu có lick/ false nếu không</returns>
        public override bool DoHotKey0(Keys keyData)
        {
            return V6ControlFormHelper.DoKeyCommand(this, keyData);
        }

        private int _showTimeCount = 0;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            _showTimeCount ++;
            Opacity = 1d - (double)_showTimeCount/_showTime;
            if (_showTimeCount >= _showTime)
            {
                timer1.Stop();
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}

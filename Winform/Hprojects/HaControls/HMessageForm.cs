using System.Windows.Forms;

namespace H_Controls
{
    internal partial class HMessageForm : Form
    {
        public HMessageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton">0-Mặc định, 1-Nút thứ nhất, 2-Nút thứ hai</param>
        public HMessageForm(string text, string caption = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            int defaultButton = 0)
        {
            InitializeComponent();
            _text = text;
            _caption = caption;
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

        private string Language = "V";

        private void MyInit()
        {
            if (Language == "V")
            {
                buttonYES.Text = "Có";
                buttonNO.Text = "Không";
                buttonOK.Text = "Nhận";
                buttonCANCEL.Text = "Hủy";
            }
            else
            {
                buttonYES.Text = "Yes";
                buttonNO.Text = "No";
                buttonOK.Text = "Ok";
                buttonCANCEL.Text = "Cancel";
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
                    if (_caption == null) _caption = Language == "V" ? "Thông báo" : "Message";
                    break;
                case MessageBoxIcon.Error://Stop
                    pictureBox1.Image = Properties.Resources.Error_48x48_72;
                    if (_caption == null) _caption = Language == "V" ? "Lỗi!" : "Error!";
                    break;
                case MessageBoxIcon.Question:
                    pictureBox1.Image = Properties.Resources.Question;
                    if (_caption == null) _caption = Language == "V" ? "Xác nhận" : "Confirm";
                    break;
                case MessageBoxIcon.Warning:
                    pictureBox1.Image = Properties.Resources.Warning_48x48_72;
                    if (_caption == null) _caption = Language == "V" ? "Cảnh báo!" : "Warning!";
                    break;
                case MessageBoxIcon.Information:
                    pictureBox1.Image = Properties.Resources.Info_48x48_72;
                    if (_caption == null) _caption = Language == "V" ? "Thông tin!" : "Information!";
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
        }

    }
}

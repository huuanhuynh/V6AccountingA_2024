using System.Windows.Forms;
using V6Init;

namespace V6Controls.Forms
{
    public partial class V6TopMessageForm : Form
    {
        //const int WS_EX_NOACTIVATE = 0x8000000;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams ret = base.CreateParams;
        //        ret.ExStyle |= WS_EX_NOACTIVATE;
        //        return ret;
        //    }
        //}

        public string Message {
            get { return lblMessage.Text; }
            set
            {
                lblMessage.Text = value;
                _showTimeCount = 0;
                timer1.Start();
                ShowToScreen();
            }
        }

        public V6TopMessageForm()
        {
            InitializeComponent();
            MakeItCenter();
        }

        /// <summary>
        /// Khởi tạo hộp thông báo.
        /// </summary>
        /// <param name="text"></param>
        public V6TopMessageForm(string text)
        {
            InitializeComponent();
            MakeItCenter();
            lblMessage.Text = text;
        }

        private void MakeItCenter()
        {
            Left = Screen.PrimaryScreen.WorkingArea.Width/2 - Width/2;
        }

        private int _showTime = 500; // 5 sec
        private int _showTimeCount = 0;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            _showTimeCount ++;
            Opacity = 1d - (double)_showTimeCount/_showTime;
            if (_showTimeCount >= _showTime)
            {
                timer1.Stop();
                //Dispose();
                //Visible = false;
                HideToScreen();
            }
        }

        private void HideToScreen()
        {
            Top = -Height;
        }

        private void ShowToScreen()
        {
            Top = 0;
        }

        private void V6TopMessageForm_Load(object sender, System.EventArgs e)
        {
            
        }
    }
}

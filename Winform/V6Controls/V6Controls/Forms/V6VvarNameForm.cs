using System.Drawing;
using System.Windows.Forms;

namespace V6Controls.Forms
{
    public partial class V6VvarNameForm : Form
    {
        public string Message {
            get { return lblMessage.Text; }
            set
            {
                lblMessage.Text = value;
                FixSize();
                _showTimeCount = 0;
                timer1.Start();
                ShowToScreen();
                MoveToVvarTextBox();
            }
        }

        private void FixSize()
        {
            
        }

        public V6VvarTextBox VvarTextBox { get; set; }

        public V6VvarNameForm()
        {
            InitializeComponent();
            MoveToVvarTextBox();
        }

        /// <summary>
        /// Khởi tạo hộp thông báo.
        /// </summary>
        /// <param name="text"></param>
        public V6VvarNameForm(string text)
        {
            InitializeComponent();
            lblMessage.Text = text;
            MoveToVvarTextBox();
        }

        private void MoveToVvarTextBox()
        {
            if (VvarTextBox == null) return;
            var screenLocation = VvarTextBox.PointToClient(new Point(0, 0));
            screenLocation = new Point(0-screenLocation.X, 0-screenLocation.Y);
            int left = screenLocation.X + 20;
            if (left + Width > Screen.PrimaryScreen.WorkingArea.Width)
                left -= left + Width - Screen.PrimaryScreen.WorkingArea.Width;
            Left = left;
            Top = screenLocation.Y - Height;
        }

        /// <summary>
        /// Ngừng hiển thị.
        /// </summary>
        public void StopShow()
        {
            _showTimeCount = _showTime-1;
        }
        private int _showTime = 500; // 5 sec
        private int _showTimeCount;
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            _showTimeCount++;
            Opacity = 1d - (double) _showTimeCount/_showTime;
            if (_showTimeCount >= _showTime)
            {
                timer1.Stop();
                HideToScreen();
            }
        }

        private void HideToScreen()
        {
            Top = -Height;
        }

        private void ShowToScreen()
        {
            
        }

        private void V6VvarNameForm_Load(object sender, System.EventArgs e)
        {
            
        }

        private void V6VvarNameForm_MouseMove(object sender, MouseEventArgs e)
        {
            MoveHole();
        }

        private void V6VvarNameForm_MouseDown(object sender, MouseEventArgs e)
        {
            MoveHole();
        }

        private void MoveHole()
        {
            var newLocation = PointToClient(MousePosition);
            lblHole.Location = newLocation;
        }

        private void lblMessage_ClientSizeChanged(object sender, System.EventArgs e)
        {
            Width = lblMessage.Width + 5;
        }
    }
}

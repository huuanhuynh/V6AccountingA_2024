using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace H_Controls
{
    public partial class ShardowFormPopup : Form
    {
        public ShardowFormPopup() 
        {
            InitializeComponent();
            MyInitForDesignView();
        }
        //public ShardowFormPopup(Control control)
        //{
        //    InitializeComponent();
        //    MyInitLocation(control);
        //}
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int LPAR);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        private void move_window(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

    }
}

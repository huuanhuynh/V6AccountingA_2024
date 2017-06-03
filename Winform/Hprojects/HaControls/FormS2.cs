using System;
using System.Drawing;
using System.Windows.Forms;

namespace H_Controls
{
    public partial class FormS2 : Form
    {
        public FormS2()
        {
            InitializeComponent();
        }

        bool enableResize = false;
        ResizeMode resizeMode = ResizeMode.NWSE;
        Point mouseDownP = new Point();
        Size oldFormSize = new Size();
        
        private void bBottomRight_MouseDown(object sender, MouseEventArgs e)
        {
            enableResize = true;
            resizeMode = ResizeMode.NWSE;
            mouseDownP = Cursor.Position;
            oldFormSize = this.Size;
            timerResize.Start();
        }
        int xAdd = 0, yAdd = 0;
        Point currentMouse = new Point();
        private void timerResize_Tick(object sender, EventArgs e)
        {
            if (enableResize)
            {
                currentMouse = Cursor.Position;
                if (resizeMode == ResizeMode.NWSE || resizeMode == ResizeMode.WE)
                {
                    xAdd = currentMouse.X - mouseDownP.X;
                }
                else
                {
                    xAdd = 0;
                }
                if (resizeMode == ResizeMode.NWSE || resizeMode == ResizeMode.NS)
                {
                    yAdd = currentMouse.Y - mouseDownP.Y;
                }
                else
                {
                    yAdd = 0;
                }

                Size newSize = new Size(oldFormSize.Width + xAdd, oldFormSize.Height + yAdd);
                if (newSize.Width < 100) newSize.Width = 100;
                if (newSize.Height < 100) newSize.Height = 100;

                this.Size = newSize;
            }
        }

        private void FormS_Resize(object sender, EventArgs e)
        {
            bBottomRight.Left = this.Width - bBottomRight.Width;
            bBottomRight.Top = this.Height - bBottomRight.Height;
        }

        private void StopResize()
        {
            timerResize.Stop();
            enableResize = false;
        }

        private void bBottomRight_MouseUp(object sender, MouseEventArgs e)
        {
            StopResize();
        }

        private void bBottomRight_MouseLeave(object sender, EventArgs e)
        {
            //StopResize();
        }

        private void bRight_MouseDown(object sender, MouseEventArgs e)
        {
            enableResize = true;
            resizeMode = ResizeMode.WE;
            mouseDownP = Cursor.Position;
            oldFormSize = this.Size;
            timerResize.Start();
        }

        private void bBottom_MouseDown(object sender, MouseEventArgs e)
        {
            enableResize = true;
            resizeMode = ResizeMode.NS;
            mouseDownP = Cursor.Position;
            oldFormSize = this.Size;
            timerResize.Start();
        }

        private void bRight_MouseUp(object sender, MouseEventArgs e)
        {
            StopResize();
        }

        private void bBottom_MouseUp(object sender, MouseEventArgs e)
        {
            StopResize();
        }
    }

    
}

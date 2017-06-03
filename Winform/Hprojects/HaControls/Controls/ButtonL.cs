using System;
using System.ComponentModel;
using System.Drawing;

namespace H_Controls.Controls
{
    public class ButtonL:LabelH
    {
        protected bool autosize = false;
        [DefaultValue(false)]
        public override bool AutoSize
        {
            get
            {
                return autosize;
            }
            set
            {
                autosize = value;
            }
        }
        Color hover = Color.Aqua, leave = Color.Transparent, click = Color.Blue;

        [Category("H")]//[DefaultValue(typeof(Color), "0xFF0000")]
        public Color HoverColor
        {
            get { return hover; }
            set { hover = value; }
        }
        [Category("H")]
        public Color LeaveColor
        {
            get { return leave; }
            set { leave = value; }
        }
        public Color ClickColor
        {
            get { return click; }
            set { click = value; }
        }

        public ButtonL()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HLabelButton
            // 
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HLabelButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.HLabelButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.HLabelButton_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HLabelButton_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HLabelButton_MouseUp);
            this.ResumeLayout(false);

        }

        private void HLabelButton_MouseEnter(object sender, EventArgs e)
        {
            BackColor = hover;
        }

        private void HLabelButton_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            BackColor = hover;
        }

        private void HLabelButton_MouseLeave(object sender, EventArgs e)
        {
            BackColor = leave;
        }

        private void HLabelButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            BackColor = click;
        }

        private void HLabelButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            BackColor = hover;
        }


        //public event EventHandler Click;
    }
}

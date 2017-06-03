using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class rptPicture: Panel
    {
        #region ==== Object Name ====
        [Category("_crProperty")]
        [Description("Tên đối tượng")]
        public string ObjectName
        {
            get
            {
                return _obj.Name;
            }
        }
        #endregion
        #region ==== Suppress (Ẩn) ====
        Color oldColor;
        [Category("_crProperty")]
        [Description("Ẩn đi")]
        public bool Suppress
        {
            get { return _obj.ObjectFormat.EnableSuppress; }
            set
            {

                if (value != _obj.ObjectFormat.EnableSuppress)
                {
                    _obj.ObjectFormat.EnableSuppress = value;
                    if (value)
                    {
                        oldColor = this.label.ForeColor;
                        this.label.ForeColor = Color.LightGray;
                    }
                    else
                    {
                        this.label.ForeColor = oldColor;
                    }
                    if(SuppressChanged!=null)
                        SuppressChanged(this, new EventArgs());
                    
                }
            }
        }
        public delegate void OnSuppressChanged(object sender, EventArgs e);
        public event OnSuppressChanged SuppressChanged;
        #endregion

        Label label = new Label();
        //PictureObject _obj;
        ReportObject _obj;
        public rptPicture(PictureObject obj)
        {
            _obj = obj;
            this.Name = obj.Name;
            this.Location = new Point(Twip.TwipToPixelX(obj.Left), Twip.TwipToPixelY(obj.Top));
            this.AutoSize = false;
            this.Size = new Size(Twip.TwipToPixelX(obj.Width), Twip.TwipToPixelY(obj.Height));
            this.BackColor = obj.Border.BackgroundColor;
            this.Tag = this.BackColor;

            if (obj.ObjectFormat.EnableSuppress)
                this.ForeColor = Color.LightGray;
                        
            label.AutoSize = true;
            label.Location = new Point(2, 2);
            label.Text = "Hình ảnh";
            this.Controls.Add(label);
            
            this.ApplyEvent();
        }
        /// <summary>
        /// Dùng thay cho BlobFieldObject
        /// </summary>
        public rptPicture(ReportObject obj, string text)
        {
            _obj = obj;
            this.Name = obj.Name;
            this.Location = new Point(Twip.TwipToPixelX(obj.Left), Twip.TwipToPixelY(obj.Top));
            this.AutoSize = false;
            this.Size = new Size(Twip.TwipToPixelX(obj.Width), Twip.TwipToPixelY(obj.Height));
            this.BackColor = obj.Border.BackgroundColor;
            this.Tag = this.BackColor;

            if (obj.ObjectFormat.EnableSuppress)
                this.ForeColor = Color.LightGray;

            label.AutoSize = true;
            label.Location = new Point(2, 2);
            label.Text = text;
            this.Controls.Add(label);

            this.ApplyEvent();
        }

        private void ApplyEvent()
        {
            ControlManager.ControlMoverOrResizer.Init(this);
            this.SizeChanged += _SizeChanged;
            this.LocationChanged += _LocationChanged;
            this.MouseClick += rptText_MouseClick;
        }
        
        void rptText_MouseClick(object sender, MouseEventArgs e)
        {
            FormRptEditor.SetPropertySelectedObject(this, this.Name);
        }

        void _LocationChanged(object sender, EventArgs e)
        {
            if (FormRptEditor._rpt != null)
            {
                ReportObject rptobj = FormRptEditor._rpt.ReportDefinition.ReportObjects[this.Name];
                if (this.Top < 0) this.Top = 0;
                if (this.Left < 0) this.Left = 0; 
                rptobj.Top = Twip.PixelToTwipY(this.Top);
                rptobj.Left = Twip.PixelToTwipX(this.Left);
            }
        }

        void _SizeChanged(object sender, EventArgs e)
        {
            if (FormRptEditor._rpt != null)
            {
                ReportObject rptobj = FormRptEditor._rpt.ReportDefinition.ReportObjects[this.Name];
                rptobj.Width = Twip.PixelToTwipX(this.Width);
                rptobj.Height = Twip.PixelToTwipY(this.Height);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.DrawRectangle(new Pen(Color.Red), 0, 0, Width - 1, Height - 1);
        }
    }
}

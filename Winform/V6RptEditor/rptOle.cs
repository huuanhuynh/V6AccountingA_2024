using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class rptOle:Label
    {
        public void ApplyEvent()
        {
            ControlManager.ControlMoverOrResizer.Init(this);
            this.SizeChanged += _SizeChanged;
            this.LocationChanged += _LocationChanged;
            this.Paint += rptOle_Paint;
        }

        void rptOle_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            Point p1 = new Point(0, 0);
            Point p2 = new Point(this.Width, this.Height);
            e.Graphics.DrawLine(pen, p1, p2);
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

        
    }
}

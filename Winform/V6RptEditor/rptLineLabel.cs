using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class rptLineLabel:Label
    {
        public rptLineLabel(int topw, int bottomw, int leftw, int rightw)
        {
            //Giá trị gửi vào
            this.Top = Twip.TwipToPixelY(topw);
            if (bottomw == topw) this.Height = 1;
            else this.Height = Twip.TwipToPixelY(bottomw - topw);
            this.Left = Twip.TwipToPixelX(leftw);
            if (rightw == leftw) this.Width = 1;
            else this.Height = Twip.TwipToPixelX(rightw - leftw);
            //Giá trị mặc định

        }

        public void ApplyEvent()
        {
            ControlManager.ControlMoverOrResizer.Init(this);
            this.SizeChanged += _SizeChanged;
            this.LocationChanged += _LocationChanged;
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

using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class rptObj:Label
    {
        #region ==== Object Name ====
        [Category("_crProperty")]
        [Description("Tên đối tượng")]
        public string ObjectName
        {
            get
            {
                return "_obj.Name";
            }
        }
        #endregion

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

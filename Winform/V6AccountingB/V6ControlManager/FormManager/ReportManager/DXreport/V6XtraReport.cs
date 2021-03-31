using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public partial class V6XtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        public V6XtraReport()
        {
            InitializeComponent();
            ScriptReferences = new string[] {
                    "V6Init.dll",
                    "V6SqlConnect.dll",
                    "V6AccountingBusiness.dll",
                    //"V6AccountingBusiness.Invoices.dll",
                    "V6Controls.dll",
                    //"V6Controls.Controls.dll",
                    //"V6Controls.Forms.dll",
                    //"V6ReportControls.dll",
                    "V6ControlManager.dll",
                    //"V6ControlManager.FormManager.ChungTuManager.dll",
                    //"V6ControlManager.FormManager.ReportManager.Filter.dll",
                    //"V6ControlManager.FormManager.ReportManager.ReportR.dll",
                    //"V6ControlManager.FormManager.ReportManager.XuLy.dll",
                    "V6Tools.dll",
                    //"V6Tools.V6Convert.dll",
            };
        }

    }
}

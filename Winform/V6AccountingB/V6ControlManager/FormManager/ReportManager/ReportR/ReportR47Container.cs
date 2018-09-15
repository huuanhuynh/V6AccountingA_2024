using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.ReportR
{
    public partial class ReportR47Container : V6Control
    {
        public ReportR47Container()
        {
            InitializeComponent();
        }

        public ReportR47Container(string itemId, string program, string reportProcedure,
            string reportFile, string reportTitle, string reportTitle2,
            string reportFileF5, string reportTitleF5, string reportTitle2F5)
        {
            InitializeComponent();
            //MyInit
            try
            {
                string itemId_1, program_1, reportProcedure_1,
                reportFile_1, reportTitle_1, reportTitle2_1,
                reportFileF5_1, reportTitleF5_1, reportTitle2F5_1,

                itemId_2, program_2, reportProcedure_2,
                reportFile_2, reportTitle_2, reportTitle2_2,
                reportFileF5_2, reportTitleF5_2, reportTitle2F5_2;

                Split47(itemId, out itemId_1, out itemId_2);
                Split47(program, out program_1, out program_2);
                Split47(reportProcedure, out reportProcedure_1, out reportProcedure_2);
                Split47(reportFile, out reportFile_1, out reportFile_2);
                Split47(reportTitle, out reportTitle_1, out reportTitle_2);
                Split47(reportTitle2, out reportTitle2_1, out reportTitle2_2);
                Split47(reportFileF5, out reportFileF5_1, out reportFileF5_2);
                Split47(reportTitleF5, out reportTitleF5_1, out reportTitleF5_2);
                Split47(reportTitle2F5, out reportTitle2F5_1, out reportTitle2F5_2);

                c1 = new ReportR47ViewBase(itemId_1, program_1, reportProcedure_1, reportFile_1, reportTitle_1, reportTitle2_1,
                    reportFileF5_1, reportTitleF5_1, reportTitle2F5_1);
                c2 = new ReportR47ViewBase(itemId_2, program_2, reportProcedure_2, reportFile_2, reportTitle_2, reportTitle2_2,
                    reportFileF5_2, reportTitleF5_2, reportTitle2F5_2);
                c1.Dock = DockStyle.Top;
                c2.Dock = DockStyle.Top;
                Controls.Add(c1);
                Controls.Add(c2);
                FixControlSize();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
        }

        private void Split47(string input, out string out1, out string out2)
        {
            string[] ss = ObjectAndString.SplitString(input, false);
            if (ss.Length > 0) out1 = ss[0];
            else out1 = "";
            if (ss.Length > 1) out2 = ss[1];
            else out2 = out1;
        }

        private ReportR47ViewBase c1;
        private ReportR47ViewBase c2;

        private void FixControlSize()
        {
            try
            {
                int half = Height/2;
                c1.Height = half;
                c2.Height = half;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void ReportR47Container_SizeChanged(object sender, EventArgs e)
        {
            FixControlSize();
        }
    }
}

using System.Threading;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINGIA_TBDD : XuLyBase0
    {
        public AINGIA_TBDD(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Text.Text("TINHGTBDDNGAY"));
        }

        protected override void ExecuteProcedure()
        {
            if (GenerateProcedureParameters())
            {
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)FilterControl.Number2, (int)FilterControl.Number3);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }
                // Tinh toan
                var tTinhToan = new Thread(TinhToan);
                tTinhToan.Start();
                timerViewReport.Start();
            }
        }


    }
}

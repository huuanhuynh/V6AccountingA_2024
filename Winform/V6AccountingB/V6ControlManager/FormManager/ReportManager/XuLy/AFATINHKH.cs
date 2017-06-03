using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AFATINHKH : XuLyBase0
    {
        public AFATINHKH(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Tính khấu hao tháng.");
        }

        //protected override void ExecuteProcedure()
        //{
        //    base.MakeReport2();
        //}

    }
}

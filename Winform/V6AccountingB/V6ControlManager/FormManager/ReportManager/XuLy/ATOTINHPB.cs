using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class ATOTINHPB : XuLyBase0
    {
        public ATOTINHPB(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Tính phân bổ tháng.");
        }

        //protected override void ExecuteProcedure()
        //{
        //    base.MakeReport2();
        //}

    }
}

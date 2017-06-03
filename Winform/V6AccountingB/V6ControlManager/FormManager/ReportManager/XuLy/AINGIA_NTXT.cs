using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINGIA_NTXT : XuLyBase0
    {
        public AINGIA_NTXT(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Tính giá nhập trước xuất trước (FIFO).");
        }

        //protected override void ExecuteProcedure()
        //{
        //    base.MakeReport2();
        //}

    }
}

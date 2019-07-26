using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XAGLAUTO_GL1_Base : XuLyBase0
    {
        public XAGLAUTO_GL1_Base(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Text.Text("TAOPKTTPCHI"));
        }

        protected override void Nhan()
        {

            
            if (this.ShowConfirmMessage(V6Text.ExecuteConfirm) != DialogResult.Yes)
            {
                return;
            }
            base.Nhan();
        }

        //protected override void ExecuteProcedure()
        //{
        //    base.MakeReport2();
        //}

    }
}

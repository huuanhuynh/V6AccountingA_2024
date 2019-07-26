using System.Data;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AARBPKH2N : XuLyBase0
    {
        public AARBPKH2N(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(V6Text.Text("KETCSDBPKHSNS"));
        }

        protected override void Nhan()
        {
            var sql = "select count(1) from ABBPKH where Nam = " + FilterControl.Number2;
            var check = ObjectAndString.ObjectToInt(SqlConnect.ExecuteScalar(CommandType.Text, sql)) > 0;
            if (check && this.ShowConfirmMessage("Năm đã có. Có chắc chắn chuyển sang không?") != DialogResult.Yes)
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

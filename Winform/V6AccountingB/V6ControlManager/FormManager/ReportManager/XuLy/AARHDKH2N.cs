using System.Data;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6SqlConnect;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AARHDKH2N : XuLyBase0
    {
        public AARHDKH2N(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Kết chuyển số dư của các hợp đồng khách hàng sang năm sau.");
        }

        protected override void Nhan()
        {
            var sql = "select count(1) from ABHDKH where Nam = " + FilterControl.Number2;
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

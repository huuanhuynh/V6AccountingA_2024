using System.Data;
using System.Windows.Forms;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuXuatTraLaiNCC.ChonPhieuNhap;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XAGLAUTO_GL1_Base : XuLyBase0
    {
        public XAGLAUTO_GL1_Base(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Tạo chứng từ phiếu kế toán từ Phiếu chi/ Báo nợ.");
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

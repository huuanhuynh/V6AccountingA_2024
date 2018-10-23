using System.Windows.Forms;
using V6ControlManager.FormManager.NhanSu;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public class HRORGVIEW1 : XuLyBase0
    {

        public HRORGVIEW1(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            FilterControl.Visible = false;

            string tableName = program, initFilter = "", sort = "";
            Control tochucViewContainer = new ToChucView(itemId, text, tableName, initFilter, sort);
            tochucViewContainer.Dock = DockStyle.Fill;
            panel1.Controls.Add(tochucViewContainer);
            tochucViewContainer.Disposed += tochucViewContainer_Disposed;
        }

        void tochucViewContainer_Disposed(object sender, System.EventArgs e)
        {
            btnHuy.PerformClick();
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(".");
        }

        protected override void Nhan()
        {
            //khoHangContainer.GetAndSetData();
        }

    }
}

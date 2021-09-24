using System.Windows.Forms;
using V6ControlManager.FormManager.NhanSu;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public class HRORGVIEW1 : XuLyBase0
    {

        public HRORGVIEW1(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            FilterControl.Visible = false;

            string tableName = program, initFilter = "";
            Control tochucViewContainer = new ToChucView(itemId, reportCaption, tableName, initFilter);
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
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = ".";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void Nhan()
        {
            //khoHangContainer.GetAndSetData();
        }

    }
}

using System.Windows.Forms;
using V6ControlManager.FormManager.KhoHangManager;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINVITRI02 : XuLyBase0
    {
        private KhoHangContainer khoHangContainer;

        public AINVITRI02(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            FilterControl.Visible = false;
            var cellWidth = 80;
            var cellHeight = 40;
            KhoParams kparams = new KhoParams
            {
                ItemId = itemId,
                Program = program,
                ReportProcedure = reportProcedure,
                ReportFile = reportFile,
                CellWidth = cellWidth,
                CellHeight = cellHeight,
                ViewLable2 = true,
                RunTimer = true,
            };
            khoHangContainer = new KhoHangContainer(kparams);
            khoHangContainer.Dock = DockStyle.Fill;
            khoHangContainer.Name = itemId;
            //khoHangContainer.CodeForm = CodeForm;
            panel1.Controls.Add(khoHangContainer);
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(".");
        }

        protected override void Nhan()
        {
            khoHangContainer.GetAndSetData();
        }

    }
}

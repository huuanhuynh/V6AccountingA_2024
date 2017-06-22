using System.Windows.Forms;
using V6ControlManager.FormManager.KhoHangManager;
using V6ControlManager.FormManager.Map;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AMAPREPORT : XuLyBase0
    {
        private FormMapManagerAutoHide khoHangContainer;

        public AMAPREPORT(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            FilterControl.Visible = false;
            var cellWidth = 50;
            var cellHeight = 20;
            KhoParams kparams = new KhoParams
            {
                ItemId = itemId,
                Program = program,
                ReportProcedure = reportProcedure,
                ReportFile = reportFile,
                ReportCaption = "",
                CellWidth = cellWidth,
                CellHeight = cellHeight,
                RunTimer = true,
            };
            khoHangContainer = new FormMapManagerAutoHide(kparams);
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
            //khoHangContainer.GetAndSetData();
        }

    }
}

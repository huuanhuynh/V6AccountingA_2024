using System.Windows.Forms;
using V6ControlManager.FormManager.KhoHangManager;
using V6ControlManager.FormManager.VitriManager;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINVITRI04 : XuLyBase0
    {
        private VitriCafeContainer khoHangContainer;

        public AINVITRI04(string itemId, string program, string reportProcedure, string reportFile, string text)
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
                RunTimer = false,
            };
            khoHangContainer = new VitriCafeContainer("SOC", itemId, program);
            khoHangContainer.Dock = DockStyle.Fill;
            khoHangContainer.Name = itemId;
            //khoHangContainer.CodeForm = CodeForm;
            panel1.Controls.Add(khoHangContainer);
            khoHangContainer.Disposed += khoHangContainer_Disposed;
        }

        void khoHangContainer_Disposed(object sender, System.EventArgs e)
        {
            if(!IsDisposed) Dispose();
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

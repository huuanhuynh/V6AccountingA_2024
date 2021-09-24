using System.Windows.Forms;
using V6ControlManager.FormManager.KhoHangManager;
using V6ControlManager.FormManager.VitriManager;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINVITRI04 : XuLyBase0
    {
        private VitriCafeContainer khoHangContainer;

        public AINVITRI04(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            FilterControl.Visible = false;
            btnSuaTTMauBC.Visible = false;
            btnThemMauBC.Visible = false;
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

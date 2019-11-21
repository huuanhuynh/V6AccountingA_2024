using System.Collections.Generic;
using System.Windows.Forms;
using V6ControlManager.FormManager.KhoHangManager;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINVITRI02 : XuLyBase0
    {
        public event HandleData V6Click;
        protected virtual void OnV6Click(IDictionary<string, object> data)
        {
            var handler = V6Click;
            if (handler != null) handler(data);
        }

        public KhoHangContainer khoHangContainer;

        public AINVITRI02(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
                RunTimer = true,
            };
            khoHangContainer = new KhoHangContainer(kparams);
            khoHangContainer.V6Click += khoHangContainer_V6Click;
            khoHangContainer.Dock = DockStyle.Fill;
            khoHangContainer.Name = itemId;
            //khoHangContainer.CodeForm = CodeForm;
            panel1.Controls.Add(khoHangContainer);
        }

        void khoHangContainer_V6Click(IDictionary<string, object> data)
        {
            OnV6Click(data);
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

﻿using System.Windows.Forms;
using V6ControlManager.FormManager.KhoHangManager;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINVITRI03 : XuLyBase0
    {
        private KhoHangContainer khoHangContainer;

        public AINVITRI03(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using V6ControlManager.FormManager.KhoHangManager;
using V6ControlManager.FormManager.KhoHangManager.Draw;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINVITRI01 : XuLyBase0
    {
        private KhoHangContainer khoHangContainer;
        private KhoHangContainerDraw khoHangContainerDraw;
        private KhoHangContainerAll khoHangContainerAll;

        public AINVITRI01(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            FilterControl.Visible = false;
            btnSuaTTMauBC.Visible = false;
            btnThemMauBC.Visible = false;
            var cellWidth = 50;
            var cellHeight = 20;
            KhoParams kparams = new KhoParams
            {
                ItemId = itemId,
                Program = program,
                ReportProcedure = reportProcedure,
                ReportFile = reportFile,
                CellWidth = cellWidth,
                CellHeight = cellHeight,
                RunTimer = true,
            };
            
            //if (program == "AINVITRI01Draw") // Test draw
            //{
            //    kparams.Program = "AINVITRI01";
            //    khoHangContainerDraw = new KhoHangContainerDraw(kparams);
            //    khoHangContainerDraw.Dock = DockStyle.Fill;
            //    khoHangContainerDraw.Name = itemId;
            //    //khoHangContainer.CodeForm = CodeForm;
            //    panel1.Controls.Add(khoHangContainerDraw);
            //}else
            if (program == "AINVITRI01Draw") // Test All
            {
                kparams.Program = "AINVITRI01";
                khoHangContainerAll = new KhoHangContainerAll(kparams);
                khoHangContainerAll.Dock = DockStyle.Fill;
                khoHangContainerAll.Name = itemId;
                //khoHangContainer.CodeForm = CodeForm;
                panel1.Controls.Add(khoHangContainerAll);
            }
            else if (program == "AINVITRI01Draw")
            {
                //kparams.Program = "AINVITRI01";
                //khoHangContainerDraw = new KhoHangContainerDraw(kparams);
                //khoHangContainerDraw.Dock = DockStyle.Fill;
                //khoHangContainerDraw.Name = itemId;
                ////khoHangContainer.CodeForm = CodeForm;
                //panel1.Controls.Add(khoHangContainerDraw);
            }
            else
            {
                khoHangContainer = new KhoHangContainer(kparams);
                khoHangContainer.Dock = DockStyle.Fill;
                khoHangContainer.Name = itemId;
                //khoHangContainer.CodeForm = CodeForm;
                panel1.Controls.Add(khoHangContainer);
                khoHangContainer.V6Click += khoHangContainer_V6Click;
            }
        }

        void khoHangContainer_V6Click(System.Collections.Generic.IDictionary<string, object> data)
        {
            try
            {
                if (data != null && data.ContainsKey("LISTVITRIDETAIL"))
                {
                    List<ViTriDetail> listVitriDetai = data["LISTVITRIDETAIL"] as List<ViTriDetail>;
                    if (listVitriDetai != null)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(".");
        }

        protected override void Nhan()
        {
            if (khoHangContainer != null)
            {
                khoHangContainer.GetAndSetData();
            }
            else if (khoHangContainerAll != null)
            {
                khoHangContainerAll.GetAndSetData();
            }
            else
            {
                khoHangContainerDraw.GetAndSetData();
            }
        }

    }
}

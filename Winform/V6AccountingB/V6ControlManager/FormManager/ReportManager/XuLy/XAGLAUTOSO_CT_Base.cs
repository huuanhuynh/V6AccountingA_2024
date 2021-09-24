using System;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XAGLAUTOSO_CT_Base : XuLyBase0
    {
        public XAGLAUTOSO_CT_Base(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = V6Text.Text("DANHLAISOCT");
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void Nhan()
        {
            try
            {
                var plist = FilterControl.GetFilterParameters();
                int check = V6BusinessHelper.CheckDataLocked("1", FilterControl.Date1.Date, (int)FilterControl.Number2, (int)FilterControl.Number3);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }

                if (this.ShowConfirmMessage(V6Text.ExecuteConfirm) != DialogResult.Yes)
                {
                    return;
                }
                string paramss = V6ControlFormHelper.PlistToString(plist);
                V6BusinessHelper.WriteV6UserLog(ItemID, GetType() + "." + MethodBase.GetCurrentMethod().Name,
                    string.Format("reportProcedure:{0} {1}", _reportProcedure, paramss));
                base.Nhan();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        //protected override void ExecuteProcedure()
        //{
        //    base.MakeReport2();
        //}

    }
}

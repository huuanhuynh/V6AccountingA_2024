using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class ACOVVKH2N : XuLyBase0
    {
        public ACOVVKH2N(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = V6Text.Text("KETCSDVVKHSNS");
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        protected override void Nhan()
        {
            try
            {
                var plist = FilterControl.GetFilterParameters();
                int check = V6BusinessHelper.CheckDataLocked("3", V6Setting.M_SV_DATE, (int)FilterControl.Number2, (int)FilterControl.Number3);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }

                var sql = "select count(1) from ABVVKH where Nam = " + FilterControl.Number2;
                var check1 = ObjectAndString.ObjectToInt(SqlConnect.ExecuteScalar(CommandType.Text, sql)) > 0;
                if (check1 && this.ShowConfirmMessage("Năm đã có. Có chắc chắn chuyển sang không?") != DialogResult.Yes)
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

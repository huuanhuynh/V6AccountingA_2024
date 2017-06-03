using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class V6BACKUP1 : XuLyBase0
    {
        public V6BACKUP1(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            GetName();
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Backup (lưu trữ) số liệu.");
        }

        string fileName = "FileName";

        //protected override void ExecuteProcedure()
        //{
        //    base.ExecuteProcedure();
        //}

        private void GetName()
        {
            var sql = "Select DBO.VFA_GET_NAMEFILE_BACKUP1()";
            try
            {
                fileName = V6SqlConnect.SqlConnect.ExecuteScalar(CommandType.Text, sql).ToString().Trim();
            }
            catch
            {
                fileName = "";
            }
            FilterControl.String1 = fileName;
        }

        protected override void DoAfterExecuteSuccess()
        {
            try
            {
                if (FilterControl.Check1)
                {
                    var filePath = fileName;
                    var zipFilePath = filePath + ".7z";
                    if (File.Exists(filePath))
                    {
                        if(File.Exists(zipFilePath)) File.Delete(zipFilePath);
                        V67z.Run7z("a " + zipFilePath + " " + filePath + " -aoa ");
                        //V67z.ZipFile(zipFilePath, true, filePath);
                        
                        //CopyToV6, khong the neu chay 7za
                        if (FilterControl.Check2)
                        {
                            var _setting = new H.Setting(Path.Combine(V6Login.StartupPath, "Setting.ini"));
                            var VPN_IP = _setting.GetSetting("VPN_IP");
                            var VPN_USER = _setting.GetSetting("VPN_USER");
                            var VPN_EPASS = _setting.GetSetting("VPN_EPASS");
                            var VPN_SUBFOLDER = _setting.GetSetting("VPN_SUBFOLDER");
                            V6FileIO.CopyToVPN(zipFilePath, VPN_IP, VPN_USER, VPN_EPASS, VPN_SUBFOLDER);
                        }
                    }
                }
                GetName();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoAfterExecuteSuccess", ex);
            }
        }
        
        public override void V6F3Execute()
        {
            if (f3count == 3)
            {
                f3count = 0;
                if (new ConfirmPasswordV6().ShowDialog() == DialogResult.OK)
                {
                    V6ControlFormHelper.ShowMainMessage("V6 Confirm ......OK....");
                    FilterControl.Call1();
                }
            }
            else
            {
                FilterControl.Call2();
            }
        }
    }
}

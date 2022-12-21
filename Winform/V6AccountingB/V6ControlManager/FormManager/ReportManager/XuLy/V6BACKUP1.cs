﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class V6BACKUP1 : XuLyBase0
    {
        public V6BACKUP1(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            GetName();
        }

        public bool AUTO_DISPOSE = false;
        public bool CheckBackup()
        {
            var svdate = V6BusinessHelper.GetServerDateTime();
            if (svdate.Date == FilterControl.Date1.Date)
            {
                return true;
            }
            return false;
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "Backup (lưu trữ) số liệu.";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
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
                            V6FileIO.CopyToVPN(zipFilePath, VPN_SUBFOLDER, VPN_IP, VPN_USER, VPN_EPASS);
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
        
        public override void V6CtrlF12Execute()
        {
            if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
            {
                V6ControlFormHelper.ShowMainMessage("V6 Confirm ......OK....");
                FilterControl.Call1();
            }
            //base.V6CtrlF12Execute(); // Không cần
        }

        public override void V6CtrlF12ExecuteUndo()
        {
            FilterControl.Call2();
        }
    }
}

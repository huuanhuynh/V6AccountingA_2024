using System;
using System.Collections.Generic;
using System.Data;
using V6ControlManager.FormManager.HeThong.QuanLyHeThong.NgonNgu;
using V6ControlManager.FormManager.MenuManager;
using V6ControlManager.FormManager.ReportManager.DanhMuc;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager
{
    public static class FormManagerHelper
    {
        public static Menu3Control CurrentMenu3Control { get; set; }

        public static void HideMainMenu()
        {
            V6ControlFormHelper.HideMainMenu();
        }
        public static void ShowMainMenu()
        {
            V6ControlFormHelper.ShowMainMenu();
        }
        public static void HideCurrentMenu3Menu()
        {
            if(CurrentMenu3Control!=null)
                CurrentMenu3Control.HideMenu();
        }
        public static void ShowCurrentMenu3Menu()
        {
            if (CurrentMenu3Control != null)
                CurrentMenu3Control.ShowMenu();
        }

        public static void ShowDanhMucPrint(string tableName, string reportFile, string reportTitle, string reportTitle2, bool dialog = true)
        {
            var f = new DanhMucReportForm(tableName, reportFile, reportTitle, reportTitle2);
            if(dialog)
                f.ShowDialog();
            else
                f.Show();
        }

        public static V6FormControl GetGeneralControl(string code, string itemId)
        {
            switch (code)
            {
                case "CORPLAN":
                    return new CorplanContainer(itemId);
                case "V6TOOLS":
                    return new ToolManager.AllTool(itemId);
            }

            return new V6UserControlEmpty("GeneralControl" + code);
        }
    }
}

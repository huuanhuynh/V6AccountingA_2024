using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public static class DXreportManager
    {
        //public static XtraReport CreateXtraReport()
        //{
        //    XtraReport repx = new XtraReport();
        //    repx.ScriptReferences = new string[] {
        //            "V6Init.dll",
        //            "V6SqlConnect.dll",
        //            "V6AccountingBusiness.dll",
        //            //"V6AccountingBusiness.Invoices.dll",
        //            "V6Controls.dll",
        //            //"V6Controls.Controls.dll",
        //            //"V6Controls.Forms.dll",
        //            //"V6ReportControls.dll",
        //            "V6ControlManager.dll",
        //            //"V6ControlManager.FormManager.ChungTuManager.dll",
        //            //"V6ControlManager.FormManager.ReportManager.Filter.dll",
        //            //"V6ControlManager.FormManager.ReportManager.ReportR.dll",
        //            //"V6ControlManager.FormManager.ReportManager.XuLy.dll",
        //            "V6Tools.dll",
        //            //"V6Tools.V6Convert.dll",
        //    };
        //    return repx;
        //}

        /// <summary>
        /// Tạo chuỗi format số dùng trong XtraReport (test).
        /// </summary>
        /// <param name="v6Tag"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetNumberFormatFromV6Tag(V6Tag v6Tag, IDictionary<string, object> parameters)
        {
            bool haveFormat = false;
            string format = "{0:";
            format += "#,#";
            if (v6Tag.Decimals != null)
            {
                int v6tagdecimals;
                if (!int.TryParse(v6Tag.Decimals, out v6tagdecimals))
                {
                    if (parameters.ContainsKey(v6Tag.Decimals))
                    {
                        v6tagdecimals = ObjectAndString.ObjectToInt(parameters[v6Tag.Decimals]);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMainMessage(V6Text.NotExist + v6Tag.Decimals);
                        Logger.WriteToLog(V6Text.NotExist + v6Tag.Decimals);
                    }
                }
                haveFormat = true;
                format += ".";
                for (int i = 0; i < v6tagdecimals; i++)
                {
                    format += "0";
                }
            }
            format += "}";
            if (haveFormat) return format;
            return "";
        }

        /// <summary>
        /// Tải file XtraReport. Nếu không có file trả về mẫu tạm XtraReport1.
        /// </summary>
        /// <param name="file">Đường dẫn file repx</param>
        /// <returns></returns>
        public static XtraReport LoadV6XtraReportFromFile(string file)
        {
            XtraReport repx = null;
            string shortName = Path.GetFileNameWithoutExtension(file);
            if (File.Exists(file))
            {
                repx = XtraReport.FromFile(file, true);
            }
            else
            {
                repx = new XtraReport1();
                repx.SaveLayoutToXml(file);
            }
            repx.Name = shortName;
            //repx.LoadLayout(file);
            repx.ScriptReferences = new string[] {
                    "V6Init.dll",
                    "V6SqlConnect.dll",
                    "V6AccountingBusiness.dll",
                    //"V6AccountingBusiness.Invoices.dll",
                    "V6Controls.dll",
                    //"V6Controls.Controls.dll",
                    //"V6Controls.Forms.dll",
                    //"V6ReportControls.dll",
                    "V6ControlManager.dll",
                    //"V6ControlManager.FormManager.ChungTuManager.dll",
                    //"V6ControlManager.FormManager.ReportManager.Filter.dll",
                    //"V6ControlManager.FormManager.ReportManager.ReportR.dll",
                    //"V6ControlManager.FormManager.ReportManager.XuLy.dll",
                    "V6Tools.dll",
                    //"V6Tools.V6Convert.dll",
            };
            return repx;
        }

        public static void SetReportFormatByTag(XtraReport repx, IDictionary<string, object> parameters)
        {
            try
            {
                //Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = V6Options.M_NUM_POINT;
                // Vidu format SL
                var dxControls = repx.AllControls<XRLabel>();
                foreach (XRLabel xrLabel in dxControls)
                {
                    V6Tag v6Tag = new V6Tag(xrLabel.Tag);
                    //if (!string.IsNullOrEmpty(v6Tag.Format))
                    {
                        string numberFormat = GetNumberFormatFromV6Tag(v6Tag, parameters);
                        if (numberFormat != "") xrLabel.DataBindings["Text"].FormatString = numberFormat;
                    }
                }

                // End vidu
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("DXreportManager.SetReportFormatByTag", ex);
            }
        }
    }
}

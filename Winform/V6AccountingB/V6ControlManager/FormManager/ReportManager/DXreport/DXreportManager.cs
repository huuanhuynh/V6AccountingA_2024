using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                if (!Int32.TryParse(v6Tag.Decimals, out v6tagdecimals))
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


            string[] param_font = V6Options.GetValue("M_RFONTNAME").Split(';');
            string font_name = param_font[0];	
            float font_size =	repx.Font.Size;
            FontStyle font_style = repx.Font.Style;
            if (param_font.Length > 1) font_size = Single.Parse(param_font[1]);
            if (param_font.Length > 2) font_style = (FontStyle)Int32.Parse(param_font[2]);
            Font m_rfontname = new Font(font_name, font_size, font_style);

            param_font = V6Options.GetValue("M_RTFONT").Split(';');
            font_name = param_font[0];
            font_size = repx.Font.Size;
            font_style = repx.Font.Style;
            if (param_font.Length > 1) font_size = Single.Parse(param_font[1]);
            if (param_font.Length > 2) font_style = (FontStyle)Int32.Parse(param_font[2]);
            Font m_rtfont = new Font(font_name, font_size, font_style);

            param_font = V6Options.GetValue("M_RSFONT").Split(';');
            font_name = param_font[0];
            font_size = repx.Font.Size;
            font_style = repx.Font.Style;
            if (param_font.Length > 1) font_size = Single.Parse(param_font[1]);
            if (param_font.Length > 2) font_style = (FontStyle)Int32.Parse(param_font[2]);
            Font m_rsfont = new Font(font_name, font_size, font_style);


            foreach (XRControl x in repx.Controls)
            {
                SetFontR(x, m_rfontname, m_rtfont, m_rsfont);
            }

            return repx;
        }

        private static void SetFontR(XRControl c, Font m_rfontname, Font m_rtfont, Font m_rsfont)
        {
            string tag_string = ";" + c.Tag + ";";
            if (!(c is XRLabel)) goto NEXT;
            if (tag_string.Contains(";nofont;"))
            {
                // no apply.
            }
            else if (tag_string.Contains(";m_rtfont;"))
            {
                bool nofsize = tag_string.Contains(";nofsize;");
                bool nofstyle = tag_string.Contains(";nofstyle;");
                if (nofsize || nofstyle)
                {
                    var newFont = new Font(m_rtfont.FontFamily, nofsize ? c.Font.Size : m_rtfont.Size, nofstyle ? c.Font.Style : m_rtfont.Style);
                    c.Font = newFont;
                }
                else
                {
                    c.Font = m_rtfont;
                }
            }
            else if (tag_string.Contains(";m_rsfont;"))
            {
                bool nofsize = tag_string.Contains(";nofsize;");
                bool nofstyle = tag_string.Contains(";nofstyle;");
                if (nofsize || nofstyle)
                {
                    var newFont = new Font(m_rsfont.FontFamily, nofsize ? c.Font.Size : m_rsfont.Size, nofstyle ? c.Font.Style : m_rsfont.Style);
                    c.Font = newFont;
                }
                else
                {
                    c.Font = m_rsfont;
                }
            }
            else
            {
                bool nofsize = tag_string.Contains(";nofsize;");
                bool nofstyle = tag_string.Contains(";nofstyle;");
                if (nofsize || nofstyle)
                {
                    var newFont = new Font(m_rfontname.FontFamily, nofsize ? c.Font.Size : m_rfontname.Size, nofstyle ? c.Font.Style : m_rfontname.Style);
                    c.Font = newFont;
                }
                else
                {
                    c.Font = m_rfontname;
                }
            }

            NEXT:
            foreach (XRControl c0 in c.Controls)
            {
                SetFontR(c0, m_rfontname, m_rtfont, m_rsfont);
            }

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

        public static void AddBaseParameters(IDictionary<string, object> reportDocumentParameters)
        {
            reportDocumentParameters.AddRange(new SortedDictionary<string, object>
            {
                {"Decimals", 0},
                {"ThousandsSeparator", V6Options.M_NUM_SEPARATOR},
                {"DecimalSymbol", V6Options.M_NUM_POINT},
                {"DecimalsSL", V6Options.M_IP_R_SL},
                {"DecimalsDG", V6Options.M_IP_R_GIA},
                {"DecimalsDGNT", V6Options.M_IP_R_GIANT},
                {"DecimalsTT", V6Options.M_IP_R_TIEN},
                {"DecimalsTTNT", V6Options.M_IP_R_TIENNT},

                // V6Soft
                {"M_TEN_CTY", V6Soft.V6SoftValue["M_TEN_CTY"].ToUpper()},
                {"M_TEN_TCTY", V6Soft.V6SoftValue["M_TEN_TCTY"].ToUpper()},
                {"M_DIA_CHI", V6Soft.V6SoftValue["M_DIA_CHI"]},
                {"M_TEN_CTY2", V6Soft.V6SoftValue["M_TEN_CTY2"].ToUpper()},
                {"M_TEN_TCTY2", V6Soft.V6SoftValue["M_TEN_TCTY2"].ToUpper()},
                {"M_DIA_CHI2", V6Soft.V6SoftValue["M_DIA_CHI2"]},
                // V6option
                {"M_MA_THUE", V6Options.GetValue("M_MA_THUE")},
                {"M_RTEN_VSOFT", V6Options.GetValue("M_RTEN_VSOFT")},
                //{"M_TEN_NLB", txtM_TEN_NLB.Text.Trim()},
                //{"M_TEN_NLB2", txtM_TEN_NLB2.Text.Trim()},
                {"M_TEN_KHO_BD", V6Options.GetValue("M_TEN_KHO_BD")},
                {"M_TEN_KHO2_BD", V6Options.GetValue("M_TEN_KHO2_BD")},
                {"M_DIA_CHI_BD", V6Options.GetValue("M_DIA_CHI_BD")},
                {"M_DIA_CHI2_BD", V6Options.GetValue("M_DIA_CHI2_BD")},

                {"M_TEN_GD", V6Options.GetValue("M_TEN_GD")},
                {"M_TEN_GD2", V6Options.GetValue("M_TEN_GD2")},
                {"M_TEN_KTT", V6Options.GetValue("M_TEN_KTT")},
                {"M_TEN_KTT2", V6Options.GetValue("M_TEN_KTT2")},
                {"M_SO_QD_CDKT", V6Options.GetValue("M_SO_QD_CDKT")},
                {"M_SO_QD_CDKT2", V6Options.GetValue("M_SO_QD_CDKT2")},
                {"M_NGAY_QD_CDKT", V6Options.GetValue("M_NGAY_QD_CDKT")},
                {"M_NGAY_QD_CDKT2", V6Options.GetValue("M_NGAY_QD_CDKT2")},

                {"M_RFONTNAME", V6Options.M_RFONTNAME},
                {"M_RTFONT", V6Options.M_RTFONT},
                {"M_RSFONT", V6Options.M_RSFONT},
                {"M_R_FONTSIZE", V6Options.GetValue("M_R_FONTSIZE")},
            });
        }
    }
}

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
        public static readonly string TemplateRepxFile = Path.Combine(V6Login.StartupPath, "ReportsDX\\Template.repx");
        public static void Init()
        {
            // Đăng ký hàm tự tạo
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXFormatNumber());
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXFormatNumberGia());
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXFormatNumberGiaNT());
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXFormatNumberSL());
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXFormatNumberTT());
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXFormatNumberTTNT());
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXToString());
        }
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
        /// Tải file XtraReport. Nếu không có file trả về mẫu tạm Template.repx đặt sẵn trong thư mục ReportsDX.
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
                if(File.Exists(TemplateRepxFile)) repx = XtraReport.FromFile(TemplateRepxFile, true);
                else repx = new XtraReport1();
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
            string tag_string = (";" + c.Tag + ";").ToLower();
            if (!(c is XRLabel)) goto NEXT;

            // Font
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

            // Number xrLabel1.DataBindings["Text"].FormatString = "{0:dd/MM/yyyy}";  
            if (tag_string.Contains(";date;"))
            {
                c.DataBindings["Text"].FormatString = "{0:dd/MM/yyyy}";
            }
            if (tag_string.Contains(";number;"))
            {
                var viewControl = c.Parent.FindControl(c.Name + "_view", true);
                if (viewControl != null)
                {
                    c.ForeColor = c.BackColor;
                    viewControl.BeforePrint += (sender, args) =>
                    {
                        string viewText = ObjectAndString.NumberToString(((XRLabel)c).Summary.GetResult(), 0,
                            V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                        viewControl.Text = viewText;
                    };
                }
            }
            else if (tag_string.Contains(";numbersl;"))
            {
                var viewControl = c.Parent.FindControl(c.Name + "_view", true);
                if (viewControl != null)
                {
                    c.ForeColor = c.BackColor;
                    viewControl.BeforePrint += (sender, args) =>
                    {
                        string viewText = ObjectAndString.NumberToString(((XRLabel)c).Summary.GetResult(), V6Options.M_IP_R_SL,
                            V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                        viewControl.Text = viewText;
                    };
                }
            }
            else if (tag_string.Contains(";numberdg;"))
            {
                var viewControl = c.Parent.FindControl(c.Name + "_view", true);
                if (viewControl != null)
                {
                    c.ForeColor = c.BackColor;
                    viewControl.BeforePrint += (sender, args) =>
                    {
                        string viewText = ObjectAndString.NumberToString(((XRLabel)c).Summary.GetResult(), V6Options.M_IP_R_GIA,
                            V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                        viewControl.Text = viewText;
                    };
                }
            }
            else if (tag_string.Contains(";numberdgnt;"))
            {
                var viewControl = c.Parent.FindControl(c.Name + "_view", true);
                if (viewControl != null)
                {
                    c.ForeColor = c.BackColor;
                    viewControl.BeforePrint += (sender, args) =>
                    {
                        string viewText = ObjectAndString.NumberToString(((XRLabel)c).Summary.GetResult(), V6Options.M_IP_R_GIANT,
                            V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                        viewControl.Text = viewText;
                    };
                }
            }
            else if (tag_string.Contains(";numbertt;"))
            {
                var viewControl = c.Parent.FindControl(c.Name + "_view", true);
                if (viewControl != null)
                {
                    c.ForeColor = c.BackColor;
                    viewControl.BeforePrint += (sender, args) =>
                    {
                        string viewText = ObjectAndString.NumberToString(((XRLabel)c).Summary.GetResult(), V6Options.M_IP_R_TIEN,
                            V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                        viewControl.Text = viewText;
                    };
                }
            }
            else if (tag_string.Contains(";numberttnt;"))
            {
                var viewControl = c.Parent.FindControl(c.Name + "_view", true);
                if (viewControl != null)
                {
                    c.ForeColor = c.BackColor;
                    viewControl.BeforePrint += (sender, args) =>
                    {
                        string viewText = ObjectAndString.NumberToString(((XRLabel)c).Summary.GetResult(), V6Options.M_IP_R_TIENNT,
                            V6Options.M_NUM_POINT, V6Options.M_NUM_SEPARATOR);
                        viewControl.Text = viewText;
                    };
                }
            }

            NEXT:
            foreach (XRControl c0 in c.Controls)
            {
                SetFontR(c0, m_rfontname, m_rtfont, m_rsfont);
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

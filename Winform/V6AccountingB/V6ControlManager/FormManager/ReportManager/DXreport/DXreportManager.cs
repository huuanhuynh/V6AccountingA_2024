using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using V6Controls;
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
            DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(new V6DXMoneyToWords());
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

        /// <summary>
        /// Mở form chỉnh sửa file XtraReport
        /// </summary>
        /// <param name="file">File repx cần sửa.</param>
        /// <param name="ds">Dữ liệu mẫu</param>
        /// <param name="reportDocumentParameters">Các param.</param>
        /// <param name="owner">Form hiện tại.</param>
        public static void EditRepx(string file, DataSet ds, IDictionary<string, object> reportDocumentParameters, IWin32Window owner = null)
        {
            bool ctrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            bool shift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
            bool alt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
            //string file = ReportFileFullDX;
            if (shift)
            {
                file = V6ControlFormHelper.ChooseOpenFile(owner, "XtraReport|*.repx");
            }
            else if (ctrl && alt)
            {
                file = TemplateRepxFile;
            }
            XtraReport x = LoadV6XtraReportFromFile(file);
            if (x != null)
            {
                x.DataSource = ds.Copy();
                //SetAllReportParams(x);
                string errors = "";
                foreach (KeyValuePair<string, object> item in reportDocumentParameters)
                {
                    try
                    {
                        if (x.Parameters[item.Key] != null)
                        {
                            x.Parameters[item.Key].Value = item.Value;
                        }
                        else
                        {
                            // missing parameters warning!
                            //errors += "\n" + item.Key + ":\t " + V6Text.NotExist;
                            // Auto create Paramter for easy edit.
                            x.Parameters.Add(new Parameter()
                            {
                                Name = item.Key,
                                Value = item.Value,
                                Visible = false,
                                Type = item.Value.GetType(),
                                Description = item.Key,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        errors += "\n" + item.Key + ": " + ex.Message;
                    }
                }

                if (errors.Length > 0)
                {
                    owner.ShowInfoMessage(errors);
                }

                XtraEditorForm1 form1 = new XtraEditorForm1(x, file);
                form1.Show(owner);
            }
        }



        /// <summary>
        /// Xuất repx ra file.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repx"></param>
        /// <param name="exportType">PDF,WORD-DOCX,HTML,IMAGE,EXCEL-XLSX-XLS</param>
        /// <param name="defaultSaveName"></param>
        public static void ExportRepxToPdfInThread_As(IWin32Window owner, XtraReport repx, string exportType, string defaultSaveName)
        {
            if (repx == null)
            {
                return;
            }
            try
            {
                string filter = "Pdf files (*.pdf)|*.pdf";
                switch (exportType.ToUpper())
                {
                    case "EXCEL":
                    case "EXCEL_RAW":
                    case "XLSX":
                    case "XLSX_RAW":
                        filter = "Excel files (*.xlsx)|*.xlsx";
                        break;
                    case "XLS":
                    case "XLS_RAW":
                        filter = "Excel files (*.xls)|*.xls";
                        break;
                    case "DOCX":
                    case "WORD":
                        filter = "Word files (*.docx)|*.docx";
                        break;
                    case "HTML":
                        filter = "Html files (*.html)|*.html";
                        break;
                    case "IMAGE":
                    case "IMG":
                    case "PNG":
                        filter = "Image files (*.png)|*.png";
                        break;
                    case "PDF":
                    default:

                        break;
                }


                var save = new SaveFileDialog
                {
                    Filter = filter,
                    Title = V6Text.Export,
                    FileName = ChuyenMaTiengViet.ToUnSign(defaultSaveName)
                };
                if (save.ShowDialog(owner) == DialogResult.OK)
                {
                    ExportRepxToPdfInThread(owner, repx, exportType, save.FileName);
                }
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                if (methodInfo.DeclaringType != null)
                {
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    V6ControlFormHelper.ShowErrorException(address, ex);
                }
            }
        }

        public static void ExportRepxToPdfInThread(IWin32Window owner, XtraReport repx, string exportType, string fileName)
        {
            ExportRepxToPdf_owner = owner;
            ExportRepxToPdf_repx = repx;
            ExportRepxToPdf_switch = exportType;
            ExportRepxToPdf_fileName = fileName;
            var thread1 = new System.Threading.Thread(ExportRepxToPdf_Thread);
            ExportRepxToPdf_running = true;
            thread1.Start();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer4_Tick;
            time_count4 = 0;
            timer.Start();
        }

        static void timer4_Tick(object sender, EventArgs e)
        {
            if (ExportRepxToPdf_running)
            {
                //V6ControlFormHelper.ShowMainMessage(V6Text.Exporting + ++time_count4);
            }
            else
            {
                ((Timer)sender).Stop();
                if (V6Options.AutoOpenExcel)// && !NoOpen)
                {
                    V6ControlFormHelper.OpenFileProcess(ExportRepxToPdf_fileName);
                }
                else
                {
                    V6ControlFormHelper.ShowInfoMessage(V6Text.ExportFinish + ++time_count4, 500);
                }
            }
        }

        private static IWin32Window ExportRepxToPdf_owner;
        private static XtraReport ExportRepxToPdf_repx;
        private static string ExportRepxToPdf_fileName;
        private static string ExportRepxToPdf_switch = "PDF";
        private static bool ExportRepxToPdf_running;
        private static int time_count4 = 0;
        public static void ExportRepxToPdf_Thread()
        {
            try
            {
                ExportRepxToPdf_repx.ExportOptions.Xls.RawDataMode = false;
                switch (ExportRepxToPdf_switch)
                {
                    case "XLS":
                        ExportRepxToPdf_repx.ExportToXls(ExportRepxToPdf_fileName);
                        break;
                    case "XLS_RAW":
                        ExportRepxToPdf_repx.ExportOptions.Xls.RawDataMode = true;
                        ExportRepxToPdf_repx.ExportToXls(ExportRepxToPdf_fileName);
                        break;
                    case "EXCEL":
                    case "XLSX":
                        ExportRepxToPdf_repx.ExportToXlsx(ExportRepxToPdf_fileName);
                        break;
                    case "EXCEL_RAW":
                    case "XLSX_RAW":
                        ExportRepxToPdf_repx.ExportOptions.Xlsx.RawDataMode = true;
                        ExportRepxToPdf_repx.ExportToXlsx(ExportRepxToPdf_fileName);
                        break;
                    case "WORD":
                    case "DOCX":
                        DocxExportOptions docx_options = new DocxExportOptions();
                        docx_options.ExportMode = DocxExportMode.SingleFile;
                        docx_options.KeepRowHeight = true;
                        docx_options.TableLayout = true;
                        ExportRepxToPdf_repx.ExportToDocx(ExportRepxToPdf_fileName, docx_options);
                        break;
                    case "HTML":
                        ExportRepxToPdf_repx.ExportToHtml(ExportRepxToPdf_fileName);
                        break;
                    case "IMAGE":
                        ExportRepxToPdf_repx.ExportToImage(ExportRepxToPdf_fileName);
                        break;
                    case "PDF":
                    default:
                        PdfExportOptions pdf_options = new PdfExportOptions();
                        pdf_options.PdfACompatibility = PdfACompatibility.PdfA1b;
                        //pdf_options.PasswordSecurityOptions.PermissionsPassword = "pwd";
                        //pdf_options.ShowPrintDialogOnOpen = true;
                        pdf_options.DocumentOptions.Application = Application.ProductName;
                        pdf_options.DocumentOptions.Author = Application.CompanyName;
                        pdf_options.DocumentOptions.Subject = "V6";
                        pdf_options.DocumentOptions.Title = "V6";
                        IList<string> result = pdf_options.Validate();
                        if (result.Count > 0)
                            Console.WriteLine(String.Join(Environment.NewLine, result));
                        else
                            ExportRepxToPdf_repx.ExportToPdf(ExportRepxToPdf_fileName, pdf_options);
                        break;
                }
            }
            catch (Exception ex)
            {
                ExportRepxToPdf_running = false;
                var methodInfo = MethodBase.GetCurrentMethod();
                if (methodInfo.DeclaringType != null)
                {
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    V6ControlFormHelper.ShowErrorException(address, ex);
                }
            }
            //return false;
            ExportRepxToPdf_running = false;
        }
    }
}

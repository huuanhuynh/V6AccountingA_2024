using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SignTokenCore;
using Spy;
using Spy.SpyObjects;
using Spy.User32Constants;
using Spy.WindowHandle;
using V6ThuePostXmlApi;
using V6ThuePostXmlApi.AttachmentService;
using V6ThuePostXmlApi.PortalService;
using V6ThuePostXmlApi.PostObjects;
using V6ThuePostXmlApi.PublishService;
using V6ThuePostXmlApi.Web_References.BusinessService;
using V6Tools;
using V6Tools.V6Convert;
using ParseDBF = V6Tools.ParseDBF;

namespace V6ThuePost
{
    static class Program
    {
        public static bool _TEST_ = true;
        private static DateTime _TEST_DATE_ = DateTime.Now;
        #region ===== VAR =====
        /// <summary>
        /// Tên cờ V6STT_REC
        /// </summary>
        //private static string flagName = "";
        /// <summary>
        /// Cờ bắt đầu.
        /// </summary>
        static string flagFileName1 = ".flag1";
        /// <summary>
        /// Cờ thành công
        /// </summary>
        static string flagFileName2 = ".flag2";
        /// <summary>
        /// Cờ lỗi
        /// </summary>
        static string flagFileName3 = ".flag3";
        /// <summary>
        /// Lấy về số hóa đơn
        /// </summary>
        static string flagFileName4 = ".flag4";
        /// <summary>
        /// Cờ kết thúc
        /// </summary>
        static string flagFileName9 = ".flag9";

        /// <summary>
        /// Config.
        /// </summary>
        public static string link_Publish = "";
        public static string link_Portal = "";
        public static string link_Business = "";
        public static string link_Attachment = "";
        public static string methodUrl = "";
        public static string mst = "";
        /// <summary>
        /// services
        /// <para>Được gán khi đọc info</para>
        /// </summary>
        public static string username { get; set; }
        public static string password;
        /// <summary>
        /// admin
        /// </summary>
        private static string account;
        private static string accountpassword;
        /// <summary>
        /// serial của chứng thư công ty đã đăng ký trong hệ thống.
        /// </summary>
        private static string _SERIAL_CERT;
        //Auto input setting
        private static string token_password_title = "";
        private static string token_password = "";

        private static string _pattern, partten_field;
        private static string _seri, seri_field;
        private static string convert = "0";
        

        //Excel config
        private static string template_xls = "template.xls";
        private static string firstCell = "A4";
        private static bool insertRow = false;
        private static bool drawLine = false;
        private static string[] columns = null;
        private static IDictionary<string, string> column_config = new SortedDictionary<string, string>();
        private static List<ConfigLine> parameters_config = new List<ConfigLine>();

        /// <summary>
        /// key đầu ngữ v6fkey
        /// </summary>
        private static string fkey0 = "V6";
        /// <summary>
        /// key trong data
        /// </summary>
        public static string fkeyA;
        /// <summary>
        /// Tên file xuất ra (chưa có phần mở rộng).
        /// </summary>
        public static string exportName;
        /// <summary>
        /// key param
        /// </summary>
        private static string fkeyP;

        private static string fkeyexcel0 = "V6";

        #endregion var

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var startupPath = Application.StartupPath;
            var dir = new DirectoryInfo(startupPath);
            var dir_name = dir.Name.ToLower();
            if (dir_name == "debug")
            {
                _TEST_ = true;
                MessageBox.Show("Test");
            }
            else
            {
                _TEST_ = false;
            }


            //MessageBox.Show("Debug!");
            if (args != null && args.Length > 0)
            {
                string result = "";
                string mode = "";
                string arg1_xmlFile = "";
                string arg2 = "";
                string arg3 = "";//File upload mode M1, data mode M2, old_fkey mode S
                string arg4 = "";//File upload mode S, [save file pdf mode E]
                try
                {
                    mode = args[0];
                    if (args.Length > 1) arg1_xmlFile = args[1];
                    if (args.Length > 2) arg2 = args[2];
                    if (args.Length > 3) arg3 = args[3];
                    if (args.Length > 4) arg4 = args[4];
                    
                    ReadXmlInfo(arg1_xmlFile);

                    if (String.Equals(mode, "ImportCert", StringComparison.CurrentCultureIgnoreCase))
                    {
                        result = ImportCertWithToken_Dll();
                    }
                    else if (String.Equals(mode, "DownloadInvPDFFkey", StringComparison.CurrentCultureIgnoreCase))
                    {
                        MessageBox.Show("Test DownloadInvPDFFkey");
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);
                        string invXml = DownloadInvPDFFkey(fkeyA);
                        string so_hoa_don = GetSoHoaDon(invXml);
                        WriteFlag(flagFileName4, so_hoa_don);
                        result += so_hoa_don;
                        //result += invXml;
                    }
                    else if (mode.ToLower() == "DownloadInvPDFFkeyNoPay".ToLower())
                    {
                        MessageBox.Show("Test DownloadInvPDFFkeyNoPay");
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);
                        string invXml = DownloadInvPDFFkeyNoPay(fkeyA);
                        string fileName = fkeyA + ".pdf";
                        MakeFile(invXml, fileName);
                        PdfiumViewerForm pdfView = new PdfiumViewerForm(fileName, "PDF");
                        pdfView.ShowDialog();
                        //WriteFlag(flagFileName4, so_hoa_don);
                        result += fileName;
                    }
                    else if (mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                    {
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);

                        string invXml = DownloadInvFkeyNoPay(fkeyA);
                        string so_hoa_don = GetSoHoaDon(invXml);
                        WriteFlag(flagFileName4, so_hoa_don);
                        result += so_hoa_don;
                        //result += invXml;
                    }
                    else if (mode.StartsWith("DOWNLOAD"))
                    {
                        var xml = DownloadInvPDFFkey(fkeyA);
                    }
                    else if (mode == "MI")
                    {
                        var xml = ReadDataXml(arg2);
                        File.Create(flagFileName1).Close();
                        result = getHashInvWithToken(xml);
                    }
                    //MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                    else if (mode.StartsWith("M") || mode == "")
                    {
                        var xml = ReadDataXml(arg2);
                        File.Create(flagFileName1).Close();

                        if (true)
                        {
                            Program.StartAutoInputTokenPassword();
                            string resultM = PublishInvWithToken_Dll(xml);
                            result = resultM;
                            //"OK:mẫu số;ký hiệu-Fkey_Số hóa đơn,"
                            //"OK:01GTKT0/001;VT/19E-A0283806HDA_XXX"
                            if (resultM.StartsWith("OK"))
                            {
                                string so_hoa_don = GetSoHoaDon_Dll(resultM);
                                WriteFlag(flagFileName4, so_hoa_don);
                            }
                            else // chạy lần 2
                            {
                                Program.StartAutoInputTokenPassword();
                                resultM = PublishInvWithToken_Dll(xml);
                                result = resultM;
                                if (resultM.StartsWith("OK"))
                                {
                                    string so_hoa_don = GetSoHoaDon_Dll(resultM);
                                    WriteFlag(flagFileName4, so_hoa_don);
                                }
                                else // Đã chạy 2 lần vẫn không được.
                                {
                                    //WriteFlag(flagFileName3, resultM);
                                }
                            }

                            // Gửi file.
                            if (arg3.Length > 0 && result.StartsWith("OK"))
                            {
                                if (mode.EndsWith("1")) //Gửi file excel có sẵn
                                {
                                    if (File.Exists(arg3))
                                    {
                                        result += UploadInvAttachmentFkey(fkeyA, arg3);
                                    }
                                    else
                                    {
                                        result += "Không tồn tại " + arg3;
                                    }
                                }
                                else if (mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                                {
                                    string export_file;
                                    bool export_ok = ExportExcel(arg3, out export_file, ref result);

                                    if (export_ok && File.Exists(export_file))
                                    {
                                        result += UploadInvAttachmentFkey(fkeyA, export_file);
                                    }
                                }
                                else if (mode.EndsWith("3")) // Tự xuất pdf rồi gửi
                                {
                                    string export_file = null;
                                    if (string.IsNullOrEmpty(exportName))
                                    {
                                        var save = new SaveFileDialog
                                        {
                                            Filter = "Pdf files (*.pdf)|*.pdf",
                                            Title = "Xuất pdf để gửi đi.",
                                        };
                                        if (save.ShowDialog() == DialogResult.OK)
                                        {
                                            export_file = save.FileName;
                                        }
                                        else
                                        {
                                            export_file = null;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        export_file = exportName + ".pdf";
                                    }
                                    string dbfDataFile = arg3;
                                    string rptFile = arg4;
                                    ReportDocument rpt = new ReportDocument();
                                    rpt.Load(rptFile);
                                    DataSet ds = new DataSet();
                                    DataTable data1 = ReadDbf(dbfDataFile);
                                    data1.TableName = "DataTable1";
                                    DataTable data2 = data1.Clone();
                                    data2.TableName = "DataTable2";
                                    var row0Data = data1.Rows[0].ToDataDictionary();
                                    data2.AddRow(row0Data);
                                    ds.Tables.Add(data1);
                                    ds.Tables.Add(data2);
                                    string tien_bang_chu =
                                        MoneyToWords(ObjectAndString.ObjectToDecimal(row0Data["T_TT"]), "V", "VND");
                                    rpt.SetDataSource(ds);
                                    rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);
                                    bool export_ok = ExportRptToPdf(null, rpt, export_file);
                                    if (export_ok)
                                    {
                                        result += "\r\nExport ok.";
                                    }
                                    else
                                    {
                                        result += "\r\nExport fail.";
                                    }

                                    if (export_ok && File.Exists(export_file))
                                    {
                                        result += UploadInvAttachmentFkey(fkeyA, export_file);
                                    }
                                }
                            }
                        }
                        else if (false)
                        {
                            #region ==== mode M M1:tạo mới gửi excel có sẵn  M2:tạo mới+xuất excel rồi gửi.

                            result = ImportAndPublishInv(xml);
                            //"OK:01GTKT0/001;VT/19E-V6XNCT_05A0283806HDA"
                            string invXml = DownloadInvFkeyNoPay(fkeyA);
                            string so_hoa_don = GetSoHoaDon(invXml);
                            WriteFlag(flagFileName4, so_hoa_don);

                            if (arg3.Length > 0 && result.StartsWith("OK"))
                            {
                                if (mode.EndsWith("1")) //Gửi file excel có sẵn
                                {
                                    if (File.Exists(arg3))
                                    {
                                        result += UploadInvAttachmentFkey(fkeyA, arg3);
                                    }
                                    else
                                    {
                                        result += "Không tồn tại " + arg3;
                                    }
                                }
                                else if (mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                                {
                                    string export_file;
                                    bool export_ok = ExportExcel(arg3, out export_file, ref result);

                                    if (export_ok && File.Exists(export_file))
                                    {
                                        result += UploadInvAttachmentFkey(fkeyA, export_file);
                                    }
                                }
                                else if (mode.EndsWith("3")) // Tự xuất pdf rồi gửi
                                {
                                    string export_file = null;
                                    if (string.IsNullOrEmpty(exportName))
                                    {
                                        var save = new SaveFileDialog
                                        {
                                            Filter = "Pdf files (*.pdf)|*.pdf",
                                            Title = "Xuất pdf để gửi đi.",
                                        };
                                        if (save.ShowDialog() == DialogResult.OK)
                                        {
                                            export_file = save.FileName;
                                        }
                                        else
                                        {
                                            export_file = null;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        export_file = exportName + ".pdf";
                                    }
                                    string dbfDataFile = arg3;
                                    string rptFile = arg4;
                                    ReportDocument rpt = new ReportDocument();
                                    rpt.Load(rptFile);
                                    DataSet ds = new DataSet();
                                    DataTable data1 = ReadDbf(dbfDataFile);
                                    data1.TableName = "DataTable1";
                                    DataTable data2 = data1.Clone();
                                    data2.TableName = "DataTable2";
                                    var row0Data = data1.Rows[0].ToDataDictionary();
                                    data2.AddRow(row0Data);
                                    ds.Tables.Add(data1);
                                    ds.Tables.Add(data2);
                                    string tien_bang_chu =
                                        MoneyToWords(ObjectAndString.ObjectToDecimal(row0Data["T_TT"]), "V", "VND");
                                    rpt.SetDataSource(ds);
                                    rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);
                                    bool export_ok = ExportRptToPdf(null, rpt, export_file);
                                    if (export_ok)
                                    {
                                        result += "\r\nExport ok.";
                                    }
                                    else
                                    {
                                        result += "\r\nExport fail.";
                                    }

                                    if (export_ok && File.Exists(export_file))
                                    {
                                        result += UploadInvAttachmentFkey(fkeyA, export_file);
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                    else if (mode.StartsWith("S"))
                    {
                        if (mode == "S_OLD")
                        {
                            var xml = ReadDataXmlS(dbfFile: arg2);
                            File.Create(flagFileName1).Close();
                            result = adjustInv(xml, fkey_old: arg3);
                            if (arg4.Length > 0 && result.StartsWith("OK"))
                            {
                                if (File.Exists(arg4))
                                {
                                    result += UploadInvAttachmentFkey(fkeyA, arg4);
                                }
                                else
                                {
                                    result += "Không tồn tại " + arg4;
                                }
                            }
                        }
                        else if (true)
                        {
                            var xml = ReadDataXmlS(dbfFile: arg2);
                            //var xml = ReadDataXmlT(dbfFile: arg2);
                            File.Create(flagFileName1).Close();
                            string so_hoa_don = arg3;
                            int type = ObjectAndString.ObjectToInt(arg4);
                            var invToken = string.Format("{0};{1};{2}", _pattern, _seri, so_hoa_don);
                            result = AdjustReplaceInvWithToken_Dll(xml, _SERIAL_CERT, type, invToken);
                            //type: thay thế = 1, điều chỉnh tăng = 2, điều chỉnh giảm = 3, điều chỉnh thông tin = 4
                        }
                    }
                    else if (mode == "T")
                    {
                        var xml = ReadDataXmlT(arg2);
                        File.Create(flagFileName1).Close();
                        result = replaceInv(xml, arg3);
                    }
                    else if (mode.StartsWith("G"))
                    {
                        MakeFlagNames(arg2);
                        if (mode == "G1") // Gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                            result = confirmPaymentFkey(arg2);
                        }
                        else if (mode == "G2") // Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
                        {
                            File.Create(flagFileName1).Close();
                            result = confirmPayment(arg2);
                        }
                        else if (mode == "G3") // Hủy gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                            result = UnconfirmPaymentFkey(arg2);
                        }
                    }
                    else if (mode == "H")
                    {
                        MakeFlagNames(arg2);
                        File.Create(flagFileName1).Close();
                        result = cancelInv(fkey_old: arg2);
                        //result = CancelInvoiceWithToken_Dll(xmlData);
                    }
                    else if (mode.StartsWith("U"))//U1,U2
                    {
                        if (mode == "U")        // upload file có sẵn, fkey truyền vào
                        {
                            string fkey = arg2;
                            string file = arg3;
                            MakeFlagNames(fkey);
                            result = UploadInvAttachmentFkey(fkey, file);
                        }
                        else if (mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                        {
                            ReadDataXml(arg2);
                            string fkey = fkeyA;
                            MakeFlagNames(fkey);
                            result = UploadInvAttachmentFkey(fkey, fkey + ".xls");
                        }
                        else if (mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                        {
                            string export_file;
                            ReadDataXml(arg2);
                            bool export_ok = ExportExcel(arg3, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                        else if (mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
                        {
                            string export_file;
                            ReadDataXml(arg2);
                            bool export_ok = ExportExcel(arg3, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += "\r\nExport ok.";
                            }
                        }
                    }
                    else if (mode.StartsWith("E"))
                    {
                        if (mode == "E")
                        {
                            
                        }
                        else if (mode == "E1")
                        {
                            try
                            {
                                string dbfDataFile = arg2;
                                string rptFile = arg3;
                                string saveFile = arg4;

                                string export_file;
                                ReadDataXml(arg2);
                                bool export_ok = ExportExcel(dbfDataFile, out export_file, ref result);

                                if (export_ok && File.Exists(export_file))
                                {
                                    result += "\r\nE1 Export ok.";
                                }
                                else
                                {
                                    result += "\r\nE1 Export fail.";
                                }
                            }
                            catch (Exception ex)
                            {
                                 result = "ERR:E1EX: " + ex.Message + "\n" + result;
                                Logger.WriteExLog("E1", ex);
                            }
                        }
                        else if (mode == "E2")  // Xuất PDF bằng RPT
                        {
                            try
                            {
                                string dbfDataFile = arg2;
                                string rptFile = arg3;
                                string saveFile = arg4;

                                ReportDocument rpt = new ReportDocument();
                                rpt.Load(rptFile);
                                DataSet ds = new DataSet();
                                DataTable data1 = ReadDbf(dbfDataFile);
                                data1.TableName = "DataTable1";
                                DataTable data2 = data1.Clone();
                                data2.TableName = "DataTable2";
                                var row0Data = data1.Rows[0].ToDataDictionary();
                                data2.AddRow(row0Data);
                                ds.Tables.Add(data1);
                                ds.Tables.Add(data2);
                                string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0Data["T_TT"]), "V", "VND");
                                rpt.SetDataSource(ds);
                                rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                                bool export_ok = false;
                                if (string.IsNullOrEmpty(saveFile))
                                {
                                    export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                                }
                                else
                                {
                                    export_ok = ExportRptToPdf(null, rpt, saveFile);
                                }

                                if (export_ok)
                                {
                                    result += "\r\nE2 Export ok.";
                                }
                                else
                                {
                                    result += "\r\nE2 Export fail.";
                                }
                            }
                            catch (Exception ex)
                            {
                                result = "ERR:E2EX: " + ex.Message + "\n" + result;
                                Logger.WriteExLog("E2", ex);
                            }
                        }
                    }
                    else if (mode.StartsWith("D"))
                    {
                        string type = "1";
                        if (mode.Length > 1) type = mode[1].ToString();
                        result = DoUpdateCus(arg2, type);
                    }
                    else if (mode.StartsWith("P")) // P0 xml fkey
                    {
                        MessageBox.Show("Test mode:" + mode);
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);
                        string invXml = DownloadInvPDFFkeyNoPay(fkeyA);
                        string fileName = null;

                        if (invXml.StartsWith("ERR")) goto EndP;

                        fileName = fkeyA + "_IN.pdf";
                        MakeFile(invXml, fileName);
                        PdfiumViewerForm pdfView = new PdfiumViewerForm(fileName, "PDF");

                        if (mode.EndsWith("0")) // P...0 // In luôn ra máy in mặc định.
                        {
                            pdfView.PrintToDefaultPrinter();
                            //WriteFlag(flagFileName4, so_hoa_don);
                        }
                        else if (mode.EndsWith("1"))
                        {
                            pdfView.AutoClickPrint = true;
                            pdfView.ShowDialog();
                            //WriteFlag(flagFileName4, so_hoa_don);
                        }
                        else if (mode.EndsWith("2")) // saved printer name
                        {
                            string printerName = ReadOldSelectPrinter();
                            if (string.IsNullOrEmpty(printerName))
                            {
                                // chạy giống P1
                                pdfView.AutoClickPrint = true;
                                pdfView.ShowDialog();
                            }
                            else
                            {
                                pdfView.PrintToPrinter(printerName);
                            }
                        }

                    EndP:
                        result += fileName ?? invXml;
                    }


                    if (result.Contains("Hóa đơn đã được gạch nợ."))
                    {
                        WriteFlag(flagFileName2, "GACH_NO");
                    }
                    else if (result.StartsWith("ERR"))
                    {
                        File.Create(flagFileName3).Close();
                    }
                    else
                    {
                        WriteFlag(flagFileName2, "OK");
                    }
                }
                catch (Exception ex)
                {
                    StopAutoInputTokenPassword();
                    File.Create(flagFileName3).Close();
                    result += "ERR:EX\r\n" + ex.Message;
                }
                File.Create(flagFileName9).Close();
                BaseMessage.Show(result, 500);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        private static void WriteFlag(string fileName, string content)
        {
            var fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Close();
            fs.Close();
        }

        private static string GetSoHoaDon(string invXml)
        {
            string result = "";
            try
            {
                string startTerm = "<InvoiceNo>";
                string endTerm = "</InvoiceNo>";
                int startIndex = invXml.IndexOf(startTerm, StringComparison.Ordinal);
                if (startIndex > 0)
                {
                    startIndex += startTerm.Length;
                    int endIndex = invXml.IndexOf(endTerm, StringComparison.Ordinal);
                    if (endIndex > startIndex)
                    {
                        result = invXml.Substring(startIndex, endIndex - startIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
        
        /// <summary>
        /// Lấy số hóa đơn từ chuỗi OK:ký hiệu;mẫu số-fkey_Số hóa đơn.
        /// </summary>
        /// <param name="result_string">OK:01GTKT0/001;VT/19E-A0283806HDA_XXX</param>
        /// <returns></returns>
        private static string GetSoHoaDon_Dll(string result_string)
        {
            string result = "";
            try
            {
                int _index = result_string.IndexOf("_", StringComparison.InvariantCulture);
                if (_index < 5) return null;
                result = result_string.Substring(_index+1, result_string.Length - _index - 1);
                if (result.EndsWith(",")) result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invXml">"OK:mẫu số;ký hiệu-Fkey_Số hóa đơn,"</param>
        /// <returns></returns>
        private static string GetSoHoaDon_Token(string invXml)
        {
            string result = "";
            try
            {
                string startTerm = "_";
                string endTerm = ",";
                int startIndex = invXml.IndexOf(startTerm, StringComparison.Ordinal);
                if (startIndex > 0)
                {
                    startIndex += startTerm.Length;
                    int endIndex = invXml.IndexOf(endTerm, StringComparison.Ordinal);
                    if (endIndex > startIndex)
                    {
                        result = invXml.Substring(startIndex, endIndex - startIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        private static string GetXmlValue_Simple(string invXml, string tagName)
        {
            string result = "";
            try
            {
                string startTerm = "<"+tagName+">";
                string endTerm = "</"+tagName+">";
                int startIndex = invXml.IndexOf(startTerm, StringComparison.OrdinalIgnoreCase);
                if (startIndex > 0)
                {
                    startIndex += startTerm.Length;
                    int endIndex = invXml.IndexOf(endTerm, StringComparison.OrdinalIgnoreCase);
                    if (endIndex > startIndex)
                    {
                        result = invXml.Substring(startIndex, endIndex - startIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        //public static bool ExportExcel_As(string dbfDataFile, out string exportFile, ref string result)
        //{

        //    try
        //    {
        //        var save = new SaveFileDialog
        //        {
        //            Filter = "Excel files (*.xls)|*.xls|*.xlsx|*.xlsx",
        //            Title = "Xuất Excel.",
        //        };
        //        if (save.ShowDialog() == DialogResult.OK)
        //        {
        //            exportFile = save.FileName;
        //            return ExportExcel(dbfDataFile, out exportFile, ref result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteExLog("Program.ExportRptToPdf_As", ex, "");
        //    }
        //    exportFile = null;
        //    return false;
        //}

        public static bool ExportExcel(string dbfDataFile, out string exportFile, ref string result)
        {
            try
            {
                if (File.Exists(dbfDataFile))
                {
                    var data2 = ReadDbf(dbfDataFile);
                    var data1 = data2.Clone();
                    var newRow = data1.NewRow();
                    var row0 = data2.Rows[0];
                    foreach (DataColumn column in data1.Columns)
                    {
                        newRow[column.ColumnName] = row0[column.ColumnName];
                    }
                    data1.Rows.Add(newRow);

                    //{Tuanmh 10/06/2018
                    //string export_file = fkeyA + ".xls";
                    string export_file = "";
                    if (string.IsNullOrEmpty(exportName))
                    {
                        var save = new SaveFileDialog
                        {
                            Filter = "Excel files (*.xls)|*.xls|*.xlsx|*.xlsx",
                            Title = "Xuất Excel.",
                        };
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            export_file = save.FileName;
                        }
                        else
                        {
                            exportFile = null;
                            return false;
                        }
                    }
                    else { 
                        export_file = exportName + ".xls";
                    }

                    // Tạo parameter.
                    SortedDictionary<string, object> parameters = new SortedDictionary<string, object>();
                    foreach (ConfigLine config_line in parameters_config)
                    {
                        string replaced_value = config_line.Value;
                        if (config_line.Value.Contains("{") && config_line.Value.Contains("}"))
                        {
                            var regex = new Regex("{(.+?)}");
                            foreach (Match match in regex.Matches(config_line.Value))
                            {
                                var matchKey = match.Groups[1].Value;
                                if (data1.Columns.Contains(matchKey))
                                {
                                    replaced_value = config_line.Value.Replace(match.Groups[0].Value,
                                        ObjectAndString.ObjectToString(row0[matchKey]));
                                }
                            }
                            if (parameters.ContainsKey(config_line.Field))
                            {
                                MessageBox.Show("Trùng khóa cấu hình excel: key=" + config_line.Field);
                                continue;
                            }
                            parameters.Add(config_line.Field, replaced_value);
                        }
                        else
                        {
                            if (data1.Columns.Contains(config_line.Value))
                            {
                                parameters.Add(config_line.Field, row0[replaced_value]);
                            }
                        }
                    }


                    bool export_ok = V6Tools.V6Export.ExportData.ToExcelTemplate(template_xls, data2, export_file,
                        firstCell, columns, parameters, NumberFormatInfo.InvariantInfo, insertRow, drawLine);

                    //{Tuanmh test
                    //File.Copy(export_file, "test_out.xls", true);
                    //}

                    if (export_ok)
                    {
                        result += "\r\nExport Excel ok";
                        exportFile = export_file;
                    }
                    else
                    {
                        exportFile = "";
                    }
                    
                    return export_ok;

                }
                else
                {
                    result += "Không tồn tại " + dbfDataFile;
                }
            }
            catch (Exception ex)
            {
                result += "Export Excel không thành công: " + ex.Message;
            }
            exportFile = "";
            return false;
        }

        public static bool ExportRptToPdf_As(IWin32Window owner, ReportDocument rpt, string defaultSaveName = "")
        {

            if (rpt == null)
            {
                return false;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Pdf files (*.pdf)|*.pdf",
                    Title = "Xuất PDF.",
                    FileName = ChuyenMaTiengViet.ToUnSign(defaultSaveName)
                };
                if (save.ShowDialog(owner) == DialogResult.OK)
                {
                    return ExportRptToPdf(owner, rpt, save.FileName);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("Program.ExportRptToPdf_As", ex, "");
            }
            return false;
        }

        public static bool ExportRptToPdf(IWin32Window owner, ReportDocument rpt, string fileName)
        {
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = fileName;
                CrExportOptions = rpt.ExportOptions;

                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;

                rpt.Export();
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("Program.ExportRptToPdf", ex, "");
            }
            return false;
        }

        /// <summary>
        /// Đọc dbf data và chuyển từ ABC về Unicode
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <returns></returns>
        private static DataTable ReadDbf(string dbfFile)
        {
            DataTable dataDbf = ParseDBF.ReadDBF(dbfFile);
            DataTable data = Data_Table.FromTCVNtoUnicode(dataDbf);
            return data;
        }

        public static string ReadDataXml(string dbfFile)
        {
            string result = "";
            column_config = new SortedDictionary<string, string>();
            parameters_config = new List<ConfigLine>();
            //try
            {
                //PostObject obj = new PostObject();
                Invoices postObject = new Invoices();
                //ReadXmlInfo(xmlFile);
                DataTable data = ReadDbf(dbfFile);
                
                //Fill data to postObject
                //var invs = new List<Inv>();
                var inv = new Inv();
                //invs.Add(inv);
                postObject.Inv.Add(inv);

                DataRow row0 = data.Rows[0];
                if (_TEST_)
                {
                    fkeyA = fkey0 + _TEST_DATE_.ToString("yyMMddHHmmss");
                }
                else
                {
                    fkeyA = fkey0 + row0["STT_REC"];
                }

                //{Tuanmh V6_05_2222222222222222_3333333333333
                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                //}

                inv.key = fkeyA;
                _pattern = row0[partten_field].ToString().Trim();
                _seri = row0[seri_field].ToString().Trim();
                //flagName = fkeyA;
                MakeFlagNames(fkeyA);
                

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    if (_TEST_ && item.Key == "CusName")
                    {
                        inv.Invoice[item.Key] = "" + _TEST_DATE_.ToString("yyyyMMdd:HHmmss");
                    }
                    else if (_TEST_ && item.Key == "EmailDeliver")
                    {
                        inv.Invoice[item.Key] = "huuan.huynh87@gmail.com";
                    }
                    else if (_TEST_ && item.Key == "SMSDeliver")
                    {
                        inv.Invoice[item.Key] = "0354867043";
                    }
                    else
                    {
                        inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    }                    
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }
                
                
                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }


                var products = new Products();
                foreach (DataRow row in data.Rows)
                {
                    //if (row["STT"].ToString() == "0") continue;

                    Product product = new Product();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                inv.Invoice["Products"] = products;
                
                
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }
                
                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = postObject.ToXml();// XmlConverter.ClassTo Xml(postObject);
            }
            //catch (Exception ex)
            {
                //
            }

            string fileName = Path.Combine(Application.StartupPath, "Data.xml");
            WriteFlag(fileName, result);
            
            return result;
        }

        public static Customer ReadCusDataXml(DataRow row)
        {
            string result = "";
            Customer cus = new Customer();

            foreach (KeyValuePair<string, ConfigLine> item in customerInfoConfig)
            {
                cus.Customer_Info[item.Key] = GetValue(row, item.Value);
            }
            
            return cus;
        }

        private static void MakeFlagNames(string flagName)
        {
            flagFileName1 = flagName + ".flag1";
            flagFileName2 = flagName + ".flag2";
            flagFileName3 = flagName + ".flag3";
            flagFileName4 = flagName + ".flag4";
            flagFileName9 = flagName + ".flag9";
        }

        public static string ReadDataXmlS(string dbfFile)
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                AdjustInv inv = new AdjustInv();
                //ReadXmlInfo(xmlFile);
                DataTable data = ReadDbf(dbfFile);

                //Fill data to postObject
                
                DataRow row0 = data.Rows[0];
                if (_TEST_)
                {
                    fkeyA = fkey0 + _TEST_DATE_.ToString("yyMMddHHmmss");
                }
                else
                {
                    fkeyA = fkey0 + row0["STT_REC"];
                }
                inv.key = fkeyA;
                _pattern = row0[partten_field].ToString().Trim();
                _seri = row0[seri_field].ToString().Trim();
                MakeFlagNames(fkeyA);

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }


                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                var products = new List<Product>();
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    Product product = new Product();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                inv.Invoice["Products"] = products;

                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = inv.ToXml();// = XmlConverter.ClassTo Xml(inv);
            }
            //catch (Exception ex)
            {
                //
            }
            return result;
        }
        
        /// <summary>
        /// Đọc và tạo xml data Thay thế.
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <returns></returns>
        public static string ReadDataXmlT(string dbfFile)
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                ReplaceInv inv = new ReplaceInv();
                //ReadXmlInfo(xmlFile);
                DataTable data = ReadDbf(dbfFile);

                //Fill data to postObject
                
                DataRow row0 = data.Rows[0];
                if (_TEST_)
                {
                    fkeyA = fkey0 + _TEST_DATE_.ToString("yyMMddHHmmss");
                }
                else
                {
                    fkeyA = fkey0 + row0["STT_REC"];
                }
                inv.key = fkeyA;
                _pattern = row0[partten_field].ToString().Trim();
                _seri = row0[seri_field].ToString().Trim();
                MakeFlagNames(fkeyA);

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }


                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                var products = new List<Product>();
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    Product product = new Product();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                inv.Invoice["Products"] = products;

                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = inv.ToXml();// = XmlConverter.ClassToXml(inv);
            }
            //catch (Exception ex)
            {
                //
            }
            return result;
        }
        
        private static object GetValue(DataRow row, ConfigLine config)
        {
            object fieldValue = config.Value;
            DataTable table = row.Table;

            string configTYPE = null, configDATATYPE = null;
            if (!string.IsNullOrEmpty(config.Type))
            {
                string[] ss = config.Type.Split(':');
                configTYPE = ss[0].ToUpper();
                if (ss.Length > 1) configDATATYPE = ss[1].ToUpper();
            }
            if (string.IsNullOrEmpty(configDATATYPE))
            {
                configDATATYPE = (config.DataType ?? "").ToUpper();
            }

            if (configTYPE == "ENCRYPT")
            {
                return UtilityHelper.DeCrypt(fieldValue.ToString());
            }

            if (configTYPE == "FIELD" && !string.IsNullOrEmpty(config.FieldV6))
            {
                // FieldV6 sẽ có dạng thông thường là (Field) hoặc dạng ghép là (Field1 + Field2) hoặc (Field1 + "abc" + field2)
                if (table.Columns.Contains(config.FieldV6))
                {
                    fieldValue = row[config.FieldV6];
                    if (table.Columns[config.FieldV6].DataType == typeof(string))
                    {
                        //Trim
                        fieldValue = fieldValue.ToString().Trim();
                    }
                }
                else
                {
                    decimal giatribt;
                    if (Number.GiaTriBieuThucTry(config.FieldV6, row.ToDataDictionary(), out giatribt))
                    {
                        fieldValue = giatribt;
                    }
                    else
                    {
                        var fields = ObjectAndString.SplitStringBy(config.FieldV6, '+');

                        string fieldValueString = null;

                        foreach (string s in fields)
                        {
                            string field = s.Trim();
                            if (table.Columns.Contains(field))
                            {
                                fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                            }
                            else
                            {
                                if (field.StartsWith("\"") && field.EndsWith("\""))
                                {
                                    field = field.Substring(1, field.Length - 2);
                                }
                                fieldValueString += field;
                            }
                        }
                        // Chốt.
                        fieldValue = fieldValueString;
                    }// end else giatribieuthuc
                }
            }

            if (!string.IsNullOrEmpty(configDATATYPE))
            {
                switch (configDATATYPE)
                {
                    case "BOOL":
                        if (fieldValue is bool)
                        {
                            return fieldValue;
                        }
                        else
                        {
                            return fieldValue != null &&
                                (fieldValue.ToString() == "1" ||
                                   fieldValue.ToString().ToLower() == "true" ||
                                   fieldValue.ToString().ToLower() == "yes");
                        }
                    case "N2C":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                    case "DECIMAL":
                    case "MONEY":
                        return ObjectAndString.ObjectToDecimal(fieldValue);
                    case "INT":
                        return ObjectAndString.ObjectToInt(fieldValue);
                    case "INT64":
                    case "LONG":
                        return ObjectAndString.ObjectToInt64(fieldValue);
                    case "UPPER":
                        return (fieldValue + "").ToUpper();
                    default:
                        return fieldValue;
                }
            }
            else
            {
                return fieldValue;
            }
        }

        public static string MoneyToWords(decimal money, string lang, string ma_nt)
        {
            if (lang == "V")
            {
                return DocSo.DOI_SO_CHU_NEW(money, V6Alnt_begin1(ma_nt), V6Alnt_end1(ma_nt), V6Alnt_only1(ma_nt), V6Alnt_point1(ma_nt),
                    V6Alnt_endpoint1(ma_nt));
            }
            else
            {
                return DocSo.NumWordsWrapper(money, V6Alnt_begin2(ma_nt), V6Alnt_end2(ma_nt), V6Alnt_only2(ma_nt), V6Alnt_point2(ma_nt),
                    V6Alnt_endpoint2(ma_nt));
            }
        }

        #region ==== ALNT ====
        private static string V6Alnt_endpoint1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "xu";
                case "EUR":
                    return "";
                case "USD":
                    return "xu";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_point1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "phẩy";
                case "EUR":
                    return "";
                case "USD":
                    return "phẩy";
                case "VND":
                    return "phẩy";
                default:
                    return "";
            }
        }

        private static string V6Alnt_only1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "chẵn";
                default:
                    return "";
            }
        }

        private static string V6Alnt_end1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "đô la Úc";
                case "EUR":
                    return "Euro";
                case "USD":
                    return "đô la Mỹ";
                case "VND":
                    return "đồng";
                default:
                    return "";
            }
        }

        private static string V6Alnt_begin1(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }
        
        private static string V6Alnt_endpoint2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "Cent(s)";
                case "EUR":
                    return "";
                case "USD":
                    return "Cent(s)";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_point2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "and";
                case "EUR":
                    return "";
                case "USD":
                    return "and";
                case "VND":
                    return "point";
                default:
                    return "";
            }
        }

        private static string V6Alnt_only2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "only";
                case "EUR":
                    return "";
                case "USD":
                    return "only";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }

        private static string V6Alnt_end2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "AUD Dollars";
                case "EUR":
                    return "";
                case "USD":
                    return "Dollars";
                case "VND":
                    return "VND";
                default:
                    return "";
            }
        }

        private static string V6Alnt_begin2(string maNt)
        {
            switch (maNt)
            {
                case "AUD":
                    return "";
                case "EUR":
                    return "";
                case "USD":
                    return "";
                case "VND":
                    return "";
                default:
                    return "";
            }
        }
        #endregion ==== ALNT ====

        /// <summary>
        /// Phát hành hóa đơn.
        /// </summary>
        /// <param name="xml">Dữ liệu các hóa đơn.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public static string ImportInv(string xml)
        {
            string result = null;
            try
            {
                var publishService = new PublishService(link_Publish);
                result = publishService.ImportInv(xml, username, password, convert == "1" ? 1 : 0);
                //"OK:01GTKT0/001;VT/19E-V6XNCT_05A0283806HDA"      "OK:01GTKT0/001;VT/19E-A0283806HDA" "OK:01GTKT0/001;VT/19E-A0283807HDA"
                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:20"))
                {
                    result += "\r\nPattern và serial không phù hợp, hoặc không tồn tại hóa đơn đã đăng kí có sử dụng Pattern và serial truyền vào.";
                }
                else if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:10"))
                {
                    result += "\r\nLô có số hóa đơn vượt quá max cho phép.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông đủ số hóa đơn cho lô phát hành.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    result += "\r\nKhông phát hành được hóa đơn.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    result += "\r\nDữ liệu xml đầu vào không đúng quy định.\nKhông có hóa đơn nào được phát hành.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.ImportAndPublishInv " + result);
            return result;
        }

        /// <summary>
        /// Phát hành hóa đơn.
        /// </summary>
        /// <param name="xml">Dữ liệu các hóa đơn.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public static string ImportAndPublishInv(string xml)
        {
            string result = null;
            try
            {
                var publishService = new PublishService(link_Publish);
                result = publishService.ImportAndPublishInv(account, accountpassword, xml, username, password, _pattern, _seri, convert == "1" ? 1 : 0);
                //result = publishService.ImportInv(xml, username, password, convert == "1" ? 1 : 0);
                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:20"))
                {
                    result += "\r\nPattern và serial không phù hợp, hoặc không tồn tại hóa đơn đã đăng kí có sử dụng Pattern và serial truyền vào.";
                }
                else if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:10"))
                {
                    result += "\r\nLô có số hóa đơn vượt quá max cho phép.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông đủ số hóa đơn cho lô phát hành.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    result += "\r\nKhông phát hành được hóa đơn.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    result += "\r\nDữ liệu xml đầu vào không đúng quy định.\nKhông có hóa đơn nào được phát hành.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.ImportAndPublishInv " + result);
            return result;
        }

        public static string DownloadInvPDFFkey(string fkey)
        {
            string result = null;
            try
            {
                result = new PortalService(link_Portal).downloadInvPDFFkey(fkey, username, password);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:11"))
                {
                    result += "\r\nHóa đơn chưa thanh toán nên không xem được.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
                else if (result.StartsWith("ERR"))
                {
                    result += "\r\nCó lỗi.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.DownloadInvFkeyNoPay " + result);
            return result;
        }
        
        public static string DownloadInvPDFFkeyNoPay(string fkey)
        {
            string result = null;
            try
            {
                result = new PortalService(link_Portal).downloadInvPDFFkeyNoPay(fkey, username, password);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:11"))
                {
                    result += "\r\nHóa đơn chưa thanh toán nên không xem được.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
                else if (result.StartsWith("ERR"))
                {
                    result += "\r\nCó lỗi.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.DownloadInvFkeyNoPay " + result);
            return result;
        }

        public static string DownloadInvFkeyNoPay(string fkey)
        {
            string result = null;
            try
            {
                result = new PortalService(link_Portal).downloadInvFkeyNoPay(fkey, username, password);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
                else if (result.StartsWith("ERR"))
                {
                    result += "\r\nCó lỗi.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.DownloadInvFkeyNoPay " + result);
            return result;
        }

        #region ========= TOKEN ==============

        //STT  Mã lỗi  Mô tả
        //0  ERR:0   Lỗi Fkey đã tồn tại.
        //1  ERR:-3  Có lỗi trong quá trình lấy chứng thư
        //2  ERR:-2  Chứng thư không có privatekey
        //3  ERR:-1  Ấn nút hủy khi nhập mã pin của chứng thư
        //4  ERR:1  Không có quyền truy cập webservice
        //5  ERR:2  Không tồn tại hoá đơn cần thay thế/điều chỉnh
        //6  ERR:3  Định dạng file xml hóa đơn không đúng
        //7  ERR:4  token hóa đơn sai định dạng
        //8  ERR:5  Có lỗi xảy ra
        //9  ERR:6  Không còn đủ số hóa đơn cho lô phát hành
        //10  ERR:7  Không tìm thấy chứng thư trong máy. Hãy cắm token
        //11  ERR:8  Hoá đơn đã được điều chỉnh, thay thế
        //12  ERR:10  Số lượng hóa đơn truyền vào lớn hơn maxBlockInv
        //13  ERR:19  pattern truyền vào không giống với pattern của hoá đơn cần điều chỉnh/thay thế
        //14  ERR:20  Tham số mẫu số và ký hiệu truyền vào không hợp lệ
        //15  ERR:21  Không tìm thấy công ty trên hệ thống
        //16  ERR:22  Công ty chưa đăng ký thông tin keystore
        //17  ERR:23  Chứng thư truyền lên không đúng định dạng
        //18  ERR:24  Chứng thư truyền lên không đúng với chứng thư công ty đăng ký trên hệ thống
        //19  ERR:26  Chứng thư đã hết hạn
        //20  ERR:27  Chứng thư chưa đến thời điểm sử dụng
        //21  ERR:28  Thông tin chứng thư chưa có trong hệ thống
        //22  ERR:30  Tạo mới lô hóa đơn lỗi (fkey trùng,…)

        public static string getHashInvWithToken(string xmlInvData)
        {
            string result = null;
            try
            {
                // Bước 2: getHashInvWithToken();
                //  trong dll chỉ có hàm GetHashInv()
                var publishService = new PublishService(link_Publish);
                // mẫu số;ký hiệu;số hóa đơn vd:01GTKT0/001;AA/17E;1
                string invToken = string.Format("{0};{1};{2}", "mau_so", "ky_hieu", "sohd");
                    //chỉ cần khi điều chỉnh, thay thế, phát hành thì = “”
                invToken = "";
                string getHashXml = null;
                getHashXml = publishService.getHashInvWithToken(account, accountpassword, xmlInvData, username, password,
                    _SERIAL_CERT, 0, invToken, _pattern, _seri, 0);

                //<Invoices><Inv><key>A0283806HDA</key><idInv>646972</idInv><hashValue>Xvnk+BZ7PH2j0tgdQ9uOZFVrnus=</hashValue><pattern>01GTKT0/001</pattern><serial>VT/19E</serial></Inv></Invoices>
                //getHashXml = GetHashInv(xmlInvData);
                result += getHashXml;

                string getHash = GetXmlValue_Simple(getHashXml, "hashValue");
                string getFkey = GetXmlValue_Simple(getHashXml, "key");
                string getIdInv = GetXmlValue_Simple(getHashXml, "idInv");
                WriteFlag(flagFileName4, getIdInv);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.getHashInvWithToken " + result);
            return result;
        }
        
        public static string PhatHanhHoaDon_Token4buoc(string xmlInvData)
        {
            string result = null;
            try
            {
                // Bước 1: Bước tạo ra giá trị xmlInvData

                // Bước 2: getHashInvWithToken();
                //  trong dll chỉ có hàm GetHashInv()
                var publishService = new PublishService(link_Publish);
                // mẫu số;ký hiệu;số hóa đơn vd:01GTKT0/001;AA/17E;1
                string invToken = string.Format("{0};{1};{2}", "mau_so", "ky_hieu", "sohd");//chỉ cần khi điều chỉnh, thay thế, phát hành thì = “”
                invToken = "";
                string getHashXml = null;
                try
                {
                    getHashXml = publishService.getHashInvWithToken(account, accountpassword, xmlInvData, username, password, _SERIAL_CERT, 0, invToken, _pattern, _seri, 0);// GetHashInv()
                }
                catch (Exception ex)
                {
                    //Báo lỗi, return
                    ;
                }
                //<Invoices><Inv><key>A0283806HDA</key><idInv>646972</idInv><hashValue>Xvnk+BZ7PH2j0tgdQ9uOZFVrnus=</hashValue><pattern>01GTKT0/001</pattern><serial>VT/19E</serial></Inv></Invoices>
                //getHashXml = GetHashInv(xmlInvData);
                result += getHashXml;
                string getHash = GetXmlValue_Simple(getHashXml, "hashValue");
                string getFkey = GetXmlValue_Simple(getHashXml, "key");
                string getIdInv = GetXmlValue_Simple(getHashXml, "idInv");
                // Bước 3: Ký số lên chuỗi <hashValue> đơn vị tích hợp thực hiện việc này tham khảo qua trang web
                // getHasH = rKdYgeYc7CYLOhjfNFDZ8nBaWjA=   BtTapDKxzoW5cMRcbleviJbDL4U=
                // đưa chuỗi getHash về mảng byte.
                var hashByte = Convert.FromBase64String(getHash);
                var digitalSignByte = V6DigitalSign.GetDigitalSignature(hashByte);

                // Bước 4:
                var digitalSign = Convert.ToBase64String(digitalSignByte);//WseKwNHQZy2DgssoeWPGpmu9v5hUonZyPGAHbWbiB63JZuYTETXGdbkT5oTHSlM6jtx8EhqfdU+qHuvTEsG9MRBvkmUcbwSHhk+1enWsUNGLhf5x+C2B2ePBobX46QuP7ddy//qYdfKT/e1glIrxeqeSZ8HWjJiaCXErACsafxs=
                
                string xmlInvDataSigned = ""
                                          + "<Invoices>"
                                          + "<SerialCert>"+_SERIAL_CERT.ToUpper()+"</SerialCert>"
                                          + "<Inv>"
                                          + "<key>"+getFkey+"</key>"                            // fkey
                                          + "<idInv>"+getIdInv+"</idInv>"                         // id hóa đơn trên hệ thống vnpt
                                          + "<signValue>" + XmlConverter.FixXmlValueChar(digitalSign) + "</signValue>"
                                          + "</Inv>"
                                          + "</Invoices>";
                
                var result4 = publishService.publishInvWithToken(account, accountpassword, xmlInvDataSigned, username, password, _pattern, _seri);
                //<Invoices><SerialCert>‎5404FFFEB7033FB316D672201B7A2249</SerialCert><Inv><key>A0283806HDA</key><idInv>647292</idInv><signValue>WseKwNHQZy2DgssoeWPGpmu9v5hUonZyPGAHbWbiB63JZuYTETXGdbkT5oTHSlM6jtx8EhqfdU+qHuvTEsG9MRBvkmUcbwSHhk+1enWsUNGLhf5x+C2B2ePBobX46QuP7ddy//qYdfKT/e1glIrxeqeSZ8HWjJiaCXErACsafxs=</signValue></Inv></Invoices>
                //"ERR:24" chứng thư truyền lên không đúng với chứng thư công ty đăng ký trên hệ thống
                result += result4;
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.AdjustReplaceInvWithToken " + result);
            return result;
        }

        /// <summary>
        /// Thay thế điều chỉnh (Token).
        /// </summary>
        /// <param name="xmlInvData"></param>
        /// <param name="serialCert"></param>
        /// <param name="type">thay thế = 1, điều chỉnh tăng = 2, điều chỉnh giảm = 3, điều chỉnh thông tin = 4</param>
        /// <param name="invToken">patternt;serial;sốhóađơn 01GTKT2/001;AA/13E;10</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “;” + Fkey + “_” + Số hóa đơn + ”,”</returns>
        public static string AdjustReplaceInvWithToken_Dll(string xmlInvData, string serialCert, int type, string invToken)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.AdjustReplaceInvWithToken(account, accountpassword, xmlInvData, username, password,
                    serialCert, type, invToken, _pattern, _seri, link_Publish);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.AdjustReplaceInvWithToken " + result);
            return result;
        }

        /// <summary>
        /// Hủy hóa đơn (Token).
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns>Thành công: trả về "OK"</returns>
        public static string CancelInvoiceWithToken_Dll(string xmlData)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.CancelInvoiceWithToken(account, accountpassword, xmlData, username, password, _pattern, linkWS: link_Publish);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.CancelInvoiceWithToken " + result);
            return result;
        }

        /// <summary>
        /// Lấy lại hash (Token)
        /// </summary>
        /// <param name="xmlFkeyInv"></param>
        /// <returns></returns>
        public static string GetHashInv_Dll(string xmlFkeyInv)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.getHashInv(account, accountpassword, username, password, _SERIAL_CERT, xmlFkeyInv, _pattern, link_Publish);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.getHashInv " + result);
            return result;
        }

        /// <summary>
        /// Lấy trạng thái hóa đơn (Token).
        /// </summary>
        /// <param name="xmlFkeyInv"><para>chuỗi xml Fkey hóa đơn cần lấy trạng thái.</para></param>
        //// <para> (cấu trúc: <Invoices><Inv><key>123</key></Inv><Inv><key>456</key></Inv><Inv><key>789</key></Inv></Invoices> ) (123, 456, 789 là Fkey)</para>
        /// <returns>
        /// <para>Trả về: xml string Cấu trúc: Invoices Inv key123 key Status 0 Status Inv ... Inv...</para>
        /// <para>0: hóa đơn mới tạo, chưa phát hành (những hóa đơn cần lấy lại hash)</para>
        /// <para>1: hóa đơn đã phát hành</para>
        /// <para>2: hóa đơn đã được kê khai thuế cũng như đưa vào các phần mêm kế toán</para>
        /// <para>3: hóa đơn bị thay thế</para>
        /// <para>4: hóa đơn bị điều chỉnh</para>
        /// <para>5: hóa đơn hủy</para>
        /// </returns>
        public static string GetStatusInv_Dll(string xmlFkeyInv)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.getStatusInv(account, accountpassword, username, password, xmlFkeyInv, _pattern, link_Publish);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.GetStatusInv " + result);
            return result;
        }

        /// <summary>
        /// Insert thông tin chứng thư vào hệ thống.
        /// </summary>
        /// <returns>Thành công: trả về "OK"</returns>
        public static string ImportCertWithToken_Dll()//, string linkWS)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.ImportCertWithToken(account, accountpassword, username, password, _SERIAL_CERT, link_Publish);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.ImportCertWithToken " + result);
            return result;
        }

        /// <summary>
        /// Phát hành khi đã lấy lại hash (Token).
        /// </summary>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + ”,”</returns>
        public static string PublishInv_Dll(string xmlHash)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.PublishInv(account, accountpassword, xmlHash, username, password, _SERIAL_CERT, _pattern, _seri, link_Publish);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.PublishInv " + result);
            return result;
        }

        /// <summary>
        /// Đẩy lên và phát hành hóa đơn có ký chữ ký số (Token).
        /// </summary>
        /// <param name="xmlInvData">chuỗi xml hóa đơn.</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + “,”</returns>
        public static string PublishInvWithToken_Dll(string xmlInvData)//, string pattern, string serial)//, string linkWS0)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.PublishInvWithToken(account, accountpassword, xmlInvData, username, password, _SERIAL_CERT, _pattern, _seri, link_Publish);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.PublishInvWithToken " + result);
            return result;
        }

        private static string GetResultDescription_Dll(string result)
        {
            string description = null;
            if (result.StartsWith("OK"))
            {

            }
            else if (result.StartsWith("ERR:-3"))
            {
                description = "\r\nCó lỗi trong quá trình lấy chứng thư.";
            }
            else if (result.StartsWith("ERR:-2"))
            {
                description = "\r\nChứng thư không có privatekey.";
            }
            else if (result.StartsWith("ERR:-1"))
            {
                description = "\r\nẤn nút hủy khi nhập mã pin của chứng thư.";
            }
            else if (result.StartsWith("ERR:30"))
            {
                description = "\r\nTạo mới lô hóa đơn lỗi (fkey trùng,…).";
            }
            else if (result.StartsWith("ERR:28"))
            {
                description = "\r\nThông tin chứng thư chưa có trong hệ thống.";
            }
            else if (result.StartsWith("ERR:27"))
            {
                description = "\r\nChứng thư chưa đến thời điểm sử dụng.";
            }
            else if (result.StartsWith("ERR:26"))
            {
                description = "\r\nChứng thư đã hết hạn.";
            }
            else if (result.StartsWith("ERR:24"))
            {
                description = "\r\nChứng thư truyền lên không đúng với chứng thư công ty đăng ký trên hệ thống";
            }
            else if (result.StartsWith("ERR:23"))
            {
                description = "\r\nChứng thư truyền lên không đúng định dạng.";
            }
            else if (result.StartsWith("ERR:22"))
            {
                description = "\r\nCông ty chưa đăng ký thông tin keystore.";
            }
            else if (result.StartsWith("ERR:21"))
            {
                description = "\r\nKhông tìm thấy công ty trên hệ thống.";
            }
            else if (result.StartsWith("ERR:20"))
            {
                description = "\r\nTham số mẫu số và ký hiệu truyền vào không hợp lệ.";
            }
            else if (result.StartsWith("ERR:19"))
            {
                description = "\r\npattern truyền vào không giống với pattern của hoá đơn cần điều chỉnh/thay thế.";
            }
            else if (result.StartsWith("ERR:10"))
            {
                description = "\r\nSố lượng hóa đơn truyền vào lớn hơn maxBlockInv.";
            }
            else if (result.StartsWith("ERR:9"))
            {
                description = "\r\n???.";
            }
            else if (result.StartsWith("ERR:8"))
            {
                description = "\r\nHoá đơn đã được điều chỉnh, thay thế.";
            }
            else if (result.StartsWith("ERR:7"))
            {
                description = "\r\nKhông tìm thấy chứng thư trong máy. Hãy cắm token.";
            }
            else if (result.StartsWith("ERR:6"))
            {
                description = "\r\nKhông còn đủ số hóa đơn cho lô phát hành.";
            }
            else if (result.StartsWith("ERR:5"))
            {
                description = "\r\nCó lỗi xảy ra.";
            }
            else if (result.StartsWith("ERR:4"))
            {
                description = "\r\ntoken hóa đơn sai định dạng.";
            }
            else if (result.StartsWith("ERR:3"))
            {
                description = "\r\nĐịnh dạng file xml hóa đơn không đúng.";
            }
            else if (result.StartsWith("ERR:2"))
            {
                description = "\r\nKhông tồn tại hoá đơn cần thay thế/điều chỉnh.";
            }
            else if (result.StartsWith("ERR:1"))
            {
                description = "\r\nKhông có quyền truy cập webservice.";
            }
            else if (result.StartsWith("ERR:0"))
            {
                description = "\r\nLỗi Fkey đã tồn tại.";
            }
            else
            {
                description = "???";
            }

            return description;
        }

        #endregion ========= TOKEN ==============

        /// <summary>
        /// Tải lên bảng kê.
        /// </summary>
        /// <param name="fkey"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string UploadInvAttachmentFkey(string fkey, string file)
        {
            string result = null;
            try
            {
                string attachment64 = FileToBase64(file);
                string ext = Path.GetExtension(file);
                if (ext.Length > 0) ext = ext.Substring(1);
                string attachmentName = Path.GetFileNameWithoutExtension(file);
                result = new AttachmentService(link_Attachment).uploadInvAttachmentFkey(fkey, username, password, attachment64, ext, attachmentName);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:11"))
                {
                    result += "\r\nDung lượng file vượt quá mức cho phép.";
                }
                else if (result.StartsWith("ERR:10"))
                {
                    result += "\r\nChuỗi Base64 cùa file không hợp lệ.";
                }
                else if (result.StartsWith("ERR:9"))
                {
                    result += "\r\nĐịnh dạng file không hợp lệ.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    result += "\r\nTên file không hợp lệ hoặc quá dài.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn.";
                }
                else if (result.StartsWith("ERR:4"))
                {
                    result += "\r\nCompany chưa có mẫu hóa đơn nào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
                else if (result.StartsWith("ERR"))
                {
                    result += "\r\nLỗi.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.UploadInvAttachmentFkey " + result);
            return result;
        }

        public static string FileToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string fileBase64 = Convert.ToBase64String(fileBytes);
            return fileBase64;
        }

        /// <summary>
        /// Tạo file từ chuỗi base64.
        /// </summary>
        /// <param name="base64string"></param>
        /// <param name="fileName"></param>
        public static void MakeFile(string base64string, string fileName)
        {
            File.WriteAllBytes(fileName, Convert.FromBase64String(base64string));
        }


        /// <summary>
        /// Đọc tên máy in từ file oldPrinter
        /// </summary>
        /// <returns></returns>
        public static string ReadOldSelectPrinter()
        {
            string s = "";
            if (File.Exists("oldPrinter"))
            {

                FileStream fs = new FileStream("oldPrinter", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                try
                {
                    s = sr.ReadLine();
                    sr.Close(); fs.Close();
                }
                catch (Exception)
                {
                    try
                    {
                        sr.Close();
                    }
                    catch
                    {
                        // ignored
                    }
                    try
                    {
                        fs.Close();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            else
            {
                s = "";
            }
            return s;
        }
        /// <summary>
        /// Ghi tên máy in xuống file oldPrinter
        /// </summary>
        /// <param name="name"></param>
        public static void WriteOldSelectPrinter(string name)
        {
            FileStream fs = new FileStream("oldPrinter", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(name);
                sw.Close(); fs.Close();
            }
            catch (Exception)
            {
                try
                {
                    sw.Close();
                }
                catch
                {
                    // ignored
                }
                try
                {
                    fs.Close();
                }
                catch
                {
                    // ignored
                }
            }
        }


        public static string adjustInv(string xml, string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(link_Business).adjustInv(account, accountpassword, xml, username, password, fkey_old, 0);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:9"))
                {
                    result += "\r\nTrạng thái hóa đơn không được điều chỉnh.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    result += "\r\nHóa đơn cần điều chỉnh đã bị thay thế. Không thể điều chỉnh được nữa.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nDải hóa đơn cũ đã hết.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    result += "\r\nKhông phát hành được hóa đơn.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    result += "\r\nDữ liệu xml đầu vào không đúng quy định.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    result += "\r\nHóa đơn cần điều chỉnh không tôn tại.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.adjustInv " + result);
            return result;
        }

        public static string replaceInv(string xml, string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(link_Business).replaceInv(account, accountpassword, xml, username, password, fkey_old, 0);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:9"))
                {
                    result += "\r\nTrạng thái hóa đơn không được thay thế.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    result += "\r\nHóa đơn đã được thay thế rồi. Không thể thay thế nữa.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nDải hóa đơn cũ đã hết.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    result += "\r\nCó lỗi trong quá trình thay thế hóa đơn.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    result += "\r\nDữ liệu xml đầu vào không đúng quy định.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    result += "\r\nKhông tồn tại hóa đơn cần thay thế.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.replaceInv " + result);
            return result;
        }

        /// <summary>
        /// Gạch nợ hóa đơn theo fkey
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <returns></returns>
        public static string confirmPaymentFkey(string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(link_Business).confirmPaymentFkey(fkey_old, username, password);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nKhông gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.confirmPaymentFkey " + result);
            return result;
        }

        /// <summary>
        /// Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <returns></returns>
        public static string confirmPayment(string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(link_Business).confirmPayment(fkey_old, username, password);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nKhông gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.confirmPayment " + result);
            return result;
        }

        public static string UnconfirmPaymentFkey(string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(link_Business).UnConfirmPaymentFkey(fkey_old, username, password);

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:13"))
                {
                    result += "\r\nHóa đơn đã được bỏ gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "\r\nKhông bỏ gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.UnconfirmPaymentFkey " + result);
            return result;
        }

        /// <summary>
        /// Hủy hóa đơn.
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <returns></returns>
        public static string cancelInv(string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(link_Business).cancelInv(account, accountpassword, fkey_old, username, password);

                if (result.StartsWith("OK"))
                {

                } else if (result.StartsWith("ERR:9"))
                {
                    result += "\r\nTrạng thái hóa đơn không được thay thế.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    result += "\r\nHóa đơn đã được thay thế rồi, hủy rồi.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    result += "\r\nKhông tồn tại hóa đơn cần hủy.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.cancelInv " + result);
            return result;
        }

        /// <summary>
        /// Thực hiện cập nhập thông tin khách hàng.
        /// </summary>
        /// <param name="dbf"></param>
        /// <param name="type">
        /// <para>1: thực hiện lặp qua từng dòng, mỗi lần gửi thông tin 1 customer.</para>
        /// <para>2: gửi 1 lần tất cả customers.</para>
        /// </param>
        /// <returns></returns>
        public static string DoUpdateCus(string dbf, string type = "1")
        {
            string result = "";
            string error = "";
            int error_count = 0, success_count = 0;
            //MessageBox.Show("Test Debug");
            try
            {
                var data = ReadDbf(dbf);
                if (type == "1")
                {
                    Customer cus = null;
                    string ma_kh = null;
                    int count = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        count++;
                        try
                        {
                            cus = null;
                            ma_kh = null;
                            Customers cuss = new Customers();
                            cus = ReadCusDataXml(row);
                            ma_kh = row["MA_KH"].ToString().Trim();
                            cuss.Customer_List.Add(cus);
                            string xml = XmlConverter.ClassToXml(cuss);

                            Logger.WriteToLog(string.Format("Preparing UpdateCus {0} {1}\r\n{2}", count, ma_kh, xml));
                            var num = UpdateCus(xml);
                            if (num == "1")
                            {
                                success_count++;
                                Logger.WriteToLog(string.Format("UpdateCus {0} {1} Success.", count, ma_kh));
                            }
                            else error_count ++;

                            result += string.Format("\r\n Update {0} status: {1}", ma_kh, num);
                        }
                        catch (Exception ex)
                        {
                            error_count++;
                            if (!string.IsNullOrEmpty(ma_kh) && cus != null)
                            {
                                error += "\nCustomer " + count + " " + ma_kh;
                            }
                            else
                            {
                                error += "\nCustomer " + count + " null OR no code.";
                            }
                            error += "\n" + ex.Message;
                        }
                    }
                }
                else
                {
                    try
                    {
                        Customers cuss = new Customers();
                        foreach (DataRow row in data.Rows)
                        {
                            Customer cus = ReadCusDataXml(row);
                            cuss.Customer_List.Add(cus);
                        }
                        string xml = XmlConverter.ClassToXml(cuss);

                        Logger.WriteToLog("Preparing UpdateCus:\r\n" + xml);
                        var num = UpdateCus(xml);
                        success_count = Convert.ToInt32(num);
                        result += "Success " + num + "/" + data.Rows.Count;
                    }
                    catch (Exception ex)
                    {
                        error += ex.Message;
                        error_count = 1;
                    }
                }
                
            }
            catch (Exception ex)
            {
                error += "\n" + ex.Message;
            }
            
            if (error.Length > 0) result += "ERR: " + error;
            Logger.WriteToLog("Program.DoUpdateCus " + result);
            //return result;
            return string.Format("Success {0}   Error {1}\n{2}", success_count, error_count, result);
        }

        public static string UpdateCus(string xml)
        {
            int result = 0;
            string message = "";
            try
            {
                result = new PublishService(link_Publish).UpdateCus(xml, username, password, 0);
                message += result;

                if (result == -5)
                {
                    message = "ERR:" + message + "\r\nCó khách hàng đã tồn tại.";
                }
                else if (result == -3)
                {
                    message = "ERR:" + message + "\r\nDữ liệu xml đầu vào không đúng quy định.";
                }
                else if (result == -2)
                {
                    message = "ERR:" + message + "\r\nKhông import được khách hàng vào database.";
                }
                else if (result == -1)
                {
                    message = "ERR:" + message + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                message = "ERR:EX\r\n" + ex.Message;
                Logger.WriteToLog("Program.UpdateCus " + message);
            }
            
            return message;
        }

        //private static Dictionary<string, string> sellerInfo;
        //private static Invoices postObject;
        /// <summary>
        /// Cấu hình các trường AM trên hóa đơn.
        /// </summary>
        private static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        private static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        private static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        private static Dictionary<string, ConfigLine> paymentsConfig = null;
        private static Dictionary<string, ConfigLine> itemInfoConfig = null;
        private static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        private static Dictionary<string, ConfigLine> taxBreakdownsConfig = null;
        private static Dictionary<string, ConfigLine> customerInfoConfig = null;
        
        public static void ReadXmlInfo(string xmlFile)
        {
            //postObject = new PostObject();
            
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();
            customerInfoConfig = new Dictionary<string, ConfigLine>();
            XmlTextReader reader = new XmlTextReader(xmlFile.ToLower());
            try
            {
                while (reader.Read())
                {
                    switch (reader.Name)
                    {
                        case "V6Info":
                        {
                            ConfigLine line = ReadXmlLine(reader);
                            switch (line.Field.ToLower())
                            {
                                case "username":
                                    username = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "password":
                                    password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "account":
                                    account = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "accountpassword":
                                    accountpassword = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "serialcert":
                                    _SERIAL_CERT = UtilityHelper.DeCrypt(line.Value).ToUpper();
                                    break;
                                case "token_password_title":
                                    token_password_title = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "token_password":
                                    token_password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "pattern":
                                    partten_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "seri":
                                    seri_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "link_publish":
                                    link_Publish = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "link_portal":
                                    link_Portal = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "link_business":
                                    link_Business = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "link_attachment":
                                    link_Attachment = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "v6fkey":
                                    fkey0 = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "v6fkeyexcel":
                                    fkeyexcel0 = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                            }
                            break;
                        }
                        case "ExcelConfig":
                        {
                            ConfigLine line = ReadXmlLine(reader);
                            if (line.Type == "2")
                            {
                                parameters_config.Add(line);
                            }
                            else if (line.Field == "TEMPLATE")
                            {
                                template_xls = line.Value;
                            }
                            else if (line.Field == "FIRSTCELL")
                            {
                                firstCell = line.Value;
                            }
                            else if (line.Field == "INSERTROW")
                            {
                                insertRow = ObjectAndString.ObjectToBool(line.Value);
                            }
                            else if (line.Field == "DRAWLINE")
                            {
                                drawLine = ObjectAndString.ObjectToBool(line.Value);
                            }
                            else if (line.Field == "COLUMNS")
                            {
                                columns = ObjectAndString.SplitString(line.Value);
                            }
                            else if (line.Field.StartsWith("COLUMNS"))
                            {
                                column_config[line.Field] = line.Value;
                            }
                            
                            break;
                        }
                        case "GeneralInvoiceInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                generalInvoiceInfoConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "BuyerInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                buyerInfoConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "SellerInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                sellerInfoConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "Payments":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                paymentsConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "ItemInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                itemInfoConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "SummarizeInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                summarizeInfoConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "TaxBreakdowns":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                taxBreakdownsConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "CustomerInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                customerInfoConfig.Add(key, ReadXmlLine(reader));
                            }
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
                }
                reader.Close();
                
            }
            catch (Exception ex)
            {
                reader.Close();
                MessageBox.Show("Read xml error: " + ex.Message);
            }
        }

        private static ConfigLine ReadXmlLine(XmlTextReader reader)
        {
            ConfigLine config = new ConfigLine();
            config.Field = reader.GetAttribute("Field");
            config.Value = reader.GetAttribute("Value");
            config.FieldV6 = reader.GetAttribute("FieldV6");
            config.Type = reader.GetAttribute("Type");
            config.DataType = reader.GetAttribute("DataType");
            return config;
        }

        private static void AutoInputTokenPassword()
        {
            try
            {
                //Find input password windows.
                Spy001 spy = new Spy001();
                var thisProcessID = Process.GetCurrentProcess().Id;
                SpyWindowHandle input_password_window = spy.FindWindow(token_password_title, thisProcessID);

                while (input_password_window == null)
                {
                    input_password_window = spy.FindWindow(token_password_title, thisProcessID);
                }
                //Find input password textbox, ok button
                //SpyWindowHandle input_handle = null;
                //SpyWindowHandle chk_soft_handle = null;
                //SpyWindowHandle ok_button_handle = null;
                //SpyWindowHandle soft_keyboard = null;

                //foreach (KeyValuePair<string, SpyWindowHandle> child_item in input_password_window.Childs)
                //{
                //    if (child_item.Value.Class.ClassName == "Edit")//Kích hoạt bàn phím ảo
                //    {
                //        input_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text == "Đăng nhập")
                //    {
                //        ok_button_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text.StartsWith("Kích hoạt"))
                //    {
                //        chk_soft_handle = child_item.Value;
                //    }
                //    else if (child_item.Value.Text == "soft keyboard")
                //    {
                //        soft_keyboard = child_item.Value;
                //    }
                //}

                //Input password
                {
                    input_password_window.SetForegroundWindow();
                    //if (input_handle != null) input_handle.SetFocus();
                    foreach (char c in token_password)
                    {
                        spy.SendKeyPress(c);
                    }
                    spy.SendKeyPressEnter();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Program.AutoInputTokenPassword " + ex.Message);
            }
        }

        public static void StartAutoInputTokenPassword()
        {
            StopAutoInputTokenPassword();
            if (string.IsNullOrEmpty(token_password)) return;
            if (string.IsNullOrEmpty(token_password_title)) return;
            autoToken = new Thread(AutoInputTokenPassword);
            //autoToken.IsBackground = true;
            autoToken.Start();
        }

        private static Thread autoToken = null;
        public static void StopAutoInputTokenPassword()
        {
            if(autoToken != null && autoToken.IsAlive) autoToken.Abort();
        }
    }
}

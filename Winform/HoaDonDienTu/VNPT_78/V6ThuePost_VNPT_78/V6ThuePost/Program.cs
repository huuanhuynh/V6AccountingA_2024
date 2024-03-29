﻿using System;
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
using Spy;
using Spy.SpyObjects;
using V6ThuePost.ResponseObjects;
using V6ThuePost.VnptObjects;
using V6ThuePostXmlApi;
using V6ThuePostXmlApi.AttachmentService;
using V6ThuePostXmlApi.BusinessService;
using V6ThuePostXmlApi.PortalService;
using V6ThuePostXmlApi.PublishService;
using V6Tools;
using V6Tools.V6Convert;
using ParseDBF = V6Tools.ParseDBF;

namespace V6ThuePost
{
    static class Program
    {
        public static bool _TEST_ = true;
        private static DateTime _TEST_DATE_ = DateTime.Now;
        public static Vnpt78WS _vnptWS = null;
        public static bool _write_log = false;
        #region ===== VAR =====
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
        public static string _baseUrl = "";
        public static string link_Publish = "";
        public static string link_Portal = "";
        public static string link_Business = "";
        public static string link_Attachment = "";
        public static string mst = "";
        /// <summary>
        /// services
        /// <para>Được gán khi đọc info</para>
        /// </summary>
        public static string _username { get; set; }
        public static string _password;
        /// <summary>
        /// admin
        /// </summary>
        public static string _account;

        public static string _accountpassword;
        /// <summary>
        /// serial của chứng thư công ty đã đăng ký trong hệ thống.
        /// </summary>
        public static string SERIAL_CERT;

        public static string pattern;
        private static string pattern_field;
        private static string pattern_test;
        public static string seri;
        private static string seri_field;
        private static string seri_test;

        private static string convert = "0";
        //Auto input setting
        private static string token_password_title = "";
        //private static string token_password_title = "Untitle"; // Test notepad
        private static string token_password = "";

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
        /// key trong data, Fkey của Invoice.
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
                MessageBox.Show("Test: Tham số key được tạo tự động, pattern được lấy từ xml pattern_test, seri_test.");
            }
            else
            {
                _TEST_ = false;
            }

            V6Return v6return = null;

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

                    _vnptWS = new Vnpt78WS(_baseUrl, _account, _accountpassword, _username, _password);
                    

                    if (String.Equals(mode, "ImportCert", StringComparison.CurrentCultureIgnoreCase))
                    {
                        result = _vnptWS.ImportCertWithToken_Dll(SERIAL_CERT);
                    }
                    else if (String.Equals(mode, "DownloadInvPDFFkey", StringComparison.CurrentCultureIgnoreCase))
                    {
                        MessageBox.Show("Test DownloadInvPDFFkey");
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);
                        result = _vnptWS.DownloadInvPDFFkey(fkeyA, 0, startupPath, out v6return);
                    }
                    else if (mode.ToLower() == "DownloadInvPDFFkeyNoPay".ToLower())
                    {
                        MessageBox.Show("Test DownloadInvPDFFkeyNoPay");
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);
                        string fileName = _vnptWS.DownloadInvPDFFkeyNoPay(fkeyA, Application.StartupPath, out v6return);
                        PdfiumViewerForm pdfView = new PdfiumViewerForm(fileName, "PDF");
                        pdfView.ShowDialog();
                        result += fileName;
                    }
                    else if (mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                    {
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);

                        string invXml = _vnptWS.DownloadInvFkeyNoPay(fkeyA, out v6return);
                        string so_hoa_don = GetSoHoaDon(invXml);
                        WriteFlag(flagFileName4, so_hoa_don);
                        result += so_hoa_don;
                    }
                    else if (mode.StartsWith("DOWNLOAD"))
                    {
                        var xml = _vnptWS.DownloadInvPDFFkey(fkeyA, 0, startupPath, out v6return);
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

                        if (mode == "M0")
                        {
                            string resultM = _vnptWS.ImportInvByPattern(xml, pattern, seri, out v6return);
                            result = resultM;
                            if (result.StartsWith("ERR:13")) // nếu đã tồn tại. thì xóa nháp + gửi lại.
                            {
                                resultM = _vnptWS.DeleteInvoiceByFkey(fkeyA, out v6return);
                                if (resultM.StartsWith("OK:"))
                                {
                                    resultM = _vnptWS.ImportInvByPattern(xml, pattern, seri, out v6return);
                                    result = resultM;
                                }
                            }
                            if (resultM.StartsWith("OK"))
                            {                             
                                WriteFlag(flagFileName4, "");
                            }
                        }
                        else if (!string.IsNullOrEmpty(SERIAL_CERT))
                        {
                            #region =================  KÝ SỐ =================
                            Program.StartAutoInputTokenPassword();
                            string resultM = _vnptWS.PublishInvWithToken32_Dll(xml, pattern, seri, SERIAL_CERT, out v6return);
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
                                resultM = _vnptWS.PublishInvWithToken32_Dll(xml, pattern, seri, SERIAL_CERT, out v6return);
                                result = resultM;
                                if (resultM.StartsWith("OK"))
                                {
                                    string so_hoa_don = GetSoHoaDon_Dll(resultM);
                                    WriteFlag(flagFileName4, so_hoa_don);
                                }
                                else // Đã chạy 2 lần vẫn không được.
                                {
                                    
                                }
                            }

                            // Gửi file.
                            if (arg3.Length > 0 && result.StartsWith("OK"))
                            {
                                if (mode.EndsWith("1")) //Gửi file excel có sẵn
                                {
                                    if (File.Exists(arg3))
                                    {
                                        result += _vnptWS.UploadInvAttachmentFkey(fkeyA, arg3);
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
                                        result += _vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
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
                                        result += _vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
                                    }
                                }
                            }
                            #endregion end ký số
                        }
                        else if (string.IsNullOrEmpty(SERIAL_CERT))
                        {
                            #region ==== mode M M1:tạo mới gửi excel có sẵn  M2:tạo mới+xuất excel rồi gửi.

                            if (mode == "M9") // phát hành hóa đơn nháp
                            {
                                result = _vnptWS.PublishInvFkey(fkeyA, pattern, seri, out v6return);
                            }
                            else
                            {
                                result = _vnptWS.ImportAndPublishInv(xml, pattern, seri, out v6return);
                                if (result.StartsWith("ERR:13")) // nếu đã tồn tại, xóa phát hành lại
                                {
                                    var resultM = _vnptWS.DeleteInvoiceByFkey(fkeyA, out v6return);
                                    if (resultM.StartsWith("OK:"))
                                    {
                                        result = _vnptWS.ImportAndPublishInv(xml, pattern, seri, out v6return);
                                    }
                                }
                            }                            
                            
                            //"OK:01GTKT0/001;VT/19E-V6XNCT_05A0283806HDA"
                            if (v6return.SO_HD != null && v6return.SO_HD.Length > 0)
                            {
                                WriteFlag(flagFileName4, v6return.SO_HD);
                            }
                            

                            if (arg3.Length > 0 && result.StartsWith("OK"))
                            {
                                if (mode.EndsWith("1")) //Gửi file excel có sẵn
                                {
                                    if (File.Exists(arg3))
                                    {
                                        result += _vnptWS.UploadInvAttachmentFkey(fkeyA, arg3);
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
                                        result += _vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
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
                                        result += _vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
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
                                    result += _vnptWS.UploadInvAttachmentFkey(fkeyA, arg4);
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
                            var invToken = string.Format("{0};{1};{2}", pattern, seri, so_hoa_don);
                            result = _vnptWS.AdjustReplaceInvWithToken68_Dll(xml, SERIAL_CERT, type, invToken, pattern, seri);
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
                            result = _vnptWS.UploadInvAttachmentFkey(fkey, file);
                        }
                        else if (mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                        {
                            ReadDataXml(arg2);
                            string fkey = fkeyA;
                            MakeFlagNames(fkey);
                            result = _vnptWS.UploadInvAttachmentFkey(fkey, fkey + ".xls");
                        }
                        else if (mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                        {
                            string export_file;
                            ReadDataXml(arg2);
                            bool export_ok = ExportExcel(arg3, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += _vnptWS.UploadInvAttachmentFkey(fkeyA, export_file);
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
                        result = DoUpdateCus(arg2, type, out v6return);

                        //v6return = new V6Return();
                        //v6return.RESULT_STRING = result;
                        //if (result.StartsWith("ERR:"))
                        //{
                        //    v6return.RESULT_ERROR_MESSAGE = result;
                        //}
                    }
                    else if (mode.StartsWith("P")) // P0 xml fkey
                    {
                        MessageBox.Show("Test mode:" + mode);
                        fkeyA = arg2;
                        MakeFlagNames(fkeyA);
                        string fileName = _vnptWS.DownloadInvPDFFkeyNoPay(fkeyA, startupPath, out v6return);
                        

                        if (!string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE)) goto EndP;
                        PdfiumViewerForm pdfView = new PdfiumViewerForm(fileName, "PDF");

                        if (mode.EndsWith("0")) // P...0 // In luôn ra máy in mặc định.
                        {
                            pdfView.PrintToDefaultPrinter();
                        }
                        else if (mode.EndsWith("1"))
                        {
                            pdfView.AutoClickPrint = true;
                            pdfView.ShowDialog();
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
                        result += fileName ?? v6return.RESULT_STRING;
                    }


                }
                catch (Exception ex)
                {
                    StopAutoInputTokenPassword();
                    File.Create(flagFileName3).Close();
                    result += "ERR:EX\r\n" + ex.Message;
                }

                if (string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE))
                {
                    WriteFlag(flagFileName2, "OK:" + v6return.RESULT_MESSAGE);
                }
                else
                {
                    WriteFlag(flagFileName3, v6return.RESULT_ERROR_MESSAGE);
                }

                File.Create(flagFileName9).Close(); // kết thúc
                if (result.StartsWith("ERR") && v6return != null)
                {
                    BaseMessage.Show(v6return.RESULT_MESSAGE + "\n" + result + "\n" + v6return.RESULT_ERROR_MESSAGE, 500);
                }
                else
                {
                    BaseMessage.Show(result, 500);
                }                
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            Process.GetCurrentProcess().Kill();
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
                    Logger.WriteToLog("TestFkey " + _TEST_DATE_ + ":" + fkeyA);
                }
                else
                {
                    fkeyA = fkey0 + row0["STT_REC"];
                }

                //{Tuanmh V6_05_2222222222222222_3333333333333
                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                //}

                inv.key = fkeyA;
                
                if (_TEST_)
                {
                    pattern = pattern_test;
                    seri = seri_test;
                }
                else
                {
                    pattern = row0[pattern_field].ToString().Trim();
                    seri = row0[seri_field].ToString().Trim();
                }
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

                if (_write_log)
                {
                    string stt_rec = row0["STT_REC"].ToString();
                    string file = Path.Combine(Application.StartupPath, stt_rec + ".json");
                    File.WriteAllText(file, result);
                }
            }
            //catch (Exception ex)
            {
                //
            }

            string fileName = Path.Combine(Application.StartupPath, "LastReadData.xml");
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
                if (_TEST_)
                {
                    pattern = pattern_test;
                    seri = seri_test;
                }
                else
                {
                    pattern = row0[pattern_field].ToString().Trim();
                    seri = row0[seri_field].ToString().Trim();
                }
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
                //inv.key = fkeyA;
                inv.Invoice["key"] = fkeyA;
                if (_TEST_)
                {
                    pattern = pattern_test;
                    seri = seri_test;
                }
                else
                {
                    pattern = row0[pattern_field].ToString().Trim();
                    seri = row0[seri_field].ToString().Trim();
                }
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
                        return FixReadMoney(row, config.Format, MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND"));
                    case "N2CE":
                        return FixReadMoney(row, config.Format, MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", "VND"));
                    case "N2CMANT":
                        return FixReadMoney(row, config.Format, MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", row["MA_NT"].ToString().Trim()));
                    case "N2CMANTE":
                        return FixReadMoney(row, config.Format, MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim()));
                    case "N2C0VNDE":
                        {
                            string ma_nt = row["MA_NT"].ToString().Trim().ToUpper();
                            if (ma_nt != "VND")
                            {
                                return FixReadMoney(row, config.Format, MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "E", row["MA_NT"].ToString().Trim()));
                            }
                            else
                            {
                                return "";
                            }
                        }
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

        private static string FixReadMoney(DataRow row, string ReadFormatField, string old_read)
        {
            string result = old_read;
            string format = null;
            if (row.Table.Columns.Contains(ReadFormatField)) format = (row[ReadFormatField] + "").Trim();
            if (!string.IsNullOrEmpty(format) && format.Contains("{0}")) result = string.Format(format, old_read);
            return result;
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


        ///// <summary>
        ///// Phát hành hóa đơn.
        ///// </summary>
        ///// <param name="xml">Dữ liệu các hóa đơn.</param>
        ///// <returns>Thông báo phát hành hd.</returns>
        //public static string ImportAndPublishInv(string xml)
        //{
        //    string result = null;
        //    try
        //    {
        //        var publishService = new PublishService(link_Publish);
        //        //seri = "LX/20E";
        //        result = publishService.ImportAndPublishInv(account, accountpassword, xml, username, password, pattern, seri, convert == "1" ? 1 : 0);
        //        //result = publishService.ImportInv(xml, username, password, convert == "1" ? 1 : 0);
        //        if (result.StartsWith("OK"))
        //        {

        //        }
        //        else if (result.StartsWith("ERR:20"))
        //        {
        //            result += "\r\nPattern và serial không phù hợp, hoặc không tồn tại hóa đơn đã đăng kí có sử dụng Pattern và serial truyền vào.";
        //        }
        //        else if (result.StartsWith("ERR:13"))
        //        {
        //            result += "\r\nHóa đơn đã được gạch nợ.";
        //        }
        //        else if (result.StartsWith("ERR:10"))
        //        {
        //            result += "\r\nLô có số hóa đơn vượt quá max cho phép.";
        //        }
        //        else if (result.StartsWith("ERR:7"))
        //        {
        //            result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
        //        }
        //        else if (result.StartsWith("ERR:6"))
        //        {
        //            result += "\r\nKhông đủ số hóa đơn cho lô phát hành.";
        //        }
        //        else if (result.StartsWith("ERR:5"))
        //        {
        //            result += "\r\nKhông phát hành được hóa đơn.";
        //        }
        //        else if (result.StartsWith("ERR:3"))
        //        {
        //            result += "\r\nDữ liệu xml đầu vào không đúng quy định.\nKhông có hóa đơn nào được phát hành.";
        //        }
        //        else if (result.StartsWith("ERR:1"))
        //        {
        //            result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "ERR:EX\r\n" + ex.Message;
        //    }

        //    Logger.WriteToLog("Program.ImportAndPublishInv " + result);
        //    return result;
        //}

        //public static string DownloadInvPDFFkey(string fkey)
        //{
        //    string result = null;
        //    try
        //    {
        //        result = new PortalService(link_Portal).downloadInvPDFFkey(fkey, username, password);

        //        if (result.StartsWith("OK"))
        //        {

        //        }
        //        else if (result.StartsWith("ERR:11"))
        //        {
        //            result += "\r\nHóa đơn chưa thanh toán nên không xem được.";
        //        }
        //        else if (result.StartsWith("ERR:7"))
        //        {
        //            result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
        //        }
        //        else if (result.StartsWith("ERR:6"))
        //        {
        //            result += "\r\nKhông tìm thấy hóa đơn.";
        //        }
        //        else if (result.StartsWith("ERR:1"))
        //        {
        //            result += "\r\nTài khoản đăng nhập sai.";
        //        }
        //        else if (result.StartsWith("ERR"))
        //        {
        //            result += "\r\nCó lỗi.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "ERR:EX\r\n" + ex.Message;
        //    }

        //    Logger.WriteToLog("Program.DownloadInvFkeyNoPay " + result);
        //    return result;
        //}
        
        //public static string DownloadInvPDFFkeyNoPay(string fkey)
        //{
        //    string result = null;
        //    try
        //    {
        //        result = new PortalService(link_Portal).downloadInvPDFFkeyNoPay(fkey, username, password);

        //        if (result.StartsWith("OK"))
        //        {

        //        }
        //        else if (result.StartsWith("ERR:11"))
        //        {
        //            result += "\r\nHóa đơn chưa thanh toán nên không xem được.";
        //        }
        //        else if (result.StartsWith("ERR:7"))
        //        {
        //            result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
        //        }
        //        else if (result.StartsWith("ERR:6"))
        //        {
        //            result += "\r\nKhông tìm thấy hóa đơn.";
        //        }
        //        else if (result.StartsWith("ERR:1"))
        //        {
        //            result += "\r\nTài khoản đăng nhập sai.";
        //        }
        //        else if (result.StartsWith("ERR"))
        //        {
        //            result += "\r\nCó lỗi.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "ERR:EX\r\n" + ex.Message;
        //    }

        //    Logger.WriteToLog("Program.DownloadInvFkeyNoPay " + result);
        //    return result;
        //}

        //public static string DownloadInvFkeyNoPay(string fkey)
        //{
        //    string result = null;
        //    try
        //    {
        //        result = new PortalService(link_Portal).downloadInvFkeyNoPay(fkey, username, password);

        //        if (result.StartsWith("OK"))
        //        {

        //        }
        //        else if (result.StartsWith("ERR:7"))
        //        {
        //            result += "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
        //        }
        //        else if (result.StartsWith("ERR:1"))
        //        {
        //            result += "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
        //        }
        //        else if (result.StartsWith("ERR"))
        //        {
        //            result += "\r\nCó lỗi.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "ERR:EX\r\n" + ex.Message;
        //    }

        //    Logger.WriteToLog("Program.DownloadInvFkeyNoPay " + result);
        //    return result;
        //}

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
                getHashXml = publishService.getHashInvWithToken(_account, _accountpassword, xmlInvData, _username, _password,
                    SERIAL_CERT, 0, invToken, pattern, seri, 0);

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
                    getHashXml = publishService.getHashInvWithToken(_account, _accountpassword, xmlInvData, _username, _password, SERIAL_CERT, 0, invToken, pattern, seri, 0);// GetHashInv()
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
                                          + "<SerialCert>"+SERIAL_CERT.ToUpper()+"</SerialCert>"
                                          + "<Inv>"
                                          + "<key>"+getFkey+"</key>"                            // fkey
                                          + "<idInv>"+getIdInv+"</idInv>"                         // id hóa đơn trên hệ thống vnpt
                                          + "<signValue>" + V6XmlConverter.FixXmlValueChar(digitalSign) + "</signValue>"
                                          + "</Inv>"
                                          + "</Invoices>";
                
                var result4 = publishService.publishInvWithToken(_account, _accountpassword, xmlInvDataSigned, _username, _password, pattern, seri);
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

        

        

        

        


        #endregion ========= TOKEN ==============
        
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
                result = new BusinessService(link_Business).adjustInv(_account, _accountpassword, xml, _username, _password, fkey_old, 0);

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
                result = new BusinessService(link_Business).replaceInv(_account, _accountpassword, xml, _username, _password, fkey_old, 0);

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
                result = new BusinessService(link_Business).confirmPaymentFkey(fkey_old, _username, _password);

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
                result = new BusinessService(link_Business).confirmPayment(fkey_old, _username, _password);

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
                result = new BusinessService(link_Business).UnConfirmPaymentFkey(fkey_old, _username, _password);

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
                result = new BusinessService(link_Business).cancelInv(_account, _accountpassword, fkey_old, _username, _password);

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
        public static string DoUpdateCus(string dbf, string type, out V6Return v6return)
        {
            string result = "";
            string error = "";
            int error_count = 0, success_count = 0;
            v6return = null;
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
                            string xml = V6XmlConverter.ClassToXml(cuss);

                            Logger.WriteToLog(string.Format("Preparing UpdateCus {0} {1}\r\n{2}", count, ma_kh, xml));
                            var num = UpdateCus(xml, out v6return);
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
                        string xml = V6XmlConverter.ClassToXml(cuss);

                        Logger.WriteToLog("Preparing UpdateCus:\r\n" + xml);
                        var num = UpdateCus(xml, out v6return);
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
            if (v6return == null)
            {
                v6return = new V6Return();
                v6return.RESULT_OBJECT = error;
                v6return.RESULT_STRING = error;
                v6return.RESULT_ERROR_MESSAGE = error;
            }
            return string.Format("Success {0}   Error {1}\n{2}", success_count, error_count, result);
        }

        public static string UpdateCus(string xml, out V6Return v6return)
        {
            int result = 0;
            string message = "";
            v6return = new V6Return();
            try
            {
                result = new PublishService(link_Publish).UpdateCus(xml, _username, _password, 0);
                v6return.RESULT_OBJECT = result;
                v6return.RESULT_STRING = "" + result;
                message += result;

                if (result == -5)
                {
                    message = "ERR:" + message + "\r\nCó khách hàng đã tồn tại.";
                    v6return.RESULT_ERROR_MESSAGE = message;
                }
                else if (result == -3)
                {
                    message = "ERR:" + message + "\r\nDữ liệu xml đầu vào không đúng quy định.";
                    v6return.RESULT_ERROR_MESSAGE = message;
                }
                else if (result == -2)
                {
                    message = "ERR:" + message + "\r\nKhông import được khách hàng vào database.";
                    v6return.RESULT_ERROR_MESSAGE = message;
                }
                else if (result == -1)
                {
                    message = "ERR:" + message + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                    v6return.RESULT_ERROR_MESSAGE = message;
                }
                else if (result < 0)
                {
                    message = "ERR:" + message + "\r\nLỗi chưa xác định.";
                    v6return.RESULT_ERROR_MESSAGE = message;
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
                            ConfigLine line = V6ThuePostUtility.ReadXmlLine(reader);
                            switch (line.Field.ToLower())
                            {
                                case "username":
                                    _username = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "password":
                                    _password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "account":
                                    _account = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "accountpassword":
                                    _accountpassword = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "serialcert":
                                    SERIAL_CERT = UtilityHelper.DeCrypt(line.Value).ToUpper();
                                    break;
                                case "token_password_title":
                                    token_password_title = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "token_password":
                                    token_password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "pattern":
                                    pattern_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "seri":
                                    seri_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "pattern_test":
                                    pattern_test = line.Value;
                                    break;
                                case "seri_test":
                                    seri_test = line.Value;
                                    break;
                                case "baselink":
                                case "baseurl":
                                    _baseUrl = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "link_publish":
                                    link_Publish = UtilityHelper.DeCrypt(line.Value);
                                    _baseUrl = link_Publish.Substring(0, link_Publish.LastIndexOf('/'));
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
                                case "writelog":
                                    _write_log = ObjectAndString.ObjectToBool(line.Value);
                                    break;
                            }
                            break;
                        }
                        case "ExcelConfig":
                        {
                            ConfigLine line = V6ThuePostUtility.ReadXmlLine(reader);
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
                                generalInvoiceInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "BuyerInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                buyerInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "SellerInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                sellerInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "Payments":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                paymentsConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "ItemInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                itemInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "SummarizeInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                summarizeInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "TaxBreakdowns":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                taxBreakdownsConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
                            }
                            break;
                        }
                        case "CustomerInfo":
                        {
                            string key = reader.GetAttribute("Field");
                            if (!string.IsNullOrEmpty(key))
                            {
                                customerInfoConfig.Add(key, V6ThuePostUtility.ReadXmlLine(reader));
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


        //private static void AutoInputTokenPassword()
        //{
        //}

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

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using V6ThuePostApi;
using V6ThuePostApi.EinvoiceService;
using V6ThuePostApi.PostObjects;
using V6ThuePostXmlApi;
using V6Tools;
using V6Tools.V6Convert;
using HangHoaEntity = V6ThuePostApi.PostObjects.HangHoaEntity;
using ParseDBF = V6Tools.ParseDBF;

namespace V6ThuePost
{
    static class Program
    {
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
        public static string link_Base = "";
        public static string link_Publish = "";
        public static string link_Portal = "";
        public static string link_Business = "";
        public static string link_Attachment = "";
        public static string methodUrl = "";
        public static string mst = "";
        /// <summary>
        /// <para>Được gán khi đọc info</para>
        /// </summary>
        public static string _username { get; set; }
        public static string _password;
        private static string account;
        private static string accountpassword;
        private static string partten, partten_field;
        private static string seri, seri_field;
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
        /// key đầu ngữ
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
                    
                    //MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                    if (mode.StartsWith("M") || mode == "")
                    {
                        var xml = ReadDataXml(arg2);
                        File.Create(flagFileName1).Close();
                        result = XuatHoaDonDienTu_XML(xml);
                        
                        if (arg3.Length>0 && result.StartsWith("OK"))
                        {
                            if (mode.EndsWith("1"))//Gửi file excel có sẵn
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
                                string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0Data["T_TT"]), "V", "VND");
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
                    else if (mode == "S")
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
                    else if (mode == "T")
                    {
                        var xml = ReadDataXmlT(arg2);
                        File.Create(flagFileName1).Close();
                        result = replaceInv(xml, arg3);
                    }
                    else if (mode.StartsWith("G"))
                    {
                        if (mode == "G1") // Gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                            confirmPaymentFkey(arg2);
                        }
                        else if (mode == "G2") // Gạch nợ
                        {
                            File.Create(flagFileName1).Close();
                            confirmPayment(arg2);
                        }
                        else if (mode == "G3") // Hủy gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                            UnconfirmPaymentFkey(arg2);
                        }
                    }
                    else if (mode == "H")
                    {
                        File.Create(flagFileName1).Close();
                        result = cancelInv(fkey_old: arg2);
                    }
                    else if (mode.StartsWith("U"))//U1,U2
                    {
                        if (mode == "U")        // upload file có sẵn, fkey truyền vào
                        {
                            string fkey = arg2;
                            string file = arg3;
                            UploadInvAttachmentFkey(fkey, file);
                        }
                        else if (mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                        {
                            ReadDataXml(arg2);
                            string fkey = fkeyA;
                            UploadInvAttachmentFkey(fkey, fkey + ".xls");
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
                //XuatHoaDonDienTu_XML postObject0 = new XuatHoaDonDienTu_XML();
                hoaDonEntity hoa_don_entity = new hoaDonEntity();
                //postObject0.hoaDonEntity = hoa_don_entity;
                //ReadXmlInfo(xmlFile);
                DataTable data = ReadDbf(dbfFile);
                
                DataRow row0 = data.Rows[0];
                fkeyA = fkey0 + row0["STT_REC"];

                //{Tuanmh V6_05_2222222222222222_3333333333333
                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                //}

                //postObject.key = fkeyA;
                partten = row0[partten_field].ToString().Trim();
                seri = row0[seri_field].ToString().Trim();
                //flagName = fkeyA;
                MakeFlagNames(fkeyA);
                

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    hoa_don_entity.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    hoa_don_entity.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    hoa_don_entity.Invoice[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }
                
                
                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    hoa_don_entity.Invoice[item.Key] = GetValue(row0, item.Value);
                }


                var products = new Products();
                foreach (DataRow row in data.Rows)
                {
                    //if (row["STT"].ToString() == "0") continue;

                    HangHoaEntity product = new HangHoaEntity();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        product.Details[item.Key] = GetValue(row, item.Value);
                    }

                    products.Add(product);
                }
                hoa_don_entity.Invoice["HangHoas"] = products;
                
                
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    hoa_don_entity.Invoice[item.Key] = GetValue(row0, item.Value);
                }
                
                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    hoa_don_entity.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = XmlConverter.ClassToXml(hoa_don_entity);
            }
            //catch (Exception ex)
            {
                //
            }
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
                fkeyA = fkey0 + row0["STT_REC"];
                inv.key = fkeyA;
                //partten = row0[partten_field].ToString().Trim();
                //seri = row0[seri_field].ToString().Trim();
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

                var products = new List<HangHoaEntity>();
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    HangHoaEntity product = new HangHoaEntity();
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

                result = XmlConverter.ClassToXml(inv);
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
                fkeyA = fkey0 + row0["STT_REC"];
                inv.key = fkeyA;
                //partten = row0[partten_field].ToString().Trim();
                //seri = row0[seri_field].ToString().Trim();
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

                var products = new List<HangHoaEntity>();
                foreach (DataRow row in data.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;

                    HangHoaEntity product = new HangHoaEntity();
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

                result = XmlConverter.ClassToXml(inv);
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
            //if (string.IsNullOrEmpty(config.Type))
            //{
            //    return fieldValue;
            //}

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

            if (configTYPE == "FIELD"
                && !string.IsNullOrEmpty(config.FieldV6)
                && row.Table.Columns.Contains(config.FieldV6))
            {
                fieldValue = row[config.FieldV6];
                if (row.Table.Columns[config.FieldV6].DataType == typeof(string))
                {
                    //Trim
                    fieldValue = fieldValue.ToString().Trim();
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
                            return fieldValue.ToString() == "1" ||
                                   fieldValue.ToString().ToLower() == "true" ||
                                   fieldValue.ToString().ToLower() == "yes";
                        }
                    case "DATETIME":
                        return ObjectAndString.ObjectToFullDateTime(fieldValue);
                    case "N2C":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                    case "DECIMAL": case "MONEY":
                        return ObjectAndString.ObjectToDecimal(fieldValue);
                    case "INT":
                        return ObjectAndString.ObjectToInt(fieldValue);
                    case "INT64":
                    case "LONG":
                        return ObjectAndString.ObjectToInt64(fieldValue);
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
        /// <param name="xmlHoaDonEntity">Dữ liệu các hóa đơn.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public static string XuatHoaDonDienTu_XML(string xmlHoaDonEntity)
        {
            string result = null;
            try
            {
                //var publishService = new EinvoiceService();// PublishService(link_Publish);
                //publishService.AuthenticationValue = new Authentication()
                //{
                //    userName = _username,
                //    password = _password
                //};

                string requestText = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"
                    + "<soap:Header><Authentication xmlns=\"http://thaison.vn/inv\">"
                    + "<userName>"+_username+"</userName><password>"+_password+"</password></Authentication></soap:Header>"
                    + "<soap:Body><XuatHoaDonDienTu_XML xmlns=\"http://thaison.vn/inv\">"
                    //+ "<hoaDonEntity><dhoadonid>0</dhoadonid><NgayNhapVien>0001-01-01T00:00:00</NgayNhapVien><NgayTaoHoaDon>0001-01-01T00:00:00</NgayTaoHoaDon><NgayXuatHoaDon>0001-01-01T00:00:00</NgayXuatHoaDon><IsSendMail xsi:nil=\"true\" /><TienThueVat>0</TienThueVat><TongTienHang>0</TongTienHang><TongTienThanhToan>0</TongTienThanhToan><TienChietKhau>0</TienChietKhau><TyGia>0</TyGia><TamUng>0</TamUng><NamHoc>0</NamHoc><IsSign xsi:nil=\"true\" /><DATECLOSEDBILL>0001-01-01T00:00:00</DATECLOSEDBILL><TrangThaiDieuChinh>0</TrangThaiDieuChinh><LoaiBangKe>0</LoaiBangKe><isSuDungBangKe>false</isSuDungBangKe><NgayDieuChinh xsi:nil=\"true\" /><InvoiceDueDate xsi:nil=\"true\" /><TransDate>0001-01-01T00:00:00</TransDate><KieuHoaDon>0</KieuHoaDon><PaymentTerm xsi:nil=\"true\" /><NoVatAmount xsi:nil=\"true\" /><TTamtNoTax>0</TTamtNoTax><TTvatNoTax>0</TTvatNoTax><TTnetNoTax>0</TTnetNoTax><TTamt0Tax>0</TTamt0Tax><TTvat0Tax>0</TTvat0Tax><TTnet0Tax>0</TTnet0Tax><TTamt5Tax>0</TTamt5Tax><TTvat5Tax>0</TTvat5Tax><TTnet5Tax>0</TTnet5Tax><TTamt10Tax>0</TTamt10Tax><TTvat10Tax>0</TTvat10Tax><TTnet10Tax>0</TTnet10Tax><OrigPurDate>0001-01-01T00:00:00</OrigPurDate><IsGiuLai>false</IsGiuLai></hoaDonEntity>"
                    + xmlHoaDonEntity
                    + "</XuatHoaDonDienTu_XML></soap:Body></soap:Envelope>";

                V6Http http = new V6Http(link_Base, null, null);
                var response = http.POST_XML(link_Publish, requestText);

                string MaEInvoice = GetXmlTagValue(response, "MaEinvoice");
                string Dhoadonid = GetXmlTagValue(response, "Dhoadonid");
                if (MaEInvoice != null)
                {
                    WriteFlag(flagFileName4, MaEInvoice);
                }
                string code = GetXmlTagValue(response, "Code");
                string message = GetXmlTagValue(response, "Message");
                string description = GetXmlTagValue(response, "Description");
                result += code + "\r\n";
                result += message + "\r\n";
                result += description + "\r\n";

                result += response;

                //var hoaDon = new HoaDonEntity();

                //var response = publishService.XuatHoaDonDienTu_XML(hoaDon);
                //var response = publishService.XuatHoaDonDienTu_XML2(xml);
                //publishService.XuatHoaDonDienTu(hoaDon);
                //result += response.MsgError;
                //result += response.SoHoaDon;

                if (result.StartsWith("ERR:20"))
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

            Logger.WriteToLog("Program.XuatHoaDonDienTu_XML " + result);
            return result;
        }

        private static string GetXmlTagValue(string xml, string tagName)
        {
            string tagOpen = string.Format("<{0}>", tagName);
            string tagClose = string.Format("</{0}>", tagName);
            int startIndex = xml.IndexOf(tagOpen, StringComparison.InvariantCultureIgnoreCase);
            if (startIndex < 0) return null;
            startIndex += tagOpen.Length;
            int endIndex = xml.IndexOf(tagClose, StringComparison.InvariantCultureIgnoreCase);// +tagClose.Length;
            int length = endIndex - startIndex;
            string value = xml.Substring(startIndex, length);
            return value;
        }

        public static string DownloadInvFkeyNoPay(string fkey)
        {
            string result = null;
            try
            {
                //result = new PortalService(link_Portal).downloadInvFkeyNoPay(fkey, _username, _password);

                if (result.StartsWith("ERR:7"))
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
                //result = new AttachmentService(link_Attachment).uploadInvAttachmentFkey(fkey, _username, _password, attachment64, ext, attachmentName);

                if (result.StartsWith("ERR:11"))
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

        public static string adjustInv(string xml, string fkey_old)
        {
            string result = null;
            try
            {
                //result = new BusinessService(link_Business).adjustInv(account, accountpassword, xml, _username, _password, fkey_old, 0);

                if (result.StartsWith("ERR:9"))
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
                //result = new BusinessService(link_Business).replaceInv(account, accountpassword, xml, _username, _password, fkey_old, 0);

                if (result.StartsWith("ERR:9"))
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
                //result = new BusinessService(link_Business).confirmPaymentFkey(fkey_old, _username, _password);

                if (result.StartsWith("ERR:13"))
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
                //result = new BusinessService(link_Business).confirmPayment(fkey_old, _username, _password);

                if (result.StartsWith("ERR:13"))
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
                //result = new BusinessService(link_Business).UnConfirmPaymentFkey(fkey_old, _username, _password);

                if (result.StartsWith("ERR:13"))
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
                //result = new BusinessService(link_Business).cancelInv(account, accountpassword, fkey_old, _username, _password);

                if (result.StartsWith("ERR:9"))
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
                    foreach (DataRow row in data.Rows)
                    {
                        try
                        {
                            cus = null;
                            ma_kh = null;
                            Customers cuss = new Customers();
                            cus = ReadCusDataXml(row);
                            ma_kh = row["MA_KH"].ToString().Trim();
                            cuss.Customer_List.Add(cus);
                            string xml = XmlConverter.ClassToXml(cuss);
                            var num = UpdateCus(xml);
                            if (num == "1") success_count++;
                            else error_count ++;

                            result += string.Format("\r\n Update {0} status: {1}", ma_kh, num);
                        }
                        catch (Exception ex)
                        {
                            error_count++;
                            if (!string.IsNullOrEmpty(ma_kh) && cus != null)
                            {
                                error += "\nCustomer" + ma_kh;
                            }
                            else
                            {
                                error += "\nCustomer null OR no code.";
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
                //result = new PublishService(link_Publish).UpdateCus(xml, _username, _password, 0);
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
                                    _username = line.Value;
                                    break;
                                case "password":
                                    _password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "account":
                                    account = line.Value;
                                    break;
                                case "accountpassword":
                                    accountpassword = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "partten":
                                    partten_field = line.Value;
                                    break;
                                case "seri":
                                    seri_field = line.Value;
                                    break;
                                case "link_base":
                                    link_Base = UtilityHelper.DeCrypt(line.Value);
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
                                    fkey0 = line.Value;
                                    break;
                                case "v6fkeyexcel":
                                    fkeyexcel0 = line.Value;
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

    }
}

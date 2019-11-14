using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;
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

        public static string mst = "";
        /// <summary>
        /// <para>Được gán khi đọc info</para>
        /// </summary>
        public static string username { get; set; }
        public static string password;
        private static string account;
        private static string accountpassword;
        private static string id, id_field = "ID_MASTER";
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

        public static IDictionary<string, object> _amData = null;
        public static List<IDictionary<string, object>> _adList = null;
        #endregion var

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //MessageBox.Show("Debug!");

            SqlConnect.StartSqlConnect("V6Soft", Application.StartupPath);
            DatabaseConfig.SelectedIndex = 0;
            
            if (args != null && args.Length > 0)
            {
                bool insertOK = false;
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

                    if (mode.ToUpper() == "MTEST")
                    {
                        
                    }
                    else if (mode.StartsWith("M") || mode == "")
                    {
                        #region ==== mode M M1:tạo mới gửi excel có sẵn  M2:tạo mới+xuất excel rồi gửi.
                        var xml = ReadDataXml(arg2);
                        File.Create(flagFileName1).Close();
                        insertOK = InsertInvoice(_amData, _adList);
                        if (insertOK)
                        {
                            result = "OK";
                        }

                        WriteFlag(flagFileName4, "0");

                        if (arg3.Length > 0 && insertOK)
                        {
                            if (mode.EndsWith("1")) //Gửi file excel có sẵn
                            {
                                if (File.Exists(arg3))
                                {
                                    //result += UploadInvAttachmentFkey(fkeyA, arg3);
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
                                    //result += UploadInvAttachmentFkey(fkeyA, export_file);
                                }
                            }
                            else if (mode.EndsWith("3")) // Tự xuất pdf rồi gửi
                            {
                                result = "ERR:No pdf";
                                //string export_file = null;
                                //if (string.IsNullOrEmpty(exportName))
                                //{
                                //    var save = new SaveFileDialog
                                //    {
                                //        Filter = "Pdf files (*.pdf)|*.pdf",
                                //        Title = "Xuất pdf để gửi đi.",
                                //    };
                                //    if (save.ShowDialog() == DialogResult.OK)
                                //    {
                                //        export_file = save.FileName;
                                //    }
                                //    else
                                //    {
                                //        export_file = null;
                                //        return;
                                //    }
                                //}
                                //else
                                //{
                                //    export_file = exportName + ".pdf";
                                //}
                                //string dbfDataFile = arg3;
                                //string rptFile = arg4;
                                //ReportDocument rpt = new ReportDocument();
                                //rpt.Load(rptFile);
                                //DataSet ds = new DataSet();
                                //DataTable data1 = ReadDbf(dbfDataFile);
                                //data1.TableName = "DataTable1";
                                //DataTable data2 = data1.Clone();
                                //data2.TableName = "DataTable2";
                                //var row0Data = data1.Rows[0].ToDataDictionary();
                                //data2.AddRow(row0Data);
                                //ds.Tables.Add(data1);
                                //ds.Tables.Add(data2);
                                //string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0Data["T_TT"]),
                                //    "V", "VND");
                                //rpt.SetDataSource(ds);
                                //rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);
                                //bool export_ok = ExportRptToPdf(null, rpt, export_file);
                                //if (export_ok)
                                //{
                                //    result += "\r\nExport ok.";
                                //}
                                //else
                                //{
                                //    result += "\r\nExport fail.";
                                //}

                                //if (export_ok && File.Exists(export_file))
                                //{
                                //    //result += UploadInvAttachmentFkey(fkeyA, export_file);
                                //}
                            }
                        }
                        else if(!insertOK) // chạy lại cho trường hợp đã tồn tại fkey
                        {
                            result = "ERR:!";
                        }
                        #endregion
                    }
                    else if (mode == "S")
                    {
                        var xml = ReadDataXml(arg2);
                        File.Create(flagFileName1).Close();
                        insertOK = UpdateInvoice(_amData, _adList);

                        WriteFlag(flagFileName4, "0");
                        if (insertOK)
                        {
                            result = "OK";
                        }
                        else
                        {
                            result = "ERR:!";
                        }
                    }
                    else if (mode == "T")
                    {
                        
                    }
                    else if (mode.StartsWith("G"))
                    {
                        MakeFlagNames(arg2);
                        if (mode == "G1") // Gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                        }
                        else if (mode == "G2") // Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
                        {
                            File.Create(flagFileName1).Close();
                        }
                        else if (mode == "G3") // Hủy gạch nợ theo fkey
                        {
                            File.Create(flagFileName1).Close();
                        }
                    }
                    else if (mode == "H")
                    {
                        MakeFlagNames(arg2);
                        File.Create(flagFileName1).Close();
                        
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
                            result = "ERR:no pdf";
                            //try
                            //{
                            //    string dbfDataFile = arg2;
                            //    string rptFile = arg3;
                            //    string saveFile = arg4;

                            //    ReportDocument rpt = new ReportDocument();
                            //    rpt.Load(rptFile);
                            //    DataSet ds = new DataSet();
                            //    DataTable data1 = ReadDbf(dbfDataFile);
                            //    data1.TableName = "DataTable1";
                            //    DataTable data2 = data1.Clone();
                            //    data2.TableName = "DataTable2";
                            //    var row0Data = data1.Rows[0].ToDataDictionary();
                            //    data2.AddRow(row0Data);
                            //    ds.Tables.Add(data1);
                            //    ds.Tables.Add(data2);
                            //    string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0Data["T_TT"]), "V", "VND");
                            //    rpt.SetDataSource(ds);
                            //    rpt.SetParameterValue("SoTienVietBangChu", tien_bang_chu);

                            //    bool export_ok = false;
                            //    if (string.IsNullOrEmpty(saveFile))
                            //    {
                            //        export_ok = ExportRptToPdf_As(null, rpt, saveFile);
                            //    }
                            //    else
                            //    {
                            //        export_ok = ExportRptToPdf(null, rpt, saveFile);
                            //    }

                            //    if (export_ok)
                            //    {
                            //        result += "\r\nE2 Export ok.";
                            //    }
                            //    else
                            //    {
                            //        result += "\r\nE2 Export fail.";
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    result = "ERR:E2EX: " + ex.Message + "\n" + result;
                            //    Logger.WriteExLog("E2", ex);
                            //}
                        }
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

        private static string GetSoHoaDon_xml(string invXml)
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
        /// Lấy số hóa đơn từ kết quả trả về của hàm ImportAndPublishInv
        /// </summary>
        /// <param name="pattern">pattern;serial1-key1_num1</param>
        /// <returns></returns>
        private static string GetSoHoaDon_pattern(string pattern)
        {
            string result = "";
            try
            {
                string startTerm = "_";
                //string endTerm = "</InvoiceNo>";
                int startIndex = pattern.LastIndexOf(startTerm, StringComparison.Ordinal);
                if (startIndex > 0)
                {
                    startIndex += startTerm.Length;
                    //int endIndex = pattern.IndexOf(endTerm, StringComparison.Ordinal);
                    if (pattern.Length > startIndex)
                    {
                        result = pattern.Substring(startIndex);
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
                        string content = config_line.Value;
                        if (config_line.Value.Contains("{") && config_line.Value.Contains("}"))
                        {
                            var regex = new Regex("{(.+?)}");
                            foreach (Match match in regex.Matches(config_line.Value))
                            {
                                var matchGroup0 = match.Groups[0].Value;
                                var matchContain = match.Groups[1].Value;
                                var matchColumn = matchContain;
                                var matchFormat = "";
                                if (matchContain.Contains(":"))
                                {
                                    int _2dotIndex = matchContain.IndexOf(":", StringComparison.InvariantCulture);
                                    matchColumn = matchContain.Substring(0, _2dotIndex);
                                    matchFormat = matchContain.Substring(_2dotIndex+1);
                                }
                                if (data1.Columns.Contains(matchColumn))
                                {
                                    if (data1.Columns[matchColumn].DataType == typeof(DateTime) && matchFormat == "")
                                    {
                                        matchFormat = "dd/MM/yyyy";
                                    }
                                    content = content.Replace(matchGroup0, ObjectAndString.ObjectToString(row0[matchColumn], matchFormat));
                                }
                                //var matchKey = match.Groups[1].Value;
                                //if (data1.Columns.Contains(matchKey))
                                //{
                                //    replaced_value = config_line.Value.Replace(match.Groups[0].Value,
                                //        ObjectAndString.ObjectToString(row0[matchKey]));
                                //}
                            }
                            if (parameters.ContainsKey(config_line.Field))
                            {
                                MessageBox.Show("Trùng khóa cấu hình excel: key=" + config_line.Field);
                                continue;
                            }
                            parameters.Add(config_line.Field, content);
                        }
                        else
                        {
                            if (data1.Columns.Contains(config_line.Value))
                            {
                                parameters.Add(config_line.Field, row0[content]);
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

        //public static bool ExportRptToPdf_As(IWin32Window owner, ReportDocument rpt, string defaultSaveName = "")
        //{

        //    if (rpt == null)
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        var save = new SaveFileDialog
        //        {
        //            Filter = "Pdf files (*.pdf)|*.pdf",
        //            Title = "Xuất PDF.",
        //            FileName = ChuyenMaTiengViet.ToUnSign(defaultSaveName)
        //        };
        //        if (save.ShowDialog(owner) == DialogResult.OK)
        //        {
        //            return ExportRptToPdf(owner, rpt, save.FileName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteExLog("Program.ExportRptToPdf_As", ex, "");
        //    }
        //    return false;
        //}

        //public static bool ExportRptToPdf(IWin32Window owner, ReportDocument rpt, string fileName)
        //{
        //    try
        //    {
        //        ExportOptions CrExportOptions;
        //        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
        //        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
        //        CrDiskFileDestinationOptions.DiskFileName = fileName;
        //        CrExportOptions = rpt.ExportOptions;

        //        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        //        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
        //        CrExportOptions.FormatOptions = CrFormatTypeOptions;

        //        rpt.Export();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteExLog("Program.ExportRptToPdf", ex, "");
        //    }
        //    return false;
        //}

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

        /// <summary>
        /// Đọc dữ liệu trả về amData và adList
        /// </summary>
        /// <param name="dbfFile"></param>
        /// <returns></returns>
        public static string ReadDataXml(string dbfFile)
        {
            string result = "";
            column_config = new SortedDictionary<string, string>();
            parameters_config = new List<ConfigLine>();
            try
            {
                _amData = new Dictionary<string, object>();
                _adList = new List<IDictionary<string, object>>();
                DataTable data = ReadDbf(dbfFile);


                DataRow row0 = data.Rows[0];
                fkeyA = fkey0 + row0["STT_REC"];
                _amData[id_field] = fkeyA;
                
                //partten = row0[partten_field].ToString().Trim();
                //seri = row0[seri_field].ToString().Trim();
                MakeFlagNames(fkeyA);

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    _amData[item.Key] = GetValue(row0, item.Value);
                    //postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    _amData[item.Key] = GetValue(row0, item.Value);
                    //postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    _amData[item.Key] = GetValue(row0, item.Value);
                    //postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }


                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    _amData[item.Key] = GetValue(row0, item.Value);
                }

                
                foreach (DataRow row in data.Rows)
                {
                    //if (row["STT"].ToString() == "0") continue;

                    var adLine = new Dictionary<string, object>();
                    adLine[id_field] = fkeyA;

                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        adLine[item.Key] = GetValue(row, item.Value);
                    }

                    _adList.Add(adLine);
                }
                
                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    _amData[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    _amData[item.Key] = GetValue(row0, item.Value);
                }

                result = XmlConverter.ObjectToXml(_amData);
                result += "\r\n\r\n";
                result += XmlConverter.ObjectToXml(_adList);

            }
            catch (Exception ex)
            {
                ;
            }
            return result;
        }


        private static void MakeFlagNames(string flagName)
        {
            flagFileName1 = flagName + ".flag1";
            flagFileName2 = flagName + ".flag2";
            flagFileName3 = flagName + ".flag3";
            flagFileName4 = flagName + ".flag4";
            flagFileName9 = flagName + ".flag9";
        }

        private static object GetValue(DataRow row, ConfigLine config)
        {
            object fieldValue = config.Value;
            DataTable table = row.Table;
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
                    var fields = ObjectAndString.SplitStringBy(config.FieldV6, '+');
                    fieldValue = null;
                    string fieldValueString = null;
                    decimal fieldValueNumber = 0m;
                    bool still_number = true;
                    foreach (string s in fields)
                    {
                        string field = s.Trim();
                        if (table.Columns.Contains(field))
                        {
                            fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                            if (still_number && ObjectAndString.IsNumberType(table.Columns[field].DataType))
                            {
                                fieldValueNumber += ObjectAndString.ObjectToDecimal(row[field]);
                            }
                            else
                            {
                                still_number = false;
                            }
                        }
                        else
                        {
                            if (still_number)
                            {                                
                                if (field.StartsWith("\"") && field.EndsWith("\""))
                                {
                                    fieldValueString += field.Substring(1, field.Length - 2);
                                }
                                else
                                {
                                    fieldValueString += field;
                                }

                                decimal tempNumber;
                                if (Decimal.TryParse(field, out tempNumber))
                                {
                                    fieldValueNumber += tempNumber;
                                }
                                else
                                {
                                    still_number = false;
                                }
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
                    }
                    // Chốt.
                    if (still_number) fieldValue = fieldValueNumber;
                    else fieldValue = fieldValueString;
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
                    case "DATE":
                    case "DATETIME":
                        return ObjectAndString.ObjectToDate(fieldValue, config.Format);
                        break;
                    case "N2C":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                    case "N2CMANT":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", row["MA_NT"].ToString().Trim());
                    case "DECIMAL":
                    case "MONEY":
                        return ObjectAndString.ObjectToDecimal(fieldValue);
                    case "INT":
                        return ObjectAndString.ObjectToInt(fieldValue);
                    case "INT64":
                    case "LONG":
                        return ObjectAndString.ObjectToInt64(fieldValue);
                    case "UPPER": // Chỉ dùng ở exe gọi bằng Foxpro.
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
                    return "cent";
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

        public static bool InsertInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList)
        {
            if(amData == null) throw new Exception("am null");
            if(adList == null) throw new Exception("ad null");

            bool insert_success = false;
            SqlTransaction TRANSACTION = null;
            string id_master = amData["ID_MASTER"].ToString().Trim();
            int j = 0;
            try
            {
                var AMStruct = V6SqlconnectHelper.GetTableStruct("TBL_MASTER");
                var ADStruct = V6SqlconnectHelper.GetTableStruct("TBL_DETAIL");

                var insert_am_sql = SqlGenerator.GenInsertSqlSimple(0, "TBL_MASTER", AMStruct, amData);
                TRANSACTION = SqlConnect.CreateSqlTransaction(AMStruct.TableName);

                //Delete AD TBL_DETAIL
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
                {
                    {"ID_MASTER", id_master}
                };
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);

                //Delete AM TBL_MASTER
                var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);

                insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;
                foreach (IDictionary<string, object> adRow in adList)
                {
                    var adSql = SqlGenerator.GenInsertSqlSimple(0, "TBL_DETAIL", ADStruct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    j += (execute > 0 ? 1 : 0);
                }

                if (insert_success && j == adList.Count)
                {
                    TRANSACTION.Commit();
                    return true;
                }
                else
                {
                    TRANSACTION.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (TRANSACTION != null) TRANSACTION.Rollback();
                Logger.WriteToLog(string.Format("InsertInvoice {0} Error {1}", id_master, ex.Message));
            }

            return false;
        }

        public static bool UpdateInvoice(IDictionary<string, object> amData, List<IDictionary<string, object>> adList)//, IDictionary<string, object> keys0)
        {
            if (amData == null) throw new Exception("am null");
            if (adList == null) throw new Exception("ad null");

            bool update_success = false;
            SqlTransaction TRANSACTION = null;
            string id_master = amData[id_field].ToString().Trim();
            int j = 0;
            try
            {
                var AMStruct = V6SqlconnectHelper.GetTableStruct("TBL_MASTER");
                var ADStruct = V6SqlconnectHelper.GetTableStruct("TBL_DETAIL");
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
                {
                    {id_field, id_master}
                };

                var update_am_sql = SqlGenerator.GenUpdateSqlSimple(0, "TBL_MASTER", AMStruct, amData, keys);
                TRANSACTION = SqlConnect.CreateSqlTransaction(AMStruct.TableName);

                //Delete AD TBL_DETAIL
                var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
                SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);

                update_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, update_am_sql) > 0;
                foreach (IDictionary<string, object> adRow in adList)
                {
                    var adSql = SqlGenerator.GenInsertSqlSimple(0, "TBL_DETAIL", ADStruct, adRow);
                    int execute = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql);
                    j += (execute > 0 ? 1 : 0);
                }

                if (update_success && j == adList.Count)
                {
                    TRANSACTION.Commit();
                    return true;
                }
                else
                {
                    TRANSACTION.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (TRANSACTION != null) TRANSACTION.Rollback();
                Logger.WriteToLog(string.Format("UpdateInvoice {0} Error {1}", id_master, ex.Message));
            }

            return false;
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
                                case "id_field":
                                    id_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "partten":
                                    partten_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "seri":
                                    seri_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
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

    }
}

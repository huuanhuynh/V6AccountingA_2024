using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BSECUS;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6ThuePostBkavApi;
using V6ThuePostBkavApi.PostObjects;
using V6ThuePostBkavApi.ResponseObjects;
using V6ThuePostViettelApi;
using V6ThuePostViettelApi.PostObjects;
using V6ThuePostViettelApi.ResponseObjects;
using V6ThuePostXmlApi;
using V6ThuePostXmlApi.AttachmentService;
using V6ThuePostXmlApi.PortalService;
using V6ThuePostXmlApi.PostObjects;
using V6ThuePostXmlApi.PublishService;
using V6ThuePostXmlApi.Web_References.BusinessService;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ThuePostManager
{
    /// <summary>
    /// Lớp quản lý POST GET
    /// </summary>
    public static class PostManager
    {
        static DataTable map_table;
        static DataTable ad_table;
        static DataTable am_table;
        static DataTable ad2_table;


        public static string _username = "";
        public static string _password = "";
        private static string baseUrl = "", createInvoiceUrl = "", modifyUrl = "";

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
        private static string account;
        private static string accountpassword;
        private static string partten, partten_field;
        private static string seri, seri_field;
        private static string convert = "0";


        private static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        private static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        private static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        private static Dictionary<string, ConfigLine> paymentsConfig = null;
        private static Dictionary<string, ConfigLine> itemInfoConfig = null;
        private static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        private static Dictionary<string, ConfigLine> taxBreakdownsConfig = null;

        //Excel config
        private static string template_xls = "template.xls";
        private static string firstCell = "A4";
        private static bool insertRow = false;
        private static bool drawLine = false;
        private static string[] columns = null;
        private static IDictionary<string, string> column_config = new SortedDictionary<string, string>();
        private static List<ConfigLine> parameters_config = new List<ConfigLine>();

        //private static RemoteCommand remoteCommand = null;
        public static string BkavPartnerGUID = "";
        public static string BkavPartnerToken = "";

        /// <summary>
        /// Hàm post chính, sẽ chuyển hướng theo string1.
        /// </summary>
        /// <param name="pmparams"></param>
        /// <param name="sohoadon">Số hóa đơn nhận về.</param>
        /// <param name="id">ID khác nhận về</param>
        /// <param name="error">Lỗi ghi nhận được.</param>
        /// <returns></returns>
        public static string PowerPost(PostManagerParams pmparams, out string sohoadon, out string id, out string error)
        {
            string result0 = null, sohoadon0 = null, id0 = null, error0 = null;
            try
            {
                map_table = pmparams.DataSet.Tables[0];
                ad_table = pmparams.DataSet.Tables[1];
                am_table = pmparams.DataSet.Tables[2];
                DataRow row0 = am_table.Rows[0];
                ad2_table = pmparams.DataSet.Tables[3];

                ReadConfigInfo(map_table);

                switch (pmparams.Branch)
                {
                    case "1":
                        result0 = EXECUTE_VIETTEL(pmparams.Mode, out sohoadon0, out id0, out error0);
                        break;
                    case "2":
                        result0 = EXECUTE_VNPT(pmparams, out sohoadon0, out id0, out error0);
                        break;
                    case "3":
                        result0 = EXECUTE_BKAV(pmparams.Mode, out sohoadon0, out id0, out error0);
                        break;
                    default:
                        error0 = V6Text.NotSupported + pmparams.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("RequestManager.PowerPost", ex);
            }
            
            sohoadon = sohoadon0;
            id = id0;
            error = error0;
            return result0;
        }

        #region ==== BKAV ====

        private static string EXECUTE_BKAV(string mode, out string sohoadon, out string id, out string error)
        {
            string result = "";
            sohoadon = null;
            id = null;
            error = null;
            try
            {
                ExecCommandFunc wsExecCommand = null;
                var webservice = new V6ThuePostBkavApi.vn.ehoadon.wsdemo.WSPublicEHoaDon();
                wsExecCommand = webservice.ExecuteCommand;
                uint Constants_Mode = RemoteCommand.DefaultMode;
                var remoteCommand = new RemoteCommand(wsExecCommand, BkavPartnerGUID, BkavPartnerToken, Constants_Mode);

                string jsonBody = null;

                if (mode == "M")
                {
                    jsonBody = ReadData();
                    //File.Create(flagFileName1).Close();
                    result = POST(remoteCommand, jsonBody, BkavConst._100_CreateNew);
                    string sGUID = null;
                    if (result.Contains("; ")) sGUID = result.Substring(result.IndexOf("; ", StringComparison.Ordinal) + 2);
                    if (!string.IsNullOrEmpty(sGUID))
                    {
                        id = sGUID;
                        //WriteFlag(flagFileName4, sGUID);
                    }
                }
                else if (mode == "S")
                {
                    jsonBody = ReadData();
                    //File.Create(flagFileName1).Close();
                    result = POST(remoteCommand, jsonBody, BkavConst._121_CreateAdjust);
                    string sGUID = null;
                    if (result.Contains("; ")) sGUID = result.Substring(result.IndexOf("; ", StringComparison.Ordinal) + 2);
                    if (!string.IsNullOrEmpty(sGUID))
                    {
                        id = sGUID;
                        //WriteFlag(flagFileName4, sGUID);
                    }
                }
                else if (mode.StartsWith("T"))
                {
                    jsonBody = ReadData();
                    //File.Create(flagFileName1).Close();
                    result = POST(remoteCommand, jsonBody, BkavConst._120_CreateReplace);
                }

                if (result.StartsWith("ERR"))
                {
                    error += result;
                    //File.Create(flagFileName3).Close();
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                error += ex.Message;
                //File.Create(flagFileName3).Close();
                result += "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }



        public static string ReadData()
        {
            string result = "";
            try
            {
                var postObject = new PostObjectBkav();
                
                DataRow row0 = am_table.Rows[0];
                fkeyA = "" + row0["FKEY_HD"];
                
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    postObject.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                //foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                //{
                //    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                //}

                //foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                //{
                //    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                //}

                //Dictionary<string, object> payment = new Dictionary<string, object>();
                //foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                //{
                //    payment[item.Key] = GetValue(row0, item.Value);
                //}
                //postObject.payments.Add(payment);//One payment only!

                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.ListInvoiceDetailsWS.Add(rowData);
                }

                //foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                //{
                //    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                //}
                if (summarizeInfoConfig.ContainsKey("ListInvoiceAttachFileWS"))
                {
                    postObject.ListInvoiceAttachFileWS = new List<string>()
                    {
                        GetValue(row0, summarizeInfoConfig["ListInvoiceAttachFileWS"]).ToString()
                    };
                }
                if (summarizeInfoConfig.ContainsKey("PartnerInvoiceID"))
                {
                    postObject.PartnerInvoiceID =
                        ObjectAndString.ObjectToInt64(GetValue(row0, summarizeInfoConfig["PartnerInvoiceID"]).ToString());
                }
                if (summarizeInfoConfig.ContainsKey("PartnerInvoiceStringID"))
                {
                    postObject.PartnerInvoiceStringID =
                        GetValue(row0, summarizeInfoConfig["PartnerInvoiceStringID"]).ToString();
                }

                //Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                //foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                //{
                //    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                //}
                //postObject.taxBreakdowns.Add(taxBreakdown);//One only!

                result = postObject.ToJson();
            }
            catch (Exception ex)
            {
                //
            }
            return "[" + result + "]";
        }

        public static string POST(RemoteCommand remoteCommand, string jsonBody, int comandType)
        {
            string result = null;
            try
            {
                string sGUID = null;
                string msg = "";
                switch (comandType)
                {
                    case BkavConst._100_CreateNew:
                        msg = DoCreateInvoice(remoteCommand, jsonBody, out sGUID);
                        break;
                    case BkavConst._121_CreateAdjust:
                        msg = DoAdjustInvoice(remoteCommand, jsonBody, out sGUID);
                        break;
                    case BkavConst._120_CreateReplace:
                        msg = DoReplaceInvoice(remoteCommand, jsonBody, out sGUID);
                        break;
                    default:
                        msg = "V6 not supported.";
                        break;
                }

                if (msg.Length > 0)
                {
                    result = "ERR: " + msg;
                }
                else
                {
                    result += "\r\nGUID: " + sGUID;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            Logger.WriteToLog("POST " + result);
            return result;
        }

        static string DoCreateInvoice(RemoteCommand remoteCommand, string listInvoice_json, out string sGUID)
        {
            string msg = "";
            sGUID = null;

            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._100_CreateNew, listInvoice_json, out result);
            if (msg.Length > 0) return msg;

            // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0) return msg;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                if (invoiceResult.Status == 0) sGUID = sGUID + "; " + invoiceResult.InvoiceGUID;
                else msg = msg + "; " + invoiceResult.MessLog;
            }

            return msg;
        }

        static string DoAdjustInvoice(RemoteCommand remoteCommand, string listInvoice_json, out string sGUID)
        {
            string msg = "";
            sGUID = null;

            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._121_CreateAdjust, listInvoice_json, out result);
            if (msg.Length > 0) return msg;

            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0) return msg;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                if (invoiceResult.Status == 0) sGUID = sGUID + "; " + invoiceResult.InvoiceGUID;
                else msg = msg + "; " + invoiceResult.MessLog;
            }

            return msg;
        }

        static string DoReplaceInvoice(RemoteCommand remoteCommand, string listInvoice_json, out string sGUID)
        {
            string msg = "";
            sGUID = null;

            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._120_CreateReplace, listInvoice_json, out result);
            if (msg.Length > 0) return msg;

            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0) return msg;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                if (invoiceResult.Status == 0) sGUID = sGUID + "; " + invoiceResult.InvoiceGUID;
                else msg = msg + "; " + invoiceResult.MessLog;
            }

            return msg;
        }


        #endregion bkav

        #region ==== VNPT ====

        private static string EXECUTE_VNPT(PostManagerParams pmp, out string sohoadon, out string id, out string error)
        {
            string result = "";
            sohoadon = null;
            id = null;
            error = null;
            try
            {
                var row0 = am_table.Rows[0];
                //MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                if (pmp.Mode.StartsWith("M") || pmp.Mode == "")
                {
                    var xml = ReadDataXml();
                    //File.Create(flagFileName1).Close();
                    result = ImportAndPublishInv(xml);

                    string invXml = DownloadInvFkeyNoPay(fkeyA);
                    sohoadon = GetSoHoaDon(invXml);
                    //WriteFlag(flagFileName4, so_hoa_don);
                    string filePath = Path.Combine(pmp.Dir, pmp.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (pmp.Mode.EndsWith("1"))//Gửi file excel có sẵn
                        {
                            if (File.Exists(filePath))
                            {
                                result += UploadInvAttachmentFkey(fkeyA, filePath);
                            }
                            else
                            {
                                result += "Không tồn tại " + filePath;
                            }
                        }
                        else if (pmp.Mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                        {
                            string export_file;
                            bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                        else if (pmp.Mode.EndsWith("3")) // Tự xuất pdf rồi gửi
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
                                    goto End;
                                }
                            }
                            else
                            {
                                export_file = exportName + ".pdf";
                            }
                            
                            string rptFile = Path.Combine(pmp.Dir, pmp.RptFileFull);
                            ReportDocument rpt = new ReportDocument();
                            rpt.Load(rptFile);
                            DataSet ds = new DataSet();
                            DataTable data1 = ad_table.Copy();
                            data1.TableName = "DataTable1";
                            DataTable data2 = am_table.Copy();
                            data2.TableName = "DataTable2";
                            ds.Tables.Add(data1);
                            ds.Tables.Add(data2);
                            string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
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
                else if (pmp.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = pmp.Fkey_hd;
                    
                    string invXml = DownloadInvFkeyNoPay(fkeyA);
                    string so_hoa_don = GetSoHoaDon(invXml);
                    //WriteFlag(flagFileName4, so_hoa_don);
                    result += so_hoa_don;
                    //result += invXml;
                }
                else if (pmp.Mode == "S" || pmp.Mode == "D")
                {
                    var xml = ReadDataXml();// ReadDataXmlS(dbfFile: arg2);
                    //File.Create(flagFileName1).Close();
                    result = adjustInv(xml, fkey_old: pmp.Fkey_hd);
                    string filePath = Path.Combine(pmp.Dir, pmp.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (File.Exists(filePath))
                        {
                            result += UploadInvAttachmentFkey(fkeyA, filePath);
                        }
                        else
                        {
                            result += "Không tồn tại " + filePath;
                        }
                    }
                }
                else if (pmp.Mode == "T")
                {
                    var xml = ReadDataXml();// ReadDataXmlT(arg2);
                    //File.Create(flagFileName1).Close();
                    result = replaceInv(xml, pmp.Fkey_hd);
                }
                else if (pmp.Mode.StartsWith("G"))
                {
                    if (pmp.Mode == "G1") // Gạch nợ theo fkey
                    {
                        //File.Create(flagFileName1).Close();
                        confirmPaymentFkey(pmp.Fkey_hd);
                    }
                    else if (pmp.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        //File.Create(flagFileName1).Close();
                        confirmPayment(pmp.Fkey_hd);
                    }
                    else if (pmp.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        //File.Create(flagFileName1).Close();
                        UnconfirmPaymentFkey(pmp.Fkey_hd);
                    }
                }
                else if (pmp.Mode == "H")
                {
                    //File.Create(flagFileName1).Close();
                    result = cancelInv(fkey_old: pmp.Fkey_hd);
                }
                else if (pmp.Mode == "D")
                {
                    //== S
                }
                else if (pmp.Mode.StartsWith("U"))//U1,U2
                {
                    if (pmp.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = pmp.Fkey_hd;
                        string file = Path.Combine(pmp.Dir, pmp.FileName);
                        UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (pmp.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = pmp.Fkey_hd;
                        UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (pmp.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (pmp.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                }
                else if (pmp.Mode.StartsWith("E"))
                {
                    if (pmp.Mode == "E")
                    {

                    }
                    else if (pmp.Mode == "E1")
                    {
                        string rptFile = pmp.RptFileFull;
                        //string saveFile = arg4;

                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (pmp.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = pmp.RptFileFull;
                        string saveFile = Path.Combine(pmp.Dir, pmp.FileName);// arg4;

                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(rptFile);
                        DataSet ds = new DataSet();
                        DataTable data1 = ad_table.Copy();
                        data1.TableName = "DataTable1";
                        DataTable data2 = am_table.Copy();
                        data2.TableName = "DataTable2";
                        ds.Tables.Add(data1);
                        ds.Tables.Add(data2);
                        string tien_bang_chu = MoneyToWords(ObjectAndString.ObjectToDecimal(row0["T_TT"]), "V", "VND");
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
                            result += "\r\nExport ok.";
                        }
                        else
                        {
                            result += "\r\nExport fail.";
                        }
                    }
                }



                if (result.StartsWith("ERR"))
                {
                    error += result;
                    //File.Create(flagFileName3).Close();
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                //File.Create(flagFileName3).Close();
                result += "ERR:EX\r\n" + ex.Message;
                error += result;
            }
            //File.Create(flagFileName9).Close();
            //BaseMessage.Show(result, 500);
            End:
            return result;
        }

        public static string ReadDataXml()
        {
            string result = "";
            //column_config = new SortedDictionary<string, string>();
            //parameters_config = new List<ConfigLine>();
            try
            {
                DataRow row0 = am_table.Rows[0];
                //PostObject obj = new PostObject();
                Invoices postObject = new Invoices();
                //ReadXmlInfo(xmlFile);
                //DataTable ad_table = ReadDbf(dbfFile);

                //Fill data to postObject
                //var invs = new List<Inv>();
                var inv = new Inv();
                //invs.Add(inv);
                postObject.Inv.Add(inv);

                //Sửa lại fkey dùng fkeyField
                fkeyA = row0["fkey_hd"].ToString().Trim();

                //{Tuanmh V6_05_2222222222222222_3333333333333
                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                //}

                inv.key = fkeyA;
                partten = row0[partten_field].ToString().Trim();
                seri = row0[seri_field].ToString().Trim();
                //flagName = fkeyA;
                //MakeFlagNames(fkeyA);


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


                var products = new Products();
                foreach (DataRow row in ad_table.Rows)
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


                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    inv.Invoice[item.Key] = GetValue(row0, item.Value);
                }

                result = XmlConverter.ClassToXml(postObject);
            }
            catch (Exception ex)
            {
                //
            }
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
                var publishService = new PublishService(baseUrl);
                result = publishService.ImportAndPublishInv(account, accountpassword, xml, _username, _password, partten, seri, convert == "1" ? 1 : 0);

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

            Logger.WriteToLog("Program.ImportAndPublishInv " + result);
            return result;
        }

        public static string DownloadInvFkeyNoPay(string fkey)
        {
            string result = null;
            try
            {
                //if (string.IsNullOrEmpty(baseUrl)) baseUrl = "https://www.google.com/";
                result = new PortalService().downloadInvFkeyNoPay(fkey, _username, _password);

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
                result = new AttachmentService().uploadInvAttachmentFkey(fkey, _username, _password, attachment64, ext, attachmentName);

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
                result = new BusinessService().adjustInv(account, accountpassword, xml, _username, _password, fkey_old, 0);

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
                result = new BusinessService().replaceInv(account, accountpassword, xml, _username, _password, fkey_old, 0);

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
                result = new BusinessService().confirmPaymentFkey(fkey_old, _username, _password);

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
                result = new BusinessService().confirmPayment(fkey_old, _username, _password);

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
                result = new BusinessService().UnConfirmPaymentFkey(fkey_old, _username, _password);

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
                result = new BusinessService().cancelInv(account, accountpassword, fkey_old, _username, _password);

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
        /// 
        /// </summary>
        /// <param name="data1">Bảng 1</param>
        /// <param name="data2">Bảng nhiều</param>
        /// <param name="exportFile"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool ExportExcel(DataTable data1, DataTable data2, out string exportFile, ref string result)
        {
            try
            {
                var row0 = data1.Rows[0];
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
                else
                {
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

        #endregion vnpt

        #region ==== VIETTEL ====

        private static string EXECUTE_VIETTEL(string mode, out string sohoadon, out string id, out string error)
        {
            string result = "";
            sohoadon = null;
            id = null;
            error = null;
            try
            {
                string jsonBody = "";
                var _V6Http = new V6Http(baseUrl, _username, _password);
                if (mode.StartsWith("M"))
                {
                    generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                    {
                        Field = "adjustmentType",
                        Value = "1",
                    };
                    Guid new_uid = Guid.NewGuid();
                    if (mode == "MG")
                    {
                        generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
                        {
                            Field = "transactionUuid",
                            Value = "" + new_uid,
                        };

                        //var fs = new FileStream(flagFileName5, FileMode.Create);
                        //StreamWriter sw = new StreamWriter(fs);
                        //sw.Write("" + new_uid);
                        //sw.Close();
                        //fs.Close();
                    }
                    jsonBody = ReadData_Viettel();
                    //File.Create(flagFileName1).Close();
                    result = POST_NEW(_V6Http, jsonBody);
                }
                else if (mode.StartsWith("S"))
                {
                    if (mode.EndsWith("3"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }
                    else if (mode.EndsWith("5"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "5",
                        };
                    }
                    else
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }

                    jsonBody = ReadData_Viettel();
                    //File.Create(flagFileName1).Close();
                    result = POST_EDIT(_V6Http, jsonBody);
                }

                //Phân tích result
                string message = "";
                try
                {
                    CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
                    if (!string.IsNullOrEmpty(responseObject.description))
                    {
                        message += " " + responseObject.description;
                    }

                    if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                    {
                        sohoadon = responseObject.result.invoiceNo;
                        message += " " + responseObject.result.invoiceNo;
                        //WriteFlag(flagFileName4, responseObject.result.invoiceNo);
                        //File.Create(flagFileName2).Close();
                    }
                    else
                    {
                        error += "Không có số hóa đơn.";
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteToLog("Program.Main ConverResultObjectException: " + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                V6ControlFormHelper.WriteExLog("RequestManager.EXECUTE_VIETTEL", ex);
            }

            return result;
        }

        public static string POST_NEW(V6Http _V6Http, string jsonBody)
        {
            string result;
            try
            {
                result = _V6Http.POST(createInvoiceUrl, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_NEW " + result);
            return result;
        }

        public static string POST_EDIT(V6Http _V6Http, string jsonBody)
        {
            string result;
            try
            {
                result = _V6Http.POST(modifyUrl, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_EDIT " + result);
            return result;
        }

        public static string ReadData_Viettel()
        {
            string result = "";
            try
            {
                DataRow row0 = am_table.Rows[0];
                var postObject = new PostObjectViettel();

                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> paymentsConfig = null;
                Dictionary<string, object> payment = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    payment[item.Key] = GetValue(row0, item.Value);
                }
                postObject.payments.Add(payment);//One payment only!
                //private static Dictionary<string, XmlLine> itemInfoConfig = null;
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.itemInfo.Add(rowData);
                }
                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private static Dictionary<string, XmlLine> taxBreakdownsConfig = null; 
                Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                }
                postObject.taxBreakdowns.Add(taxBreakdown);//One only!

                result = postObject.ToJson();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("RequestManager.ReadData", ex);
            }
            return result;
        }

        #endregion viettel

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
                    case "DATE":
                    case "DATETIME":
                        return ObjectAndString.ObjectToDate(fieldValue, config.Format);
                        break;
                    case "N2C":
                        return MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
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
            return V6BusinessHelper.MoneyToWords(money, lang, ma_nt);
        }

        
        /// <summary>
        /// Cần viết thêm đọc xml.
        /// </summary>
        /// <param name="map_table"></param>
        public static void ReadConfigInfo(DataTable map_table)
        {
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();

            parameters_config = new List<ConfigLine>();

            try
            {
                foreach (DataRow row in map_table.Rows)
                {
                    string GROUP_NAME = row["GroupName"].ToString().Trim().ToUpper();
                    ConfigLine line = ReadConfigLine(row);
                    string line_field = line.Field.ToLower();
                    switch (GROUP_NAME)
                    {
                        case "V6INFO":
                        {
                            switch (line_field)
                            {
                                    //Viettel
                                case "username":
                                    _username = line.Value;
                                    break;
                                case "password":
                                    _password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "baselink":
                                    if (line.Type == "ENCRYPT")
                                    {
                                        baseUrl = UtilityHelper.DeCrypt(line.Value);
                                    }
                                    else
                                    {
                                        baseUrl = line.Value;
                                    }
                                    break;
                                case "createlink":
                                    if (line.Type == "ENCRYPT")
                                    {
                                        createInvoiceUrl = UtilityHelper.DeCrypt(line.Value);
                                    }
                                    else
                                    {
                                        createInvoiceUrl = line.Value;
                                    }
                                    break;
                                case "modifylink":
                                    if (line.Type == "ENCRYPT")
                                    {
                                        modifyUrl = UtilityHelper.DeCrypt(line.Value);
                                    }
                                    else
                                    {
                                        modifyUrl = line.Value;
                                    }
                                    break;
                                    //Vnpt, có dùng cả username, password
                                case "link":
                                    //if (line.Type == "ENCRYPT")
                                    {
                                        baseUrl = UtilityHelper.DeCrypt(line.Value);
                                    }
                                    //else
                                    //{
                                    //    baseUrl = line.Value;
                                    //}
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
                                    //Bkav
                                case "bkavpartnerguid":
                                    BkavPartnerGUID = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "bkavpartnertoken":
                                    BkavPartnerToken = UtilityHelper.DeCrypt(line.Value);
                                    break;
                            }
                            break;
                        }
                        case "GENERALINVOICEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    generalInvoiceInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "BUYERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    buyerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SELLERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    sellerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "PAYMENTS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    paymentsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "ITEMINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    itemInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SUMMARIZEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    summarizeInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "TAXBREAKDOWNS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    taxBreakdownsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "EXCELCONFIG":
                            {
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
                        default:
                            {
                                break;
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                //f9Error += ex.Message;
                //f9ErrorAll += ex.Message;
            }
        }

        private static ConfigLine ReadConfigLine(DataRow reader)
        {
            ConfigLine config = new ConfigLine();
            config.Field = reader["Field"].ToString().Trim();
            config.Value = reader["Value"].ToString().Trim();
            config.FieldV6 = reader["FieldV6"].ToString().Trim();
            config.Type = reader["Type"].ToString().Trim();
            config.DataType = reader["DataType"].ToString().Trim();
            //config.Format = reader["Format"].ToString().Trim();
            return config;
        }
        
    }
}

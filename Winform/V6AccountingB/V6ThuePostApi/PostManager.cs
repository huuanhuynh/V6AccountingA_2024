using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using BSECUS;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using SignTokenCore;
using Spy;
using Spy.SpyObjects;
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
using V6ThuePostXmlApi.BusinessService;
using V6ThuePostXmlApi.PortalService;
using V6ThuePostXmlApi.PostObjects;
using V6ThuePostXmlApi.PublishService;
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
        private static string Fkey_hd_tt = null;
        /// <summary>
        /// Bảng dữ liệu xuất Excel
        /// </summary>
        static DataTable ad2_table;
        /// <summary>
        /// Bảng dữ liệu thuế
        /// </summary>
        static DataTable ad3_table;

        /// <summary>
        /// Tài khoản ws vnpt
        /// </summary>
        public static string _username = "";
        public static string _password = "";
        public static string _codetax = "";
        private static string baseUrl = "", _createInvoiceUrl = "", _modifylink = "";
        /// <summary>
        /// InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile (getInvoiceRepresentationFile url part.)
        /// </summary>
        private static string _downloadlinkpdf = "";

        public static string _link_Publish_vnpt = "";
        public static string _link_Portal_vnpt = "";
        public static string _link_Business_vnpt = "";
        public static string _link_Attachment_vnpt = "";

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
        /// <summary>
        /// Tài khoản đăng nhập vnpt
        /// </summary>
        private static string _account = null;
        private static string _accountpassword = null;
        private static string _SERIAL_CERT = null;
        private static string _token_password_title = null;
        private static string _token_password = null;
        private static string __partten, partten_field;
        private static string __serial, seri_field;
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
        public static int BkavCommandTypeNew = 112;

        /// <summary>
        /// <para>Hàm post chính, sẽ chuyển hướng theo string1-pmparams.Branch</para>
        /// <para>Kết quả hàm sai sẻ có error.</para>
        /// <para>Kết quả trả về = kết quả api trả về.</para>
        /// <para>Các tham số out để hứng giá trị lưu lại theo từng Branch.</para>
        /// </summary>
        /// <param name="paras">Các tham số đầu vào cần thiết. Tùy mode, branch. Sau khi thực hiện hàm xong paras nhận luôn Result.</param>
        /// <returns></returns>
        public static string PowerPost(PostManagerParams paras)//, out string sohoadon, out string id, out string error)
        {
            string result0 = null;
            try
            {
                map_table = paras.DataSet.Tables[0];
                ad_table = paras.DataSet.Tables[1];
                am_table = paras.DataSet.Tables[2];
                Fkey_hd_tt = paras.Fkey_hd_tt;
                DataRow row0 = am_table.Rows[0];
                ad2_table = paras.DataSet.Tables[3];
                if (paras.DataSet.Tables.Count > 4)
                {
                    ad3_table = paras.DataSet.Tables[4];
                }
                else
                {
                    ad3_table = null;
                }

                ReadConfigInfo(map_table);

                switch (paras.Branch)
                {
                    case "1":
                        result0 = EXECUTE_VIETTEL(paras);
                        break;
                    case "2":
                        result0 = EXECUTE_VNPT(paras);
                        break;
                    case "3":
                        result0 = EXECUTE_BKAV(paras);
                        break;
                    case "4":
                        result0 = EXECUTE_VNPT_TOKEN(paras);
                        break;
                    default:
                        paras.Result = new PM_Result();
                        paras.Result.ResultDictionary = new Dictionary<string, object>();
                        paras.Result.ResultDictionary["RESULT_ERROR"] = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                paras.Result = new PM_Result();
                paras.Result.ResultDictionary = new Dictionary<string, object>();
                paras.Result.ResultDictionary["EXCEPTION_MESSAGE"] = ex.Message;
                V6ControlFormHelper.WriteExLog("RequestManager.PowerPost", ex);
            }
            
            //sohoadon = sohoadon0;
            //id = id0;
            //error = error0;
            return result0;
        }

        /// <summary>
        /// <para>Tham số cần thiết: DataSet[map_table][ad_table][am_table], Branch[1viettel][2vnpt]</para>
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="error">Lỗi trả về.</param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public static string PowerDownloadPDF(PostManagerParams paras, out string error)
        {
            string result = null;
            error = null;
            paras.Result = new PM_Result();
            try
            {
                map_table = paras.DataSet.Tables[0];
                //ad_table = pmparams.DataSet.Tables[1];
                //am_table = pmparams.DataSet.Tables[2];
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = pmparams.DataSet.Tables[3];

                ReadConfigInfo(map_table);

                switch (paras.Branch)
                {
                    case "1":
                        result = ViettelDownloadInvoicePDF(paras);
                        break;
                    case "2":
                        result = VnptWS.DownloadInvPDFFkey(_link_Portal_vnpt, paras.Fkey_hd, _username, _password, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.ResultDictionary);
                        break;
                    case "3":
                        result = BkavDownloadInvoicePDF(paras);
                        break;
                    case "4":
                        result = VnptWS.DownloadInvPDFFkey(_link_Portal_vnpt, paras.Fkey_hd, _username, _password, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.ResultDictionary);
                        break;
                    default:
                        paras.Result.ResultError = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                paras.Result.ExceptionMessage = ex.Message;
                error = ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.PowerDownloadPDF", ex);
            }
            return result;
        }

        #region ==== BKAV ====

        private static string EXECUTE_BKAV(PostManagerParams paras)
        {
            //IDictionary<string, object> rd = new Dictionary<string, object>();
            string result = "";
            //int so_hd = 0;
            //sohoadon = null;
            //id = null;
            //error = null;
            paras.Result = new PM_Result();
            //paras.Result.ResultDictionary = rd;
            try
            {
                BkavWS bkavWS = new BkavWS();
                
                ExecCommandFunc wsExecCommand = null;
                var webservice = new V6ThuePostBkavApi.vn.ehoadon.wsdemo.WSPublicEHoaDon(baseUrl);
                wsExecCommand = webservice.ExecuteCommand;
                uint Constants_Mode = RemoteCommand.DefaultMode;
                var remoteCommand = new RemoteCommand(wsExecCommand, BkavPartnerGUID, BkavPartnerToken, Constants_Mode);

                string jsonBody = null;

                if (paras.Mode == "E_G1") // Gạch nợ.
                {
                    paras.Result.ResultError = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1")
                {
                    jsonBody = paras.Fkey_hd;
                    result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._202_CancelInvoiceByPartnerInvoiceID, out paras.Result.ResultDictionary);
                }
                else if (paras.Mode == "E_H2") // Hủy và ký hủy
                {
                    jsonBody = paras.Fkey_hd;
                    result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._202_CancelInvoiceByPartnerInvoiceID, out paras.Result.ResultDictionary);
                    if (!result.StartsWith("ERR"))
                    {
                        result = bkavWS.POST(remoteCommand, paras.V6PartnerID, BkavConst._205_SignGUID, out paras.Result.ResultDictionary);
                        result = bkavWS.POST(remoteCommand, "0f8fad5b-d9cb-469f-a165-70867728950e", BkavConst._205_SignGUID, out paras.Result.ResultDictionary);
                    }
                }
                else if (paras.Mode == "E_S1")
                {
                    jsonBody = ReadData_Bkav("S");
                    result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._121_CreateAdjust, out paras.Result.ResultDictionary);
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Bkav("T");
                    result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._123_CreateReplace, out paras.Result.ResultDictionary);
                }
                else if (paras.Mode == "M")
                {
                    jsonBody = ReadData_Bkav("M");
                    int commandType = BkavCommandTypeNew;
                    if (paras.Key_Down == "F4") commandType = BkavConst._101_CreateEmpty;
                    else if (paras.Key_Down == "F6") commandType = BkavConst._200_Update;

                    result = bkavWS.POST(remoteCommand, jsonBody, commandType, out paras.Result.ResultDictionary);
                }
                else if (paras.Mode == "S")
                {
                    jsonBody = ReadData_Bkav("S");
                    result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._121_CreateAdjust, out paras.Result.ResultDictionary);
                }
                else if (paras.Mode.StartsWith("T"))
                {
                    jsonBody = ReadData_Bkav("T");
                    result = bkavWS.POST(remoteCommand, jsonBody, BkavConst._123_CreateReplace, out paras.Result.ResultDictionary);
                }

                if (result.StartsWith("ERR"))
                {
                    if (string.IsNullOrEmpty(paras.Result.ResultError)) paras.Result.ResultError = result;
                }
                else
                {
                    //if(so_hd != 0) sohoadon = so_hd.ToString();
                }
            }
            catch (Exception ex)
            {
                paras.Result.ExceptionMessage = ex.Message;
                result += "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }



        public static string ReadData_Bkav(string mode)
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
                    if (item.Key == "OriginalInvoiceIdentify")
                    {
                        if (mode == "T")
                        {
                            postObject.Invoice[item.Key] = Fkey_hd_tt;// GetValue(row0, item.Value);
                            //postObject.Invoice["InvoiceStatusID"] = "1";
                            postObject.Invoice["InvoiceCode"] = null;
                            //postObject.Invoice["InvoiceNo"] = 0;
                            //postObject.Invoice["InvoiceForm"] = "";
                            //postObject.Invoice["InvoiceSerial"] = "";

                            //string OriginalInvoiceIdentify = string.Format("[{0}]_[{1}]_[{2}]",     //  "[01GTKT0/003]_[AA/17E]_[0000105]";
                            //    postObject.Invoice["InvoiceForm"],
                            //    postObject.Invoice["InvoiceSerial"],
                            //    postObject.Invoice["InvoiceNo"]);

                            //postObject.Invoice["OriginalInvoiceIdentify"] = OriginalInvoiceIdentify;
                        }
                    }
                    else
                    {
                        postObject.Invoice[item.Key] = GetValue(row0, item.Value);
                    }
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
                        ObjectAndString.ObjectToString(GetValue(row0, summarizeInfoConfig["PartnerInvoiceID"]), "ddMMyyyyHHmmss");
                    if (postObject.PartnerInvoiceID.ToString().Length < 14 && ObjectAndString.ObjectToInt(postObject.PartnerInvoiceID) != 0)
                    {
                        postObject.PartnerInvoiceID = ("00000000000000" + postObject.PartnerInvoiceID).Right("ddMMyyyyHHmmss".Length);
                    }
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

        
        

        #endregion bkav

        #region ==== VNPT ====

        private static string EXECUTE_VNPT(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    var xml = ReadDataXml();
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultError = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = VnptWS.ConfirmPaymentFkey(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = VnptWS.ConfirmPayment(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey
                    {
                        result = VnptWS.UnconfirmPaymentFkey(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        result = cancelInv(paras.Fkey_hd);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var xml = ReadDataXmlS();
                        result = adjustInv(xml, paras.Fkey_hd);
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var xml = ReadDataXmlT();
                        result = replaceInv(xml, paras.Fkey_hd);
                    }
                }
                else  if (paras.Mode.StartsWith("M") || paras.Mode == "") // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    var xml = ReadDataXml();
                    //File.Create(flagFileName1).Close();
                    result = ImportAndPublishInv(xml);

                    string invXml = DownloadInvFkeyNoPay(fkeyA);
                    paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
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
                        else if (paras.Mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                        {
                            string export_file;
                            bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                        else if (paras.Mode.EndsWith("3")) // Tự xuất pdf rồi gửi
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
                            
                            string rptFile = Path.Combine(paras.Dir, paras.RptFileFull);
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
                    else if (!result.StartsWith("OK"))       // Hoặc đã có trên hệ thống HDDT ERR:0
                    {
                        invXml = DownloadInvFkeyNoPay(fkeyA);
                        paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                        if (!string.IsNullOrEmpty(paras.Result.InvoiceNo))
                        {
                            result = "OK-Đã tồn tại fkey.";
                            paras.Result.ResultString = result;
                        }
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = paras.Fkey_hd;
                    
                    string invXml = DownloadInvFkeyNoPay(fkeyA);
                    paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    //WriteFlag(flagFileName4, so_hoa_don);
                    result += paras.Result.InvoiceNo;
                    //result += invXml;
                }
                else if (paras.Mode == "S")// || paras.Mode == "D")
                {
                    var xml = ReadDataXmlS();
                    result = adjustInv(xml, paras.Fkey_hd);
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
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
                else if (paras.Mode == "T")
                {
                    var xml = ReadDataXmlT();
                    result = replaceInv(xml, paras.Fkey_hd);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = VnptWS.ConfirmPaymentFkey(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = VnptWS.ConfirmPayment(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = VnptWS.UnconfirmPaymentFkey(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                }
                else if (paras.Mode == "H")
                {
                    result = cancelInv(paras.Fkey_hd);
                }
                else if (paras.Mode == "D")
                {
                    // Danh muc??
                    //string type = "1";
                    //if (mode.Length > 1) type = mode[1].ToString();
                    //result = DoUpdateCus(arg2, type);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    if (paras.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = paras.Fkey_hd;
                        string file = Path.Combine(paras.Dir, paras.FileName);
                        UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (paras.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = paras.Fkey_hd;
                        UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (paras.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (paras.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
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
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string rptFile = paras.RptFileFull;
                        //string saveFile = arg4;

                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

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
                    //error += result;
                    paras.Result.ResultError = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ExceptionMessage = ex.Message;
            }

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
                __partten = row0[partten_field].ToString().Trim();
                __serial = row0[seri_field].ToString().Trim();
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
                throw ex;
            }
            return result;
        }
        public static string ReadDataXmlS()
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                AdjustInv inv = new AdjustInv();
                
                DataRow row0 = am_table.Rows[0];
                fkeyA = row0["fkey_hd"].ToString().Trim();
                inv.key = fkeyA;
                
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
        public static string ReadDataXmlT()
        {
            string result = "";
            //try
            {
                //PostObject obj = new PostObject();
                ReplaceInv inv = new ReplaceInv();
                //ReadXmlInfo(xmlFile);

                DataRow row0 = am_table.Rows[0];
                fkeyA = row0["fkey_hd"].ToString().Trim();
                
                inv.key = fkeyA;
                //partten = row0[partten_field].ToString().Trim();
                //seri = row0[seri_field].ToString().Trim();
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

                var products = new List<Product>();
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
        /// Phát hành hóa đơn.
        /// </summary>
        /// <param name="xml">Dữ liệu các hóa đơn.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public static string ImportAndPublishInv(string xml)
        {
            string result = null;
            try
            {
                var publishService = new PublishService(_link_Publish_vnpt);
                result = publishService.ImportAndPublishInv(_account, _accountpassword, xml, _username, _password, __partten, __serial, convert == "1" ? 1 : 0);

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

        /// <summary>
        /// Download invoice VNPT
        /// </summary>
        /// <param name="fkey"></param>
        /// <returns></returns>
        public static string DownloadInvFkeyNoPay(string fkey)
        {
            string result = null;
            try
            {
                result = new PortalService(_link_Portal_vnpt).downloadInvFkeyNoPay(fkey, _username, _password);

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
                result = new AttachmentService(_link_Attachment_vnpt).uploadInvAttachmentFkey(fkey, _username, _password, attachment64, ext, attachmentName);

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
                result = new BusinessService(_link_Business_vnpt).adjustInv(_account, _accountpassword, xml, _username, _password, fkey_old, 0);

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
                result = new BusinessService(_link_Business_vnpt).replaceInv(_account, _accountpassword, xml, _username, _password, fkey_old, 0);

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
        public static string confirmPaymentFkey0(string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(_link_Business_vnpt).confirmPaymentFkey(fkey_old, _username, _password);

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
        /// Hủy hóa đơn.
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <returns></returns>
        public static string cancelInv(string fkey_old)
        {
            string result = null;
            try
            {
                result = new BusinessService(_link_Business_vnpt).cancelInv(_account, _accountpassword, fkey_old, _username, _password);

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

        private static string GetSoHoaDon_VNPT(string invXml)
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

        #endregion ==== VNPT ====


        #region ==== VNPT_TOKEN ====

        public static void StartAutoInputTokenPassword()
        {
            StopAutoInputTokenPassword();
            if (string.IsNullOrEmpty(_token_password)) return;
            if (string.IsNullOrEmpty(_token_password_title)) return;
            autoToken = new Thread(AutoInputTokenPassword);
            //autoToken.IsBackground = true;
            autoToken.Start();
        }

        private static Thread autoToken = null;
        public static void StopAutoInputTokenPassword()
        {
            if (autoToken != null && autoToken.IsAlive) autoToken.Abort();
        }
        private static void AutoInputTokenPassword()
        {
            try
            {
                //Find input password windows.
                Spy001 spy = new Spy001();
                var thisProcessID = Process.GetCurrentProcess().Id;
                SpyWindowHandle input_password_window = spy.FindWindow(_token_password_title, thisProcessID);

                while (input_password_window == null)
                {
                    input_password_window = spy.FindWindow(_token_password_title, thisProcessID);
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
                    foreach (char c in _token_password)
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

        private static string EXECUTE_VNPT_TOKEN(PostManagerParams paras)//, out string sohoadon, out string id, out string error)
        {
            string result = "";
            paras.Result = new PM_Result();
            try
            {
                var row0 = am_table.Rows[0];
                //MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                if (paras.Mode.StartsWith("M") || paras.Mode == "")
                {
                    var xml = ReadDataXml();
                    StartAutoInputTokenPassword();
                    string resultM = PublishInvWithToken_Dll(xml);
                    result = resultM;
                    paras.Result.ResultString = result;
                    //"OK:mẫu số;ký hiệu-Fkey_Số hóa đơn,"
                    //"OK:01GTKT0/001;VT/19E-A0283806HDA_XXX"
                    if (resultM.StartsWith("OK"))
                    {
                        paras.Result.InvoiceNo = GetSoHoaDon_Dll(resultM);
                    }
                    else if (resultM.StartsWith("ERR:0"))       // Hoặc đã có trên hệ thống HDDT ERR:0
                    {
                        string invXml = DownloadInvFkeyNoPay(fkeyA);
                        paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                        if (!string.IsNullOrEmpty(paras.Result.InvoiceNo))
                        {
                            paras.Result.ResultString = "OK-Đã tồn tại fkey.";
                        }
                    }
                    else // chạy lần 2
                    {
                        StartAutoInputTokenPassword();
                        resultM = PublishInvWithToken_Dll(xml);
                        result = resultM;
                        paras.Result.ResultString = result;
                        if (resultM.StartsWith("OK"))
                        {
                            paras.Result.InvoiceNo = GetSoHoaDon_Dll(resultM);
                        }
                        else // Đã chạy 2 lần vẫn không được.
                        {
                            paras.Result.ResultError = resultM;
                        }
                    }

                    // Gửi file.
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
                    if (filePath.Length > 0 && result.StartsWith("OK"))
                    {
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
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
                        else if (paras.Mode.EndsWith("2")) // Tự xuất excel rồi gửi.
                        {
                            string export_file;
                            bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                            if (export_ok && File.Exists(export_file))
                            {
                                result += UploadInvAttachmentFkey(fkeyA, export_file);
                            }
                        }
                        else if (paras.Mode.EndsWith("3")) // Tự xuất pdf rồi gửi
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

                            string rptFile = Path.Combine(paras.Dir, paras.RptFileFull);
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
                else if (String.Equals(paras.Mode, "DownloadInvFkeyNoPay", StringComparison.CurrentCultureIgnoreCase))
                {
                    fkeyA = paras.Fkey_hd;
                    string invXml = DownloadInvFkeyNoPay(fkeyA);
                    string so_hoa_don = GetSoHoaDon_VNPT(invXml);
                    paras.Result.InvoiceNo = so_hoa_don;
                    result += so_hoa_don;
                }
                else if (paras.Mode == "S" || paras.Mode == "D")
                {
                    var xml = ReadDataXmlS();
                    result = adjustInv(xml, paras.Fkey_hd);
                    paras.Result.ResultString = result;
                    string filePath = Path.Combine(paras.Dir, paras.FileName);
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
                else if (paras.Mode == "T")
                {
                    var xml = ReadDataXmlT();
                    result = replaceInv(xml, paras.Fkey_hd);
                    paras.Result.ResultString = result;
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        VnptWS.ConfirmPaymentFkey(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        VnptWS.ConfirmPayment(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        VnptWS.UnconfirmPaymentFkey(_link_Business_vnpt, paras.Fkey_hd, _username, _password, out paras.Result.ResultDictionary);
                    }
                }
                else if (paras.Mode == "H")
                {
                    //File.Create(flagFileName1).Close();
                    result = cancelInv(fkey_old: paras.Fkey_hd);
                }
                else if (paras.Mode == "D")
                {
                    //== S
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    if (paras.Mode == "U")        // upload file có sẵn, fkey truyền vào
                    {
                        string fkey = paras.Fkey_hd;
                        string file = Path.Combine(paras.Dir, paras.FileName);
                        UploadInvAttachmentFkey(fkey, file);
                    }
                    else if (paras.Mode == "U1") // upload file có sẵn, fkey tự đọc từ data
                    {
                        //ReadDataXml(arg2);
                        string fkey = paras.Fkey_hd;
                        UploadInvAttachmentFkey(fkey, fkey + ".xls");
                    }
                    else if (paras.Mode == "U2") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel rồi upload.
                    {
                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += UploadInvAttachmentFkey(fkeyA, export_file);
                        }
                    }
                    else if (paras.Mode == "U3") // Đọc dữ liệu hóa đơn, lấy fkey, đọc dữ liệu excel và xuất excel để đó xem.
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
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string rptFile = paras.RptFileFull;
                        //string saveFile = arg4;

                        string export_file;
                        //ReadDataXml(arg2);
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref result);

                        if (export_ok && File.Exists(export_file))
                        {
                            result += "\r\nExport ok.";
                        }
                    }
                    else if (paras.Mode == "E2")  // Xuất PDF bằng RPT
                    {
                        string rptFile = paras.RptFileFull;
                        string saveFile = Path.Combine(paras.Dir, paras.FileName);// arg4;

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
                    paras.Result.ResultError = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                paras.Result.ExceptionMessage = ex.Message;
                result += "ERR:EX\r\n" + ex.Message;
            }
            StopAutoInputTokenPassword();
        //File.Create(flagFileName9).Close();
        //BaseMessage.Show(result, 500);
        End:
            return result;
        }


        /// <summary>
        /// Đẩy lên và phát hành hóa đơn có ký chữ ký số (Token).
        /// </summary>
        /// <param name="xmlInvData">chuỗi xml hóa đơn.</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + “,”</returns>
        public static string PublishInvWithToken_Dll(string xmlInvData)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.PublishInvWithToken(_account, _accountpassword, xmlInvData, _username, _password, _SERIAL_CERT, __partten, __serial, _link_Publish_vnpt);
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
                result = result_string.Substring(_index + 1, result_string.Length - _index - 1);
                if (result.EndsWith(",")) result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
        #endregion ==== VNPT_TOKEN ====

        #region ==== VIETTEL ====

        private static string EXECUTE_VIETTEL(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            IDictionary<string, object> rd = new SortedDictionary<string, object>();
            paras.Result.ResultDictionary = rd;

            try
            {
                string jsonBody = "";
                var _V6Http = new ViettelWS(baseUrl, _username, _password);

                if (paras.Mode == "E_G1") // Gạch nợ
                {
                    rd["RESULT_ERROR"] = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1") // Hủy hóa đơn
                {
                    ViettelWS viettel_http = new ViettelWS(baseUrl, _username, _password);
                    DataRow row0 = am_table.Rows[0];
                    var item = generalInvoiceInfoConfig["invoiceIssuedDate"];
                    string strIssueDate = ((DateTime)GetValue(row0, item)).ToString("yyyyMMddHHmmss");
                    string additionalReferenceDesc = paras.AM_new["STT_REC"].ToString();
                    paras.InvoiceNo = paras.AM_new["SO_SERI"].ToString().Trim() + paras.AM_new["SO_CT"].ToString().Trim();
                    result = viettel_http.CancelTransactionInvoice(_codetax, paras.InvoiceNo, strIssueDate, additionalReferenceDesc, strIssueDate);
                    
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Viettel(paras);
                    result = POST_REPLACE(_V6Http, jsonBody);
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                    {
                        Field = "adjustmentType",
                        Value = "1",
                    };
                    Guid new_uid = Guid.NewGuid();
                    if (paras.Mode == "MG")
                    {
                        generalInvoiceInfoConfig["transactionUuid"] = new ConfigLine
                        {
                            Field = "transactionUuid",
                            Value = "" + new_uid,
                        };
                    }
                    jsonBody = ReadData_Viettel(paras);
                    //File.Create(flagFileName1).Close();
                    result = POST_NEW(_V6Http, jsonBody);
                }
                else if (paras.Mode.StartsWith("S"))
                {
                    if (paras.Mode.EndsWith("3"))
                    {
                        generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                        {
                            Field = "adjustmentType",
                            Value = "3",
                        };
                    }
                    else if (paras.Mode.EndsWith("5"))
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

                    jsonBody = ReadData_Viettel(paras);
                    //File.Create(flagFileName1).Close();
                    result = POST_EDIT(_V6Http, jsonBody);
                }

                //Phân tích result
                paras.Result.ResultDictionary["RESULT_STRING"] = result;
                string message = "";
                try
                {
                    CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
                    paras.Result.ResultDictionary["RESULT_OBJECT"] = responseObject;
                    if (!string.IsNullOrEmpty(responseObject.description))
                    {
                        message += " " + responseObject.description;
                    }

                    if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                    {
                        paras.Result.ResultDictionary["SO_HD"] = responseObject.result.invoiceNo;
                        paras.Result.ResultDictionary["ID"] = responseObject.result.transactionID;
                        paras.Result.ResultDictionary["SECRECT_CODE"] = responseObject.result.reservationCode;
                        message += " " + responseObject.result.invoiceNo;
                        
                    }
                    else if (responseObject.errorCode == null)
                    {
                        paras.Result.ResultDictionary["SO_HD"] = paras.InvoiceNo;
                        paras.Result.ResultDictionary["RESULT_MESSAGE"] = responseObject.description;
                    }
                    else
                    {
                        paras.Result.ResultDictionary["RESULT_ERROR"] = responseObject.description;
                        paras.Result.ResultDictionary["RESULT_ERROR_CODE"] = responseObject.errorCode;
                    }
                }
                catch (Exception ex)
                {
                    paras.Result.ResultDictionary["RESULT_ERROR"] = ex.Message;
                    paras.Result.ResultDictionary["EXCEPTION_MESSAGE"] = ex.Message;
                    Logger.WriteToLog("EXECUTE_VIETTEL ConverResultObjectException: " + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
            }
            catch (Exception ex)
            {
                paras.Result.ResultDictionary["RESULT_ERROR"] = ex.Message;
                paras.Result.ResultDictionary["EXCEPTION_MESSAGE"] = ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.EXECUTE_VIETTEL", ex);
            }

            return result;
        }

        public static string POST_NEW(ViettelWS _V6Http, string jsonBody)
        {
            string result;
            try
            {
                result = _V6Http.POST(_createInvoiceUrl, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_NEW " + result);
            return result;
        }

        public static string POST_EDIT(ViettelWS _V6Http, string jsonBody)
        {
            string result;
            try
            {
                result = _V6Http.POST(_modifylink, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_EDIT " + result);
            return result;
        }

        /// <summary>
        /// Hàm giống tạo mới nhưng có khác biệt trong dữ liệu.
        /// </summary>
        /// <param name="_V6Http"></param>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        public static string POST_REPLACE(ViettelWS _V6Http, string jsonBody)
        {
            string result;
            try
            {
                result = _V6Http.POST(_createInvoiceUrl, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_NEW " + result);
            return result;
        }

        public static string POST_DRAFT(ViettelWS _V6Http, string jsonBody)
        {
            string result;
            try
            {
                result = _V6Http.POST("InvoiceAPI/InvoiceWS/createOrUpdateInvoiceDraft/" + _codetax, jsonBody);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            Logger.WriteToLog("Program.POST_NEW " + result);
            return result;
        }

        public static string ReadData_Viettel(PostManagerParams paras)
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

                if (paras.Mode == "E_T1")
                {
                    //Lập hóa đơn thay thế:
                    //adjustmentType = ‘3’
                    postObject.generalInvoiceInfo["adjustmentType"] = "3";
                    //Các trường dữ liệu về hóa đơn gốc là bắt buộc
                    //originalInvoiceId
                    postObject.generalInvoiceInfo["originalInvoiceId"] =  paras.AM_old["FKEY_TT"].ToString().Trim();  // [AA/17E0003470]
                    //originalInvoiceIssueDate
                    postObject.generalInvoiceInfo["originalInvoiceIssueDate"] = paras.AM_old["NGAY_CT"];

                    //Thông tin về biên bản đính kèm hóa đơn gốc:
                    //additionalReferenceDate
                    postObject.generalInvoiceInfo["additionalReferenceDate"] = paras.AM_old["NGAY_CT"];
                    //additionalReferenceDesc
                    postObject.generalInvoiceInfo["additionalReferenceDesc"] = paras.AM_new["GHI_CHU03"];
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
                //Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                //foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                //{
                //    taxBreakdown[item.Key] = GetValue(row0, item.Value);
                //}
                //postObject.taxBreakdowns.Add(taxBreakdown);//One only!
                if (taxBreakdownsConfig != null && ad3_table != null && ad3_table.Rows.Count > 0)
                {
                    foreach (DataRow ad3_row in ad3_table.Rows)
                    {
                        Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                        {
                            taxBreakdown[item.Key] = GetValue(ad3_row, item.Value);
                        }
                        postObject.taxBreakdowns.Add(taxBreakdown);
                    }
                }
                

                result = postObject.ToJson();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("RequestManager.ReadData", ex);
            }
            return result;
        }

        /// <summary>
        /// Trả về đường dẫn file pdf.
        /// </summary>
        /// <param name="postManagerParams"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public static string BkavDownloadInvoicePDF(PostManagerParams postManagerParams)
        {
            BkavWS bkav_ws = new BkavWS();
            ExecCommandFunc wsExecCommand = null;
            var webservice = new V6ThuePostBkavApi.vn.ehoadon.wsdemo.WSPublicEHoaDon(baseUrl);
            wsExecCommand = webservice.ExecuteCommand;
            uint Constants_Mode = RemoteCommand.DefaultMode;
            var remoteCommand = new RemoteCommand(wsExecCommand, BkavPartnerGUID, BkavPartnerToken, Constants_Mode);
            return bkav_ws.DownloadInvoicePDF(remoteCommand, postManagerParams.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory);
        }

        /// <summary>
        /// Trả về đường dẫn file pdf.
        /// </summary>
        /// <param name="postManagerParams"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public static string ViettelDownloadInvoicePDF(PostManagerParams postManagerParams)
        {
            ViettelWS viettel_ws = new ViettelWS(baseUrl, _username, _password);
            
            return viettel_ws.DownloadInvoicePDF(_codetax, _downloadlinkpdf, postManagerParams.InvoiceNo, postManagerParams.Parttern, V6Setting.V6SoftLocalAppData_Directory);
        }
        
        #endregion viettel

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
                    //case "UPPER": // Chỉ dùng ở exe gọi bằng Foxpro.
                    //    return (fieldValue + "").ToUpper();
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
                                case "username":
                                    _username = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "codetax":
                                    _codetax = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "password":
                                    _password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "baselink":
                                    baseUrl = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "createlink":
                                    _createInvoiceUrl = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "modifylink":
                                    _modifylink = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "downloadlinkpdf":
                                    _downloadlinkpdf = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                    //Vnpt, có dùng cả username, password
                                case "link_publish":
                                        _link_Publish_vnpt = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "link_business":
                                    _link_Business_vnpt = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "link_portal":
                                    _link_Portal_vnpt = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "link_attachment":
                                    _link_Attachment_vnpt = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "account":
                                    _account = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "accountpassword":
                                    _accountpassword = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "serialcert":
                                    _SERIAL_CERT = UtilityHelper.DeCrypt(line.Value).ToUpper();
                                    break;
                                case "token_password_title":
                                    _token_password_title = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "token_password":
                                    _token_password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "partten":
                                    partten_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "seri":
                                    seri_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                    //Bkav
                                case "bkavpartnerguid":
                                    BkavPartnerGUID = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "bkavpartnertoken":
                                    BkavPartnerToken = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "bkavcommandtypenew":
                                    BkavCommandTypeNew = ObjectAndString.ObjectToInt(UtilityHelper.DeCrypt(line.Value));
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
                throw;
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

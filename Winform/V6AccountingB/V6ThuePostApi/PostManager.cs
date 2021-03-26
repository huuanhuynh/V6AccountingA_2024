﻿using System;
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
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ThuePost;
using V6ThuePost.MInvoiceObject.Request;
using V6ThuePost.MInvoiceObject.Response;
using V6ThuePost.MONET_Objects.Response;
using V6ThuePost.ResponseObjects;
using V6ThuePost.ViettelObjects;
using V6ThuePost.ViettelV2Objects;
using V6ThuePost.VnptObjects;
using V6ThuePostBkavApi;
using V6ThuePostBkavApi.PostObjects;
using V6ThuePostMInvoiceApi;
using V6ThuePostMonetApi;
using V6ThuePostSoftDreamsApi;
using V6ThuePostThaiSonApi;
using V6ThuePostThaiSonApi.EinvoiceService;
using V6ThuePostViettelApi;
using V6ThuePostXmlApi;
using V6ThuePostXmlApi.AttachmentService;
using V6ThuePostXmlApi.BusinessService;
using V6ThuePostXmlApi.PortalService;
using V6ThuePostXmlApi.PublishService;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Reader;
using CreateInvoiceResponse = V6ThuePost.ViettelObjects.CreateInvoiceResponse;

namespace V6ThuePostManager
{
    /// <summary>
    /// Lớp quản lý POST GET
    /// </summary>
    public static class PostManager
    {
        static DataTable _map_table;
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
        /// Toàn bộ dữ liệu config V6Info (chưa giải mã nếu có mã hóa).
        /// </summary>
        public static Dictionary<string, string> V6Infos = new Dictionary<string, string>();
        /// <summary>
        /// Tài khoản ws vnpt
        /// </summary>
        public static string _username = "";
        public static string _password = "";
        public static string _codetax = "";
        public static string _version = "";
        public static string _ma_dvcs = "";
        public static string _baseUrl = "", _site = "", _datetype = "", _createInvoiceUrl = "", _modifylink = "";
        /// <summary>
        /// InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile (getInvoiceRepresentationFile url part.)
        /// </summary>
        private static string _downloadlinkpdf = "";
        private static string _downloadlinkpdfe = "";

        public static string _link_Publish_vnpt_thaison = "";
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
        /// <summary>
        /// Số seri của TOKEN USB
        /// </summary>
        private static string _SERIAL_CERT = null;
        private static string _token_password_title = null;
        private static string _token_password = null;

        /// <summary>
        /// Có sau khi ReadData VNPT THAISON SOFTDREAMS
        /// </summary>
        private static string __pattern;
        private static string pattern_field;
        private static string __serial, seri_field;
        private static string convert = "0";
        private static string _signmode = "0";


        private static Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        private static Dictionary<string, ConfigLine> buyerInfoConfig = null;
        private static Dictionary<string, ConfigLine> sellerInfoConfig = null;
        private static Dictionary<string, ConfigLine> metadataConfig = null;
        private static Dictionary<string, ConfigLine> paymentsConfig = null;
        private static Dictionary<string, ConfigLine> itemInfoConfig = null;
        private static Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        private static Dictionary<string, ConfigLine> taxBreakdownsConfig = null;
        private static Dictionary<string, ConfigLine> customerInfoConfig = null;

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
                _map_table = paras.DataSet.Tables[0];
                ad_table = paras.DataSet.Tables[1];
                am_table = paras.DataSet.Tables[2];
                Fkey_hd_tt = paras.Fkey_hd_tt;
                //DataRow row0 = am_table.Rows[0];
                ad2_table = paras.DataSet.Tables[3];
                ad3_table = paras.DataSet.Tables.Count > 4 ? paras.DataSet.Tables[4] : null;

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        result0 = EXECUTE_VIETTEL(paras);
                        break;
                    case "Test":
                        result0 = EXECUTE_VIETTEL_V2CALL(paras);
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
                    case "5":
                        result0 = EXECUTE_SOFTDREAMS(paras);
                        break;
                    case "6":
                        result0 = EXECUTE_THAI_SON(paras);
                        break;
                    case "7":
                        result0 = EXECUTE_MONET(paras);
                        break;
                    case "8":
                        result0 = EXECUTE_MINVOICE(paras);
                        break;
                    default:
                        paras.Result = new PM_Result();
                        paras.Result.V6ReturnValues = new V6Return();
                        paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                paras.Result = new PM_Result();
                paras.Result.V6ReturnValues = new V6Return();
                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "EX: " + ex.Message;
                V6ControlFormHelper.WriteExLog("RequestManager.PowerPost", ex);
            }
            
            //sohoadon = sohoadon0;
            //id = id0;
            //error = error0;
            return result0;
        }

        public static string GetConfigBaseLink(DataTable mapTable)
        {
            _site = "";
            _map_table = mapTable;
            ReadConfigInfo(mapTable);
            return _site;
        }

        /// <summary>
        /// Hàm kiểm tra kết nối. Nếu thành công trả về rỗng hoặc null.
        /// </summary>
        /// <param name="paras"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string PowerCheckConnection(PostManagerParams paras, out string exception)
        {
            string result = null;
            exception = null;
            paras.Result = new PM_Result();
            try
            {
                _map_table = paras.DataSet.Tables[0];
                
                //ad_table = pmparams.DataSet.Tables[1];
                //am_table = pmparams.DataSet.Tables[2];
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = pmparams.DataSet.Tables[3];

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        if (_version == "V2")
                        {
                            var process = new Process
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    WorkingDirectory = "ViettelV2",
                                    FileName = "ViettelV2\\V6ThuePostViettelV2.exe",
                                    UseShellExecute = false,
                                    RedirectStandardOutput = true,
                                    CreateNoWindow = true
                                }
                            };
                            
                            process.StartInfo.Arguments = "MTEST_JSON V6ThuePost.xml";
                            process.Start();
                            string process_result = process.StandardOutput.ReadToEnd();
                            process.WaitForExit();
                            // Phân tích Result tại đây.
                            paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                            if (paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE != null && paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE.Contains("JSON_PARSE_ERROR"))
                            {
                                return null;
                            }
                            if (paras.Result.V6ReturnValues.RESULT_STRING.Contains("\"errorCode\":\"TEMPLATE_NOT_FOUND\""))
                            {
                                return null;
                            }

                            return "Lỗi kết nối." + paras.Result.V6ReturnValues.RESULT_STRING;
                        }
                        ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);
                        result = viettel_ws.CheckConnection();
                        break;
                    case "2":
                    case "4":
                        VnptWS vnptWS = CreateVnptWS();
                        result = vnptWS.CheckConnection();
                        break;
                    case "3":
                        BkavWS bkavWS = new BkavWS(_baseUrl, BkavPartnerGUID, BkavPartnerToken);
                        result = bkavWS.POST("{}", BkavCommandTypeNew, out paras.Result.V6ReturnValues);
                        if (result.Contains("ERR:Có lỗi xảy ra."))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Kết nối lỗi.";
                        }

                        break;
                    case "5":
                        SoftDreamsWS softDreamsWs = new SoftDreamsWS(_baseUrl, _username, _password, _SERIAL_CERT);
                        result = softDreamsWs.GetInvoicePdf(paras.Fkey_hd, paras.Mode == "2" ? 2 : paras.Mode == "1" ? 1 : 0, paras.Pattern, paras.Serial, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        if (paras.Result.V6ReturnValues.RESULT_MESSAGE != null && paras.Result.V6ReturnValues.RESULT_MESSAGE.Contains("Có lỗi xả ra: {\"Status\":4,\"Message\":\"Ikey không được bỏ trống\"}"))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Kết nối lỗi.";
                        }
                        break;
                    case "6":
                        _map_table = paras.DataSet.Tables[0];
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

                        ReadConfigInfo(_map_table);

                        ThaiSonWS thaiSonWS = new ThaiSonWS(_baseUrl, _link_Publish_vnpt_thaison, _username, _password, _SERIAL_CERT);
                        var hoadon_entity = (HoaDonEntity)ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                        hoadon_entity.SoHoaDon = "TEST";
                        hoadon_entity.KyHieu = "TEST";
                        hoadon_entity.MauSo = "TEST";
                        //result = thaiSonWS.ImportThongTinHoaDon(hoadon_entity, out paras.Result.V6ReturnValues);
                        result = thaiSonWS.XuatHoaDonDienTu(hoadon_entity, out paras.Result.V6ReturnValues);
                        if (result.Contains("Hóa đơn mang ký hiệu TEST,") || paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE.Contains("Nhập sai thông tin"))
                        {
                            result = null;
                        }
                        else
                        {
                            result = "Kết nối lỗi.";
                        }
                        break;
                    case "7":
                        MONET_WS monetWS = new MONET_WS(_baseUrl, _username, _password, _codetax);
                        result = monetWS.CheckConnection(_createInvoiceUrl);
                        break;
                    case "8":
                        MInvoiceWS mInvoiceWs = new MInvoiceWS(_baseUrl, _username, _password, _ma_dvcs, _codetax);
                        result = mInvoiceWs.CheckConnection(out paras.Result.V6ReturnValues);
                        break;
                    default:
                        paras.Result.ResultErrorMessage = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                result += "Ex: " + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
                exception = ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.PowerCheckConnection", ex);
            }
            return result;
        }

        static V6Return GetV6ReturnFromCallExe(string call_result)
        {
            var v6return = JsonConvert.DeserializeObject<V6Return>(call_result);
            if (!string.IsNullOrEmpty(v6return.RESULT_ERROR_MESSAGE))
            {
                v6return.RESULT_ERROR_MESSAGE = ChuyenMaTiengViet.TCVNtoUNICODE(v6return.RESULT_ERROR_MESSAGE);
            }
            if (!string.IsNullOrEmpty(v6return.RESULT_MESSAGE))
            {
                v6return.RESULT_MESSAGE = ChuyenMaTiengViet.TCVNtoUNICODE(v6return.RESULT_MESSAGE);
            }
            return v6return;
        }
        
        /// <summary>
        /// <para>Tham số cần thiết: DataSet[_map_table][ad_table][am_table], Branch[1viettel][2vnpt]</para>
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
                _map_table = paras.DataSet.Tables[0];
                //ad_table = pmparams.DataSet.Tables[1];
                //am_table = pmparams.DataSet.Tables[2];
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = pmparams.DataSet.Tables[3];

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        result = ViettelDownloadInvoicePDF(paras);
                        break;
                    case "2": case "4":
                        int option = ObjectAndString.ObjectToInt(paras.Mode);
                        VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
                        result = vnptWS.DownloadInvPDFFkey(paras.Fkey_hd, option,V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "3":
                        BkavWS bkav_ws = new BkavWS(_baseUrl, BkavPartnerGUID, BkavPartnerToken);
                        result = bkav_ws.DownloadInvoicePDF(paras.Fkey_hd, V6Setting.V6SoftLocalAppData_Directory);
                        break;
                    case "5":
                        SoftDreamsWS softDreamsWs = new SoftDreamsWS(_baseUrl, _username, _password, _SERIAL_CERT);
                        result = softDreamsWs.GetInvoicePdf(paras.Fkey_hd, paras.Mode == "2" ? 2 : paras.Mode == "1" ? 1 : 0, paras.Pattern, paras.Serial, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "6":
                        ThaiSonWS thaiSonWS = new ThaiSonWS(_baseUrl, _link_Publish_vnpt_thaison, _username, _password, _SERIAL_CERT);
                        result = thaiSonWS.GetInvoicePdf(paras.V6PartnerID, paras.Mode == "0" ? 0 : 1, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    case "7":
                        MONET_WS monetWS = new MONET_WS(_baseUrl, _username, _password, _codetax);
                        result = monetWS.DownloadInvoicePDF(paras.V6PartnerID, paras.Pattern, V6Setting.V6SoftLocalAppData_Directory);
                        break;
                    case "8":
                        MInvoiceWS mInvoiceWs = new MInvoiceWS(_baseUrl, _username, _password, _ma_dvcs, _codetax);
                        result = mInvoiceWs.DownloadInvoicePDF(paras.V6PartnerID, V6Setting.V6SoftLocalAppData_Directory, out paras.Result.V6ReturnValues);
                        break;
                    default:
                        paras.Result.ResultErrorMessage = V6Text.NotSupported + paras.Branch;
                        break;
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
                error = ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.PowerDownloadPDF", ex);
            }
            return result;
        }

        #region ==== BKAV ====

        private static string EXECUTE_BKAV(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            try
            {
                BkavWS bkavWS = new BkavWS(_baseUrl, BkavPartnerGUID, BkavPartnerToken);
                
                string jsonBody;

                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Bkav("M");
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode == "E_G1") // Gạch nợ.
                {
                    paras.Result.ResultErrorMessage = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1")
                {
                    jsonBody = paras.Fkey_hd;
                    result = bkavWS.POST(jsonBody, BkavConst._202_CancelInvoiceByPartnerInvoiceID, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "E_H2") // Hủy và ký hủy
                {
                    jsonBody = paras.Fkey_hd;
                    result = bkavWS.POST(jsonBody, BkavConst._202_CancelInvoiceByPartnerInvoiceID, out paras.Result.V6ReturnValues);
                    if (!result.StartsWith("ERR") && V6Infos.ContainsKey("BKAVSIGN") &&  V6Infos["BKAVSIGN"] == "1")
                    {
                        result = result + "\r\n" + bkavWS.SignInvoice(paras.V6PartnerID, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "E_S1")
                {
                    jsonBody = ReadData_Bkav("S");
                    result = bkavWS.POST(jsonBody, BkavConst._121_CreateAdjust, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Bkav("T");
                    result = bkavWS.POST(jsonBody, BkavConst._123_CreateReplace, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "M")
                {
                    jsonBody = ReadData_Bkav("M");
                    int commandType = BkavCommandTypeNew;
                    if (paras.Key_Down == "F4") commandType = BkavConst._101_CreateEmpty;
                    else if (paras.Key_Down == "F6") commandType = BkavConst._200_Update;

                    result = bkavWS.POST(jsonBody, commandType, out paras.Result.V6ReturnValues);
                    if (string.IsNullOrEmpty(paras.Key_Down) && V6Infos.ContainsKey("BKAVSIGN") &&  V6Infos["BKAVSIGN"] == "1")
                    {
                        V6Return v6return2;
                        result = result + "\r\n" + bkavWS.SignInvoice(paras.Result.V6ReturnValues.ID, out v6return2);
                    }
                }
                else if (paras.Mode == "S")
                {
                    jsonBody = ReadData_Bkav("S");
                    result = bkavWS.POST(jsonBody, BkavConst._121_CreateAdjust, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("T"))
                {
                    jsonBody = ReadData_Bkav("T");
                    result = bkavWS.POST(jsonBody, BkavConst._123_CreateReplace, out paras.Result.V6ReturnValues);
                }
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }

                if (result.StartsWith("ERR"))
                {
                    if (string.IsNullOrEmpty(paras.Result.ResultErrorMessage)) paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //if(so_hd != 0) sohoadon = so_hd.ToString();
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
                result += "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }


        /// <summary>
        /// Đọc dữ liệu cho Bkav
        /// </summary>
        /// <param name="mode">M or S or T</param>
        /// <returns></returns>
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
                    if (postObject.PartnerInvoiceID.Length < 14 && ObjectAndString.ObjectToInt(postObject.PartnerInvoiceID) != 0)
                    {
                        postObject.PartnerInvoiceID = ("00000000000000" + postObject.PartnerInvoiceID).Right("ddMMyyyyHHmmss".Length);
                    }
                }
                if (summarizeInfoConfig.ContainsKey("PartnerInvoiceStringID"))
                {
                    postObject.PartnerInvoiceStringID =
                        GetValue(row0, summarizeInfoConfig["PartnerInvoiceStringID"]).ToString();
                }

                result = postObject.ToJson();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "[" + result + "]";
        }

        
        

        #endregion bkav

        #region ==== VNPT ====

        /// <summary>
        /// Hàm chỉ áp dụng được sau khi đã chạy và nhận cấu hình.
        /// </summary>
        /// <returns></returns>
        private static VnptWS CreateVnptWS()
        {
            if (V6Infos == null || V6Infos.Count == 0)
            {
                throw new Exception(V6Text.NoDefine);
            }
            VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
            return vnptWS;
        }

        private static string EXECUTE_VNPT(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                var row0 = am_table.Rows[0];
                VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Vnpt();
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultErrorMessage = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey
                    {
                        result = vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        result = vnptWS.cancelInv(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var xml = ReadDataS_Vnpt();
                        result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var xml = ReadDataXmlT();
                        result = vnptWS.replaceInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else  if (paras.Mode.StartsWith("M") || paras.Mode == "") // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    try // Update danh mục khách hàng.
                    {
                        if (customerInfoConfig != null && customerInfoConfig.Count > 0)
                        {
                            DoUpdateCus(vnptWS, am_table);
                        }
                    }
                    catch (Exception)
                    {
                        // Bỏ qua lỗi.
                    }
                    var xml = ReadData_Vnpt();
                    
                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        result = vnptWS.ImportAndPublishInv(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                    }
                    else
                    {
                        StartAutoInputTokenPassword();
                        result = PublishInvWithToken_Dll(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                    }
                    

                    //string invXml = DownloadInvFkeyNoPay(fkeyA);
                    //paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    
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
                            string export_file;
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
                        V6Return v6return;
                        vnptWS.DownloadInvFkeyNoPay(fkeyA, out v6return);
                        paras.Result.InvoiceNo = v6return.SO_HD;
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
                    
                    string invXml = vnptWS.DownloadInvFkeyNoPay(fkeyA, out paras.Result.V6ReturnValues);
                    result += paras.Result.InvoiceNo;
                }
                else if (paras.Mode == "S")
                {
                    var xml = ReadDataS_Vnpt();
                    result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
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
                    result = vnptWS.replaceInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    result = vnptWS.cancelInv(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "D")
                {
                    // Danh muc??
                    string type = "1";
                    if (paras.Mode.Length > 1) type = paras.Mode[1].ToString();
                    result = DoUpdateCus(vnptWS, am_table, type);
                    //DataTable data = V6BusinessHelper.Select("ALKH", "*", "ISNULL([E_MAIL],'') <> ''").Data;
                    //result = DoUpdateCus(data, type);
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
                        string export_file;
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
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    //error += result;
                    paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
            End:
            return result;
        }

        public static string DoUpdateCus(DataTable data, string type = "1")
        {
            return DoUpdateCus(CreateVnptWS(), data, type);
        }
        public static string DoUpdateCus(VnptWS vnptWS, DataTable data, string type = "1")
        {
            string update_cus_result = "";
            string error = "";
            int error_count = 0, success_count = 0;
            
            try
            {
                //var data = ReadDbf(dbf);
                if (type == "1") // 1 times 1 cus
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
                            V6Return v6return;
                            var num = vnptWS.UpdateCus(xml, out v6return);
                            if (num == "1")
                            {
                                success_count++;
                                Logger.WriteToLog(string.Format("UpdateCus {0} {1} Success.", count, ma_kh));
                            }
                            else error_count++;

                            update_cus_result += string.Format("\r\n Update {0} status: {1}", ma_kh, num);
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
                else // 1 times all cus
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
                        V6Return v6return;
                        var num = vnptWS.UpdateCus(xml, out v6return);
                        success_count = Convert.ToInt32(num);
                        update_cus_result += "Success " + num + "/" + data.Rows.Count;
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

            if (error.Length > 0) update_cus_result += "ERR: " + error;
            Logger.WriteToLog("Program.DoUpdateCus " + update_cus_result);
            //return result;
            return string.Format("Success {0}   Error {1}\n{2}", success_count, error_count, update_cus_result);
        }

        public static Customer ReadCusDataXml(DataRow row)
        {
            Customer cus = new Customer();
            foreach (KeyValuePair<string, ConfigLine> item in customerInfoConfig)
            {
                cus.Customer_Info[item.Key] = GetValue(row, item.Value);
            }
            return cus;
        }

        public static string ReadData_Vnpt()
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
                __pattern = row0[pattern_field].ToString().Trim();
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

                result = postObject.ToXml();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string ReadDataS_Vnpt()
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

                result = inv.ToXml();
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
                //pattern = row0[pattern_field].ToString().Trim();
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

                result = inv.ToXml();
            }
            //catch (Exception ex)
            {
                //
            }
            return result;
        }

        public static Invoices ReadData_SoftDreams(string mode)
        {
            Invoices postObject = null;
            //string result = "";
            column_config = new SortedDictionary<string, string>();
            parameters_config = new List<ConfigLine>();
            //try
            {
                postObject = new Invoices();
                //ReadXmlInfo(xmlFile);
                //DataTable data = ReadDbf(dbfFile);

                var inv = new Inv();
                postObject.Inv.Add(inv);

                DataRow row0 = am_table.Rows[0];
                //fkeyA = fkey0 + row0["STT_REC"];
                fkeyA = row0["fkey_hd"].ToString().Trim();
                
                //{Tuanmh V6_05_2222222222222222_3333333333333
                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                //}

                inv.Invoice["Ikey"] = fkeyA;
                __pattern = row0[pattern_field].ToString().Trim();
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

                //result = XmlConverter.ClassToXml(postObject);
            }
            //catch (Exception ex)
            {
                //
            }
            return postObject;
        }

        /// <summary>
        /// Đọc dữ liệu trả về Class object cho Webservice.
        /// </summary>
        /// <param name="mode">M-H</param>
        /// <returns></returns>
        public static object ReadData_ThaiSon(string mode)
        {
            object result_entity = null;

            column_config = new SortedDictionary<string, string>();
            var am_data = new Dictionary<string, object>();
            List<HangHoaEntity> list_hanghoa = new List<HangHoaEntity>();
            parameters_config = new List<ConfigLine>();
            //try
            {
                DataTable data = am_table;

                DataRow row0 = am_table.Rows[0];
                //fkeyA = fkey0 + row0["STT_REC"];
                fkeyA = row0["fkey_hd"].ToString().Trim();

                exportName = string.Format("{0}_{1}_{2}_{3}", fkeyexcel0, row0["MA_KH"].ToString().Trim(), row0["SO_CT"].ToString().Trim(), row0["STT_REC"]);
                
                __pattern = row0[pattern_field].ToString().Trim();
                __serial = row0[seri_field].ToString().Trim();
                
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }
                
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }
                
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }


                if (mode == "H") goto EndDetail;
                foreach (DataRow row in ad_table.Rows)
                {
                    //if (row["STT"].ToString() == "0") continue;
                    var ad_data = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        ad_data[item.Key] = GetValue(row, item.Value);
                    }

                    var product = ad_data.ToClass<HangHoaEntity>();
                    list_hanghoa.Add(product);
                }
            EndDetail:

                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }

                foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                {
                    am_data[item.Key] = GetValue(row0, item.Value);
                }

                if (mode == "H")
                {
                    var hoa_don_huy_entity = am_data.ToClass<HoaDonHuyEntity>();
                    result_entity = hoa_don_huy_entity;
                }
                else // mode M
                {
                    var hoa_don_entity = am_data.ToClass<HoaDonEntity>();
                    hoa_don_entity.HangHoas = list_hanghoa.ToArray();
                    //hoa_don_entity.dataExtension = list_extension.ToArray();
                    //hoa_don_entity.emptysField = emptysField.ToArray();
                    result_entity = hoa_don_entity;
                }

                
            }
            //catch (Exception ex)
            {
                //
            }
            return result_entity;
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

        public static string cancelInv_VNPT_TOKEN(string fkey_old)
        {
            string result = null;
            try
            {
                string xml = ReadData_Vnpt();
                result = VNPTEInvoiceSignToken.CancelInvoiceWithToken(_account, _accountpassword, xml, _username,
                    _password, __pattern, _link_Business_vnpt);
                //result = new BusinessService(_link_Business_vnpt).cancelInv(_account, _accountpassword, fkey_old, _username, _password);

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
        /// Download invoice VNPT
        /// </summary>
        /// <param name="fkey"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public static string DownloadInvFkeyNoPay(string fkey, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new PortalService(_link_Portal_vnpt).downloadInvFkeyNoPay(fkey, _username, _password);
                v6Return.RESULT_STRING = result;
                if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
                else if (result.StartsWith("ERR"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi.";
                }
                else
                {
                    v6Return.SO_HD = GetSoHoaDon_VNPT(result);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

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

        /// <summary>
        /// Chạy tiến trình tự động điền password token nếu có thông tin pass và title.
        /// </summary>
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

        private static string EXECUTE_VNPT_TOKEN(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            try
            {
                var row0 = am_table.Rows[0];
                VnptWS vnptWS = new VnptWS(_baseUrl, _account, _accountpassword, _username, _password);
                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Vnpt();
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode.StartsWith("M"))     //  MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    var xml = ReadData_Vnpt();
                    StartAutoInputTokenPassword();
                    string resultM = PublishInvWithToken_Dll(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                    result = resultM;
                    //"OK:mẫu số;ký hiệu-Fkey_Số hóa đơn,"
                    //"OK:01GTKT0/001;VT/19E-A0283806HDA_XXX"
                    if (resultM.StartsWith("ERR:0"))       // Hoặc đã có trên hệ thống HDDT ERR:0
                    {
                        V6Return v6return;
                        string invXml = DownloadInvFkeyNoPay(fkeyA, out v6return);
                        paras.Result.InvoiceNo = v6return.SO_HD;
                        if (!string.IsNullOrEmpty(paras.Result.InvoiceNo))
                        {
                            result = "OK-Đã tồn tại fkey. " + resultM;
                            paras.Result.ResultString = result;
                        }
                    }
                    else // chạy lần 2
                    {
                        StartAutoInputTokenPassword();
                        resultM = PublishInvWithToken_Dll(xml, __pattern, __serial, out paras.Result.V6ReturnValues);
                        result = resultM;
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
                    string invXml = DownloadInvFkeyNoPay(fkeyA, out paras.Result.V6ReturnValues);
                    result += paras.Result.InvoiceNo;
                }
                else if (paras.Mode == "S")
                {
                    var xml = ReadDataS_Vnpt();
                    result = vnptWS.adjustInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
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
                    result = vnptWS.replaceInv(xml, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    paras.Result.ResultString = result;
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        vnptWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        vnptWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        vnptWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    //File.Create(flagFileName1).Close();
                    result = cancelInv_VNPT_TOKEN(fkey_old: paras.Fkey_hd);
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
                        string export_file;
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
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
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
        /// <param name="pattern">Mấu số 01GTKT0/001</param>
        /// <param name="serial">Ký hiệu VT/19E</param>
        /// <param name="v6Return">Kết quả</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + “,”</returns>
        public static string PublishInvWithToken_Dll(string xmlInvData, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = VNPTEInvoiceSignToken.PublishInvWithToken(_account, _accountpassword, xmlInvData, _username, _password, _SERIAL_CERT, pattern, serial, _link_Publish_vnpt_thaison);
                v6Return.RESULT_STRING = result;
                if (result.StartsWith("OK"))
                {
                    //"OK:mẫu số;ký hiệu-Fkey_Số hóa đơn,"
                    //"OK:01GTKT0/001;VT/19E-A0283806HDA_XXX"
                    int index = result.IndexOf('_');
                    string so_hd = result.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                }
                else if (result.StartsWith("ERR:-3"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi trong quá trình lấy chứng thư.";
                }
                else if (result.StartsWith("ERR:-2"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư không có privatekey.";
                }
                else if (result.StartsWith("ERR:-1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nẤn nút hủy khi nhập mã pin của chứng thư.";
                }
                else if (result.StartsWith("ERR:30"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTạo mới lô hóa đơn lỗi (fkey trùng,…).";
                }
                else if (result.StartsWith("ERR:28"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nThông tin chứng thư chưa có trong hệ thống.";
                }
                else if (result.StartsWith("ERR:27"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư chưa đến thời điểm sử dụng.";
                }
                else if (result.StartsWith("ERR:26"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư đã hết hạn.";
                }
                else if (result.StartsWith("ERR:24"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư truyền lên không đúng với chứng thư công ty đăng ký trên hệ thống";
                }
                else if (result.StartsWith("ERR:23"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nChứng thư truyền lên không đúng định dạng.";
                }
                else if (result.StartsWith("ERR:22"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCông ty chưa đăng ký thông tin keystore.";
                }
                else if (result.StartsWith("ERR:21"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy công ty trên hệ thống.";
                }
                else if (result.StartsWith("ERR:20"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTham số mẫu số và ký hiệu truyền vào không hợp lệ.";
                }
                else if (result.StartsWith("ERR:19"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\npattern truyền vào không giống với pattern của hoá đơn cần điều chỉnh/thay thế.";
                }
                else if (result.StartsWith("ERR:10"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nSố lượng hóa đơn truyền vào lớn hơn maxBlockInv.";
                }
                else if (result.StartsWith("ERR:9"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\n???.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHoá đơn đã được điều chỉnh, thay thế.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy chứng thư trong máy. Hãy cắm token.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông còn đủ số hóa đơn cho lô phát hành.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi xảy ra.";
                }
                else if (result.StartsWith("ERR:4"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\ntoken hóa đơn sai định dạng.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nĐịnh dạng file xml hóa đơn không đúng.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tồn tại hoá đơn cần thay thế/điều chỉnh.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông có quyền truy cập webservice.";
                }
                else if (result.StartsWith("ERR:0"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nLỗi Fkey đã tồn tại.";
                }
                else
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "???";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }
            Logger.WriteToLog("Program.PublishInvWithToken " + result);
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
            if (_version == "V2") return EXECUTE_VIETTEL_V2CALL(paras);

            string result = "";
            paras.Result = new PM_Result();
            V6Return rd = new V6Return();
            paras.Result.V6ReturnValues = rd;

            try
            {
                string jsonBody = "";
                //var _V6Http = new ViettelWS(_baseUrl, _username, _password);
                ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);

                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Viettel(paras);
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode == "E_G1") // Gạch nợ
                {
                    rd.RESULT_ERROR_MESSAGE = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1") // Hủy hóa đơn
                {
                    
                    DataRow row0 = am_table.Rows[0];
                    var item = generalInvoiceInfoConfig["invoiceIssuedDate"];
                    string strIssueDate = ((DateTime)GetValue(row0, item)).ToString("yyyyMMddHHmmss");
                    string additionalReferenceDesc = paras.AM_new["STT_REC"].ToString();
                    paras.InvoiceNo = paras.AM_new["SO_SERI"].ToString().Trim() + paras.AM_new["SO_CT"].ToString().Trim();
                    result = viettel_ws.CancelTransactionInvoice(_codetax, paras.InvoiceNo, strIssueDate, additionalReferenceDesc, strIssueDate);
                    
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Viettel(paras);
                    result = viettel_ws.POST_REPLACE(_createInvoiceUrl, jsonBody);
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

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBody = ReadData_Viettel(paras);
                        if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                        {
                            result = viettel_ws.POST_DRAFT(_codetax, jsonBody);
                        }
                        else
                        {
                            result = viettel_ws.POST_NEW(jsonBody);
                        }
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBody = ReadData_Viettel(paras);
                        string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        result = viettel_ws.CreateInvoiceUsbTokenGetHash_Sign(jsonBody, templateCode, _SERIAL_CERT);
                    }
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
                    result = viettel_ws.POST_EDIT(jsonBody);
                }

                //Phân tích result
                paras.Result.V6ReturnValues.RESULT_STRING = result;
                string message = "";
                try
                {
                    CreateInvoiceResponse responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponse>(result);
                    paras.Result.V6ReturnValues.RESULT_OBJECT = responseObject;
                    if (!string.IsNullOrEmpty(responseObject.description))
                    {
                        message += " " + responseObject.description;
                    }

                    if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                    {
                        paras.Result.V6ReturnValues.SO_HD = responseObject.result.invoiceNo;
                        paras.Result.V6ReturnValues.ID = responseObject.result.transactionID;
                        paras.Result.V6ReturnValues.SECRET_CODE = responseObject.result.reservationCode;
                        message += " " + responseObject.result.invoiceNo;
                        
                    }
                    else if (responseObject.errorCode == null)
                    {
                        paras.Result.V6ReturnValues.SO_HD = paras.InvoiceNo;
                        paras.Result.V6ReturnValues.RESULT_MESSAGE = responseObject.description;
                    }
                    else
                    {
                        paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = responseObject.errorCode + ":" + responseObject.description;
                    }
                }
                catch (Exception ex)
                {
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "CONVERT EXCEPTION: " +  ex.Message;
                    Logger.WriteToLog("EXECUTE_VIETTEL ConverResultObjectException: " + paras.Fkey_hd + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
            }
            catch (Exception ex)
            {
                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.EXECUTE_VIETTEL", ex);
            }

            return result;
        }

        
        private static string EXECUTE_VIETTEL_V2CALL(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();
            V6Return rd = new V6Return();
            paras.Result.V6ReturnValues = rd;

            try
            {
                string jsonBody = "";
                //ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax); // Thay thế WS bằng call Process
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = "ViettelV2",
                        FileName = "ViettelV2\\V6ThuePostViettelV2.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                if (paras.Mode == "TestView")
                {
                    var xml = ReadData_Viettel(paras);
                    result = xml;
                    paras.Result.ResultString = xml;
                }
                else if (paras.Mode == "E_G1") // Gạch nợ
                {
                    rd.RESULT_ERROR_MESSAGE = V6Text.NotSupported;
                }
                else if (paras.Mode == "E_H1") // Hủy hóa đơn
                {
                    // V6ThuePostViettelV2.exe H_JSON "V6ThuePost.xml" "AB/19E0000341" "27/11/2019" "stt_rec"
                    DataRow row0 = am_table.Rows[0];
                    var item = generalInvoiceInfoConfig["invoiceIssuedDate"];
                    string strIssueDate = ((DateTime)GetValue(row0, item)).ToString("yyyyMMddHHmmss");
                    string additionalReferenceDesc = paras.AM_new["STT_REC"].ToString();
                    paras.InvoiceNo = paras.AM_new["SO_SERI"].ToString().Trim() + paras.AM_new["SO_CT"].ToString().Trim();
                    //result = viettel_ws.CancelTransactionInvoice(_codetax, paras.InvoiceNo, strIssueDate, additionalReferenceDesc, strIssueDate);
                    //V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", string.Format("{0};{1};{2};{3};{4}", _codetax, paras.InvoiceNo, strIssueDate, additionalReferenceDesc, strIssueDate));
                    process.StartInfo.Arguments = string.Format("H_JSON V6ThuePost.xml {0} {1} {2}",
                        paras.InvoiceNo, ObjectAndString.ObjectToString((DateTime)GetValue(row0, item), "dd/MM/yyyy"), additionalReferenceDesc);
                    process.Start();
                    string process_result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    // Phân tích Result tại đây.
                    paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                    result = paras.Result.V6ReturnValues.RESULT_STRING;
                    CreateInvoiceResponseV2 resultObject = JsonConvert.DeserializeObject<V6ThuePost.ViettelV2Objects.CreateInvoiceResponseV2>(result);
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = resultObject.errorCode + resultObject.message;
                }
                else if (paras.Mode == "E_T1")
                {
                    jsonBody = ReadData_Viettel(paras);
                    V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody);
                    process.StartInfo.Arguments = "E_T1_JSON V6ThuePost.xml tprint_soa.json";
                    process.Start();
                    result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    //result = viettel_ws.POST_REPLACE(_createInvoiceUrl, jsonBody);
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    generalInvoiceInfoConfig["adjustmentType"] = new ConfigLine
                    {
                        Field = "adjustmentType",
                        Value = "1",
                    };
                    
                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBody = ReadData_Viettel(paras);
                        V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody);

                        if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                        {
                            process.StartInfo.Arguments = "M_F4_JSON V6ThuePost.xml tprint_soa.json";
                        }
                        else
                        {
                            process.StartInfo.Arguments = "M_JSON V6ThuePost.xml tprint_soa.json";
                        }

                        process.Start();
                        string process_result = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                        // Phân tích Result tại đây.
                        paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                        result = paras.Result.V6ReturnValues.RESULT_STRING;
                        //paras.Form.ShowInfoMessage(result);
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBody = ReadData_Viettel(paras);
                        string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        //result = viettel_ws.CreateInvoiceUsbTokenGetHash_Sign(jsonBody, templateCode, _SERIAL_CERT);
                        V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody + "<;>" + templateCode);
                        process.StartInfo.Arguments = "M_TOKEN_JSON V6ThuePost.xml tprint_soa.json";
                        process.Start();
                        string process_result = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                        // Phân tích Result tại đây.
                        paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                        result = paras.Result.V6ReturnValues.RESULT_STRING;
                    }
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
                    V6BusinessHelper.WriteTextFile("ViettelV2\\tprint_soa.json", jsonBody);
                    process.StartInfo.Arguments = "S_JSON V6ThuePost.xml tprint_soa.json";
                    process.Start();
                    string process_result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    // Phân tích Result tại đây.
                    paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                    result = paras.Result.V6ReturnValues.RESULT_STRING;
                }

                //Phân tích result
                paras.Result.V6ReturnValues.RESULT_STRING = result;
                string message = "";
                try
                {
                    CreateInvoiceResponseV2 responseObject = JsonConvert.DeserializeObject<CreateInvoiceResponseV2>(result);
                    paras.Result.V6ReturnValues.RESULT_OBJECT = responseObject;
                    if (!string.IsNullOrEmpty(responseObject.description))
                    {
                        message += " " + responseObject.description;
                    }

                    if (responseObject.result != null && !string.IsNullOrEmpty(responseObject.result.invoiceNo))
                    {
                        paras.Result.V6ReturnValues.SO_HD = responseObject.result.invoiceNo;
                        paras.Result.V6ReturnValues.ID = responseObject.result.transactionID;
                        paras.Result.V6ReturnValues.SECRET_CODE = responseObject.result.reservationCode;
                        message += " " + responseObject.result.invoiceNo;
                        
                    }
                    else if (responseObject.code == 400)
                    {
                        paras.Result.V6ReturnValues.SO_HD = paras.InvoiceNo;
                        paras.Result.V6ReturnValues.RESULT_MESSAGE = result;
                    }
                    else
                    {
                        paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = responseObject.errorCode + ":" + responseObject.description;
                        if (string.IsNullOrEmpty(responseObject.errorCode + "" + responseObject.description) && string.IsNullOrEmpty(paras.Result.V6ReturnValues.SO_HD))
                        {
                            paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "Không có số Hóa Đơn phản hồi.";
                        }
                        else
                        {
                            paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "Lỗi.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "CONVERT EXCEPTION: " +  ex.Message;
                    Logger.WriteToLog("EXECUTE_VIETTEL ConverResultObjectException: " + paras.Fkey_hd + ex.Message);
                    message = "Kết quả:";
                }
                result = message + "\n" + result;
            }
            catch (Exception ex)
            {
                paras.Result.V6ReturnValues.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                V6ControlFormHelper.WriteExLog("PostManager.EXECUTE_VIETTEL", ex);
            }

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
                    postObject.generalInvoiceInfo["originalInvoiceId"] =  paras.Fkey_hd_tt;// .AM_old["FKEY_HD_TT"].ToString().Trim();  // AA/17E0003470
                    //originalInvoiceIssueDate
                    postObject.generalInvoiceInfo["originalInvoiceIssueDate"] = paras.AM_old["NGAY_CT"];

                    //Thông tin về biên bản đính kèm hóa đơn gốc:
                    //additionalReferenceDate
                    postObject.generalInvoiceInfo["additionalReferenceDate"] = paras.AM_old["NGAY_CT"];
                    //additionalReferenceDesc
                    postObject.generalInvoiceInfo["additionalReferenceDesc"] = paras.AM_new["GHI_CHU_TT"];
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
                if (metadataConfig != null)
                {
                    foreach (KeyValuePair<string, ConfigLine> metaItem in metadataConfig)
                    {
                        Dictionary<string, object> metadata = new Dictionary<string, object>();
                        metadata["invoiceCustomFieldId"] = ObjectAndString.ObjectToInt(metaItem.Value.SL_TD1);
                        metadata["keyTag"] = metaItem.Key;
                        metadata["valueType"] = metaItem.Value.DataType; // text, number, date
                        if (metaItem.Value.DataType.ToLower() == "date")
                        {
                            metadata["dateValue"] = GetValue(row0, metaItem.Value);
                        }
                        else if (metaItem.Value.DataType.ToLower() == "number")
                        {
                            metadata["numberValue"] = GetValue(row0, metaItem.Value);
                        }
                        else // if (metaItem.Value.DataType.ToLower() == "date")
                        {
                            metaItem.Value.DataType = "text";
                            metadata["stringValue"] = ObjectAndString.ObjectToString(GetValue(row0, metaItem.Value));
                        }
                        metadata["keyLabel"] = ObjectAndString.ObjectToString(metaItem.Value.MA_TD2);
                        metadata["isRequired"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD2);
                        metadata["isSeller"] = ObjectAndString.ObjectToBool(metaItem.Value.SL_TD3);

   //{
   //   "invoiceCustomFieldId": 1135,
   //   "keyTag": "dueDate",
   //   "valueType": "date",
   //   "dateValue": 1544115600000,
   //   "keyLabel": "Hạn thanh toán",
   //   "isRequired": false,
   //   "isSeller": false
   // },
                        postObject.metadata.Add(metadata);
                    }
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

                if (string.IsNullOrEmpty(_datetype)) _datetype = "VIETTEL";
                result = postObject.ToJson(_datetype);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("PostManager.ReadData", ex);
            }
            return result;
        }
        
        /// <summary>
        /// Trả về đường dẫn file pdf.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns>Trả về đường dẫn file pdf.</returns>
        public static string ViettelDownloadInvoicePDF(PostManagerParams paras)
        {
            if (_version == "V2")
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = "ViettelV2",
                        FileName = "ViettelV2\\V6ThuePostViettelV2.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                string soseri_soct = paras.Serial + paras.InvoiceNo;//"XL/20E0000019"
                string templateCode = paras.Pattern; // "01GTKT0/001" 
                string uid = paras.Fkey_hd;//"bf0a819a-dd5f-4446-8850-bc81263beb04"
                
                if (paras.Mode == "1") // Thể hiện V6ThuePostViettelV2.exe P "V6ThuePost.xml" "XL/20E0000019" "01GTKT0/001" "bf0a819a-dd5f-4446-8850-bc81263beb04"
                {
                    process.StartInfo.Arguments = string.Format("P_JSON V6ThuePost.xml \"{0}\" \"{1}\" \"{2}\"", soseri_soct, templateCode, uid);
                }
                else // V6ThuePostViettelV2.exe P2 "V6ThuePost.xml" "XL/20E0000019" "template" "uid"
                {
                    process.StartInfo.Arguments = string.Format("P2_JSON V6ThuePost.xml \"{0}\" \"{1}\" \"{2}\"", soseri_soct, templateCode, uid);
                }

                process.Start();
                string process_result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                // Phân tích Result tại đây.
                paras.Result.V6ReturnValues = GetV6ReturnFromCallExe(process_result);
                return paras.Result.V6ReturnValues.PATH;
                return process_result;
            }
            else
            {
                ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);

                if (paras.Mode == "1") // Mode Thể hiện
                    return viettel_ws.DownloadInvoicePDF(_codetax, _downloadlinkpdf, paras.InvoiceNo, paras.Pattern,
                        V6Setting.V6SoftLocalAppData_Directory);
                string strIssueDate = paras.InvoiceDate.ToString("yyyyMMddHHmmss"); // V1 dùng không thống nhất ???
                return viettel_ws.DownloadInvoicePDFexchange(_codetax, _downloadlinkpdfe, paras.InvoiceNo, strIssueDate,
                    V6Setting.V6SoftLocalAppData_Directory);
            }
        }
        
        #endregion viettel


        #region ==== SOFTDREAMS ====
        /// <summary>
        /// Copy từ Vnpt, sửa từ từ.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static string EXECUTE_SOFTDREAMS(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                SoftDreamsWS softDreamsWS = new SoftDreamsWS(_baseUrl, _username, _password, _SERIAL_CERT);
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    var invoices = ReadData_SoftDreams(paras.Mode);
                    result = invoices.ToXml();
                    paras.Result.ResultString = result;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultErrorMessage = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = softDreamsWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ // !!!!! SoftDreams không có hàm.
                    {
                        result = softDreamsWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        result = softDreamsWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        result = softDreamsWS.CancelInvoice(paras.Fkey_hd, paras.Pattern, paras.Serial);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var invoices = ReadData_SoftDreams(paras.Mode);
                        foreach (Inv inv in invoices.Inv)
                        {
                            var adj = inv.ToAdjustInv();
                            result += softDreamsWS.AdjustInvoice(adj, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var invoices = ReadData_SoftDreams(paras.Mode);
                        var inv = invoices.Inv[0].ToReplaceInv();
                        result = softDreamsWS.ReplaceInvoice(inv, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode.StartsWith("M")) // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    Invoices invoices = ReadData_SoftDreams(paras.Mode.Substring(0, 1));
                    bool issue = true;
                    if (paras.Key_Down == "F4" || paras.Key_Down == "F6")
                    {
                        issue = false;
                        result = softDreamsWS.ImportInvoicesNoIssue(invoices, __pattern, __serial, _signmode, out paras.Result.V6ReturnValues);
                    }
                    else
                    {
                        StartAutoInputTokenPassword();
                        result = softDreamsWS.ImportInvoices(invoices, __pattern, __serial, issue, _signmode, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = paras.Fkey_hd;

                    string invXml = softDreamsWS.DownloadInvFkeyNoPay(fkeyA);
                    paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    //WriteFlag(flagFileName4, so_hoa_don);
                    result += paras.Result.InvoiceNo;
                    //result += invXml;
                }
                else if (paras.Mode == "S")
                {
                    var invoices = ReadData_SoftDreams(paras.Mode);
                    foreach (Inv inv in invoices.Inv)
                    {
                        var adj = inv.ToAdjustInv();
                        result += softDreamsWS.AdjustInvoice(adj, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                    }
                    
                    //string filePath = Path.Combine(paras.Dir, paras.FileName);
                    //if (filePath.Length > 0 && result.StartsWith("OK"))
                    //{
                    //    if (File.Exists(filePath))
                    //    {
                    //        result += UploadInvAttachmentFkey(fkeyA, filePath);
                    //    }
                    //    else
                    //    {
                    //        result += "Không tồn tại " + filePath;
                    //    }
                    //}
                }
                else if (paras.Mode == "T")
                {
                    var invoices = ReadData_SoftDreams(paras.Mode);
                    var inv = invoices.Inv[0].ToReplaceInv();
                    result = softDreamsWS.ReplaceInvoice(inv, paras.Fkey_hd, paras.Pattern, paras.Serial, true, _signmode, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = softDreamsWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = softDreamsWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = softDreamsWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    result = softDreamsWS.CancelInvoice(paras.Fkey_hd, paras.Pattern, paras.Serial);
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
                        string export_file;
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
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    //error += result;
                    paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return result;
        }

        #endregion ==== SOFTDREAMS ====


        #region ==== THAI_SON ====
        /// <summary>
        /// Copy từ SoftDreams - Vnpt, sửa từ từ.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static string EXECUTE_THAI_SON(PostManagerParams paras)
        {
            string result = "";
            paras.Result = new PM_Result();

            try
            {
                ThaiSonWS thaiSonWS = new ThaiSonWS(_baseUrl, _link_Publish_vnpt_thaison, _username, _password, _SERIAL_CERT);
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    var invoices = ReadData_ThaiSon(paras.Mode);
                    result = V6XmlConverter.ClassToXml(invoices);
                    paras.Result.ResultString = result;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    if (paras.Mode == "E_G1") // Gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        if (string.IsNullOrEmpty(paras.Fkey_hd))
                        {
                            paras.Result.ResultErrorMessage = "Không có Fkey_hd truyền vào.";
                        }
                        else
                        {
                            result = thaiSonWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                        }
                    }
                    else if (paras.Mode == "E_G2") // Gạch nợ // !!!!! SoftDreams không có hàm.
                    {
                        result = thaiSonWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_G3") // Hủy gạch nợ theo fkey !!!!! SoftDreams không có hàm.
                    {
                        result = thaiSonWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_H1")
                    {
                        var hoadon_entity = ReadData_ThaiSon("H");
                        result = thaiSonWS.CancelInvoice((HoaDonHuyEntity)hoadon_entity, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_S1")
                    {
                        var invoice = (HoaDonEntity)ReadData_ThaiSon("S");
                        invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                        result += thaiSonWS.AdjustInvoice(invoice, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "E_T1")
                    {
                        var invoice = (HoaDonEntity)ReadData_ThaiSon("T");
                        invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                        result = thaiSonWS.ReplaceInvoice(invoice, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode.StartsWith("M")) // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    //Invoices invoices = ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                    //bool issue = true;
                    //if (paras.Key_Down == "F4" || paras.Key_Down == "F6") issue = false;
                    //StartAutoInputTokenPassword();
                    //result = thaiSonWS.ImportInvoices(invoices, __pattern, __serial, issue, _signmode, out paras.Result.V6ReturnValues);

                    var hoadon_entity = ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                    //File.Create(flagFileName1).Close();
                    result = thaiSonWS.XuatHoaDonDienTu((HoaDonEntity)hoadon_entity, out paras.Result.V6ReturnValues);
                    //result = XuatHoaDonDienTu_XML(xml);

                    if (result.StartsWith("OK"))
                    {
                        string filePath = Path.Combine(paras.Dir, paras.FileName);
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
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    fkeyA = paras.Fkey_hd;

                    string invXml = thaiSonWS.DownloadInvPDFFkeyNoPay(fkeyA);
                    paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    //WriteFlag(flagFileName4, so_hoa_don);
                    result += paras.Result.InvoiceNo;
                    //result += invXml;
                }
                else if (paras.Mode == "S")
                {
                    var invoice = (HoaDonEntity)ReadData_ThaiSon("S");
                    invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    result += thaiSonWS.AdjustInvoice(invoice, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "T")
                {
                    var invoice = (HoaDonEntity)ReadData_ThaiSon("T");
                    invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    result = thaiSonWS.ReplaceInvoice(invoice, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    if (paras.Mode == "G1") // Gạch nợ theo fkey
                    {
                        result = thaiSonWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    {
                        result = thaiSonWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                    else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    {
                        result = thaiSonWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "H")
                {
                    var hoadon_entity = ReadData_ThaiSon(paras.Mode.Substring(0, 1));
                    result = thaiSonWS.CancelInvoice((HoaDonHuyEntity)hoadon_entity, out paras.Result.V6ReturnValues);
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
                        string export_file;
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
                else
                {
                    result = "ERR: Mode " + paras.Mode + " " + V6Text.NotSupported;
                }


                if (result.StartsWith("ERR"))
                {
                    //error += result;
                    paras.Result.ResultErrorMessage = result;
                }
                else
                {
                    //File.Create(flagFileName2).Close();
                }
            }
            catch (Exception ex)
            {
                result += "ERR:EX\r\n" + ex.Message;
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return result;
        }

        #endregion ==== THAI_SON ====

        #region ==== MONET ====

        /// <summary>
        /// Copy từ Thái sơn - SoftDreams - Vnpt, sửa từ từ.
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static string EXECUTE_MONET(PostManagerParams paras)
        {
            MONET_API_Response response = new MONET_API_Response();
            paras.Result = new PM_Result();

            try
            {
                MONET_WS monetWS = new MONET_WS(_baseUrl, _username, _password, _codetax);
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    var invoiceString = ReadData_Monet(paras.Mode);
                    paras.Result.ResultString = invoiceString;
                }
                else if (paras.Mode.StartsWith("E_"))
                {
                    
                }
                else if (paras.Mode.StartsWith("M")) // MSHDT//Mới Sửa Hủy ĐiềuChỉnh(S) ThayThế
                {
                    var json = ReadData_Monet(paras.Mode.Substring(0, 1));
                    response = monetWS.POST_NEW(_link_Publish_vnpt_thaison, json, out paras.Result.V6ReturnValues);
                    
                    if (response.isSuccess)
                    {
                        string filePath = Path.Combine(paras.Dir, paras.FileName);
                        if (paras.Mode.EndsWith("1"))//Gửi file excel có sẵn
                        {
                            
                        }
                    }
                }
                else if (paras.Mode.ToLower() == "DownloadInvFkeyNoPay".ToLower())
                {
                    //fkeyA = paras.Fkey_hd;

                    //string invXml = monetWS.DownloadInvPDFFkeyNoPay(fkeyA);
                    //paras.Result.InvoiceNo = GetSoHoaDon_VNPT(invXml);
                    ////WriteFlag(flagFileName4, so_hoa_don);
                    //response += paras.Result.InvoiceNo;
                    ////result += invXml;
                }
                else if (paras.Mode == "S")
                {
                    var invoice = ReadData_Monet("S");
                    //invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    response = monetWS.POST_EDIT(_modifylink, "1", paras.Fkey_hd_tt, __serial, __pattern, paras.InvoiceNo, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "T")
                {
                    var invoice = ReadData_Monet("T");
                    //invoice.SoHoaDonGoc = paras.Fkey_hd_tt;
                    response = monetWS.POST_EDIT(_modifylink, "4", paras.Fkey_hd_tt, __serial, __pattern, paras.InvoiceNo, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    //if (paras.Mode == "G1") // Gạch nợ theo fkey
                    //{
                    //    response = monetWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    //{
                    //    response = monetWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    //{
                    //    response = monetWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                }
                else if (paras.Mode == "H")
                {
                    var hoadon_entity = ReadData_Monet(paras.Mode.Substring(0, 1));
                    response = monetWS.POST_DELETE(_SERIAL_CERT,paras.InvoiceNo, paras.Serial, __pattern, paras.Fkey_hd_tt, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        string response0 = "";
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref response0);
                        if (export_ok && File.Exists(export_file))
                        {
                            response.isSuccess = true;// += "\r\nExport ok.";
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
                            response.isSuccess = true;// += "\r\nExport ok.";
                        }
                        else
                        {
                            response.isSuccess = false;// += "\r\nExport fail.";
                        }
                    }
                }

                if (response.isSuccess)
                {

                }
                else
                //if (response.StartsWith("ERR"))
                {
                    //error += result;
                    paras.Result.ResultErrorMessage += "ERR";
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return paras.Result.V6ReturnValues.RESULT_STRING;
        }

        public static string ReadData_Monet(string mode)
        {
            string result = "";
            
            var postObject = new Dictionary<string, object>();
            
            //Fill data to postObject
            DataRow row0 = am_table.Rows[0];

            fkeyA = "" + row0["FKEY_HD"];
            __pattern = row0[pattern_field].ToString().Trim();
            __serial = row0[seri_field].ToString().Trim();
            //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
            foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }

            if (mode == "T")
            {
                //Lập hóa đơn thay thế:
                //adjustmentType = ‘3’
                //postObject["adjustmentType"] = "3";
            }

            //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
            foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }

            foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }

            //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
            foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
            {
                postObject[item.Key] = GetValue(row0, item.Value);
            }


            var items = new List<Dictionary<string, object>>();
            postObject["items"] = items;
            foreach (DataRow row in ad_table.Rows)
            {
                if (row["STT"].ToString() == "0") continue;
                Dictionary<string, object> rowData = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                {
                    rowData[item.Key] = GetValue(row, item.Value);
                }

                items.Add(rowData);
            }

            result = V6JsonConverter.ObjectToJson(postObject, "yyyy-MM-dd");
            
            return result;
        }

        #endregion

        #region ==== MINVOICE ====

        private static string EXECUTE_MINVOICE(PostManagerParams paras)
        {
            MInvoiceResponse response = new MInvoiceResponse();
            paras.Result = new PM_Result();

            try
            {
                MInvoiceWS mInvoiceWs = new MInvoiceWS(_baseUrl, _username, _password, _ma_dvcs, _codetax);
                MInvoicePostObject jsonBodyObject = null;
                var row0 = am_table.Rows[0];

                if (paras.Mode == "TestView")
                {
                    jsonBodyObject = ReadData_Minvoice(paras.Mode);
                    paras.Result.ResultString = jsonBodyObject.ToJson();
                }
                else if (paras.Mode.StartsWith("M"))
                {
                    StartAutoInputTokenPassword();

                    if (string.IsNullOrEmpty(_SERIAL_CERT))
                    {
                        jsonBodyObject = ReadData_Minvoice("M");
                        //File.Create(flagFileName1).Close();
                        response = mInvoiceWs.POST_NEW(jsonBodyObject, out paras.Result.V6ReturnValues);
                    }
                    else // Ký số client. /InvoiceAPI/InvoiceWS/createInvoiceUsbTokenGetHash/{supplierTaxCode}
                    {
                        generalInvoiceInfoConfig["certificateSerial"] = new ConfigLine
                        {
                            Field = "certificateSerial",
                            Value = _SERIAL_CERT,
                        };
                        jsonBodyObject = ReadData_Minvoice("M");
                        //string templateCode = generalInvoiceInfoConfig["templateCode"].Value;
                        response = mInvoiceWs.POST_NEW_TOKEN(jsonBodyObject, out paras.Result.V6ReturnValues);
                    }
                }
                else if (paras.Mode == "S")
                {
                    jsonBodyObject = ReadData_Minvoice("S");
                    response = mInvoiceWs.POST_EDIT(jsonBodyObject, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "T")
                {
                    jsonBodyObject = ReadData_Minvoice("T");
                    response = mInvoiceWs.POST_EDIT(jsonBodyObject, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("G"))
                {
                    //if (paras.Mode == "G1") // Gạch nợ theo fkey
                    //{
                    //    response = monetWS.ConfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G2") // Gạch nợ theo lstInvToken(01GTKT2/001;AA/13E;10)
                    //{
                    //    response = monetWS.ConfirmPayment(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                    //else if (paras.Mode == "G3") // Hủy gạch nợ theo fkey
                    //{
                    //    response = monetWS.UnconfirmPaymentFkey(paras.Fkey_hd, out paras.Result.V6ReturnValues);
                    //}
                }
                else if (paras.Mode == "E_H1")
                {
                    response = mInvoiceWs.POST_CANCEL(paras.V6PartnerID, "", paras.InvoiceDate, "", out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode == "H")
                {
                    var hoadon_entity = ReadData_Minvoice(paras.Mode.Substring(0, 1));
                    //response = mInvoiceWs.POST_DELETE(_SERIAL_CERT,paras.InvoiceNo, paras.Serial, __pattern, paras.Fkey_hd_tt, paras.Fkey_hd, out paras.Result.V6ReturnValues);
                }
                else if (paras.Mode.StartsWith("U"))//U1,U2
                {
                    
                }
                else if (paras.Mode.StartsWith("E"))
                {
                    if (paras.Mode == "E")
                    {

                    }
                    else if (paras.Mode == "E1")
                    {
                        string export_file;
                        string response0 = "";
                        bool export_ok = ExportExcel(am_table, ad2_table, out export_file, ref response0);
                        if (export_ok && File.Exists(export_file))
                        {
                            response.ok = "1";
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

                        }
                    }
                }

                if (ObjectAndString.ObjectToBool(response.ok))
                {

                }
                else
                {
                    paras.Result.ResultErrorMessage = "ERR" + paras.Result.ResultErrorMessage;
                }
            }
            catch (Exception ex)
            {
                paras.Result.ResultErrorMessage = ex.Message;
            }
            StopAutoInputTokenPassword();
        End:
            return paras.Result.V6ReturnValues.RESULT_STRING;
        }

        public static MInvoicePostObject ReadData_Minvoice(string mode)
        {
            string result = "";
            MInvoicePostObject postObject = null;
            //try
            {
                postObject = new MInvoicePostObject();
                postObject.windowid = "WIN00187";
                if (mode.StartsWith("M")) postObject.editmode = "1";
                if (mode.StartsWith("S")) postObject.editmode = "2";
                postObject.data = new List<InvoiceData>();
                InvoiceData invoiceData = new InvoiceData();
                postObject.data.Add(invoiceData);
                
                //Fill data to postObject
                DataRow row0 = am_table.Rows[0];

                fkeyA = "" + row0["FKEY_HD"];
                //MakeFlagNames(fkeyA);
                //private static Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }

                if (mode.StartsWith("S"))
                {
                    invoiceData["inv_invoiceNumber"] = row0["SO_CT"];
                }

                if (mode == "T")
                {
                    //Lập hóa đơn thay thế:
                    //adjustmentType = '3'
                    invoiceData["adjustmentType"] = "3";
                    //Các trường dữ liệu về hóa đơn gốc là bắt buộc
                    //originalInvoiceId
                    invoiceData["originalInvoiceId"] = row0["FKEY_TT_OLD"].ToString().Trim();  // [AA/17E0003470]
                    //originalInvoiceIssueDate
                    invoiceData["originalInvoiceIssueDate"] = row0["NGAY_CT_OLD"];

                    //Thông tin về biên bản đính kèm hóa đơn gốc:
                    //additionalReferenceDate
                    invoiceData["additionalReferenceDate"] = row0["NGAY_CT_OLD"];
                    //additionalReferenceDesc
                    invoiceData["additionalReferenceDesc"] = row0["GHI_CHU03"];
                }

                //private static Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }

                List<DetailObject> listDetailObject = new List<DetailObject>();
                DetailObject detailObject = new DetailObject();
                listDetailObject.Add(detailObject);
                detailObject.tab_id = "TAB00188";
                invoiceData["details"] = listDetailObject;

                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["STT"].ToString() == "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    detailObject.data.Add(rowData);
                }
                //private static Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    invoiceData[item.Key] = GetValue(row0, item.Value);
                }

                result = postObject.ToJson();
            }
            //catch (Exception ex)
            {
                //
            }

            return postObject;
        }

        #endregion MINVOICE


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
                        var fields = ObjectAndString.SplitStringBy(config.FieldV6.Replace("\\+", "~plus~"), '+');

                        string fieldValueString = "";

                        foreach (string s in fields)
                        {
                            string field = s.Trim();
                            if (table.Columns.Contains(field))
                            {
                                fieldValueString += ObjectAndString.ObjectToString(row[field]).Trim();
                            }
                            else
                            {
                                //if (field.StartsWith("\"") && field.EndsWith("\""))
                                //{
                                //    field = field.Substring(1, field.Length - 2);
                                //}
                                fieldValueString += field;
                            }
                        }
                        // Chốt.
                        fieldValue = fieldValueString.Replace("~plus~", "+");
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
                    case "NUMBER":
                        return ObjectAndString.ObjectToDecimal(fieldValue);
                    case "INT":
                        return ObjectAndString.ObjectToInt(fieldValue);
                    case "INT64":
                    case "LONG":
                        return ObjectAndString.ObjectToInt64(fieldValue);
                    //case "UPPER": // Chỉ dùng ở exe gọi bằng Foxpro.
                    //    return (fieldValue + "").ToUpper();
                    case "INTSTRING": // Đưa kiểu số về chuỗi nguyên (không lấy phần thập phân).
                        return ObjectAndString.ObjectToInt(fieldValue).ToString();
                    case "STRING":
                        return "" + fieldValue;
                    default:    // Kiểu nguyên mẫu của dữ liệu.
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
        /// <param name="mapTable"></param>
        public static void ReadConfigInfo(DataTable mapTable)
        {
            V6Infos = new Dictionary<string, string>();
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            metadataConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();
            customerInfoConfig = new Dictionary<string, ConfigLine>();

            parameters_config = new List<ConfigLine>();

            try
            {
                foreach (DataRow row in mapTable.Rows)
                {
                    string GROUP_NAME = row["GroupName"].ToString().Trim().ToUpper();
                    ConfigLine line = ReadConfigLine(row);
                    string line_field = line.Field.ToLower();
                    switch (GROUP_NAME)
                    {
                        case "V6INFO":
                        {
                            V6Infos[line.Field.ToUpper()] = line.Value;
                            switch (line_field)
                            {
                                case "username":
                                    _username = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "password":
                                    _password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "codetax":
                                    _codetax = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "version":
                                    _version = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "ma_dvcs":
                                    _ma_dvcs = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "baselink":
                                case "baseurl":
                                    _baseUrl = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "site":
                                case "login":
                                case "sitelogin":
                                case "loginsite":
                                case "website":
                                    _site = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "datetype":
                                    _datetype = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
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
                                case "downloadlinkpdfe":
                                    _downloadlinkpdfe = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                    //Vnpt, có dùng cả username, password
                                case "link_publish":
                                        _link_Publish_vnpt_thaison = UtilityHelper.DeCrypt(line.Value);
                                        _baseUrl = _link_Publish_vnpt_thaison.Substring(0, _link_Publish_vnpt_thaison.LastIndexOf('/'));
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
                                case "certificateserial":
                                    _SERIAL_CERT = UtilityHelper.DeCrypt(line.Value).ToUpper();
                                    break;
                                case "token_password_title":
                                    _token_password_title = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "token_password":
                                    _token_password = UtilityHelper.DeCrypt(line.Value);
                                    break;
                                case "pattern":
                                case "partten":
                                    pattern_field = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
                                    break;
                                case "seri":
                                case "serial":
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
                                case "signmode":
                                    _signmode = line.Type == "ENCRYPT" ? UtilityHelper.DeCrypt(line.Value) : line.Value;
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
                        case "METADATA":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    metadataConfig.Add(line.Field, line);
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
                        case "CUSTOMERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    customerInfoConfig.Add(line.Field, line);
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
                V6ControlFormHelper.WriteExLog("PostManager.ReadConfigInfo", ex);
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
            config.MA_TD2 = ObjectAndString.ObjectToString(reader["MA_TD2"]);
            config.MA_TD3 = ObjectAndString.ObjectToString(reader["MA_TD3"]);
            config.SL_TD1 = ObjectAndString.ObjectToDecimal(reader["SL_TD1"]);
            config.SL_TD2 = ObjectAndString.ObjectToDecimal(reader["SL_TD2"]);
            config.SL_TD3 = ObjectAndString.ObjectToDecimal(reader["SL_TD3"]);
            return config;
        }

        public static string SearchInvoice(PostManagerParams paras)
        {
            try
            {
                _map_table = paras.DataSet.Tables[0];
                //ad_table = paras.DataSet.Tables[1];
                //am_table = paras.DataSet.Tables[2];
                //Fkey_hd_tt = paras.Fkey_hd_tt;
                //DataRow row0 = am_table.Rows[0];
                //ad2_table = paras.DataSet.Tables[3];
                //if (paras.DataSet.Tables.Count > 4)
                //{
                //    ad3_table = paras.DataSet.Tables[4];
                //}
                //else
                //{
                //    ad3_table = null;
                //}

                ReadConfigInfo(_map_table);

                switch (paras.Branch)
                {
                    case "1":
                        ViettelWS viettel_ws = new ViettelWS(_baseUrl, _username, _password, _codetax);
                        return viettel_ws.GetListInvoiceDataControl(paras.InvoiceDate, paras.InvoiceDate);
                        break;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("PostManager.SearchInvoice", ex);
            }
            return null;
        }
    }
}

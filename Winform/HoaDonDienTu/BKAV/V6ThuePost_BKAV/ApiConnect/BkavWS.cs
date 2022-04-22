using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using BSECUS;
using Newtonsoft.Json;
using V6ThuePost.ResponseObjects;
using V6ThuePostBkavApi.PostObjects;
using V6ThuePostBkavApi.ResponseObjects;
using V6ThuePostBkavApi.vn.ehoadon.wsdemo;

namespace V6ThuePostBkavApi
{
    public class BkavWS
    {
        private string _baseUrl;
        private string BkavPartnerGUID;
        private string BkavPartnerToken;
        private RemoteCommand remoteCommand;
        public BkavWS(string baseUrl, string bkavPartnerGUID, string bkavPartnerToken)
        {
            _baseUrl = baseUrl;
            BkavPartnerGUID = bkavPartnerGUID;
            BkavPartnerToken = bkavPartnerToken;
            
            var webservice = new WSPublicEHoaDon(_baseUrl);
            uint Constants_Mode = RemoteCommand.DefaultMode;
            remoteCommand = new RemoteCommand(webservice.ExecuteCommand, BkavPartnerGUID, BkavPartnerToken, Constants_Mode);
        }

        public string POST(string jsonBody, int commandType, out V6Return v6return)
        {
            string result = null;
            //so_hd = 0;
            //guid = null;
            string msg = null;
            v6return = null;

            try
            {                
                switch (commandType)
                {
                    case BkavConst._100_CreateNew:
                    case BkavConst._101_CreateEmpty: // Tạo hóa đơn rỗng có sẵn số?, 200 sửa,
                    case BkavConst._110_CreateClient:
                    case BkavConst._111_CreateClientNo:
                    case BkavConst._112_CreateWithParternSerial: // Hóa đơn tự sinh số chưa phát hành.
                    case BkavConst._200_Update: // 200 sửa
                        msg = DoCreateInvoice(commandType, jsonBody, out v6return);
                        break;
                    case BkavConst._121_CreateAdjust:
                        msg = DoAdjustInvoice(jsonBody, out v6return);
                        break;
                    case BkavConst._120_CreateReplace:
                    case BkavConst._123_CreateReplace:
                        msg = DoReplaceInvoice(commandType, jsonBody, out v6return);
                        break;
                    case BkavConst._201_CancelInvoiceByInvoiceGUID:
                    case BkavConst._202_CancelInvoiceByPartnerInvoiceID:
                        msg = CancelInvoice(commandType, jsonBody, out v6return);
                        break;
                    case BkavConst._205_SignGUID:
                        msg = SignInvoice(jsonBody, out v6return);
                        break;
                    default:
                        msg = "V6 not supported.";
                        v6return = new V6Return();
                        break;
                }

                if (msg.Length > 0)
                {
                    result = "ERR:" + msg;
                    v6return.RESULT_ERROR_MESSAGE = result;
                }
                else if (v6return != null)
                {
                     result += v6return.RESULT_STRING;// string.Format("SO_HD:{0}; GUID:{1}", so_hd, guid);
                }
            }
            catch (Exception ex)
            {
                v6return = new V6Return();
                v6return.RESULT_STRING = ex.Message;
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                result = ex.Message;
            }
            
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="listInvoice_json"></param>
        /// <param name="v6return">Giá trị trả về cho PostManagerResult. RESULT_STRING RESULT_OBJECT SO_HD ID RESULT_ERROR</param>
        /// <returns></returns>
        string DoCreateInvoice(int commandType, string listInvoice_json, out V6Return v6return)
        {
            string msg = null;
            v6return = new V6Return();
            
            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(commandType, listInvoice_json, out result);
            if (result != null)
            {
                v6return.RESULT_OBJECT = result.Object;
                v6return.RESULT_STRING = result.Object.ToString();
            }
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

            // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                v6return.RESULT_OBJECT = invoiceResult;
                if (invoiceResult.Status == 0 || invoiceResult.MessLog.StartsWith("Đã tồn tại Hóa đơn")) // Không có lỗi hoặc tồn tại.
                {
                    v6return.SO_HD = invoiceResult.InvoiceNo.ToString();
                    v6return.ID = invoiceResult.InvoiceGUID.ToString();
                    v6return.SECRET_CODE = invoiceResult.MTC;
                }
                else
                {
                    msg = msg + "; " + invoiceResult.MessLog;
                    v6return.RESULT_ERROR_MESSAGE = msg.Substring(2);
                }
            }

            if (msg.Length>2) msg = msg.Substring(2);
            return msg;
        }

        string DoAdjustInvoice(string listInvoice_json, out V6Return v6return)
        {
            string msg = null;
            v6return = new V6Return();

            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._121_CreateAdjust, listInvoice_json, out result);
            
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();

            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                v6return.RESULT_OBJECT = invoiceResult;
                if (invoiceResult.Status == 0 || invoiceResult.MessLog.StartsWith("Đã tồn tại Hóa đơn")) // Không có lỗi hoặc tồn tại.
                {
                    v6return.SO_HD = invoiceResult.InvoiceNo.ToString();
                    v6return.ID = invoiceResult.InvoiceGUID.ToString();
                    v6return.SECRET_CODE = invoiceResult.MTC;
                }
                else
                {
                    msg = msg + "; " + invoiceResult.MessLog;
                    v6return.RESULT_ERROR_MESSAGE = msg.Substring(2);
                }
            }

            return msg;
        }


        public string DoReplaceInvoice(int commandType, string listInvoice_json, out V6Return v6return)
        {
            string msg = null;
            v6return = new V6Return();

            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(commandType, listInvoice_json, out result);
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();

            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                v6return.RESULT_OBJECT = invoiceResult;
                if (invoiceResult.Status == 0 || invoiceResult.MessLog.StartsWith("Đã tồn tại Hóa đơn")) // Không có lỗi hoặc tồn tại.
                {
                    v6return.SO_HD = invoiceResult.InvoiceNo.ToString();
                    v6return.ID = invoiceResult.InvoiceGUID.ToString();
                    v6return.SECRET_CODE = invoiceResult.MTC;
                    v6return.RESULT_MESSAGE = invoiceResult.MessLog;
                }
                else
                {
                    msg = msg + "; " + invoiceResult.MessLog;
                    v6return.RESULT_ERROR_MESSAGE = msg.Substring(2);
                }
            }

            return msg;
        }

        public string CancelInvoice(int CmdType, string id, out V6Return v6return)
        {
            string msg = "";
            v6return = new V6Return();
            var postObject = new PostObjectBkav();

            //List<InvoiceDataWS> listInvoiceDataWS = new List<InvoiceDataWS>();
            //InvoiceDataWS invoiceDataWS = new InvoiceDataWS();
            if (CmdType == BkavConst._202_CancelInvoiceByPartnerInvoiceID)
            {
                postObject.PartnerInvoiceID = "0";
                postObject.PartnerInvoiceStringID = id;
            }
            else // GUID
            {
                //invoiceDataWS.Invoice = new InvoiceWS();
                Guid guid = Guid.Empty;
                msg = Convertor.StringToGuid(id, out guid);
                if (msg.Length > 0) return "Giá trị không thể convert sang GUID";
                postObject.Invoice["InvoiceGUID"] = guid;
            }

            //listInvoiceDataWS.Add(invoiceDataWS);

            string list = null;
            list = "["+postObject.ToJson()+"]";
            //msg = Convertor.ObjectToString<List<InvoiceDataWS>>(rdXML.Checked, listInvoiceDataWS, out list);
            //if (msg.Length > 0) return msg;

            Result result = null;
            msg = remoteCommand.TransferCommandAndProcessResult(CmdType, list, out result);
            
            if (msg != null && msg.StartsWith("Hóa đơn này đã được Hủy."))
            {
                v6return.RESULT_OBJECT = result.Object;
                v6return.RESULT_STRING = result.Object.ToString();
                return "";
            }
            else if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();

            // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

            //foreach (InvoiceResult invoiceResult in listInvoiceResult)
            //    if (invoiceResult.Status != 0) return invoiceResult.MessLog;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                v6return.RESULT_OBJECT = invoiceResult;
                if (invoiceResult.Status == 0)
                {
                    v6return.SO_HD = invoiceResult.InvoiceNo.ToString();
                    v6return.ID = invoiceResult.InvoiceGUID.ToString();
                    v6return.SECRET_CODE = invoiceResult.MTC;
                }
                else
                {
                    msg = msg + "; " + invoiceResult.MessLog;
                    v6return.RESULT_ERROR_MESSAGE = msg.Substring(2);
                }
            }

            return msg;
        }

        public string SignInvoice(string uid, out V6Return v6return)
        {
            string msg = "";
            v6return = new V6Return();
            var postObject = new PostObjectBkav();
            
            Guid guid = Guid.Empty;
            msg = Convertor.StringToGuid(uid, out guid);
            if (msg.Length > 0) return "Giá trị không thể convert sang GUID";
            
            Result result = null;
            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._205_SignGUID, uid, out result);

            if (msg != null && msg.StartsWith("Hóa đơn đang được ký phát hành."))
            {
                v6return.RESULT_OBJECT = result.Object;
                v6return.RESULT_STRING = result.Object.ToString();
                return "";
            }
            else if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();

            return msg;
            // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
            List<InvoiceResult> listInvoiceResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out listInvoiceResult);
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

            //foreach (InvoiceResult invoiceResult in listInvoiceResult)
            //    if (invoiceResult.Status != 0) return invoiceResult.MessLog;

            foreach (InvoiceResult invoiceResult in listInvoiceResult)
            {
                v6return.RESULT_OBJECT = invoiceResult;
                if (invoiceResult.Status == 0)
                {
                    v6return.SO_HD = invoiceResult.InvoiceNo.ToString();
                    v6return.ID = invoiceResult.InvoiceGUID.ToString();
                    v6return.SECRET_CODE = invoiceResult.MTC;
                }
                else
                {
                    msg = msg + "; " + invoiceResult.MessLog;
                    v6return.RESULT_ERROR_MESSAGE = msg.Substring(2);
                }
            }

            return msg;
        }

        /// <summary>
        /// Tải về file PDF hóa đơn, thành công trả về đường dẫn file.
        /// </summary>
        /// <param name="stringID"></param>
        /// <param name="savefolder"></param>
        /// <param name="mode">1 (hoặc khác 2) thể hiện 2 chuyển đổi</param>
        /// <returns></returns>
        public string DownloadInvoicePDF(string stringID, string savefolder, string mode, out V6Return v6Return)
        {
            string msg = null;
            v6Return = new V6Return();
            var postObject = new PostObjectBkav();
            //List<InvoiceDataWS> listInvoiceDataWS = new List<InvoiceDataWS>();
            
            //InvoiceDataWS invoiceDataWS = new InvoiceDataWS();
            //Chỉ được truyền giá trị cho PartnerInvoiceID trong hoặc PartnerInvoiceStringID
            postObject.PartnerInvoiceID = "0";
            postObject.PartnerInvoiceStringID = stringID;

            //string list = postObject.ToJson();//\"N000986211SOA
            //list = "[" + list + "]";
            
            Result result = null;
            if (mode == "2")
            {
                Dictionary<string, object> commandObject = new Dictionary<string, object>();
                commandObject["PartnerInvoiceID"] = 0;
                commandObject["PartnerInvoiceStringID"] = stringID;
                msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._804_GetInvoiceLink_ChuyenDoi, new[]{commandObject}, out result);
                //string j = V6Tools.V6Convert.V6JsonConverter.ObjectToJson(commandObject, null);
                if (msg.Length > 0)
                {
                    // throw new Exception(msg);
                    v6Return.RESULT_ERROR_MESSAGE = msg;
                    return null;
                }

                v6Return.RESULT_STRING = result.Object.ToString();

                // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
                //List<InvoiceResult> listInvoiceResult = null;
                InvoiceResult[] getlinkE_Result = null;
                msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out getlinkE_Result);
                if (msg.Length > 0)
                {
                    // throw new Exception(msg);
                    v6Return.RESULT_ERROR_MESSAGE = msg;
                    return null;
                }
                InvoiceResult linkpdfResult = getlinkE_Result[0];
                if (linkpdfResult != null && linkpdfResult.MessLog != null)
                {
                    string path = Path.Combine(savefolder, stringID + "_E.pdf");
                    string download_link = _baseUrl.Substring(0, _baseUrl.LastIndexOf('/') + 1) + linkpdfResult.MessLog; 
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(download_link, path);
                    }

                    v6Return.RESULT_OBJECT = download_link;
                    v6Return.RESULT_STRING = download_link;
                    v6Return.RESULT_MESSAGE = path;
                    v6Return.PATH = path;
                    if (File.Exists(path))
                    {
                        return path;
                    }
                    else
                    {
                        v6Return.RESULT_ERROR_MESSAGE = "ERR:" + download_link;
                        return null;
                    }
                }
                else
                {
                    v6Return.RESULT_ERROR_MESSAGE = "downloadResult = null";
                }
            }
            else
            {
                msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._808_GetInvoicePDF64, stringID, out result);

                if (msg.Length > 0)
                {
                    // throw new Exception(msg);
                    v6Return.RESULT_ERROR_MESSAGE = msg;
                    return null;
                }

                v6Return.RESULT_STRING = result.Object.ToString();

                // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
                //List<InvoiceResult> listInvoiceResult = null;
                PdfResult pdfResult = null;
                msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out pdfResult);
                if (msg.Length > 0)
                {
                    // throw new Exception(msg);
                    v6Return.RESULT_ERROR_MESSAGE = msg;
                    return null;
                }

                if (pdfResult != null && pdfResult.PDF != null)
                {
                    string path = Path.Combine(savefolder, stringID + ".pdf");
                    try
                    {
                        if (File.Exists(path)) File.Delete(path);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (!File.Exists(path)) File.WriteAllBytes(path, pdfResult.PDF);
                    }

                    v6Return.PATH = path;
                    return path;
                }
                else
                {
                    v6Return.RESULT_ERROR_MESSAGE = "pdfResult = null";
                }
            }
            
            
            
            return null;
        }
    }
}

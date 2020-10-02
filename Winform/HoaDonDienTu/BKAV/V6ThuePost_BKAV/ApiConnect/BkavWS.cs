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

namespace V6ThuePostBkavApi
{
    public class BkavWS
    {

        public string POST(RemoteCommand remoteCommand, string jsonBody, int commandType, out V6Return v6return)
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
                        msg = DoCreateInvoice(remoteCommand, commandType, jsonBody, out v6return);
                        break;
                    case BkavConst._121_CreateAdjust:
                        msg = DoAdjustInvoice(remoteCommand, jsonBody, out v6return);
                        break;
                    case BkavConst._120_CreateReplace:
                    case BkavConst._123_CreateReplace:
                        msg = DoReplaceInvoice(remoteCommand, commandType, jsonBody, out v6return);
                        break;
                    case BkavConst._201_CancelInvoiceByInvoiceGUID:
                    case BkavConst._202_CancelInvoiceByPartnerInvoiceID:
                        msg = CancelInvoice(remoteCommand, commandType, jsonBody, out v6return);
                        break;
                    case BkavConst._205_SignGUID:
                        msg = SignInvoice(remoteCommand, jsonBody, out v6return);
                        break;
                    default:
                        msg = "V6 not supported.";
                        v6return = new V6Return();
                        v6return.RESULT_ERROR_MESSAGE = msg;
                        break;
                }

                if (msg.Length > 0)
                {
                    result = "ERR:" + msg;
                }
                else if (v6return != null)
                {
                     result += v6return.RESULT_STRING;// string.Format("SO_HD:{0}; GUID:{1}", so_hd, guid);
                }
            }
            catch (Exception ex)
            {
                v6return = new V6Return();
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                result = ex.Message;
            }
            
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remoteCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="listInvoice_json"></param>
        /// <param name="v6return">Giá trị trả về cho PostManagerResult. RESULT_STRING RESULT_OBJECT SO_HD ID RESULT_ERROR</param>
        /// <returns></returns>
        string DoCreateInvoice(RemoteCommand remoteCommand, int commandType, string listInvoice_json, out V6Return v6return)
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

        string DoAdjustInvoice(RemoteCommand remoteCommand, string listInvoice_json, out V6Return v6return)
        {
            string msg = null;
            v6return = new V6Return();

            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._121_CreateAdjust, listInvoice_json, out result);
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

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


        public string DoReplaceInvoice(RemoteCommand remoteCommand, int commandType, string listInvoice_json, out V6Return v6return)
        {
            string msg = null;
            v6return = new V6Return();

            Result result = null;

            msg = remoteCommand.TransferCommandAndProcessResult(commandType, listInvoice_json, out result);
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

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

        public string CancelInvoice(RemoteCommand remoteCommand, int CmdType, string id, out V6Return v6return)
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
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();
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

            //foreach (InvoiceResult invoiceResult in listInvoiceResult)
            //    if (invoiceResult.Status != 0) return invoiceResult.MessLog;

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

        public string SignInvoice(RemoteCommand remoteCommand, string uid, out V6Return v6return)
        {
            string msg = "";
            v6return = new V6Return();
            var postObject = new PostObjectBkav();
            
            Guid guid = Guid.Empty;
            msg = Convertor.StringToGuid(uid, out guid);
            if (msg.Length > 0) return "Giá trị không thể convert sang GUID";
            
            Result result = null;
            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._205_SignGUID, uid, out result);
            v6return.RESULT_OBJECT = result.Object;
            v6return.RESULT_STRING = result.Object.ToString();
            if (msg.Length > 0)
            {
                v6return.RESULT_ERROR_MESSAGE = msg;
                return msg;
            }

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
                if (invoiceResult.Status == 0 || invoiceResult.MessLog.StartsWith("Đã tồn tại Hóa đơn"))
                    // Không có lỗi hoặc tồn tại.
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

        public string DownloadInvoicePDF(RemoteCommand remoteCommand, string fkey_hd, string savefolder)
        {
            string msg = null;

            var postObject = new PostObjectBkav();
            //List<InvoiceDataWS> listInvoiceDataWS = new List<InvoiceDataWS>();
            
            //InvoiceDataWS invoiceDataWS = new InvoiceDataWS();
            //Chỉ được truyền giá trị cho PartnerInvoiceID trong hoặc PartnerInvoiceStringID
            postObject.PartnerInvoiceID = "0";
            postObject.PartnerInvoiceStringID = fkey_hd;

            //string list = postObject.ToJson();//\"N000986211SOA
            //list = "[" + list + "]";
            
            Result result = null;
            //msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._804_GetInvoiceLink, list, out result);
            msg = remoteCommand.TransferCommandAndProcessResult(BkavConst._808_GetInvoicePDF64, fkey_hd, out result);
            if (msg.Length > 0) throw new Exception(msg);

            // Không có lỗi, Hệ thống trả ra danh sách kết quả của Hóa đơn
            //List<InvoiceResult> listInvoiceResult = null;
            PdfResult pdfResult = null;
            msg = Convertor.StringToObject(false, Convert.ToString(result.Object), out pdfResult);
            if (msg.Length > 0) throw new Exception(msg);

            if (pdfResult != null && pdfResult.PDF != null)
            {
                string path = Path.Combine(savefolder, fkey_hd + ".pdf");
                if(!File.Exists(path)) File.WriteAllBytes(path, pdfResult.PDF);
                return path;
            }

            //string url = null;
            //foreach (InvoiceResult invoiceResult in listInvoiceResult)
            //{
            //    if (invoiceResult.Status == 0)
            //    {
            //        msg = "";
            //        url = invoiceResult.MessLog;
            //    }//Link tải trả về trong trường MessLog
            //    else return invoiceResult.MessLog;
            //}

            //if (!string.IsNullOrEmpty(url))
            //{
            //    string path = Path.Combine(savefolder, fkey_hd + ".pdf");

            //    using (WebClient webClient = new WebClient())
            //    {
            //        //webClient.DownloadFile("https://ws.ehoadon.vn" + url, path);
            //        webClient.DownloadFile("https://demo.ehoadon.vn" + url, path);
            //    }
            //    return path;
            //}
            
            return null;
        }
    }
}

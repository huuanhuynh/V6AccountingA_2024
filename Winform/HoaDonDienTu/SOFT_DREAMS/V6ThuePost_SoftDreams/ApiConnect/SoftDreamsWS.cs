using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using V6EasyInvoice.Client;
using V6EasyInvoice.Client.Services;
using V6ThuePost;
using V6ThuePost.ResponseObjects;
using V6ThuePost.VnptObjects;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ThuePostSoftDreamsApi
{
    public class SoftDreamsWS
    {
        private EasyService _easyService = new EasyService();
        private string _host = "", _id = "", _pass = "", _token_serial;
        public SoftDreamsWS(string host, string id, string pass, string token_serial)
        {
            try
            {
                _easyService = new EasyService();
                _host = host;
                _id = id;
                _pass = pass;
                _token_serial = token_serial;
                return;
            }
            catch (Exception)
            {
                //EasyInvoice.Json.JsonConvert
            }
            return;
        }

        /// <summary>
        /// Phát hành hóa đơn.
        /// </summary>
        /// <param name="invoices"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="issue">Phát hành</param>
        /// <param name="signmode">Kiểu ký 1 client, mặc định server.</param>
        /// <param name="v6return">Các giá trị trả về.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public string ImportInvoices(Invoices invoices, string pattern, string serial, bool issue, string signmode, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var xml = V6XmlConverter.ClassToXml(invoices);
                var request = new Request()
                {
                    XmlData = xml,
                    Pattern = pattern,
                    Serial = serial,
                    CertString = null,
                };
                var response = signmode == "1" 
                    ? _easyService.ClientImportInvoices(request, _token_serial, issue, _host, _id, _pass)
                    : _easyService.ServerImportInvoices(request, issue, _host, _id, _pass);
                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.Message;
                v6return.RESULT_ERROR_CODE = response.Status.ToString();

                if (response.Status == 2)
                {
                    foreach (Inv inv in invoices.Inv)
                    {
                        var ikey = inv.Invoice["Ikey"].ToString().Trim();

                        if (response.Data.KeyInvoiceNo != null)
                        {
                            string sohd = response.Data.KeyInvoiceNo[ikey];
                            v6return.SO_HD = sohd;
                            try
                            {
                                v6return.SECRET_CODE = response.Data.Invoices[0].LookupCode;
                            }
                            catch (Exception)
                            {
                                
                            }
                            result += "OK:" + string.Format("Ikey:{0}, InvoiceNo:{1}", ikey, sohd);
                        }
                        else
                            result += "OK:" + string.Format("Ikey:{0}, InvoiceNo:{1}", ikey, "null");
                    }
                }
                else
                {
                    result += "ERR:" + response.Status + " " + response.Message + " ";
                    if (response.Data != null)
                    foreach (Inv inv in invoices.Inv)
                    {
                        var ikey = inv.Invoice["Ikey"].ToString().Trim();
                        result += ikey + " " + response.Data.KeyInvoiceMsg[ikey];
                    }
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
        /// Phát hành hóa đơn có số chưa phát hành.
        /// </summary>
        /// <param name="invoices"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="signmode">Kiểu ký 1 client, mặc định server.</param>
        /// <param name="v6return">Các giá trị trả về.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public string ImportInvoicesNoIssue(Invoices invoices, string pattern, string serial, string signmode, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var xml = V6XmlConverter.ClassToXml(invoices);
                var request = new Request()
                {
                    XmlData = xml,
                    Pattern = pattern,
                    Serial = serial,
                    CertString = null,
                };
                var response = signmode == "1" 
                    ? _easyService.ClientImportInvoices(request, _token_serial, false, _host, _id, _pass)
                    : _easyService.ServerImportInvoices(request, false, _host, _id, _pass);
                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.Message;
                v6return.RESULT_ERROR_CODE = response.Status.ToString();

                if (response.Status == 2)
                {
                    var ikey = response.Data.Ikeys[0]; // inv.Invoice["Ikey"].ToString().Trim();

                    if (response.Data.Invoices != null)
                    {
                        string sohd = "" + response.Data.Invoices[0].No;
                        v6return.SO_HD = sohd;
                        try
                        {
                            v6return.SECRET_CODE = response.Data.Invoices[0].LookupCode;
                        }
                        catch (Exception)
                        {
                        }
                        result += "OK:" + string.Format("Ikey:{0}, InvoiceNo:{1}", ikey, sohd);
                    }
                    else
                        result += "OK:" + string.Format("Ikey:{0}, InvoiceNo:{1}", ikey, "null");
                }
                else
                {
                    result += "ERR:" + response.Status + " " + response.Message + " ";
                    if (response.Data != null)
                    foreach (Inv inv in invoices.Inv)
                    {
                        var ikey = inv.Invoice["Ikey"].ToString().Trim();
                        result += ikey + " " + response.Data.KeyInvoiceMsg[ikey];
                    }
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
        /// <para>Tạo dải hoá đơn trống (ImportInvoices?). Không có ikey => V6 không thể áp dụng.</para>
        /// <para>Trả về số hóa đơn.</para>
        /// </summary>
        /// <param name="ikey">ikey chơi chơi.</param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="soluong">V6 đang ghi đè là 1.</param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string CreateInvoiceStrip(string ikey, string pattern, string serial, int soluong, out V6Return v6return)
        {
            string result = null;
            soluong = 1;
            v6return = new V6Return();
            try
            {
                string data = "{“Pattern”:”Mẫu hóa đơn”,”Serial”:”Ký hiệu hóa đơn”, “Quantity”: “Số lượng” }";
                var request = new Request()
                {
                    Pattern = pattern,
                    Serial = serial,
                    Quantity = soluong,
                };
                var response = _easyService.CreateReservedInvoices(request, _host, _id, _pass);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.Message;
                v6return.RESULT_ERROR_CODE = response.Status.ToString();

                if (response.Status == 2)
                {
                    //                “Data”: {
                    //“Pattern”: “Tên mẫu”,
                    //“Serial”: “Ký hiệu”,
                    //“InvoiceNo”: [
                    // “số hoá đơn chờ ký 1”,
                    // “số hoá đơn chờ ký 2”
                    //]
                    v6return.SO_HD = response.Data.InvoiceNo[0].ToString();
                    result += "OK:" + string.Format("Ikey:{0}, InvoiceNo:{1}", ikey, response.Data.InvoiceNo[0]);
                }
                else
                {
                    result += "ERR:" + response.Status + " " + response.Message + " ";
                    if (response.Data != null)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="old_ikey"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="issue"></param>
        /// <param name="signmode">Kiểu ký 1 client, mặc định server.</param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string AdjustInvoice(AdjustInv inv, string old_ikey, string pattern, string serial, bool issue, string signmode, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var xml = V6XmlConverter.ClassToXml(inv);
                //var ikey = inv.Invoice["Ikey"].ToString().Trim();
                var request = new Request()
                {
                    XmlData = xml,
                    Ikey = old_ikey,
                    Pattern = pattern,
                    Serial = serial,
                    CertString = null,
                };
                var response = signmode == "1"
                ? _easyService.ClientAdjustInvoice(request, _token_serial, issue, _host, _id, _pass)
                : _easyService.ServerAdjustInvoice(request, issue, _host, _id, _pass);

                v6return.RESULT_OBJECT = response;
                v6return.RESULT_ERROR_CODE = response.Status.ToString();
                
                if (response.Status == 2)
                {
                    result += "OK:" + response.Data.KeyInvoiceNo[old_ikey];
                }
                else
                {
                    result += "ERR:" + response.Message;
                    if (response.Data != null)
                    result += string.Format("ERR:{0} {1} {2}", response.Status, response.Message, response.Data.KeyInvoiceMsg[old_ikey]);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.adjustInv " + result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="old_ikey"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="issue">Phát hành.</param>
        /// <param name="signmode">Kiểu ký 1 client, mặc định server.</param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string ReplaceInvoice(ReplaceInv inv, string old_ikey, string pattern, string serial, bool issue, string signmode, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                var xml = V6XmlConverter.ClassToXml(inv);
                //var ikey = inv.Invoice["Ikey"].ToString().Trim();
                var request = new Request()
                {
                    XmlData = xml,
                    Ikey = old_ikey,
                    Pattern = pattern,
                    Serial = serial,
                    CertString = null,
                };
                var response = signmode == "1"
                    ? _easyService.ClientReplaceInvoice(request, _token_serial, issue, _host, _id, _pass)
                    : _easyService.ServerReplaceInvoice(request, issue, _host, _id, _pass);
                if (response.Status == 2)
                {
                    result += "OK:" + response.Data.KeyInvoiceNo[old_ikey];
                }
                else
                {
                    if (response.Data != null)
                    result += string.Format("ERR:{0} {1} {2}", response.Status, response.Message, response.Data.KeyInvoiceMsg[old_ikey]);
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
        /// Hủy bỏ hóa đơn (đã ký số).
        /// </summary>
        /// <param name="ikey"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <returns></returns>
        public string CancelInvoice(string ikey, string pattern, string serial)
        {
            string result = null;
            try
            {
                var request = new Request()
                {
                    Ikey = ikey,
                    Pattern = pattern,
                    Serial = serial,
                    CertString = null,
                };
                var response = _easyService.CancelInvoice(request, _host, _id, _pass);
                if (response.Status == 2)
                {
                    result += "OK:" + response.Message;
                }
                else
                {
                    if (response.Data != null)
                    result += string.Format("ERR:{0} {1} {2}", response.Status, response.Message, response.Data.KeyInvoiceMsg[ikey]);
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
        /// Phát hành hóa đơn đã đưa lên
        /// </summary>
        /// <param name="ikey"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="signmode">Kiểu ký 1 client, mặc định server.</param>
        /// <returns></returns>
        public string IssueInvoices(string ikey, string pattern, string serial, string signmode)
        {
            string result = null;
            try
            {
                var request = new Request()
                {
                    Ikey = "[\"" + ikey + "\"]",    // List Ikey nhưng chỉ dùng 1
                    Pattern = pattern,
                    Serial = serial,
                    CertString = null,
                };
                var response = signmode == "1"
                    ? _easyService.ClientIssueInvoices(request, _host, _id, _pass)
                    : _easyService.ServerIssueInvoices(request, _host, _id, _pass);
                if (response.Status == 2)
                {
                    result += "OK:" + response.Data.KeyInvoiceNo[ikey];
                }
                else
                {
                    if (response.Data != null)
                    result += string.Format("ERR:{0} {1} {2}", response.Status, response.Message, response.Data.KeyInvoiceMsg[ikey]);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            Logger.WriteToLog("Program.IssueInvoices " + result);
            return result;
        }

        /// <summary>
        /// Tải hoá đơn định dạng PDF
        /// </summary>
        /// <param name="ikey"></param>
        /// <param name="option">0 - Bản pdf thông thường; 1 - Bản pdf chuyển đổi chứng minh nguồn gốc; 2 – Bản pdf chuyển đổi lưu trữ</param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="savefolder"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string GetInvoicePdf(string ikey, int option, string pattern, string serial, string savefolder, out V6Return v6return)
        {
            string path = Path.Combine(savefolder, ikey + ".pdf");
            v6return = new V6Return();
            var request = new Request()
            {
                Ikey = ikey,
                Option = option,
                Pattern = pattern,
                Serial = serial
            };

            if (!File.Exists(path))
            {
                var response =_easyService.GetInvoicePdf(request, path, _host, _id, _pass);
                v6return.RESULT_OBJECT = response;
                v6return.RESULT_STRING = V6XmlConverter.ClassToXml(response);
                v6return.RESULT_MESSAGE = response.Message;
            }

            if (File.Exists(path))
            {
                v6return.PATH = path;
                return path;
            }
            else
            {
                v6return.PATH = null;
                return null;
            }
        }

        /// <summary>
        /// Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string ConfirmPayment(string fkey_old, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = "ERR:";// = new BusinessService(link_Business).confirmPayment(fkey_old, username, password);

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

        /// <summary>
        /// Gạch nợ hóa đơn theo fkey
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string ConfirmPaymentFkey(string fkey_old, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = "ERR:";// = new BusinessService(link_Business).confirmPaymentFkey(fkey_old, username, password);

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

        public string UnconfirmPaymentFkey(string fkey_old, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = "ERR:";// = newBusinessService(link_Business).UnConfirmPaymentFkey(fkey_old, username, password);

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


        


        

        public string DownloadInvPDFFkeyNoPay(string fkeyA)
        {
            throw new NotImplementedException();
        }

        public string DownloadInvFkeyNoPay(string fkeyA)
        {
            var response = _easyService.GetInvoicesByIkeys(null);
            return response.Message;
        }

        public string UploadInvAttachmentFkey(string fkeyA, string arg3)
        {
            throw new NotImplementedException();
        }
    }
}

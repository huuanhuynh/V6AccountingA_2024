using System;
using System.Collections.Generic;
using System.IO;
using V6ThuePost.ResponseObjects;

namespace V6ThuePostXmlApi
{
    public class VnptTokenV5WS
    {
        private string _baseLink = "https://admindemo.vnpt-invoice.com.vn";
        private string _publishLink = "/PublishService.asmx";
        private string _businessLink = "/BusinessService.asmx";
        private string _portalLink = "/PortalService.asmx";
        /// <summary>
        /// username đăng nhập web.
        /// </summary>
        private string _account = "";
        private string _accountpassword = "";
        /// <summary>
        /// username webservice.
        /// </summary>
        private string _username = "";
        private string _password = "";
        /// <summary>
        /// Khởi tạo VnptWS
        /// </summary>
        /// <param name="baseLink">Đường dẫn server.</param>
        /// <param name="account">Tài khoản đăng nhập admin.</param>
        /// <param name="accountPassword">Mật khẩu admin.</param>
        /// <param name="userName">Tài khoản web service.</param>
        /// <param name="password">Mật khẩu ws.</param>
        public VnptTokenV5WS(string baseLink, string account, string accountPassword, string userName, string password)
        {
            _baseLink = baseLink;
            _account = account;
            _accountpassword = accountPassword;
            _username = userName;
            _password = password;
        }

        /// <summary>
        /// Phát hành hóa đơn.
        /// </summary>
        /// <param name="xml">Dữ liệu các hóa đơn.</param>
        /// <param name="serial">Ký hiệu VN/20E</param>
        /// <param name="v6Return">Kết quả trả về đã phân tích.</param>
        /// <param name="pattern">Mẫu số 01GTKT0...</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public string ImportAndPublishInv(string xml, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                var publishService = new PublishService.PublishService(_baseLink + _publishLink);
                result = publishService.ImportAndPublishInv(_account, _accountpassword, xml, _username, _password, pattern, serial, 0);
                v6Return.RESULT_STRING = result;

                if (result.StartsWith("OK") && result.Contains("_"))
                {
                    //OK:01GTKT3/001;AA/12E;key_0000002
                    int index = result.IndexOf('_');
                    string so_hd = result.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                }
                else if (result.StartsWith("ERR:20"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nPattern và serial không phù hợp, hoặc không tồn tại hóa đơn đã đăng kí có sử dụng Pattern và serial truyền vào.";
                }
                else if (result.StartsWith("ERR:13"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:10"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nLô có số hóa đơn vượt quá max cho phép.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông đủ số hóa đơn cho lô phát hành.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông phát hành được hóa đơn.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nDữ liệu xml đầu vào không đúng quy định.\nKhông có hóa đơn nào được phát hành.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
                else
                {
                    v6Return.RESULT_ERROR_MESSAGE = result;
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            return result;
        }


        /// <summary>
        /// Download invoice VNPT
        /// </summary>
        /// <param name="fkey"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string DownloadInvFkeyNoPay(string fkey, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new PortalService.PortalService(_baseLink + _portalLink).downloadInvFkeyNoPay(fkey, _username, _password);
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

        public string GetSoHoaDon_VNPT(string invXml)
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
        /// Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
        /// </summary>
        /// <param name="_link_Business"></param>
        /// <param name="lstInvToken">01GTKT2/001;AA/13E;10_????????</param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <param name="v6return">Các giá trị trả về.</param>
        /// <returns></returns>
        public static string ConfirmPayment(string _link_Business, string lstInvToken, string userName, string userPass, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_link_Business).confirmPayment(lstInvToken, userName, userPass);
                v6return.RESULT_STRING = result;
                if (result.StartsWith("ERR")) v6return.RESULT_ERROR_MESSAGE = result;

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
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Gạch nợ hóa đơn theo fkey
        /// </summary>
        /// <param name="_link_Business"></param>
        /// <param name="fkey_old"></param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public static string ConfirmPaymentFkey(string _link_Business, string fkey_old, string userName, string userPass, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_link_Business).confirmPaymentFkey(fkey_old, userName, userPass);
                v6return.RESULT_STRING = result;
                v6return.RESULT_OBJECT = result;
                if (result.StartsWith("ERR:13"))
                {
                    result += "Hóa đơn đã được gạch nợ.";
                    v6return.RESULT_ERROR_MESSAGE = result;
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "Không gạch nợ được.";
                    v6return.RESULT_ERROR_MESSAGE = result;
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "Không tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                    v6return.RESULT_ERROR_MESSAGE = result;
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "Tài khoản đăng nhập sai.";
                    v6return.RESULT_ERROR_MESSAGE = result;
                }
            }
            catch (Exception ex)
            {
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }

        public static string UnconfirmPaymentFkey(string _link_Business, string fkey_old, string userName, string userPass, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_link_Business).UnConfirmPaymentFkey(fkey_old, userName, userPass);
                v6return.RESULT_STRING = result;
                if (result.StartsWith("ERR")) v6return.RESULT_ERROR_MESSAGE = result;

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

            return result;
        }

        /// <summary>
        /// Tải về file PDF hóa đơn. Trả về đường dẫn file.
        /// </summary>
        /// <param name="option">0 - Bản pdf thông thường; 1 - Bản pdf chuyển đổi.</param>
        /// <param name="fkey"></param>
        /// <param name="saveFolder"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public string DownloadInvPDFFkey(string fkey, int option,string saveFolder, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                if (option == 1)
                {
                    result = new PortalService.PortalService(_baseLink + _portalLink).convertForStoreFkey(fkey, _username, _password);
                    string fileName = fkey;
                    string path = Path.Combine(saveFolder, fileName + ".html");
                    try
                    {
                        if (File.Exists(path)) File.Delete(path);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (!File.Exists(path)) File.WriteAllText(path, result);
                    }

                    v6return.PATH = path;
                    return path;
                }
                else
                {
                    result = new PortalService.PortalService(_baseLink + _portalLink).downloadInvPDFFkey(fkey, _username, _password);
                }
                v6return.RESULT_STRING = result;
                if (result.StartsWith("ERR")) v6return.RESULT_ERROR_MESSAGE = result;

                if (result.StartsWith("ERR:11"))
                {
                    result += "Hóa đơn chưa thanh toán nên không xem được.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "User name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "Không tìm thấy hóa đơn.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "Tài khoản đăng nhập sai hoặc không có quyền.";
                }
                else if (result.StartsWith("ERR"))
                {
                    result += "Có lỗi xảy ra.";
                }
                else
                {
                    string fileName = fkey;
                    string path = Path.Combine(saveFolder, fileName + ".pdf");
                    try
                    {
                        if (File.Exists(path)) File.Delete(path);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (!File.Exists(path)) File.WriteAllBytes(path, Convert.FromBase64String(result));
                    }
                    
                    v6return.PATH = path;
                    return path;
                }
            }
            catch (Exception ex)
            {
                v6return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            if(result != null)
                throw new Exception(result);
            return null;
        }

        public static string UpdateCus(string link_Publish, string xml, string username, string password, out V6Return v6return)
        {
            v6return = new V6Return();
            int result = 0;
            string message = "";
            try
            {
                result = new PublishService.PublishService(link_Publish).UpdateCus(xml, username, password, 0);
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
                //Logger.WriteToLog("Program.UpdateCus " + message);
            }

            return message;
        }

        public static string CheckConnection()
        {
            return "false";
            //string result = Program.ImportAndPublishInv("<V6test>Test</V6test>");
            //lblResult.Text = result;
            //if (result != null && result.Contains("Dữ liệu xml đầu vào không đúng quy định"))
            //{
            //    BaseMessage.Show("Kết nối ổn!", 500, this);
            //}
            //else
            //{
            //    BaseMessage.Show(result);
            //}
        }
    }
}

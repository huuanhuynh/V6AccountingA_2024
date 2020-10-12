using System;
using System.Collections.Generic;
using System.IO;
using SignTokenCore;
using V6ThuePost.ResponseObjects;

namespace V6ThuePostXmlApi
{
    public class VnptWS
    {
        private string _baseLink = "https://admindemo.vnpt-invoice.com.vn";
        private string _publishLink = "/PublishService.asmx";
        private string _businessLink = "/BusinessService.asmx";
        private string _portalLink = "/PortalService.asmx";

        public string PublishLink
        {
            get { return _baseLink + PublishLink; }
        }
        public string PortalLink
        {
            get { return _baseLink + _portalLink; }
        }
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
        public VnptWS(string baseLink, string account, string accountPassword, string userName, string password)
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
        /// <param name="pattern">Mấu số 01GTKT3/001</param>
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
        /// Đẩy lên và phát hành hóa đơn có ký chữ ký số (Token).
        /// </summary>
        /// <param name="xmlInvData">chuỗi xml hóa đơn.</param>
        /// <param name="pattern">Mấu số 01GTKT0/001</param>
        /// <param name="serial">Ký hiệu VT/19E</param>
        /// <param name="SERIAL_CERT">Serial của chứng thư công ty đã đăng ký trong hệ thống.</param>
        /// <param name="v6Return">Kết quả</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + “,”</returns>
        public string PublishInvWithToken_Dll(string xmlInvData, string pattern, string serial, string SERIAL_CERT, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = VNPTEInvoiceSignToken.PublishInvWithToken(_account, _accountpassword, xmlInvData, _username, _password, SERIAL_CERT, pattern, serial, PublishLink);
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
            //Logger.WriteToLog("Program.PublishInvWithToken " + result);
            return result;
        }

        public string adjustInv(string xml, string fkey_old, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_baseLink + _businessLink).adjustInv(_account, _accountpassword, xml, _username, _password, fkey_old, 0);
                v6Return.RESULT_STRING = result;
                if (result.StartsWith("OK") && result.Contains("_"))
                {
                    //OK:01GTKT3/001;AA/12E;key_0000002
                    int index = result.IndexOf('_');
                    string so_hd = result.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                }
                else if (result.StartsWith("ERR:9"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTrạng thái hóa đơn không được điều chỉnh.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn cần điều chỉnh đã bị thay thế. Không thể điều chỉnh được nữa.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nDải hóa đơn cũ đã hết.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông phát hành được hóa đơn.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nDữ liệu xml đầu vào không đúng quy định.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn cần điều chỉnh không tôn tại.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Download invoice VNPT, trả về invXml.
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
        /// <param name="lstInvToken">01GTKT2/001;AA/13E;10_????????</param>
        /// <param name="v6return">Các giá trị trả về.</param>
        /// <returns></returns>
        public string ConfirmPayment(string lstInvToken, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_baseLink + _businessLink).confirmPayment(lstInvToken, _username, _password);
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
        /// <param name="fkey_old"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string ConfirmPaymentFkey(string fkey_old, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_baseLink + _businessLink).confirmPaymentFkey(fkey_old, _username, _password);
                v6Return.RESULT_STRING = result;
                v6Return.RESULT_OBJECT = result;
                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:13"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn đã được gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai.";
                }
                else if (result.StartsWith("ERR"))
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

        public string UnconfirmPaymentFkey(string fkey_old, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_baseLink + _businessLink).UnConfirmPaymentFkey(fkey_old, _username, _password);
                v6Return.RESULT_STRING = result;

                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:13"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn đã được bỏ gạch nợ.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông bỏ gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai.";
                }
                else if (result.StartsWith("ERR")) v6Return.RESULT_ERROR_MESSAGE = result;
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            return result;
        }

        public string replaceInv(string xml, string fkey_old, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_baseLink + _businessLink).replaceInv(_account, _accountpassword, xml, _username, _password, fkey_old, 0);
                v6Return.RESULT_STRING = result;

                if (result.StartsWith("OK"))
                {
                    //OK:01GTKT3/001;AA/12E;key_0000002
                    int index = result.IndexOf('_');
                    string so_hd = result.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                }
                else if (result.StartsWith("ERR:9"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTrạng thái hóa đơn không được thay thế.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn đã được thay thế rồi. Không thể thay thế nữa.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nDải hóa đơn cũ đã hết.";
                }
                else if (result.StartsWith("ERR:5"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi trong quá trình thay thế hóa đơn.";
                }
                else if (result.StartsWith("ERR:3"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nDữ liệu xml đầu vào không đúng quy định.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tồn tại hóa đơn cần thay thế.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
                else if (result.StartsWith("ERR"))
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
        /// Hủy hóa đơn.
        /// </summary>
        /// <param name="fkey_old"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string cancelInv(string fkey_old, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_baseLink + _businessLink).cancelInv(_account, _accountpassword, fkey_old, _username, _password);
                v6Return.RESULT_STRING = result;
                if (result.StartsWith("OK"))
                {

                }
                else if (result.StartsWith("ERR:9"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTrạng thái hóa đơn không được thay thế.";
                }
                else if (result.StartsWith("ERR:8"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn đã được thay thế rồi, hủy rồi.";
                }
                else if (result.StartsWith("ERR:2"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tồn tại hóa đơn cần hủy.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
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
        /// Tải về file PDF hóa đơn. Trả về đường dẫn file.
        /// </summary>
        /// <param name="option">0 - Bản pdf thông thường; 1 - Bản pdf chuyển đổi.</param>
        /// <param name="fkey"></param>
        /// <param name="saveFolder"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string DownloadInvPDFFkey(string fkey, int option, string saveFolder, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                if (option == 1)
                {
                    result = new PortalService.PortalService(_baseLink + _portalLink).convertForStoreFkey(fkey, _username, _password);
                    v6Return.RESULT_STRING = result;
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

                    v6Return.PATH = path;
                    return path;
                }
                else
                {
                    result = new PortalService.PortalService(_baseLink + _portalLink).downloadInvPDFFkey(fkey, _username, _password);
                    v6Return.RESULT_STRING = result;
                }
                v6Return.RESULT_STRING = result;

                if (!result.StartsWith("ERR"))
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
                    
                    v6Return.PATH = path;
                    return path;
                }
                else if (result.StartsWith("ERR:11"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn chưa thanh toán nên không xem được.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy hóa đơn.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
                else if (result.StartsWith("ERR"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi xảy ra.";
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
        /// Tải về file PDF dạng base64
        /// </summary>
        /// <param name="fkey"></param>
        /// <returns></returns>
        public string DownloadInvPDFFkeyNoPay(string fkey, string saveFolder, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new PortalService.PortalService(PortalLink).downloadInvPDFFkeyNoPay(fkey, _username, _password);
                v6Return.RESULT_STRING = result;
                if (!result.StartsWith("ERR"))
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
                    
                    v6Return.PATH = path;
                    return path;
                }
                else if (result.StartsWith("ERR:11"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn chưa thanh toán nên không xem được.";
                }
                else if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nUser name không phù hợp, không tìm thấy company tương ứng cho user.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy hóa đơn.";
                }
                else if (result.StartsWith("ERR:1"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nTài khoản đăng nhập sai.";
                }
                else if (result.StartsWith("ERR"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi.";
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            //Logger.WriteToLog("Program.DownloadInvFkeyNoPay " + result);
            return result;
        }

        public string UpdateCus(string xml, out V6Return v6Return)
        {
            v6Return = new V6Return();
            int result = 0;
            string message = "";
            try
            {
                result = new PublishService.PublishService(_baseLink + _publishLink).UpdateCus(xml, _username, _password, 0);
                v6Return.RESULT_STRING = "" + result;
                message += result;

                if (result == -5)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ERR:" + result + "\r\nCó khách hàng đã tồn tại.";
                }
                else if (result == -3)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ERR:" + result + "\r\nDữ liệu xml đầu vào không đúng quy định.";
                }
                else if (result == -2)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ERR:" + result + "\r\nKhông import được khách hàng vào database.";
                }
                else if (result == -1)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "ERR:" + result + "\r\nTài khoản đăng nhập sai hoặc không có quyền.";
                }
            }
            catch (Exception ex)
            {
                message = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = message;
            }

            return message;
        }

        /// <summary>
        /// Kiểm tra kết nối lên server. Ok trả về null.
        /// </summary>
        /// <returns></returns>
        public string CheckConnection()
        {
            try
            {
                V6Return v6return;
                string result = ImportAndPublishInv("<V6test>Test</V6test>", "", "", out v6return);
                if (result != null &&(result.StartsWith("ERR:3") || result.StartsWith("ERR:20")))
                {
                    return null;
                }
                else
                {
                    return "Fail: " + result;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
}

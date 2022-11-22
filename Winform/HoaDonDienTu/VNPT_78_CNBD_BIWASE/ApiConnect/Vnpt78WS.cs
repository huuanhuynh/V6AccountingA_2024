using System;
using System.Collections.Generic;
using System.IO;
using SignTokenCore;
using V6ThuePost.ResponseObjects;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ThuePostXmlApi
{
    public class Vnpt78WS
    {
        private string _baseLink = "http://xemayminhthanhhcm-tt78demo.vnpt-invoice.com.vn";
        private string _publishLink = "/PublishService.asmx";
        private string _publishLink_BIWASE = "/PublishServiceBIWASE.asmx";
        private string _businessLink = "/BusinessService.asmx";
        private string _businessLink_BIWASE = "/BusinessServiceBIWASE.asmx";
        private string _portalLink = "/PortalService.asmx";

        private string _attachmentLink = "/AttachmentService.asmx";

        public string PublishLink
        {
            get { return _baseLink + _publishLink; }
        }
        public string PublishLink_BIWASE
        {
            get { return _baseLink + _publishLink_BIWASE; }
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
        /// Khởi tạo Vnpt78WS
        /// </summary>
        /// <param name="baseLink">Đường dẫn server.</param>
        /// <param name="account">Tài khoản đăng nhập admin.</param>
        /// <param name="accountPassword">Mật khẩu admin.</param>
        /// <param name="userName">Tài khoản web service.</param>
        /// <param name="password">Mật khẩu ws.</param>
        public Vnpt78WS(string baseLink, string account, string accountPassword, string userName, string password)
        {
            _baseLink = baseLink;
            _account = account;
            _accountpassword = accountPassword;
            _username = userName;
            _password = password;
        }

        /// <summary>
        /// Gửi hóa đơn nháp theo pattern.
        /// </summary>
        /// <param name="xml">Dữ liệu các hóa đơn.</param>
        /// <param name="pattern">Mấu số 01GTKT3/001</param>
        /// <param name="serial">Ký hiệu VN/20E</param>
        /// <param name="v6Return">Kết quả trả về đã phân tích.</param>
        /// <returns>Thông báo phát hành hd.</returns>
        public string ImportInvByPattern(string xml, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                var publishService = new PublishService.PublishService(_baseLink + _publishLink);
                result = publishService.ImportInvByPattern(_account,_accountpassword, xml, _username, _password, pattern, serial, 0);
                v6Return.RESULT_STRING = result;
                v6Return.RESULT_MESSAGE = "Có lỗi:";
                if (result.StartsWith("OK"))
                {
                    v6Return.RESULT_MESSAGE = "Thành công:";
                    //OK:01GTKT3/001;AA/12E;key_0000002
                    //int index = result.IndexOf('_');
                    //string so_hd = "0";
                    //v6Return.SO_HD = so_hd;
                    //v6Return.RESULT_ERROR_MESSAGE = "TEST để dành F6...";
                }
                else if (result.StartsWith("ERR:20"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nPattern và serial không phù hợp, hoặc không tồn tại hóa đơn đã đăng kí có sử dụng Pattern và serial truyền vào.";
                }
                else if (result.StartsWith("ERR:13"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nHóa đơn đã tồn tại.";
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
        /// Xóa hóa đơn nháp theo Fkey
        /// </summary>
        /// <param name="fkey"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string DeleteInvoiceByFkey(string fkey, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                var publishService = new PublishService.PublishService(_baseLink + _publishLink);
                result = publishService.deleteInvoiceByFkey(fkey, _username, _password, _account, _accountpassword);
                v6Return.RESULT_STRING = result;

                // result = "OK:N001625010SOA"

                if (result.StartsWith("OK:"))
                {
                    result += " delete success.";
                }
                else
                {
                    ReadErrorCode(result, v6Return);
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
                else
                {
                    ReadErrorCode(result, v6Return);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            return result;
        }

        public string ImportAndPublishInv_BIWASE(string xml, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                var publishService = new PublishServiceBIWASE.PublishServiceBIWASE(_baseLink + _publishLink);
                result = publishService.ImportAndPublishInv(_account, _accountpassword, xml, _username, _password, pattern, serial, 0);
                v6Return.RESULT_STRING = result;

                if (result.StartsWith("OK") && result.Contains("_"))
                {
                    //OK:01GTKT3/001;AA/12E;key_0000002
                    int index = result.IndexOf('_');
                    string so_hd = result.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                }
                else
                {
                    ReadErrorCode(result, v6Return);
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
        public string PublishInvWithToken32_Dll(string xmlInvData, string pattern, string serial, string SERIAL_CERT, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = VNPTEInvoiceSignToken.PublishInvWithToken32(_account, _accountpassword, xmlInvData, _username, _password, SERIAL_CERT, pattern, serial, PublishLink);
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

        /// <summary>
        /// Phát hành hóa đơn nháp theo Fkey
        /// </summary>
        /// <param name="fkey">danh sách Fkey cách nhau bằng gạch dưới _</param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="v6Return"></param>
        /// <returns></returns>
        public string PublishInvFkey(string fkey, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                var publishService = new PublishService.PublishService(_baseLink + _publishLink);
                result = publishService.PublishInvFkey(_account, _accountpassword, fkey, _username, _password, pattern, serial);
                v6Return.RESULT_STRING = result;

                // result = "ERR:6#danh sách fkey không tồn tại||ERR:15#ds fkey đã phát hành||OK:#N001625010SOA_2" fkey_sốHD
                var dic = ObjectAndString.StringToStringDictionary(result, '|', '#');
                v6Return.RESULT_MESSAGE = "Có lỗi:"; // sẽ ghi đè nếu thành công.

                if (dic.ContainsKey("OK:") && dic["OK:"].Trim().Length > 0)
                {
                    //OK:01GTKT3/001;AA/12E;key_0000002
                    string resultOK = dic["OK:"];
                    int index = resultOK.IndexOf('_');
                    string so_hd = resultOK.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                    v6Return.RESULT_MESSAGE = "Thành công số HD:" + so_hd;
                }
                else if (dic.ContainsKey("ERR:6") && dic["ERR:6"].Length > 0)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "Fkey không tồn tại: " + dic["ERR:6"];
                }
                else if (dic.ContainsKey("ERR:15") && dic["ERR:15"].Length > 0)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "Fkey đã phát hành: " + dic["ERR:15"];
                }
                else
                {
                    ReadErrorCode(result, v6Return);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            return result;
        }

        public string PublishInvFkey_BIWASE(string fkey, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                var publishService = new PublishServiceBIWASE.PublishServiceBIWASE(PublishLink_BIWASE);
                result = publishService.PublishInvFkey(_account, _accountpassword, fkey, _username, _password, pattern, serial);
                v6Return.RESULT_STRING = result;

                // result = "ERR:6#danh sách fkey không tồn tại||ERR:15#ds fkey đã phát hành||OK:#N001625010SOA_2" fkey_sốHD
                var dic = ObjectAndString.StringToStringDictionary(result, '|', '#');
                v6Return.RESULT_MESSAGE = "Có lỗi:"; // sẽ ghi đè nếu thành công.

                if (dic.ContainsKey("OK:") && dic["OK:"].Trim().Length > 0)
                {
                    //OK:01GTKT3/001;AA/12E;key_0000002
                    string resultOK = dic["OK:"];
                    int index = resultOK.IndexOf('_');
                    string so_hd = resultOK.Substring(index + 1);
                    v6Return.SO_HD = so_hd;
                    v6Return.RESULT_MESSAGE = "Thành công số HD:" + so_hd;
                }
                else if (dic.ContainsKey("ERR:6") && dic["ERR:6"].Length > 0)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "Fkey không tồn tại: " + dic["ERR:6"];
                }
                else if (dic.ContainsKey("ERR:15") && dic["ERR:15"].Length > 0)
                {
                    v6Return.RESULT_ERROR_MESSAGE = "Fkey đã phát hành: " + dic["ERR:15"];
                }
                else
                {
                    ReadErrorCode(result, v6Return);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

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
                    ReadErrorCode(result, v6Return);
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
        /// <param name="v6Return">Các giá trị trả về.</param>
        /// <returns></returns>
        public string ConfirmPayment(string lstInvToken, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessService.BusinessService(_baseLink + _businessLink).confirmPayment(lstInvToken, _username, _password);
                v6Return.RESULT_STRING = result;
                if (result.StartsWith("ERR")) v6Return.RESULT_ERROR_MESSAGE = result;

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
                else
                {
                    ReadErrorCode(result, v6Return);
                }

            }
            catch (Exception ex)
            {
                v6Return.RESULT_ERROR_MESSAGE = "WS EXCEPTION: " + ex.Message;
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
                else
                {
                    ReadErrorCode(result, v6Return);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            return result;
        }

        public string ConfirmPaymentFkeyPattern_BIWASE(string fkey_old, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessServiceBIWASE.BusinessServiceBIWASE(_baseLink + _businessLink_BIWASE).confirmPaymentFkeyPattern(fkey_old, _username, _password, pattern, serial);
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
                else
                {
                    ReadErrorCode(result, v6Return);
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
                if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông bỏ gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else
                {
                    ReadErrorCode(result, v6Return);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            return result;
        }

        public string UnconfirmPaymentFkeyPattern_BIWASE(string fkey_old, string pattern, string serial, out V6Return v6Return)
        {
            string result = null;
            v6Return = new V6Return();
            try
            {
                result = new BusinessServiceBIWASE.BusinessServiceBIWASE(_baseLink + _businessLink).UnConfirmPaymentFkeyPattern(fkey_old, _username, _password, pattern, serial);
                v6Return.RESULT_STRING = result;

                if (result.StartsWith("OK"))
                {

                }
                if (result.StartsWith("ERR:7"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông bỏ gạch nợ được.";
                }
                else if (result.StartsWith("ERR:6"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                }
                else
                {
                    ReadErrorCode(result, v6Return);
                }
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
                else if (result.StartsWith("ERR:5"))
                {
                    v6Return.RESULT_ERROR_MESSAGE = result + "\r\nCó lỗi trong quá trình thay thế hóa đơn.";
                }
                else
                {
                    ReadErrorCode(result, v6Return);
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
        /// Tải về file hóa đơn PDF (hoặc html-chuyển đổi). Trả về đường dẫn file.
        /// </summary>
        /// <param name="option">1 - Bản pdf thông thường; 2 - Bản pdf chuyển đổi.</param>
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
                if (option == 2)
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
                    ReadErrorCode(result, v6Return);
                }
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
                v6Return.RESULT_ERROR_MESSAGE = result;
            }

            return null;
        }

        /// <summary>
        /// Tải về file PDF dạng base64, trả về tên file nếu không có lỗi.
        /// </summary>
        /// <param name="fkey"></param>
        /// <param name="saveFolder"></param>
        /// <param name="v6Return"></param>
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
                else if (result.StartsWith("ERR:13"))
                {
                    v6Return.RESULT_MESSAGE = "Hóa đơn đã được gạch nợ.";
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
                    ReadErrorCode(result, v6Return);
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
                if (result != null && (result.StartsWith("ERR:3") || result.StartsWith("ERR:20")))
                {
                    return null;
                }
                else
                {
                    return "Fail: " + v6return.RESULT_ERROR_MESSAGE;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Tải lên bảng kê.
        /// </summary>
        /// <param name="fkey"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string UploadInvAttachmentFkey(string fkey, string file)
        {
            string result = null;
            try
            {
                string attachment64 = FileToBase64(file);
                string ext = Path.GetExtension(file);
                if (ext.Length > 0) ext = ext.Substring(1);
                string attachmentName = Path.GetFileNameWithoutExtension(file);
                result = new AttachmentService.AttachmentService(_baseLink +  _attachmentLink).uploadInvAttachmentFkey(fkey, _username, _password, attachment64, ext, attachmentName);

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

        public string FileToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string fileBase64 = Convert.ToBase64String(fileBytes);
            return fileBase64;
        }


        /// <summary>
        /// Thay thế điều chỉnh (Token).
        /// </summary>
        /// <param name="xmlInvData"></param>
        /// <param name="serialCert"></param>
        /// <param name="type">thay thế = 1, điều chỉnh tăng = 2, điều chỉnh giảm = 3, điều chỉnh thông tin = 4</param>
        /// <param name="invToken">patternt;serial;sốhóađơn 01GTKT2/001;AA/13E;10</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “;” + Fkey + “_” + Số hóa đơn + ”,”</returns>
        public string AdjustReplaceInvWithToken68_Dll(string xmlInvData, string serialCert, int type, string invToken, string pattern, string seri)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.AdjustReplaceInvWithToken68(_account, _accountpassword, xmlInvData, _username, _password,
                    serialCert, type, invToken, pattern, seri, PublishLink);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.AdjustReplaceInvWithToken " + result);
            return result;
        }

        /// <summary>
        /// Hủy hóa đơn (Token).
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns>Thành công: trả về "OK"</returns>
        public string CancelInvoiceWithToken_Dll(string xmlData, string pattern)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.CancelInvoiceWithToken(_account, _accountpassword, xmlData, _username, _password, pattern, linkWS: PublishLink);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.CancelInvoiceWithToken " + result);
            return result;
        }

        /// <summary>
        /// Lấy lại hash (Token)
        /// </summary>
        /// <param name="xmlFkeyInv"></param>
        /// <returns></returns>
        public string GetHashInv_Dll(string xmlFkeyInv, string SERIAL_CERT, string pattern)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.getHashInv(_account, _accountpassword, _username, _password, SERIAL_CERT, xmlFkeyInv, pattern, PublishLink);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.getHashInv " + result);
            return result;
        }

        /// <summary>
        /// Lấy trạng thái hóa đơn (Token).
        /// </summary>
        /// <param name="xmlFkeyInv"><para></para></param>
        /// <para> (cấu trúc: <Invoices><Inv><key>123</key></Inv><Inv><key>456</key></Inv><Inv><key>789</key></Inv></Invoices> ) (123, 456, 789 là Fkey)</para>
        /// <returns>
        /// <para>Trả về: xml string Cấu trúc: Invoices Inv key123 key Status 0 Status Inv ... Inv...</para>
        /// <para>0: hóa đơn mới tạo, chưa phát hành (những hóa đơn cần lấy lại hash)</para>
        /// <para>1: hóa đơn đã phát hành</para>
        /// <para>2: hóa đơn đã được kê khai thuế cũng như đưa vào các phần mêm kế toán</para>
        /// <para>3: hóa đơn bị thay thế</para>
        /// <para>4: hóa đơn bị điều chỉnh</para>
        /// <para>5: hóa đơn hủy</para>
        /// </returns>
        public string GetStatusInv_Dll(string xmlFkeyInv, string pattern)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.getStatusInv(_account, _accountpassword, _username, _password, xmlFkeyInv, pattern, PublishLink);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.GetStatusInv " + result);
            return result;
        }



        /// <summary>
        /// Insert thông tin chứng thư vào hệ thống.
        /// </summary>
        /// <returns>Thành công: trả về "OK"</returns>
        public string ImportCertWithToken_Dll(string SERIAL_CERT)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.ImportCertWithToken(_account, _accountpassword, _username, _password, SERIAL_CERT, PublishLink);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.ImportCertWithToken " + result);
            return result;
        }

        /// <summary>
        /// Phát hành khi đã lấy lại hash (Token).
        /// </summary>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + ”,”</returns>
        public string PublishInv_Dll(string xmlHash, string SERIAL_CERT, string pattern, string seri)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.PublishInv(_account, _accountpassword, xmlHash, _username, _password, SERIAL_CERT, pattern, seri, PublishLink);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.PublishInv " + result);
            return result;
        }

        /// <summary>
        /// Đẩy lên và phát hành hóa đơn có ký chữ ký số (Token).
        /// </summary>
        /// <param name="xmlInvData">chuỗi xml hóa đơn.</param>
        /// <returns>Thành công: trả về "OK:" + mẫu số + “;” + ký hiệu + “-” + Fkey + “_” + Số hóa đơn + “,”</returns>
        public string PublishInvWithToken68_Dll(string xmlInvData, string SERIAL_CERT, string pattern, string seri)
        {
            string result = null;
            try
            {
                result = VNPTEInvoiceSignToken.PublishInvWithToken68(_account, _accountpassword, xmlInvData, _username, _password, SERIAL_CERT, pattern, seri, PublishLink);
                result += GetResultDescription_Dll(result);
            }
            catch (Exception ex)
            {
                result = "ERR:EX\r\n" + ex.Message;
            }
            Logger.WriteToLog("Program.PublishInvWithToken " + result);
            return result;
        }





        private string GetResultDescription_Dll(string result)
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
        /// Phân tích mã lỗi.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="v6Return"></param>
        private void ReadErrorCode(string result, V6Return v6Return)
        {
            if (v6Return == null) v6Return = new V6Return();

            if (result.StartsWith("ERR:20"))
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
                v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông đủ số hóa đơn cho lô phát hành.";
            }
            else if (result.StartsWith("ERR:5"))
            {
                v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông phát hành được hóa đơn. Lỗi không xác định.";
            }
            else if (result.StartsWith("ERR:3"))
            {
                v6Return.RESULT_ERROR_MESSAGE = result + "\r\nDữ liệu xml đầu vào không đúng quy định.\nKhông có hóa đơn nào được phát hành.";
            }
            else if (result.StartsWith("ERR:2"))
            {
                v6Return.RESULT_ERROR_MESSAGE = result + "\r\nKhông tồn tại hóa đơn cần thay thế.";
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
    }
}

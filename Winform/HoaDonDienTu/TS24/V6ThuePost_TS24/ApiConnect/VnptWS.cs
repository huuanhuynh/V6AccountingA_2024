using System;
using System.Collections.Generic;
using System.IO;

namespace V6ThuePostXmlApi
{
    public class VnptWS
    {
        /// <summary>
        /// Gạch nợ hóa đơn theo lstInvToken(01GTKT2/001;AA/13E;10)
        /// </summary>
        /// <param name="_link_Business"></param>
        /// <param name="lstInvToken">01GTKT2/001;AA/13E;10_????????</param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <param name="rd">Các giá trị trả về.</param>
        /// <returns></returns>
        public static string ConfirmPayment(string _link_Business, string lstInvToken, string userName, string userPass, out IDictionary<string, object> rd)
        {
            string result = null;
            rd = new Dictionary<string, object>();
            try
            {
                result = new BusinessService.BusinessService(_link_Business).confirmPayment(lstInvToken, userName, userPass);
                rd["RESULT_STRING"] = result;
                if (result.StartsWith("ERR")) rd["RESULT_ERROR"] = result;

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
                rd["EXCEPTION_MESSAGE"] = ex.Message;
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
        /// <param name="rd"></param>
        /// <returns></returns>
        public static string ConfirmPaymentFkey(string _link_Business, string fkey_old, string userName, string userPass, out IDictionary<string, object> rd)
        {
            string result = null;
            rd = new Dictionary<string, object>();
            try
            {
                result = new BusinessService.BusinessService(_link_Business).confirmPaymentFkey(fkey_old, userName, userPass);
                rd["RESULT_STRING"] = result;
                rd["RESULT_OBJECT"] = result;
                if (result.StartsWith("ERR:13"))
                {
                    result += "Hóa đơn đã được gạch nợ.";
                    rd["RESULT_ERROR"] = result;
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "Không gạch nợ được.";
                    rd["RESULT_ERROR"] = result;
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "Không tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                    rd["RESULT_ERROR"] = result;
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "Tài khoản đăng nhập sai.";
                    rd["RESULT_ERROR"] = result;
                }
            }
            catch (Exception ex)
            {
                rd["EXCEPTION_MESSAGE"] = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            return result;
        }

        public static string UnconfirmPaymentFkey(string _link_Business, string fkey_old, string userName, string userPass, out IDictionary<string, object> rd)
        {
            string result = null;
            rd = new Dictionary<string, object>();
            try
            {
                result = new BusinessService.BusinessService(_link_Business).UnConfirmPaymentFkey(fkey_old, userName, userPass);
                rd["RESULT_STRING"] = result;
                if (result.StartsWith("ERR")) rd["RESULT_ERROR"] = result;

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
        /// <returns></returns>
        public static string DownloadInvPDFFkey(string linkPortal, string fkey, string userName, string userPass, string saveFolder, out IDictionary<string, object> rd)
        {
            string result = null;
            rd = new Dictionary<string, object>();
            try
            {
                result = new PortalService.PortalService(linkPortal).downloadInvPDFFkey(fkey, userName, userPass);
                rd["RESULT_STRING"] = result;
                if (result.StartsWith("ERR")) rd["RESULT_ERROR"] = result;

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
                    File.WriteAllBytes(path, Convert.FromBase64String(result));
                    rd["PATH"] = path;
                    return path;
                }
            }
            catch (Exception ex)
            {
                rd["EXCEPTION_MESSAGE"] = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            if(result != null)
                throw new Exception(result);
            return null;
        }
    }
}

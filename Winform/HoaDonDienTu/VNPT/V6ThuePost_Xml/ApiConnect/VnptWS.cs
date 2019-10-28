using System;
using System.Collections.Generic;
using System.IO;
using V6ThuePost.ResponseObjects;

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
                if (result.StartsWith("ERR")) v6return.RESULT_ERROR = result;

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
                v6return.EXCEPTION_MESSAGE = ex.Message;
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
                    v6return.RESULT_ERROR = result;
                }
                else if (result.StartsWith("ERR:7"))
                {
                    result += "Không gạch nợ được.";
                    v6return.RESULT_ERROR = result;
                }
                else if (result.StartsWith("ERR:6"))
                {
                    result += "Không tìm thấy hóa đơn tương ứng chuỗi đưa vào.";
                    v6return.RESULT_ERROR = result;
                }
                else if (result.StartsWith("ERR:1"))
                {
                    result += "Tài khoản đăng nhập sai.";
                    v6return.RESULT_ERROR = result;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
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
                if (result.StartsWith("ERR")) v6return.RESULT_ERROR = result;

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
        public static string DownloadInvPDFFkey(string linkPortal, string fkey, string userName, string userPass, string saveFolder, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                result = new PortalService.PortalService(linkPortal).downloadInvPDFFkey(fkey, userName, userPass);
                v6return.RESULT_STRING = result;
                if (result.StartsWith("ERR")) v6return.RESULT_ERROR = result;

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
                    v6return.PATH = path;
                    return path;
                }
            }
            catch (Exception ex)
            {
                v6return.EXCEPTION_MESSAGE = ex.Message;
                result = "ERR:EX\r\n" + ex.Message;
            }

            if(result != null)
                throw new Exception(result);
            return null;
        }
    }
}

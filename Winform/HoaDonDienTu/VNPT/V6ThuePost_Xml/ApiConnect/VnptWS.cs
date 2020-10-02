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
        /// <param name="linkPortal"></param>
        /// <param name="option">0 - Bản pdf thông thường; 1 - Bản pdf chuyển đổi.</param>
        /// <param name="fkey"></param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <param name="saveFolder"></param>
        /// <param name="v6return"></param>
        /// <returns></returns>
        public static string DownloadInvPDFFkey(string linkPortal, int option, string fkey, string userName, string userPass, string saveFolder, out V6Return v6return)
        {
            string result = null;
            v6return = new V6Return();
            try
            {
                if (option == 1)
                {
                    result = new PortalService.PortalService(linkPortal).convertForStoreFkey(fkey, userName, userPass);
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
                    result = new PortalService.PortalService(linkPortal).downloadInvPDFFkey(fkey, userName, userPass);
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

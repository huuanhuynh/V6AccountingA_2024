using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace V6Soft.Web.Accounting.Controllers
{
    public class Crypto
    {

        //public static string KEY_DECRYPT_ENCRYPT = "s";
        /// <summary>
        /// Hàm mã hóa dữ liệu
        /// </summary>
        /// <param name="strEnCrypt">Giá trị cần mã hóa (String)</param>
        /// <param name="key">Chuỗi key nhập vào để làm khóa mã hóa</param>
        /// <returns>Chuỗi được mã hóa</returns>
        public static string EnCrypt(string strEnCrypt)
        {
            if (strEnCrypt != "")
            {
                try
                {
                    byte[] keyArr;
                    byte[] EnCryptArr = UTF8Encoding.UTF8.GetBytes(strEnCrypt);
                    MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                    keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(DateTime.Now.ToString("dd/MM/yyyy")));// KEY_DECRYPT_ENCRYPT));
                    TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                    tripDes.Key = keyArr;
                    tripDes.Mode = CipherMode.ECB;
                    tripDes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] arrResult = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length);
                    return Convert.ToBase64String(arrResult, 0, arrResult.Length);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.EnCrypt : kiểm tra lại tham số");
            }
        }

        /// <summary>
        /// Hàm giải mã dữ liệu
        /// </summary>
        /// <param name="strDecypt">Giá trị cần giải mã (String)</param>
        /// <param name="key">Chuỗi key nhập vào để làm khóa mã hóa</param>
        /// <returns>Chuỗi được giải mã</returns>
        public static string DeCrypt(string strDecypt)
        {

            if (strDecypt != "")
            {
                try
                {
                    byte[] keyArr;
                    byte[] DeCryptArr = Convert.FromBase64String(strDecypt);
                    MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                    keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(DateTime.Now.ToString("dd/MM/yyyy")));//KEY_DECRYPT_ENCRYPT));
                    TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                    tripDes.Key = keyArr;
                    tripDes.Mode = CipherMode.ECB;
                    tripDes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] arrResult = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length);
                    return UTF8Encoding.UTF8.GetString(arrResult);
                }
                catch (Exception e)
                {
                    throw new Exception("UtilityHelper.Decrypt : " + e.Message);
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.DeCrypt : kiểm tra lại tham số");
            }
        }

    }
}
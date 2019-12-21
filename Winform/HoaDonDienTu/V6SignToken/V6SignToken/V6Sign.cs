using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace V6SignToken
{
    public class V6Sign
    {
        /// <summary>
        /// Ký lên chuỗi HASH với USB_TOKEN.
        /// </summary>
        /// <param name="hash">Chuỗi HASH cần ký.</param>
        /// <param name="token_serial_hex">Serial của USB_TOKEN sử dụng.</param>
        /// <returns></returns>
        public string Sign(string hash, string token_serial_hex)
        {
            X509Certificate2 signCert = (X509Certificate2)null;

            string certString = GetCertString(token_serial_hex, out signCert);

            return certString;
        }

        private static string GetCertString(string token_serial, out X509Certificate2 signCert)
        {
            signCert = SelectCertificate(token_serial);
            if (signCert == null)
                return (string)null;
            return Convert.ToBase64String(signCert.RawData);
        }

        /// <summary>
        /// Chọn đúng Token bằng serial, không thấy trả về null.
        /// </summary>
        /// <param name="token_serial"></param>
        /// <returns></returns>
        private static X509Certificate2 SelectCertificate(string token_serial)
        {
            X509Certificate2 x509Certificate2_1 = (X509Certificate2)null;
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            int num = 0;
            x509Store.Open((OpenFlags)num);
            foreach (X509Certificate2 certificate in x509Store.Certificates)
            {
                if (certificate.SerialNumber != null && certificate.SerialNumber.ToUpper() == token_serial.ToUpper())
                {
                    x509Certificate2_1 = certificate;
                    break;
                }
            }
            return x509Certificate2_1;
        }
    }
}

using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace V6AccountingBGenkey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Gen();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            Gen();
        }

        private void Gen()
        {
            if (textBox3.Text == "12345V6" && textBox1.Text != "")
            {
                var seri0 = ConvertHexToString(textBox1.Text);
                var key0 = EnCrypt(seri0);
                textBox2.Text = ConvertStringToHex(key0);
            }
        }

        public static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                try
                {
                    StrValue += Convert.ToChar(Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                    HexValue = HexValue.Substring(2, HexValue.Length - 2);
                }
                catch //(Exception)
                {
                    return "";
                }
            }
            return StrValue;
        }
        public static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:X}", (uint)tmp);
            }
            return hex;
        }
        private string KEY_DECRYPT_ENCRYPT = "MrV6@0936976976";
        /// <summary>
        /// Hàm mã hóa dữ liệu
        /// </summary>
        /// <param name="strEnCrypt">Giá trị cần mã hóa (String)</param>
        /// <returns>Chuỗi được mã hóa</returns>
        private string EnCrypt(string strEnCrypt)
        {
            if (strEnCrypt != "")
            {
                try
                {
                    byte[] keyArr;
                    byte[] EnCryptArr = Encoding.UTF8.GetBytes(strEnCrypt);
                    MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                    keyArr = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(KEY_DECRYPT_ENCRYPT));
                    TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                    tripDes.Key = keyArr;
                    tripDes.Mode = CipherMode.ECB;
                    tripDes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] arrResult = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length);
                    return Convert.ToBase64String(arrResult, 0, arrResult.Length);
                }
                catch// (Exception)
                {
                    return "V6SOFT";
                }
            }
            else
            {
                return "V6SOFT";
            }
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.Utils.Db
{
   public class V6SqlconnectHelper
    {
        //    /// <summary>
        //    /// Hàm backup dữ liệu từ SQLSERVER
        //    /// </summary>
        //    /// <param name="conString">Chuỗi kết nối</param>
        //    /// <param name="databaseName">Tên database</param>
        //    /// <param name="filePath">đường dẫn lưu file</param>
        //    /// <returns></returns>
        //    public static bool BackupData(string conString, string databaseName)
        //    {
        //        if (conString != "" && databaseName != "")
        //        {
        //            try
        //            {
        //                SaveFileDialog objSaveFile = new SaveFileDialog();
        //                objSaveFile.Filter = "Backup file(bak)|*.bak";
        //                if (objSaveFile.ShowDialog() == DialogResult.OK)
        //                {
        //                    Cursor.Current = Cursors.WaitCursor;
        //                    SqlConnection connect = new SqlConnection(conString);
        //                    connect.Open();
        //                    SqlCommand command = new SqlCommand(@"backup database " + databaseName + " to disk = '" + objSaveFile.FileName + "' with init,stats=10", connect);
        //                    command.ExecuteNonQuery();
        //                    connect.Close();
        //                    //MessageBox.Show("Sao lưu hoàn tất !");
        //                    return true;
        //                }
        //                else
        //                    return false;
        //            }
        //            catch (Exception e)
        //            {
        //                throw new Exception("V6SqlconnectHelper.BackupData : " + e.Message);
        //            }
        //        }
        //        else
        //        {
        //            throw new ArgumentException("V6SqlconnectHelper.BackupData : kiểm tra lại tham số");
        //        }
        //    }

        public static string KEY_DECRYPT_ENCRYPT = "MrV6@0936976976";
        /// <summary>
        /// Hàm mã hóa dữ liệu
        /// </summary>
        /// <param name="strEnCrypt">Giá trị cần mã hóa (String)</param>
        /// <param name="key">Chuỗi key nhập vào để làm khóa mã hóa</param>
        /// <returns>Chuỗi được mã hóa</returns>
        //public static string EnCrypt(string strEnCrypt)
        //{
        //    if (strEnCrypt != "")
        //    {
        //        try
        //        {
        //            byte[] enCryptArr = Encoding.UTF8.GetBytes(strEnCrypt);
        //            var md5Hash = new MD5CryptoServiceProvider();
        //            byte[] keyArr = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(KEY_DECRYPT_ENCRYPT));
        //            var tripDes = new TripleDESCryptoServiceProvider
        //            {
        //                Key = keyArr,
        //                Mode = CipherMode.ECB,
        //                Padding = PaddingMode.PKCS7
        //            };
        //            var transform = tripDes.CreateEncryptor();
        //            var arrResult = transform.TransformFinalBlock(enCryptArr, 0, enCryptArr.Length);
        //            return Convert.ToBase64String(arrResult, 0, arrResult.Length);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("V6SqlconnectHelper.EnCrypt : kiểm tra lại tham số");
        //    }
        //}

        /// <summary>
        /// Hàm giải mã dữ liệu
        /// </summary>
        /// <param name="strDecypt">Giá trị cần giải mã (String)</param>
        /// <returns>Chuỗi được giải mã</returns>
        public static string DeCrypt(string strDecypt)
        {

            if (strDecypt != "")
            {
                try
                {
                    byte[] deCryptArr = Convert.FromBase64String(strDecypt);
                    var md5Hash = new MD5CryptoServiceProvider();
                    var keyArr = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(KEY_DECRYPT_ENCRYPT));
                    var tripDes = new TripleDESCryptoServiceProvider
                    {
                        Key = keyArr,
                        Mode = CipherMode.ECB,
                        Padding = PaddingMode.PKCS7
                    };
                    var transform = tripDes.CreateDecryptor();
                    var arrResult = transform.TransformFinalBlock(deCryptArr, 0, deCryptArr.Length);
                    return Encoding.UTF8.GetString(arrResult);
                }
                catch (Exception e)
                {
                    throw new Exception("V6SqlconnectHelper.Decrypt : " + e.Message);
                }
            }
            else
            {
                throw new ArgumentException("V6SqlconnectHelper.DeCrypt : kiểm tra lại tham số");
            }
        }

    }
}

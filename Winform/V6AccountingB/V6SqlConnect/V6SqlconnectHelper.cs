using System;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using V6Structs;

namespace V6SqlConnect
{
    public static class V6SqlconnectHelper
    {
        /// <summary>
        /// Lấy lên cấu trúc bảng dữ liệu.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static V6TableStruct GetTableStruct(string tableName)
        {
            if (string.IsNullOrEmpty(tableName)) throw new Exception("V6ERRORNOTABLE");
            var resultStruct = new V6TableStruct(tableName);

            try
            {
                string sql = "Select ORDINAL_POSITION, COLUMN_NAME" +
                             ", DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT, CHARACTER_MAXIMUM_LENGTH" +
                             ", NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE" +
                             " From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = '" +
                             tableName + "'" +
                             " Order by ORDINAL_POSITION";
                DataTable columnsStructInfo = SqlConnect.ExecuteDataset(CommandType.Text, sql)
                    .Tables[0];

                foreach (DataRow row in columnsStructInfo.Rows)
                {
                    var columnStruct = new V6ColumnStruct
                    {
                        ColumnName = row["COLUMN_NAME"].ToString().Trim(),
                        AllowNull = "YES" == row["IS_NULLABLE"].ToString(),
                        ColumnDefault = (row["COLUMN_DEFAULT"] == null || row["COLUMN_DEFAULT"].ToString().Trim() == "" ) ? null : row["COLUMN_DEFAULT"].ToString().Trim(),
                        sql_data_type_string = row["DATA_TYPE"].ToString().Trim()
                    };
                    try
                    {
                        int num;
                        string stringLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        string numLength = row["NUMERIC_PRECISION"].ToString();
                        string numDecimal = row["NUMERIC_SCALE"].ToString();

                        if (stringLength != "")
                        {
                            num = (int)row["CHARACTER_MAXIMUM_LENGTH"];
                            columnStruct.MaxLength = num;
                        }
                        else if (numLength != "")
                        {
                            //columnStruct.MaxLength = -2;
                            num = Int32.Parse(numLength);
                            columnStruct.MaxNumLength = num;
                            num = Int32.Parse(numDecimal);
                            columnStruct.MaxNumDecimal = num;
                        }

                    }
                    catch
                    {
                        // ignored
                    }

                    resultStruct.Add(columnStruct.ColumnName.ToUpper(), columnStruct);
                }
                return resultStruct;
            }
            catch
            {
                // ignored
            }
            return resultStruct;
        }

        public static V6TableStruct GetTableStruct_C(string conString, string tableName)
        {
            if (string.IsNullOrEmpty(tableName)) throw new Exception("V6ERRORNOTABLE");
            var resultStruct = new V6TableStruct(tableName);

            try
            {
                string sql = "Select ORDINAL_POSITION, COLUMN_NAME" +
                             ", DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT, CHARACTER_MAXIMUM_LENGTH" +
                             ", NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE" +
                             " From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = '" +
                             tableName + "'" +
                             " Order by ORDINAL_POSITION";
                DataTable columnsStructInfo = SqlHelper.ExecuteDataset(conString, CommandType.Text, sql)
                    .Tables[0];

                foreach (DataRow row in columnsStructInfo.Rows)
                {
                    var columnStruct = new V6ColumnStruct
                    {
                        ColumnName = row["COLUMN_NAME"].ToString().Trim(),
                        AllowNull = "YES" == row["IS_NULLABLE"].ToString(),
                        ColumnDefault = (row["COLUMN_DEFAULT"] == null || row["COLUMN_DEFAULT"].ToString().Trim() == "") ? null : row["COLUMN_DEFAULT"].ToString().Trim(),
                        sql_data_type_string = row["DATA_TYPE"].ToString().Trim()
                    };
                    try
                    {
                        int num;
                        string stringLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        string numLength = row["NUMERIC_PRECISION"].ToString();
                        string numDecimal = row["NUMERIC_SCALE"].ToString();

                        if (stringLength != "")
                        {
                            num = (int)row["CHARACTER_MAXIMUM_LENGTH"];
                            columnStruct.MaxLength = num;
                        }
                        else if (numLength != "")
                        {
                            //columnStruct.MaxLength = -2;
                            num = Int32.Parse(numLength);
                            columnStruct.MaxNumLength = num;
                            num = Int32.Parse(numDecimal);
                            columnStruct.MaxNumDecimal = num;
                        }
                    }
                    catch
                    {
                        // ignored
                    }

                    resultStruct.Add(columnStruct.ColumnName.ToUpper(), columnStruct);
                }
                return resultStruct;
            }
            catch
            {
                // ignored
            }
            return resultStruct;
        }

        internal static string Left(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(0, length);
        }
        internal static string Right(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(str.Length - length, length);
        }
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
    //                if (objSaveFile.ShowDialog(null) == DialogResult.OK)
    //                {
    //                    Cursor.Current = Cursors.WaitCursor;
    //                    SqlConnection connect = new SqlConnection(conString);
    //                    connect.Open();
    //                    SqlCommand command = new SqlCommand(@"backup database " + databaseName + " to disk = '" + objSaveFile.FileName + "' with init,stats=10", connect);
    //                    command.ExecuteNonQuery();
    //                    connect.Close();
    //                    //V6Message.Show("Sao lưu hoàn tất !");
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

        private static string KEY_DECRYPT_ENCRYPT = "MrV6@0936976976";
        /// <summary>
        /// Hàm mã hóa dữ liệu
        /// </summary>
        /// <param name="strEnCrypt">Giá trị cần mã hóa (String)</param>
        /// <returns>Chuỗi được mã hóa</returns>
        public static string EnCrypt(string strEnCrypt)
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
                catch
                {
                    return "V6SOFT";
                }
            }
            else
            {
                return "V6SOFT";
            }
        }

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

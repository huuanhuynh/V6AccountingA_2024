using System;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace V6Tools
{
    public class UtilityHelper
    {
        public static string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        public static string GetValueFromV6Lookup(string ConnectionString, string vVar, string v6LookUpField)
        {
            if (vVar.Trim() == "" || vVar == null)
            {
                throw new Exception("vVar rỗng!");
            }
            if (v6LookUpField.Trim() == "" || v6LookUpField == null)
            {
                throw new Exception("v6LookUpField rỗng!");
            }
            try
            {
                string strReturn = null;
                string strCommand = "select top 1 " + v6LookUpField + " from V6Lookup where vVar = '" + vVar + "'";
                SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, strCommand);
                if (reader.Read())
                {
                    strReturn = reader[v6LookUpField].ToString();
                }
                return strReturn.Trim();
            }
            catch (Exception ex)
            {
                MessageBox. Show("GetValueFromV6Lookup: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Hàm backup dữ liệu từ SQLSERVER
        /// </summary>
        /// <param name="owner">Form hoặc control chủ gọi hàm này.</param>
        /// <param name="conString">Chuỗi kết nối</param>
        /// <param name="databaseName">Tên database</param>
        /// <returns></returns>
        public static bool BackupData(IWin32Window owner, string conString, string databaseName)
        {
            if (conString != "" && databaseName != "")
            {
                try
                {
                    SaveFileDialog objSaveFile = new SaveFileDialog();
                    objSaveFile.Filter = "Backup file(bak)|*.bak";
                    if (objSaveFile.ShowDialog(owner) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        SqlConnection connect = new SqlConnection(conString);
                        connect.Open();
                        SqlCommand command = new SqlCommand(@"backup database " + databaseName + " to disk = '" + objSaveFile.FileName + "' with init,stats=10", connect);
                        command.ExecuteNonQuery();
                        connect.Close();
                        MessageBox. Show("Sao lưu hoàn tất !");
                        return true;
                    }
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("UtilityHelper.BackupData : " + e.Message);
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.BackupData : kiểm tra lại tham số");
            }
        }

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
                catch// (Exception)
                {
                    return "V6SOFT";
                }
            }
            else
            {
                return "V6SOFT";
                //throw new ArgumentException("UtilityHelper.EnCrypt : kiểm tra lại tham số");
            }
        }

        /// <summary>
        /// Hàm mã hóa dữ liệu
        /// </summary>
        /// <param name="strEnCrypt">Giá trị cần mã hóa (String)</param>
        /// <param name="key">Chuỗi key nhập vào để làm khóa mã hóa</param>
        /// <returns>Chuỗi được mã hóa</returns>
        public static string EnCrypt(string strEnCrypt, string key)
        {
            if (strEnCrypt != "")
            {
                try
                {
                    byte[] keyArr;
                    byte[] EnCryptArr = Encoding.UTF8.GetBytes(strEnCrypt);
                    MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                    keyArr = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(key));
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
                //throw new ArgumentException("UtilityHelper.EnCrypt : kiểm tra lại tham số");
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
                    byte[] keyArr;
                    byte[] DeCryptArr = Convert.FromBase64String(strDecypt);
                    MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                    keyArr = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(KEY_DECRYPT_ENCRYPT));
                    TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                    tripDes.Key = keyArr;
                    tripDes.Mode = CipherMode.ECB;
                    tripDes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] arrResult = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length);
                    return Encoding.UTF8.GetString(arrResult);
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.DeCrypt : kiểm tra lại tham số");
            }
        }

        /// <summary>
        /// Hàm giải mã dữ liệu
        /// </summary>
        /// <param name="strDecypt">Giá trị cần giải mã (String)</param>
        /// <param name="key">Chuỗi key nhập vào để làm khóa mã hóa</param>
        /// <returns>Chuỗi được giải mã</returns>
        public static string DeCrypt(string strDecypt, string key)
        {
            if (strDecypt != "")
            {
                try
                {
                    byte[] DeCryptArr = Convert.FromBase64String(strDecypt);
                    MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                    var keyArr = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                    TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                    tripDes.Key = keyArr;
                    tripDes.Mode = CipherMode.ECB;
                    tripDes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] arrResult = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length);
                    return Encoding.UTF8.GetString(arrResult);
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.DeCrypt : kiểm tra lại tham số");
            }
        }


        /// <summary>
        /// Hàm lấy chuỗi kết nối từ file config
        /// </summary>
        /// <param name="fileName">Tên file config</param>
        /// <returns>Trả về chuỗi kết nối</returns>
        public static string ReadConnectionStringFromFileXML(string fileName)
        {
            if (fileName != "")
            {
                string strConnection = "";
                string strConnectionPass = "";
                string strDecryptedConnectionPass = "";
                XmlTextReader reader = new XmlTextReader(fileName.ToLower());
                try
                {
                    while (reader.Read())
                    {
                        XmlNodeType nType = reader.NodeType;
                        if (nType == XmlNodeType.Element)
                        {
                            if (reader.Name == "ConnectionString")
                                strConnection = reader.GetAttribute("value");
                            if (reader.Name == "ConnectionPassword")
                                strConnectionPass = reader.GetAttribute("value");
                        }
                    }
                    if (strConnectionPass != null)
                        strDecryptedConnectionPass = DeCrypt(strConnectionPass.Trim());
                    reader.Close();
                }
                catch (Exception e)
                {
                    reader.Close();
                    throw new Exception("UtilityHelper.ReadConnectionStringFromFileXML : " + e.Message);
                }
                //Giải mã password connection
                return strConnection + strDecryptedConnectionPass;
            }
            else
            {
                throw new ArgumentException("UtilityHelper.ReadConnectionStringFromFileXML : Tên file \"config.xml\" không hợp lệ hoặc không tìm thấy file");
            }
        }
        
        public static string GetConnectionFromXML(string filePath)
        {
            DataTable table =  new DataTable();
            XmlTextReader xr = new XmlTextReader(filePath);
            
            string strCon = "", strPass = "";
            while (xr.Read())
            {
                switch (xr.Name)
                {
                    case "ConnectionString":
                        strCon = xr.GetAttribute("value");
                        break;
                    case "ConnectionPassword":
                        strPass = xr.GetAttribute("value");
                        break;
                    default:
                        break;
                }
                if (strCon != "" && strPass != "") break;
            }

            xr.Close();
            //table = LoadXMLToDataTable(filePath);
            //string strCon = table.Rows[0]["value"].ToString();
            //string strPass = table.Rows[1]["value"].ToString();
            string strDecryptedConnectionPass = DeCrypt(strPass.Trim());
            return strCon + strDecryptedConnectionPass;
        }

        #region ===Chuyễn mã TCVN3 qua UNICODE===
        /// <summary>
        /// Khai báo các ký tự trong bảng mã TCVN3
        /// </summary>
        private static char[] tcvnchars = {
            'µ', '¸', '¶', '·', '¹',
            '¨', '»', '¾', '¼', '½', 'Æ',
            '©', 'Ç', 'Ê', 'È', 'É', 'Ë',
            '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ',
            'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö',
            '×', 'Ý', 'Ø', 'Ü', 'Þ',
            'ß', 'ã', 'á', 'â', 'ä',
            '«', 'å', 'è', 'æ', 'ç', 'é',
            '¬', 'ê', 'í', 'ë', 'ì', 'î',
            'ï', 'ó', 'ñ', 'ò', 'ô',
            '*', 'õ', 'ø', 'ö', '÷', 'ù',
            'ú', 'ý', 'û', 'ü', 'þ',
            '¡', '¢', '§', '£', '¤', '¥', '¦'
            };

        /// <summary>
        /// Khai báo các ký tự trong bảng mã UNICODE
        /// </summary>
        private static char[] unichars = {
            'à', 'á', 'ả', 'ã', 'ạ',
            'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ',
            'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ',
            'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ',
            'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ',
            'ì', 'í', 'ỉ', 'ĩ', 'ị',
            'ò', 'ó', 'ỏ', 'õ', 'ọ',
            'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ',
            'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ',
            'ù', 'ú', 'ủ', 'ũ', 'ụ',
            'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự',
            'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ',
            'ă', 'â', 'đ', 'ê', 'ô', 'ơ', 'ư'
            };

        /// <summary>
        /// Mảng trung gian dùng để chứa các ký tự được convert
        /// </summary>
        private static char[] convertTable;

        /// <summary>
        /// Hàm chuyển từ mã TCVN3 sang UNICODE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TCVN3ToUnicode(string value)
        {
            if (value != "")
            {
                try
                {
                    convertTable = new char[256];
                    for (int i = 0; i < 256; i++)
                        convertTable[i] = (char)i;
                    for (int i = 0; i < tcvnchars.Length; i++)
                        convertTable[tcvnchars[i]] = unichars[i];

                    char[] chars = value.ToCharArray();
                    for (int i = 0; i < chars.Length; i++)
                        if (chars[i] < (char)256)
                            chars[i] = convertTable[chars[i]];
                    return new string(chars);
                }
                catch (Exception e)
                {
                    throw new Exception("UtilityHelper.TCVN3ToUnicode : " + e.Message);
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.TCVN3ToUnicode : kiểm tra lại tham số");
            }
        }
        #endregion ===Chuyễn mã TCVN3 qua UNICODE===

        /// <summary>
        /// Đọc giá trị lưu trong registry
        /// </summary>
        /// <param name="KeyName">Tên biến trong registry)</param>
        /// <returns>Giá trị kiểu chuỗi</returns>
        public static string ReadRegistry(string KeyName)
        {
            if (KeyName != "")
            {
                // Opening the registry key
                RegistryKey rk = Registry.CurrentUser;
                // Open a subKey as read-only
                RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\V6SOFT");
                // If the RegistrySubKey doesn""t exist -> (null)
                if (sk1 == null)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        // If the RegistryKey exists I get its value
                        // or null is returned.
                        return (string)sk1.GetValue(KeyName.ToUpper());
                    }
                    catch (Exception e)
                    {
                        throw new Exception("UtilityHelper.ReadRegistry : " + e.Message);
                    }
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.ReadRegistry : kiểm tra lại tham số");
            }
        }

        public static bool CheckRegistry(string KeyName)
        {
            if (KeyName != "")
            {
                // Opening the registry key
                RegistryKey rk = Registry.CurrentUser;
                // Open a subKey as read-only
                RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\V6SOFT");
                // If the RegistrySubKey doesn""t exist -> (null)
                if (sk1 == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        // If the RegistryKey exists I get its value
                        // or null is returned.
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw new Exception("UtilityHelper.ReadRegistry : " + e.Message);
                    }
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.ReadRegistry : kiểm tra lại tham số");
            }
        }
        
        /// <summary>
        /// Ghi giá trị vào Registry để thiết lập biến ngôn ngữ
        /// </summary>
        /// <param name="KeyName">Tên biến chứa giá trị</param>
        /// <param name="Value">Giá trị cần thiết lập</param>
        /// <returns>Trạng thái thiết lập (true - false)</returns>
        public static bool WriteRegistry(string KeyName, string Value)
        {
            if (KeyName != "" && Value != "")
            {
                try
                {
                    // Setting
                    RegistryKey rk = Registry.CurrentUser; //Ghi vào thẻ CURRENT_USER
                    // I have to use CreateSubKey 
                    // (create or open it if already exits), 
                    // ""cause OpenSubKey open a subKey as read-only
                    RegistryKey sk1 = rk.CreateSubKey(@"SOFTWARE\V6SOFT");
                    //RegistryKey sk1 = rk.OpenSubKey("SOFTWARE\\V6SOFT");
                    // Save the value
                    if (sk1 != null) sk1.SetValue(KeyName.ToUpper(), Value);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox. Show("UtilityHelper.WriteRegistry :" + e.Message);
                    return false;
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.WriteRegistry : kiểm tra lại tham số");
            }
        }

        /// <summary>
        /// Hàm kiểm tra chuỗi không dấu
        /// </summary>
        /// <param name="strValue">Chuỗi kiểm tra</param>
        /// <returns>true - nếu chuỗi không có dấu</returns>
        /// <returns>false - nếu chuỗi có dấu</returns>
        public static bool MatchString(string strValue)
        {
            if (strValue != "")
            {
                try
                {
                    string pattern = "[A-Z]";
                    Regex myRegex = new Regex(pattern);
                    int i = 0;
                    while (i < strValue.Length)
                    {
                        string str = strValue[i].ToString();
                        if (String.Compare(str, " ", StringComparison.Ordinal) == 0)
                        {
                            i++;
                            continue;
                        }
                        Match m = myRegex.Match(str);
                        i++;
                        if (!m.Success)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception("UtilityHelper.MatchString : " + e.Message);
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.MatchString : kiểm tra lại tham số");
            }
        }

        ///// <summary>
        ///// Hàm ghi file log
        ///// </summary>
        ///// <param name="error">Lỗi</param>
        //public static void Log(string error, string fileLogName)
        //{
        //    if (!File.Exists(fileLogName)) // Kiểm tra file log tồn tại chưa, nếu chưa thì tạo trước khi ghi
        //    {
        //        StreamWriter sw = new StreamWriter(fileLogName);
        //        sw.Flush();
        //        sw.Close();
        //        sw.Dispose();
        //        XElement xml = new XElement("Logging",
        //                            new XElement("CreationDate", DateTime.Now.ToString("dd-MM-yyyy"))
        //                            );
        //        xml.Save(fileLogName);
        //    }
        //    XDocument xmlDoc = XDocument.Load(fileLogName);
        //    xmlDoc.Element("Logging").Add(
        //                                    new XElement("Logs",
        //                                        new XElement("Date", DateTime.Now.ToString("dd-MM-yyyy * HH:mm:ss")),
        //                                        new XElement("Message", error))
        //                                );
        //    xmlDoc.Save(fileLogName);
        //}

        ///// <summary>
        ///// Hàm tạo file log
        ///// </summary>
        //public static void CreateFileLog()
        //{
        //    if (!File.Exists("logfile.xml"))
        //    {
        //        StreamWriter sw = new StreamWriter("logfile.xml");
        //        sw.Flush();
        //        sw.Close();
        //        sw.Dispose();
        //        XElement xml = new XElement("Logging",
        //                            new XElement("CreationDate", DateTime.Now.ToString("dd-MM-yyyy"))
        //                            );
        //        xml.Save("logfile.xml");
        //    }
        //}

        /// <summary>
        /// Cú pháp giải nén : ("x -o+ 'updateFolder' 'updateFolder' ") ;
        /// Cú pháp nén : (a 'targetFileName')
        /// Cú pháp giải nén tất cả file đến thư mục hiện tại(orverride file) : ("e -o+ 'sourceFileName' ");
        /// Tham khảo thêm trong file .chm trong thư mục Winrar
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="argument"></param>
        /// <param name="isShowMessage"></param>
        public static void CarryOutProgramUsingProcess(string programName, string argument, bool isShowMessage)
        {
            try
            {
                Process ProcessObj = new Process();
                // StartInfo contains the startup information of
                // the new process
                ProcessObj.StartInfo.FileName = programName;
                ProcessObj.StartInfo.Arguments = argument;
                // These two optional flags ensure that no DOS window
                // appears
                ProcessObj.StartInfo.UseShellExecute = false;
                ProcessObj.StartInfo.CreateNoWindow = true;

                //ProcessObj.StartInfo.WindowStyle = ProcessWindowStyle.Hidden


                // This ensures that you get the output from the DOS application
                ProcessObj.StartInfo.RedirectStandardOutput = true;
                // Start the process
                ProcessObj.Start();
                // Wait that the process exits
                ProcessObj.WaitForExit();
                // Now read the output of the DOS application
                if (isShowMessage)
                {
                    string Result = ProcessObj.StandardOutput.ReadToEnd();
                    MessageBox. Show(Result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UtilityHelper.CarryOutProgramUsingProcess : " + ex.Message);
            }
        }

        //public static DataTable LoadXMLToDataTable(string fileName)
        //{
        //    if (fileName != "")
        //    {
        //        try
        //        {
        //            XElement x = XElement.Load(fileName);
        //            DataTable dt = new DataTable();
        //            XElement setup = (from p in x.Descendants() select p).First();
        //            foreach (XElement xe in setup.Descendants()) // build your DataTable
        //                dt.Columns.Add(new DataColumn(xe.Name.ToString(), typeof(string))); // add columns to your dt
        //            var all = from p in x.Descendants(setup.Name.ToString()) select p;
        //            foreach (XElement xe in all)
        //            {
        //                DataRow dr = dt.NewRow();
        //                foreach (XElement xe2 in xe.Descendants())
        //                    dr[xe2.Name.ToString()] = xe2.Value; //add in the values
        //                dt.Rows.Add(dr);
        //            }
        //            return dt;
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("UtilityHelper.LoadXMLToDataTable : " + e.Message);
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("UtilityHelper.LoadXMLToDataTable : kiểm tra lại tham số ");
        //    }
        //}

        /// <summary>
        /// Hàm đưa dữ liệu vào database theo cơ chế Bulk
        /// </summary>
        /// <param name="conString">chuỗi kết nối</param>
        /// <param name="destinationTable">tên bảng cần đưa dữ liệu vào</param>
        /// <param name="table">dữ liệu cần thêm</param>
        /// <returns>trạng thái true-false</returns>
        public static bool PushDataTableIntoDB(string conString, string destinationTable, DataTable table)
        {
            if (conString != "" && destinationTable != "" && table != null)
            {
                try
                {
                    // Bulk Copy to SQL Server
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conString))
                    {
                        bulkCopy.DestinationTableName = destinationTable;
                        bulkCopy.WriteToServer(table);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("UtilityHelper.PushDataTableIntoDB : " + e.Message);
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.PushDataTableIntoDB : kiểm tra lại tham số ");
            }
        }

        /// <summary>
        /// Hàm thực thi 1 chuỗi chương trình, để tạo ra file thực thi(.exe) hoặc file không thực thi(.dll)
        /// </summary>
        /// <param name="strProgram">Chuỗi chương trình</param>
        /// <param name="programName">Tên đặt cho chương trình tạo ra</param>
        /// <param name="isExecutable">Chương trình thực thi được hay không?</param>
        /// <param name="rtbErrorContainer">Richtextbox để chứa lỗi, sử dụng trong trường hợp fix bug</param>
        /// <returns></returns>
        public static bool ExecuteString(string strProgram, string programName, bool isExecutable , RichTextBox rtbErrorContainer)
        {
            if (strProgram != "" && programName != "")
            {
                try
                {
                    CSharpCodeProvider provider = new CSharpCodeProvider();
                    CompilerParameters cp = new CompilerParameters();
                    cp.GenerateExecutable = isExecutable;
                    //Nếu người dùng quên phần mở rộng thì kiểm tra và thêm vào
                    if (isExecutable)
                    {
                        if (programName.ToLower().Contains(".exe"))
                            cp.OutputAssembly = programName; //"VariablesLibrary.exe";
                        else
                            cp.OutputAssembly = programName + ".exe"; //"VariablesLibrary.exe";
                    }
                    else
                    {
                        if (programName.ToLower().Contains(".dll"))
                            cp.OutputAssembly = programName; //"VariablesLibrary.dll";
                        else
                            cp.OutputAssembly = programName + ".dll"; //"VariablesLibrary.dll";
                    }
                    //cp.GenerateInMemory = false;
                    CompilerResults cr = provider.CompileAssemblyFromSource(cp, strProgram);
                    if (cr.Errors.Count > 0)
                    {
                        if (rtbErrorContainer != null)
                        {
                            foreach (CompilerError error in cr.Errors)
                            {
                                rtbErrorContainer.AppendText(error + Environment.NewLine);
                            }
                        }
                        return false;
                    }
                    else
                    {
                        if (rtbErrorContainer != null)
                        {
                            rtbErrorContainer.AppendText("Đã tạo thành công : " + programName);
                        }
                        return true;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("UtilityHelper.ExecuteString : " + e.Message);
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.ExecuteString : kiểm tra lại tham số" );
            }
        }

        public static string EncryptedUserName(string username, int id)
        {
            //string result = "";
            //string sT = "";
            //for (int i = 0; i < 64; i++)
            //{
            //    sT = sT + (Convert.ToChar(i));
            //}
            //if (username == String.Empty)
            //    return sT;
            //if (username == sT)
            //    return "";
            //for (int i = 0; i < username.Length; i++)
            //{
            //    result += Convert.ToChar(Convert.ToInt32(username[i]) + id);
            //}
            //return result;

            string result = "";
            string sT = "";
            for (int i = 1; i <= 64; i++)
            {
                sT = sT + (Convert.ToChar(i));
            }
            if (username == String.Empty)
                return sT;
            if (username == sT)
                return "";
            for (int i = 0; i < username.Length; i++)
            {
                result += Convert.ToChar(username[i] + id);
            }
            return result;
        }
        
        /// <summary>
        /// Kiểm tra xem chương trình có được đăng ký vào startup của windows ?
        /// </summary>
        /// <param name="programName">Tên chương trình</param>
        /// <returns>True - false</returns>
        public static bool CheckProgramStartUp(string programName)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp.GetValue(programName) == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                return false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                return true;
            }
        }

        /// <summary>
        /// Set hoặc unset chương trình trong registry
        /// </summary>
        /// <param name="programName">Tên chương trình</param>
        /// <param name="isRunAtStartup">Tham số để set startup (true - false)</param>
        public static void SetOrUnSetStartUpProgram(string programName, bool isRunAtStartup)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isRunAtStartup)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue(programName, Application.ExecutablePath);
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue(programName, false);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

//XCOPY /S /Y /I /c $(TargetDir)* Z:\
//XCOPY /S /Y /I /c $(TargetDir)* E:\Copy\Code\V6AccountingB\EXE\
namespace V6AccountingB
{
    internal static class Program
    {
        //private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 6666;
        //static ASCIIEncoding encoding = new ASCIIEncoding();
        //private static int instances = 0;
        //private static int result = 0;
        //private static int timeOut = 550;
        //private static int ttl = 5;

        /// <summary>
        /// Lấy số seri chương trình từ server. Đòi hỏi phải có service chạy trước.
        /// </summary>
        /// <param name="serverIp">IP hoac Name</param>
        /// <param name="localPath"></param>
        /// <returns></returns>
        private static string GetServerSeri(string serverIp, string localPath)
        {
            var result = "";
            try
            {
                TcpClient client = new TcpClient();
                //string serverIp = comboBox1.Text;
                // 1. connect
                client.Connect(serverIp, PORT_NUMBER);
                Stream stream = client.GetStream();
                string host = V6Login.ClientName;
                //Console.WriteLine("Connected to Y2Server.");
                while (true)
                {
                    var reader = new StreamReader(stream);
                    var writer = new StreamWriter(stream);
                    writer.AutoFlush = true;

                    // 2. send computername + dir(in file)
                    writer.WriteLine(host+ ";" + localPath);

                    // 3. receive
                    var str = reader.ReadLine();
                    //Console.WriteLine(str);
                    if (!string.IsNullOrEmpty(str))
                    {
                        result = str;
                        break;
                    }
                }

                // 4. close
                stream.Close();
                client.Close();
            }
            catch// (Exception ex)
            {
                result = "";
            }
            return result;
        }

        private static bool IsNetworkPath(string path)
        {
            path = Path.GetFullPath(path);
            if (path.StartsWith(@"\\")) return true;
            
            var drivers = DriveInfo.GetDrives();

            for (int i = drivers.Length-1; i >= 0; i--)
            {
                var driver = drivers[i];
                if (path.StartsWith(driver.Name))
                {
                    if (driver.DriveType == DriveType.Network) return true;
                }
            }
            
            return false;
        }
        public static bool WriteV6DirFile(string dir)
        {
            try
            {
                if (!File.Exists(Path.Combine(dir, "V6dir")))
                {
                    string fileName = "V6dir";
                    {
                        FileStream fs = new FileStream(fileName, FileMode.Create);
                        try
                        {
                            StreamWriter sw = new StreamWriter(fs);
                            sw.WriteLine(dir);
                            sw.Close();
                        }
                        catch
                        {
                            fs.Close();
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }
        public static string ReadV6dir()
        {
            string result = "";
            string fileName = "V6dir";
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                try
                {
                    StreamReader sr = new StreamReader(fs);
                    result = sr.ReadLine();
                }
                catch
                {
                    result = "";
                }
                fs.Close();
            }
            return result;
        }

        private static bool CheckAllow()
        {
            return true;
            var xtbfile = "Clients.xtb";
            var _message = "";
            try
            {

                xtbfile = Path.Combine(V6Login.StartupPath, xtbfile);
                DataTable data = new DataTable("Clients");
                if (File.Exists(xtbfile))
                {
                    try
                    {
                        var ds = new DataSet();
                        ds.ReadXml(xtbfile);
                        data = ds.Tables[0];
                        //data.Load(xtbfile,);
                    }
                    catch (Exception ex)
                    {
                        _message += "read xml ex: " + ex.Message;
                        _message += "\nAuto create new xml!";
                        data.Columns.Add("Name", typeof(string));
                        data.Columns.Add("Allow", typeof(string));
                    }
                }
                else
                {
                    data.Columns.Add("Name", typeof(string));
                    data.Columns.Add("Allow", typeof(string));
                }
                var check = false;
                var have = false;
                foreach (DataRow row in data.Rows)
                {
                    if (row["Name"].ToString().Trim().ToUpper() == V6Login.ClientName)
                    {
                        have = true;
                        var allow = row["Allow"].ToString().Trim();
                        check = allow == "1";
                        if (check)
                        {
                            break;
                        }
                    }
                }
                if (check)
                {
                    return true;
                }

                if (!have)
                {
                    DataRow row = data.NewRow();
                    row["Name"] = V6Login.ClientName;
                    row["Allow"] = "0";
                    data.Rows.Add(row);
                    data.WriteXml(Path.Combine(V6Login.StartupPath, xtbfile), true);
                }

            }
            catch (Exception ex)
            {
                _message += "CheckAllowConnect ex: " + ex.Message;
                var length = _message.Length;
                //Program.Log(_message);
                V6ControlFormHelper.ShowErrorMessage(_message);
                //_message = _message.Substring(length);
            }
            return false;
        }

        //internal static bool CheckLicenseV6Online(string seri, string key)
        //{
        //    try
        //    {
        //        SqlParameter[] prList =
        //        {
        //            new SqlParameter("@name", V6Login.ClientName), 
        //            new SqlParameter("@seri", seri), 
        //            new SqlParameter("@key", key), 
        //        };
        //        var data = SqlConnect.Select("V6ONLINES", "*", "name=@name and seri=@seri and [key]=@key", "", "", prList).Data;
        //        if (data != null && data.Rows.Count == 1)
        //        {
        //            var row = data.Rows[0].ToDataDictionary();

        //            var seri0 = License.ConvertHexToString(seri);
        //            var mahoa_seri0 = UtilityHelper.EnCrypt(seri0);
        //            var key0 = License.ConvertHexToString(key);
        //            var check_seri = mahoa_seri0 == key0;

        //            var allow = 1 == ObjectAndString.ObjectToInt(row["ALLOW"]);
        //            var eCodeName = (row["CODE_NAME"] ?? "").ToString().Trim();
        //            var rCodeName = eCodeName == "" ? "" : UtilityHelper.DeCrypt(eCodeName);
        //            //var allow = row["Allow"].ToString().Trim();
        //            var checkCode = row["CHECKCODE"].ToString().Trim();
        //            var is_allow =
        //                    allow
        //                    && rCodeName.Length > V6Login.ClientName.Length + 1
        //                    && rCodeName.StartsWith(V6Login.ClientName)
        //                    && rCodeName.Substring(V6Login.ClientName.Length, 1) == "1"
        //                    && rCodeName.EndsWith(checkCode);

        //            return allow && check_seri && is_allow;
        //        }
        //        else
        //        {
        //            Program.InsertLicenseV6Online(seri, key);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteToLog(ex.Message);
        //    }
        //    return false;
        //}

        //internal static void InsertLicenseV6Online(string seri, string key)
        //{
        //    //SqlConnect.StartSqlConnect("V6Soft", Application.StartupPath);
        //    var checkCode = SqlConnect.GetServerDateTime().ToString("yyyyMMddHH:mm:ss");
        //    var NAME = V6Login.ClientName;
        //    IDictionary<string, object> data = new SortedDictionary<string, object>();
        //    IDictionary<string, object> keys = new SortedDictionary<string, object>();
        //    data.Add("NAME", NAME);
        //    data.Add("SERI", seri);
        //    data.Add("KEY", key);
        //    keys.AddRange(data);
        //    data.Add("CHECKCODE", checkCode);
        //    data.Add("CODE_NAME", UtilityHelper.EnCrypt(NAME + "0" + checkCode));
            
        //    var d = V6BusinessHelper.Delete("V6ONLINES", keys);
        //    bool b = V6BusinessHelper.Insert("V6ONLINES", data);
        //}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            V6Login.StartupPath = Application.StartupPath;
            V6Login.ClientName = Dns.GetHostName().ToUpper();

            var seri = "!";
            var key = "";
            var allow = false;

            //Check o dia mang
            if (IsNetworkPath(V6Login.StartupPath)) //Chay link server
            {
                V6Login.IsNetwork = true;
                if (SqlConnect.CheckServer == "1")
                {
                    seri = "?";
                    var dir = ReadV6dir();
                    seri = GetServerSeri(SqlConnect.ServerIP, dir);
                }
                else
                {
                    seri = "allow";
                    //Check allow
                    allow = CheckAllow();
                }
            }
            else
            {
                V6Login.IsNetwork = false;
                //Tao file V6dir
                WriteV6DirFile(V6Login.StartupPath);
            }

            if (seri == "!" && !V6Login.StartupPath.StartsWith(@"\\"))
            {
                seri = License.GetSeri(V6Login.StartupPath);
            }

            if (seri == "")
            {
                V6ControlFormHelper.ShowInfoMessage(V6Setting.IsVietnamese ? "Kiểm tra máy chủ!" : "Check server!");
                return;
            }

            if (seri != "allow")
                key = License.ReadLicenseKey(V6Login.StartupPath);

            if (seri == "allow" && allow)
            {
                var f = new FormLogin();
                if (f.ShowDialog(null) == DialogResult.OK && f.ReadyLogin)
                {
                    try
                    {
                        var mf = new MainForm();
                        Application.Run(mf);
                    }
                    catch (Exception ex)
                    {
                        V6ControlFormHelper.ShowErrorMessage(
                            " Main:\r\nRunning: " + V6ControlFormHelper.RunningListString
                            + "\r\nLastAction: " + V6ControlFormHelper.LastActionListString
                            + "\r\nException: " + ex.Message + " " + ex.StackTrace);
                    }
                }
            }
            else if (seri == "allow")
            {
                V6ControlFormHelper.ShowInfoMessage(V6Setting.IsVietnamese ? "Kiểm tra máy chủ!" : "Check server!");
                return;
            }
            else if (License.CheckLicenseKey(seri, key) || new FormKey(seri).ShowDialog(null) == DialogResult.OK)
                //var checkLicenseKeyOk = License.CheckLicenseKey(seri, key);
                //if (checkLicenseKeyOk)
            {
                var f = new FormLogin();
                if (f.ShowDialog(null) == DialogResult.OK && f.ReadyLogin)
                {
                    try
                    {
                        var mf = new MainForm();
                        Application.Run(mf);
                    }
                    catch (Exception ex)
                    {
                        V6ControlFormHelper.ShowErrorMessage(
                            " Main:\r\nRunning: " + V6ControlFormHelper.RunningListString
                            + "\r\nLastAction: " + V6ControlFormHelper.LastActionListString
                            + "\r\nException: " + ex.Message + " " + ex.StackTrace);
                    }
                }
            }
            //else // Enter License
            //{
            //    while (new FormKey(seri).ShowDialog(null) == DialogResult.No)
            //    {
            //        ;
            //    }
            //}
        }

    }
}

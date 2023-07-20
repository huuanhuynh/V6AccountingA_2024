using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using V6Tools.V6Convert;

namespace V6SqlConnect
{
    public static class DatabaseConfig
    {
        private static string _v6Key, check_key;
        private static string _iniLocation;
        private static bool _loaded;
        public static bool Loaded { get { return _loaded; } }
        public static void LoadDatabaseConfig(string v6Key, string iniLocation)
        {
            _v6Key = v6Key;
            check_key = CheckV6Key() ? "" : "Â" + _v6Key;
            _iniLocation = iniLocation;
            //ReadIniInfo();
            ReadConfigData();
            _loaded = true;
        }

        private const string ConfigFile = "V6DatabaseConfig.xml";
        public static DataTable ConnectionConfigData;
        public static DataTable ServerConfigData;

        /// <summary>
        /// Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword;
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                var s = "Server=" + Server + ";Database=" + Database + ";User Id=" + UserId
                    + ";Password=" + V6SqlconnectHelper.DeCrypt(EPassword + check_key) + ";";

                return s;
            }
        }
        
        public static string ConnectionString2_TH
        {
            get
            {
                try
                {
                    if (EPassword2_TH == "") return "";
                    var s = "Server=" + Server2_TH + ";Database=" + Database2_TH + ";User Id=" + UserId2_TH
                            + ";Password=" + V6SqlconnectHelper.DeCrypt(EPassword2_TH + check_key) + ";";

                    return s;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        
        public static string ConnectionString3_TH
        {
            get
            {
                try
                {
                    if (EPassword3_TH == "") return "";
                    var s = "Server=" + Server3_TH + ";Database=" + Database3_TH + ";User Id=" + UserId3_TH
                            + ";Password=" + V6SqlconnectHelper.DeCrypt(EPassword3_TH + check_key) + ";";

                    return s;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static string ConnectionString_IPSR
        {
            get
            {
                var s = "Server=" + IPSRV6Server + ";Database=" + IPSRV6Database + ";User Id=" + IPSRV6UserId
                    + ";Password=" + V6SqlconnectHelper.DeCrypt(IPSRV6EPassword + check_key) + ";";

                return s;
            }
        }

        public static string ConfigDataDisplayMember = "Display";
        public static string ConfigDataValueMember = "STT";

        public static int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                GetConfigValues(_selectedIndex);
            }
        }
        private static int _selectedIndex;

        public static void GetConfigValues(int index)
        {
            try
            {
                var selectedRow = ConnectionConfigData.Rows[index];
                STT = selectedRow["Stt"].ToString().Trim();
                Server = selectedRow["Server"].ToString().Trim();
                Database = selectedRow["Database"].ToString().Trim();
                UserId = selectedRow["UserId"].ToString().Trim();
                EPassword = selectedRow["EPassword"].ToString().Trim();
                Api = selectedRow["Api"].ToString().Trim();
                Key = selectedRow["Key"].ToString().Trim();

                Server2 = selectedRow["Server2"].ToString().Trim();
                Database2 = selectedRow["Database2"].ToString().Trim();
                UserId2 = selectedRow["UserId2"].ToString().Trim();
                EPassword2 = selectedRow["EPassword2"].ToString().Trim();
                Api2 = selectedRow["Api2"].ToString().Trim();
                if (ConnectionConfigData.Columns.Contains("Key2")) Key2 = selectedRow["Key2"].ToString().Trim();

                var ip_constring = "";
                if (ConnectionConfigData.Columns.Contains("IPServer"))
                {
                    // Kiểm tra IPServer (nơi trungg gian ghi nhận lưu trữ IP cho các máy chủ khác nhau) có kết nối thì đổi Server.
                    string IPName = selectedRow["IPName"].ToString().Trim();
                    string IPServer = selectedRow["IPServer"].ToString().Trim(); // Đây là server (V6) cố định lưu trữ ip máy chủ data.
                    if (IPServer == "") goto Next1;
                    string IPDatabase = selectedRow["IPDatabase"].ToString().Trim();
                    string IPUserId = selectedRow["IPUserId"].ToString().Trim();
                    string IPEPassword = selectedRow["IPEPassword"].ToString().Trim();
                    ip_constring = "Server=" + IPServer + ";Database=" + IPDatabase + ";User Id=" + IPUserId
                                       + ";Password=" + V6SqlconnectHelper.DeCrypt(IPEPassword + check_key) + ";";
                    if (CheckConnectionString(ip_constring))
                    {
                        V6Tools.Logger.WriteToLog("IPServer: CheckConnect OK.");
                        string _key;
                        string server = GetServerFromIPServer(ip_constring, IPName, out Server_IP, out _key);
                        if (!string.IsNullOrEmpty(server))
                        {
                            Server = server;
                            IsIPServer = true;
                            V6Tools.Logger.WriteToLog("IPServer: IsIPServer = true. " + Server);
                            var _setting = new H.Setting(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Setting.ini"));
                            var DynamicIP_Dic = ObjectAndString.StringToStringDictionary(_setting.GetSetting("DynamicIP"));
                            var DynamicIP0_Dic = ObjectAndString.StringToStringDictionary(_setting.GetSetting("DynamicIP0"));
                            if (!DynamicIP_Dic.ContainsKey(Database) || DynamicIP_Dic[Database] != _key)// _setting.GetSetting("DynamicIP") != _key)
                            {
                                DynamicIP_Dic[Database] = _key;
                                DynamicIP0_Dic[Database] = Server_IP;


                                _setting.SetSetting("DynamicIP", ObjectAndString.StringDictionaryToString(DynamicIP_Dic));
                                _setting.SetSetting("DynamicIP0", ObjectAndString.StringDictionaryToString(DynamicIP0_Dic));
                                _setting.SaveSetting();
                            }
                        }
                        else
                        {
                            IsIPServer = false;
                            V6Tools.Logger.WriteToLog("IPServer: IsIPServer = true. " + Server);
                        }
                    }
                    else
                    {
                        V6Tools.Logger.WriteToLog("IPServer: CheckConnect Fail.");
                        var _setting = new H.Setting(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Setting.ini"));
                        var DynamicIP_Dic = ObjectAndString.StringToStringDictionary(_setting.GetSetting("DynamicIP"));
                        var DynamicIP0_Dic = ObjectAndString.StringToStringDictionary(_setting.GetSetting("DynamicIP0"));

                        string _key = DynamicIP_Dic.ContainsKey(Database) ? DynamicIP_Dic[Database] : "";// _setting.GetSetting("DynamicIP"); // Giá trị Server đã lưu lại trước đó.
                        if (!string.IsNullOrEmpty(_key))
                        {
                            Server = V6Tools.UtilityHelper.DeCrypt(_key);
                            Server_IP = DynamicIP0_Dic.ContainsKey(Database) ? DynamicIP0_Dic[Database] : ""; // Name Server.
                            IsIPServer = true;
                        }
                        else
                        {
                            IsIPServer = false;
                        }                        
                    }
                }
                //else if (ip_constring != "")
                //{

                //}

                if (ConnectionConfigData.Columns.Contains("IPSRV6Name"))
                {
                    IPSRV6Name = selectedRow["IPSRV6Name"].ToString().Trim();
                    IPSRV6Port = selectedRow["IPSRV6Port"].ToString().Trim();
                    IPSRV6Server = selectedRow["IPSRV6Server"].ToString().Trim();
                    IPSRV6Database = selectedRow["IPSRV6Database"].ToString().Trim();
                    IPSRV6UserId = selectedRow["IPSRV6UserId"].ToString().Trim();
                    IPSRV6EPassword = selectedRow["IPSRV6EPassword"].ToString().Trim();
                    if (selectedRow.Table.Columns.Contains("IPSRV6BlackTables"))
                    {
                        IPSRV6BlackTables = selectedRow["IPSRV6BlackTables"].ToString().Trim();
                    }
                }


                    Next1:

                if (ConnectionConfigData.Columns.Contains("Server2_TH"))
                {
                    Server2_TH = selectedRow["Server2_TH"].ToString().Trim();
                    Database2_TH = selectedRow["Database2_TH"].ToString().Trim();
                    UserId2_TH = selectedRow["UserId2_TH"].ToString().Trim();
                    EPassword2_TH = selectedRow["EPassword2_TH"].ToString().Trim();
                }
                
                if (ConnectionConfigData.Columns.Contains("Server3_TH"))
                {
                    Server3_TH = selectedRow["Server3_TH"].ToString().Trim();
                    Database3_TH = selectedRow["Database3_TH"].ToString().Trim();
                    UserId3_TH = selectedRow["UserId3_TH"].ToString().Trim();
                    EPassword3_TH = selectedRow["EPassword3_TH"].ToString().Trim();
                }


                try
                {
                    TimeOut = Convert.ToInt32(selectedRow["TimeOut"].ToString().Trim());
                    if (TimeOut < 99) TimeOut = 99;
                }
                catch (Exception)
                {
                    TimeOut = 99;
                }
                if (ConnectionConfigData.Columns.Contains("Note"))
                    Note = selectedRow["Note"].ToString().Trim();
                else Note = "";
                if (ConnectionConfigData.Columns.Contains("UseIsolation"))
                    UseIsolation = selectedRow["UseIsolation"].ToString().Trim() == "1";
                else UseIsolation = false;

                var serverConfigRow = ServerConfigData.Rows[0];
                ServerName = serverConfigRow["ServerName"].ToString().Trim();
                ServerIP = serverConfigRow["ServerIP"].ToString().Trim();
                CheckServer = serverConfigRow["CheckServer"].ToString().Trim();
                PasswordV6 = serverConfigRow["PasswordV6"].ToString().Trim();
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteToLog("GetConfigValues error: " + ex.Message);
            }
        }

        private static string GetServerFromIPServer(string ipConstring, string name, out string ip_name, out string key)
        {
            string server = "";
            ip_name = "";
            key = "";
            string log = "GetServerFromIPServer: ";
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@name", name),
                };

                var data = SqlHelper.ExecuteDataset(ipConstring, CommandType.Text,
                    "Select [IP_NAME], [KEY] from V6IP_CUSTS"
                    + " Where [name]=@name", param).Tables[0];
                log += data.Rows.Count + "row(s)";
                if (data.Rows.Count == 1)
                {
                    key = data.Rows[0]["KEY"].ToString().Trim();
                    server = V6Tools.UtilityHelper.DeCrypt(key);
                    ip_name = data.Rows[0]["IP_NAME"].ToString().Trim();
                    log += string.Format("\r\n key_to_server({0}) ip_name({1})", key, ip_name);
                }
            }
            catch (Exception ex)
            {
                V6Tools.Logger.WriteExLog("GetServerFromIPServer", ex);
            }
            V6Tools.Logger.WriteToLog(log);
            return server;
        }


        public static bool CheckConnectionString(string conString)
        {
            int check = 0;
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    //richTextBox1.AppendText(DatabaseConfig.Database + " Connection open ok.\n");
                    check++;
                }
                con.Close();
                if (con.State == ConnectionState.Closed)
                {
                    //richTextBox1.AppendText(DatabaseConfig.Database + " Connection closed ok.\n");
                    check++;
                }
                //richTextBox1.AppendText("==============================================\n");
                //richTextBox1.SelectionStart = richTextBox1.Text.Length;
                //richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
                //richTextBox1.AppendText(DatabaseConfig.Database + " Error.\n");
                //richTextBox1.AppendText(ex.Message + "\n");
                //richTextBox1.AppendText("==============================================\n");
                //richTextBox1.SelectionStart = richTextBox1.Text.Length;
                //richTextBox1.ScrollToCaret();
                check = -1;
            }

            return check == 2;
        }


        public static int GetConfigDataRunIndex()
        {
            int index = 0;
            foreach (DataRow row in ConnectionConfigData.Rows)
            {
                var run = row["Run"].ToString().Trim();
                if (run == "1")
                {
                    SelectedIndex = index;
                    break;
                }
                index++;
            }
            return _selectedIndex;
        }

        private static int _currentConnection = 1;
        public static int ChangeConnection()
        {
            //Lưu tạm
            var tstt = STT;
            var ts = Server;
            var td = Database;
            var tu = UserId;
            var te = EPassword;
            var ta = Api;
            var tkey = Key;
            
            // Check key
            var trueKey2 = V6SqlconnectHelper.EnCrypt(STT + Server2 + Database2);
            if (trueKey2 == Key2)
            {
                //Gán mới
                Server = Server2;
                Database = Database2;
                UserId = UserId2;
                EPassword = EPassword2;
                Api = Api2;
                Key = Key2;
                //Gán cũ
                Server2 = ts;
                Database2 = td;
                UserId2 = tu;
                EPassword2 = te;
                Api2 = ta;
                Key2 = tkey;

                _currentConnection = 3 - _currentConnection;
            }

            
            return _currentConnection;
        }

        public static bool IsIPServer { get; private set; }
        public static string STT = "";
        /// <summary>
        /// Tên server dữ liệu, có thể là IP.
        /// </summary>
        public static string Server { get; set; }
        /// <summary>
        /// Lưu giữ phần IP của Server nếu trường hợp get IP động
        /// </summary>
        public static string Server_IP = "";
        public static string Database = "";
        public static string UserId = "";
        public static string EPassword = "";
        public static string Api = "";
        public static string Key = "";

        public static string Server2 = "";
        public static string Database2 = "";
        public static string UserId2 = "";
        public static string EPassword2 = "";
        public static string Api2 = "";
        public static string Key2 = "";

        public static string Server2_TH = "";
        public static string Database2_TH = "";
        public static string UserId2_TH = "";
        public static string EPassword2_TH = "";
        
        public static string Server3_TH = "";
        public static string Database3_TH = "";
        public static string UserId3_TH = "";
        public static string EPassword3_TH = "";
        
        public static int TimeOut = 99;
        public static string Note = "";
        public static bool UseIsolation;

        //Phần chung
        public static string ServerName = "";
        public static string ServerIP = "";
        public static string CheckServer = "";
        public static string PasswordV6 = "Ai0I9gx1t2OUAygIZXqi4g==";

        // Phần lưu IP động
        public static string IPSRV6Name = "";
        public static string IPSRV6Port = "";
        public static string IPSRV6Server = "";
        public static string IPSRV6Database = "";
        public static string IPSRV6UserId = "";
        public static string IPSRV6EPassword = "";
        public static string IPSRV6BlackTables = "";// VPA_CHECK_BLACK_LIST:1


        private static bool CheckV6Key()
        {
            if (_v6Key == "V6Soft") return true;
            return false;
        }

        public static void ReadConfigData()
        {
            try
            {
                var fileName = Path.Combine(_iniLocation, ConfigFile);
                var ds = new DataSet("Config");

                if (File.Exists(fileName)) ds.ReadXml(fileName);
                
                if (ds.Tables.Count > 0)
                {
                    ConnectionConfigData = ds.Tables[0];
                }
                else
                {
                    ConnectionConfigData = new DataTable("DatabaseList");
                    ds.Tables.Add(ConnectionConfigData);
                }

                if (ds.Tables.Count > 1)
                {
                    ServerConfigData = ds.Tables[1];
                }
                else
                {
                    ServerConfigData = new DataTable("ServerConfig");
                    ds.Tables.Add(ServerConfigData);
                }

                FixConfigData(fileName);

                if (!ConnectionConfigData.Columns.Contains("Display"))
                {
                    ConnectionConfigData.Columns.Add("Display", typeof (string));
                }
                foreach (DataRow row in ConnectionConfigData.Rows)
                {
                    row["Display"] = row["Note"];
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("ReadConfigData error: " + ex.Message);
            }
        }

        private static void FixConfigData(string fileName)
        {
            // Cờ ghi nhận cần thêm trường trong config.
            var change = false;

            #region ==== ConnectionConfigData ====
            if (!ConnectionConfigData.Columns.Contains("STT"))
            {
                ConnectionConfigData.Columns.Add("STT", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("Server"))
            {
                ConnectionConfigData.Columns.Add("Server", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("Server2"))
            {
                ConnectionConfigData.Columns.Add("Server2", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("Database"))
            {
                ConnectionConfigData.Columns.Add("Database", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("Database2"))
            {
                ConnectionConfigData.Columns.Add("Database2", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("UserID"))
            {
                ConnectionConfigData.Columns.Add("UserID", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("UserID2"))
            {
                ConnectionConfigData.Columns.Add("UserID2", typeof(string));
                change = true;
            }

            if (!ConnectionConfigData.Columns.Contains("Epassword"))
            {
                ConnectionConfigData.Columns.Add("Epassword", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("Epassword2"))
            {
                ConnectionConfigData.Columns.Add("Epassword2", typeof(string));
                change = true;
            }

            if (!ConnectionConfigData.Columns.Contains("Api"))
            {
                ConnectionConfigData.Columns.Add("Api", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("Api2"))
            {
                ConnectionConfigData.Columns.Add("Api2", typeof(string));
                change = true;
            }

            if (!ConnectionConfigData.Columns.Contains("Note"))
            {
                ConnectionConfigData.Columns.Add("Note", typeof(string));
                change = true;
                foreach (DataRow row in ConnectionConfigData.Rows)
                {
                    row["Note"] = row["Server"] + " " + row["Database"];
                }
            }

            if (!ConnectionConfigData.Columns.Contains("Type"))
            {
                ConnectionConfigData.Columns.Add("Type", typeof(string));
                change = true;
            }
            if (!ConnectionConfigData.Columns.Contains("Key"))
            {
                ConnectionConfigData.Columns.Add("Key", typeof(string));
                change = true;
            }
            
            //Check Key // Remove if false
            for (int i = ConnectionConfigData.Rows.Count; i > 0; i--)
            {
                var cRow = ConnectionConfigData.Rows[i-1];
                var cSTT = cRow["STT"].ToString().Trim();
                if (cSTT != i.ToString())
                {
                    ConnectionConfigData.Rows.Remove(cRow);
                    continue;
                }

                var trueKey = V6SqlconnectHelper.EnCrypt(cSTT + cRow["Server"] + cRow["Database"]);
                //cRow["Key"] = checkString;// Phải bỏ dòng này đi.!!!!
                //change = true;// Phải bỏ dòng này đi.!!!!!!!!!!!!!!!!
                var cKey = cRow["Key"].ToString().Trim();
                if(trueKey != cKey)
                    ConnectionConfigData.Rows.Remove(cRow);
            }

            if (ConnectionConfigData.Rows.Count == 0)
            {
                var newRow = ConnectionConfigData.NewRow();
                for (int i = 0; i < ConnectionConfigData.Columns.Count; i++)
                {
                    newRow[i] = "Nodata";
                }
                ConnectionConfigData.Rows.Add(newRow);
                change = true;
            }
            #endregion ConnectionConfigData

            #region ==== ServerConfigData ====
            if (!ServerConfigData.Columns.Contains("ServerName"))
            {
                ServerConfigData.Columns.Add("ServerName", typeof(string));
                change = true;
            }
            if (!ServerConfigData.Columns.Contains("ServerIP"))
            {
                ServerConfigData.Columns.Add("ServerIP", typeof(string));
                change = true;
            }
            if (!ServerConfigData.Columns.Contains("CheckServer"))
            {
                ServerConfigData.Columns.Add("CheckServer", typeof(string));
                change = true;
            }
            if (ServerConfigData.Rows.Count == 0)
            {
                var newRow = ServerConfigData.NewRow();
                for (int i = 0; i < ServerConfigData.Columns.Count; i++)
                {
                    newRow[i] = "Nodata";
                }
                ServerConfigData.Rows.Add(newRow);
                change = true;
            }
            #endregion ServerConfigData

            if (change)
            {
                ConnectionConfigData.DataSet.WriteXml(fileName);
            }
        }
    }
}

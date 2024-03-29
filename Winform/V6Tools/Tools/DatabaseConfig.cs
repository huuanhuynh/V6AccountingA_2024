﻿using System;
using System.Data;
using System.IO;

namespace V6SqlConnect
{
    public static class DatabaseConfig
    {
        private static string _v6Key, check_key;
        private static string _configFile;
        private static bool _loaded;
        public static bool Loaded { get { return _loaded; } }
        public static void LoadDatabaseConfig(string v6Key, string configFile)
        {
            _v6Key = v6Key;
            check_key = CheckV6Key() ? "" : "Â" + _v6Key;
            _configFile = configFile;
            //ReadIniInfo();
            ReadConfigData();
            _loaded = true;
        }

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
                    + ";Password=" + V6Tools.UtilityHelper.DeCrypt(EPassword + check_key) + ";";

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
                            + ";Password=" + V6Tools.UtilityHelper.DeCrypt(EPassword2_TH + check_key) + ";";

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
                            + ";Password=" + V6Tools.UtilityHelper.DeCrypt(EPassword3_TH + check_key) + ";";

                    return s;
                }
                catch (Exception)
                {
                    return "";
                }
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

        private static void GetConfigValues(int index)
        {
            try
            {
                var selectedRow = ConnectionConfigData.Rows[index];
                Server = selectedRow["Server"].ToString().Trim();
                Database = selectedRow["Database"].ToString().Trim();
                UserId = selectedRow["UserId"].ToString().Trim();
                EPassword = selectedRow["EPassword"].ToString().Trim();
                Api = selectedRow["Api"].ToString().Trim();

                Server2 = selectedRow["Server2"].ToString().Trim();
                Database2 = selectedRow["Database2"].ToString().Trim();
                UserId2 = selectedRow["UserId2"].ToString().Trim();
                EPassword2 = selectedRow["EPassword2"].ToString().Trim();
                Api2 = selectedRow["Api2"].ToString().Trim();

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
            catch (Exception)
            {
                // ignored
            }
        }

        private static int _selectedIndex;

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
            var ts = Server;
            var td = Database;
            var tu = UserId;
            var te = EPassword;
            var ta = Api;
            //Gán mới
            Server = Server2;
            Database = Database2;
            UserId = UserId2;
            EPassword = EPassword2;
            Api = Api2;
            //Gán cũ
            Server2 = ts;
            Database2 = td;
            UserId2 = tu;
            EPassword2 = te;
            Api2 = ta;

            _currentConnection = 3 - _currentConnection;
            return _currentConnection;
        }

        /// <summary>
        /// Tên server dữ liệu, có thể là IP.
        /// </summary>
        public static string Server { get; set; }
        public static string Database = "";
        public static string UserId = "";
        public static string EPassword = "";
        public static string Api = "";

        public static string Server2 = "";
        public static string Database2 = "";
        public static string UserId2 = "";
        public static string EPassword2 = "";
        public static string Api2 = "";

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
        
        private static bool CheckV6Key()
        {
            if (_v6Key == "V6Soft") return true;
            return false;
        }

        public static void ReadConfigData()
        {
            try
            {
                var fileName = _configFile;
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
            
            //Check Key
            for (int i = ConnectionConfigData.Rows.Count; i > 0; i--)
            {
                var cRow = ConnectionConfigData.Rows[i-1];
                var cSTT = cRow["STT"].ToString().Trim();
                if (cSTT != i.ToString())
                {
                    ConnectionConfigData.Rows.Remove(cRow);
                    continue;
                }

                var trueKey = V6Tools.UtilityHelper.EnCrypt(cSTT + cRow["Server"] + cRow["Database"]);
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

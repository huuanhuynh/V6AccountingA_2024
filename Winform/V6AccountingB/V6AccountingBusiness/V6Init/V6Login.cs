using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;
using V6AccountingBusiness;
using V6AccountingBusiness.V6Init.User;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Init
{
    public static class V6Login
    {
        public static V6Rights UserRight { get; set; }
        
        public static bool IsAdmin
        {
            get
            {
                try
                {
                    //code_user = userName + pass + admin?
                    var codeUser = UtilityHelper.DeCrypt(UserInfo["Code_User"].ToString().Trim());

                    return UserName == codeUser.Substring(0,UserName.Length)
                        && codeUser.Substring(codeUser.Length - 1) == "1";
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Cờ lấy lại thông tin User.
        /// </summary>
        public static bool UserInfo_HaveChanged { get; set; }
        public static DataRow UserInfo
        {
            get
            {
                if (UserInfo_HaveChanged)
                {
                    GetInfo(_uName);
                    UserInfo_HaveChanged = false;
                }
                return _userInfo;
            }
            set
            {
                _userInfo = value;
                GetXmlInfo();
            }
        }
        private static DataRow _userInfo = null;

        public static UserXmlInfo XmlInfo { get { return _xmlInfo; } }
        private static UserXmlInfo _xmlInfo;

        private static void GetXmlInfo()
        {
            try
            {
                _xmlInfo = new UserXmlInfo(null);
                if (_userInfo != null && _userInfo.Table.Columns.Contains("XML_INFOR"))
                {
                    DataSet ds = new DataSet("DataSet");
                    ds.ReadXml(new StringReader(_userInfo["XML_INFOR"].ToString()));
                    if (ds.Tables.Count > 0)
                    {
                        var table = ds.Tables[0];
                        _xmlInfo = new UserXmlInfo(table.ToDataDictionary("KEY", "VALUE"));
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (_xmlInfo != null) _xmlInfo.DataDic.Add("ERROR", ex.Message);
                else _xmlInfo = new UserXmlInfo(new Dictionary<string, object>() {{"ERROR", ex.Message}});
            }
        }

        
        private static string _uName = "";

        public static int UserId
        {
            get
            {
                return ObjectAndString.ObjectToInt(UserInfo==null?0:UserInfo["User_id"]);
            }
        }
        /// <summary>
        /// User level
        /// </summary>
        public static string Level
        {
            // Tuanmh 16/02/2016
            get
            {
                return ObjectAndString.ObjectToString(UserInfo["Level"]);
            }
        }
        
        public static string Message { get; set; }
        public static string UserFullName { get; set; }
        public static string UserName { get { return _uName; } }
        /// <summary>
        /// CHON LOGIN
        /// </summary>
        public static string Madvcs { get; set; }

        public static string Comment
        {
            get { return UserInfo == null ? "" : UserInfo["comment"].ToString().Trim(); }
        }

        /// <summary>
        /// Tên máy tính chạy chương trình.
        /// </summary>
        public static string ClientName { get; set; }

        public static GetDataMode GetDataMode { get; set; }

        public static void GetInfo(string userName)
        {
            if (GetDataMode == GetDataMode.API)
            {
                //UserInfo = LoginApiInvoker.GetUserInfo(userName).Rows[0];
            }
            else
            {
                UserInfo = SqlConnect.SelectV6User(userName);
            }
        }

        /// <summary>
        /// Khi đăng nhập lần đầu tiên cần Gán SelectedLanguage và SelectedModule
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="madvcs"></param>
        /// <returns></returns>
        public static bool Login(string username, string password, string madvcs)
        {
            GetInfo(username);
            var result = CheckLogin(username, password, madvcs);
            return result;
        }

        /// <summary>
        /// Khi đăng nhập lần đầu tiên cần Gán SelectedLanguage và SelectedModule
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="madvcs"></param>
        /// <returns></returns>
        public static UserConfig LoginCheck(string username, string password, string madvcs)
        {
            var userConfig = ConfigManager.GetUserConfig(username);
            bool check = CheckLoginCheck(userConfig, username, password, madvcs);
            return check ? userConfig : null;
        }

        /// <summary>
        /// Hàm check ko ảnh hưởng thông tin login.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord">Mã hóa của pass gõ vào</param>
        /// <param name="madvcs"></param>
        /// <returns></returns>
        private static bool CheckLoginCheck(UserConfig userConfig, string userName, string passWord, string madvcs)
        {
            //_uName = userName;
            if (userConfig == null) return false;
            var ePass = UtilityHelper.EnCrypt(userName + passWord);
            
            
            if (userConfig.password == ePass)
            {   
                return true;
            }
            else
            {
                Message = "Wrong Password.";
                return false;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord">Mã hóa của pass gõ vào</param>
        /// <param name="madvcs"></param>
        /// <returns></returns>
        private static bool CheckLogin(string userName, string passWord, string madvcs)
        {
            _uName = userName;
            var ePass = UtilityHelper.EnCrypt(userName + passWord);

            if (GetDataMode == GetDataMode.API)
            {
                //LoginToken = LoginApiInvoker.Login(username, ePass, madvcs,
                //    V6Setting.Language, module);
                //UserRight = new V6Rights(UserInfo);
                //return !string.IsNullOrEmpty(LoginToken);
            }

            if (UserInfo != null)
            {
                if (UserInfo.Table.Columns.Contains("password") &&
                    (UserInfo["password"].ToString().Trim() == ePass))
                {
                    Message = "Login Success.";
                    Madvcs = madvcs;

                    var dvcsDataTable = SqlConnect.Select("ALDVCS", "*", "MA_DVCS='" + madvcs + "'").Data;
                    if (dvcsDataTable != null && dvcsDataTable.Rows.Count == 1)
                    {
                        var dvcsData = dvcsDataTable.Rows[0];
                        V6Setting.DataDVCS = dvcsData;
                        V6Setting.Ten_dvcsx = dvcsData["Ten_dvcsx"].ToString().Trim();
                        V6Setting.Ten_dvcs2x = dvcsData["Ten_dvcs2x"].ToString().Trim();
                        V6Setting.Dia_chix = dvcsData["Dia_chix"].ToString().Trim();
                        V6Setting.Dia_chi2x = dvcsData["Dia_chi2x"].ToString().Trim();
                        V6Setting.Dien_thoai = dvcsData["Dien_thoai"].ToString().Trim();
                        V6Setting.Ma_so_thue = dvcsData["Ma_so_thue"].ToString().Trim();
                    }

                    V6Setting.LASTUSERW = userName;
                    V6Setting.LoadSetting(UserId);
                    UserRight = new V6Rights(UserInfo);

                    SaveIP();
                    
                    return true;
                }
                else
                {
                    Message = "Wrong Password.";
                    return false;
                }
            }
            else
            {
                Message = "Wrong UserName.";
                return false;
            }
        }
        
        public static void Logout()
        {
            UserInfo = null;
            UserRight = new V6Rights(null);
            _uName = "";
            //_ePass = "";
        }

        public static string LoginToken { get; set; }
        /// <summary>
        /// Số lượng DVCS theo user. dbo.VFA_Inlist_MEMO(ma_dvcs, '" + V6Login.UserInfo["r_dvcs"] + "')=1
        /// </summary>
        public static int MadvcsCount { get; set; }
        /// <summary>
        /// Tổng số DVCS lưu trong Aldvcs. Select Count(Ma_dvcs) from Aldvcs
        /// </summary>
        public static int MadvcsTotal { get; set; }

        /// <summary>
        /// Mã ngôn ngữ lưu trong reg, truy cập qua V6Setting.Language = V, E
        /// </summary>
        public static string SelectedLanguage
        {
            get { return V6Setting.Language; }
            set { V6Setting.Language = value; }
        }
        /// <summary>
        /// Chữ hiển thị của ngôn ngữ. vd "Tiếng Việt"
        /// </summary>
        public static string SelectedLanguageName { get; set; }
        public static string SelectedModule { get; set; }
        /// <summary>
        /// Lưu đường dẫn start của chương trình. không có dấu \ cuối.
        /// </summary>
        public static string StartupPath { get; set; }
        public static FileInfo FileInfo { get; set; }
        public static string V6UpdateInfo
        {
            get
            {
                return string.Format("Path:{0};FileInfo:{1:dd/MM/yyyy HH:mm:ss}", FileInfo.FullName, FileInfo.LastWriteTime);
            }
        }
        /// <summary>
        /// Exe
        /// </summary>
        public static bool IsNetwork { get; set; }
        /// <summary>
        /// Exe
        /// </summary>
        public static bool IsLocal { get { return !IsNetwork; } }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="tableName">.</param>
        /// <param name="filterType">Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo</param>
        /// <returns></returns>
        public static string GetInitFilter(string tableName, string filterType)
        {
            string result = "";

            switch (("" + tableName).ToUpper().Trim())
            {
                case "V6OPTION":
                    result = "Attribute=1";
                    break;

                case "V6SOFT":
                    result = "Attribute=1";
                    break;
                case "ALCT1":
                    result = "(Loai='2' or Loai='3' or Loai='1')";
                    break;

                case "ALKH":
                     result = IsAdmin ? "" : V6Lookup.GetValueByTableName(tableName, "InitFilter");
                    result = result.Replace("{0}", "'"+Madvcs+"'");

                    break;
                case "ALBP":
                    result = IsAdmin ? "" : V6Lookup.GetValueByTableName(tableName, "InitFilter");
                    result = result.Replace("{0}", "'" + Madvcs + "'");

                    break;
                case "ALDVCS":

                    result = IsAdmin ? "" : V6Lookup.GetValueByTableName(tableName, "InitFilter");
                    //result = result.Replace("{0}", "'" + Madvcs + "'");
                    result = result.Replace("{0}", "{MA_DVCS}");

                    break;
                case "ABVT":

                    result = "Nam = " + V6Setting.M_Nam_bd;
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }
                    

                    break;
                case "ABKH":

                    result = "Nam = " + V6Setting.M_Nam_bd;
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }
                    
                    break;
                case "ABVV":
                    result = "Nam = " + V6Setting.M_Nam_bd;
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }
                    break;

                case "ABVVKH":
                    result = "Nam = " + V6Setting.M_Nam_bd;
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }
                    break;

                case "ABBPKH":
                    result = "Nam = " + V6Setting.M_Nam_bd;
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }
                    break;

                case "ABTK":


                    result = "Nam = " + V6Setting.M_Nam_bd;
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }
                    

                    break;
                case "ABLO":

                    result = "Nam = " + V6Setting.M_Nam_bd;
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }

                    break;

                case "ALQDDVT":
                    result = "XTYPE='1'";
                    break;

                case "V6MENU":
                    result = "MODULE_ID='" + V6Options.GetValue("MODULE_ID").Trim() + "'";//+ "' AND ITEMID<>'A0000000' AND ITEMID<>'B0000000' ";
                    break;

                case "ALKHO":
                    if (MadvcsCount == 1)
                    {
                        result = IsAdmin ? "" : V6Lookup.GetValueByTableName(tableName, "InitFilter");
                        //result = result.Replace("{0}", "'" + Madvcs + "'");
                        result = result.Replace("{0}", "{MA_DVCS}");
                    }
                   break;

                case "V_ALTS":
                    if (MadvcsCount == 1)
                    {
                        result = IsAdmin ? "" : "Ma_dvcs='" + Madvcs + "'";

                        if (result != "")
                            result += " AND isnull(ma_giam_ts,'')<>'' ";
                        else
                            result += " isnull(ma_giam_ts,'')<>'' ";
                    }
                    else
                    {
                        result = IsAdmin ? "" : "";
                        if (result != "")
                            result += " AND isnull(ma_giam_ts,'')<>'' ";
                        else
                            result += " isnull(ma_giam_ts,'')<>'' ";
                    }
                    break;

                case "V_ALCC":
                    if (MadvcsCount == 1)
                    {
                        result = IsAdmin ? "" : "Ma_dvcs='" + Madvcs + "'";

                        if (result != "")
                            result += " AND isnull(ma_giam_cc,'')<>'' ";
                        else
                            result += " isnull(ma_giam_cc,'')<>'' ";
                    }
                    else
                    {
                        result = IsAdmin ? "" : "";
                        if (result != "")
                            result += " AND isnull(ma_giam_cc,'')<>'' ";
                        else
                            result += " isnull(ma_giam_cc,'')<>'' ";
                    }
                    break;
                case "V_ALTS01":
                    if (MadvcsCount == 1)
                    {
                        result = IsAdmin ? "" : "Ma_dvcs='" + Madvcs + "'";

                        if (result != "")
                            result += " AND isnull(NGAY_KH1,'')<>'' ";
                        else
                            result += " isnull(NGAY_KH1,'')<>'' ";
                    }
                    else
                    {
                        result = IsAdmin ? "" : "";
                        if (result != "")
                            result += " AND isnull(NGAY_KH1,'')<>'' ";
                        else
                            result += " isnull(NGAY_KH1,'')<>'' ";
                    }
                    break;

                case "V_ALCC01":
                    if (MadvcsCount == 1)
                    {
                        result = IsAdmin ? "" : "Ma_dvcs='" + Madvcs + "'";

                        if (result != "")
                            result += " AND isnull(NGAY_PB1,'')<>'' ";
                        else
                            result += " isnull(NGAY_PB1,'')<>'' ";
                    }
                    else
                    {
                        result = IsAdmin ? "" : "";
                        if (result != "")
                            result += " AND isnull(NGAY_PB1,'')<>'' ";
                        else
                            result += " isnull(NGAY_PB1,'')<>'' ";
                    }
                    break;

                case "ALPB":

                   
                    if (MadvcsCount == 1)
                    {
                        result = IsAdmin ? "" : "Ma_dvcs='" + Madvcs + "'";

                    }

                    break;
                case "ABNTXT":

                    result = "Nam = " + V6Setting.M_Nam_bd;
                    result += " and Ma_ct = 'S08'";
                    if (MadvcsCount == 1)
                    {
                        result += IsAdmin ? "" : " and Ma_dvcs='" + Madvcs + "'";
                    }


                    break;
                default:

                    bool filter_all = ObjectAndString.ObjectToBool(V6Lookup.GetValueByTableName(tableName, "FILTER_ALL"));
                    if (filter_all)
                    {
                        result = V6Lookup.GetValueByTableName(tableName, "InitFilter");
                    }
                    else
                    {
                        result = IsAdmin ? "" : V6Lookup.GetValueByTableName(tableName, "InitFilter");
                    }
                    break;
        
                
            }
            string adv_filter = null;
            if (string.IsNullOrEmpty(filterType)) filterType = "0";
            SqlParameter[] plist =
                {
                    new SqlParameter("@IsAldm", true),
                    new SqlParameter("@TableName", tableName),
                    new SqlParameter("@Type", filterType),
                    new SqlParameter("@User_id", V6Login.UserId),
                };

            adv_filter = (V6BusinessHelper.ExecuteProcedureScalar("VPA_GetAdvanceFilter", plist) ?? "").ToString().Trim();
            if (!string.IsNullOrEmpty(adv_filter))
            {
                if (string.IsNullOrEmpty(result)) return adv_filter;
                return string.Format("({0}) and ({1})", result, adv_filter);
            }
            return result;
        }

        public static string GetMaDvcsFilterByMaKho(string ma_kh, string ma_dvcs)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Ma_ct", "ALL"),
                new SqlParameter("@Ma_dvcs", ma_dvcs),
                new SqlParameter("@Ma_kh", ma_kh),
                new SqlParameter("@user_id", V6Login.UserId),
                new SqlParameter("@Lan", V6Login.SelectedLanguage),
            };
            object where = V6BusinessHelper.ExecuteProcedureScalar("VPA_GET_WHERE_INVOICE_MADVCS2MAKHO", plist);

            return "" + where;
        }

        /// <summary>
        /// Lấy lại filter kho khi chọn lại dvcs.
        /// </summary>
        /// <param name="ma_dvcs"></param>
        /// <returns></returns>
        public static string GetFilterKhoByDVCS(string ma_dvcs)
        {
            string filter = "";

            string get = GetMaDvcsFilterByMaKho("V6LOGIN", ma_dvcs);
            if (!string.IsNullOrEmpty(get))
            {
                filter = get + " and (dbo.VFA_Inlist_MEMO(ma_kho, '" + UserInfo["r_kho"] + "')=1 or 1=" + (IsAdmin ? 1 : 0) + ")";
                return filter;
            }

            if (!string.IsNullOrEmpty(ma_dvcs))
            {
                filter = string.Format("MA_DVCS='{0}' and ", ma_dvcs);
            }

            filter += "(dbo.VFA_Inlist_MEMO(ma_kho, '" + UserInfo["r_kho"] + "')=1 or 1="
                      + (IsAdmin ? 1 : 0) + ")";
            return filter;
        }
        /// <summary>
        /// Lấy lại filter kho
        /// </summary>
        /// <param name="fma_kho"></param>
        /// <returns></returns>
        public static string GetFilterKho(string fma_kho)
        {
            if (string.IsNullOrEmpty(fma_kho))
            {
                fma_kho = "MA_KHO";
            }
            string filter = string.Format("(dbo.VFA_Inlist_MEMO({0}, '" + UserInfo["r_kho"] + "')=1 or 1="
                      + (IsAdmin ? 1 : 0) + ")", fma_kho);
            return filter;
        }
        public static DataTable GetLanguageTable()
        {
            if (GetDataMode == GetDataMode.Local)
            {
                return CorpLang.LanguageTable;
            }
            else
            {
                return null; //return LoginApiInvoker.GetLanguageTable;
            }
        }

        public static DataTable GetModuleTable()
        {
            DataTable moduleTable;
            if (GetDataMode == GetDataMode.Local)
            {
                moduleTable = V6Menu.ModuleTable;
                foreach (DataRow row in moduleTable.Rows)
                {
                    if (row["module_id"].ToString().Trim() == "A")
                        row["name"] = "Accounting";
                    else if (row["module_id"].ToString().Trim() == "H")
                        row["name"] = "Human Resource Management";
                    else if (row["module_id"].ToString().Trim() == "C")
                        row["name"] = "Customer Relation Management";
                    else if (row["module_id"].ToString().Trim() == "I")
                        row["name"] = "Inventory";
                    else if (row["module_id"].ToString().Trim() == "E")
                        row["name"] = "ERP";
                }
                return moduleTable;
            }
            else
            {
                return null; //return LoginApiInvoker.ModuleTable;
            }
        }

        /// <summary>
        /// Ma_dvcs, Ten_dvcs as Name, Ten_dvcs2 as Name2
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DataTable GetAgentTable(string key)
        {
            if (GetDataMode == GetDataMode.Local)
            {
                var data = SqlConnect.Select("Aldvcs", "Ma_dvcs, Ten_dvcs as Name, Ten_dvcs2 as Name2",
                    key, "", "stt_order,ma_dvcs").Data;
                return data;
            }
            else
            {
                return null;// LoginApiInvoker.GetAgentTable(key, V6Setting.Language);
            }
        }

        public static bool StartSqlConnect(string key, string iniLocation)
        {
            if (GetDataMode == GetDataMode.Local)
            {
                DatabaseConfig.GetConfigValues(DatabaseConfig.SelectedIndex);
                var b = V6BusinessHelper.StartSqlConnect(key, iniLocation);
                if (b)
                {
                    if (!SqlConnect.ConnectionOk)
                        throw new Exception(V6Text.NoConnection);
                }
                return b;
            }

            return true;
        }

        private static bool IsNetworkPath(string path)
        {
            if (path.StartsWith(@"\\")) return true;
            var drivers = DriveInfo.GetDrives();

            for (int i = drivers.Length - 1; i >= 0; i--)
            {
                var driver = drivers[i];
                if (path.StartsWith(driver.Name))
                {
                    if (driver.DriveType == DriveType.Network) return true;
                }
            }

            return false;
        }

        public static DataTable GetClientTable()
        {
            if (GetDataMode == GetDataMode.API)
            {
                return new DataTable("V6Clients");
            }
            else
            {
                return SqlConnect.SelectTable("V6Clients");
            }
        }
        
        public static DataTable GetV6onlineTable()
        {
            if (GetDataMode == GetDataMode.API)
            {
                return new DataTable("V6ONLINES");
            }
            else
            {
                return SqlConnect.SelectTable("V6ONLINES");
            }
        }

        private static void InsertNewClient(string NAME)
        {
            try
            {
                var checkCode = ObjectAndString.ObjectToString(SqlConnect.GetServerDateTime(), "yyyyMMddHH:mm:ss")
                    .Replace(" ", "");
                var data = new SortedDictionary<string, object>
                {
                    {"NAME", NAME},
                    {"ALLOW", "0"},
                    {"CHECKCODE", checkCode},
                    {"CODE_NAME", UtilityHelper.EnCrypt(NAME+"0"+checkCode)},
                    {"STATUS", "1"},
                };

                if (GetDataMode == GetDataMode.Local)
                {
                    var tableName = "V6Clients";
                    var tStruct = V6SqlconnectHelper.GetTableStruct(tableName);
                    var sql = SqlGenerator.GenInsertSql(UserId, tableName, tStruct, data);
                    SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Login InsertNewClient: " + ex.Message, "V6Init");
            }
        }
        
        public static void AllowClient()
        {
            try
            {
                var checkCode = ObjectAndString.ObjectToString(SqlConnect.GetServerDateTime(), "yyyyMMddHH:mm:ss")
                    .Replace(" ", "");
                var data = new SortedDictionary<string, object>
                {
                    {"NAME", ClientName},
                    {"ALLOW", "1"},
                    {"CHECKCODE", checkCode},
                    {"CODE_NAME", UtilityHelper.EnCrypt(ClientName+"1"+checkCode)},
                    {"STATUS", "1"},
                    {"V6UPDATE", V6UpdateInfo},
                };
                var keys = new SortedDictionary<string, object>
                {
                    {"NAME", ClientName}
                };

                if (GetDataMode == GetDataMode.Local)
                {
                    var tableName = "V6Clients";
                    var tStruct = V6SqlconnectHelper.GetTableStruct(tableName);
                    var sql = SqlGenerator.GenUpdateSqlSimple(UserId, tableName, tStruct, data, keys);
                    SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Login InsertNewClient: " + ex.Message, "V6Init");
            }
        }

        public static bool CheckAllowClient(string localPath)
        {
            var is_allow = false;
            //ClientName = CLIENT_NAME;
            //Check o dia mang
            if (IsNetworkPath(localPath))//Chay link server
            {
                //CheckAllow
                var data = GetClientTable();

                var have = false;
                foreach (DataRow row in data.Rows)
                {
                    if (row["NAME"].ToString().Trim().ToUpper() == ClientName)
                    {
                        have = true;
                        var eCodeName = (row["CODE_NAME"]??"").ToString().Trim();
                        var rCodeName = eCodeName == "" ? "" : UtilityHelper.DeCrypt(eCodeName);
                        var allow = row["Allow"].ToString().Trim();
                        var checkCode = row["CheckCode"].ToString().Trim();
                        is_allow =
                            allow == "1"
                            && rCodeName.Length > ClientName.Length + 1
                            && rCodeName.StartsWith(ClientName)
                            && rCodeName.Substring(ClientName.Length, 1) == allow
                            && rCodeName.EndsWith(checkCode);
                        if (is_allow)
                        {
                            break;
                        }
                    }
                }

                if (have)
                {
                    UpdateClientInfo();
                }
                else
                {
                    InsertNewClient(ClientName);
                }
            }
            else
            {
                is_allow = true;
                UpdateOnlineInfo();
            }
            return is_allow;
        }

        private static void UpdateClientInfo()
        {
            try
            {
                var data = new SortedDictionary<string, object>
                {
                    {"V6UPDATE", V6UpdateInfo},
                };
                var keys = new SortedDictionary<string, object>
                {
                    {"NAME", ClientName}
                };

                var tableName = "V6Clients";
                var tStruct = V6SqlconnectHelper.GetTableStruct(tableName);
                var sql = SqlGenerator.GenUpdateSqlSimple(UserId, tableName, tStruct, data, keys);
                SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
                
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Login UpdateClientInfo: " + ex.Message, "V6Init");
            }
        }
        
        private static void UpdateOnlineInfo()
        {
            try
            {
                var data = new SortedDictionary<string, object>
                {
                    {"V6UPDATE", V6UpdateInfo},
                };
                var keys = new SortedDictionary<string, object>
                {
                    {"NAME", ClientName},
                    {"SERI", License.Seri},
                    {"KEY", License.Key},
                };
                var tableName = "V6ONLINES";
                var tStruct = V6SqlconnectHelper.GetTableStruct(tableName);
                var sql = SqlGenerator.GenUpdateSqlSimple(UserId, tableName, tStruct, data, keys);
                SqlConnect.ExecuteNonQuery(CommandType.Text, sql);
                
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("V6Login UpdateOnlineInfo: " + ex.Message, "V6Init");
            }
        }

        public static bool CheckAllowVersion(string version)
        {
            return V6Options.CurrentVersion == version;
        }

        /// <summary>
        /// Nếu M_COMPANY_BY_MA_DVCS = 1 và CountDVCS = 1 thay thế các M_TEN... = DataDVCS
        /// </summary>
        /// <param name="reportDocumentParameters"></param>
        public static void SetCompanyInfo(IDictionary<string, object> reportDocumentParameters)
        {
            var M_COMPANY_BY_MA_DVCS = V6Options.ContainsKey("M_COMPANY_BY_MA_DVCS") ? V6Options.GetValue("M_COMPANY_BY_MA_DVCS").Trim() : "";
            if (M_COMPANY_BY_MA_DVCS == "1" && MadvcsCount == 1)
            {
                reportDocumentParameters["M_TEN_CTY"] = UtilityHelper.DeCrypt(V6Setting.Ten_dvcsx);
                reportDocumentParameters["M_TEN_CTY2"] = UtilityHelper.DeCrypt(V6Setting.Ten_dvcs2x);
                reportDocumentParameters["M_DIA_CHI"] = UtilityHelper.DeCrypt(V6Setting.Dia_chix);
                reportDocumentParameters["M_DIA_CHI2"] = UtilityHelper.DeCrypt(V6Setting.Dia_chi2x);
                reportDocumentParameters["M_MA_THUE"] = V6Setting.Ma_so_thue;

                var dataRow = V6Setting.DataDVCS;
                
                var GET_FIELD = "TEN_GD";
                if (dataRow.Table.Columns.Contains(GET_FIELD))
                    reportDocumentParameters["M_"+GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();
                GET_FIELD = "TEN_GD2";
                if (dataRow.Table.Columns.Contains(GET_FIELD))
                    reportDocumentParameters["M_" + GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();
                GET_FIELD = "TEN_KTT";
                if (dataRow.Table.Columns.Contains(GET_FIELD))
                    reportDocumentParameters["M_" + GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();
                GET_FIELD = "TEN_KTT2";
                if (dataRow.Table.Columns.Contains(GET_FIELD))
                    reportDocumentParameters["M_" + GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();
                //GET_FIELD = "TEN_NLB";
                //if (dataRow.Table.Columns.Contains(GET_FIELD))
                //    reportDocumentParameters["M_" + GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();
                //GET_FIELD = "TEN_NLB2";
                //if (dataRow.Table.Columns.Contains(GET_FIELD))
                //    reportDocumentParameters["M_" + GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();

                //GET_FIELD = "TEN_GOI_GD";
                //if (dataRow.Table.Columns.Contains(GET_FIELD))
                //    reportDocumentParameters["M_" + GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();
                //GET_FIELD = "TEN_GOI_KTT";
                //if (dataRow.Table.Columns.Contains(GET_FIELD))
                //    reportDocumentParameters["M_" + GET_FIELD] = V6Setting.DataDVCS[GET_FIELD].ToString();
                
            }
        }

        public static object GetValueNull(string name)
        {
            try
            {
                string NAME = name.ToUpper();
                Type t = typeof(V6Login);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (FieldInfo fi in fields)
                {
                    if (fi.Name.ToUpper() == NAME)
                        return fi.GetValue(null);
                }

                PropertyInfo[] properties = t.GetProperties(BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo fi in properties)
                {
                    if (fi.Name.ToUpper() == NAME)
                        return fi.GetValue(null, null);
                }
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static void SaveIP()
        {
            try
            {
                if (!string.IsNullOrEmpty(DatabaseConfig.IPSRV6Name))
                {
                    string ip = GetMyIP();
                    UpdateMyIP(DatabaseConfig.IPSRV6Name, ip, DatabaseConfig.IPSRV6Port);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public static string GetMyIP()
        {
            String address = "";
            try
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    address = stream.ReadToEnd();
                }

                int first = address.IndexOf("Address: ") + 9;
                int last = address.LastIndexOf("</body>");
                address = address.Substring(first, last - first);
            }
            catch (Exception ex)
            {

            }

            return address;
        }

        public static void UpdateMyIP(string name, string new_ip, string extra_port)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@name", name),
                new SqlParameter("@ip", new_ip),
                new SqlParameter("@key", UtilityHelper.EnCrypt(new_ip + extra_port)),
            };
            
            SqlHelper.ExecuteNonQuery(DatabaseConfig.ConnectionString_IPSR, CommandType.Text,
             "Update V6IP_CUSTS set [IP_NAME]=@ip, "
            + " [Date2]=CONVERT(smalldatetime, GetDate(), 103), [Time2]=CONVERT(Char(10), GetDate(), 108),"
            + " [DateTime2] = GetDate(),"
            + " [Key] = @key"
            + " Where [name]=@name", param);


        }

    }

    
}

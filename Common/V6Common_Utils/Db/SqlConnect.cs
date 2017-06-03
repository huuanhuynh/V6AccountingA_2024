using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using V6Soft.Common.Utils.Db;

namespace V6SqlConnect
{
    public static class SqlConnect
    {
        private static IniInfo _iniInfo;
        private static string _v6Key = "";

        /// <summary>
        /// Tên server
        /// </summary>
        public static string SeriServerName
        {
            get { return _iniInfo == null ? "" : _iniInfo.ServerName; }
        }
        public static string ServerIP
        {
            get { return _iniInfo == null ? "" : _iniInfo.ServerIP; }
        }
        public static string CheckServer
        {
            get { return _iniInfo == null ? "" : _iniInfo.CheckServer; }
        }
        public static bool CheckV6Key()
        {
            if (_v6Key == "V6Soft") return true;
            return false;
        }

        public static bool StartSqlConnect(string key = "key")
        {
            try
            {
                _v6Key = key;
                if (CheckV6Key())
                {
                    _iniInfo = new IniInfo(_v6Key);
                    return true;
                }
            }
            catch
            {
                // ignored
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra kết nối có hoạt động tốt không?
        /// </summary>
        public static bool ConnectionOk
        {
            get
            {
                try
                {
                    var t = GetServerDateTime();
                    if (t.Year > 1900)
                        return true;

                    return false;
                }
                catch// (Exception)
                {
                    try
                    {
                        ChangeConnection();
                        var t = GetServerDateTime();
                        if (t.Year > 1900)
                            return true;
                    }
                    catch
                    {
                        // ignored
                    }
                    return false;
                }
            }
        }

        public static int ChangeConnection()
        {
            return _iniInfo.ChangeConnection();
        }

        public static SqlTransaction CreateSqlTransaction(string name)
        {
            var con = new SqlConnection(_iniInfo.ConnectionString);
            con.Open();
            return con.BeginTransaction(name);
        }

        public static int CountRows(string tableName, string where = "")
        {
            if (!CheckV6Key()) throw new Exception("No permission.");
            var sql = "SELECT COUNT(*) FROM [" + tableName + "]";
            if (!string.IsNullOrEmpty(@where))
            {
                sql += " Where " + @where;
            }
            var result = SqlHelper.ExecuteScalar(_iniInfo.ConnectionString, CommandType.Text, sql);
            return (int)result;
        }


        /// <summary>
        /// Chuyển pass thường thành EPass bằng hàm VFA_FEADString
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string GetEPassFromSqlFunction(string pass)
        {
            if (CheckV6Key())
            {
                const string queryString = "select dbo.VFA_FEADString(@pass, 30)";
                var result = SqlHelper.ExecuteScalar(_iniInfo.ConnectionString, CommandType.Text,
                    queryString, new SqlParameter("@pass", pass));

                return result.ToString();
            }
            return "";
        }

        public static DateTime GetServerDateTime()
        {
            return (DateTime)ExecuteScalar(CommandType.Text, "Select GETDATE()");
        }

        /// <summary>
        /// Lấy lên bảng dữ liệu gồm tất cả các dòng
        /// </summary>
        /// <param name="tableName">Tên bảng dữ liệu</param>
        /// <returns></returns>
        public static V6SelectResult Select(string tableName)
        {
            if (CheckV6Key())
            {
                var result = new V6SelectResult { TotalRows = CountRows(tableName) };
                var t = SqlHelper.ExecuteDataset(_iniInfo.ConnectionString, CommandType.Text,
                    "Select*from [" + tableName + "]").Tables[0];
                result.Data = t;
                return result;
            }
            return null;
        }

        /// <summary>
        /// Select from sql
        /// Chưa chuyển mã sẵn vì không ref tới V6Tools
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="where">Không cần có sẵn chữ Where</param>
        /// <param name="group"></param>
        /// <param name="sort"></param>
        /// <param name="prList"></param>
        /// <returns></returns>
        public static V6SelectResult Select(string tableName, string fields, string where = "", string group = "", string sort = "", params SqlParameter[] prList)
        {
            if (CheckV6Key())
            {
                var fieldsSelect = string.IsNullOrEmpty(fields) ? "*" : fields;
                var whereClause = string.IsNullOrEmpty(where) ? "" : " WHERE " + where;
                var groupClause = string.IsNullOrEmpty(group) ? "" : " GROUP BY " + group;
                var sortOrder = string.IsNullOrEmpty(sort) ? "" : " ORDER BY " + sort;

                var sql = string.Format("Select {0} from [{1}] {2} {3} {4}",
                    fieldsSelect, tableName, whereClause, groupClause, sortOrder);
                var ds = SqlHelper.ExecuteDataset(_iniInfo.ConnectionString, CommandType.Text, sql, prList);
                var t = ds.Tables.Count > 0 ? ds.Tables[0] : null;


                var result = new V6SelectResult
                {
                    Data = t,
                    Page = 1
                };

                if (t != null)
                {
                    result.TotalRows = t.Rows.Count;
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="where"></param>
        /// <param name="sort">Không được trống, ít nhất là 1 field nào đó</param>
        /// <returns></returns>
        public static V6SelectResult Select(string tableName, string fields, int page, int size, string where, string sort)
        {
            if (CheckV6Key())
            {
                var result = new V6SelectResult
                {
                    Page = page,
                    PageSize = size,
                    TotalRows = CountRows(tableName, where),
                    Fields = fields,
                    Where = where,
                    Sort = sort
                };
                var fieldsSelect = string.IsNullOrEmpty(fields) ? "*" : fields.Replace("'", "''");

                var whereClause = string.IsNullOrEmpty(@where) ? "" : " WHERE " + @where;

                var sortOrder = string.IsNullOrEmpty(sort) ? "" : " ORDER BY " + sort;

                var sql = " WITH Allrecord AS ("
                          + "SELECT [RowNum] = ROW_NUMBER() OVER(" + sortOrder + "), " + fieldsSelect
                          + " FROM [" + tableName + "] " + whereClause + ")"
                          + " SELECT " + fieldsSelect + " FROM Allrecord"
                          + " WHERE [RowNum] BETWEEN @from AND @to" + @sortOrder;

                var t = SqlHelper.ExecuteDataset(_iniInfo.ConnectionString,
                    CommandType.Text,
                    sql,
                    new SqlParameter("@from", ((page - 1) * size) + 1),
                    new SqlParameter("@to", page * size)
                    ).Tables[0];

                result.Data = t;
                result.Fields = fieldsSelect;

                return result;
            }
            return null;
        }

        public static DataRow SelectV6User(string userName)
        {
            if (CheckV6Key())
            {
                const string sql = "select * from [V6user]" + //[user_name], [password], [is_admin]
                                   " where [user_name] = @user";

                var prUser = new SqlParameter("@user", userName);

                var ds = ExecuteDataset(CommandType.Text, sql, prUser);

                if (ds.Tables.Count <= 0) return null;
                return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0] : null;
            }
            return null;
        }

        public static DataSet ExecuteDataset(CommandType commandType, string sql, IDbConnection connection, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteDataset(_iniInfo.ConnectionString, commandType, sql, connection, prlist);
            }
            return null;
        }

        public static DataSet ExecuteDataset(SqlTransaction tran, CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteDataset(tran, commandType, sql, prlist);
            }
            return null;
        }

        public static int ExecuteNonQuery(CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteNonQuery(_iniInfo.ConnectionString, commandType, sql, prlist);
            }
            return -1;
        }

        public static int ExecuteNonQuery(SqlTransaction tran, CommandType ctype, string sql,
            params SqlParameter[] prlist)
        {
            return SqlHelper.ExecuteNonQuery(tran, ctype, sql, prlist);
        }

        public static object ExecuteScalar(CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteScalar(_iniInfo.ConnectionString, commandType, sql, prlist);
            }
            return null;
        }

        public static SqlDataReader ExecuteReader(CommandType text, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteReader(_iniInfo.ConnectionString, text, sql, prlist);
            }
            return null;
        }
    }

    internal class IniInfo
    {
        private string _v6Key;
        public IniInfo(string v6Key)
        {
            _v6Key = v6Key;
            ReadIniInfo();
        }

        private const string IniFile = "V6SqlConnect.ini";

        /// <summary>
        /// Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword;
        /// </summary>
        public string ConnectionString
        {
            get
            {
                var s = "Server=" + Server + ";Database=" + Database + ";User Id=" + UserId
                    + ";Password=" + V6SqlconnectHelper.DeCrypt(EPassword + _v6Key) + ";";

                return s;
            }
        }

        public string ServerName = "";
        public string ServerIP = "";
        public string CheckServer = "";

        private int _currentConnection = 1;
        public int ChangeConnection()
        {
            //Lưu tạm
            var ts = Server;
            var td = Database;
            var tu = UserId;
            var te = EPassword;
            //Gán mới
            Server = Server2;
            Database = Database2;
            UserId = UserId2;
            EPassword = EPassword2;
            //Gán cũ
            Server2 = ts;
            Database2 = td;
            UserId2 = tu;
            EPassword2 = te;

            _currentConnection = 3 - _currentConnection;
            return _currentConnection;
        }

        public string Server = "";
        public string Database = "";
        public string UserId = "";
        public string EPassword = "";

        public string Server2 = "";
        public string Database2 = "";
        public string UserId2 = "";
        public string EPassword2 = "";

        public void ReadIniInfo()
        {
            FileStream fs;

            try
            {
                _v6Key = SqlConnect.CheckV6Key() ? "" : "error!";

                fs = new FileStream(IniFile + _v6Key, FileMode.Open);
                var sr = new StreamReader(fs);
                var s = sr.ReadToEnd().Split('\n');
                sr.Close();
                var dic = (from s1 in s
                           select s1.Trim()
                               into s2
                               where s2 != "" && s2.Contains("=")
                               select s2.Split(new[] { '=' }, 2)
                    ).ToDictionary(ss2 => ss2[0], ss2 => ss2[1] + _v6Key);

                //Gán value cho fields
                fs = new FileStream(IniFile, FileMode.Append);
                var sw = new StreamWriter(fs);
                //Config Server 1
                {
                    if (dic.ContainsKey("Server")) Server = dic["Server"];
                    else sw.WriteLine("Server=");

                    if (dic.ContainsKey("Database")) Database = dic["Database"];
                    else sw.WriteLine("Database=");

                    if (dic.ContainsKey("UserId")) UserId = dic["UserId"];
                    else sw.WriteLine("UserId=");

                    if (dic.ContainsKey("EPassword")) EPassword = dic["EPassword"];
                    else sw.WriteLine("EPassword=");
                }
                //Config Server 2
                {

                    if (dic.ContainsKey("Server2")) Server2 = dic["Server2"];
                    else
                    {
                        sw.WriteLine();
                        sw.WriteLine("Server2=");
                    }

                    if (dic.ContainsKey("Database2")) Database2 = dic["Database2"];
                    else sw.WriteLine("Database2=");

                    if (dic.ContainsKey("UserId2")) UserId2 = dic["UserId2"];
                    else sw.WriteLine("UserId2=");

                    if (dic.ContainsKey("EPassword2")) EPassword2 = dic["EPassword2"];
                    else sw.WriteLine("EPassword2=");
                }
                {
                    if (dic.ContainsKey("ServerName")) ServerName = dic["ServerName"];
                    else sw.WriteLine("ServerName=");

                    if (dic.ContainsKey("ServerIP")) ServerIP = dic["ServerIP"];
                    else sw.WriteLine("ServerIP=");

                    if (dic.ContainsKey("CheckServer")) CheckServer = dic["CheckServer"];
                    else sw.WriteLine("CheckServer=");

                }
                sw.Close();
            }
            catch (FileNotFoundException)
            {
                fs = new FileStream(IniFile, FileMode.CreateNew);
                var sw = new StreamWriter(fs);
                sw.WriteLine("Server=");
                sw.WriteLine("Database=");
                sw.WriteLine("UserId=");
                sw.WriteLine("EPassword=");

                sw.WriteLine("Server2=");
                sw.WriteLine("Database2=");
                sw.WriteLine("UserId2=");
                sw.WriteLine("EPassword2=");
                sw.Close();

                throw new Exception("Check ini config file: " + IniFile);
            }

        }
    }

    public class V6SelectResult
    {
        public V6SelectResult()
        {
            Page = 1;
            PageSize = 20;
            Where = "";
            Sort = "";
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Where { get; set; }
        public string Sort { get; set; }

        /// <summary>
        /// Tự tính ra từ TotalRows và PageSize
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (TotalRows == 0 || PageSize == 0) return 0;
                int pages = TotalRows / PageSize;
                if (TotalRows % PageSize > 0) pages++;
                return pages;
            }
        }

        public int TotalRows { get; set; }
        /// <summary>
        /// Dữ liệu khi dùng Sqlconnect
        /// </summary>
        public DataTable Data { get; set; }
        /// <summary>
        /// Dữ liệu khi dùng cho API
        /// </summary>
        public object DataObject { get; set; }
        public string Fields { get; set; }
        public SortedDictionary<string, string> FieldsHeaderDictionary { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6Structs;

namespace V6SqlConnect
{
    public static class SqlConnect
    {
        private static string _v6Key = "";

        /// <summary>
        /// Tên server
        /// </summary>
        public static string SeriServerName
        {
            get { return DatabaseConfig.ServerName; }
        }
        public static string ServerIP
        {
            get { return DatabaseConfig.ServerIP; }
        }
        public static string CheckServer
        {
            get { return DatabaseConfig.CheckServer; }
        }
        public static bool CheckV6Key()
        {
            if (_v6Key == "V6Soft") return true;
            return false;
        }
        
        public static bool StartSqlConnect(string key, string iniLocation)//!!!!!!!!!!!
        {
            try
            {
                _v6Key = key;
                if (CheckV6Key())
                {
                    if(!DatabaseConfig.Loaded)
                        DatabaseConfig.LoadDatabaseConfig(_v6Key, iniLocation);
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
            return DatabaseConfig.ChangeConnection();
        }

        public static SqlTransaction CreateSqlTransaction(string name)
        {
            var con = new SqlConnection(DatabaseConfig.ConnectionString);
            con.Open();
            if(DatabaseConfig.UseIsolation)
                return con.BeginTransaction(IsolationLevel.Serializable, name);
            else return con.BeginTransaction(name);
        }

        public static int CountRows(string tableName, string where = "")
        {
            if (!CheckV6Key()) throw new Exception("No permission.");
            var sql = "SELECT COUNT(*) FROM [" + tableName + "]";
            if (!string.IsNullOrEmpty(@where))
            {
                sql += " Where " + @where;
            }
            var result = SqlHelper.ExecuteScalar(DatabaseConfig.ConnectionString, CommandType.Text, sql, DatabaseConfig.TimeOut);
            return (int) result;
        }


        /// <summary>
        /// Chuyển pass thường thành EPass bằng hàm VFA_FEADString
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string GetEPassFromSqlFunction(string pass)
        {
            if(CheckV6Key())
            {
                const string queryString = "select dbo.VFA_FEADString(@pass, 30)";
                var result = SqlHelper.ExecuteScalar(DatabaseConfig.ConnectionString, CommandType.Text,
                    queryString, DatabaseConfig.TimeOut, new SqlParameter("@pass", pass));

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
                tableName = tableName.Replace("'", "''");
                var result = new V6SelectResult {TotalRows = CountRows(tableName)};
                var t = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.Text,
                    "Select * from [" + tableName + "]", DatabaseConfig.TimeOut).Tables[0];
                t.TableName = tableName;
                result.Data = t;
                return result;
            }
            return null;
        }

        public static DataTable SelectTable(string tableName)
        {
            if (CheckV6Key())
            {
                tableName = tableName.Replace("'", "''");
                var data = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.Text,
                    "Select * from [" + tableName + "]", DatabaseConfig.TimeOut).Tables[0];
                data.TableName = tableName;
                return data;
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
        public static V6SelectResult Select(string tableName, string fields, string where="", string group="", string sort = "", params SqlParameter[] prList)
        {
            if (CheckV6Key())
            {
                if (fields == null) fields = "*";
                
                var fieldsSelect = string.IsNullOrEmpty(fields) ? "*" : fields;
                var whereClause = string.IsNullOrEmpty(where) ? "" : " WHERE " + where;
                var groupClause = string.IsNullOrEmpty(group) ? "" : " GROUP BY " + group;
                var sortOrder = string.IsNullOrEmpty(sort) ? "" : " ORDER BY " + sort;

                var sql = string.Format("Select {0} from {1} {2} {3} {4}",
                    fieldsSelect, tableName, whereClause, groupClause, sortOrder);
                var ds = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.Text, sql, DatabaseConfig.TimeOut, prList);
                var t = ds.Tables.Count > 0 ? ds.Tables[0] : null;


                var result = new V6SelectResult
                {
                    Data = t,
                    Page = 1
                };

                if (t != null)
                {
                    t.TableName = tableName;
                    result.TotalRows = t.Rows.Count;
                }
                return result;
            }
            return null;
        }

        public static DataTable SelectSimple(string tableName, string fields, string where="", string group="", string sort = "", params SqlParameter[] pList)
        {
            if (CheckV6Key())
            {
                if (fields == null) fields = "*";
                
                var fieldsSelect = string.IsNullOrEmpty(fields) ? "*" : fields;
                var whereClause = string.IsNullOrEmpty(where) ? "" : " WHERE " + where;
                var groupClause = string.IsNullOrEmpty(group) ? "" : " GROUP BY " + group;
                var sortOrder = string.IsNullOrEmpty(sort) ? "" : " ORDER BY " + sort;

                var sql = string.Format("Select {0} from {1} {2} {3} {4}", fieldsSelect, tableName, whereClause, groupClause, sortOrder);
                var ds = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.Text, sql, DatabaseConfig.TimeOut, pList);
                var t = ds.Tables.Count > 0 ? ds.Tables[0] : null;

                if (t != null)
                {
                    t.TableName = tableName;
                }
                return t;
            }
            return null;
        }

        public static DataTable SelectOneRow(string tablename, IDictionary<string, object> keys)
        {
            if(string.IsNullOrEmpty(tablename))
                throw new ArgumentException("tablename");
            if (keys == null || keys.Count == 0)
                throw new ArgumentException("keyList");

            SqlParameter[] plist;
            var where = SqlGenerator.GenSqlWhereOutParameters(keys, out plist);
            var sql = string.Format("Select {0} from {1} where " + where, "*", tablename);
            var ds = ExecuteDataset(CommandType.Text, sql, plist);
            if (ds != null && ds.Tables.Count > 0) return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// Lấy lên 1 giá trị duy nhất trong 1 bảng dữ liệu.
        /// </summary>
        /// <param name="tablename">Bảng dữ liệu nguồn.</param>
        /// <param name="field">Cột chứa giá trị.</param>
        /// <param name="keys">Khóa lấy giá trị.</param>
        /// <returns></returns>
        public static object SelectOneValue(string tablename, string field, IDictionary<string, object> keys)
        {
            if(string.IsNullOrEmpty(tablename))
                throw new ArgumentException("tablename");
            if (string.IsNullOrEmpty(field))
                throw new ArgumentException("field");
            if (keys == null || keys.Count == 0)
                throw new ArgumentException("keyList");

            SqlParameter[] plist;
            var where = SqlGenerator.GenSqlWhereOutParameters(keys, out plist);
            var sql = string.Format("Select {0} from {1} where " + where, field, tablename);
            return ExecuteScalar(CommandType.Text, sql, plist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="page"></param>
        /// <param name="size">0 hoặc nhỏ hơn = load all.</param>
        /// <param name="where"></param>
        /// <param name="sortField">Không được trống, ít nhất là 1 field nào đó</param>
        /// <param name="ascending"></param>
        /// <param name="prlist"></param>
        /// <returns></returns>
        public static V6SelectResult SelectPaging(string tableName, string fields, int page, int size,
            string where, string sortField, bool ascending, params SqlParameter[] prlist)
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
                    SortField = sortField
                };
                var fieldsSelect = string.IsNullOrEmpty(fields) ? "*" : fields.Replace("'", "''");

                var whereClause = string.IsNullOrEmpty(@where) ? "" : " WHERE " + where;

                var orderByClause = string.IsNullOrEmpty(sortField) ? "" : 
                    string.Format(" ORDER BY {0} {1}", sortField, ascending ? "asc" : "desc");
                string sql;
                var plist = new List<SqlParameter>(prlist);
                if(size>0)
                {
                    sql = " WITH Allrecord AS ("
                          + "SELECT [RowNum] = ROW_NUMBER() OVER(" + orderByClause + "), " + fieldsSelect
                          + " FROM [" + tableName + "] " + whereClause + ")"
                          + " SELECT " + fieldsSelect + " FROM Allrecord"
                          + " WHERE [RowNum] BETWEEN @from AND @to " + orderByClause;

                    plist.Add(new SqlParameter("@from", (page - 1)*size + 1));
                    plist.Add(new SqlParameter("@to", page*size));
                }
                else
                {
                    sql = " WITH Allrecord AS ("
                          + "SELECT [RowNum] = ROW_NUMBER() OVER(" + orderByClause + "), " + fieldsSelect
                          + " FROM [" + tableName + "] " + whereClause + ")"
                          + " SELECT " + fieldsSelect + " FROM Allrecord "
                          + orderByClause;
                }

                var t = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString,
                    CommandType.Text, sql, DatabaseConfig.TimeOut, plist.ToArray()
                    ).Tables[0];

                t.TableName = tableName;
                result.Data = t;
                result.IsSortOrderAscending = ascending;

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
        public static DataRow SelectV6UserById(int userId)
        {
            if (CheckV6Key())
            {
                const string sql = "select * from [V6user] where [user_id] = @p";
                var prUser = new SqlParameter("@p", userId);
                var ds = ExecuteDataset(CommandType.Text, sql, prUser);

                if (ds.Tables.Count <= 0) return null;
                return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0] : null;
            }
            return null;
        }

        public static DataSet ExecuteDataset(CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, commandType, sql, DatabaseConfig.TimeOut, prlist);
            }
            return null;
        }

        public static DataSet ExecuteDataset(string sql, params object[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, sql, DatabaseConfig.TimeOut, prlist);
            }
            return null;
        }

        public static DataSet ExecuteDataset(SqlTransaction tran, CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteDataset(tran, commandType, sql, DatabaseConfig.TimeOut, prlist);
            }
            return null;
        }

        public static int ExecuteNonQuery(CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteNonQuery(DatabaseConfig.ConnectionString, commandType, sql, DatabaseConfig.TimeOut, prlist);
            }
            return -1;
        }

        public static int ExecuteNonQuery(SqlTransaction tran, CommandType ctype, string sql,
            params SqlParameter[] prlist)
        {
            return SqlHelper.ExecuteNonQuery(tran, ctype, sql, DatabaseConfig.TimeOut, prlist);
        }

        public static object ExecuteScalar(CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteScalar(DatabaseConfig.ConnectionString, commandType, sql, DatabaseConfig.TimeOut, prlist);
            }
            return null;
        }

        public static object ExecuteScalar(SqlTransaction tran, CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteScalar(tran, commandType, sql, DatabaseConfig.TimeOut, prlist);
            }
            return null;
        }

        public static SqlDataReader ExecuteReader(CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteReader(DatabaseConfig.ConnectionString, commandType, sql, DatabaseConfig.TimeOut, prlist);
            }
            return null;
        }

        public static SqlDataReader ExecuteReader(SqlTransaction tran, CommandType commandType, string sql, params SqlParameter[] prlist)
        {
            if (CheckV6Key())
            {
                return SqlHelper.ExecuteReader(tran, commandType, sql, DatabaseConfig.TimeOut, prlist);
            }
            return null;
        }
    }

    
}

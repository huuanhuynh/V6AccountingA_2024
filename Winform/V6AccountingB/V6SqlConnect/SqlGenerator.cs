using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using V6Structs;
using V6Tools.V6Convert;


namespace V6SqlConnect
{
    public static class SqlGenerator
    {
        static readonly string _comma_decimal_symbol = ",";// CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private static readonly string clientName = Dns.GetHostName().ToUpper();

        public static string GenInsertSql(int UserId, string tableName, V6TableStruct structTable, DataRow row)
        {
            var serverDateTime = SqlConnect.GetServerDateTime();
            var dateString = serverDateTime.ToString("yyyyMMdd").Replace(" ", "");
            var timeString = serverDateTime.ToString("HH:mm:ss").Replace(" ", "");
            var datetimeString = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss").Replace(" : ", ":");
            
            string fields = "";
            string values = "";
            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    fields += ",\n[" + FIELD + "]";
                    
                    switch (FIELD)
                    {
                        case "DATE0":
                        case "DATE2":
                            values += ",\n'" + dateString + "'";
                            break;
                        case "TIME0":
                        case "TIME2":
                            values += ",\n'" + timeString + "'";
                            break;
                        case "DATETIME0":
                        case "DATETIME2":
                            values += ",\n'" + datetimeString + "'";
                            break;

                        case "USER_ID0":
                        case "USER_ID2":
                            values += "," + UserId;
                            break;
                        case "CLIENT_NAME0":
                        case "CLIENT_NAME2":
                            values += ",\n'" + clientName + "'";
                            break;
                        default:
                            values += "," + GenSqlStringValue(
                                row[FIELD],
                                column.sql_data_type_string, column.ColumnDefault,
                                column.AllowNull,
                                column.MaxLength);
                            break;
                    }
                }
            }
            if (fields.Length > 0)
            {
                fields = fields.TrimStart(',');
            }
            if (values.Length > 0)
            {
                values = values.TrimStart(',');
            }
            string sql = "Insert into [" + tableName + "] (" + fields + ")\nValues\n(" + values + ")";
            return sql;
        }

        public static string GenInsertSql(int UserId, string tableName, V6TableStruct structTable, IDictionary<string, string> dataDictionary)
        {
            var serverDateTime = SqlConnect.GetServerDateTime();
            var dateString = serverDateTime.ToString("yyyyMMdd").Replace(" ", "");
            var timeString = serverDateTime.ToString("HH:mm:ss").Replace(" ", "");
            var datetimeString = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss").Replace(" : ", ":");
            string fields = "";
            string values = "";
            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    fields += ",\n[" + FIELD + "]";
                    
                    switch (FIELD)
                    {
                        case "DATE0":
                        case "DATE2":
                            values += ",\n'" + dateString + "'";
                            break;
                        case "TIME0":
                        case "TIME2":
                            values += ",\n'" + timeString + "'";
                            break;
                        case "DATETIME0":
                        case "DATETIME2":
                            values += ",\n'" + datetimeString + "'";
                            break;

                        case "USER_ID0":
                        case "USER_ID2":
                            values += "," + UserId;
                            break;
                        case "CLIENT_NAME0":
                        case "CLIENT_NAME2":
                            values += ",\n'" + clientName + "'";
                            break;
                        default:
                            values += "," + GenSqlStringValue(
                        dataDictionary.ContainsKey(FIELD) ?dataDictionary[FIELD] : "",
                        column.sql_data_type_string, column.ColumnDefault,
                        column.AllowNull,
                        column.MaxLength);
                            break;
                    }
                }
            }
            if (fields.Length > 0)
            {
                fields = fields.TrimStart(',');
            }
            if (values.Length > 0)
            {
                values = values.TrimStart(',');
            }
            string sql = "Insert into [" + tableName + "] (" + fields + ")\nValues\n(" + values + ")";
            return sql;
        }

        /// <summary>
        /// Thêm dữ liệu vào bảng.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="tableName"></param>
        /// <param name="structTable"></param>
        /// <param name="dataDictionary"></param>
        /// <returns></returns>
        public static string GenInsertSql(int UserId, string tableName, V6TableStruct structTable, IDictionary<string, object> dataDictionary)
        {
            var serverDateTime = SqlConnect.GetServerDateTime();
            var dateString = serverDateTime.ToString("yyyyMMdd").Replace(" ", "");
            var timeString = serverDateTime.ToString("HH:mm:ss").Replace(" ", "");
            var datetimeString = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss").Replace(" : ", ":");
            string fields = "";
            string values = "";
            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    fields += ",\n[" + FIELD + "]";

                    switch (FIELD)
                    {
                        case "DATE0":
                        case "DATE2":
                            values += ",\n'"+dateString+"'";
                            break;
                        case "TIME0":
                        case "TIME2":
                            values += ",\n'"+timeString+"'";
                            break;
                        case "DATETIME0":
                        case "DATETIME2":
                            values += ",\n'" + datetimeString + "'";
                            break;

                        case "USER_ID0":
                        case "USER_ID2":
                            values += ",\n" + UserId;
                            break;
                        case "CLIENT_NAME0":
                        case "CLIENT_NAME2":
                            values += ",\n'" + clientName + "'";
                            break;
                        default:
                            values += ",\n" + GenSqlStringValue(
                                dataDictionary.ContainsKey(FIELD)
                                    ? dataDictionary[FIELD]
                                    : null, column.sql_data_type_string, column.ColumnDefault,
                                    column.AllowNull,
                                    column.MaxLength);
                            break;
                    }
                }
            }
            if (fields.Length > 0)
            {
                fields = fields.TrimStart(',');
            }
            if (values.Length > 0)
            {
                values = values.TrimStart(',');
            }
            string sql = "Insert into [" + tableName + "] (" + fields + ")\n Values \n(" + values + ")";
            return sql;
        }

        /// <summary>
        /// Tạo câu query thêm dữ liệu vào bảng không có các trường tự động.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="tableName"></param>
        /// <param name="structTable"></param>
        /// <param name="dataDictionary"></param>
        /// <returns></returns>
        public static string GenInsertSqlSimple(int UserId, string tableName, V6TableStruct structTable, IDictionary<string, object> dataDictionary)
        {
            string fields = "";
            string values = "";
            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    fields += ",\n[" + FIELD + "]";

                    values += ",\n" + GenSqlStringValue(
                        dataDictionary.ContainsKey(FIELD)
                            ? dataDictionary[FIELD]
                            : "", column.sql_data_type_string, column.ColumnDefault,
                        column.AllowNull,
                        column.MaxLength);
                }
            }
            if (fields.Length > 0)
            {
                fields = fields.TrimStart(',');
            }
            if (values.Length > 0)
            {
                values = values.TrimStart(',');
            }
            string sql = "Insert into [" + tableName + "] (" + fields + ")\n Values \n(" + values + ")";
            return sql;
        }

        /// <summary>
        /// Đặc biệt dùng cho chứng từ!!!
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="structTable"></param>
        /// <param name="dataDictionary"></param>
        /// <param name="isnew">Cờ tạo timeString</param>
        /// <returns></returns>
        public static string GenInsertAMSql(int UserId, V6TableStruct structTable,
            IDictionary<string, object> dataDictionary, bool isnew=true)
        {
            //GetStructureTableAndColumnsStruct();
            string fields = "";
            string values = "";
            var serverDateTime = SqlConnect.GetServerDateTime();
            var dateString = serverDateTime.ToString("yyyyMMdd").Replace(" ", "");
            var timeString = serverDateTime.ToString("HH:mm:ss").Replace(" ", "");
            var datetimeString = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss").Replace(" : ", ":");
            
            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    fields += ",\n[" + FIELD + "]";

                    switch (FIELD)
                    {
                        case "DATE0":
                            
                            if (isnew){
                                values += ",\n'"+dateString+"'";
                            }
                            else
                            {
                                values += ",\n" + GenSqlStringValue(
                                    dataDictionary.ContainsKey(FIELD)
                                        ? dataDictionary[FIELD]
                                        : "",
                                    column.sql_data_type_string, column.ColumnDefault,
                                    column.AllowNull,
                                    column.MaxLength);
                            }
                            break;
                        case "DATE2":
                            values += ",\n'" + dateString + "'";
                            break;
                        case "TIME0":
                            if (isnew)
                            {
                                values += ",\n'" + timeString + "'";
                            }
                            else
                            {
                                values += "," + GenSqlStringValue(
                                    dataDictionary.ContainsKey(FIELD) ? dataDictionary[FIELD] : "",
                                    column.sql_data_type_string, column.ColumnDefault,
                                    column.AllowNull,
                                    column.MaxLength);
                            }
                            break;
                        case "TIME2":
                            values += ",\n'" + timeString + "'";
                            break;
                        case "DATETIME0":
                            if (isnew)
                            {
                                values += ",\n'" + datetimeString + "'";
                            }
                            else
                            {
                                values += "," + GenSqlStringValue(
                                    dataDictionary.ContainsKey(FIELD) ? dataDictionary[FIELD] : "",
                                    column.sql_data_type_string, column.ColumnDefault,
                                    column.AllowNull,
                                    column.MaxLength);
                            }
                            break;
                        case "DATETIME2":
                            values += ",\n'" + datetimeString + "'";
                            break;

                        case "CLIENT_NAME0":
                            if (isnew)
                            {
                                values += ",\n'" + clientName + "'";
                            }
                            else
                            {
                                values += ",\n" + GenSqlStringValue(
                                    dataDictionary.ContainsKey(FIELD) ? dataDictionary[FIELD] : "",
                                    column.sql_data_type_string, column.ColumnDefault,
                                    column.AllowNull,
                                    column.MaxLength);
                            }
                            break;

                        case  "USER_ID0":
                            values += ",\n" + UserId;
                            break;
                        case "USER_ID2":
                            values += ",\n" + UserId;
                            break;
                        case "CLIENT_NAME2":
                            values += ",\n'" + clientName + "'";
                            break;
                            
                        default:
                            values += ",\n" + GenSqlStringValue(
                                dataDictionary.ContainsKey(FIELD)
                                    ? dataDictionary[FIELD]
                                    : "",
                                column.sql_data_type_string, column.ColumnDefault,
                                column.AllowNull,
                                column.MaxLength);
                            break;
                    }
                }
            }
            if (fields.Length > 0)
            {
                fields = fields.TrimStart(',');
            }
            if (values.Length > 0)
            {
                values = values.TrimStart(',');
            }
            string sql = "Insert into [" + structTable.TableName + "] (" + fields + ") Values (" + values + ")";
            return sql;
        }


        /// <summary>
        /// Update dữ liệu, không update keys
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="tableName"></param>
        /// <param name="structTable"></param>
        /// <param name="dataDictionary"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GenUpdateSql(int UserId, string tableName, V6TableStruct structTable,
            IDictionary<string, object> dataDictionary, IDictionary<string, object> keys)
        {
            var serverDateTime = SqlConnect.GetServerDateTime();
            var dateString = serverDateTime.ToString("yyyyMMdd").Replace(" ", "");
            var timeString = serverDateTime.ToString("HH:mm:ss").Replace(" ", "");
            var datetimeString = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss").Replace(" : ", ":");
            var sql = "Update [" + tableName + "] Set";// field = value[, field = value[...]]
            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    if (dataDictionary.ContainsKey(FIELD))
                    {
                        object value = dataDictionary[FIELD];
                        {
                            //Không update key, và những trường 0_2
                            if (   FIELD == "DATE0"
                                || FIELD == "TIME0"
                                || FIELD == "USER_ID0"
                                || FIELD == "DATE2"
                                || FIELD == "TIME2"
                                || FIELD == "DATETIME0"
                                || FIELD == "DATETIME2"
                                || FIELD == "USER_ID2"
                                || FIELD == "CLIENT_NAME0"
                                || FIELD == "CLIENT_NAME2")
                                continue;

                            value = GenSqlStringValue(value, column.sql_data_type_string, column.ColumnDefault, column.AllowNull,
                                column.MaxLength);

                            sql += "\n[" + FIELD + "] = " + value + ",";
                        }
                    }
                }
            }
            //Đặc biệt: update DATE2, TIME2
            if (structTable.ContainsKey("DATE2")) sql += "\n[DATE2] = '"+dateString+"',";
            if (structTable.ContainsKey("TIME2")) sql += "\n[TIME2] = '"+timeString+"',";
            if (structTable.ContainsKey("DATETIME2")) sql += "\n[DATETIME2] = '"+datetimeString+"',";
            if (structTable.ContainsKey("USER_ID2")) sql += "\n[USER_ID2] = "+ UserId+",";
            if (structTable.ContainsKey("CLIENT_NAME2")) sql += "\n[CLIENT_NAME2] = '" + clientName + "',";
            
            sql = sql.TrimEnd(',');
            var where = GenWhere(structTable, keys);
            if(where.Trim()=="") throw new Exception("Where rỗng!");
            sql += " Where " + where;

            return sql;
        }
        
        
        public static string GenUpdateSqlParameter(int UserId, string tableName, V6TableStruct structTable,
            IDictionary<string, object> dataDictionary, IDictionary<string, object> keys,
            out SqlParameter[] plist)
        {
            var serverDateTime = SqlConnect.GetServerDateTime();
            var dateString = serverDateTime.ToString("yyyyMMdd").Replace(" ", "");
            var timeString = serverDateTime.ToString("HH:mm:ss").Replace(" ", "");
            var datetimeString = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss").Replace(" : ", ":");
            var p_list = new List<SqlParameter>();
            var sql = "Update [" + tableName + "] Set";// field = value[, field = value[...]]
            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    if (dataDictionary.ContainsKey(FIELD))
                    {
                        object value = dataDictionary[FIELD];
                        {
                            //Không update key, và những trường 0_2
                            if (   FIELD == "DATE0"
                                || FIELD == "TIME0"
                                || FIELD == "USER_ID0"
                                || FIELD == "DATE2"
                                || FIELD == "TIME2"
                                || FIELD == "DATETIME0"
                                || FIELD == "DATETIME2"
                                || FIELD == "USER_ID2"
                                || FIELD == "CLIENT_NAME0"
                                || FIELD == "CLIENT_NAME2")
                                continue;

                            //value = GenSqlStringValue(value, column.SqlDataType, column.ColumnDefault, column.AllowNull,
                            //    column.MaxLength);

                            sql += "\n[" + FIELD + "] = @" + FIELD + ",";
                            p_list.Add(new SqlParameter("@" + FIELD, value){SqlDbType = column.SqlDbType});
                        }
                    }
                }
            }
            //Đặc biệt: update DATE2, TIME2
            if (structTable.ContainsKey("DATE2")) sql += "\n[DATE2] = '"+dateString+"',";
            if (structTable.ContainsKey("TIME2")) sql += "\n[TIME2] = '"+timeString+"',";
            if (structTable.ContainsKey("DATETIME2")) sql += "\n[DATETIME2] = '"+datetimeString+"',";
            if (structTable.ContainsKey("USER_ID2")) sql += "\n[USER_ID2] = "+ UserId+",";
            if (structTable.ContainsKey("CLIENT_NAME2")) sql += "\n[CLIENT_NAME2] = '" + clientName + "',";
            
            sql = sql.TrimEnd(',');
            List<SqlParameter> wherePlist;
            var where = GenWhereParameter(structTable, keys, out wherePlist);
            p_list.AddRange(wherePlist);
            if(where.Trim()=="") throw new Exception("Where rỗng!");
            sql += " Where " + where;

            plist = p_list.ToArray();
            return sql;
        }

        /// <summary>
        /// Tạo câu sql update không có các thông tin tự động DATE TIME và USER_ID
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="tableName"></param>
        /// <param name="structTable">Nếu có struct thì tạo theo struct, nếu không có thì tạo theo data</param>
        /// <param name="dataDictionary"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GenUpdateSqlSimple(int UserId, string tableName, V6TableStruct structTable,
            IDictionary<string, object> dataDictionary, IDictionary<string, object> keys)
        {
            var sql = "Update [" + tableName + "] Set"; // field = value[, field2 = value2[...]]
            string where;
            int temp_count = 0;
            if (structTable != null)
            {
                foreach (V6ColumnStruct column in structTable.Values)
                {
                    var FIELD = column.ColumnName.ToUpper();
                    if (FIELD != "UID" && dataDictionary.ContainsKey(FIELD))
                    {
                        object value = dataDictionary[FIELD];
                        {
                            value = GenSqlStringValue(value, column.sql_data_type_string, column.ColumnDefault, column.AllowNull, column.MaxLength);
                            sql += "\n[" + FIELD + "] = " + value + ",";
                            temp_count++;
                        }
                    }
                }
                where = GenWhere(structTable, keys);
            }
            else
            {
                foreach (KeyValuePair<string, object> item in dataDictionary)
                {
                    var FIELD = item.Key.ToUpper();
                    if (FIELD != "UID")
                    {
                        string value = GenSqlStringValue(item.Value, false);
                        sql += "\n[" + item.Key + "] = " + value + ",";
                        temp_count++;
                    }
                }
                where = GenWhere2(keys);
            }
            if(temp_count == 0) throw new Exception("No columns!");
            //if (structTable.ContainsKey("USER_ID2")) sql += "\n[USER_ID2] = " + V6Login.UserId + ",";

            sql = sql.TrimEnd(',');
            
            if (@where.Trim() == "") throw new Exception("Where rỗng!");
            sql += " Where " + @where;

            return sql;
        }

        /// <summary>
        /// Không có sẵn chữ Where, dùng and và =
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GenSqlWhere(IDictionary<string, object> keys)
        {
            if (keys == null) return "";
            string where = "";
            foreach (var key in keys)
            {
                where += string.Format(" and [{0}] = {1}", key.Key, GenSqlStringValue(key.Value, false));
            }
            if (where.Length > 4) where = where.Substring(4);

            return where;
        }

        /// <summary>
        /// Tạo ra chuỗi where và plist theo keys. Chuỗi where không chứa giá trị mà chỉ dùng @parameter.
        /// </summary>
        /// <param name="keys">Các giá trị khóa gửi vào</param>
        /// <param name="plist">Danh sách parameters lấy giá trị từ keys.</param>
        /// <returns></returns>
        public static string GenSqlWhereOutParameters(IDictionary<string, object> keys, out SqlParameter[] plist)
        {
            if (keys == null)
            {
                plist = null;
                return "";
            }
            string where = "";
            List<SqlParameter> pr_list = new List<SqlParameter>();
            foreach (var key in keys)
            {
                where += string.Format(" and [{0}] = @{0}", key.Key);
                pr_list.Add(new SqlParameter("@" + key.Key, key.Value));
            }
            if (where.Length > 4) where = where.Substring(4);
            plist = pr_list.ToArray();
            return where;
        }

        /// <summary>
        /// Không thêm where ở đầu. khi dùng hãy tự kiểm tra. 
        /// </summary>
        /// <param name="structTable"></param>
        /// <param name="keys"></param>
        /// <param name="oper">= hoặc khac hoặc like</param>
        /// <param name="and"></param>
        /// <param name="tableLable"></param>
        /// <returns></returns>
        public static string GenWhere(V6TableStruct structTable, IDictionary<string, object> keys,
            string oper = "=", bool and = true, string tableLable = "")
        {
            var and_or = and ? " AND " : " OR ";
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            string result = "";
            foreach (KeyValuePair<string, object> key in keys)
            {
                string FIELD = key.Key.ToUpper();
                if (structTable.ContainsKey(FIELD))
                {
                    var column = structTable[FIELD];
                    if (column.sql_data_type_string == "xml") continue;
                    // " and table.[key] = value"
                    if (key.Value == DBNull.Value && oper == "=")
                    {
                        result += string.Format("{0}{1}[{2}] is null",
                            and_or, tbL, key.Key);
                    }
                    else if (key.Value == DBNull.Value && oper == "<>")
                    {
                        result += string.Format("{0}{1}[{2}] is not null",
                            and_or, tbL, key.Key);
                    }
                    else
                    {
                        result += string.Format("{0}{1}[{2}] {3} {4}",
                            and_or, tbL,
                            key.Key, oper,

                            GenSqlStringValue(
                                key.Value,
                                column.sql_data_type_string,
                                column.ColumnDefault,
                                column.AllowNull,
                                column.MaxLength,
                                oper.ToLower())
                                );
                    }
                }
            }
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        /// <summary>
        /// <para>Tạo câu where kiểm tra mã lồng.</para>
        /// <para>Mã bắt đầu với giá trị này (Mã A10 so với A1)</para>
        /// <para>hoặc tồn tại mã nào đó bắt đầu giá trị này (Mã A so với A1).</para>
        /// </summary>
        /// <param name="tableStruct">Cấu trúc bảng.</param>
        /// <param name="keys">Dữ liệu khóa cần kiểm tra.</param>
        /// <param name="oper">Không dùng</param>
        /// <param name="and">Kết nối nhiều đoạn bằng and hoặc or</param>
        /// <param name="tableLable">Không dùng</param>
        /// <returns></returns>
        public static string GenWhere_CheckLong(V6TableStruct tableStruct, IDictionary<string, object> keys,
            string oper = "=", bool and = true, string tableLable = "")
        {
            var and_or = and ? " AND " : " OR ";
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            string result = "";
            foreach (KeyValuePair<string, object> key in keys)
            {
                string FIELD = key.Key.ToUpper();
                if (tableStruct.ContainsKey(FIELD))
                {
                    var column = tableStruct[FIELD];
                    if (column.sql_data_type_string == "xml") continue;
                    // " and table.[key] = value"
                    result += string.Format("{0} {2} in (Select {2} from {1} Where {4} like (Rtrim(Left({2}, Len({4}))))+'%')",//"  {1}[{2}] {3} {4}",
                        and_or, tableStruct.TableName,
                        key.Key, oper,
                        GenSqlStringValue(
                            key.Value,
                            column.sql_data_type_string,
                            column.ColumnDefault,
                            column.AllowNull,
                            column.MaxLength,
                            oper.ToLower())
                            );
                }
            }
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        public static string GenWhereParameter(V6TableStruct structTable, IDictionary<string, object> keys,
            out List<SqlParameter> plist, 
            string oper = "=", bool and = true, string tableLable = "")
        {
            var and_or = and ? " AND " : " OR ";
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            var p_list = new List<SqlParameter>();
            string result = "";
            foreach (KeyValuePair<string, object> key in keys)
            {
                string FIELD = key.Key.ToUpper();
                if (structTable.ContainsKey(FIELD))
                {
                    var column = structTable[FIELD];
                    // " and table.[key] = value"
                    result += string.Format("{0}{1}[{2}] {3} @{4}",
                        and_or, tbL,
                        key.Key, oper,
                        FIELD
                            );
                    p_list.Add(new SqlParameter("@" + FIELD, key.Value){SqlDbType = column.SqlDbType});
                }
            }
            if (result.Length > 4) result = result.Substring(4);
            plist = p_list;
            return result;
        }

        /// <summary>
        /// Dùng cho filter, bỏ qua value rỗng (continue).
        /// </summary>
        /// <param name="tableStruct"></param>
        /// <param name="keys"></param>
        /// <param name="oper">tự động thêm '%value%' khi dùng like</param>
        /// <param name="and"></param>
        /// <param name="tableLable"></param>
        /// <returns></returns>
        public static string GenWhere2(V6TableStruct tableStruct, IDictionary<string, object> keys,
            string oper = "=", bool and = true, string tableLable = "")
        {
            var and_or = and ? " AND " : " OR ";
            var oper0 = oper == "start" ? "like" : oper;
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            string result = "";
            foreach (KeyValuePair<string, object> key in keys)
            {
                if(key.Value is string && string.IsNullOrEmpty(key.Value as string))
                    continue;
                string FIELD = key.Key.ToUpper();
                if (tableStruct.ContainsKey(FIELD))
                {
                    var column = tableStruct[FIELD];
                    if (column.sql_data_type_string == "xml") continue;
                    // " and table.[key] = value"
                    if (key.Value == DBNull.Value && oper == "=")
                    {
                        result += string.Format("{0}{1}[{2}] is null",
                            and_or, tbL, key.Key);
                    }
                    else if (key.Value == DBNull.Value && oper == "<>")
                    {
                        result += string.Format("{0}{1}[{2}] is not null",
                            and_or, tbL, key.Key);
                    }
                    else
                    {
                        result += string.Format("{0}{1}[{2}] {3} {4}",
                            and_or, tbL,
                            key.Key, oper0,

                            GenSqlStringValue(
                                key.Value,
                                column.sql_data_type_string,
                                column.ColumnDefault,
                                column.AllowNull,
                                column.MaxLength,
                                oper.ToLower())
                        );
                    }
                }
            }
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        /// <summary>
        /// Nâng cấp của Genwhere2 có xử lý oper start
        /// </summary>
        /// <param name="structTable"></param>
        /// <param name="keys"></param>
        /// <param name="oper"></param>
        /// <param name="and"></param>
        /// <param name="tableLable"></param>
        /// <returns></returns>
        public static string GenWhere2_oper(V6TableStruct structTable, IDictionary<string, object> keys,
            string oper = "=", bool and = true, string tableLable = "")
        {
            var and_or = and ? " AND " : " OR ";
            var oper0 = oper == "start" ? "like" : oper;
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            string result = "";
            foreach (KeyValuePair<string, object> key in keys)
            {
                if (key.Value is string && string.IsNullOrEmpty(key.Value as string))
                    continue;
                string FIELD = key.Key.ToUpper();
                if (structTable.ContainsKey(FIELD))
                {
                    var column = structTable[FIELD];
                    if (column.sql_data_type_string == "xml") continue;
                    // " and table.[key] = value"
                    if (key.Value == DBNull.Value && oper == "=")
                    {
                        result += string.Format("{0}{1}[{2}] is null",
                            and_or, tbL, key.Key);
                    }
                    else if (key.Value == DBNull.Value && oper == "<>")
                    {
                        result += string.Format("{0}{1}[{2}] is not null",
                            and_or, tbL, key.Key);
                    }
                    else
                    {
                        result += string.Format("{0}{1}[{2}] {3} {4}",
                            and_or, tbL,
                            key.Key, oper0,

                            GenSqlStringValue_oper(
                                key.Value,
                                column.sql_data_type_string,
                                column.ColumnDefault,
                                column.AllowNull,
                                column.MaxLength,
                                oper)
                        );
                    }
                }
            }
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        /// <summary>
        /// mặc định bỏ qua string value rỗng. không dùng struct (không kiểm tra tồn tại, dễ lỗi)
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="oper">Không tự động thêm % khi dùng like</param>
        /// <param name="and"></param>
        /// <param name="tableLable"></param>
        /// <param name="ignoreEmptyString"></param>
        /// <returns></returns>
        public static string GenWhere2(IDictionary<string, object> keys,
            string oper = "=", bool and = true, string tableLable = "", bool ignoreEmptyString = true)
        {
            var and_or = and ? " AND " : " OR ";
            var tbL = string.IsNullOrEmpty(tableLable) ? "" : tableLable + ".";
            string result = "";
            foreach (KeyValuePair<string, object> key in keys)
            {
                if (ignoreEmptyString && key.Value is string && string.IsNullOrEmpty(key.Value as string))
                    continue;
                if (key.Value == DBNull.Value && oper == "=")
                {
                    result += string.Format("{0}{1}[{2}] is null",
                        and_or, tbL, key.Key);
                }
                else if (key.Value == DBNull.Value && oper == "<>")
                {
                    result += string.Format("{0}{1}[{2}] is not null",
                        and_or, tbL, key.Key);
                }
                else
                {
                    result += string.Format("{0}{1}[{2}] {3} {4}",
                        and_or, tbL,
                        key.Key, oper,
                        GenSqlStringValue(key.Value, false, oper));
                }
                
            }
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }
        
        public static string GenUpdateAMSql(int UserId, string tableName, V6TableStruct structTable,
            IDictionary<string, object> dataDictionary, IDictionary<string, object> keys, bool update_info_2 = true)
        {
            //GetStructureTableAndColumnsStruct();
            string sql = "Update [" + tableName + "] Set";// field = value[, field = value[...]]
            var serverDateTime = SqlConnect.GetServerDateTime();
            var dateString = serverDateTime.ToString("yyyyMMdd").Replace(" ", "");
            var timeString = serverDateTime.ToString("HH:mm:ss").Replace(" ", "");
            var datetimeString = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss").Replace(" : ", ":");

            foreach (V6ColumnStruct column in structTable.Values)
            {
                var FIELD = column.ColumnName.ToUpper();
                if (FIELD != "UID")
                {
                    if (dataDictionary.ContainsKey(FIELD)) // true)
                    {
                        
                        if (keys.ContainsKey(FIELD) //Không update key
                            || FIELD.ToUpper() == "DATE0"
                            || FIELD.ToUpper() == "TIME0"
                            || FIELD.ToUpper() == "DATE2"
                            || FIELD.ToUpper() == "TIME2"
                            || FIELD.ToUpper() == "DATETIME0"
                            || FIELD.ToUpper() == "DATETIME2"
                            || FIELD.ToUpper() == "USER_ID0"
                            || FIELD.ToUpper() == "USER_ID2"
                            || FIELD.ToUpper() == "CLIENT_NAME0"
                            || FIELD.ToUpper() == "CLIENT_NAME2")
                            continue;

                        object valueObj = dataDictionary[FIELD];
                        var value = GenSqlStringValue(valueObj,
                            column.sql_data_type_string, column.ColumnDefault, column.AllowNull, column.MaxLength);
                        
                        sql += "\n[" + FIELD + "] = " + value + ",";
                    }
                }
            }
            //Đặc biệt: update DATE2, TIME2
            if (update_info_2)
            {
                if (structTable.ContainsKey("DATE2")) sql += "\n[DATE2] = '" + dateString + "',";
                if (structTable.ContainsKey("TIME2")) sql += "\n[TIME2] = '" + timeString + "',";
                if (structTable.ContainsKey("DATETIME2")) sql += "\n[DATETIME2] = '" + datetimeString + "',";
                if (structTable.ContainsKey("USER_ID2")) sql += "\n[USER_ID2] = " + UserId + ",";
                if (structTable.ContainsKey("CLIENT_NAME2")) sql += "\n[CLIENT_NAME2] = '" + clientName + "',";
            }

            sql = sql.TrimEnd(',');
            var where = GenWhere(structTable, keys);
            if (where.Trim() == "") throw new Exception("Where rỗng!");
            sql += " Where " + where;
            
            return sql;
        }

        /// <summary>
        /// Tạo giá trị không null theo sql type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sqltype"></param>
        /// <returns></returns>
        public static string GenSqlStringValue(string value, string sqltype)
        {
            return GenSqlStringValue(value, sqltype, null, false, 0);
        }

        /// <summary>
        /// không null
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="sqltype"></param>
        /// <param name="defaultValue">Giá trị mặc định trong csdl</param>
        /// <param name="allowNull"></param>
        /// <param name="oper">like hoặc start hoặc =</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenSqlStringValue(object objValue, string sqltype, string defaultValue,
            bool allowNull, int length, string oper = "=")
        {
            switch (sqltype)
            {
                case "date":
                    if (objValue != null && objValue is DateTime)
                    {
                        DateTime d = (DateTime)objValue;
                        return "'" + d.ToString("yyyyMMdd") + "'";
                    }
                    break;
                case "smalldatetime":
                    if (objValue != null && objValue is DateTime)
                    {
                        DateTime d = (DateTime)objValue;
                        if(d.Year>=1900 && d.Year<=2079)
                            return "'" + d.ToString("yyyyMMdd") + "'";
                        return "'19000101'";
                    }
                    else if (objValue is decimal || objValue is int || objValue is double || objValue is long)
                    {
                        DateTime d = DateTime.FromOADate(Convert.ToDouble(objValue));
                        if(d.Year>=1900 && d.Year<=2079)
                            return "'" + d.ToString("yyyyMMdd") + "'";
                        return "'19000101'";
                    }
                    break;
                case "datetime":
                    if (objValue != null && objValue is DateTime)
                    {
                        DateTime d = (DateTime)objValue;
                        if (d.Year >= 1753)
                            return "'" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        return "'17530101'";
                    }
                    else if (objValue is decimal || objValue is int || objValue is double || objValue is long)
                    {
                        DateTime d = DateTime.FromOADate(Convert.ToDouble(objValue));
                        if(d.Year>=1900 && d.Year<=2079)
                            return "'" + d.ToString("yyyyMMdd") + "'";
                        return "'17530101'";
                    }
                    break;
                case "nchar":
                case "nvarchar":
                case "ntext":
                case "char":
                case "varchar":
                case "text":
                case "xml":
                    string text = objValue==null? null:objValue.ToString();
                    if (length > 0 && text != null && text.Length > length)
                    {
                        text = text.Left(length);
                    }

                    return GenSqlStringValueF(text, sqltype, defaultValue, allowNull, oper);
                    //break;
                case "bigint":
                case "numeric":
                case "bit":
                case "smallint":

                case "decimal":
                case "smallmoney":
                case "int":
                case "tinyint":
                case "money":
                case "float":
                case "real":
                    if (objValue != null && objValue is string)
                    {
                        switch (objValue.ToString().Trim().ToLower())
                        {
                            case "true":
                            case "ok":
                            case "yes":
                                return "1";
                            case "false":
                            case "no":
                            case "cancel":
                                return "0";
                            default:
                                objValue = ObjectAndString.StringToDecimal("" + objValue);
                                break;
                        }
                    }
                    break;
                default:
                    if (objValue is DateTime)
                    {
                        objValue = ObjectAndString.ObjectToString(objValue);
                    }
                    break;
            }
            
            var value = objValue==null? null:objValue.ToString();

            return GenSqlStringValueF(value, sqltype, defaultValue, allowNull, oper);
        }

        public static string GenSqlStringValue_oper(object objValue, string sqltype, string defaultValue,
            bool allowNull, int length, string oper = "=")
        {
            switch (sqltype)
            {
                case "date":
                    if (objValue != null && objValue is DateTime)
                    {
                        DateTime d = (DateTime)objValue;
                        return "'" + d.ToString("yyyyMMdd") + "'";
                    }
                    break;
                case "smalldatetime":
                    if (objValue != null && objValue is DateTime)
                    {
                        DateTime d = (DateTime)objValue;
                        if (d.Year >= 1900 && d.Year <= 2079)
                            return "'" + d.ToString("yyyyMMdd") + "'";
                        return "'19000101'";
                    }
                    else if (objValue is decimal || objValue is int || objValue is double || objValue is long)
                    {
                        DateTime d = DateTime.FromOADate(Convert.ToDouble(objValue));
                        if(d.Year>=1900 && d.Year<=2079)
                            return "'" + d.ToString("yyyyMMdd") + "'";
                        return "'19000101'";
                    }
                    break;
                case "datetime":
                    if (objValue != null && objValue is DateTime)
                    {
                        DateTime d = (DateTime)objValue;
                        if (d.Year >= 1753)
                            return "'" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        return "'17530101'";
                    }
                    else if (objValue is decimal || objValue is int || objValue is double || objValue is long)
                    {
                        DateTime d = DateTime.FromOADate(Convert.ToDouble(objValue));
                        if(d.Year>=1900 && d.Year<=2079)
                            return "'" + d.ToString("yyyyMMdd") + "'";
                        return "'17530101'";
                    }
                    break;
                case "nchar":
                case "nvarchar":
                case "ntext":
                case "char":
                case "varchar":
                case "text":
                case "xml":
                    string text = objValue == null ? null : objValue.ToString();
                    if (length > 0 && text != null && text.Length > length)
                    {
                        text = text.Left(length);
                    }

                    return GenSqlStringValueF_oper(text, sqltype, defaultValue, allowNull, oper);
                //break;
                case "bigint":
                case "numeric":
                case "bit":
                case "smallint":

                case "decimal":
                case "smallmoney":
                case "int":
                case "tinyint":
                case "money":
                case "float":
                case "real":
                    if (objValue != null && objValue is string)
                    {
                        switch (objValue.ToString().Trim().ToLower())
                        {
                            case "true":
                            case "ok":
                            case "yes":
                                return "1";
                            case "false":
                            case "no":
                            case "cancel":
                                return "0";
                        }
                    }
                    break;
                default:
                    if (objValue is DateTime)
                    {
                        objValue = ObjectAndString.ObjectToString(objValue);
                    }
                    break;
            }

            var value = objValue == null ? null : objValue.ToString();

            return GenSqlStringValueF_oper(value, sqltype, defaultValue, allowNull, oper);
        }

        /// <summary>
        /// Loại bỏ injection, thêm single quote ('), chuyển mã U->A
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sqltype"></param>
        /// <param name="defaultValue">Giá trị mặc định trong csdl</param>
        /// <param name="allowNull"></param>
        /// <param name="like">like hoặc start hoặc =</param>
        /// <returns></returns>
        public static string GenSqlStringValueF(string value, string sqltype, string defaultValue,
            bool allowNull, string like)
        {
            if (!string.IsNullOrEmpty(value))
                value = RemoveSqlInjection(value);

            string s = "";
            if (value!=null)
            {
                switch (sqltype)
                {
                    case "nchar":
                    case "nvarchar":
                    case "ntext":
                    case "char":
                    case "varchar":
                    case "text":
                    case "xml":
                        s = value;
                        if (like == "like")
                        {
                            s = "N'%" + s + "%'";
                        }
                        else if (like == "start")
                        {
                            s = "N'" + s + "%'";
                        }
                        else
                        {
                            s = "N'" + s + "'";
                        }
                        //.Replace("'", "''") đã có ở trên value = RemoveSqlInjection(value);
                        
                        break;
                    case "date":
                    case "smalldatetime":
                    case "datetime":
                        
                        try
                        {
                            if (string.IsNullOrEmpty(value) || s == "null")
                            {
                                if (allowNull)
                                    s = "null";
                                else
                                    s = "'" + SqlConnect.GetServerDateTime().ToString("yyyyMMdd") + "'";
                            }
                            else
                            {
                                DateTime d;
                                try
                                {
                                    //d = DateTime.ParseExact(value, "d/M/yyyy", null);
                                    if (!DateTime.TryParseExact(value, "d/M/yyyy", null, DateTimeStyles.None, out d))
                                    {
                                        if (!DateTime.TryParse(value, out d))
                                        {
                                            d = SqlConnect.GetServerDateTime();
                                        }
                                    }
                                }
                                catch
                                {
                                    d = SqlConnect.GetServerDateTime();
                                }
                                
                                if(sqltype == "date") return "'" + d.ToString("yyyyMMdd") + "'";
                                else if(sqltype == "smalldatetime")
                                {
                                    if (d.Year >= 1900 && d.Year <= 2079)
                                        return "'" + d.ToString("yyyyMMdd") + "'";
                                    else return "'19000101'";
                                }
                                else if (sqltype == "datetime")
                                {
                                    if (d.Year >= 1753)
                                        return "'" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                    else return "'17530101'";
                                }
                            }
                        }
                        catch
                        {
                            if (allowNull)
                                s = "null";
                            else
                                s = "'" + SqlConnect.GetServerDateTime().ToString("yyyyMMdd") + "'";
                        }
                        break;
                    case "bigint":
                    case "numeric":
                    case "bit":
                    case "smallint":
                    case "decimal":
                    case "smallmoney":
                    case "int":
                    case "tinyint":
                    case "money":
                    case "float":
                    case "real":
                        try
                        {
                            switch (value.Trim().ToLower())
                            {
                                case "true":
                                case "ok":
                                case "yes":
                                    return "1";
                                case "false":
                                case "no":
                                case "cancel":
                                    return "0";
                            }

                            decimal tryp;
                            if (decimal.TryParse(value, out tryp)) s = value.Replace(_comma_decimal_symbol, ".");
                            else s = "0";
                        }
                        catch
                        {
                            s = "0";
                        }
                        break;
                    case "uniqueidentifier":
                        s = "'"+value+"'";
                        break;
                    default:
                        s = "'" + value + "'";
                        break;
                }
            }
            else if (defaultValue != null && defaultValue.Length>0)
            {
                s = defaultValue;
            }
            else if (allowNull)
            {
                if (",nchar,nvarchar,ntext,char,varchar,text,xml,".Contains("," + sqltype + ","))
                {
                    s = "''";
                }
                else if (sqltype == "uniqueidentifier")
                {
                    s = "null";
                }
                else
                {
                    s = "null";
                }
            }
            else
            {
                switch (sqltype)
                {
                    case "nchar":
                    case "nvarchar":
                    case "ntext":
                    case "char":
                    case "varchar":
                    case "text":
                    case "xml":
                        s = "''";
                        break;
                    case "date":
                    case "smalldatetime":
                    case "datetime":
                        s = "'" + new DateTime(1900, 1, 1).ToString("yyyyMMdd") + "'";
                        break;
                    case "bigint":
                    case "numeric":
                    case "bit":
                    case "smallint":
                    case "decimal":
                    case "smallmoney":
                    case "int":
                    case "tinyint":
                    case "money":
                        s = "0";
                        break;
                    case "uniqueidentifier":
                        s = "'" + Guid.NewGuid() + "'";
                        break;
                    default:
                        s = "''";
                        break;
                }
            }

            return s;
        }

        public static string GenSqlStringValueF_oper(string value, string sqltype, string defaultValue,
            bool allowNull, string oper)
        {
            if (!string.IsNullOrEmpty(value))
                value = RemoveSqlInjection(value);

            string s = "";
            bool like = oper == "like";
            bool start = oper == "start";
            if (value != null)
            {
                switch (sqltype)
                {
                    case "nchar":
                    case "nvarchar":
                    case "ntext":
                    case "char":
                    case "varchar":
                    case "text":
                    case "xml":
                        s = value;
                        if (like)
                        {
                            s = "N'%" + s + "%'";
                        }
                        else if (start)
                        {
                            s = "N'" + s + "%'";
                        }
                        else
                        {
                            s = "N'" + s + "'";
                        }
                        //.Replace("'", "''") đã có ở trên value = RemoveSqlInjection(value);

                        break;
                    case "date":
                    case "smalldatetime":
                    case "datetime":

                        try
                        {
                            if (string.IsNullOrEmpty(value) || s == "null")
                            {
                                if (allowNull)
                                    s = "null";
                                else
                                    s = "'" + SqlConnect.GetServerDateTime().ToString("yyyyMMdd") + "'";
                            }
                            else
                            {
                                DateTime d;
                                try
                                {
                                    //d = DateTime.ParseExact(value, "d/M/yyyy", null);
                                    if (!DateTime.TryParseExact(value, "d/M/yyyy", null, DateTimeStyles.None, out d))
                                    {
                                        if (!DateTime.TryParse(value, out d))
                                        {
                                            d = SqlConnect.GetServerDateTime();
                                        }
                                    }
                                }
                                catch
                                {
                                    d = SqlConnect.GetServerDateTime();
                                }

                                if (sqltype == "date") return "'" + d.ToString("yyyyMMdd") + "'";
                                else if (sqltype == "smalldatetime")
                                {
                                    if (d.Year >= 1900 && d.Year <= 2079)
                                        return "'" + d.ToString("yyyyMMdd") + "'";
                                    else return "'19000101'";
                                }
                                else if (sqltype == "datetime")
                                {
                                    if (d.Year >= 1753)
                                        return "'" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                    else return "'17530101'";
                                }
                            }
                        }
                        catch
                        {
                            if (allowNull)
                                s = "null";
                            else
                                s = "'" + SqlConnect.GetServerDateTime().ToString("yyyyMMdd") + "'";
                        }
                        break;
                    case "bigint":
                    case "numeric":
                    case "bit":
                    case "smallint":
                    case "decimal":
                    case "smallmoney":
                    case "int":
                    case "tinyint":
                    case "money":
                    case "float":
                    case "real":
                        try
                        {
                            switch (value.Trim().ToLower())
                            {
                                case "true":
                                case "ok":
                                case "yes":
                                    return "1";
                                case "false":
                                case "no":
                                case "cancel":
                                    return "0";
                            }

                            decimal tryp;
                            if (decimal.TryParse(value, out tryp)) s = value.Replace(_comma_decimal_symbol, ".");
                            else s = "0";
                        }
                        catch
                        {
                            s = "0";
                        }
                        break;
                    case "uniqueidentifier":
                        s = "'" + value + "'";
                        break;
                    default:
                        s = "'" + value + "'";
                        break;
                }
            }
            else if (defaultValue != null && defaultValue.Length > 0)
            {
                s = defaultValue;
            }
            else if (allowNull)
            {
                if (",nchar,nvarchar,ntext,char,varchar,text,xml,".Contains("," + sqltype + ","))
                {
                    s = "''";
                }
                else if (sqltype == "uniqueidentifier")
                {
                    s = "null";
                }
                else
                {
                    s = "null";
                }
            }
            else
            {
                switch (sqltype)
                {
                    case "nchar":
                    case "nvarchar":
                    case "ntext":
                    case "char":
                    case "varchar":
                    case "text":
                    case "xml":
                        s = "''";
                        break;
                    case "date":
                    case "smalldatetime":
                    case "datetime":
                        s = "'" + new DateTime(1900, 1, 1).ToString("yyyyMMdd") + "'";
                        break;
                    case "bigint":
                    case "numeric":
                    case "bit":
                    case "smallint":
                    case "decimal":
                    case "smallmoney":
                    case "int":
                    case "tinyint":
                    case "money":
                    case "float":
                    case "real":
                        s = "0";
                        break;
                    case "uniqueidentifier":
                        s = "'" + Guid.NewGuid() + "'";
                        break;
                    default:
                        s = "''";
                        break;
                }
            }

            return s;
        }

        /// <summary>
        /// Không có cấu trúc
        /// </summary>
        /// <param name="value"></param>
        /// <param name="allowNull"></param>
        /// <param name="oper">Kiểu phép so sánh.</param>
        /// <returns></returns>
        public static string GenSqlStringValue(object value, bool allowNull, string oper = "=")
        {
            string s;
            bool like = oper == "like";
            bool start = oper == "start";
            if (value != null)
            {
                switch (value.GetType().ToString())
                {
                    case "System.DateTime":
                    case "System.DateTime?":
                    case "System.Nullable`1[System.DateTime]":
                        try
                        {
                            s = "'" + ((DateTime)value)
                                .ToString("yyyyMMdd") + "'";
                        }
                        catch
                        {
                            if (allowNull)
                                s = "null";
                            else
                                s = "'" + SqlConnect.GetServerDateTime().ToString("yyyyMMdd") + "'";
                        }
                        break;
                    case "System.Boolean":
                        s = (bool)value ? "1" : "0";
                        break;
                    case "System.Byte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Decimal":
                    case "System.Double":
                        try
                        {
                            s = value.ToString().Replace(_comma_decimal_symbol, ".");
                        }
                        catch
                        {
                            s = "0";
                        }
                        break;
                    case "System.DBNull":
                        s = "null";
                        break;
                    default:
                        s = "N'" + (like? "%" : "")
                            + value.ToString().Trim().Replace("'", "''")
                            + (like || start ? "%" : "") + "'";
                        break;
                }
            }
            else if (allowNull)
            {
                s = "null";
            }
            else
            {
                s = "''";
            }

            return s;
        }

        

        /// <summary>
        /// Tạo câu sql delete
        /// </summary>
        /// <param name="structTable"></param>
        /// <param name="keys">Có sẵn value, khi dùng không cần thêm tham số.</param>
        /// <returns></returns>
        public static string GenDeleteSql(V6TableStruct structTable, IDictionary<string,object> keys)
        {
            var where = GenWhere(structTable, keys);

            if (where.Length > 0)
            {
                var sql = "DELETE FROM [" + structTable.TableName + "]";
                sql += " WHERE " + where;
                return sql;
            }

            return "";
        }

        ///// <summary>
        ///// Tạo một key AND
        ///// </summary>
        ///// <returns>" AND FieldName LIKE Values%"</returns>
        //public static string GenManyLikeKey(SortedList<string, string> list)
        //{
        //    string keys = "";
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        keys += GenOneLikeKey(list.Keys[i], list.Values[i]);
        //    }
        //    return keys;
        //}
        public static string RemoveSqlInjection(string value)
        {   
            value = value.Replace("'", "''");
            //value = value.Replace("drop", "");
            //value = value.Replace("delete", "");
            //value = value.Replace("insert", "");
            //value = value.Replace("update", "");
            //value = value.Replace("select", "");
            return value;
        }
        public static string GenInKey(string field, string value_s)
        {
            if (value_s == "") return "";

            value_s = RemoveSqlInjection(value_s);
            string[] values = value_s.Split(',');
            value_s = "(";
            foreach (string value in values)
            {
                value_s += "'" + value + "',";
            }
            value_s = value_s.TrimEnd(',') + ")";

            var key = " AND " + field + " IN " + value_s;
            return key;
        }
        //public static string GenOneMatchKey(string field, string value)
        //{
        //    if (value == "") return "";
        //    string key = "";
        //    value = RemoveSqlInjection(value);
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(value))
        //        {
        //            key = " AND " + field + " = '" + value + "'";
        //        }
        //    }
        //    catch
        //    {
        //        // ignored
        //    }
        //    return key;
        //}
        //public static string GenOneLikeKey(string field, string value)
        //{
        //    if (value == "") return "";
        //    string key = "";
        //    value = RemoveSqlInjection(value);
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(value))
        //        {
        //            if (!value.Contains('%') && !value.Contains('_'))
        //            {
        //                value = "%" + value + "%";
        //            }
        //            key = " AND " + field + " LIKE '" + value + "'";
        //        }
        //    }
        //    catch
        //    {
        //        // ignored
        //    }
        //    return key;
        //}

        public static bool checkSQLConnection(string strCon)
        {
            try
            {
                SqlConnection sc = new SqlConnection(strCon);
                sc.Open();
                sc.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }

        

        /// <summary>
        /// Trả về true nếu hai chuỗi khác rỗng và có ngày tháng bằng nhau theo định dạng được chỉ định
        /// </summary>
        /// <param name="ngay1">Chuỗi ngày tháng cần so sánh 1</param>
        /// <param name="dinhdang1">Định dạng ngày tháng của ngay1</param>
        /// <param name="ngay2">Chuỗi ngày tháng cần so sánh 2</param>
        /// <param name="dinhdang2">Định dạng ngày tháng của ngay2</param>
        /// <returns>1:Cùng ngày, 0: khác ngay, -1:Lỗi</returns>
        public static int SoSanhChuoiNgayThang(string ngay1, string dinhdang1, string ngay2, string dinhdang2)
        {
            if (string.IsNullOrEmpty(ngay1) || string.IsNullOrEmpty(ngay2)) return -1;
            try
            {
                string date1 = DateTime.ParseExact(ngay1, dinhdang1, null).ToString("yyyyMMdd").Replace(" ", "");
                string date2 = DateTime.ParseExact(ngay2, dinhdang2, null).ToString("yyyyMMdd").Replace(" ", "");
                if (date1 == date2)
                {
                    return 1;
                }
                else return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Biến chuỗi giá trị thành chuỗi sử dụng trong chuỗi sql. (tự bọc dấu ' )
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Operator">like|start|=|...</param>
        /// <returns></returns>
        public static string FormatStringValue(string value, string Operator = "like")
        {
            if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
                return string.Format("N'{0}'", value.Replace("'", "''"));
            else if (Operator == "like")
                return string.Format("N'%{0}%'", value.Replace("'", "''"));
            else if (Operator == "start")
                return string.Format("N'{0}%'", value.Replace("'", "''"));
            return "";
        }

        /// <summary>
        /// Lấy chuỗi truy vấn dùng cho Where trong sql.
        ///  If value not containt(",") return "AccessibleName like 'ABC'";
        /// else return "AccessibleName like 'ABC' or AccessibleName like 'DEF'"
        /// </summary>
        /// <param name="text">Giá trị kiểu chuỗi</param>
        /// <param name="accessiblename">FieldName</param>
        /// <param name="oper">các dấu so sánh trong sql.
        /// nếu dùng like sẽ dùng % ở 2 đầu.
        /// nếu dùng start chỉ dùng % ở sau.</param>
        /// <returns></returns>
        public static string GetQuery(string text, string accessiblename, string oper = "like")
        {
            var sValue = text.Trim();
            var result = "";

            var oper1 = oper == "start" ? "like" : oper;

            if (sValue.Contains(","))
            {
                string[] sss = sValue.Split(',');
                foreach (string s in sss)
                {
                    result += string.Format(" or {0} {1} {2}", accessiblename, oper1, FormatStringValue(s, oper));
                }

                if (result.Length > 4)
                {
                    result = result.Substring(4);
                    result = string.Format("({0})", result);
                }
            }
            else
            {
                result = string.Format("{0} {1} {2}", accessiblename, oper1, FormatStringValue(sValue, oper1));
            }
            return result;
        }
    }//End class
}//End namespace

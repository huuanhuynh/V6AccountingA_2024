using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;
using V6AccountingBusiness.Invoices;
//using V6SyncLibrary2021.EXEC5.Invoices;
using V6Tools;

namespace V6SyncLibrary2021
{
    public class MyThread : BaseThread
    {
        #region ==== BaseVar ====
        //private Thread thread;
        //public string ThreadName = "";
        //public int Index = 0;
        //public int Status = 0;
        //public int Value = 0;
        //public bool Error = false;
        //public string Message = "";
        //protected static Random r = new Random();
        #endregion
        #region ==== New Var ====
        
        string DatabaseName;
        
        private string _CONSTRING_MAIN;
        private string sqlConString1_nouse, _sqlConString2, sqlConString3_nouse;
        
        private string sqlTableName, _sqlSelectFields, sqlPrimarykeyField;

        #endregion ==== | ====

        DataTable sqlStructTable;

        #region ==== Khởi tạo đối tượng ====
        
        public MyThread(string conString1, string conString2, string Name, int Index, DataRow Config):base(Name, Index)
        {
            _CONSTRING_MAIN = conString1;
            _sqlConString2 = conString2;
            _log = new Logger(__dir, __logName);

            Sync2ThConfig = new Sync2THConfig(__dir, Config.ToDataDictionary());            
        }
        
        #endregion

          ///////////////////////////////////////////
         ///////////////////////////////////////////
        ///////////////////////////////////////////
        #region ==== ==== ==== Viết code ở đây ==== ==== ====
        
        //=====================================
        /// <summary>
        /// Hàm hoạt động chính của tiến trình.
        /// </summary>        
        protected override void VoidMain()
        {
            _Message = "";
            _HaveLog = false;

            if (IsInTime)
            {
                _HaveLog = true;
                //Login();
                if (Sync2ThConfig.Type == "1")
                {
                    Log("Intime type1 nocode.");
                    
                }
                else if (Sync2ThConfig.Type == "2") //sql-sql
                {
                    _HaveLog = true;
                    EXEC2();
                }
                else if (Sync2ThConfig.Type == "3" || Sync2ThConfig.Type == "4")
                {
                    _Message = "Type '" + Sync2ThConfig.Type + "' not run this time!";
                }
            }
            else // Out Time
            {
                if (Sync2ThConfig.Type == "3") //backup
                {
                    _HaveLog = true;
                    
                }
                else if (Sync2ThConfig.Type == "4") //Backup folder
                {
                    _HaveLog = true;
                    
                }
            }

            if (Sync2ThConfig.Type == "5") // Đồng bộ dữ liệu khác cấu trúc. Từ chức năng Hệ thống/Quản lý số liệu/ Nhận dữ liệu từ PM khác
            {
                _HaveLog = true;
                
            }
        }
        
        private bool f9Running;
        private string f9Message = "";
        private string f9MessageAll = "";
        V6Invoice81 Invoice = null;
        private IDictionary<string, object> AM_DATA;
        private bool chkAutoSoCt_Checked = false;
        

        #region === Main config var ===

        
        #endregion === Main config var ===
        
        /// <summary>
        /// Trả về value của Key, không có trả về chuỗi rỗng ""
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetXmlValue(string XmlFile, string Key)
        {
            //string xmlFile = APPPATH + xmlFileName;
            XmlTextReader reader = new XmlTextReader(XmlFile.ToLower());
            try
            {
                string value = "";
                while (reader.Read())
                {
                    if (reader.Name.ToUpper() == Key.ToUpper())
                    {
                        value = reader.GetAttribute("value");
                        break;
                    }
                }
                reader.Close();
                return value;
            }
            catch
            {
                reader.Close();
                return "";
            }
        }

        public bool existTable(string tableName, string strCon)
        {
            try
            {
                string strCommand = "select 1 from " + tableName + " where 1 = 0";
                SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public byte existRow(string tableName, string strCon, string filter, string sqlWhere)
        {
            try
            {
                string where = ""; if (filter != "") where = " where " + filter; ;
                string strCommand = "select count(1) from [" + tableName + "]" + where;
                //_MessageBox.Show(strCommand);
                if ((int)SqlHelper.ExecuteDataset(strCon, CommandType.Text, strCommand).Tables[0].Rows[0][0] > 0)
                {
                    if (where != "") where += (" and " + sqlWhere);
                    strCommand = "select count(1) from [" + tableName + "]" + where;
                    //_MessageBox.Show(strCommand);
                    if ((int)SqlHelper.ExecuteDataset(strCon, CommandType.Text, strCommand).Tables[0].Rows[0][0] > 0)
                        return 2;
                    else
                        return 1;
                
	            }
                else return 0;
            }
            catch
            {
                return 0;
            }
        }
        
        private static DataTable GetSQLData(string sqlConString, string TableName, string Fields, string where)
        {
            Fields = Fields.Replace("'", "''");
            SqlConnection con = new SqlConnection(sqlConString);
            if (where.Trim() != "") where = " Where " + where;
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select " + Fields + " from [" + TableName + "]" + where, con);
                da.Fill(ds, TableName);
                con.Close();
                return ds.Tables[TableName];
            }
            catch
            {
                con.Close();
                return null;
            }
        }


        #region ==== Convert Table ====
        public static DataTable ConvertTable(DataTable data, string convertFrom, string convertTo)
        {
            DataTable convertTable = data.Copy();
            if (!string.IsNullOrEmpty(convertFrom) || !string.IsNullOrEmpty(convertTo))
            {
                //Chuyễn mã các tiêu đề cột
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    switch (data.Columns[i].DataType.ToString())
                    {
                        //case "System.Char":
                        case "System.String":
                            string s = ChuyenMaTiengViet.VIETNAM_CONVERT(data.Columns[i].ColumnName, convertFrom, convertTo);
                            data.Columns[i].ColumnName = s;
                            break;
                    }

                }

                //Chuyễn mã từng dòng dữ liệu
                for (int i = 0; i < convertTable.Rows.Count; i++)
                {
                    for (int j = 0; j < convertTable.Columns.Count; j++)
                    {
                        if (data.Columns[j].DataType.ToString() == "System.String")
                        {
                            convertTable.Rows[i][j] = ChuyenMaTiengViet.VIETNAM_CONVERT(convertTable.Rows[i][j].ToString(), convertFrom, convertTo);
                        }
                    }
                }
            }
            return convertTable;
        }
        #endregion ==== Convert Table ====


        private string error = "";
        private int UpdateDatabase(string _Con, string tableName, DataTable structTable, DataTable data)
        {
            int n = 0;
            error = "";
            foreach (DataRow row in data.Rows)
            {
                try
                {
                    UpdateOne(_Con, tableName, structTable, row);
                    n++;
                }
                catch (Exception ex)
                {
                    if (error.Length > 0) error += "\n";
                    error += ex.Message;
                }
            }
            return n;
        }

        private int InsertDatabase(string _Con, string tableName, string primaryKey, DataTable structTable, DataTable data, string sqlWhere)
        {
            
            int n = 0;
            error = "";
            foreach (DataRow row in data.Rows)
            {
                string curentkey = GenSqlStringValue(row[primaryKey], false);
                
                if (existRow(tableName, _Con, "[" + primaryKey + "]=" + curentkey, sqlWhere) == 0)
                    try
                    {
                        _Message = "Insert sql [" + tableName + "]: " + curentkey;
                        InsertOne(_Con, tableName, structTable, row);
                        n++;
                    }
                    catch (Exception ex)
                    {
                        _Message = "Insert sql error: " + ex.Message;
                        if (error.Length > 0) error += "\n";
                        error += ex.Message;
                    }
            }
            return n;
        }

        private void InsertOne(string _Con, string tableName, DataTable structTable, DataRow row)
        {
            string insertSql = GenInsertSql(tableName, structTable, row);
            SqlHelper.ExecuteNonQuery(_Con, CommandType.Text, insertSql);
        }
        private void UpdateOne(string _Con, string tableName, DataTable structTable, DataRow row)
        {
            string PrimaryKeyField;
            DataColumn[] primaricolumns = structTable.PrimaryKey;
            if (primaricolumns.Length == 1)
            {
                PrimaryKeyField = primaricolumns[0].ColumnName;
                string PrimaryKeyValue = row[PrimaryKeyField].ToString();
                string updateSql = GenUpdateSql(tableName, PrimaryKeyField, PrimaryKeyValue, structTable, row);
                SqlHelper.ExecuteNonQuery(_Con, CommandType.Text, updateSql);
            }
            else
            {
                throw new Exception("UpdateOne: Chưa hỗ trợ bảng nhiều khóa chính hoặc không có khóa chính");
            }
        }

        public string GenUpdateSql(string tableName, string PrimaryKeyField, object PrimaryKeyValue, DataTable structTable, DataRow row)
        {
            //GetStructureTableAndColumnsStruct();
            string sql = "Update [" + tableName + "] Set";// field = value[, field = value[...]]
            string field = null;
            string value = "";
            for (int i = 0; i < structTable.Columns.Count; i++)
            {
                field = structTable.Columns[i].ColumnName;
                if (field.ToLower() == PrimaryKeyField.ToLower())
                    continue;


                value = GenSqlStringValue(row[field], structTable.Columns[i].AllowDBNull);
                sql += "\n[" + field + "] = " + value + ",";

            }
            sql = sql.TrimEnd(',');
            sql += " Where [" + PrimaryKeyField + "] = " +
                GenSqlStringValue(PrimaryKeyValue.ToString(), PrimaryKeyValue.GetType());

            return sql;
        }
        public string GenInsertSql(string tableName, DataTable structTable, DataRow row)
        {
            //GetStructureTableAndColumnsStruct();
            string sql = "Insert into [" + tableName + "] (";
            string fields = "";
            string values = "";
            for (int i = 0; i < structTable.Columns.Count; i++)
            {
                if (true)
                {
                    string field = structTable.Columns[i].ColumnName;
                    fields += ",[" + field + "]";
                    if (row.Table.Columns.Contains(field))
                    {
                        values += "," + GenSqlStringValue(
                            //---------
                             row[field] ,
                            structTable.Columns[i].AllowDBNull);
                    }
                    else
                    {
                        values += "," + GenSqlStringValue(null, structTable.Columns[i].DataType, structTable.Columns[i].AllowDBNull);
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
            sql += fields + ") Values (" + values + ")";
            return sql;
        }


        public string GenUpdateSql(string tableName, string PrimaryKeyField, DataTable structTable, DataRow row)
        {
            //GetStructureTableAndColumnsStruct();
            string sql = "Update [" + tableName + "] Set";// field = value[, field = value[...]]
            string field = null;
            string value = "";
            for (int i = 0; i < structTable.Columns.Count; i++)
            {
                field = structTable.Columns[i].ColumnName;
                if (field.ToLower() == PrimaryKeyField.ToLower())
                    continue;

                value = GenSqlStringValue(row[field], structTable.Columns[i].AllowDBNull);
                sql += " [" + field + "] = " + value + ",";

            }
            sql = sql.TrimEnd(',');
            object PrimaryKeyValue = row[PrimaryKeyField];
            sql += " Where [" + PrimaryKeyField + "] = " +
                GenSqlStringValue(PrimaryKeyValue.ToString(), PrimaryKeyValue.GetType());

            return sql;
        }

        public string RemoveSqlInjection(string value)
        {
            value = value.Replace("'", "''");
            //value = value.Replace("drop", "");
            //value = value.Replace("delete", "");
            //value = value.Replace("insert", "");
            //value = value.Replace("update", "");
            //value = value.Replace("select", "");
            return value;
        }


        public string GenSqlStringValue(string value, Type type)
        {
            return GenSqlStringValue(value, type, false);
        }
        /// <summary>
        /// Loại bỏ injection, thêm single quote ('), chuyển mã U->A
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="AllowNull"></param>
        /// <returns></returns>
        public string GenSqlStringValue(string value, Type type, bool AllowNull)
        {
            if (!string.IsNullOrEmpty(value))
                value = RemoveSqlInjection(value);

            string s = "";
            if (!string.IsNullOrEmpty(value))
            {
                switch (type.ToString())
                {
                    case "System.Char":
                    case "System.String":
                        s = "'" + value.ToString().Trim().Replace("'", "''") + "'";
                        break;
                    case "System.DateTime":
                        try
                        {
                            s = "'" + DateTime.ParseExact(value, "d/M/yyyy", null)
                                .ToString("yyyy/MM/dd") + "'";
                        }
                        catch
                        {
                            if (AllowNull)
                                s = "null";
                            else
                                s = "'" + new DateTime(1900, 1, 1)
                                    .ToString("yyyy/MM/dd") + "'";
                        }
                        break;
                    case "System.Boolean":
                        if (value.ToLower().Contains("true") || value.ToLower().Contains("yes") || value.Trim() == "1")
                            s = "1";
                        else s = "0";
                        break;
                    case "System.Byte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Decimal":
                    case "System.Double":
                        try
                        {
                            decimal tryp;
                            if (decimal.TryParse(value, out tryp)) s = value;
                            else s = "0";
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
                        s = "'" + value + "'";
                        break;
                }
            }
            else if (AllowNull)
            {
                s = "null";
            }
            else
            {
                switch (type.ToString())
                {
                    case "System.Char":
                    case "System.String":
                        s = "''";
                        break;
                    case "System.DateTime":
                        s = "'" + new DateTime(1900, 1, 1).ToString("yyyy/MM/dd") + "'";
                        break;
                    case "System.Boolean":
                    case "System.Byte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Decimal":
                    case "System.Double":
                        s = "0";
                        break;
                    default:
                        s = "''";
                        break;
                }
            }

            return s;
        }
        public string GenSqlStringValue(object value, bool AllowNull)
        {
            string s = "";
            if (value != null)
            {
                switch (value.GetType().ToString())
                {
                    case "System.Char":
                    case "System.String":
                        s = "'" + value.ToString().Trim().Replace("'", "''") + "'";
                        break;
                    case "System.DateTime":
                        try
                        {
                            s = "'" + ((DateTime)value)
                                .ToString("yyyy/MM/dd") + "'";
                        }
                        catch
                        {
                            if (AllowNull)
                                s = "null";
                            else
                                s = "'" + new DateTime(1900, 1, 1)
                                    .ToString("yyyy/MM/dd") + "'";
                        }
                        break;
                    case "System.Boolean":
                        if ((bool)value == true)
                            s = "1";
                        else s = "0";
                        break;
                    case "System.Byte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Decimal":
                    case "System.Double":
                        try
                        {
                            s = value.ToString();
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
                        s = "'" + value + "'";
                        break;
                }
            }
            else if (AllowNull)
            {
                s = "null";
            }
            else
            {
                //switch (value.GetType().ToString())
                //{
                //    case "System.Char":
                //    case "System.String":
                //        s = "''";
                //        break;
                //    case "System.DateTime":
                //        s = "'" + new DateTime(1900, 1, 1).ToString("yyyy/MM/dd") + "'";
                //        break;
                //    case "System.Boolean":
                //    case "System.Byte":
                //    case "System.Int16":
                //    case "System.Int32":
                //    case "System.Int64":
                //    case "System.Decimal":
                //    case "System.Double":
                //        s = "0";
                //        break;
                //    default:
                        s = "''";
                  //      break;
                //}
            }

            return s;
        }

        public DataTable getTableStruct(string _ConString, string tableName)
        {
            DataTable structTable, _ColumnsStruct;
            if (string.IsNullOrEmpty(tableName)) throw new Exception("V6ERRORNOTABLE");
            try
            {
                structTable = SqlHelper.
                    ExecuteDataset(_ConString, CommandType.Text,
                    "Select * From [" + tableName + "] Where 1=0")
                    .Tables[0];

                _ColumnsStruct = SqlHelper.
                    ExecuteDataset(_ConString, CommandType.Text,
                    "Select ORDINAL_POSITION, COLUMN_NAME," +
                    " DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH" +
                    " From INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME = '" +
                    tableName + "'" +
                    " Order by ORDINAL_POSITION")
                    .Tables[0];

                foreach (DataRow row in _ColumnsStruct.Rows)
                {
                    structTable.Columns[row["COLUMN_NAME"].ToString()].AllowDBNull =
                       "YES" == row["IS_NULLABLE"].ToString();

                    if (row["CHARACTER_MAXIMUM_LENGTH"].ToString() != "")
                        structTable.Columns[row["COLUMN_NAME"].ToString()].MaxLength =
                            int.Parse(row["CHARACTER_MAXIMUM_LENGTH"].ToString());
                }
                //Get primary key
                string[] pks = GetPrimaryColumnName(_ConString, tableName);
                structTable.PrimaryKey = new DataColumn[] { structTable.Columns[pks[0]] };

                return structTable;
            }
            catch// (Exception ex)
            {
                return new DataTable();
                //throw;
            }
        }
        public string[] GetPrimaryColumnName(string _Con, string tableName)
        {
            DataTable tableStruct = SqlHelper.ExecuteDataset(_Con, "sp_pkeys", tableName, null, null).Tables[0];
            List<string> Lpk = new List<string>();
            foreach (DataRow row in tableStruct.Rows)
            {
                Lpk.Add(row[3].ToString());
            }
            return Lpk.ToArray();
        }
        public void AddColumn(string _Con, string tableName, string columnName, Type type, int length)
        {
            try
            {
                string s = "";
                switch (type.ToString())
                {
                    case "System.Char":
                    case "System.String":
                        s = "nvarchar(" + length + ")"; break;
                    case "System.DateTime":
                        s = "datetime"; break;
                    case "System.Boolean":
                        s = "bit"; break;
                    case "System.Byte":
                        s = "tinyint"; break;
                    case "System.Int16":
                    case "System.Int32":
                        s = "int"; break;
                    case "System.Int64":
                        s = "bigint"; break;
                    case "System.Float":
                    case "System.Decimal":
                    case "System.Double":
                        s = "decimal";
                        break;
                    default:
                        s = "nvarchar(" + length + ")";
                        break;
                }

                string AddColumnSql =
                        "IF NOT EXISTS (SELECT TABLE_NAME,COLUMN_NAME " +
                        "FROM INFORMATION_SCHEMA.COLUMNS " +
                        "WHERE COLUMN_NAME = @columnName " +
                        "AND TABLE_NAME=@tableName " +
                        "   AND (OBJECTPROPERTY(OBJECT_ID(TABLE_NAME), 'istable') = 1) ) " +
                        "BEGIN " +
                        "   ALTER TABLE [" + tableName + "] ADD [" + columnName + "] " + s + " " +
                        "END";
                List<SqlParameter> lp = new List<SqlParameter>();
                lp.Add(new SqlParameter("@columnName", columnName));
                lp.Add(new SqlParameter("@tableName", tableName));


                SqlHelper.ExecuteNonQuery(_Con, CommandType.Text, AddColumnSql, lp.ToArray());
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".AddColumn", ex, null, Application.ProductName);
            }
        }



        private DataTable tb1, tb2, tb3, tb4;
        
        private string V6Options_M_MA_NT0 = "VND";
        private int V6Setting_RoundGia = 2;
        private int V6Setting_RoundTien = 0;
        public DataTable ALFCOPY2LIST;
        public DataTable ALFCOPY2DATA;

        private void EXEC2()//********************* Type = 2 ***************************
        {
            try
            {
                string strSQL0 = "SELECT CONVERT(VARCHAR(8),GETDATE(),108) AS curTime";
                tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strSQL0).Tables[0];
                string timehhmm = tb3.Rows[0][0].ToString().Trim();
                
                #region ==== SYNC LIST ====

                _Message = "Beginning ...List ";
                string _file_type_list = "";
                
                //ds_V6Sync_list.ReadXml(_fileName);
                if (ALFCOPY2LIST.Rows.Count > 0)
                {
                    tb1 = ALFCOPY2LIST.Copy();

                    DataView vtb = new DataView(tb1);
                    vtb.Sort = "STT ASC ";
                    vtb.RowFilter = "COPY_ALL='1'";


                    foreach (DataRow rowc in vtb.ToTable().Rows)
                    {
                        _Message = rowc["FILE_SQLF"].ToString();
                        _file_type_list = rowc["FILE_TYPE"].ToString().Trim();

                        // goto DoNoThing;
                        switch (_file_type_list)
                        {
                            case "D":
                            case "L":
                                RunSyncList_server2client(rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString(), rowc["FIELD_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), rowc["LOAI"].ToString(),
                                    Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id, rowc["FILE_SQLF"].ToString());


                                break;
                            case "R":
                            {
                                _Message = "Beginning ...List Rights";
                                RunSyncList_server2clientR(rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString(), rowc["FIELD_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), rowc["LOAI"].ToString(),
                                    Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id, rowc["FILE_SQLF"].ToString());
                            }

                                break;
                        }
                    }
                }


                _Message = "Ending ...List ";

                #endregion ==== SYNC LIST ====

                
                #region ==== SYNC DATA ====

                _Message = "Beginning ...Data ";
                
                
                if (ALFCOPY2DATA.Rows.Count > 0)
                {
                    tb1 = ALFCOPY2DATA.Copy();

                    DataView vtb = new DataView(tb1);
                    vtb.Sort = "STT ASC ";
                    vtb.RowFilter = "COPY_ALL='1'";
                    string _file_type = "";


                    foreach (DataRow rowc in vtb.ToTable().Rows)
                    {
                        _Message = rowc["FILE_SQLF"].ToString();

                        _file_type = rowc["FILE_TYPE"].ToString().Trim();
                        switch (_file_type)
                        {
                            case "K":
                            case "V":

                                RunSyncData2Data_V(rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString());


                                break;
                            case "B":
                                RunSyncData2Data_B(rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString());
                                break;
                            case "C":
                                RunSyncData2Data_C(rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }


                _Message = "Ending ...Data ";

                #endregion ==== SYNC DATA ====
                _Status = Status.Finish;
            }
            catch
            {
                _Status = Status.Exception;
            }
        }


        public void RunSyncList_server2clientR(string pprocedure, string pparaname, string pparavalue, string pparacheck, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string plistfield, string pkey_ma, string pFile_type, string pLoai, string pUnits, string pWs, string pAl)
        {
            string _pkey_ma, _pLoai, _plistws_id, _pcopy_all, _pkey_SQLF, _pkey_SQL, _pFile_type, _pUnits, _pWs, _pAl;
            string _pprocedure, _pparaname, _pparavalue, _pparacheck, _Key_where;

            _Key_where = "";

            if (pprocedure != "" && pprocedure != ".")
                _pprocedure = pprocedure;
            else
                _pprocedure = "";
            if (pparaname != "" && pparaname != ".")
                _pparaname = pparaname;
            else
                _pparaname = "";

            if (pparavalue != "" && pparavalue != ".")
                _pparavalue = pparavalue;
            else
                _pparavalue = "";

            if (pparacheck != "" && pparacheck != ".")
                _pparacheck = pparacheck;
            else
                _pparacheck = "";

            if (pLoai != "" && pLoai != ".")
                _pLoai = pLoai.Trim();
            else
                _pLoai = "";

            if (pkey_ma != "" && pkey_ma != ".")
                _pkey_ma = pkey_ma.Trim();
            else
                _pkey_ma = "";

            if (plistws_id != "" && plistws_id != ".")
                _plistws_id = plistws_id;
            else
                _plistws_id = "";

            if (pcopy_all != "" && pcopy_all != ".")
                _pcopy_all = pcopy_all;
            else
                _pcopy_all = "";

            if (pkey_SQLF != "" && pkey_SQLF != ".")
                _pkey_SQLF = pkey_SQLF;
            else
                _pkey_SQLF = "1=1";
            if (pkey_SQL != "" && pkey_SQL != ".")
                _pkey_SQL = pkey_SQL;
            else
                _pkey_SQL = "1=1";

            if (pFile_type != "" && pFile_type != ".")
                _pFile_type = pFile_type.Trim();
            else
                _pFile_type = "";
            if (pUnits != "" && pUnits != ".")
                _pUnits = pUnits;
            else
                _pUnits = "";

            if (pWs != "" && pWs != ".")
                _pWs = pWs;
            else
                _pWs = "";

            if (pAl != "" && pAl != ".")
                _pAl = pAl;
            else
                _pAl = "";



            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "")
                return;



            // Scan Al _CON- Server , _CON2 : Client
            string strsql = "", _Key_ma = "", _Ma_code = "";

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];


            //Corpuser
            _pkey_SQLF = " (dbo.vfa_inlist_NEW('V6M',CHECK_SYNC)=1 OR dbo.vfa_inlist_NEW('V6S',CHECK_SYNC)=1) AND (dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0) ";
            _pkey_SQLF = _pkey_SQLF + " AND (dbo.VFA_Inlist_MEMO('" + _pUnits + "',LTRIM(RTRIM(CAST(R_DVCS AS NVARCHAR(1000)))))=1)";


            strsql = " SELECT USER_ID  FROM V6USER WHERE  " + _pkey_SQLF;
            tb2 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];


            foreach (DataRow rowc in tb2.Rows)
            {

                if (_pkey_ma.Split(',').Length - 1 == 0)
                {
                    if (tb2.Columns.Contains(_pkey_ma))
                        _Key_ma = rowc[_pkey_ma].ToString();
                    else
                        _Key_ma = "";

                }
                else
                {
                    _Key_ma = _pkey_ma;
                }


                if (_Key_ma != "")
                {
                    _Ma_code = _Key_ma;


                    string[] a_fieldskey = _pkey_ma.Split(',');

                    if (a_fieldskey.Length > 0)
                    {
                        _Key_where = "";
                        foreach (string fieldkey in a_fieldskey)
                        {
                            _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";

                        }
                        _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);

                        // DELETE CLIENT
                        Delete_Data(_sqlConString2, _pAl, _Key_where);

                        // INSERT CLIENT
                        Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pAl, _Key_where, _pAl, "1=0");

                    }


                }



            }



        }

        public void RunSyncList_server2client(string pprocedure, string pparaname, string pparavalue, string pparacheck, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string plistfield, string pkey_ma, string pFile_type, string pLoai, string pUnits, string pWs, string pAl)
        {
            string _pkey_ma, _pLoai, _plistws_id, _pcopy_all, _pkey_SQLF, _pkey_SQL, _pFile_type, _pUnits, _pWs, _pAl;
            string _pprocedure, _pparaname, _pparavalue, _pparacheck, _Key_where;

            _Key_where = "";

            if (pprocedure != "" && pprocedure != ".")
                _pprocedure = pprocedure;
            else
                _pprocedure = "";
            if (pparaname != "" && pparaname != ".")
                _pparaname = pparaname;
            else
                _pparaname = "";

            if (pparavalue != "" && pparavalue != ".")
                _pparavalue = pparavalue;
            else
                _pparavalue = "";

            if (pparacheck != "" && pparacheck != ".")
                _pparacheck = pparacheck;
            else
                _pparacheck = "";

            if (pLoai != "" && pLoai != ".")
                _pLoai = pLoai.Trim();
            else
                _pLoai = "";

            if (pkey_ma != "" && pkey_ma != ".")
                _pkey_ma = pkey_ma.Trim();
            else
                _pkey_ma = "";

            if (plistws_id != "" && plistws_id != ".")
                _plistws_id = plistws_id;
            else
                _plistws_id = "";

            if (pcopy_all != "" && pcopy_all != ".")
                _pcopy_all = pcopy_all;
            else
                _pcopy_all = "";

            if (pkey_SQLF != "" && pkey_SQLF != ".")
                _pkey_SQLF = pkey_SQLF;
            else
                _pkey_SQLF = "1=1";
            if (pkey_SQL != "" && pkey_SQL != ".")
                _pkey_SQL = pkey_SQL;
            else
                _pkey_SQL = "1=1";

            if (pFile_type != "" && pFile_type != ".")
                _pFile_type = pFile_type.Trim();
            else
                _pFile_type = "";
            if (pUnits != "" && pUnits != ".")
                _pUnits = pUnits;
            else
                _pUnits = "";

            if (pWs != "" && pWs != ".")
                _pWs = pWs;
            else
                _pWs = "";

            if (pAl != "" && pAl != ".")
                _pAl = pAl;
            else
                _pAl = "";



            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "")
                return;



            // Scan Al _CON- Server , sqlConString2 : Client
            string strsql = "", _Key_ma = "", _Check_sync = "", _Ma_code = "", _Check = "0", _Gc_td1 = "";

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

            // WS_ID
            if (tb1.Columns.Contains("STT_REC"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
                _pkey_SQL = _pkey_SQL + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
            }

            // Ma_dvcs_i

            if (tb1.Columns.Contains("MA_DVCS_I"))
                if (_pUnits != "")
                {
                    _pkey_SQLF = _pkey_SQLF + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                    _pkey_SQL = _pkey_SQL + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                }


            if (tb1.Columns.Contains("CHECK_SYNC"))
            {

                if (_pFile_type == "D") // List not MEMO
                {
                    switch (_pLoai)
                    {
                        case "M":
                            _pkey_SQLF = _pkey_SQLF + " AND dbo.vfa_inlist_NEW('V6M',CHECK_SYNC)=1 AND dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0";
                            break;
                        case "S":
                            _pkey_SQLF = _pkey_SQLF + " AND dbo.vfa_inlist_NEW('V6S',CHECK_SYNC)=1 AND dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0";
                            break;
                        case "X":
                            _pkey_SQLF = _pkey_SQLF + " AND dbo.vfa_inlist_NEW('V6X',CHECK_SYNC)=1 AND dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0";
                            break;
                    }
                }
                else // List  MEMO=L
                {
                    switch (_pLoai)
                    {
                        case "M":
                            if (tb1.Columns.Contains("R_DVCS"))
                                _pkey_SQLF = _pkey_SQLF + " AND dbo.vfa_inlist_NEW('V6M',CHECK_SYNC)=1 AND dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0 and dbo.VFA_Inlist_MEMO('" + _pUnits + "',LTRIM(RTRIM(CAST(R_DVCS AS NVARCHAR(1000)))))=1";
                            else
                                _pkey_SQLF = _pkey_SQLF + " AND dbo.vfa_inlist_NEW('V6M',CHECK_SYNC)=1 AND dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0";
                            break;
                        case "S":
                            if (tb1.Columns.Contains("R_DVCS"))
                                _pkey_SQLF = _pkey_SQLF + " AND dbo.vfa_inlist_NEW('V6S',CHECK_SYNC)=1 AND dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0 and dbo.VFA_Inlist_MEMO('" + _pUnits + "',LTRIM(RTRIM(CAST(R_DVCS AS NVARCHAR(1000)))))=1";
                            else
                                _pkey_SQLF = _pkey_SQLF + " AND dbo.vfa_inlist_NEW('V6S',CHECK_SYNC)=1 AND dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0";

                            break;
                        case "X":
                            break;
                    }
                }








            }

            //if (_pAl == "ALGIA2")
            //{

            //    strsql = "AAA";

            //}


            strsql = " SELECT * FROM " + _pAl + " WHERE  " + _pkey_SQLF;



            //MessageBox.Show(strsql);

            tb2 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];


            foreach (DataRow rowc in tb2.Rows)
            {

                if (_pkey_ma.Split(',').Length - 1 == 0)
                {
                    if (tb1.Columns.Contains(_pkey_ma))
                        _Key_ma = rowc[_pkey_ma].ToString();
                    else
                        _Key_ma = "";

                }
                else
                {
                    _Key_ma = _pkey_ma;
                }



                if (tb1.Columns.Contains("CHECK_SYNC"))
                    _Check_sync = rowc["CHECK_SYNC"].ToString();




                // Thread.Sleep(1000);
                if (_Key_ma != "")
                {
                    _Ma_code = _Key_ma;

                    if (_pLoai == "X")
                    {

                        _Message = _pUnits + " Sync ..list..  " + _pAl;


                        // UpdaTE SERVER
                        strsql = "UPDATE " + _pAl + " SET ";
                        strsql = strsql + "CHECK_SYNC ='" + _Check_sync.ToString().Trim() + "," + _pUnits.Trim() + "'";

                        // strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                        strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);

                        SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql);

                        if (tb1.Columns.Contains("GC_TD1"))
                        {
                            _Gc_td1 = (rowc["GC_TD1"]).ToString();

                            if (_Gc_td1 != "")
                            {



                                strsql = _pprocedure.Trim();

                                List<SqlParameter> lstProcParam = new List<SqlParameter>();

                                string[] a_paraname = _pparaname.Split(',');
                                string[] a_paravalue = _pparavalue.Split(',');
                                string[] a_paracheck = _pparacheck.Split(',');

                                for (int i = 0; i < a_paraname.Length; i++)
                                {

                                    switch (a_paracheck[i].ToString().Trim())
                                    {
                                        case "1":
                                            lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue[i]));
                                            break;
                                        case "2":

                                            lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue[i]]));
                                            break;
                                        case "0":
                                            break;


                                    }


                                }

                                SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.StoredProcedure, strsql, lstProcParam.ToArray());
                                //UtilityHelper.getDataTableFromProcedure(sqlConString2, strsql, lstProcParam);

                                // Delete client
                                strsql = "DELETE FROM " + _pAl;
                                //strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                                strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);

                                SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql);
                            }
                        }
                    }


                    #region ==== New + Edit List====

                    else
                    {


                        _Message = _pUnits + " Sync ..list..  " + _pAl;



                        strsql = _pprocedure.Trim();

                        List<SqlParameter> lstProcParam = new List<SqlParameter>();

                        string[] a_paraname = _pparaname.Split(',');
                        string[] a_paravalue = _pparavalue.Split(',');
                        string[] a_paracheck = _pparacheck.Split(',');

                        for (int i = 0; i < a_paraname.Length; i++)
                        {

                            switch (a_paracheck[i].ToString().Trim())
                            {
                                case "1":
                                    lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue[i]));
                                    break;
                                case "2":

                                    lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue[i]]));
                                    break;
                                case "0":
                                    break;


                            }


                        }

                        //"EXEC VPA_isValidOneCode_FULL "  + " 'ALVT',1, 'MA_VT'," + "'" + ALLTRIM(M.MA_VT) + "',''"                                                                                                                                                                    

                        /*

                        strsql = "VPA_isValidOneCode_FULL";
                        List<MyParamProcedure> lstProcParam = new List<MyParamProcedure>();

                        lstProcParam.Add(new MyParamProcedure("@cInputTable", _pAl));
                        lstProcParam.Add(new MyParamProcedure("@nStatus", 1));
                        lstProcParam.Add(new MyParamProcedure("@cInputField", _pkey_ma));
                        lstProcParam.Add(new MyParamProcedure("@cpInput", _Ma_code));
                        lstProcParam.Add(new MyParamProcedure("@cOldItems", ""));

                        */

                        //MessageBox.Show(strsql);

                        //tb3 = UtilityHelper.getDataTableFromProcedure(sqlConString2, strsql, lstProcParam);
                        tb3 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.StoredProcedure, strsql, lstProcParam.ToArray()).Tables[0];

                        _Check = tb3.Rows[0][0].ToString();
                        if (_Check == "1")
                        {

                            //MessageBox.Show("M" + _Ma_code);

                            string[] a_fieldskey = _pkey_ma.Split(',');

                            if (a_fieldskey.Length > 0)
                            {
                                _Key_where = "";
                                foreach (string fieldkey in a_fieldskey)
                                {

                                    _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";


                                }
                                _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);
                                // INSERT CLIENT
                                Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pAl, _Key_where, _pAl, "1=0");

                                // UpdaTE SERVER
                                strsql = "UPDATE " + _pAl + " SET ";
                                strsql = strsql + "CHECK_SYNC ='" + _Check_sync.ToString().Trim() + "," + _pUnits.Trim() + "'";
                                strsql = strsql + "  WHERE " + _Key_where;

                                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql);

                            }

                        }
                        else
                        {
                            if (_pLoai == "S")
                            {
                                // EDIT CLIENT
                                //MessageBox.Show("S"+_Ma_code);

                                if (_pFile_type == "D")
                                {
                                    strsql = "UPDATE " + _pAl + " SET ";
                                    string[] a_fields = plistfield.Split(',');

                                    foreach (string field in a_fields)
                                    {

                                        strsql += field + "=" + MakeSqlValueString(rowc[field]) + ",";


                                    }

                                    strsql = strsql.Substring(0, strsql.Length - 1);


                                    string[] a_fieldskey = _pkey_ma.Split(',');

                                    if (a_fieldskey.Length > 0)
                                    {
                                        _Key_where = "";
                                        foreach (string fieldkey in a_fieldskey)
                                        {

                                            _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";


                                        }
                                        _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);


                                        strsql = strsql + "  WHERE " + _Key_where;

                                        SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql);


                                        // UpdaTE SERVER
                                        strsql = "UPDATE " + _pAl + " SET ";
                                        strsql = strsql + "CHECK_SYNC ='" + _Check_sync.ToString().Trim() + "," + _pUnits.Trim() + "'";
                                        strsql = strsql + "  WHERE " + _Key_where;

                                        SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql);

                                    }


                                }
                                else
                                {
                                    string[] a_fieldskey = _pkey_ma.Split(',');

                                    if (a_fieldskey.Length > 0)
                                    {
                                        _Key_where = "";
                                        foreach (string fieldkey in a_fieldskey)
                                        {

                                            _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";


                                        }
                                        _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);

                                        // DELETE CLIENT
                                        Delete_Data(_sqlConString2, _pAl, _Key_where);

                                        // INSERT CLIENT
                                        Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pAl, _Key_where, _pAl, "1=0");

                                        // UpdaTE SERVER
                                        strsql = "UPDATE " + _pAl + " SET ";
                                        strsql = strsql + "CHECK_SYNC ='" + _Check_sync.ToString().Trim() + "," + _pUnits.Trim() + "'";
                                        strsql = strsql + "  WHERE " + _Key_where;

                                        SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql);

                                    }


                                }




                            }

                        }



                    }

                }

                    #endregion ==== New + Edit ====


            }



        }


        public void RunSyncData2Data_V(string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string pFile_type, string pUnits, string pWs, string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30)
        {

            string _Key_server = "1=1", _pfield_key, _plistws_id, _pcopy_all, _pkey_SQLF, _pkey_SQL, _pFile_type, _pUnits, _pWs, _pAm, _pAd, _pAra00, _pAri70, _pArs20, _pArs30, _pArv20, _pArv30;

            if (pfield_key != "" && pfield_key != ".")
                _pfield_key = pfield_key;
            else
                _pfield_key = "";


            if (plistws_id != "" && plistws_id != ".")
                _plistws_id = plistws_id;
            else
                _plistws_id = "";

            if (pcopy_all != "" && pcopy_all != ".")
                _pcopy_all = pcopy_all;
            else
                _pcopy_all = "";

            if (pkey_SQLF != "" && pkey_SQLF != ".")
                _pkey_SQLF = pkey_SQLF;
            else
                _pkey_SQLF = "1=1";
            if (pkey_SQL != "" && pkey_SQL != ".")
                _pkey_SQL = pkey_SQL;
            else
                _pkey_SQL = "1=1";

            if (pFile_type != "" && pFile_type != ".")
                _pFile_type = pFile_type;
            else
                _pFile_type = "";
            if (pUnits != "" && pUnits != ".")
                _pUnits = pUnits;
            else
                _pUnits = "";

            if (pWs != "" && pWs != ".")
                _pWs = pWs;
            else
                _pWs = "";

            if (pAm != "" && pAm != ".")
                _pAm = pAm;
            else
                _pAm = "";
            if (pAd != "" && pAd != ".")
                _pAd = pAd;
            else
                _pAd = "";
            if (pAra00 != "" && pAra00 != ".")
                _pAra00 = pAra00;
            else
                _pAra00 = "";
            if (pAri70 != "" && pAri70 != ".")
                _pAri70 = pAri70;
            else
                _pAri70 = "";
            if (pArs20 != "" && pArs20 != ".")
                _pArs20 = pArs20;
            else
                _pArs20 = "";
            if (pArs30 != "" && pArs30 != ".")
                _pArs30 = pArs30;
            else
                _pArs30 = "";
            if (pArv20 != "" && pArv20 != ".")
                _pArv20 = pArv20;
            else
                _pArv20 = "";
            if (pArv30 != "" && pArv30 != ".")
                _pArv30 = pArv30;
            else
                _pArv30 = "";

            if (_pcopy_all == "" || _pFile_type == "" || _pWs == "" || _plistws_id == "" || _pAm == "")
                return;


            // Scan Am _CON- Server , _CON2 : Client
            string strsql = "", _Ngay_ct1, _Ngay_ct2;

            // Ngay_ct 
            strsql = "SELECT getdate()- " + Sync2ThConfig.date_num + " AS Ngay_ct1,getdate() as Ngay_ct2 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];
            _Ngay_ct1 = tb1.Rows[0]["Ngay_ct1"].ToString();
            _Ngay_ct2 = tb1.Rows[0]["Ngay_ct2"].ToString();

            _Ngay_ct1 = Convert.ToDateTime(_Ngay_ct1).ToString("yyyyMMdd");
            _Ngay_ct2 = Convert.ToDateTime(_Ngay_ct2).ToString("yyyyMMdd");


            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];
            if (tb1.Columns.Contains("NGAY_CT"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND Ngay_ct BETWEEN '" + _Ngay_ct1 + "' AND '" + _Ngay_ct2 + "'";
                _pkey_SQL = _pkey_SQL + " AND Ngay_ct BETWEEN '" + _Ngay_ct1 + "' AND '" + _Ngay_ct2 + "'";
            }

            // WS_ID
            if (tb1.Columns.Contains("STT_REC"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
                _pkey_SQL = _pkey_SQL + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
            }

            // Ma_dvcs_i

            if (tb1.Columns.Contains("MA_DVCS_I"))
                if (_pUnits != "")
                {
                    _pkey_SQLF = _pkey_SQLF + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                    _pkey_SQL = _pkey_SQL + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                }


            //Status
            if (tb1.Columns.Contains("STATUS"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND STATUS IN ('1','2') ";
                _pkey_SQL = _pkey_SQL + " AND STATUS IN ('5','6') ";
            }




            //  System.Windows.Forms.MessageBox.Show(_pkey_SQLF);
            // return;
            string _stt_rec = "", _status = "", _so_ct = "", _ngay_ct = "";

            #region //1. Post Client to Server (New 1->3, Edit 2->4)


            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF + "  ORDER BY NGAY_CT";

            //MessageBox.Show(strsql);

            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql).Tables[0];






            // _Message = tb2.Rows.Count.ToString();


            foreach (DataRow rowc in tb2.Rows)
            {
                if (tb1.Columns.Contains("STT_REC"))
                    _stt_rec = rowc["stt_rec"].ToString();

                if (tb1.Columns.Contains("STATUS"))
                    _status = rowc["status"].ToString();

                if (tb1.Columns.Contains("SO_CT"))
                    _so_ct = rowc["SO_CT"].ToString();

                if (tb1.Columns.Contains("NGAY_CT"))
                    _ngay_ct = Convert.ToDateTime(rowc["NGAY_CT"].ToString()).ToString("dd/MM/yyyy");

                _Message = _pUnits + " Client->Server :  " + _pAm + " : " + _ngay_ct + " -->" + _so_ct.Trim();


                if (_stt_rec != "")
                {

                    //  MessageBox.Show(_stt_rec);
                    // - Delete server

                    Delete_Data(_CONSTRING_MAIN, _pAra00, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pAri70, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pArs20, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pArv20, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pArs30, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pArv30, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pAd, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pAm, "STT_REC='" + _stt_rec + "'");


                    // - Change value

                    strsql = "SELECT * FROM " + _pAm + " WHERE STT_REC='" + _stt_rec + "'";
                    tb3 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql).Tables[0];

                    SortedList<string, object> lists = new SortedList<string, object>();
                    if (_status == "1")
                    {
                        lists.Add("STATUS", '3');

                    }
                    else
                    {
                        lists.Add("STATUS", '4');

                    }
                    Change_Datatable(tb3, lists);

                    // INSERT

                    //----------------AM------------------------------
                    strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
                    tb4 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];
                    Insert_datatable(_CONSTRING_MAIN, tb3, tb4, _pAm);

                    //----------------AD------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAd, "STT_REC='" + _stt_rec + "'", _pAd, "1=0");

                    //----------------Ara00------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAra00, "STT_REC='" + _stt_rec + "'", _pAra00, "1=0");

                    //----------------Ari70------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAri70, "STT_REC='" + _stt_rec + "'", _pAri70, "1=0");

                    //----------------Ars20------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pArs20, "STT_REC='" + _stt_rec + "'", _pArs20, "1=0");

                    //----------------Ars30------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pArs30, "STT_REC='" + _stt_rec + "'", _pArs30, "1=0");

                    //----------------Arv20------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pArv20, "STT_REC='" + _stt_rec + "'", _pArv20, "1=0");

                    //----------------Arv30------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pArv30, "STT_REC='" + _stt_rec + "'", _pArv30, "1=0");

                    //-----------------UPDATE CLIENT -AM-----------------------------

                    strsql = "UPDATE " + _pAm + " SET STATUS='" +
                        lists.Values[lists.IndexOfKey("STATUS")].ToString().Trim() + "' WHERE STT_REC='" + _stt_rec + "'";
                    SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql);

                }

                else // Table no column "stt_rec"
                {

                }

            }
            #endregion //1. Post Client to Server  2->4


            #region //1D.Delete CLient ->Delete Server (New 1->9, Edit 2-> 8)
            {


                strsql = "SELECT dbo.VFV_iFsize('D_" + _pAm.Trim() + "','STT_REC','C') AS Check_field";
                tb3 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql).Tables[0];

                if (Convert.ToInt16(tb3.Rows[0]["Check_field"]) > 0)
                {

                    strsql = "SELECT STT_REC FROM " + "D_" + _pAm.Trim() + " WHERE STATUS IN ('1','2','3','4','5','6','A','B') ";
                    tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql).Tables[0];
                    // Delete server
                    foreach (DataRow rowc in tb2.Rows)
                    {

                        if (tb2.Columns.Contains("STT_REC"))
                            _stt_rec = rowc["stt_rec"].ToString();

                        if (tb2.Columns.Contains("STATUS"))
                            _status = rowc["status"].ToString();

                        if (tb2.Columns.Contains("SO_CT"))
                            _so_ct = rowc["SO_CT"].ToString();

                        if (tb2.Columns.Contains("NGAY_CT"))
                            _ngay_ct = Convert.ToDateTime(rowc["NGAY_CT"].ToString()).ToString("dd/MM/yyyy");

                        _Message = _pUnits + " Delete Client->Delete Server :  " + _pAm + " : " + _ngay_ct + " -->" + _so_ct.Trim();


                        if (_stt_rec != "")
                        {

                            Delete_Data(_CONSTRING_MAIN, _pAra00, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_CONSTRING_MAIN, _pAri70, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_CONSTRING_MAIN, _pArs20, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_CONSTRING_MAIN, _pArv20, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_CONSTRING_MAIN, _pArs30, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_CONSTRING_MAIN, _pArv30, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_CONSTRING_MAIN, _pAd, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_CONSTRING_MAIN, _pAm, "STT_REC='" + _stt_rec + "'");

                            //-----------------UPDATE CLIENT -AM-----------------------------
                            strsql = "UPDATE " + "D_" + _pAm.Trim() + " SET STATUS='8' WHERE STT_REC='" + _stt_rec + "'";
                            SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql);

                        }

                    }
                }

            }



            #endregion //1D.Delete CLient ->Delete Server (New 1-> 9, Edit 2-> 8)


            #region //2. Post  Server to CLient (New 5->A, Edit 6->B)

            if (tb1.Columns.Contains("STT_REC"))
                _Key_server = _Key_server + " AND  (LEFT(STT_REC,1) IN (" + _plistws_id.Trim() + "))";


            _pkey_SQL = _pkey_SQL + " AND " + _Key_server;

            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL + "  ORDER BY NGAY_CT";

            //  MessageBox.Show(strsql);

            tb2 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

            //  MessageBox.Show(tb2.Rows.Count.ToString());


            foreach (DataRow rowc in tb2.Rows)
            {
                if (tb1.Columns.Contains("STT_REC"))
                    _stt_rec = rowc["stt_rec"].ToString();

                if (tb1.Columns.Contains("STATUS"))
                    _status = rowc["status"].ToString();

                if (tb1.Columns.Contains("SO_CT"))
                    _so_ct = rowc["SO_CT"].ToString();

                if (tb1.Columns.Contains("NGAY_CT"))
                    _ngay_ct = Convert.ToDateTime(rowc["NGAY_CT"].ToString()).ToString("dd/MM/yyyy");

                _Message = _pUnits + " Server->Client : " + _pAm + " : " + _ngay_ct + " -->" + _so_ct.Trim();


                if (_stt_rec != "")
                {

                    // MessageBox.Show(_stt_rec);

                    // - Delete client

                    Delete_Data(_sqlConString2, _pAra00, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_sqlConString2, _pAri70, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_sqlConString2, _pArs20, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_sqlConString2, _pArv20, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_sqlConString2, _pArs30, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_sqlConString2, _pArv30, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_sqlConString2, _pAd, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_sqlConString2, _pAm, "STT_REC='" + _stt_rec + "'");


                    // - Change value

                    strsql = "SELECT * FROM " + _pAm + " WHERE STT_REC='" + _stt_rec + "'";
                    tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

                    SortedList<string, object> lists = new SortedList<string, object>();
                    if (_status == "5")
                    {
                        lists.Add("STATUS", 'A');

                    }
                    else
                    {
                        lists.Add("STATUS", 'B');

                    }
                    Change_Datatable(tb3, lists);

                    // INSERT

                    //----------------AM------------------------------
                    strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
                    tb4 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql).Tables[0];
                    Insert_datatable(_sqlConString2, tb3, tb4, _pAm);

                    //----------------AD------------------------------
                    Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pAd, "STT_REC='" + _stt_rec + "'", _pAd, "1=0");

                    //----------------Ara00------------------------------
                    Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pAra00, "STT_REC='" + _stt_rec + "'", _pAra00, "1=0");

                    //----------------Ari70------------------------------
                    Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pAri70, "STT_REC='" + _stt_rec + "'", _pAri70, "1=0");

                    //----------------Ars20------------------------------
                    Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pArs20, "STT_REC='" + _stt_rec + "'", _pArs20, "1=0");

                    //----------------Ars30------------------------------
                    Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pArs30, "STT_REC='" + _stt_rec + "'", _pArs30, "1=0");

                    //----------------Arv20------------------------------
                    Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pArv20, "STT_REC='" + _stt_rec + "'", _pArv20, "1=0");

                    //----------------Arv30------------------------------
                    Tranfer_Data2Data(_CONSTRING_MAIN, _sqlConString2, _pArv30, "STT_REC='" + _stt_rec + "'", _pArv30, "1=0");

                    //-----------------UPDATE CLIENT -AM-----------------------------

                    strsql = "UPDATE " + _pAm + " SET STATUS='" +
                        lists.Values[lists.IndexOfKey("STATUS")].ToString().Trim() +
                        "' WHERE STT_REC='" + _stt_rec + "'";
                    SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql);

                }

                else // Table no column "stt_rec"
                {

                }

            }

            #endregion //1. Post Client to Server  2->4

            #region //2D.Delete Server ->Delete CLient (->D)
            {

                strsql = "SELECT dbo.VFV_iFsize('D_" + _pAm.Trim() + "','STT_REC','C') AS Check_field";
                tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

                if (Convert.ToInt16(tb3.Rows[0]["Check_field"]) > 0)
                {

                    strsql = "SELECT STT_REC FROM " + "D_" + _pAm.Trim() + " WHERE STATUS IN ('1','2','3','4','5','6','A','B') AND " + _Key_server;
                    tb2 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

                    // Delete server
                    foreach (DataRow rowc in tb2.Rows)
                    {

                        if (tb2.Columns.Contains("STT_REC"))
                            _stt_rec = rowc["stt_rec"].ToString();

                        if (tb2.Columns.Contains("STATUS"))
                            _status = rowc["status"].ToString();

                        if (tb2.Columns.Contains("SO_CT"))
                            _so_ct = rowc["SO_CT"].ToString();

                        if (tb2.Columns.Contains("NGAY_CT"))
                            _ngay_ct = Convert.ToDateTime(rowc["NGAY_CT"].ToString()).ToString("dd/MM/yyyy");

                        _Message = _pUnits + " Delete Server ->Delete CLient :  " + _pAm + " : " + _ngay_ct + " -->" + _so_ct.Trim();


                        if (_stt_rec != "")
                        {

                            Delete_Data(_sqlConString2, _pAra00, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_sqlConString2, _pAri70, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_sqlConString2, _pArs20, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_sqlConString2, _pArv20, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_sqlConString2, _pArs30, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_sqlConString2, _pArv30, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_sqlConString2, _pAd, "STT_REC='" + _stt_rec + "'");
                            Delete_Data(_sqlConString2, _pAm, "STT_REC='" + _stt_rec + "'");

                            //-----------------UPDATE CLIENT -AM-----------------------------
                            strsql = "UPDATE " + "D_" + _pAm.Trim() + " SET STATUS='D' WHERE STT_REC='" + _stt_rec + "'";
                            SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql);

                        }

                    }
                }

            }



            #endregion //1D.Delete CLient ->Delete Server

        }
        public void RunSyncData2Data_B(string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string pFile_type, string pUnits, string pWs, string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30)
        {

            string _pfield_key, _plistws_id, _pcopy_all, _pkey_SQLF, _pkey_SQL, _pFile_type, _pUnits, _pWs, _pAm, _pAd, _pAra00, _pAri70, _pArs20, _pArs30, _pArv20, _pArv30;

            if (pfield_key != "" && pfield_key != ".")
                _pfield_key = pfield_key;
            else
                _pfield_key = "";

            if (plistws_id != "" && plistws_id != ".")
                _plistws_id = plistws_id;
            else
                _plistws_id = "";

            if (pcopy_all != "" && pcopy_all != ".")
                _pcopy_all = pcopy_all;
            else
                _pcopy_all = "";

            if (pkey_SQLF != "" && pkey_SQLF != ".")
                _pkey_SQLF = pkey_SQLF;
            else
                _pkey_SQLF = "1=1";
            if (pkey_SQL != "" && pkey_SQL != ".")
                _pkey_SQL = pkey_SQL;
            else
                _pkey_SQL = "1=1";

            if (pFile_type != "" && pFile_type != ".")
                _pFile_type = pFile_type;
            else
                _pFile_type = "";
            if (pUnits != "" && pUnits != ".")
                _pUnits = pUnits;
            else
                _pUnits = "";

            if (pWs != "" && pWs != ".")
                _pWs = pWs;
            else
                _pWs = "";

            if (pAm != "" && pAm != ".")
                _pAm = pAm;
            else
                _pAm = "";
            if (pAd != "" && pAd != ".")
                _pAd = pAd;
            else
                _pAd = "";
            if (pAra00 != "" && pAra00 != ".")
                _pAra00 = pAra00;
            else
                _pAra00 = "";
            if (pAri70 != "" && pAri70 != ".")
                _pAri70 = pAri70;
            else
                _pAri70 = "";
            if (pArs20 != "" && pArs20 != ".")
                _pArs20 = pArs20;
            else
                _pArs20 = "";
            if (pArs30 != "" && pArs30 != ".")
                _pArs30 = pArs30;
            else
                _pArs30 = "";
            if (pArv20 != "" && pArv20 != ".")
                _pArv20 = pArv20;
            else
                _pArv20 = "";
            if (pArv30 != "" && pArv30 != ".")
                _pArv30 = pArv30;
            else
                _pArv30 = "";

            if (_pcopy_all == "" || _pFile_type == "" || _pWs == "" || _plistws_id == "" || _pAm == "")
                return;


            // Scan Am _CON- Server , _CON2 : Client
            string strsql = "", _Ngay_ct1, _Ngay_ct2;

            // Ngay_ct 
            strsql = "SELECT getdate()- " + Sync2ThConfig.date_num + " AS Ngay_ct1,getdate() as Ngay_ct2 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];
            _Ngay_ct1 = tb1.Rows[0]["Ngay_ct1"].ToString();
            _Ngay_ct2 = tb1.Rows[0]["Ngay_ct2"].ToString();

            _Ngay_ct1 = Convert.ToDateTime(_Ngay_ct1).ToString("yyyyMMdd");
            _Ngay_ct2 = Convert.ToDateTime(_Ngay_ct2).ToString("yyyyMMdd");


            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];
            if (tb1.Columns.Contains("NGAY_CT"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND Ngay_ct BETWEEN '" + _Ngay_ct1 + "' AND '" + _Ngay_ct2 + "'";
                _pkey_SQL = _pkey_SQL + " AND Ngay_ct BETWEEN '" + _Ngay_ct1 + "' AND '" + _Ngay_ct2 + "'";
            }

            // WS_ID
            if (tb1.Columns.Contains("STT_REC"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
                _pkey_SQL = _pkey_SQL + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
            }

            // Ma_dvcs_i

            if (tb1.Columns.Contains("MA_DVCS_I"))
                if (_pUnits != "")
                {
                    _pkey_SQLF = _pkey_SQLF + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                    _pkey_SQL = _pkey_SQL + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                }




            #region //1. Post Client to Server


            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF;

            //MessageBox.Show(strsql);

            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql).Tables[0];


            //tb3
            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL;
            tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];


            // _Message = tb2.Rows.Count.ToString();


            foreach (DataRow rowc in tb2.Rows)
            {

                string _key_filter = "1=1";

                //_pfield_key="STT_REC,DATE0,DATE2,TIME0,TIME2"

                string[] a_fields = _pfield_key.Split(',');

                foreach (string field in a_fields)
                {

                    _key_filter += " AND " + field + "=" + MakeSqlValueString(rowc[field]);

                }


                // MessageBox.Show(_key_filter);




                _Message = _pUnits + " Client->Server :  " + _pAm;



                if (_key_filter != "1=1")
                {

                    DataView vtb1 = new DataView(tb3);
                    vtb1.RowFilter = _key_filter;

                    if (vtb1.Count == 0)
                    {
                        // INSERT
                        //----------------AM------------------------------
                        strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
                        tb4 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

                        Insert_datarow(_CONSTRING_MAIN, rowc, tb4, _pAm);

                    }



                }

                else // Table no column "stt_rec"
                {

                }

            }
            #endregion //1. Post Client to Server  2->4
            
        }


        public void RunSyncData2Data_C(string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string pFile_type, string pUnits, string pWs, string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30)
        {

            string _pfield_key, _plistws_id, _pcopy_all, _pkey_SQLF, _pkey_SQL, _pFile_type, _pUnits, _pWs, _pAm, _pAd, _pAra00, _pAri70, _pArs20, _pArs30, _pArv20, _pArv30;

            if (pfield_key != "" && pfield_key != ".")
                _pfield_key = pfield_key;
            else
                _pfield_key = "";

            if (plistws_id != "" && plistws_id != ".")
                _plistws_id = plistws_id;
            else
                _plistws_id = "";

            if (pcopy_all != "" && pcopy_all != ".")
                _pcopy_all = pcopy_all;
            else
                _pcopy_all = "";

            if (pkey_SQLF != "" && pkey_SQLF != ".")
                _pkey_SQLF = pkey_SQLF;
            else
                _pkey_SQLF = "1=1";
            if (pkey_SQL != "" && pkey_SQL != ".")
                _pkey_SQL = pkey_SQL;
            else
                _pkey_SQL = "1=1";

            if (pFile_type != "" && pFile_type != ".")
                _pFile_type = pFile_type;
            else
                _pFile_type = "";
            if (pUnits != "" && pUnits != ".")
                _pUnits = pUnits;
            else
                _pUnits = "";

            if (pWs != "" && pWs != ".")
                _pWs = pWs;
            else
                _pWs = "";

            if (pAm != "" && pAm != ".")
                _pAm = pAm;
            else
                _pAm = "";
            if (pAd != "" && pAd != ".")
                _pAd = pAd;
            else
                _pAd = "";
            if (pAra00 != "" && pAra00 != ".")
                _pAra00 = pAra00;
            else
                _pAra00 = "";
            if (pAri70 != "" && pAri70 != ".")
                _pAri70 = pAri70;
            else
                _pAri70 = "";
            if (pArs20 != "" && pArs20 != ".")
                _pArs20 = pArs20;
            else
                _pArs20 = "";
            if (pArs30 != "" && pArs30 != ".")
                _pArs30 = pArs30;
            else
                _pArs30 = "";
            if (pArv20 != "" && pArv20 != ".")
                _pArv20 = pArv20;
            else
                _pArv20 = "";
            if (pArv30 != "" && pArv30 != ".")
                _pArv30 = pArv30;
            else
                _pArv30 = "";

            if (_pcopy_all == "" || _pFile_type == "" || _pWs == "" || _plistws_id == "" || _pAm == "")
                return;


            // Scan Am _CON- Server , _CON2 : Client
            string strsql = "", _Ngay_ct1, _Ngay_ct2;

            // Ngay_ct 
            strsql = "SELECT getdate()- " + Sync2ThConfig.date_num + " AS Ngay_ct1,getdate() as Ngay_ct2 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];
            _Ngay_ct1 = tb1.Rows[0]["Ngay_ct1"].ToString();
            _Ngay_ct2 = tb1.Rows[0]["Ngay_ct2"].ToString();

            _Ngay_ct1 = Convert.ToDateTime(_Ngay_ct1).ToString("yyyyMMdd");
            _Ngay_ct2 = Convert.ToDateTime(_Ngay_ct2).ToString("yyyyMMdd");


            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];
            if (tb1.Columns.Contains("NGAY_CT"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND Ngay_ct BETWEEN '" + _Ngay_ct1 + "' AND '" + _Ngay_ct2 + "'";
                _pkey_SQL = _pkey_SQL + " AND Ngay_ct BETWEEN '" + _Ngay_ct1 + "' AND '" + _Ngay_ct2 + "'";
            }

            // WS_ID
            if (tb1.Columns.Contains("STT_REC"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
                _pkey_SQL = _pkey_SQL + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
            }

            // Ma_dvcs_i

            if (tb1.Columns.Contains("MA_DVCS_I"))
                if (_pUnits != "")
                {
                    _pkey_SQLF = _pkey_SQLF + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                    _pkey_SQL = _pkey_SQL + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                }


            //Status
            if (tb1.Columns.Contains("STATUS"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND STATUS <>'1' ";
                _pkey_SQL = _pkey_SQL + " AND STATUS <>'1' ";
            }


            #region //1. Post Client to Server


            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF;

            //MessageBox.Show(strsql);

            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql).Tables[0];


            //tb3
            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL;
            tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

            //Status
            SortedList<string, object> lists = new SortedList<string, object>();
            if (tb1.Columns.Contains("STATUS"))
            {
                lists.Add("STATUS", '1');
                Change_Datatable(tb2, lists);
            }





            foreach (DataRow rowc in tb2.Rows)
            {

                string _key_filter = "1=1";

                //_pfield_key="STT_REC,DATE0,DATE2,TIME0,TIME2"

                string[] a_fields = _pfield_key.Split(',');

                foreach (string field in a_fields)
                {

                    _key_filter += " AND " + field + "=" + MakeSqlValueString(rowc[field]);

                }


                // MessageBox.Show(_key_filter);




                _Message = _pUnits + " Client->Server :  " + _pAm;



                if (_key_filter != "1=1")
                {

                    DataView vtb1 = new DataView(tb3);
                    vtb1.RowFilter = _key_filter;

                    if (vtb1.Count == 0)
                    {
                        // INSERT
                        //----------------AM------------------------------
                        strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
                        tb4 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql).Tables[0];

                        Insert_datarow(_CONSTRING_MAIN, rowc, tb4, _pAm);

                        //Status
                        if (tb1.Columns.Contains("STATUS"))
                        {
                            //-----------------UPDATE CLIENT -AM-----------------------------

                            strsql = "UPDATE " + _pAm + " SET STATUS='" +
                                lists.Values[lists.IndexOfKey("STATUS")].ToString().Trim() + "' WHERE " + _key_filter;
                            SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql);
                        }


                    }



                }

                else // Table no column "stt_rec"
                {

                }

            }
            #endregion //1. Post Client to Server  2->4








        }

        public void Delete_Data(string pCON, string pTable, string pWhere)
        {
            if (pTable != "" && pWhere != "")
            {
                string strsql = "DELETE FROM " + pTable + " WHERE " + pWhere;
                SqlHelper.ExecuteNonQuery(pCON, CommandType.Text, strsql);
            }


        }
        public void Tranfer_Data2Data(string pCONfrom, string pCONto, string pTablefrom, string pWherefrom, string pTableto, string pWhereto)
        {
            if (pTablefrom != "" && pWherefrom != "" && pTableto != "" && pWhereto != "")
            {
                string strsql = "SELECT * FROM " + pTablefrom + " WHERE " + pWherefrom;
                tb3 = SqlHelper.ExecuteDataset(pCONfrom, CommandType.Text, strsql).Tables[0];

                strsql = "SELECT * FROM " + pTableto + " WHERE " + pWhereto;
                tb4 = SqlHelper.ExecuteDataset(pCONto, CommandType.Text, strsql).Tables[0];

                Insert_datatable(pCONto, tb3, tb4, pTableto);
            }

        }

        public void Change_Datatable(DataTable pDataTable, SortedList<string, object> lists)
        {
            // ex   lists.Add("STATUS", '4')       
            for (int i = 0; i < pDataTable.Rows.Count; i++)
            {
                for (int j = 0; j < lists.Count; j++)
                {
                    pDataTable.Rows[i][lists.Keys[j]] = lists.Values[j];

                }
            }
        }

        public void Insert_datatable(string _CON0, DataTable tbfrom, DataTable tbto, string TableName)
        {

            string insertsql = "";
            string fields = "", paramlist = "";
            for (int i = 0; i < tbto.Columns.Count; i++)
            {
                fields += "," + tbto.Columns[i].ColumnName;
                paramlist += ",@" + tbto.Columns[i].ColumnName;
            }
            if (fields.Length > 0)
            {
                fields = fields.Substring(1);//bỏ đi cái dấu [,]
            }
            if (paramlist.Length > 0)
            {
                paramlist = paramlist.Substring(1);
            }
            insertsql = "Insert into [" + TableName + "]\n(" + fields + ")\nValues(" + paramlist + ")";

            List<SqlParameter> insertParams;
            foreach (DataRow item in tbfrom.Rows)
            {
                insertParams = new List<SqlParameter>();
                for (int i = 0; i < tbto.Columns.Count; i++)
                {
                    string field = tbto.Columns[i].ColumnName;
                    if (tbfrom.Columns.Contains(field))
                        insertParams.Add(new SqlParameter(field, item[field]));
                    else
                    {
                        if (tbto.Columns[i].AllowDBNull)
                            insertParams.Add(new SqlParameter(field, null));
                        else
                        {
                            switch (tbto.Columns[i].DataType.ToString())
                            {
                                case "System.Char":
                                case "System.String":
                                    insertParams.Add(new SqlParameter(field, ""));
                                    break;
                                case "System.DateTime":
                                    insertParams.Add(new SqlParameter(field, DateTime.Now));
                                    break;
                                case "System.Boolean":
                                    insertParams.Add(new SqlParameter(field, false));
                                    break;
                                case "System.Byte":
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Decimal":
                                case "System.Double":
                                    insertParams.Add(new SqlParameter(field, 0));
                                    break;
                                case "System.DBNull":
                                    insertParams.Add(new SqlParameter(field, ""));
                                    break;
                                default:
                                    insertParams.Add(new SqlParameter(field, ""));
                                    break;
                            }
                        }
                    }
                }
                //Thực thi sql
                // errormessage = insertsql;


                SqlHelper.ExecuteNonQuery(_CON0, CommandType.Text, insertsql, insertParams.ToArray());

            }
        }

        public void Insert_datarow(string _CON0, DataRow item, DataTable tbto, string TableName)
        {

            string insertsql = "";
            string fields = "", paramlist = "";
            for (int i = 0; i < tbto.Columns.Count; i++)
            {
                fields += "," + tbto.Columns[i].ColumnName;
                paramlist += ",@" + tbto.Columns[i].ColumnName;
            }
            if (fields.Length > 0)
            {
                fields = fields.Substring(1);//bỏ đi cái dấu [,]
            }
            if (paramlist.Length > 0)
            {
                paramlist = paramlist.Substring(1);
            }
            insertsql = "Insert into [" + TableName + "]\n(" + fields + ")\nValues(" + paramlist + ")";

            List<SqlParameter> insertParams;

            insertParams = new List<SqlParameter>();
            for (int i = 0; i < tbto.Columns.Count; i++)
            {
                string field = tbto.Columns[i].ColumnName;

                if (item.Table.Columns.Contains(field))
                    insertParams.Add(new SqlParameter(field, item[field]));
                else
                {
                    if (tbto.Columns[i].AllowDBNull)
                        insertParams.Add(new SqlParameter(field, null));
                    else
                    {
                        switch (tbto.Columns[i].DataType.ToString())
                        {
                            case "System.Char":
                            case "System.String":
                                insertParams.Add(new SqlParameter(field, ""));
                                break;
                            case "System.DateTime":
                                insertParams.Add(new SqlParameter(field, DateTime.Now));
                                break;
                            case "System.Boolean":
                                insertParams.Add(new SqlParameter(field, false));
                                break;
                            case "System.Byte":
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Decimal":
                            case "System.Double":
                                insertParams.Add(new SqlParameter(field, 0));
                                break;
                            case "System.DBNull":
                                insertParams.Add(new SqlParameter(field, ""));
                                break;
                            default:
                                insertParams.Add(new SqlParameter(field, ""));
                                break;
                        }
                    }
                }
            }

            //Thực thi sql
            // errormessage = insertsql;


            SqlHelper.ExecuteNonQuery(_CON0, CommandType.Text, insertsql, insertParams.ToArray());

        }

        public string MakeSqlValueString(object field_value)
        {
            string value = "";
            switch (field_value.GetType().ToString())
            {
                case "System.Boolean":
                case "System.Char":
                case "System.String":
                    value = "'" + field_value.ToString() + "'";
                    break;
                case "System.DateTime":
                    value = "'" + Convert.ToDateTime(field_value).ToString("yyyy/MM/dd") + "'";
                    break;
                case "System.Byte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Decimal":
                case "System.Double":
                    value = field_value.ToString();
                    break;
                case "System.DBNull":
                    value = "null";
                    break;
                default:
                    value = "'" + field_value.ToString() + "'";
                    break;
            }
            return value;
            //Hết hàm
            //Sử dụng hàm            
            //strsql += field + "=" + MakeSqlValueString(row[field]) + ",";
        }
        

        #endregion ==== ==== ==== Viết code ở trên ==== ==== ====
          ///////////////////////////////////////////
         ///////////////////////////////////////////
        ///////////////////////////////////////////
    }
}
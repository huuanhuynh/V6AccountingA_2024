using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;
using V6AccountingBusiness;
using V6Tools;
using V6Tools.V6Convert;

namespace V6SyncLibrary2021
{
    public class MyThread : BaseThread
    {
        #region ==== New Var ====
        
        //string DatabaseName;
        
        private string _CONSTRING_MAIN;
        private string _sqlConString2;

        public int ConnectionTimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }
        private int timeOut = 120;

        //private string sqlTableName, _sqlSelectFields, sqlPrimarykeyField;

        #endregion ==== | ====

        //DataTable sqlStructTable;

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
                    int @lock = 0;
                    var pDate = ObjectAndString.StringToDate(Sync2ThConfig.NGAY_CT1, "yyyyMMdd");
                    @lock += V6BusinessHelper.CheckSyncDataLocked_C(_CONSTRING_MAIN, Sync2ThConfig.GetString("MA_DVCS"), "1", pDate, pDate.Month, pDate.Year);
                    pDate = ObjectAndString.StringToDate(Sync2ThConfig.NGAY_CT2, "yyyyMMdd");
                    @lock += V6BusinessHelper.CheckSyncDataLocked_C(_CONSTRING_MAIN, Sync2ThConfig.GetString("MA_DVCS"), "1", pDate, pDate.Month, pDate.Year);

                    if (@lock == 0)
                    {
                        if (_CONSTRING_MAIN.ToUpper() != _sqlConString2.ToUpper())
                        {
                            EXEC2();
                        }
                        else
                        {
                            _Message = "EXEC2 Locked because same DATABASE.";
                        }
                    }
                    else
                    {
                        _Message = "Số liệu đã khóa sổ.";
                    }

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
                SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strCommand, timeOut);
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
                if ((int)SqlHelper.ExecuteDataset(strCon, CommandType.Text, strCommand, timeOut).Tables[0].Rows[0][0] > 0)
                {
                    if (where != "") where += (" and " + sqlWhere);
                    strCommand = "select count(1) from [" + tableName + "]" + where;
                    //_MessageBox.Show(strCommand);
                    if ((int)SqlHelper.ExecuteDataset(strCon, CommandType.Text, strCommand, timeOut).Tables[0].Rows[0][0] > 0)
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

        
        private DataTable tb1, tb2, tb3, tb4;
        public DataTable ALFCOPY2LIST;
        public DataTable ALFCOPY2DATA;
        public IDictionary<string, string> TKGOP_DIC = new Dictionary<string, string>();

        private void EXEC2()//********************* Type = 2 ***************************
        {
            try
            {
                string strSQL0 = "SELECT CONVERT(VARCHAR(8),GETDATE(),108) AS curTime";
                tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strSQL0, timeOut).Tables[0];
                string timehhmm = tb3.Rows[0][0].ToString().Trim();

                //Sync2ThConfig

                #region ==== SYNC LIST ====

                _Message = "Beginning ...List ";
                string FILE_TYPE = "";
                
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
                        FILE_TYPE = rowc["FILE_TYPE"].ToString().Trim();

                        // goto DoNoThing;
                        switch (FILE_TYPE)
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
                                _Message = "Beginning ...List Rights";
                                RunSyncList_server2clientR(rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString(), rowc["FIELD_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), rowc["LOAI"].ToString(),
                                    Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id, rowc["FILE_SQLF"].ToString());
                                break;
                            case "F":
                                _Message = "Beginning ...List Rights";
                                RunSyncList_server2clientF(rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(),
                                    Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString(), rowc["FIELD_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), rowc["LOAI"].ToString(),
                                    Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString());
                                break;

                                // THÊM MỚI (TỒN TẠI KHÔNG INSERT)
                            case "1":
                            case "2":
                                RunSyncList_server2client_1(rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString(), rowc["FIELD_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), rowc["LOAI"].ToString(),
                                    Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id, rowc["FILE_SQLF"].ToString());


                                break;
                            case "3":
                                _Message = "Beginning ...List Rights";
                                RunSyncList_server2client_R3(rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString(), rowc["FIELD_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), rowc["LOAI"].ToString(),
                                    Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id, rowc["FILE_SQLF"].ToString());
                                break;
                            case "4":
                                _Message = "Beginning ...RunSyncList_server2client_F4";
                                RunSyncList_server2client_F4(rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(),
                                    Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString(), rowc["FIELD_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), rowc["LOAI"].ToString(),
                                    Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString());
                                break;

                            default:
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

                                RunSyncData2Data_V(rowc.ToDataDictionary(), rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString());
                                break;

                            case "P":
                                RunSyncData2Data_P(rowc.ToDataDictionary(), rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString());
                                break;

                            case "B":
                                RunSyncData2Data_B(rowc.ToDataDictionary(), rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString());
                                break;
                            case "C":
                                RunSyncData2Data_C(rowc.ToDataDictionary(), rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString());
                                break;
                            case "T":
                                RunSyncData2Data_T(rowc.ToDataDictionary(), rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString());
                                break;
                            case "F":
                                RunSyncData2Data_F(rowc.ToDataDictionary(), rowc["PPROCEDURE"].ToString(), rowc["PPARANAME"].ToString(),
                                    rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK"].ToString(),
                                    rowc["FIELD_KEY"].ToString(), Sync2ThConfig.strlistws_id,
                                    rowc["COPY_ALL"].ToString(), rowc["STRSQL_KEY"].ToString(),
                                    rowc["STRSQL_KEY"].ToString(),
                                    rowc["FILE_TYPE"].ToString(), Sync2ThConfig.strlistdvcs, Sync2ThConfig.strlistws_id,
                                    rowc["FILE_SQLF"].ToString(), rowc["FILE_SQLFC"].ToString(),
                                    rowc["FILE_ARA00"].ToString(), rowc["FILE_ARI70"].ToString(),
                                    rowc["FILE_ARS20"].ToString(), rowc["FILE_ARS30"].ToString(),
                                    rowc["FILE_ARV20"].ToString(), rowc["FILE_ARV30"].ToString(),
                                    rowc["SCRIPT_SQL"].ToString());
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
            catch(Exception ex)
            {
                _Status = Status.Exception;
                _Message = ex.Message;
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



            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "" || _pAl == "")
                return;

            var pal_struct = V6BusinessHelper.GetTableStruct(_pAl);

            // Scan Al _CON- Server , _CON2 : Client
            string strsql = "", _Key_ma = "", _Ma_code = "";

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];


            //Corpuser
            _pkey_SQLF = " (dbo.vfa_inlist_NEW('V6M',CHECK_SYNC)=1 OR dbo.vfa_inlist_NEW('V6S',CHECK_SYNC)=1) AND (dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0) ";
            _pkey_SQLF = _pkey_SQLF + " AND (dbo.VFA_Inlist_MEMO('" + _pUnits + "',LTRIM(RTRIM(CAST(R_DVCS AS NVARCHAR(1000)))))=1)";


            strsql = " SELECT USER_ID  FROM V6USER WHERE  " + _pkey_SQLF;
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];


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
                        Delete_Data(_CONSTRING_MAIN, _pAl, _Key_where);

                        // INSERT CLIENT
                        Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");

                    }
                }
            }
        }

        public void RunSyncList_server2client_R3(string pprocedure, string pparaname, string pparavalue, string pparacheck, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string plistfield, string pkey_ma, string pFile_type, string pLoai, string pUnits, string pWs, string pAl)
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



            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "" || _pAl == "")
                return;

            var pal_struct = V6BusinessHelper.GetTableStruct(_pAl);

            // Scan Al _CON- Server , _CON2 : Client
            string strsql = "", _Key_ma = "", _Ma_code = "";

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];


            //Corpuser
            _pkey_SQLF = " (dbo.vfa_inlist_NEW('V6M',CHECK_SYNC)=1 OR dbo.vfa_inlist_NEW('V6S',CHECK_SYNC)=1) AND (dbo.vfa_inlist_NEW('" + _pUnits + "',CHECK_SYNC)=0) ";
            _pkey_SQLF = _pkey_SQLF + " AND (dbo.VFA_Inlist_MEMO('" + _pUnits + "',LTRIM(RTRIM(CAST(R_DVCS AS NVARCHAR(1000)))))=1)";


            strsql = " SELECT USER_ID  FROM V6USER WHERE  " + _pkey_SQLF;
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];


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

                        // Check CLIENT
                        bool check_exist = Check_Exist_Data(_CONSTRING_MAIN, _pAl, _Key_where);

                        // INSERT CLIENT
                        if (!check_exist)
                            Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");

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

            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "" || _pAl == "")
                return;

            var pal_struct = V6BusinessHelper.GetTableStruct(_pAl);


            // Scan Al _CON- Server , sqlConString2 : Client
            string strsql = "", _Key_ma = "", _Check_sync = "", _Ma_code = "", _Check = "0", _Gc_td1 = "";

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            // WS_ID
            if (tb1.Columns.Contains("STT_REC"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
                _pkey_SQL = _pkey_SQL + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
            }

            // Ma_dvcs_i

            if (tb1.Columns.Contains("MA_DVCS_I") && _pUnits != "")
            {
                _pkey_SQLF = _pkey_SQLF + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                _pkey_SQL = _pkey_SQL + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
            }

            if (pal_struct.ContainsKey("CHECK_SYNC"))
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
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];


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
                if (_Key_ma == "") continue;
                
                _Ma_code = _Key_ma;

                if (_pLoai == "X")
                {

                    _Message = _pUnits + " Sync ..list..  " + _pAl;

                    // UpdaTE SERVER
                    if (pal_struct.ContainsKey("CHECK_SYNC"))
                    {
                        strsql = "UPDATE " + _pAl + " SET ";
                        strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                        // strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                        strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);
                        SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                    }
                        

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
                                string a_paravalue_i = a_paravalue[i].Trim();
                                switch (a_paracheck[i].Trim())
                                {
                                    case "1":
                                        lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue[i]));
                                        break;
                                    case "2":
                                        lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue[i]]));
                                        break;
                                    case "3":
                                        var ss = a_paravalue_i.ToUpper().Split('.');
                                        if (ss.Length >= 2)
                                        {
                                            if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                                        }
                                        break;
                                    case "0":
                                        break;
                                }
                            }

                            SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.StoredProcedure, strsql, timeOut, lstProcParam.ToArray());
                            
                            // Delete client
                            strsql = "DELETE FROM " + _pAl;
                            //strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                            strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);

                            SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut);
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
                        string a_paravalue_i = a_paravalue[i].Trim();
                        switch (a_paracheck[i].Trim())
                        {
                            case "1":
                                lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue[i]));
                                break;
                            case "2":
                                lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue[i]]));
                                break;
                            case "3":
                                var ss = a_paravalue_i.ToUpper().Split('.');
                                if (ss.Length >= 2)
                                {
                                    if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                                }
                                break;
                            case "0":
                                break;

                        }
                    }

                    string test = "debug";
                    tb3 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.StoredProcedure, strsql, timeOut, lstProcParam.ToArray()).Tables[0];

                    _Check = tb3.Rows[0][0].ToString();
                    if (_Check == "1")
                    {

                        string[] a_fieldskey = ObjectAndString.SplitString(_pkey_ma);

                        if (a_fieldskey.Length > 0)
                        {
                            _Key_where = "";
                            foreach (string fieldkey in a_fieldskey)
                            {
                                _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";
                            }
                            _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);
                            // INSERT CLIENT
                            Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");

                            // UpdaTE SERVER
                            if (pal_struct.ContainsKey("CHECK_SYNC"))
                            {
                                strsql = "UPDATE " + _pAl + " SET ";
                                strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                                strsql = strsql + "  WHERE " + _Key_where;
                                SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                            }
                        }
                    }
                    else if (_pLoai == "S")
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

                                SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);


                                // UpdaTE SERVER
                                if (pal_struct.ContainsKey("CHECK_SYNC"))
                                {
                                    strsql = "UPDATE " + _pAl + " SET ";
                                    strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                                    strsql = strsql + "  WHERE " + _Key_where;
                                    SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                                }
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
                                Delete_Data(_CONSTRING_MAIN, _pAl, _Key_where);

                                // INSERT CLIENT
                                Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");

                                // UpdaTE SERVER
                                if (pal_struct.ContainsKey("CHECK_SYNC"))
                                {
                                    strsql = "UPDATE " + _pAl + " SET ";
                                    strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                                    strsql = strsql + "  WHERE " + _Key_where;
                                    SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                                }
                            }
                        }
                    }
                }
                #endregion ==== New + Edit ====

            } // end for
        }

        

        public void RunSyncList_server2client_1(string pprocedure, string pparaname, string pparavalue, string pparacheck, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string plistfield, string pkey_ma, string pFile_type, string pLoai, string pUnits, string pWs, string pAl)
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

            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "" || _pAl == "")
                return;

            var pal_struct = V6BusinessHelper.GetTableStruct(_pAl);


            // Scan Al _CON- Server , sqlConString2 : Client
            string strsql = "", _Key_ma = "", _Check_sync = "", _Ma_code = "", _Check = "0", _Gc_td1 = "";

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            // WS_ID
            if (tb1.Columns.Contains("STT_REC"))
            {
                _pkey_SQLF = _pkey_SQLF + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
                _pkey_SQL = _pkey_SQL + " AND LEFT(STT_REC,1) IN (" + _plistws_id + ")";
            }

            // Ma_dvcs_i

            if (tb1.Columns.Contains("MA_DVCS_I") && _pUnits != "")
            {
                _pkey_SQLF = _pkey_SQLF + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
                _pkey_SQL = _pkey_SQL + " AND UPPER(RTRIM(LTRIM(Ma_dvcs_i)))=" + _pUnits.Trim().ToUpper();
            }

            if (pal_struct.ContainsKey("CHECK_SYNC"))
            {

                if (_pFile_type == "1") // List not MEMO
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

            //MessageBox.Show(strsql, timeOut);

            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];


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
                if (_Key_ma == "") continue;

                _Ma_code = _Key_ma;

                if (_pLoai == "X")
                {

                    _Message = _pUnits + " Sync ..list..  " + _pAl;

                    // UpdaTE SERVER
                    if (pal_struct.ContainsKey("CHECK_SYNC"))
                    {
                        strsql = "UPDATE " + _pAl + " SET ";
                        strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                        // strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                        strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);
                        SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                    }


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
                                string a_paravalue_i = a_paravalue[i].Trim();
                                switch (a_paracheck[i].Trim())
                                {
                                    case "1":
                                        lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue[i]));
                                        break;
                                    case "2":
                                        lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue[i]]));
                                        break;
                                    case "3":
                                        var ss = a_paravalue_i.ToUpper().Split('.');
                                        if (ss.Length >= 2)
                                        {
                                            if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                                        }
                                        break;
                                    case "0":
                                        break;

                                }
                            }

                            SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.StoredProcedure, strsql, timeOut, lstProcParam.ToArray());

                            // Delete client
                            strsql = "DELETE FROM " + _pAl;
                            //strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                            strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);

                            SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut);
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
                        string a_paravalue_i = a_paravalue[i].Trim();
                        switch (a_paracheck[i].Trim())
                        {
                            case "1":
                                lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue[i]));
                                break;
                            case "2":
                                lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue[i]]));
                                break;
                            case "3":
                                var ss = a_paravalue_i.ToUpper().Split('.');
                                if (ss.Length >= 2)
                                {
                                    if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                                }
                                break;
                            case "0":
                                break;

                        }
                    }

                    //tb3 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.StoredProcedure, strsql, timeOut, lstProcParam.ToArray()).Tables[0];
                    //_Check = tb3.Rows[0][0].ToString();
                    //if (_Check == "1")
                    //{
                    //    //MessageBox.Show("M" + _Ma_code);
                    //    string[] a_fieldskey = _pkey_ma.Split(',');
                    //    if (a_fieldskey.Length > 0)
                    //    {
                    //        _Key_where = "";
                    //        foreach (string fieldkey in a_fieldskey)
                    //        {
                    //            _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";
                    //        }
                    //        _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);
                    //        // INSERT
                    //        Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");
                    //        // UpdaTE SERVER
                    //        if (pal_struct.ContainsKey("CHECK_SYNC"))
                    //        {
                    //            strsql = "UPDATE " + _pAl + " SET ";
                    //            strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                    //            strsql = strsql + "  WHERE " + _Key_where;
                    //            SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                    //        }
                    //    }
                    //}
                    //if (_pLoai == "S")
                    // EDIT CLIENT
                    //MessageBox.Show("S"+_Ma_code);

                    string[] a_fieldskey = ObjectAndString.SplitString(_pkey_ma);

                    if (a_fieldskey.Length > 0)
                    {
                        _Key_where = "";
                        foreach (string fieldkey in a_fieldskey)
                        {
                            _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";
                        }

                        _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);

                        // Check exist
                        bool check_exist = Check_Exist_Data(_CONSTRING_MAIN, _pAl, _Key_where);

                        // INSERT
                        if (check_exist && _pLoai == "S")
                        {   
                            Delete_Data(_CONSTRING_MAIN, _pAl, _Key_where);
                            Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");
                        }
                        else if (!check_exist)
                        {   
                            Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");
                        }
                        else
                        {

                        }
                                    

                        // UpdaTE SERVER
                        if (pal_struct.ContainsKey("CHECK_SYNC"))
                        {
                            strsql = "UPDATE " + _pAl + " SET ";
                            strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                            strsql = strsql + "  WHERE " + _Key_where;
                            SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                        }
                    }
                }
                #endregion ==== New + Edit ====

            } // end for
        }

        public void RunSyncList_server2clientF(string pprocedure, string pparaname, string pparavalue, string pparacheck, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string plistfield, string pkey_ma, string pFile_type, string pLoai, string pUnits, string pWs, string pAl)
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



            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "" || _pAl == "")
                return;

            var pal_struct = V6BusinessHelper.GetTableStruct(_pAl);

            // Scan Al _CON- Server , sqlConString2 : Client
            string strsql = "", _Key_ma = "", _Check_sync = "", _Ma_code = "", _Check = "0", _Gc_td1 = "";

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

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

                if (_pFile_type == "F") // List not MEMO
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
            

            strsql = " SELECT * FROM " + _pAl + " WHERE  " + _pkey_SQLF;
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];

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
                        if (pal_struct.ContainsKey("CHECK_SYNC"))
                        {
                            strsql = "UPDATE " + _pAl + " SET ";
                            strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                            // strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                            strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);
                            SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                        }

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
                                    string a_paravalue_i = a_paravalue[i].Trim();
                                    switch (a_paracheck[i].Trim())
                                    {
                                        case "1":
                                            lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue_i));
                                            break;
                                        case "2":
                                            lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue_i]));
                                            break;
                                        case "3":
                                            var ss = a_paravalue_i.ToUpper().Split('.');
                                            if (ss.Length >= 2)
                                            {
                                                if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                                            }
                                            break;
                                        case "0":
                                            break;
                                    }
                                }

                                SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.StoredProcedure, strsql, timeOut, lstProcParam.ToArray());
                                
                                // Delete client
                                strsql = "DELETE FROM " + _pAl;
                                //strsql = strsql + "  WHERE " + _pkey_ma + " ='" + _Ma_code + "'";
                                strsql = strsql + "  WHERE " + _pkey_ma + " =" + MakeSqlValueString(rowc[_pkey_ma]);

                                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut);
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
                            string a_paravalue_i = a_paravalue[i].Trim();
                            switch (a_paracheck[i].Trim())
                            {
                                case "1":
                                    lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue_i));
                                    break;
                                case "2":

                                    lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue_i]));
                                    break;
                                case "3":
                                    var ss = a_paravalue_i.ToUpper().Split('.');
                                    if (ss.Length >= 2)
                                    {
                                        if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                                    }
                                    break;
                                case "0":
                                    break;

                            }
                        }

                        tb3 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.StoredProcedure, strsql, timeOut, lstProcParam.ToArray()).Tables[0];

                        _Check = tb3.Rows[0][0].ToString();
                        if (_Check == "1")
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
                                // INSERT CLIENT
                                Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");

                                if (pal_struct.ContainsKey("CHECK_SYNC"))
                                {
                                    // UpdaTE SERVER
                                    strsql = "UPDATE " + _pAl + " SET ";
                                    strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                                    strsql = strsql + "  WHERE " + _Key_where;
                                    SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                                }
                            }
                        }
                        else if (_pLoai == "S")
                        {
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
                                    SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut);

                                    // UpdaTE SERVER
                                    if (pal_struct.ContainsKey("CHECK_SYNC"))
                                    {
                                        strsql = "UPDATE " + _pAl + " SET ";
                                        strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                                        strsql = strsql + "  WHERE " + _Key_where;
                                        SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                                    }
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
                                    Delete_Data(_CONSTRING_MAIN, _pAl, _Key_where);

                                    // INSERT CLIENT
                                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAl, _Key_where, _pAl, "1=0");

                                    // UpdaTE SERVER
                                    if (pal_struct.ContainsKey("CHECK_SYNC"))
                                    {
                                        strsql = "UPDATE " + _pAl + " SET ";
                                        strsql = strsql + "CHECK_SYNC ='" + _Check_sync.Trim() + "," + _pUnits.Trim() + "'";
                                        strsql = strsql + "  WHERE " + _Key_where;
                                        SqlHelper.ExecuteNonQuery(_sqlConString2, CommandType.Text, strsql, timeOut);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion ==== New + Edit ====
            }
        }

        public void RunSyncList_server2client_F4(string pprocedure, string pparaname, string pparavalue, string pparacheck, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string plistfield, string pkey_ma, string pFile_type, string pLoai, string pUnits, string pWs, string pAl)
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



            if (_pLoai == "" || _pkey_ma == "" || _pFile_type == "" || _pAl == "")
                return;

            // Scan Am _CON- Server , _CON2 : Client
            string strsql = "", _Ngay_ct1, _Ngay_ct2;

            // Ngay_ct 
            _Ngay_ct1 = Sync2ThConfig.NGAY_CT1;
            _Ngay_ct2 = Sync2ThConfig.NGAY_CT2;

            strsql = "SELECT * FROM " + _pAl + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            _pkey_SQLF = plistfield;// script_sql; 
            _pkey_SQL = plistfield;// script_sql;
            List<SqlParameter> plist = new List<SqlParameter>
            {
                new SqlParameter("@ngay_ct1", _Ngay_ct1),
                new SqlParameter("@ngay_ct2", _Ngay_ct2),
                new SqlParameter("@ma_dvcs", Sync2ThConfig.GetString("MA_DVCS"))
            };

            #region //1. Post Client to Server

            strsql = "SELECT * FROM " + _pAl + " WHERE " + _pkey_SQLF;
            //tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];
            List<SqlParameter> lstProcParam = new List<SqlParameter>();

            string[] a_paraname = pparaname.Split(',');
            string[] a_paravalue = pparavalue.Split(',');
            string[] a_paracheck = pparacheck.Split(',');

            for (int i = 0; i < a_paraname.Length; i++)
            {
                string a_paravalue_i = a_paravalue[i].Trim();
                switch (a_paracheck[i].Trim())
                {
                    case "1":
                        lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue_i));
                        break;
                    //case "2":
                    //    lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue_i]));
                    //    break;
                    case "3":
                        var ss = a_paravalue_i.ToUpper().Split('.');
                        if (ss.Length >= 2)
                        {
                            if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                        }
                        break;
                    case "0":
                        break;
                }
            }

            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.StoredProcedure, pprocedure, timeOut, lstProcParam.ToArray()).Tables[0];

            //tb3
            //strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL;
            //tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];

            string[] a_fieldskey = ObjectAndString.SplitString(_pkey_ma);
            if (a_fieldskey.Length > 0)
            {
                foreach (DataRow rowc in tb2.Rows)
                {
                    _Message = _pUnits + " Client->Server :  " + _pAl;

                    // INSERT
                    //----------------AM------------------------------
                    _Key_where = "";
                    foreach (string fieldkey in a_fieldskey)
                    {
                        _Key_where += fieldkey + "=" + MakeSqlValueString(rowc[fieldkey]) + " AND ";
                    }

                    _Key_where = _Key_where.Substring(0, _Key_where.Length - 5);

                    // Check CLIENT
                    bool check_exist = Check_Exist_Data(_CONSTRING_MAIN, _pAl, _Key_where);

                    // INSERT CLIENT
                    if (!check_exist)
                    {
                        var insert_data = rowc.ToDataDictionary();
                        V6BusinessHelper.InsertC(_CONSTRING_MAIN, _pAl, insert_data);
                    }

                }
            }
            #endregion //1. Post Client to Server  2->4
        }


        public void RunSyncData2Data_V(IDictionary<string, object> ALFCOPY2DATA_rowData, string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string pFile_type, string pUnits, string pWs,
            string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30,
            string script_sql)
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
                _pAm = pAm.Trim();
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

            _Ngay_ct1 = Sync2ThConfig.NGAY_CT1;
            _Ngay_ct2 = Sync2ThConfig.NGAY_CT2;
            
            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            _pkey_SQLF = script_sql;
            _pkey_SQL = script_sql;
            List<SqlParameter> plist = new List<SqlParameter>
            {
                new SqlParameter("@ngay_ct1", _Ngay_ct1),
                new SqlParameter("@ngay_ct2", _Ngay_ct2),
                new SqlParameter("@ma_dvcs", Sync2ThConfig.GetString("MA_DVCS"))
            };
            
            
            string _stt_rec = "", _status = "", _so_ct = "", _ngay_ct = "";

            // Delete
            if (!string.IsNullOrEmpty(script_sql) && Sync2ThConfig.GetString("DELE_YN") == "1")
            {
                strsql = "Delete " + pAd + " Where STT_REC in (Select stt_rec from " + pAm + " Where " + script_sql + ")";
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());
                
                strsql = "Delete " + pAm + " Where " + script_sql;
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());
            }

            #region //1. Post 2 to main (New 1->3, Edit 2->4)


            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF + "  ORDER BY NGAY_CT";
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];
            
            foreach (DataRow rowc in tb2.Rows)
            {
                if (tb1.Columns.Contains("STT_REC"))
                    _stt_rec = rowc["stt_rec"].ToString();

                if (tb1.Columns.Contains("STATUS"))
                    _status = rowc["status"].ToString();

                if (tb1.Columns.Contains("SO_CT"))
                    _so_ct = rowc["SO_CT"].ToString();

                if (tb1.Columns.Contains("NGAY_CT"))
                    _ngay_ct = ObjectAndString.ObjectToString(rowc["NGAY_CT"]);

                _Message = _pUnits + " Client->Server :  " + _pAm + " : " + _ngay_ct + " -->" + _so_ct.Trim();


                if (_stt_rec != "")
                {

                    //  MessageBox.Show(_stt_rec);
                    // - Delete server

                    //Delete_Data(_CONSTRING_MAIN, _pAra00, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pAri70, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArs20, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArv20, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArs30, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArv30, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pAd, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pAm, "STT_REC='" + _stt_rec + "'");


                    // - Change value

                    strsql = "SELECT * FROM " + _pAm + " WHERE STT_REC='" + _stt_rec + "'";
                    tb3 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];
                                      
                    // INSERT
                    var copy_type = ALFCOPY2DATA_rowData["COPY_TYPE"].ToString().Trim();

                    //----------------AM------------------------------
                    strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
                    tb4 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];
                    Insert_datatable(_CONSTRING_MAIN, tb3, tb4, _pAm, null);

                    //----------------AD------------------------------
                    Tranfer_Data2Data(_sqlConString2, _CONSTRING_MAIN, _pAd, "STT_REC='" + _stt_rec + "'", _pAd, "1=0");
                    

                }
                else // Table no column "stt_rec"
                {

                }

            }
            #endregion //1. Post Client to Server  2->4
            

        }

        public void RunSyncData2Data_P(IDictionary<string, object> ALFCOPY2DATA_rowData, string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL, string pFile_type, string pUnits, string pWs,
            string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30,
            string script_sql)
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
                _pAm = pAm.Trim();
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

            _Ngay_ct1 = Sync2ThConfig.NGAY_CT1;
            _Ngay_ct2 = Sync2ThConfig.NGAY_CT2;

            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            _pkey_SQLF = script_sql;
            _pkey_SQL = script_sql;
            List<SqlParameter> plist = new List<SqlParameter>
            {
                new SqlParameter("@ngay_ct1", _Ngay_ct1),
                new SqlParameter("@ngay_ct2", _Ngay_ct2),
                new SqlParameter("@ma_dvcs", Sync2ThConfig.GetString("MA_DVCS"))
            };
            //"PPARANAME"].ToString(), rowc["PPARAVALUE"].ToString(), rowc["PPARACHECK

            string PPARANAME = ALFCOPY2DATA_rowData["PPARANAME"].ToString().Trim();
            string PPARAVALUE = ALFCOPY2DATA_rowData["PPARAVALUE"].ToString().Trim();
            string PPARACHECK = ALFCOPY2DATA_rowData["PPARACHECK"].ToString().Trim();
            string[] a_paraname = ObjectAndString.SplitString(PPARANAME);
            string[] a_paravalue = ObjectAndString.SplitString(PPARAVALUE);
            string[] a_paracheck = ObjectAndString.SplitString(PPARACHECK);
            var lstProcParam = new List<SqlParameter>();

            for (int i = 0; i < a_paraname.Length; i++)
            {
                string a_paravalue_i = a_paravalue[i].Trim();
                switch (a_paracheck[i].Trim())
                {
                    case "1":
                        lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue_i));
                        break;
                    //case "2":
                    //    lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue_i]));
                    //    break;
                    case "3":
                        var ss = a_paravalue_i.ToUpper().Split('.');
                        if (ss.Length >= 2)
                        {
                            if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                        }
                        break;
                    case "0":
                        break;
                }
            }

            string _stt_rec = "", _status = "", _so_ct = "", _ngay_ct = "";

            //Delete
            if (!string.IsNullOrEmpty(script_sql) && Sync2ThConfig.GetString("DELE_YN") == "1")
            {
                strsql = "Delete " + pAd + " Where STT_REC in (Select stt_rec from " + pAm + " Where " + script_sql + ")";
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());

                strsql = "Delete " + pAm + " Where " + script_sql;
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());
            }

            #region //1. Post 2 to main (New 1->3, Edit 2->4)


            //strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF + "  ORDER BY NGAY_CT";
            string PPROCEDURE = ALFCOPY2DATA_rowData["PPROCEDURE"].ToString().Trim();
            var ds = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.StoredProcedure, PPROCEDURE, timeOut, lstProcParam.ToArray());
            tb2 = ds.Tables[0];
            tb3 = ds.Tables[1];
            DataView AD_view = new DataView(tb3.Copy());

            foreach (DataRow rowc in tb2.Rows)
            {
                if (tb1.Columns.Contains("STT_REC"))
                    _stt_rec = rowc["stt_rec"].ToString();

                if (tb1.Columns.Contains("STATUS"))
                    _status = rowc["status"].ToString();

                if (tb1.Columns.Contains("SO_CT"))
                    _so_ct = rowc["SO_CT"].ToString();

                if (tb1.Columns.Contains("NGAY_CT"))
                    _ngay_ct = ObjectAndString.ObjectToString(rowc["NGAY_CT"]);

                _Message = _pUnits + " Client->Server :  " + _pAm + " : " + _ngay_ct + " -->" + _so_ct.Trim();


                if (_stt_rec != "")
                {

                    //  MessageBox.Show(_stt_rec);
                    // - Delete server

                    //Delete_Data(_CONSTRING_MAIN, _pAra00, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pAri70, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArs20, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArv20, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArs30, "STT_REC='" + _stt_rec + "'");
                    //Delete_Data(_CONSTRING_MAIN, _pArv30, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pAd, "STT_REC='" + _stt_rec + "'");
                    Delete_Data(_CONSTRING_MAIN, _pAm, "STT_REC='" + _stt_rec + "'");

                    // Insert AM
                    V6BusinessHelper.InsertC(_CONSTRING_MAIN, _pAm, rowc.ToDataDictionary());

                    // Insert AD
                    AD_view.RowFilter = "STT_REC= '" + _stt_rec + "'";
                    var ad_data = AD_view.ToTable();
                    foreach (DataRow item in ad_data.Rows)
                    {
                        V6BusinessHelper.InsertC(_CONSTRING_MAIN, _pAd, item.ToDataDictionary());
                    }
                }
                else // Table no column "stt_rec"
                {

                }

            }
            #endregion //1. Post Client to Server  2->4
        }


        public void RunSyncData2Data_B(IDictionary<string, object> ALFCOPY2DATA_rowData, string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL,
            string pFile_type, string pUnits, string pWs, string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30,
            string script_sql)
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

            _Ngay_ct1 = Sync2ThConfig.NGAY_CT1;
            _Ngay_ct2 = Sync2ThConfig.NGAY_CT2;


            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            _pkey_SQLF = script_sql;
            _pkey_SQL = script_sql;
            List<SqlParameter> plist = new List<SqlParameter>
            {
                new SqlParameter("@ngay_ct1", _Ngay_ct1),
                new SqlParameter("@ngay_ct2", _Ngay_ct2),
                new SqlParameter("@ma_dvcs", Sync2ThConfig.GetString("MA_DVCS"))
            };
            
            // Delete
            if (!string.IsNullOrEmpty(script_sql) && Sync2ThConfig.GetString("DELE_YN") == "1")
            {
                
                strsql = "Delete " + pAm + " Where " + script_sql;
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());
            }

            #region //1. Post Client to Server


            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF;
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];


            //tb3
            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL;
            tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];
            var copy_type = ALFCOPY2DATA_rowData["COPY_TYPE"].ToString().Trim();
            

            foreach (DataRow rowc in tb2.Rows)
            {
                string _key_filter = "1=1";

                //_pfield_key="STT_REC,DATE0,DATE2,TIME0,TIME2"

                string[] a_fields = ObjectAndString.SplitString(_pfield_key);

                foreach (string field in a_fields)
                {
                    _key_filter += " AND " + field + "=" + MakeSqlValueString(rowc[field]);
                }
                
                _Message = _pUnits + " Client->Server :  " + _pAm;
                // INSERT
                //----------------AM------------------------------
                var insert_data = rowc.ToDataDictionary();
                if (copy_type == "G")
                {
                    foreach (string field in Sync2ThConfig.fields)
                    {
                        var FIELD = field.ToUpper().Trim();
                        if (insert_data.ContainsKey(FIELD))
                        {
                            var OLD_VALUE = insert_data[FIELD].ToString().Trim().ToUpper();
                            if (TKGOP_DIC.ContainsKey(OLD_VALUE))
                            {
                                insert_data[FIELD] = TKGOP_DIC[OLD_VALUE];
                            }
                        }
                    }
                }
                V6BusinessHelper.InsertC(_CONSTRING_MAIN, _pAm, insert_data);
                    
                
            }
            #endregion //1. Post Client to Server  2->4
            
        }

        public void RunSyncData2Data_F(IDictionary<string, object> ALFCOPY2DATA_rowData, string pprocedure,
            string pparaname, string pparavalue, string pparacheck,
            string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL,
            string pFile_type,
            string pUnits, string pWs, string pAm, string pAd, string pAra00, string pAri70, string pArs20,
            string pArs30, string pArv20, string pArv30,
            string script_sql)
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
            _Ngay_ct1 = Sync2ThConfig.NGAY_CT1;
            _Ngay_ct2 = Sync2ThConfig.NGAY_CT2;

            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            _pkey_SQLF = script_sql;
            _pkey_SQL = script_sql;
            List<SqlParameter> plist = new List<SqlParameter>
            {
                new SqlParameter("@ngay_ct1", _Ngay_ct1),
                new SqlParameter("@ngay_ct2", _Ngay_ct2),
                new SqlParameter("@ma_dvcs", Sync2ThConfig.GetString("MA_DVCS"))
            };

            #region //1. Post Client to Server

            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF;
            //tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut).Tables[0];
            List<SqlParameter> lstProcParam = new List<SqlParameter>();

            string[] a_paraname = pparaname.Split(',');
            string[] a_paravalue = pparavalue.Split(',');
            string[] a_paracheck = pparacheck.Split(',');

            for (int i = 0; i < a_paraname.Length; i++)
            {
                string a_paravalue_i = a_paravalue[i].Trim();
                switch (a_paracheck[i].Trim())
                {
                    case "1":
                        lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), a_paravalue_i));
                        break;
                    //case "2":
                    //    lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), rowc[a_paravalue_i]));
                    //    break;
                    case "3":
                        var ss = a_paravalue_i.ToUpper().Split('.');
                        if (ss.Length >= 2)
                        {
                            if (ss[0] == "CONFIG") lstProcParam.Add(new SqlParameter(a_paraname[i].Trim(), Sync2ThConfig.DATA[ss[1]]));
                        }
                        break;
                    case "0":
                        break;
                }
            }

            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.StoredProcedure, pprocedure, timeOut, lstProcParam.ToArray()).Tables[0];

            //tb3
            //strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL;
            //tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];
            // Delete
            if (!string.IsNullOrEmpty(script_sql) && Sync2ThConfig.GetString("DELE_YN") == "1")
            {
                strsql = "Delete " + pAm + " Where " + _pkey_SQL;
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());
            }

            var copy_type = ALFCOPY2DATA_rowData["COPY_TYPE"].ToString().Trim();
            
            foreach (DataRow rowc in tb2.Rows)
            {
                _Message = _pUnits + " Client->Server :  " + _pAm;
                
                // INSERT
                //----------------AM------------------------------
                var insert_data = rowc.ToDataDictionary();
                if (copy_type == "G")
                {
                    foreach (string field in Sync2ThConfig.fields)
                    {
                        var FIELD = field.ToUpper().Trim();
                        if (insert_data.ContainsKey(FIELD))
                        {
                            var OLD_VALUE = insert_data[FIELD].ToString().Trim().ToUpper();
                            if (TKGOP_DIC.ContainsKey(OLD_VALUE))
                            {
                                insert_data[FIELD] = TKGOP_DIC[OLD_VALUE];
                            }
                        }
                    }
                }
                V6BusinessHelper.InsertC(_CONSTRING_MAIN, _pAm, insert_data);
            }
            #endregion //1. Post Client to Server  2->4
            
        }


        public void RunSyncData2Data_C(IDictionary<string, object> ALFCOPY2DATA_rowData, string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL,
            string pFile_type, string pUnits, string pWs, string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30,
            string script_sql)
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
            _Ngay_ct1 = Sync2ThConfig.NGAY_CT1;
            _Ngay_ct2 = Sync2ThConfig.NGAY_CT2;

            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            _pkey_SQLF = "SO_THE_CC in (Select so_the_cc from ALCC Where " + script_sql + ")";
            _pkey_SQL = "SO_THE_CC in (Select so_the_cc from ALCC Where " + script_sql + ")";
            List<SqlParameter> plist = new List<SqlParameter>
            {
                new SqlParameter("@ngay_ct1", _Ngay_ct1),
                new SqlParameter("@ngay_ct2", _Ngay_ct2),
                new SqlParameter("@ma_dvcs", Sync2ThConfig.GetString("MA_DVCS"))
            };
            // Delete
            if (!string.IsNullOrEmpty(script_sql) && Sync2ThConfig.GetString("DELE_YN") == "1")
            {
                strsql = "Delete " + pAm + " Where " + _pkey_SQLF;// SO_THE_CC in (Select so_the_cc from ALCC Where " + script_sql + ")";
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());
            }

            #region //1. Post Client to Server


            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF;
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];
            
            //tb3
            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL;
            tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];
            var copy_type = ALFCOPY2DATA_rowData["COPY_TYPE"].ToString().Trim();
            

            foreach (DataRow rowc in tb2.Rows)
            { 
                _Message = _pUnits + " Client->Server :  " + _pAm;
                
                //DataView vtb1 = new DataView(tb3);
                
                // INSERT
                //----------------AM------------------------------
                var insert_data = rowc.ToDataDictionary();
                if (copy_type == "G")
                {
                    foreach (string field in Sync2ThConfig.fields)
                    {
                        var FIELD = field.ToUpper().Trim();
                        if (insert_data.ContainsKey(FIELD))
                        {
                            var OLD_VALUE = insert_data[FIELD].ToString().Trim().ToUpper();
                            if (TKGOP_DIC.ContainsKey(OLD_VALUE))
                            {
                                insert_data[FIELD] = TKGOP_DIC[OLD_VALUE];
                            }
                        }
                    }
                }
                V6BusinessHelper.InsertC(_CONSTRING_MAIN, _pAm, insert_data);
                
                
            }
            #endregion //1. Post Client to Server  2->4
        }

        public void RunSyncData2Data_T(IDictionary<string, object> ALFCOPY2DATA_rowData, string pfield_key, string plistws_id, string pcopy_all, string pkey_SQLF, string pkey_SQL,
            string pFile_type, string pUnits, string pWs, string pAm, string pAd, string pAra00, string pAri70, string pArs20, string pArs30, string pArv20, string pArv30,
            string script_sql)
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
            _Ngay_ct1 = Sync2ThConfig.NGAY_CT1;
            _Ngay_ct2 = Sync2ThConfig.NGAY_CT2;

            strsql = "SELECT * FROM " + _pAm + " WHERE 1=0 ";
            tb1 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut).Tables[0];

            _pkey_SQLF = "SO_THE_TS in (Select so_the_ts from ALTS Where " + script_sql + ")";
            _pkey_SQL = "SO_THE_TS in (Select so_the_ts from ALTS Where " + script_sql + ")";
            List<SqlParameter> plist = new List<SqlParameter>
            {
                new SqlParameter("@ngay_ct1", _Ngay_ct1),
                new SqlParameter("@ngay_ct2", _Ngay_ct2),
                new SqlParameter("@ma_dvcs", Sync2ThConfig.GetString("MA_DVCS"))
            };

            // Delete
            if (!string.IsNullOrEmpty(script_sql) && Sync2ThConfig.GetString("DELE_YN") == "1")
            {
                strsql = "Delete " + pAm + " Where " + _pkey_SQLF;// SO_THE_TS in (Select so_the_ts from ALTS Where " + script_sql + ")";
                SqlHelper.ExecuteNonQuery(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray());
            }

            #region //1. Post Client to Server


            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQLF;
            tb2 = SqlHelper.ExecuteDataset(_sqlConString2, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];

            //tb3
            strsql = "SELECT * FROM " + _pAm + " WHERE " + _pkey_SQL;
            tb3 = SqlHelper.ExecuteDataset(_CONSTRING_MAIN, CommandType.Text, strsql, timeOut, plist.ToArray()).Tables[0];
            var copy_type = ALFCOPY2DATA_rowData["COPY_TYPE"].ToString().Trim();


            foreach (DataRow rowc in tb2.Rows)
            {
                _Message = _pUnits + " Client->Server :  " + _pAm;
                //DataView vtb1 = new DataView(tb3);
                    
                // INSERT
                //----------------AM------------------------------
                var insert_data = rowc.ToDataDictionary();
                if (copy_type == "G")
                {
                    foreach (string field in Sync2ThConfig.fields)
                    {
                        var FIELD = field.ToUpper().Trim();
                        if (insert_data.ContainsKey(FIELD))
                        {
                            var OLD_VALUE = insert_data[FIELD].ToString().Trim().ToUpper();
                            if (TKGOP_DIC.ContainsKey(OLD_VALUE))
                            {
                                insert_data[FIELD] = TKGOP_DIC[OLD_VALUE];
                            }
                        }
                    }
                }
                V6BusinessHelper.InsertC(_CONSTRING_MAIN, _pAm, insert_data);
            }
            #endregion //1. Post Client to Server  2->4
        }


        public void Delete_Data(string pCON, string pTable, string pWhere)
        {
            if (pTable != "" && pWhere != "")
            {
                string strsql = "DELETE FROM " + pTable + " WHERE " + pWhere;
                SqlHelper.ExecuteNonQuery(pCON, CommandType.Text, strsql, timeOut);
            }
        }

        public bool Check_Exist_Data(string pCON, string pTable, string pWhere)
        {
            int result = 0;
            if (pTable != "" && pWhere != "")
            {
                string strsql = "Select Count(1) FROM " + pTable + " WHERE " + pWhere;
                result = (int)SqlHelper.ExecuteScalar(pCON, CommandType.Text, strsql, timeOut);
            }
            return result != 0;
        }

        public void Tranfer_Data2Data(string pCONfrom, string pCONto, string pTablefrom, string pWherefrom, string pTableto, string pWhereto, IDictionary<string, string> GOP_DATA = null)
        {
            if (pTablefrom != "" && pWherefrom != "" && pTableto != "" && pWhereto != "")
            {
                string strsql = "SELECT * FROM " + pTablefrom + " WHERE " + pWherefrom;
                tb3 = SqlHelper.ExecuteDataset(pCONfrom, CommandType.Text, strsql, timeOut).Tables[0];

                strsql = "SELECT * FROM " + pTableto + " WHERE " + pWhereto;
                tb4 = SqlHelper.ExecuteDataset(pCONto, CommandType.Text, strsql, timeOut).Tables[0];

                Insert_datatable(pCONto, tb3, tb4, pTableto, GOP_DATA);
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

        public void Insert_datatable(string _CON0, DataTable tbfrom, DataTable tbto, string TableName, IDictionary<string, string> GOP_DATA)
        {
            foreach (DataRow item in tbfrom.Rows)
            {
                try
                {
                    var insert_data = item.ToDataDictionary();
                    if(GOP_DATA != null)
                    {
                        foreach (string field in Sync2ThConfig.fields)
                        {
                            var FIELD = field.ToUpper().Trim();
                            if (insert_data.ContainsKey(FIELD))
                            {
                                var OLD_VALUE = insert_data[FIELD].ToString().Trim().ToUpper();
                                if (GOP_DATA.ContainsKey(OLD_VALUE))
                                {
                                    insert_data[FIELD] = GOP_DATA[OLD_VALUE];
                                }
                            }
                        }
                    }
                    V6BusinessHelper.InsertC(_CON0, TableName, insert_data);
                }
                catch (Exception ex)
                {
                    _Message += ex.Message;
                }
            }
        }

        public string MakeSqlValueString(object field_value)
        {
            string value = "";
            switch (field_value.GetType().ToString())
            {
                case "System.Boolean":
                case "System.Char":
                case "System.String":
                    value = "'" + field_value + "'";
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
                    value = "'" + field_value + "'";
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
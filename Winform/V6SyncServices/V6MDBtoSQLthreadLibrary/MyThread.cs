using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Data.OleDb;
using System.Diagnostics;
using System.Reflection;
using V6Tools;

namespace V6ThreadLibrary
{
    //public struct ThreadVar
    //{
    //    public string var1, var2, var3;
    //    public int int1, int2, int3;
    //}
    public class MyThread:BaseThread
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
        //public Logger _log;
        //public string __dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //public const string __logName = "V6ThreadLog";
        //protected void Log(string message)
        //{
        //    _log.WriteLog(__dir, __logName, message);
        //}

        string DatabaseName;
        private string mdbxmlfilename, sqlxmlfilename, sqlxmlfilename2, sqlxmlfilename3, vpn_supfolder;
        private string options = "";
        private string mdbConString, sqlConString, sqlConString2, sqlConString3;
        private string mdbTableName, mdbSelectFields, mdbPrimarykeyField, mdbUpdateFields;
        private string sqlTableName, _sqlSelectFields, sqlPrimarykeyField;

        private string mdbWhere, sqlWhere, mdbCode, sqlCode;
        string server, user, pass, Ecommand;

        private string type = "0";
        #endregion ==== | ====

        DataTable sqlStructTable;

        #region ==== Khởi tạo đối tượng ====
        
        //public MyThread(string name, int index):base(name,index)
        //{
        //}

        public MyThread(V6SyncSetting setting,
            int Index, string Name, DataRow Config ):base(Name,Index)
        {
            _setting = setting;
            _log = new Logger(__dir, __logName);

            DatabaseName = Config["Database"].ToString();
            this.mdbxmlfilename = Config["mdbxml"].ToString();
            this.sqlxmlfilename = Config["sqlxml"].ToString();
            this.sqlxmlfilename2 = Config["sqlxml2"].ToString();
            this.sqlxmlfilename3 = Config["sqlxml3"].ToString();
            this.vpn_supfolder = Config["vpn_subfolder"].ToString();

            if (!Path.IsPathRooted(mdbxmlfilename)) mdbxmlfilename = __dir + "\\" + mdbxmlfilename;
            if (!Path.IsPathRooted(sqlxmlfilename)) sqlxmlfilename = __dir + "\\" + sqlxmlfilename;
            if (!Path.IsPathRooted(sqlxmlfilename2)) sqlxmlfilename2 = __dir + "\\" + sqlxmlfilename2;
            if (!Path.IsPathRooted(sqlxmlfilename3)) sqlxmlfilename3 = __dir + "\\" + sqlxmlfilename3;

            options = Config["options"].ToString();

            mdbWhere = Config["mdbWhere"].ToString();
            sqlWhere = Config["sqlWhere"].ToString();
            mdbCode = Config["mdbCode"].ToString();
            sqlCode = Config["sqlCode"].ToString();
            try
            {
                server = Config["Server"].ToString();
                user = Config["Username"].ToString();
                pass = Config["Password"].ToString();
            }
            catch { }
            
            if(Config.Table.Columns.Contains("Ecommand"))
                Ecommand = Config["Ecommand"].ToString();

            if (Config.Table.Columns.Contains("Type"))
                type = Config["Type"].ToString();
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

            InitMainConfigXml();
            
            if (IsInTime)
            {
                _HaveLog = true;
                Login();
                if (type == "1")      //Mdb-sql
                {
                    //EXEC(mdbWhere, sqlWhere, mdbCode, sqlCode);
                }
                else if (type == "2") //sql-sql
                {
                    //_HaveLog = true;
                    //EXEC2();
                }
                else if(type == "3" || type == "4")
                {
                    _Message = "Type '" + type + "' not run this time!";
                }
            }
            else // Out Time
            {
                if (type == "3") //backup
                {
                    _HaveLog = true;
                    EXEC3();
                }
                else if (type == "4") //Backup folder
                {
                    _HaveLog = true;
                    EXEC4();
                }
            }

            if(type.ToUpper() == "S")
            {
                EXEC_S();
            }
        }


        private void EXEC_S()
        {
            SmsThread smsThread = new SmsThread();
            smsThread._sqlxmlfilename = sqlxmlfilename;
            smsThread.SendSmsAndEmail(ref _Message);
            
        }

        /// <summary>
        /// Tạo tên file có số thứ tự trong thư mục.
        /// Xoa file da cu.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private string GetUniqueName(string name, string ext, string folderPath, int maxFile)
        {
            if (!ext.StartsWith("."))   ext = "." + ext;
            if (!folderPath.EndsWith(@"\")) folderPath += @"\";

            int tries = 1;
            int delete = 1;

            string validatedFileName = folderPath + string.Format("{0}_{1:00}" + ext, name, tries);
            string deleteFile = "";

            while (File.Exists(validatedFileName))
            {
                tries++;
                validatedFileName = folderPath + string.Format("{0}_{1:00}" + ext, name, tries);
                
                if((tries==maxFile || tries==99) && File.Exists(validatedFileName))
                {
                    //Theo thong thuong se khong co truong hop nay.
                    //Nếu tồn tại đầy đủ tới file max thì xóa 1 và 2, tạo 1
                    delete = 1; tries = 1;
                    validatedFileName = folderPath + string.Format("{0}_{1:00}" + ext, name, 1);
                    if(File.Exists(validatedFileName)) File.Delete(validatedFileName);
                    //Xoa 1 o day roi thi 2 cung se bi xoa o duoi.
                                                            
                    break;
                }
            }

            if (tries == maxFile || tries == 99) delete = 1; //Neu tao file max xoa file 1
            else delete = tries+1;            //Khong thi xoa file tiep theo

            deleteFile = folderPath + string.Format("{0}_{1:00}" + ext, name, delete);
            if(File.Exists(deleteFile)) File.Delete(deleteFile);
            
            return validatedFileName;
        }

        /// <summary>
        /// Backup folder
        /// </summary>
        public void EXEC4()
        {
            string pathbak = GetXmlValue(sqlxmlfilename3, "Pathbak");
            string pathdat = GetXmlValue(sqlxmlfilename3, "Pathdat");
            string maxfile = GetXmlValue(sqlxmlfilename3, "Maxfile");
            string fromFolder = pathdat, toFolder = pathbak, toZipFile;

            int _maxfile = 5;
            _maxfile = Convert.ToInt16(maxfile);
            toZipFile = GetUniqueName("V6BackUp", ".7z", toFolder, _maxfile);

            this._Message = string.Format("Backup from [{0}] to [{1}]...", fromFolder, toFolder);

            if (File.Exists(toZipFile)) File.Delete(toZipFile);
            //V6Tools.V67z.ZipFolder(fromFolder, toZipFile);
            V6Tools.V67z.Run7z(@"a " + toZipFile + " " + fromFolder);
            //CopyToV6
            if (_setting.CopyToV6)
            {
                V6FileIO.CopyToVPN(toZipFile, _setting.VPN_IP, _setting.VPN_USER, _setting.VPN_EPASS, vpn_supfolder);
            }
            this._Message = "Sao lưu hoàn tất!";

        }
        public void EXEC3()
        {   
            sqlConString3 = UtilityHelper.ReadConnectionStringFromFileXML(sqlxmlfilename3);
            #region ==== BACKUP DATA ====

            //if (check_runnow == false)
            //{
                string pathbak = GetXmlValue(sqlxmlfilename3, "Pathbak");
                string maxfile = GetXmlValue(sqlxmlfilename3, "Maxfile");

                int _maxfile = 0;
                _maxfile = Convert.ToInt16(maxfile);

                if (pathbak != "")
                {
                    _Message = "Beginning ...Backup database ";
                    Log(_Message);
                    RunSync_Backupdatabases(pathbak, this.DatabaseName, _maxfile);
                    _Message = "Ending ...Backup database ";
                    Log(_Message);
                }
            //}
            #endregion ==== BACKUP DATA ====
        }
        public void RunSync_Backupdatabases(string pathbak, string pdatabase, int maxfile)
        {

            string _pathbak = "";

            string date_num = GetXmlValue(sqlxmlfilename3, "DateNum");
            string strsqlgetdate = "SELECT getdate()- " + date_num.ToString() + " AS Ngay_ct1,getdate() as Ngay_ct2 ";
            DataTable tb1 = SqlHelper
                .ExecuteDataset(sqlConString3, CommandType.Text, strsqlgetdate)
                .Tables[0];
            string _Ngay_ct1 = Convert.ToDateTime(tb1.Rows[0]["Ngay_ct1"]).ToString("yyyyMMdd");
            string _Ngay_ct2 = Convert.ToDateTime(tb1.Rows[0]["Ngay_ct2"]).ToString("yyyyMMdd");

            string _EXEC3_CON = this.sqlConString3;
            DataTable tb3 = null;
                    
            _pathbak = pathbak;


            string strsql = "", _filename1 = "", _filename = "", _filenamezip = "";



            //strsql = "SELECT max_file FROM DMBAKUP WHERE 1=1 ";
            strsql = "SELECT max_file FROM V6BAK WHERE 1=1 ";
            tb3 = SqlHelper.ExecuteDataset(_EXEC3_CON, CommandType.Text, strsql).Tables[0];

            int _maxfile1 = 0;
            int _maxfile2 = tb3.Rows.Count;

            if (_maxfile2 == 0)
                _maxfile1 = 0;
            else
                _maxfile1 = Int16.Parse(tb3.Rows[0][0].ToString().Trim());

            
            //_filename1 =@_pathbak+"\\"+pdatabase+"_"+DateTime.Today.ToString("yyyyMMdd")+".BAK" ;
            _filename1 = Path.Combine(@_pathbak, pdatabase);


            if (_maxfile1 == maxfile)
            {
                _filename = _filename1 + "_1" + ".BAK";
                _maxfile1 = 0;
                _filenamezip = _filename1 + "_1";
            }
            else
            {
                _filename = _filename1 + "_" + (_maxfile1 + 1).ToString() + ".BAK";
                _filenamezip = _filename1 + "_" + (_maxfile1 + 1).ToString();
            }



            strsql = "BACKUP DATABASE  [" + pdatabase.Trim() + "] TO DISK =N'" + _filename + "' WITH INIT,NOUNLOAD,NAME=N'" + pdatabase + " Backup', NOSKIP,STATS=10,NOFORMAT ";
            SqlHelper.ExecuteNonQuery(_EXEC3_CON, CommandType.Text, strsql, 1800);



            //Zip database
            string _Zipbakup = GetXmlValue(sqlxmlfilename3, "Zipbakup");

            if (_Zipbakup == "1")
            {
                FileInfo fi = new FileInfo(_filename);
                var s = 0;
                while (V6FileIO.IsFileLocked(fi))
                {
                    s++;
                    if (s == 3600) return;
                    Thread.Sleep(1000);
                }

                if (File.Exists(_filename))
                {
                    if (File.Exists(_filenamezip + ".7z")) File.Delete(_filenamezip + ".7z");
                    V6Tools.V67z.Run7z("a " + _filenamezip + ".7z " + _filename + " -aoa ");
                    //V6Tools.V67z.ZipFile(_filenamezip, _filename);
                    if (_setting.CopyToV6)
                    {
                        V6FileIO.CopyToVPN(_filenamezip + ".7z", _setting.VPN_IP, _setting.VPN_USER, _setting.VPN_EPASS, vpn_supfolder);
                    }
                }
            }

            // Dele file
            string _Delebakup = GetXmlValue(sqlxmlfilename3, "Delebakup");
            if (_Delebakup == "1")
            {
                string _filename0 = "";
                for (int i = 0; i < maxfile + 1; i++)
                {
                    if (i != (_maxfile1 + 1))
                    {
                        _filename0 = _filename1 + "_" + i.ToString() + ".BAK";
                        if (File.Exists(_filename0))
                            File.Delete(_filename0);

                    }
                }
            }

            // Insert DMBackup
            if (_maxfile2 == 0)

                //strsql = " INSERT DMBAKUP (File_name,max_file,file_zip,ngay_bk) VALUES ('" + _filename + "'," + (_maxfile1 + 1).ToString() + ",'" + _filenamezip + "','" + _Ngay_ct2 + "')";
                strsql = " INSERT V6BAK (File_name,max_file,file_zip,ngay_bk) VALUES ('" + _filename + "'," + (_maxfile1 + 1).ToString() + ",'" + _filenamezip + "','" + _Ngay_ct2 + "')";

            else
                //strsql = " UPDATE DMBAKUP SET File_name='" + _filename + "',max_file=" + (_maxfile1 + 1).ToString() + ",file_zip='" + _filenamezip + "',ngay_bk='" + _Ngay_ct2 + "' WHERE 1=1";
                strsql = " UPDATE V6BAK SET File_name='" + _filename + "',max_file=" + (_maxfile1 + 1).ToString() + ",file_zip='" + _filenamezip + "',ngay_bk='" + _Ngay_ct2 + "' WHERE 1=1";

            SqlHelper.ExecuteNonQuery(_EXEC3_CON, System.Data.CommandType.Text, strsql);

            //_Message = "RunSync_Backupdatabase finish.";
        }


        private void EXEC2()//********************* Type = 2 ***************************
        {
            // Hàm xử lý khi type = 2
            sqlConString = UtilityHelper.ReadConnectionStringFromFileXML(sqlxmlfilename);
            sqlConString2 = UtilityHelper.ReadConnectionStringFromFileXML(sqlxmlfilename2);
            loadInfo();
            try
            {
                //DataTable tempTable = SqlHelper
                    //.ExecuteDataset(sqlConString, CommandType.Text, "select*from tbl").Tables[0];
                //Các câu lệnh sẽ viết ở đây.
                _Message = "Not any command here...";
            }
            catch (Exception ex)
            {
                _Message = "Lỗi: " + ex.Message;
            }
            
            
        }

        #region === Main config var ===
        string _CONSTRING;
        int _hhfrom, _hhto;
        bool _sqlYN;
        #endregion === Main config var ===
        /// <summary>
        /// Đọc lại main config xml.
        /// </summary>
        private void InitMainConfigXml()
        {
            try
            {
                string mainxmlfile = __dir + "\\" + Process.GetCurrentProcess().ProcessName + ".xml";
                string __hhfrom = GetXmlValue(mainxmlfile, "Timefrom");
                string __hhto = GetXmlValue(mainxmlfile, "Timeto");
                string __sqlYN = GetXmlValue(mainxmlfile, "SqlYN");
                _CONSTRING = UtilityHelper.ReadConnectionStringFromFileXML(mainxmlfile);

                if (__hhfrom == "")
                    __hhfrom = "07";
                if (__hhto == "")
                    __hhto = "21";
                if (__sqlYN == "")
                    __sqlYN = "1";

                _hhfrom = Convert.ToInt16(__hhfrom);
                _hhto = Convert.ToInt16(__hhto);
                _sqlYN = "1" == __sqlYN;
            }
            catch(Exception ex)
            {
                throw new Exception("InitMainConfig error. " + ex.Message);
            }

        }
        //bool IsInTime0()
        //{
        //    InitMainConfigXml();
        //    int hh = 0;//Lấy thời gian hiện tại theo kiểu HH
        //    if (_sqlYN)
        //    {
        //        string strSQL0 = "SELECT CONVERT(VARCHAR(8),GETDATE(),108) AS curTime";
        //        DataTable tb3 = SqlHelper.ExecuteDataset(_CONSTRING, System.Data.CommandType.Text, strSQL0).Tables[0];
        //        hh = Convert.ToInt16(tb3.Rows[0][0].ToString().Trim().Substring(0, 2));
        //    }
        //    else
        //    {
        //        hh = DateTime.Now.Hour;
        //    }
        //    if (hh >= _hhfrom && hh <= _hhto)
        //    {
        //        return true;
        //    }
        //    else { return false; }
        //}

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

        private void Login()
        {
            if (!string.IsNullOrEmpty(Ecommand))
            {
                //Thuwjc thi command
                try
                {
                    string Dcommand = UtilityHelper.DeCrypt(Ecommand);
                    // create the ProcessStartInfo using "cmd" as the program to be run,
                    // and "/c " as the parameters.
                    // Incidentally, /c tells cmd that we want it to execute the command that follows,
                    // and then exit.
                    System.Diagnostics.ProcessStartInfo procStartInfo =
                        new System.Diagnostics.ProcessStartInfo("cmd", "/c " + Dcommand);

                    // The following commands are needed to redirect the standard output.
                    // This means that it will be redirected to the Process.StandardOutput StreamReader.
                    procStartInfo.RedirectStandardOutput = true;
                    procStartInfo.UseShellExecute = false;
                    // Do not create the black window.
                    procStartInfo.CreateNoWindow = true;
                    // Now we create a process, assign its ProcessStartInfo and start it
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo = procStartInfo;
                    proc.Start();
                    // Get the output into a string
                    string result = proc.StandardOutput.ReadToEnd();
                    // Display the command output.
                    //Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Log("ThreadLogin: " + ex.Message);
                }
            }
            
        }

        /// <summary>
        /// Mdb-sql
        /// </summary>
        /// <param name="mdbWhere"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="mdbCode"></param>
        /// <param name="sqlCode"></param>
        public void EXEC(
            string mdbWhere, string sqlWhere,
            string mdbCode, string sqlCode)
        {
            mdbConString = UtilityHelper.ReadConnectionStringFromFileXML(mdbxmlfilename);
            sqlConString = UtilityHelper.ReadConnectionStringFromFileXML(sqlxmlfilename);
            loadInfo();

            int n1 = 0, n2 = 0, n3 = 0;
            string P_error = "";

            if (options.Contains("\\r1") || options.Contains("\\r2"))
            try
            {
                _Message = "Update sql:";
                string updateSql = "Update alnohd set dorel=1 where fileno in "
                    + "(select ma_hd from alhd where ma_hd in "
                    + "(select fileno from alnohd where dorel=0) and dorel=1)";
                SqlHelper.ExecuteNonQuery(sqlConString, CommandType.Text, updateSql);
            }
            catch (Exception ex)
            {
                if (P_error != "") P_error += "\n";
                P_error += ex.Message;
            }

            DataTable mdbData
                = GetMDBData(mdbConString, mdbTableName, mdbSelectFields, mdbWhere);
            //MessageBox.Show("data access dorel = 0 :" + mdbData.Rows.Count);

            //Lọc bỏ bớt dữ liệu

            for (int i = mdbData.Rows.Count - 1; i >= 0; i--)
            {
                //Lấy khóa từ dữ liệu Access
                DataRow row = mdbData.Rows[i];
                string tempKey = GenSqlStringValue(row[mdbPrimarykeyField], false);
                
                string query = "[" + sqlPrimarykeyField + "]=" + tempKey;
                //Kiểm tra trong csdl SQL, nếu có thì kiểm tra update access và loại bỏ khỏi danh sách insert
                switch (existRow(sqlTableName,
                    sqlConString,
                    query, sqlWhere))
                {
                    case 2:
                        //Cập nhật access
                        if (options.Contains("\\r1") || options.Contains("\\r2"))
                        {
                            
                            _Message = "Update access: " + tempKey;
                            try
                            {
                                string fields = sqlPrimarykeyField + "," + mdbUpdateFields;
                                DataTable sqlData = GetSQLData(sqlConString, sqlTableName, fields, query);
                                //DataTable strucTable = getTableStruct(sqlConString, sqlTableName);
                                sqlData = ConvertTable(sqlData, sqlCode, mdbCode);
                                n2 += UpdateMDB(mdbConString, mdbTableName, mdbPrimarykeyField, sqlData, sqlData);
                            }
                            catch (Exception ex)
                            {
                                if (P_error != "") P_error += "\n";
                                P_error += ex.Message;
                            }
                        }
                        //xóa khỏi insert vào sql
                        mdbData.Rows.Remove(row);
                        break;
                    case 1:
                        //xóa khỏi insert vào sql
                        _Message = "Added row: " + tempKey;
                        mdbData.Rows.Remove(row); break;
                    case 0:
                        _Message = "New row: " + tempKey;
                        break;
                    default: break;
                }
            }


            //Chuyển mã nếu có.
            if (mdbCode.ToUpper() != sqlCode.ToUpper() && !string.IsNullOrEmpty(mdbCode) && !string.IsNullOrEmpty(sqlCode))
            {
                mdbData = ConvertTable(mdbData, mdbCode, sqlCode);
            }
            //Đưa dữ liệu lấy từ access đưa vào sql


            if (options.Contains("\\r0") || options.Contains("\\r2"))
            {
                //MessageBox.Show("r0 hoac r2");
                try
                {
                    sqlStructTable = getTableStruct(sqlConString, sqlTableName);
                    n1 = InsertDatabase(sqlConString, sqlTableName, sqlPrimarykeyField, sqlStructTable, mdbData, sqlWhere);
                    if (error != "") P_error += error;
                }
                catch (Exception ex)
                {
                    if (P_error != "") P_error += "\n";
                    P_error += ex.Message;
                    n1 = 0;
                }

                //MessageBox.Show("Test//Chuyển sql Alnohd => Alhd");
                try
                {
                    string getAlnohdSql = "Select hblno as so_bill, fileno as ma_hd, hblno as ten_hd, dorel"
                        + ",'1' as status, getdate() as date0, '0' as ct"
                        +" from ALNOHD where Fileno <> '' and Fileno is not null and Fileno not in (select ma_hd from alhd)";
                    DataTable data_alnohd = SqlHelper.ExecuteDataset(sqlConString, CommandType.Text, getAlnohdSql).Tables[0];
                    //MessageBox.Show("data alnohd=>alhd:"+data_alnohd.Rows.Count);
                    sqlStructTable = getTableStruct(sqlConString, "ALHD");
                    n3 = InsertDatabase(sqlConString, "ALHD","Ma_hd", sqlStructTable, data_alnohd, "");
                }
                catch (Exception ex)
                {
                    if (P_error != "") P_error += "\n";
                    P_error += ex.Message;
                    n3 = 0;
                }
            }
            

            //if (options.Contains("\\m"))
            {
                if (P_error == "")
                    _Message = "Đã cập nhập " + n1 + " dòng từ Access vào SQL.\nvà " +
                        n2 + " dòng từ SQL vào Access.\nvà " +
                        n3 + " dòng từ sql-sql.";
                else
                {
                    _Message = "Đã cập nhập " + n1 + " dòng từ Access vào SQL\nvà " +
                      n2 + " dòng từ SQL vào Access." +
                      "\nCó lỗi trong quá trình cập nhập:\n" + P_error;
                    throw new Exception(_Message);
                }

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
        private void loadInfo()
        {
            #region === load MDB info ===
            XmlTextReader reader = new XmlTextReader(mdbxmlfilename);
            
            try
            {
                while (reader.Read())
                {
                    switch (reader.Name.ToUpper())
                    {
                        case "TABLE":
                            {
                                if (reader.GetAttribute("Name") != null && reader.GetAttribute("Name") != "")
                                {
                                    mdbTableName = reader.GetAttribute("Name");
                                }
                                if (reader.GetAttribute("DataFields") != null && reader.GetAttribute("DataFields") != "")
                                {
                                    mdbSelectFields = reader.GetAttribute("DataFields");
                                }
                                if (reader.GetAttribute("Primarykey") != null && reader.GetAttribute("Primarykey") != "")
                                {
                                    mdbPrimarykeyField = reader.GetAttribute("Primarykey");
                                }
                                if (reader.GetAttribute("UpdateFields") != null && reader.GetAttribute("UpdateFields") != "")
                                {
                                    mdbUpdateFields = reader.GetAttribute("UpdateFields");
                                }

                                
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                reader.Close();
                _Message = "loadInfoMDB:" + ex.Message;
            }
            
            #endregion === MDB info ===

            #region === load SQL info ===

            reader = new XmlTextReader(sqlxmlfilename);
            try
            {
                while (reader.Read())
                {
                    switch (reader.Name.ToUpper())
                    {
                        case "TABLE":
                            {
                                if (reader.GetAttribute("Name") != null && reader.GetAttribute("TableName") != "")
                                {
                                    sqlTableName = reader.GetAttribute("Name");
                                }
                                //if (reader.GetAttribute("SelectFields") != null && reader.GetAttribute("SelectFields") != "")
                                //{
                                //    sqlSelectFields = reader.GetAttribute("SelectFields");
                                //}
                                if (reader.GetAttribute("Primarykey") != null && reader.GetAttribute("Primarykey") != "")
                                {
                                    sqlPrimarykeyField = reader.GetAttribute("Primarykey");
                                }

                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                reader.Close();
                _Message = "loadInfoSQL:" + ex.Message;
            }
            #endregion === SQL info ===

            #region === load SQL2 info ===
            //Nếu cần thì sử dụng, copy hàm giống ở trên, tùy biến sử dụng.
            #endregion === SQL2 info ===

        }

        int UpdateMDB(string mdbConString, string mdbTableName, string primaryKeyField, DataTable data, DataTable structTable)
        {
            int n = 0;
            using (OleDbConnection connection = new OleDbConnection(mdbConString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                string commandText = "";
                foreach (DataRow row in data.Rows)
                {
                    commandText = GenUpdateSql(mdbTableName, primaryKeyField, structTable, row);
                    //_MessageBox.Show(commandText);
                    command.CommandText = commandText;
                    n += command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return n;
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

        static DataTable GetMDBData(string mdbConString, string TableName, string Fields, string where)
        {
            OleDbConnection con = new OleDbConnection(mdbConString);
            if (where.Trim() != "") where = " Where " + where;
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter("select " + Fields + " from [" + TableName + "]" + where, con);
                da.Fill(ds, TableName);
                con.Close();
                return ds.Tables[TableName];
            }
            catch (Exception ex)
            {
                con.Close();
                throw new Exception("Get MDB data: " + ex.Message);
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
            catch (Exception)
            {

                throw;
            }
        }

        #endregion ==== ==== ==== Viết code ở trên ==== ==== ====
          ///////////////////////////////////////////
         ///////////////////////////////////////////
        ///////////////////////////////////////////
    }
}
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Threading;
using System.Xml;
using V6ThreadLibrary;
using V6Tools;

namespace V6SyncServices
{
    public partial class SyncService : ServiceBase
    {
        
        MyThreads MultiThreads;
        V6SyncSetting _Setting;

        public static bool _Exit = false;
        private bool _IsRunning = false;
        int _Sleepedtime = 10;
        int _SleepTime_m, _SleepTime_s;
        bool _StopWhenFinish = false;

        // Tuanmh 24/02/2016
        public string Server_IP = "";

        public SyncService()
        {
            InitializeComponent();
            MyInit();
        }

        void MyInit()
        {
            

            _Setting = new V6SyncSetting(AppDomain.CurrentDomain.BaseDirectory + "\\Setting.ini");
            
            string xmltablefile = _Setting.LastOpenXmlTable;
            
            if (string.IsNullOrEmpty(xmltablefile))
            {
                Program.Log("Null or empty xmltablefile");
            }
            else
            {
                LoadXmlTable(xmltablefile);
            }

        }

        /// <summary>
        /// Lấy thông tin trong Setting.ini
        /// </summary>
        private void LoadSettingIni()
        {   
            string readString;

            #region ==== SleepTime ====

            Server_IP = _Setting.ServerIP;

            readString = _Setting.GetSetting("SleepTime_m");
            try
            {
                _SleepTime_m = 1;
                int.TryParse(readString, out _SleepTime_m);
            }
            catch
            {}

            readString = _Setting.GetSetting("SleepTime_s");
            try
            {
                _SleepTime_s = 1;
                int.TryParse(readString, out _SleepTime_s);
            }
            catch
            {}
            #endregion SleepTime

            

            #region ==== StopWhenFinish ====
            readString = _Setting.GetSetting("StopWhenFinish");
            try
            {
                _StopWhenFinish = _Setting.GetSetting("StopWhenFinish") == "1";
                //checkBoxStopWhenFinish.Checked = booltemp;
                //txtSleepTime_m.Enabled = !booltemp;
                //txtSleepTime_s.Enabled = !booltemp;
                //btnSetSleepTime.Enabled = !booltemp;
            }
            catch
            {
                //checkBoxStopWhenFinish.Checked = false;
                _StopWhenFinish = false;
                _Setting.SetSetting("StopWhenFinish", "0");
                _Setting.SaveSetting();
            }
            #endregion StopWhenFinish
        }
        
        protected override void OnStart(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += currentDomain_UnhandledException;
            //MediaTypeNames.Application.ThreadException += Application_ThreadException;
            try
            {
                Program.Log("OnStart: " + Program.__dir);
                StartThread();
                StartSeriServer();
            }
            catch (Exception ex)
            {
                Program.Log("Start Service Error: " + ex.Message);
            }
        }

        #region ==== Seri Server ====
        const int MAX_CONNECTION = 100;
        const int PORT_NUMBER = 6666;
        static int _connectionsCount = 0;
        static TcpListener serverListener;
        private IPAddress _address;
        
        private void StartSeriServer()
        {
            try
            {
                var computer_name = Environment.MachineName;
                //_clientList = GetClientList();
                var ip = Dns.GetHostAddresses(computer_name)[1].ToString();
               
                //Tuanmh {Fix IP
                ip = Server_IP;
                Program.Log("Start SeriServer IP: " + ip);
                //}

                _address = IPAddress.Parse(ip);
                Thread T = new Thread(ServerListener);
                T.IsBackground = true;
                T.Start();
            }
            catch (Exception ex)
            {
                Program.Log("Start SeriServer Ex: " + ex.Message);
            }
        }

        private void ServerListener()
        {
            try
            {
                Program.Log("StartLicenceServerSeri");
                serverListener = new TcpListener(_address, PORT_NUMBER);
                serverListener.Start();
                while (true)
                {
                    if (_connectionsCount < MAX_CONNECTION)
                    {
                        try
                        {
                            Socket ClientSocket = serverListener.AcceptSocket();
                            _connectionsCount++;
                            _message += "Accept client LocalEndPoint " + ClientSocket.LocalEndPoint.ToString();
                            _message += "Accept client RemoteEndPoint " + ClientSocket.RemoteEndPoint.ToString();
                            //_message += "Accept client AddressFamily " + (ClientSocket.AddressFamily).ToString();

                            var length = _message.Length;
                            Program.Log(_message);
                            _message = _message.Substring(length);

                            Thread t = new Thread((obj) =>
                            {
                                DoWork((Socket)obj);
                            });
                            t.Start(ClientSocket);
                        }
                        catch (Exception ex)
                        {
                            Program.Log(string.Format("ServerListener while ({0})", ex.Message));
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Program.Log("ServerListener: " + ex.Message);
            }
        }

        private string _message = "";
        private void DoWork(Socket socket)
        {
            try
            {
                var stream = new NetworkStream(socket);
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                
                while (true)
                {
                    //read client computerName and dir
                    string s = reader.ReadLine()??"";
                    string[] clientInfos = s.Split(';');
                    string clientName="", dir="";
                    if (clientInfos.Length == 2)
                    {
                        clientName = clientInfos[0];
                        dir = clientInfos[1];
                    }

                    if (!String.IsNullOrEmpty(clientName))
                    {
                        if (AllowConnect(clientName))
                        {
                            writer.WriteLine(V6Init.License.GetSeri(dir));
                            Program.Log("Seriserver Allow " + clientName);
                        }
                        else
                        {
                            Program.Log("Seriserver not Allow " + clientName);
                        }
                        break;//Gui 1 lan roi thoi
                    }
                }
                _connectionsCount--;
                stream.Close();
            }
            catch (Exception ex)
            {
                Program.Log("Seriserver Dowork Error: " + ex);
            }
            socket.Close();
        }

        bool AllowConnect(string clientName)
        {
            try
            {
                var xtbfile = "Clients.xtb";
                xtbfile = Path.Combine(Program.__dir, xtbfile);
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
                    if (row["Name"].ToString().Trim().ToUpper() == clientName.ToUpper())
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
                    row["Name"] = clientName.ToUpper();
                    row["Allow"] = "0";
                    data.Rows.Add(row);
                    data.WriteXml(Path.Combine(Program.__dir, "Clients.xtb"), true);
                }

            }
            catch (Exception ex)
            {
                _message += "CheckAllowConnect ex: " + ex.Message;
                var length = _message.Length;
                Program.Log(_message);
                _message = _message.Substring(length);
            }
            return false;
        }
        #endregion

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Program.Log("ThreadException " + e.Exception.Message);
        }

        void currentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Program.Log("UnhandledException " + e.ExceptionObject.ToString());
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Program.Log("Reload setting.");
            _Setting.ReloadSetting();
        }

        protected override void OnStop()
        {
            Program.Log("OnStop.");
        }

        public static DataTable dtServerList;
        public static DataView dvServerList;

        private void LoadXmlTable(string fileName)
        {
            Program.Log("" + "LoadXmlTable");
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileName);
                
                if (ds.Tables.Count > 0)
                {
                    dtServerList = ds.Tables[0];
                    Program.Log("Count dtServerList row: " + dtServerList.Rows.Count);
                    //Thêm các cột thiếu.
                    //Nếu chưa có cột server
                    if (!dtServerList.Columns.Contains("STT"))
                        dtServerList.Columns.Add("STT", typeof(string));
                    if (!dtServerList.Columns.Contains("Server"))
                        dtServerList.Columns.Add("Server", typeof(string));
                    //if (!dtServerList.Columns.Contains("Database"))
                    //    dtServerList.Columns.Add("Database", typeof(string));
                    //if (!dtServerList.Columns.Contains("User"))
                    //    dtServerList.Columns.Add("User", typeof(string));
                    //if (!dtServerList.Columns.Contains("EPass"))
                    //    dtServerList.Columns.Add("EPass", typeof(string));
                    if (!dtServerList.Columns.Contains("Run"))
                        dtServerList.Columns.Add("Run", typeof(string));
                    if (!dtServerList.Columns.Contains("Status"))
                        dtServerList.Columns.Add("Status", typeof(string));
                    //
                    //dgvServerConfig.AccessibleName = fileName;
                }
                else
                {
                    dtServerList = new DataTable();
                    //dgvServerConfig.AccessibleName = "";
                    //MessageBox.Show("Không đọc được bảng dữ liệu nào!");
                }
                //change(tbl);

                //BindingSource bSource = new BindingSource();
                dvServerList = new DataView(dtServerList);
                dvServerList.Sort = "STT asc";
                //bSource.DataSource = tblServerList;
                //bSource.DataSource = dvServerList;

                //dataGridView1.Columns.Clear();
                //dgvServerConfig.DataSource = bSource;
                
            }
            catch (Exception ex)
            {
                Program.Log("Load xml table error: " + ex.Message);
            }
        }

        /// <summary>
        /// Tạo list MyThread nếu chưa tạo
        /// </summary>
        void CreateThreads()
        {
            try
            {
                //if (MultiThreads == null)
                {
                    MultiThreads = new MyThreads(_Setting);

                    if (dvServerList != null)
                    {
                        DataTable dtbSLt = dvServerList.ToTable();
                        for (int i = 0; i < dvServerList.Count; i++)
                        {
                            if (dtbSLt.Rows[i]["Run"].ToString() == "1")
                                MultiThreads.Add(new
                                    MyThread(_Setting, i,
                                        dtbSLt.Rows[i][1].ToString(),
                                        dtbSLt.Rows[i]));
                        }
                    }
                    else
                    {
                        Program.Log("Chua co cau hinh server nao! StopThread");
                        //    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        StopThread();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log("CreateThreads error " + ex.Message);
            }
        }

        void DestroyThreads()
        {
            MultiThreads = null;
        }

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

        private string _CONSTRING = "";
        private int _hhfrom = 07, _hhto = 21;
        private int _outTimeCount;
        private bool _sqlYN = false;
        /// <summary>
        /// Đọc lại main config xml.
        /// </summary>
        private void LoadMainConfigXml()
        {
            try
            {
                string mainxmlfile = Program.__dir + "\\" + Process.GetCurrentProcess().ProcessName + ".xml";
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
            catch (Exception ex)
            {
                throw new Exception("InitMainConfig error. " + ex.Message);
            }

        }
        bool IsInTime()
        {
            string debug = "1";
            try
            {
                
                LoadMainConfigXml();
                debug = "2;";

                int hh = 0;//Lấy thời gian hiện tại theo kiểu HH
                if (_sqlYN)
                {
                    debug = "3";

                    string strSQL0 = "SELECT CONVERT(VARCHAR(8),GETDATE(),108) AS curTime";
                    DataTable tb3 = SqlHelper.ExecuteDataset(_CONSTRING, CommandType.Text, strSQL0).Tables[0];
                    debug = "4";
                    hh = Convert.ToInt16(tb3.Rows[0][0].ToString().Trim().Substring(0, 2));
                }
                else
                {
                    hh = DateTime.Now.Hour;
                }

                if (hh >= _hhfrom && hh <= _hhto)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                Program.Log("IsInTime error: " + debug + " " + _CONSTRING + ex.Message);
                throw ex;
            }
        }

        
        void StartThread()
        {
            string debug = "1";
            LoadSettingIni();
            LoadMainConfigXml();
            CreateThreads();
            try
            {
                if (IsInTime())
                {
                    Program.Log("Start in time.");
                    _outTimeCount = 0;
                    MultiThreads.IsInTime = true;
                    MultiThreads.Start();
                    _IsRunning = true;
                    timerRunning.Start();
                }
                else//outTime
                {
                    Program.Log("Start out time.");
                    _outTimeCount++;
                    MultiThreads.IsInTime = false;
                    if (_outTimeCount == 1)
                    {
                        MultiThreads.Start();
                        _IsRunning = true;
                        timerRunning.Start();
                    }
                    else if (_outTimeCount == 10)
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log("StartThread error: " + debug + " " + ex.Message);
            }
        }

        void StopThread()
        {
            Program.Log("StopThreads.");
            try
            {
                _IsRunning = false;
                timerSleep.Stop();
                if (MultiThreads != null) MultiThreads.Stop();
            }
            catch (Exception ex)
            {
                Program.Log("StopThread error: " + ex.Message);
            }
        }

        
        void WriteLog()
        {
            foreach (MyThread item in MultiThreads)
            {
                if (item._HaveLog)
                {
                    if (!dtServerList.Columns.Contains("Status"))
                    {
                        dtServerList.Columns.Add("Status");
                    }
                    Program.Log("Item:" +
                                item._ThreadName +
                                ", " + item._Status +
                                ", " + item._Message);
                }
            }
        }

        private void timerSleep_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            _Sleepedtime++;
            long sleeped_m = _Sleepedtime / 60,
                sleeped_s = _Sleepedtime % 60;

            if (sleeped_m > _SleepTime_m ||
                (sleeped_m == _SleepTime_m && sleeped_s == _SleepTime_s))
            {
                _Sleepedtime = 0;
                timerSleep.Stop();
                
                if (dvServerList != null)
                {
                    //Khởi chạy lại Thread
                    StartThread();

                    _IsRunning = true;
                    //lblSleep_s.Text = "Run";
                    timerRunning.Start();
                }
                else
                {
                    //btnStop_Click(sender, e);
                    Program.Log("Cannot Restart thread because dvslist null!");
                    StopThread();
                }
            }
        }

        private void timerStatus_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            //if (_IsRunning) UpdateProgress();//Winform
            if (MultiThreads.Status != MyThreadStatus.Run)
            {
                if (_StopWhenFinish)
                {
                    Program.Log("_StopWhenFinish");
                    StopThread();
                }
                else
                {
                    //WriteLog();
                    timerRunning.Stop();
                    DestroyThreads();

                    timerSleep.Start();
                    
                    _IsRunning = false;
                }
            }
        }


    }
}

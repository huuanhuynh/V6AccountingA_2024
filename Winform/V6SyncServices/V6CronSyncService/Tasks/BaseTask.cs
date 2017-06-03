using System;
using System.Data;
using System.Diagnostics;
using System.Xml;
using Quartz;
using V6ThreadLibrary;
using V6Tools;

namespace V6CronSyncService.Tasks
{
    public class BaseTask : IJob
    {
        #region ==== Properties ====

        protected V6SyncSetting _Setting;

        protected MyThreads MultiThreads;
        public static DataTable dtServerList;
        public static DataView dvServerList;
        
        public static bool _Exit = false;
        private bool _IsRunning = false;
        int _SleepTime_m, _SleepTime_s;

        // Tuanmh 24/02/2016
        public string Server_IP = "";
        #endregion properties
        
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                StartThread();
            }
            catch (Exception ex)
            {
                Program.WriteExLog(GetType() + ".Execute", ex, "");
            }
        }

        void StartThread()
        {
            string debug = "1";
            try
            {
                LoadSettingIni();
                LoadMainConfigXml();
                LoadXmlTable();
                CreateThreads();
                if (MultiThreads.Count == 0) return;
                Program.WriteToLog(GetType() + ".StartThread");
                MultiThreads.Start();
            }
            catch (Exception ex)
            {
                Program.WriteExLog(GetType() + ".StartThread", ex, MultiThreads.Message);
            }
            Program.WriteToLog(GetType() + ".EndThread" + MultiThreads.Message);
        }

        /// <summary>
        /// Lọc lại tác vụ cần chạy vd: if (dtbSLt.Rows[i]["Run"].ToString() == "1")
        /// </summary>
        protected virtual void CreateThreads()
        {
            try
            {
                MultiThreads = new MyThreads(_Setting);

                if (dvServerList != null)
                {
                    DataTable dtbSLt = dvServerList.ToTable();
                    for (int i = 0; i < dvServerList.Count; i++)
                    {
                        var newMyThread = new MyThread(_Setting, i, dtbSLt.Rows[i][1].ToString(), dtbSLt.Rows[i]);
                        newMyThread.ThrowExceptionEvent += newMyThread_ThrowExceptionEvent;
                        if (dtbSLt.Rows[i]["Run"].ToString() == "1")
                            MultiThreads.Add(newMyThread);
                    }
                }
                else
                {
                    Program.WriteToLog("Chua co cau hinh server nao! StopThread");
                    //    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Program.WriteToLog(GetType() + ".CreateThreads error " + ex.Message);
            }
        }

        protected virtual void CreateThreads(string taskType)
        {
            try
            {
                MultiThreads = new MyThreads(_Setting);

                if (dvServerList != null)
                {
                    DataTable dtbSLt = dvServerList.ToTable();
                    for (int i = 0; i < dvServerList.Count; i++)
                    {
                        var newMyThread = new MyThread(_Setting, i, dtbSLt.Rows[i][1].ToString(), dtbSLt.Rows[i]);
                        newMyThread.ThrowExceptionEvent += newMyThread_ThrowExceptionEvent;
                        if (dtbSLt.Rows[i]["Run"].ToString() == "1"
                            && dtbSLt.Rows[i]["TaskType"].ToString() == taskType)
                            MultiThreads.Add(newMyThread);
                    }
                }
                else
                {
                    Program.WriteToLog("Chua co cau hinh server nao! StopThread");
                }
            }
            catch (Exception ex)
            {
                Program.WriteExLog(GetType() + ".CreateThreads error ", ex, taskType);
            }
        }

        void newMyThread_ThrowExceptionEvent(Exception ex)
        {
            Program.WriteExLog("ThreadThrowException", ex);
        }
        
        private void LoadSettingIni()
        {
            _Setting = new V6SyncSetting(AppDomain.CurrentDomain.BaseDirectory + "\\Setting.ini");
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
            { }

            readString = _Setting.GetSetting("SleepTime_s");
            try
            {
                _SleepTime_s = 1;
                int.TryParse(readString, out _SleepTime_s);
            }
            catch
            { }
            #endregion SleepTime

        }

        private void LoadXmlTable()
        {
            Program.WriteToLog("" + "LoadXmlTable");
            string fileName = _Setting.LastOpenXmlTable;
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileName);

                if (ds.Tables.Count > 0)
                {
                    dtServerList = ds.Tables[0];
                    Program.WriteToLog("Count dtServerList row: " + dtServerList.Rows.Count);
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
                Program.WriteExLog(GetType() + ".StartThread", ex, "");
            }
        }

        private string _CONSTRING = "";
        private int _hhfrom = 07, _hhto = 21;
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

    }
}

using System;
using System.IO;
using System.Threading;

namespace HaUtility.Helper
{
    public class Logger
    {
        public Logger()
        {

        }
        public Logger(string dir, string name)
        {
            _logDir = dir;
            _logFile = name;
        }

        private string _logDir = "";

        private string _logFile = "Log";

        /// <summary>
        /// Thư mục chứa các file log.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="file"></param>
        public void SetLogger(string dir, string file)
        {
            _logDir = dir;
            _logFile = file;
        }
        
        DateTime _oldDate;
        string _oldLogFile;
        FileStream _fs;
        StreamWriter _sw;
        string _messageToWrite = "";
        int _keepDays = 7;
        int _deleteDeep = 10;

        /// <summary>
        /// Ghi log với thời gian (của máy đang chạy)
        /// </summary>
        /// <param name="message">Thông tin ghi vào</param>
        /// /// <param name="logFile">File log ghi vào (không dir, vd: "Program").</param>
        /// <param name="logDir">Vị trí lưu log (Không truyền=để rỗng hoặc C:\LogDir).</param>
        public static void WriteToLog(string message, string logFile = "Log", string logDir = "")
        {
            try
            {
                Logger lg = new Logger();
                lg.WriteLog(lg._logDir, logFile, message);
            }
            catch
            {
                // ignored
            }
        }

        public void WriteLog0(string message)
        {
            WriteLog(_logDir, _logFile, message);
        }

        /// <summary>
        /// Ghi log vào file
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="message"></param>
        /// <param name="dir"></param>
        public void WriteLog(string dir, string logName, string message)
        {
            _logDir = dir;
            _logFile = logName;

            _messageToWrite = DateTime.Now + ": " + message;
            WriteLogT();
        }
        public void WriteLog(string dir, string logName, string message, int keepDays, int deleteOldDayDeep)
        {
            _logDir = dir;
            _logFile = logName;

            _keepDays = keepDays;
            _deleteDeep = deleteOldDayDeep;
            _messageToWrite = DateTime.Now + ": " + message;
            var T = new Thread(WriteLogT) { IsBackground = true };
            T.Start();
        }
        void WriteLogT()
        {

            try
            {
                if (!string.IsNullOrEmpty(_logDir))
                {
                    if (!Directory.Exists(_logDir))
                    {
                        Directory.CreateDirectory(_logDir);
                    }
                }
                string todayLogFile = Path.Combine(_logDir, _logFile + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                _fs = new FileStream(todayLogFile, FileMode.Append);

                _sw = new StreamWriter(_fs);
                _sw.WriteLine(_messageToWrite);
                _sw.Close();
                _fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally // delete old logfile
            {
                var t = new Thread(DeleteOldLog) { IsBackground = true };
                t.Start();
            }
        }

        void DeleteOldLog()
        {
            try
            {
                if (_keepDays < 3) _keepDays = 7;
                if (_deleteDeep < 3) _deleteDeep = 10;
                for (int i = 0 - _keepDays; i > 0 - _keepDays - _deleteDeep; i--)
                {
                    _oldDate = DateTime.Now.AddDays(i);
                    _oldLogFile = Path.Combine(_logDir, _logFile + _oldDate.ToString("yyyyMMdd") + ".txt");
                    if (File.Exists(_oldLogFile)) File.Delete(_oldLogFile);
                }
            }
            catch
            {
                // ignored
            }
        }

    }
}

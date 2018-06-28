using System;
using System.IO;
using System.Threading;

namespace V6Tools
{
    public class Logger
    {
        public Logger()
        {

        }
        public Logger(string dir, string name)
        {
            logDir = dir;
            logFile = name;
        }

        private string logDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

        private string logFile = "V6Log";

        /// <summary>
        /// Thư mục chứa các file log.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="file"></param>
        public void SetLogger(string dir, string file)
        {
            logDir = dir;
            logFile = file;
        }

        DateTime oldDate;
        string oldLogFile;
        FileStream fs;
        StreamWriter sw;
        string messageToWrite = "";
        int _keepDays = 7;
        int _deleteDeep = 10;

        /// <summary>
        /// Ghi log với thời gian (của máy đang chạy). V6Login.ClientName + " " + GetType() + ".Method"
        /// </summary>
        /// <param name="message">Thông tin muốn ghi.</param>
        /// <param name="logFile">Tên file log. Thường dùng tên chương trình.</param>
        public static void WriteToLog(string message, string logFile = "V6Log")
        {
            try
            {
                Logger lg = new Logger();
                lg.WriteLog(lg.logDir, logFile, message);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Ghi log với thời gian (của máy đang chạy)
        /// </summary>
        /// <param name="address">ClientName + class.Method.</param>
        /// <param name="ex">Exception</param>
        /// <param name="lastActions"></param>
        /// <param name="logFile">Tên file log. Thường dùng tên chương trình.</param>
        public static void WriteExLog(string address, Exception ex, string lastActions, string logFile = "V6Log")
        {
            try
            {
                var message = address
                    + "\r\nExceptionType: " + ex.GetType()
                    + "\r\nExceptionMessage: " + ex.Message
                    + "\r\nStackTrace: " + ex.StackTrace
                    + (string.IsNullOrEmpty(lastActions) ? "" : "\r\nLastActions:" + lastActions);

                Logger lg = new Logger();
                lg.WriteLog(lg.logDir, logFile, message);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Ghi log vào file
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="message"></param>
        /// <param name="dir"></param>
        public void WriteLog(string dir, string logName, string message)
        {
            logDir = dir;
            logFile = logName;

            messageToWrite = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
            WriteLogT();
        }
        public void WriteLog(string dir, string logName, string message, int keepDays, int deleteOldDayDeep)
        {
            logDir = dir;
            logFile = logName;

            _keepDays = keepDays;
            _deleteDeep = deleteOldDayDeep;
            messageToWrite = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
            var T = new Thread(WriteLogT) {IsBackground = true};
            T.Start();
        }
        void WriteLogT()
        {
            
            try
            {
                string todayLogFile = Path.Combine(logDir, logFile + DateTime.Now.ToString("yyyyMMdd") + ".log");
                fs = new FileStream(todayLogFile, FileMode.Append);
                
                sw = new StreamWriter(fs);
                sw.WriteLine(messageToWrite);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally // delete old logfile
            {
                var t = new Thread(DeleteOldLog) {IsBackground = true};
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
                    oldDate = DateTime.Now.AddDays(i);
                    oldLogFile = Path.Combine(logDir, logFile + oldDate.ToString("yyyyMMdd") + ".log");
                    if (File.Exists(oldLogFile)) File.Delete(oldLogFile);
                }
            }
            catch
            {
                // ignored
            }
        }

    }
}

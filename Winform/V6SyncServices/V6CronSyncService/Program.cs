using System;
using System.ServiceProcess;
using V6Tools;

namespace V6CronSyncService
{
    static class Program
    {
        public static string ProducName = "V6CronSyncService";
        public static string __dir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

        public static void WriteToLog(string log)
        {
            Logger.WriteToLog(log, ProducName);
        }
        
        public static void WriteExLog(string address, Exception ex, string lastAction = "")
        {
            Logger.WriteExLog(address, ex, lastAction, ProducName);
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new V6CronSyncService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

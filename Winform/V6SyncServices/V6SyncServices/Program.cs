using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using V6Init;
using V6Tools;


namespace V6SyncServices
{
    static class Program
    {
        public static string __dir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
        const string __logName = "V6SSlog";
        static Logger _log = new Logger(__dir, __logName);
        public static void Log(string message)
        {
            _log.WriteLog(__dir, __logName, message);
        }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var seri = License.GetSeri(__dir);
            Log("Seri:" +seri);
            if (seri == "")
            {
                Log("return");
                return;
            }

            var key = License.ReadLicenseKey(__dir);
            Log("Key:" + key);

            if (License.CheckLicenseKey(seri, key))
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new SyncService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}

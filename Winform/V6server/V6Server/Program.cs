using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using V6Init;

namespace V6Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var localPath = Application.StartupPath;
            var seri = "!";
                seri = License.GetSeri(Application.StartupPath);

            var key = License.ReadLicenseKey();
            
            if (License.CheckLicenseKey(seri, key))
            {
                Application.Run(new Form1());
            }
            
        }
    }
}

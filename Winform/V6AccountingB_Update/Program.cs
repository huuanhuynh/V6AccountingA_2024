using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace V6AccountingB_Update
{
    static class Program
    {
        //public static readonly string V6SoftLocalAppData_Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "V6Soft");
        public static readonly string StartupFolder = Application.StartupPath;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (args.Length > 0)
            {
                string ftp_folder = args[0];
                string update_available_file = args[1];
                
                var form = new FormUpdate();
                form.ftp_folder = ftp_folder;
                form.update_available_file = update_available_file;
                Application.Run(form);
            }
            else
            {
                Application.Run(new FormCreateUpdate());
            }
        }
    }
}

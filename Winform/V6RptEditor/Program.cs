using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace V6RptEditor
{
    static class Program
    {
        public static string[] _args;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            _args = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormRptEditor());
        }
    }
}

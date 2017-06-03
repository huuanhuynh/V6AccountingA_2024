using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace V6MDBtoSQLthread
{
    static class Program
    {
        #region ==== 1 ====
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    //Application.SetCompatibleTextRenderingDefault(false);
        //    FormMain fm = new FormMain();
        //    if (fm.CheckShowOnStartUp())
        //    {
        //        fm.Visible = true;
        //    }
        //    else
        //    {
        //        fm.Visible = false;
        //    }            
            
        //    SingleInstance.SingleApplication.Run(fm);
        //}
        #endregion ==== 1 ====


        #region ==== 3 ====
        public static string MyAppID = "\"V6MDBtoSQL-Anhh\"";
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //biến kiểm tra
            bool IsFirstTime;
            //tạo đối tượng mutex mới
            using (Mutex Checker = new Mutex(true, MyAppID, out IsFirstTime))
            {   
                if (IsFirstTime)
                {
                    if (CheckRunOne())
                    {
                        FormMain frm = new FormMain();
                        frm.Visible = V6SyncUtility.CheckShowOnStartUp();
                        SingleApplication.Run(frm);
                    }
                    else
                    {
                        MessageBox.Show("Đã có 1 tiến trình của ứng dụng đang chạy!\nChương trình sẽ thoát!");
                    }
                }
                else
                {
                    SingleApplication.SwitchToCurrentInstance();
                }
                
            }
        }

        static bool CheckRunOne()
        {
            string cp = Process.GetCurrentProcess().ProcessName;
            Process[] ps = Process.GetProcesses();
            byte count = 0;
            foreach (Process p in ps)
            {
                //MessageBox.Show(p.ProcessName);
                if (cp.Contains(p.ProcessName)) count++;
            }
            if (count > 1)
            {   
                return false;
            }
            return true;
        }
        #endregion ==== 3 ====

    }
}

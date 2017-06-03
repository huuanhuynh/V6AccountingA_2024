using System;
using System.Data;
using V6ThreadLibrary;

namespace V6CronSyncService.Tasks
{
    public class Task0 : BaseTask
    {
        protected override void CreateThreads()
        {
            try
            {
                CreateThreads("0");
                //MultiThreads = new MyThreads(_Setting);

                //if (dvServerList != null)
                //{
                //    DataTable dtbSLt = dvServerList.ToTable();
                //    for (int i = 0; i < dvServerList.Count; i++)
                //    {
                //        if (dtbSLt.Rows[i]["Run"].ToString() == "1"
                //            && dtbSLt.Rows[i]["TaskType"].ToString() == "0")
                //        {
                //            MultiThreads.Add(new MyThread(_Setting, i, dtbSLt.Rows[i][1].ToString(), dtbSLt.Rows[i]));
                //        }
                //    }
                //}
                //else
                //{
                //    Program.WriteToLog("Chua co cau hinh server nao! StopThread");
                //}
            }
            catch (Exception ex)
            {
                Program.WriteToLog(GetType() + ".CreateThreads error " + ex.Message);
            }
        }
    }
}

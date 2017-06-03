using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace V6CronSyncService
{
    public partial class V6CronSyncService : ServiceBase
    {
        ITaskScheduler _scheduler;
        public V6CronSyncService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Program.WriteToLog("Start Service");
            _scheduler = new TaskScheduler();
            _scheduler.Run();
        }

        protected override void OnStop()
        {
            Program.WriteToLog("Stop Service");
            if (_scheduler != null)
            {
                _scheduler.Stop();
            }
        }
    }
}

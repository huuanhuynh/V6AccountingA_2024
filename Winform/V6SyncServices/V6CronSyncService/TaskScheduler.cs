using System.Collections.Generic;
using System.Configuration;
using Quartz;
using Quartz.Impl;
using V6CronSyncService.Tasks;

namespace V6CronSyncService
{
    public class TaskScheduler : ITaskScheduler
    {
        private IScheduler _scheduler;
        public string Name
        {
            get { return GetType().Name; }
        }

        public void Run()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();
            var dictionary = new Dictionary<IJobDetail, Quartz.Collection.ISet<ITrigger>>();

            string taskGroup = "V6TaskGroup";
            
            Program.WriteToLog("Create TaskList");
            for (int i = 0; i < 10; i++)
            {
                var task_name = "Task" + i;
                IJobDetail taskJob = JobBuilder.Create<BaseTask>()
                    .WithIdentity(task_name, taskGroup)
                    .Build();
                switch (i)
                {
                    case 0: taskJob = JobBuilder.Create<Task0>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 1: taskJob = JobBuilder.Create<Task1>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 2: taskJob = JobBuilder.Create<Task2>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 3: taskJob = JobBuilder.Create<Task3>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 4: taskJob = JobBuilder.Create<Task4>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 5: taskJob = JobBuilder.Create<Task5>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 6: taskJob = JobBuilder.Create<Task6>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 7: taskJob = JobBuilder.Create<Task7>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 8: taskJob = JobBuilder.Create<Task8>().WithIdentity(task_name, taskGroup).Build(); break;
                    case 9: taskJob = JobBuilder.Create<Task9>().WithIdentity(task_name, taskGroup).Build(); break;
                }
                ITrigger taskTrigger = TriggerBuilder.Create()
                        .WithIdentity(task_name, taskGroup)
                        .StartNow()
                        .WithCronSchedule(ConfigurationManager.AppSettings[task_name])
                        .Build();

                dictionary.Add(taskJob, new Quartz.Collection.HashSet<ITrigger>{taskTrigger});
            }
            //IJobDetail syncJob = JobBuilder.Create<Task0>()
            //        .WithIdentity("Sync", taskGroup)
            //        .Build();
            //ITrigger testTrigger = TriggerBuilder.Create()
            //        .WithIdentity("Sync", taskGroup)
            //        .StartNow()
            //        .WithCronSchedule(ConfigurationManager.AppSettings["Task0"])
            //        .Build();
            
            //IJobDetail backupJob = JobBuilder.Create<Task1>()
            //        .WithIdentity("Backup", taskGroup)
            //        .Build();
            //ITrigger testTrigger2 = TriggerBuilder.Create()
            //        .WithIdentity("Backup", taskGroup)
            //        .StartNow()
            //        .WithCronSchedule(ConfigurationManager.AppSettings["Task1"])
            //        .Build();

            //dictionary.Add(syncJob, new Quartz.Collection.HashSet<ITrigger>()
            //                    {
            //                        testTrigger
            //                    });
            
            //dictionary.Add(backupJob, new Quartz.Collection.HashSet<ITrigger>()
            //                    {
            //                        testTrigger2
            //                    });


            _scheduler.ScheduleJobs(dictionary, false);
            _scheduler.Start();
        }

        public void Stop()
        {
            _scheduler.Shutdown();
        }
    }
}

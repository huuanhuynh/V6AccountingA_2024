using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using V6Tools;

namespace V6ThreadLibrary
{
    public enum MyThreadStatus
    {
        None,
        Error,
        Run,
        Stop,
        Finish
    }
    #region ===== Quản lý đa tiến trình =====
    /// <summary>
    /// Đa tiến trình
    /// </summary>
    public class MyThreads : List<MyThread>
    {
        public Logger _log;
        public V6SyncSetting _setting;
        public string __dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public const string __logName = "V6ThreadLog";
        public bool IsInTime { get; set; }

        protected void Log(string message)
        {
            if(_log == null) _log = new Logger(__dir, __logName);
            _log.WriteLog(__dir, __logName, message);
        }

        public MyThreadStatus Status = MyThreadStatus.None;
        public string Message = "";

        private Thread vthread_queue;

        public MyThreads(V6SyncSetting setting)
        {
            _setting = setting;
        }

        public void Start()
        {   
            try
            {
                //if(vthread_queue == null)
                vthread_queue = new Thread(RunQueue);
                
                Status = MyThreadStatus.Run;// 1;
                Message = "Start Multi.";
                vthread_queue.Start();
                Message = "MultiThread has been starting.";
                
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Status = MyThreadStatus.Error;// -2;
            }
        }
        public void Stop()
        {
            vthread_queue.Abort();
            Status = MyThreadStatus.Stop;// -3;
            Message = "Stopping MultiThread.";
            foreach (MyThread item in this)
            {
                item.Stop();
            }
            Message = "MultiThread has stoped by user.";
        }
        
        void RunQueue()
        {
            try
            {
                foreach (MyThread item in this)
                {
                    item.IsInTime = IsInTime;
                    Message = item._ThreadName + " is running.";
                    item.Run();
                }
                Message = "" + this.Count + " thread(s) finish!";
                this.Status = MyThreadStatus.Finish;
            }
            catch (Exception ex)
            {
                Message = "RunQueue error: " + ex.Message;
                Log(Message);
            }
        }
    }
    
    #endregion End Quản lý đa tiến trình

    #region ==== Base Thread ====
    /// <summary>
    /// Một tiến trình đơn.
    /// </summary>
    public abstract class BaseThread
    {
        protected V6SyncSetting _setting;
        public Logger _log;
        public string __dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public const string __logName = "V6ThreadLog";
        public bool IsInTime { get; set; }

        protected void Log(string message)
        {
            _log.WriteLog(__dir, __logName, message);
        }

        #region ==== Var ====
        private Thread _1thread;
        public string _ThreadName = "";
        public int _Index = 0;
        /// <summary>
        /// 1 run,-1 finish,-2 error
        /// </summary>
        public int _Status = 0;
        public int _Value = 0;
        public bool _Error = false;
        public bool _HaveLog = false;
        /// <summary>
        /// Gán thông báo
        /// </summary>
        public string _Message = "";
        protected static Random r = new Random();
        #endregion ==== Var ====

        #region ==== Khởi tạo đối tượng ====
        public BaseThread() { }
        public BaseThread(string Name, int index)
        {
            _ThreadName = Name;
            _Index = index;
        }
        #endregion

        #region ==== Events ====

        public delegate void ThrowException(Exception ex);

        public event ThrowException ThrowExceptionEvent;
        protected virtual void OnThrowExceptionEvent(Exception ex)
        {
            var handler = ThrowExceptionEvent;
            if (handler != null) handler(ex);
        }
        #endregion events

        #region ==== Start - Stop ====
        /// <summary>
        /// Khởi chạy tiến trình.
        /// </summary>
        public void Start()
        {
            try
            {
                _1thread = new Thread(Run);
                _Message = "Start one.";
                _1thread.Start();
            }
            catch { }
        }
        /// <summary>
        /// Dừng tiến trình.
        /// </summary>
        public void Stop()
        {
            try
            {
                _1thread.Abort();
            }
            catch { }
        }
        #endregion

        ///////////////////////////////////////////
        ///////////////////////////////////////////
        ///////////////////////////////////////////
        #region ==== Body ====
        //=====================================
        /// <summary>
        /// Hàm hoạt động chính của tiến trình.
        /// </summary>
        public void Run()
        {
            //Hàm viết ở đây
            //Hàm này không được có tham số
            try
            {
                _Error = false;
                _Status = 1;
                VoidMain();
                _Status = -1;//finish
            }
            catch (Exception ex)
            {
                _Status = -2;
                this._Error = true;
                _Message = ex.Message;
            }
        }
        #region ==== Hàm sẽ được sửa đổi ở lớp kế thừa ====
        protected abstract void VoidMain();
        #endregion
        #endregion
        ///////////////////////////////////////////
        ///////////////////////////////////////////
        ///////////////////////////////////////////
        
    }
    #endregion
}

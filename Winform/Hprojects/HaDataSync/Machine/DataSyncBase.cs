using System;
using System.Collections.Generic;
using H_DatabaseAccess;
using H_Utility.Converter;
using H_Utility.Helper;

//using Microsoft.Win32.SafeHandles;

namespace HaDataSync.Machine
{
    public class DataSyncBase// : IDisposable
    {
        public DataSyncBase(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
        {
            DBA = dba;
            SyncDownOn = syncDown;
            SyncUpOn = syncUp;
        }

        public event EventHandler SyncCompleteSuccess;
        protected virtual void OnSyncCompleteSuccess()
        {
            var handler = SyncCompleteSuccess;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public delegate void DataHandler(IDictionary<string,object> data, IDictionary<string,object> keys);
        public event DataHandler InsertSuccess;
        protected virtual void OnInsertSuccess(IDictionary<string,object> data, IDictionary<string,object> keys)
        {
            var handler = InsertSuccess;
            if (handler != null) handler(data, keys);
        }
        public event DataHandler InsertFailed;
        protected virtual void OnInsertFailed(IDictionary<string,object> data, IDictionary<string,object> keys)
        {
            var handler = InsertFailed;
            if (handler != null) handler(data, keys);
        }
        public event DataHandler UpdateSuccess;
        protected virtual void OnUpdateSuccess(IDictionary<string,object> data, IDictionary<string,object> keys)
        {
            var handler = UpdateSuccess;
            if (handler != null) handler(data, keys);
        }
        public event DataHandler UpdateFailed;
        protected virtual void OnUpdateFailed(IDictionary<string,object> data, IDictionary<string,object> keys)
        {
            var handler = UpdateFailed;
            if (handler != null) handler(data, keys);
        }

        protected DatabaseAccess DBA { get; set; }
        protected string TableName { get; set; }
        protected string CheckField { get; set; }
        public bool SyncDownOn { get; set; }
        public bool SyncUpOn { get; set; }

        public int CompletePercent
        {
            get
            {
                int total = 0;
                if (SyncDownOn) total += 100;
                if (SyncUpOn) total += 100;

                int complete = (SyncDownOn ? CompletePercentDown : 0) + (SyncUpOn ? CompletePercentUp : 0);
                int result = complete*100/total;
                return result;
            }
        }

        protected int CompletePercentDown { get; set; }
        protected int CompletePercentUp { get; set; }
        public bool Finish { get; set; }
        public bool FinishSuccess { get; set; }
        public string Message { get; set; }

        public void RunSync()
        {
            Finish = false;
            CompletePercentDown = 0;
            try
            {
                DoSync();
                
                FinishSuccess = true;
                try
                {
                    OnSyncCompleteSuccess();
                }
                catch (Exception ex)
                {
                    Logger.WriteToLog("DataSyncBase Call SyncCompleteSuccess event " + ex.Message, "HaDataSync");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("DataSyncBase RunSync " + ex.Message, "HaDataSync");
                FinishSuccess = false;
            }
            Finish = true;
        }

        private void DoSync()
        {
            if (SyncDownOn) DoSyncDown();
            if (SyncUpOn) DoSyncUp();
            CompletePercentDown = 100;
        }

        protected virtual void DoSyncDown()
        {
            
        }
        
        protected virtual void DoSyncUp()
        {
            
        }

        protected bool IsExist(string tableName, string field, string value)
        {
            var sql = SqlGenerator.GenCountSql(tableName, field, value);
            var count = PrimitiveTypes.ObjectToInt(DBA.ExecuteScalar(sql));
            return count > 0;
        }

        protected bool IsExist(string tableName, string field, int value)
        {
            var sql = SqlGenerator.GenCountSql(tableName, field, value);
            var count = PrimitiveTypes.ObjectToInt(DBA.ExecuteScalar(sql));
            return count > 0;
        }

        protected void CallAfterInsertEvent(IDictionary<string,object> data, bool success)
        {
            try
            {
                if (success) OnInsertSuccess(data, null);
                else OnInsertFailed(data, null);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("ThuocSync DoSync AfterInsertEvent " + ex.Message, "HaDataSync");
            }
        }
        protected void CallAfterUpdateEvent(IDictionary<string,object> data,IDictionary<string,object> keys, bool success)
        {
            try
            {
                if (success) OnUpdateSuccess(data, keys);
                else OnUpdateFailed(data, keys);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("ThuocSync DoSync AfterUpdateEvent " + ex.Message, "HaDataSync");
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}{2}", TableName, SyncUpOn?"Up ":"", SyncDownOn?"Down":"");
        }

        //#region ==== IDisposable Implementation ====

        //public bool IsDisposed
        //{
        //    get { return _disposed; }
        //}
        //bool _disposed = false;
        //// Instantiate a SafeHandle instance.
        //readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        //// Public implementation of Dispose pattern callable by consumers.
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //// Protected implementation of Dispose pattern.
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (_disposed)
        //        return;

        //    if (disposing)
        //    {
        //        _handle.Dispose();
        //        // Free any other managed objects here.
        //        //
        //    }

        //    // Free any unmanaged objects here.
        //    //
        //    _disposed = true;
        //}
        //#endregion dispose implement

        
    }
}

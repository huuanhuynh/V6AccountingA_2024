using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using H_DatabaseAccess;
using H_Utility.Helper;

namespace HaDataSync.Service
{
    public class HaSyncService
    {
        private DatabaseAccess DBA { get; set; }
        public bool IsRunning { get { return _running; } }

        public HaSyncService(DatabaseAccess dba)
        {
            DBA = dba;
        }

        #region ==== Config properties (public) ====
        public string Name = "HaSyncService";
        /// <summary>
        /// Thời gian nghỉ sau khi chạy xong một lượt.
        /// </summary>
        public int PauseTime = 1800;//s

        public bool SyncBenhNhanOn = true;
        public bool SyncBenhNhanDown = true;
        public bool SyncBenhNhanLen = true;
        
        public bool SyncLichKhamOn = true;
        public bool SyncLichKhamXuong = true;
        public bool SyncLichKhamLen = true;

        public bool SyncLoaiSieuAmOn = true;
        public bool SyncLoaiSieuAmXuong = true;
        public bool SyncLoaiSieuAmLen = true;
        
        public bool SyncThuocOn = true;
        public bool SyncThuocXuong = true;
        public bool SyncThuocLen = true;

        public bool SyncToaThuocOn = true;
        public bool SyncToaThuocXuong = true;
        public bool SyncToaThuocLen = true;

        #endregion config

        
        private bool _running = false;
        private bool _pausing = false;
        private int _pauseTimeCount = 0;
        public bool _flagRun = false;

        public void StartSync()
        {
            try
            {
                if (_running) return;

                Thread T = new Thread(RunSyncThread);
                T.IsBackground = true;
                _flagRun = true;
                T.Start();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("StartSync " + ex.Message, Name);
            }
        }

        public void StopSync()
        {
            _flagRun = false;
        }

        private void RunSyncThread()
        {
            Logger.WriteToLog("RunSyncThread Start", Name);
            while (_flagRun)
            {
                if (_pausing)
                {
                    _running = false;
                    Thread.Sleep(1000);
                    _pauseTimeCount++;
                    if (_pauseTimeCount >= PauseTime)
                    {
                        _pausing = false;
                    }
                }
                else
                {
                    _running = true;
                    DoSync();
                    _pausing = true;
                    _pauseTimeCount = 0;
                }
            }
            _running = false;
            Logger.WriteToLog("RunSyncThread End", Name);
        }

        private void DoSync()
        {
            try
            {
                SyncBenhNhan();
                SyncLichKham();
                SyncLoaiSieuAm();
                SyncThuoc();
                SyncToaThuoc();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("DoSync " + ex.Message, Name);
            }
        }

        private void SyncToaThuoc()
        {
            if (SyncToaThuocOn)
            {
                SyncManager.DongBoToaThuoc(DBA, SyncToaThuocXuong, SyncToaThuocLen);
            }
        }

        private void SyncThuoc()
        {
            if (SyncThuocOn)
            {
                SyncManager.DongBoThuoc(DBA, SyncThuocXuong, SyncThuocLen);
            }
        }

        private void SyncLoaiSieuAm()
        {
            if (SyncLoaiSieuAmOn)
            {
                SyncManager.DongBoLoaiSieuAm(DBA, SyncLoaiSieuAmXuong, SyncLoaiSieuAmLen);
            }
        }

        private void SyncLichKham()
        {
            if (SyncLichKhamOn)
            {
                SyncManager.DongBoLichKhamBenh(DBA, SyncLichKhamXuong, SyncLichKhamLen);
            }
        }

        private void SyncBenhNhan()
        {
            if (SyncBenhNhanOn)
            {
                SyncManager.DongBoThuoc(DBA, SyncBenhNhanDown, SyncBenhNhanLen);
            }
        }
    }
}

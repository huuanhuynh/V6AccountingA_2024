using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaDataSync.Machine;
using H_DatabaseAccess;

namespace HaDataSync
{
    public class SyncManager
    {
        public static bool DongBoLoaiSieuAm(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
        {
            LoaiSieuAmSync sync = new LoaiSieuAmSync(dba, syncDown, syncUp);
            sync.RunSync();
            if (sync.FinishSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DongBoDanhSachBenhNhan(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
        {
            BenhNhanSync sync = new BenhNhanSync(dba, syncDown, syncUp);
            sync.RunSync();
            if (sync.FinishSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DongBoLichKhamBenh(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
        {
            LichKhamSync sync = new LichKhamSync(dba, syncDown, syncUp);
            sync.RunSync();
            if (sync.FinishSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DongBoThuoc(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
        {
            ThuocSync sync = new ThuocSync(dba, syncDown, syncUp);
            sync.RunSync();
            if (sync.FinishSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DongBoToaThuoc(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
        {
            ToaThuocSync sync = new ToaThuocSync(dba);
            sync.RunSync();
            if (sync.FinishSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using H_DatabaseAccess;
using H_Utility.Converter;
using H_Utility.Helper;
using xAdapter.ModelStrure;
using xAdapter.NhaBaoSanhApi;

namespace HaDataSync.Machine
{
    public class LoaiSieuAmSync : DataSyncBase
    {
        public LoaiSieuAmSync(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
            : base(dba, syncDown, syncUp)
        {
            TableName = "LoaiSieuAm";
            CheckField = "MALOAISIEUAM";
        }

        protected override void DoSyncDown()
        {
            try
            {
                ApiLoaiSieuAm api = new ApiLoaiSieuAm();
                var listLoaiSieuAm = api.GetList();
                CompletePercentDown = 45;
                var total = listLoaiSieuAm.data.Count;
                var syncCount = 0;
                foreach (ModelLoaiSieuAm benhNhan in listLoaiSieuAm.data)
                {
                    try
                    {
                        var maserver = benhNhan.id;
                        var benhNhanData = benhNhan.ToDic();
                        //Fix data
                        benhNhanData[CheckField] = maserver;
                    
                        //Kiem tra neu masever co roi thi update, neu chua co thi insert.
                        if (IsExist(TableName, CheckField, maserver))
                        {
                            IDictionary<string, object> keys = new Dictionary<string, object>();
                            keys.Add(CheckField, maserver);
                            var a = DatabaseAccessHelper.Update(DBA, TableName, benhNhanData, keys);
                        }
                        else
                        {
                            var a = DatabaseAccessHelper.Insert(DBA, TableName, benhNhanData);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteToLog("BenhNhanSync DoSync each " + ex.Message, "HaDataSync");
                    }


                    syncCount++;
                    CompletePercentDown = syncCount * 55 / total;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BenhNhanSync DoSyncDown " + ex.Message, "HaDataSync");
            }
        }

        
    }
}

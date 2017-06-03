using System;
using System.Collections.Generic;
using H_DatabaseAccess;
using H_Utility.Converter;
using H_Utility.Helper;
using xAdapter.ModelStrure;
using xAdapter.NhaBaoSanhApi;

namespace HaDataSync.Machine
{
    public class ToaThuocSync : DataSyncBase
    {
        public ToaThuocSync(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
            : base(dba, syncDown, syncUp)
        {
            TableName = "ToaThuoc";
            CheckField = "MATOATHUOC";
        }

        protected override void DoSyncDown()
        {
            try
            {
                ApiToaThuoc api = new ApiToaThuoc();
                var getResult = api.GetList();
                CompletePercentDown = 45;
                var total = getResult.data.Count;
                var syncCount = 0;
                foreach (ModelToaThuoc item in getResult.data)
                {
                    try
                    {
                        var maserver = item.id;
                        var benhNhanData = item.ToDic();
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
                        Logger.WriteToLog("ToaThuocSync DoSync each " + ex.Message, "HaDataSync");
                    }
                
                    syncCount++;
                    CompletePercentDown = syncCount*55/total;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("ToaThuocSync DoSyncDown " + ex.Message, "HaDataSync");

            }
        }

        
    }
}

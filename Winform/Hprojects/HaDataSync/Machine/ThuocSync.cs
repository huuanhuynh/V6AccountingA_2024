using System;
using System.Collections.Generic;
using H_DatabaseAccess;
using H_Utility.Converter;
using H_Utility.Helper;
using xAdapter.ModelStrure;
using xAdapter.NhaBaoSanhApi;

namespace HaDataSync.Machine
{
    public class ThuocSync : DataSyncBase
    {
        public ThuocSync(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
            : base(dba, syncDown, syncUp)
        {
            TableName = "Thuoc";
            CheckField = "MATHUOC";
        }

        protected override void DoSyncDown()
        {
            try
            {
                ApiThuoc api = new ApiThuoc();
                var getResult = api.GetList();
                CompletePercentDown = 45;
                var total = getResult.data.Count;
                var syncCount = 0;
                foreach (ModelThuoc item in getResult.data)
                {
                    try
                    {
                        var maserver = item.id;
                        var itemData = item.ToDic();
                        //Fix data
                        itemData["MATHUOC"] = maserver;
                    
                        //Kiem tra neu masever co roi thi update, neu chua co thi insert.
                        if (IsExist(TableName, CheckField, maserver))
                        {
                            IDictionary<string, object> keys = new Dictionary<string, object>();
                            keys.Add(CheckField, maserver);
                            var a = DatabaseAccessHelper.Update(DBA, TableName, itemData, keys);
                            CallAfterUpdateEvent(itemData, keys, a > 0);
                        }
                        else
                        {
                            var a = DatabaseAccessHelper.Insert(DBA, TableName, itemData);
                            CallAfterInsertEvent(itemData, a > 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteToLog("ThuocSync DoSync each " + ex.Message, "HaDataSync");
                    }
                

                    syncCount++;
                    CompletePercentDown = syncCount*55/total;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("ThuocSync DoSyncDown " + ex.Message, "HaDataSync");

            }
        }

        
    }
}

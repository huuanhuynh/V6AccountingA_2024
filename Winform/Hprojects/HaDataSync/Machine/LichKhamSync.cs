using System;
using System.Collections.Generic;
using H_DatabaseAccess;
using H_Utility.Converter;
using H_Utility.Helper;
using xAdapter.ModelStrure;
using xAdapter.NhaBaoSanhApi;

namespace HaDataSync.Machine
{
    public class LichKhamSync : DataSyncBase
    {
        public LichKhamSync(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
            : base(dba, syncDown, syncUp)
        {
            TableName = "LichKhamBenh";
            CheckField = "MASERVER";
        }

        protected override void DoSyncDown()
        {
            try
            {
                ApiLichKhamBenh api = new ApiLichKhamBenh();
                var getResult = api.GetList();
                CompletePercentDown = 45;
                var total = getResult.data.Count;
                var syncCount = 0;
                foreach (ModelLichKhamBenh item in getResult.data)
                {
                    try
                    {
                        var maserver = item.id;
                        var itemData = item.ToDic();
                        //Fix data
                        itemData[CheckField] = maserver;
                    
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
                            var a = DatabaseAccessHelper.Insert(DBA, TableName, itemData, new List<string> { "MALICHKHAMBENH" });
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

using System;
using System.Collections.Generic;
using H_DatabaseAccess;
using H_Utility.Converter;
using H_Utility.Helper;
using xAdapter.ModelStrure;
using xAdapter.NhaBaoSanhApi;

namespace HaDataSync.Machine
{
    public class BenhNhanSync : DataSyncBase
    {
        public BenhNhanSync(DatabaseAccess dba, bool syncDown = true, bool syncUp = false)
            : base(dba, syncDown, syncUp)
        {
            TableName = "BenhNhan";
            CheckField = "MASERVER";
        }

        protected override void DoSyncDown()
        {
            try
            {
                ApiBenhNhan api = new ApiBenhNhan();
                var listBenhNhan = api.GetList();
                CompletePercentDown = 45;
                var total = listBenhNhan.data.Count;
                var syncCount = 0;
                foreach (ModelBenhNhan benhNhan in listBenhNhan.data)
                {
                    try
                    {
                        var maserver = benhNhan.id;
                        var itemData = benhNhan.ToDic();
                        //Fix data
                        itemData[CheckField] = maserver;
                        if (itemData.ContainsKey("GIOITINH"))
                        {
                            if (itemData["GIOITINH"].ToString() == "1")
                            {
                                itemData["GIOITINH"] = "Nam";
                            }
                            else if (itemData["GIOITINH"].ToString() == "0")
                            {
                                itemData["GIOITINH"] = "Nữ";
                            }
                        }

                        //Kiem tra neu masever co roi thi update, neu chua co thi insert.
                        if (IsExist(TableName, CheckField, maserver))
                        {
                            IDictionary<string, object> keys = new Dictionary<string, object>();
                            keys.Add(CheckField, maserver);
                            var a = DatabaseAccessHelper.Update(DBA, TableName, itemData, keys);
                            CallAfterUpdateEvent(itemData, keys, a>0);
                        }
                        else
                        {
                            var a = DatabaseAccessHelper.Insert(DBA, TableName, itemData, new List<string> {"MABENHNHAN"});
                            CallAfterInsertEvent(itemData, a>0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteToLog("BenhNhanSync DoSyncDown each " + ex.Message, "HaDataSync");
                    }

                    syncCount++;
                    CompletePercentDown = syncCount*55/total;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BenhNhanSync DoSyncDown " + ex.Message, "HaDataSync");

            }
        }

        
    }
}

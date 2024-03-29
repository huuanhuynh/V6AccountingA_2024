﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6SqlConnect;
//using System.Threading.Tasks;

namespace V6Init
{
    public static class V6Menu
    {
        public static DataTable ModuleTable
        {
            get
            {
                DataTable data = SqlConnect.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1", "module_id", "module_id").Data;
                return data;
            }
        }

        public static DataTable GetMenuTable1(int userid, string moduleID)
        {
            var prList = new List<SqlParameter>();
            string sql = "SELECT * FROM V6MENU WHERE Itemid='A0000000'";
            sql += " AND Module_id=@moduleID AND HIDE_YN<>1";
            sql += " AND V2ID IN (SELECT V2ID FROM V6MENU WHERE Itemid NOT IN('A0000000','B0000000')";
            sql += " AND JOBID<>'B099' AND (1=@isAdmin or dbo.VFA_Inlist_MEMO(ITEMID,@mrights)=1)";
            sql += " AND hide_yn<>1 AND Module_id=@moduleID GROUP BY V2ID) ORDER BY stt_box";
            //.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1 group by module_id", "module_id").Data;
            prList.Add(new SqlParameter("@moduleID",moduleID));
            prList.Add(new SqlParameter("@isAdmin", V6Login.IsAdmin));
            prList.Add(new SqlParameter("@mrights", V6Login.UserRight.Mrights));
            DataTable data = SqlConnect.ExecuteDataset(CommandType.Text, sql, prList.ToArray()).Tables[0];
            
            return data;
        }

        /// <summary>
        /// Menu cấp 2 (Tab)
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="moduleID"></param>
        /// <param name="v2ID"></param>
        /// <returns></returns>
        public static DataTable GetMenuTable2(int userid, string moduleID, string v2ID)
        {
            var prList = new List<SqlParameter>();
            string sql = "SELECT * FROM V6MENU WHERE Itemid='B0000000'";
            sql += "\n AND Module_id=@moduleID AND HIDE_YN<>1";
            sql += "\n AND V2ID =@v2ID AND JOBID<>'B099'";
            sql += "\n AND V2ID+JOBID IN ";
            sql += "\n    (SELECT V2ID+JOBid FROM V6MENU WHERE (((1=@isAdmin or dbo.VFA_Inlist_MEMO(ITEMID,@mrights)=1)) AND hide_yn<>1 AND Module_id=@moduleID )";
            sql += "\n    GROUP BY V2ID,JOBID) ORDER BY stt_box";
            //.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1 group by module_id", "module_id").Data;
            prList.Add(new SqlParameter("@moduleID", moduleID));
            prList.Add(new SqlParameter("@v2ID", v2ID));
            prList.Add(new SqlParameter("@isAdmin", V6Login.IsAdmin));
            prList.Add(new SqlParameter("@mrights", V6Login.UserRight.Mrights));
            DataTable data = SqlConnect.ExecuteDataset(CommandType.Text, sql, prList.ToArray()).Tables[0];
            
            return data;
        }


        public static DataTable GetMenuTable3(int userId, string moduleID, string v2ID, string jobID)
        {
            var prList = new List<SqlParameter>();
            string sql = "SELECT * FROM V6MENU WHERE V2ID=@v2ID AND JOBID=@jobID";
            sql += "\n AND ((nhat_ky='') or (nhat_ky<>'' and program='0000'))";
            sql += "\n AND Itemid NOT IN('A0000000','B0000000')";
            sql += "\n AND V2ID+JOBID+ItemID IN ";
            sql += "\n    (SELECT V2ID+JOBid+ItemID FROM V6MENU WHERE (((1=@isAdmin or dbo.VFA_Inlist_MEMO(ITEMID,@mrights)=1)) AND hide_yn<>1 AND Module_id=@moduleID )";
            sql += "\n    GROUP BY V2ID,JOBID,ItemID) ORDER BY stt_box";
            //.Select("v6menu", "distinct module_id,MAX(vbar) as name", "hide_yn<>1 group by module_id", "module_id").Data;
            prList.Add(new SqlParameter("@moduleID", moduleID));
            prList.Add(new SqlParameter("@v2ID", v2ID));
            prList.Add(new SqlParameter("@jobID", jobID));
            prList.Add(new SqlParameter("@isAdmin", V6Login.IsAdmin));
            prList.Add(new SqlParameter("@mrights", V6Login.UserRight.Mrights));
            DataTable data = SqlConnect.ExecuteDataset(CommandType.Text, sql, prList.ToArray()).Tables[0];
            
            return data;
        }

        public static DataTable GetMenuQuickRun()
        {
            int type = 1;
            SqlParameter[] plist = new []
            {
                new SqlParameter("@ModuleID", V6Options.MODULE_ID), 
                new SqlParameter("@User_id", V6Login.UserId), 
                new SqlParameter("@TYPE", type), 
            };
            DataTable data = SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_GETV6MENU3", plist).Tables[0];
            return data;
        }

        public static DataTable GetKey2Hm2()
        {
            int type = 1;
            var sql = "Select * from V6Menu Where Key2 = @Key2";
            sql += "\n AND V2ID+JOBID+ItemID IN ";
            sql += "\n    (SELECT V2ID+JOBid+ItemID FROM V6MENU WHERE (((1=@isAdmin or dbo.VFA_Inlist_MEMO(ITEMID,@mrights)=1)) AND hide_yn<>1 AND Module_id=@moduleID )";
            sql += "\n    GROUP BY V2ID,JOBID,ItemID) ORDER BY stt_box";
            SqlParameter[] prList = new[]
            {
                new SqlParameter("@Key2", "HM2"), 
                new SqlParameter("@isAdmin", V6Login.IsAdmin), 
                new SqlParameter("@mrights", V6Login.UserRight.Mrights), 
                new SqlParameter("@moduleID", V6Options.MODULE_ID), 
            };
            DataTable data = SqlConnect.ExecuteDataset(CommandType.Text, sql, prList).Tables[0];
            return data;
        }
    }
}

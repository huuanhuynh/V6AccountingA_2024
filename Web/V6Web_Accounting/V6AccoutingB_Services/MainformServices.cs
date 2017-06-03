using System.Data;
using System.Data.SqlClient;
using V6Init;
using V6SqlConnect;

namespace V6AccoutingB_Services
{
    public class MainformServices : IMainformServices
    {
        
        public DataTable GetGrandMenu(int userId, string moduleId)
        {
            return V6Menu.GetMenuTable1(userId, moduleId);
        }

        public DataTable GetParentMenu(int userId, string moduleId, string v2Id)
        {
            return V6Menu.GetMenuTable2(userId, moduleId, v2Id);
        }

        public DataTable GetChildMenu(int userId, string moduleId, string v2Id, string jobId)
        {
            return V6Menu.GetMenuTable3(userId, moduleId, v2Id, jobId);
        }

        public DataTable VPA_V6VIEW_MESSAGE(int userId, string type, string advance1, string advance2, string advance3)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("User_id", userId),
                new SqlParameter("Type", type),
                new SqlParameter("Advance1", advance1),
                new SqlParameter("Advance2", advance2),
                new SqlParameter("Advance3", advance3)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_V6VIEW_MESSAGE", plist).Tables[0];
        }
    }
}

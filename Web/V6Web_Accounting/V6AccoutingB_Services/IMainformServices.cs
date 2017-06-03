using System.Data;

namespace V6AccoutingB_Services
{
    public interface IMainformServices
    {
        DataTable GetGrandMenu(int userId, string moduleId);
        DataTable GetParentMenu(int userId, string moduleId, string v2Id);
        DataTable GetChildMenu(int userId, string moduleId, string v2Id, string jobId);
        DataTable VPA_V6VIEW_MESSAGE(int userId, string type, string advance1, string advance2, string advance3);
    }
}

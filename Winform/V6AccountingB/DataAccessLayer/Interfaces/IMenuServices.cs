using System.Data;

namespace DataAccessLayer.Interfaces
{
    public interface IMenuServices
    {
        DataTable GetGrandMenu(int userId, string moduleId);
        DataTable GetParentMenu(int userId, string moduleId, string v2Id);
        DataTable GetChildMenu(int userId, string moduleId, string v2Id, string jobId);
    }
}

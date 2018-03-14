namespace DataAccessLayer.Interfaces
{
    public interface ILookupService
    {
        string GetValueByTableName(string v_MaFile, string field);
    }
}

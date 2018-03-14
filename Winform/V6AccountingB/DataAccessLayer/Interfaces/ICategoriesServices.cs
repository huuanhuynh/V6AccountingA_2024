using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using V6SqlConnect;
using V6Structs;

namespace DataAccessLayer.Interfaces
{
    public interface ICategoriesServices
    {
        int Insert(int userId, string tableName, SortedDictionary<string, object> data);
        DataTable SelectTable(string tableName);
        V6SelectResult Select(string tableName, string field, string where, string group, string sort, params SqlParameter[] plist);
        V6SelectResult SelectPaging(string tableName, string fields, int page, int size, string where, string sort, bool ascending);
        int Delete(int userId, string tableName, SortedDictionary<string, object> keys, SqlTransaction transaction=null);
        int Update(int userId, string tableName, SortedDictionary<string, object> data, SortedDictionary<string, object> keys, SqlTransaction transaction=null);
        SortedDictionary<string, string> GetHideColumns(string tableName);
        V6TableStruct GetTableStruct(string tableName);
        bool IsValidOneCode_Full(string cInputTable, byte nStatus, string cInputField, string cpInput, string cOldItems);
        bool IsExistOneCode_List(string tables, string field, string value);
    }
}

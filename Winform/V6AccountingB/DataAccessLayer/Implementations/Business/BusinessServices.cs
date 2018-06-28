using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces.Business;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace DataAccessLayer.Implementations.Business
{
    public class BusinessServices
    {
        //public V6SelectResult Select(string tableName, string fields, string where, string group, string sort)
        //{
        //    var result = SqlConnect.Select(tableName, fields, where, group, sort);
        //    return result;
        //}

        //public V6SelectResult SelectPaging(string tableName, string fields, int page, int size, string where, string sort, bool ascending)
        //{
        //    return SqlConnect.SelectPaging(tableName, fields, page, size, where, sort, ascending);
        //}



        
        //public DataSet ExecuteProcedure(string procName, Dictionary<string, string> parameters)
        //{
        //    CheckIdentifier(procName);
        //    List<SqlParameter> plist = new List<SqlParameter>();
        //    foreach (KeyValuePair<string, string> parameter in parameters)
        //    {
        //        plist.Add(new SqlParameter(parameter.Key, parameter.Value));
        //    }
        //    return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, procName, plist.ToArray());
        //}

        //public object ExecuteProcedureScalar(string procName, Dictionary<string, string> parameters)
        //{
        //    CheckIdentifier(procName);
        //    List<SqlParameter> plist = new List<SqlParameter>();
        //    foreach (KeyValuePair<string, string> parameter in parameters)
        //    {
        //        plist.Add(new SqlParameter(parameter.Key, parameter.Value));
        //    }
        //    var result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, procName, plist.ToArray());
        //    return result;
        //}

        //public int ExecuteProcedureNoneQuery(string procName, Dictionary<string, string> parameters)
        //{
        //    CheckIdentifier(procName);
        //    List<SqlParameter> plist = new List<SqlParameter>();
        //    foreach (KeyValuePair<string, string> parameter in parameters)
        //    {
        //        plist.Add(new SqlParameter(parameter.Key, parameter.Value));
        //    }
        //    return SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, procName, plist.ToArray());
        //}



    }
}

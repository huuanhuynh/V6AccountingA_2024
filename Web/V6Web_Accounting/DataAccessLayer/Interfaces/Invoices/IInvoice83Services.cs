using System;
using System.Collections.Generic;
using System.Data;
using V6Structs;

namespace DataAccessLayer.Interfaces.Invoices
{
    public interface IInvoice83Services
    {
        bool InsertInvoice(int userId, V6TableStruct AMStruct, V6TableStruct ADStruct, V6TableStruct AD3Struct,
            SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList, List<SortedDictionary<string, object>> adList3,
            out string message, bool post);
        bool UpdateInvoice(int userId, V6TableStruct AMStruct, V6TableStruct ADStruct, V6TableStruct AD3Struct,
            SortedDictionary<string, object> am,
            List<SortedDictionary<string, object>> adList,
             List<SortedDictionary<string, object>> adList3,
            SortedDictionary<string, object> keys,
            out string message, bool post);
        DataTable SearchAM(string tableNameAM, string tableNameAD, string mact,
            string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs);
        DataTable LoadAd(string AD, string sttRec);
        DataTable LoadAD3(string tableName, string sttRec);
        bool DeleteInvoice(int userId, string mact, string sttrec);
        DataTable GetGiaBan(string field, string mact, DateTime ngayct, string mant, string mavt, string dvt1, string makh, string magia);
        DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct);
        DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct);
        DataTable GetStock(string mact, string mavt, string makho, string sttRec, DateTime ngayct);
    }
}

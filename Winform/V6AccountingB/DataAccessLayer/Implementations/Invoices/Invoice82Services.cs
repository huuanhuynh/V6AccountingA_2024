using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Transactions;
using DataAccessLayer.Interfaces.Invoices;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace DataAccessLayer.Implementations.Invoices
{
    public class Invoice82Services
    {
        
        public DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }

        public DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            malo = malo.Replace("'", "''");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_lo = '"+malo+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }

        public DataTable GetStock(string mact, string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@Type", 1),
                new SqlParameter("@Ngay_ct", ngayct),
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@Stt_rec", sttRec),
                new SqlParameter("@Advance", string.Format("a.MA_VT='"+mavt+"' AND a.MA_KHO='"+makho+"'")),
                new SqlParameter("@OutputInsert", "")
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CheckTonXuatAm", plist).Tables[0];
        }
    }
}
